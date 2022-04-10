﻿using Dom5Edit.Commands;
using Dom5Edit.Mods;
using Dom5Edit.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dom5Edit.Entities
{
    public class Spell : IDEntity
    {
        public static Dictionary<Command, Func<Property>> _propertyMap = new Dictionary<Command, Func<Property>>();

        static Spell()
        {
            //String properties
            //_propertyMap.Add(Command.NAME, StringProperty.Create);
        }

        public Spell(string value, string comment, Mod _parent, bool selected = false) : base(value, comment, _parent, selected)
        {
        }

        internal override Dictionary<int, IDEntity> GetIDList()
        {
            return Parent.Spells;
        }

        internal override Dictionary<string, IDEntity> GetNamedList()
        {
            return Parent.NamedSpells;
        }

        internal override Command GetNewCommand()
        {
            return Command.NEWSPELL;
        }

        internal override Dictionary<Command, Func<Property>> GetPropertyMap()
        {
            return _propertyMap;
        }

        internal override Command GetSelectCommand()
        {
            return Command.SELECTSPELL;
        }
    }
}
