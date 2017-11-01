var pjd;
var context = window.parent;
function pushBossData() {
    if (!context["stage"])
        {
            console.log("No stage found");
            return;
        }

    pjd = context.stage.pJsnData
    
}

document.addEventListener('DOMContentLoaded', function () {
    chrome.runtime.sendMessage({ greeting: "hello" }, function (response) { console.log(response.farewell); });
})
