using System;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace SuperSlime
{
    [ApiVersion(2, 1)]
    public class SuperSlime : TerrariaPlugin {
        public override string Name => "SuperSlime";
        public override string Author => "Comdar";
        public override string Description => "Spawn King Slime with custom parameters.";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public SuperSlime(Main game) : base(game) {
        }

        public override void Initialize() {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args) {
            Commands.ChatCommands.Add(new Command("superslime", Superslime, "superslime") { HelpText = "Usage: /superslime <hp>" });
        }

        private void Superslime(CommandArgs args) {
            int hp;

            if (args.Parameters.Count == 1 && Int32.TryParse(args.Parameters[0], out hp)) {
                int npcid = NPC.NewNPC((int)args.Player.X, (int)args.Player.Y, 50);
                Main.npc[npcid].SetDefaults(50);
                Main.npc[npcid].life = hp;
            } else {
                args.Player.SendErrorMessage("Incorrect syntax. Type /superslime <hp>");
            }
        }
    }
}