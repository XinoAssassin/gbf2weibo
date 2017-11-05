var apiUrl = "chrome-extension://fgpokpknehglcioijejfeebigdnbnokj/content/api.html";
var isApiLoaded = false;
var apiHost = null;
var pendingRequests = {};
var nextRequestId = 1;

var lastRaidState = null;

var textarea = document.getElementById("ta1");

window.addEventListener("DOMContentLoaded", onLoad, false);

function onLoad () {
    window.addEventListener("message", onMessage, false);
    tryLoadApi();
};

function tryLoadApi () {
    console.log("Loading API");
    apiHost = document.querySelector("iframe#api_host");
    apiHost.addEventListener("load", onApiLoaded, false);
    apiHost.src = apiUrl;
};

function onApiLoaded () {
    console.log("API loaded");
    isApiLoaded = true;
    if(isApiLoaded)
    {
        tryGetCombatState();
    }
};

function onMessage (evt) {
    if (evt.data.type !== "result")
        return;

    if (evt.data.result && evt.data.result.error) {
        console.log("Request failed", evt.data.result.error);
        return;
    } else {
        console.log("Got request response", evt.data);
    }

    var callback = pendingRequests[evt.data.id];
    if (!callback)
        return;

    callback(evt.data.result);
};

function sendApiRequest (request, callback) {
    if (!isApiLoaded) {
        console.log("API not loaded");
        callback({error: "api not loaded"});
        return;
    }

    console.log("Sending request", request);
    var id = nextRequestId++;
    request.id = id;
    pendingRequests[id] = callback;

    apiHost.contentWindow.postMessage(
        request, "*"
    );
};

function tryGetCombatState () {
    sendApiRequest({type: "getCombatState"}, function (combatState) {
        if (combatState) {
            lastRaidState = combatState;

            textarea.value = lastRaidState.raidCode + " :参戦ID" + '\n' + "参加者募集！" + '\n'+ lastRaidState.enemies[0].name.en;
            textarea.select();
            document.execCommand("copy", false, null);
        } else {
            return;
        }
    });
};
