
var base = Module.findBaseAddress("DreadHungerServer-Win64-Shipping.exe");


//SpellManager
var MaxSpellInsn_addr = base.add(0xE5843E);
Interceptor.attach(MaxSpellInsn_addr, {
    onEnter : function(args) {
        this.context.rbx.add(0x228).writeU32(3);
    }
});

//SetInThrall SpellManager
var SpellManagerSpawn_addr = base.add(0xE4E201);
var UDH_GameInstance_GetInstance = new NativeFunction(base.add(0xD93ED0), 'pointer', ['pointer'], 'win64');
var ADH_SpellManager_SetEquippedSpells = new NativeFunction(base.add(0xE810E0), 'void', ['pointer', 'pointer'], 'win64');
Interceptor.attach(SpellManagerSpawn_addr, {
    onEnter : function(args) {
        var SpellManager = this.context.rax;
        var GameInstance = UDH_GameInstance_GetInstance(SpellManager);
        var ThrallSpells = GameInstance.add(0x440);
        ADH_SpellManager_SetEquippedSpells(SpellManager, ThrallSpells);
    }

});

