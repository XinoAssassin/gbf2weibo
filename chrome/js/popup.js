var button = document.getElementById("button1");
var textarea = document.getElementById("ta1");

window.addEventListener("DOMContentLoaded", function(evt){
    console.log("dom loaded"); tryGetCombatState();}, false);

button.addEventListener('click',function(){
    handleState();
},false);

function handleState(){
    textarea.value = lastRaidState.raidCode + lastRaidState.enemies[0].name.ja + lastRaidState.enemies[0].name.en
}