
var base = Module.findBaseAddress("DreadHungerServer-Win64-Shipping.exe");
var SpellNum = 3
//SpellManager
var MaxSpellInsn_addr = base.add(0xE5843E);
Interceptor.attach(MaxSpellInsn_addr, {
    onEnter : function(args) {
        this.context.rbx.add(0x228).writeU32(SpellNum);
    }
});


function shuffle(array) {
    var res = [], random;
    while(array.length > 0) {
        random = Math.floor(Math.random() * array.length);
        res.push(array[random]);
        array.splice(random, 1);
    }
    return res;
}
function getArraySize(TArray){
    return TArray.add(8).readU32();
}
function getArrayItemAddr(TArray, size, index){
    var ArrNum = getArraySize(TArray);
    if(index > ArrNum)
        return null;
    return TArray.readPointer().add(index * size);
}
//SetInThrall SpellManager
var SpellManagerSpawn_addr = base.add(0xE4E201);
var UDH_GameInstance_GetInstance = new NativeFunction(base.add(0xD93ED0), 'pointer', ['pointer'], 'win64');
var ADH_SpellManager_SetEquippedSpells = new NativeFunction(base.add(0xE810E0), 'void', ['pointer', 'pointer'], 'win64');
Interceptor.attach(SpellManagerSpawn_addr, {
    onEnter : function(args) {

        var SpellManager = this.context.rax;
        var GameInstance = UDH_GameInstance_GetInstance(SpellManager);
        var ThrallSpells = GameInstance.add(0x440);
        var SpellNum = getArraySize(ThrallSpells);
        var arr = [];
        for(var i = 0; i < SpellNum; i++) {
            arr.push(i);
        }
        arr = shuffle(arr);
        for(var i = 0; i < SpellNum; i++){
            arr[i] = getArrayItemAddr(ThrallSpells, 8, arr[i]).readU64();
        }
        for(var i = 0; i < SpellNum; i++){
            getArrayItemAddr(ThrallSpells, 8, i).writeU64(arr[i]);
        }
        ADH_SpellManager_SetEquippedSpells(SpellManager, ThrallSpells);
    }

});
