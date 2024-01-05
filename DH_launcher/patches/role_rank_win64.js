var base = Module.findBaseAddress("DreadHungerServer-Win64-Shipping.exe");

var GameStateInit_addr = base.add(0xE29034);
Interceptor.attach(GameStateInit_addr, {
    onEnter : function(args) {
        this.context.rbx.add(0x614).writeU64(30);
        
    }

});