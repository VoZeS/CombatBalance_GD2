using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ReflectionFactory
{
    public class CommandFactory
    {
        private Dictionary<FightCommandTypes, Type> _commandsByType;

        public CommandFactory()
        {
            var weaponTypes = Assembly.GetAssembly(typeof(FightCommand)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(FightCommand)));

            _commandsByType = new Dictionary<FightCommandTypes, Type>();

            foreach (var type in weaponTypes)
            {
                var tempCommand = Activator.CreateInstance(type);
                _commandsByType.Add(((FightCommand)tempCommand).FightCommandType, type);
            }
        }

        public FightCommand GetCommand(FightCommandTypes commandType)
        {
            if (_commandsByType.ContainsKey(commandType))
            {
                return Activator.CreateInstance(_commandsByType[commandType]) as FightCommand;
            }
            return null;
        }

        
    }
}
