var button = document.getElementById("button1");
var textarea = document.getElementById("ta1");

window.addEventListener("DOMContentLoaded", function(evt){
    console.log("dom loaded"); tryGetCombatState();}, false);

button.onclick = handleState();

function handleState(){
    textarea.value = "";
    textarea.value = lastRaidState.raidCode + '\n' + lastRaidState.enemies[0].name.ja +'\n' + lastRaidState.enemies[0].name.en;
    console.log("click");
}