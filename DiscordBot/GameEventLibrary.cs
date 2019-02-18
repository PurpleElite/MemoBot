using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public static class GameEventLibrary
    {
        public static List<GameEvent> GameEvents;

        public static void Initialize()
        {
            FormattableString flavorText;
            int[] killers;
            int[] killed;
            GameEvent newEvent;

            flavorText = $"{new Slot(0)} sits around doing nothing interesting.";
            newEvent = new GameEvent(flavorText, 1);
            GameEvents.Add(newEvent);

            flavorText = $"{new Slot(0)} kills {new Slot(1)}.";
            killers = new int[] { 0 };
            killed = new int[] { 1 };
            newEvent = new GameEvent(flavorText, 2, killers, killed);
            GameEvents.Add(newEvent);

            flavorText = $"{new Slot(0)} kills {new Slot(1)} and {new Slot(2)}.";
            killers = new int[] { 0 };
            killed = new int[] { 1, 2 };
            newEvent = new GameEvent(flavorText, 3, killers, killed);
            GameEvents.Add(newEvent);

            flavorText = $"{new Slot(0)} kills {new Slot(1)}, {new Slot(2)}, and {new Slot(3)}.";
            killers = new int[] { 0 };
            killed = new int[] { 1, 2, 3 };
            newEvent = new GameEvent(flavorText, 4, killers, killed);
            GameEvents.Add(newEvent);

            flavorText = $"{new Slot(0)} kills {new Slot(1)}, {new Slot(2)}, {new Slot(3)}, and {new Slot(4)}.";
            killers = new int[] { 0 };
            killed = new int[] { 1, 2, 3, 4 };
            newEvent = new GameEvent(flavorText, 5, killers, killed);
            GameEvents.Add(newEvent);
        }
    }
}
