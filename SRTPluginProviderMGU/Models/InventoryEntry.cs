using SRTPluginProviderMGU.Enumerations;
using System;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class InventoryEntry : BaseNotifyModel, IEquatable<InventoryEntry>
    {
        /// <summary>
        /// Debugger display message.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (!IsEmpty)
                    return string.Format("[#{0}] Slot {1} Item {2} Quantity {3}", Index, Slot, Type, Quantity);
                else
                    return string.Format("[#{0}] Empty Slot", Index);
            }
        }

        public int Index { get; private set; }

        internal short _type = 0;
        public ItemEnumeration Type
        {
            get => (ItemEnumeration)_type;
            set => SetField(ref _type, (short)value);
        }

        internal string _name;
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(_name))
                    SetField(ref _name, GetName());
                return _name;
            }
            set => SetField(ref _name, value);
        }

        internal short _quantity;
        public short Quantity
        {
            get => _quantity;
            set => SetField(ref _quantity, value);
        }

        public bool HasQuantity
        {
            get
            {
                switch (Type)
                {
                    default:
                        return false;
                }
            }
        }

        internal bool _isEquipped;
        public bool IsEquipped
        {
            get => _isEquipped;
            set => SetField(ref _isEquipped, value);
        }

        internal bool _isEmpty = true;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetField(ref _isEmpty, value);
        }

        public int Slot { get; set; }
        public int SlotRow { get; set; }
        public int SlotColumn { get; set; }

        public InventoryEntry(int index)
        {
            Index = index;
            Slot = index;
            SlotRow = Slot / 6;
            SlotColumn = Slot % 6;
        }

        internal string GetName()
        {
            switch (Type)
            {
				case ItemEnumeration.Door:
					return "Door";

				case ItemEnumeration.Helmet:
					return "Helmet";

				case ItemEnumeration.Panel:
					return "Panel";

				case ItemEnumeration.StorageHatch:
					return "Storage Hatch";

				case ItemEnumeration.MainDoor:
					return "Main Door";

				case ItemEnumeration.VacTube:
					return "Vac-Tube";

				case ItemEnumeration.Computer:
					return "Computer";

				case ItemEnumeration.Drawer:
					return "Drawer";

				case ItemEnumeration.Rover:
					return "Rover";

				case ItemEnumeration.Cutter:
					return "Cutter";

				case ItemEnumeration.Microscope:
					return "Microscope";

				case ItemEnumeration.Fan:
					return "Fan";

				case ItemEnumeration.Mood:
					return "Mood";

				case ItemEnumeration.SpiritusFireplace:
					return "Spiritus Fireplace";

				case ItemEnumeration.Chair:
					return "Chair";

				case ItemEnumeration.Table:
					return "Table";

				case ItemEnumeration.Radio:
					return "Radio";

				case ItemEnumeration.Lighter:
					return "Lighter";

				case ItemEnumeration.MicrocorderAndrewMuir:
					return "Microcorder (Andrew Muir)";

				case ItemEnumeration.FlareGun:
					return "Flare Gun";

				case ItemEnumeration.Scarab:
					return "Scarab";

				case ItemEnumeration.Tag:
					return "Tag";

				case ItemEnumeration.BenGunn:
					return "Ben Gunn";

				case ItemEnumeration.Book:
					return "Book";

				case ItemEnumeration.NoteBabybug:
					return "Note (Babybug)";

				case ItemEnumeration.DeadBody:
					return "Dead Body";

				case ItemEnumeration.Biosensor:
					return "Biosensor";

				case ItemEnumeration.Digicamera:
					return "Digicamera";

				case ItemEnumeration.HairClippers:
					return "Hair Clippers";

				case ItemEnumeration.Flask:
					return "Flask";

				case ItemEnumeration.BrownHerbs:
					return "Brown Herbs";

				case ItemEnumeration.Newspaper:
					return "Newspaper";

				case ItemEnumeration.GardenClippers:
					return "Garden Clippers";

				case ItemEnumeration.Container:
					return "Container";

				case ItemEnumeration.Gloves:
					return "Gloves";

				case ItemEnumeration.Adhesive:
					return "Adhesive";

				case ItemEnumeration.Padlock:
					return "Padlock";

				case ItemEnumeration.MusicBox:
					return "Music Box";

				case ItemEnumeration.Dart:
					return "Dart";

				case ItemEnumeration.Toy:
					return "Toy";

				case ItemEnumeration.GamesChest:
					return "Games Chest";

				case ItemEnumeration.Crank:
					return "Crank";

				case ItemEnumeration.SoilScoop:
					return "Soil Scoop";

				case ItemEnumeration.Slides:
					return "Slides";

				case ItemEnumeration.Cannister:
					return "Cannister";

				case ItemEnumeration.Syringe:
					return "Syringe";

				case ItemEnumeration.Stethoscope:
					return "Stethoscope";

				case ItemEnumeration.AlarmClock:
					return "Alarm Clock";

				case ItemEnumeration.Elevator:
					return "Elevator";

				case ItemEnumeration.ElectronicScrewdriver:
					return "Electronic Screwdriver";

				case ItemEnumeration.MeasuringTape:
					return "Measuring Tape";

				case ItemEnumeration.DuctPanel:
					return "Duct Panel";

				case ItemEnumeration.DuctEntrance:
					return "Duct Entrance";

				case ItemEnumeration.Hacksaw:
					return "Hacksaw";

				case ItemEnumeration.MonkeyWrench:
					return "Monkey Wrench";

				case ItemEnumeration.Key:
					return "Key";

				case ItemEnumeration.Lenses:
					return "Lenses";

				case ItemEnumeration.FlareBolts:
					return "Flare Bolts";

				case ItemEnumeration.DriveBelt:
					return "Drive Belt";

				case ItemEnumeration.ExerciseBike:
					return "Exercise Bike";

				case ItemEnumeration.Lighter2:
					return "Lighter";

				case ItemEnumeration.Cigarettes:
					return "Cigarettes";

				case ItemEnumeration.Watch:
					return "Watch";

				case ItemEnumeration.Satsuma:
					return "Satsuma";

				case ItemEnumeration.NicotinePatch:
					return "Nicotine Patch";

				case ItemEnumeration.ContactLens:
					return "Contact Lens";

				case ItemEnumeration.Lipstick:
					return "Lipstick";

				case ItemEnumeration.Booklet:
					return "Booklet";

				case ItemEnumeration.PhotoMatlock:
					return "Photo (Matlock)";

				case ItemEnumeration.Letter:
					return "Letter";

				case ItemEnumeration.SilverBullet:
					return "Silver Bullet";

				case ItemEnumeration.Wrapper:
					return "Wrapper";

				case ItemEnumeration.BrownTag:
					return "Brown Tag";

				case ItemEnumeration.Cable:
					return "Cable";

				case ItemEnumeration.LooseCable:
					return "Loose Cable";

				case ItemEnumeration.GardenClippers2:
					return "Garden Clippers";

				case ItemEnumeration.OilyRag:
					return "Oily Rag";

				case ItemEnumeration.ControlPanel:
					return "Control Panel";

				case ItemEnumeration.BlueTag:
					return "Blue Tag";

				case ItemEnumeration.GreenTagDorm1:
					return "Green Tag (Dorm 1)";

				case ItemEnumeration.Corpse:
					return "Corpse";

				case ItemEnumeration.YellowTag:
					return "Yellow Tag";

				case ItemEnumeration.OrangeTag:
					return "Orange Tag";

				case ItemEnumeration.PurpleTag:
					return "Purple Tag";

				case ItemEnumeration.GrayTag:
					return "Gray Tag";

				case ItemEnumeration.RedTag:
					return "Red Tag";

				case ItemEnumeration.WhiteTag:
					return "White Tag";

				case ItemEnumeration.BlackTag:
					return "Black Tag";

				case ItemEnumeration.RainbowTag:
					return "Rainbow Tag";

				case ItemEnumeration.EvaSuit:
					return "Eva Suit";

				case ItemEnumeration.PopGun:
					return "Pop-Gun";

				case ItemEnumeration.Piccolo:
					return "Piccolo";

				case ItemEnumeration.Diabolus:
					return "Diabolus";

				case ItemEnumeration.Dillinger:
					return "Dillinger";

				case ItemEnumeration.NailGun:
					return "Nail Gun";

				case ItemEnumeration.FlareGun2:
					return "Flare Gun";

				case ItemEnumeration.WeedKillerSprayGun:
					return "Weed Killer Spray-Gun";

				case ItemEnumeration.Welder:
					return "Welder";

				case ItemEnumeration.MartianRock:
					return "Martian Rock";

				case ItemEnumeration.RockCutter:
					return "Rock Cutter";

				case ItemEnumeration.ShapedRock:
					return "Shaped Rock";

				case ItemEnumeration.RockSculpture:
					return "Rock Sculpture";

				case ItemEnumeration.ReceptorPanel:
					return "Receptor Panel";

				case ItemEnumeration.GeneratorHood:
					return "Generator Hood";

				case ItemEnumeration.ChemicalCombiner:
					return "Chemical Combiner";

				case ItemEnumeration.MeltedFlask:
					return "Melted Flask";

				case ItemEnumeration.FlaskNitroglycerine:
					return "Flask (Nitroglycerine)";

				case ItemEnumeration.SprayCartridgeAcid:
					return "Spray Cartridge (Acid)";

				case ItemEnumeration.PiccoloAmmo:
					return "Piccolo Ammo";

				case ItemEnumeration.AirFilter:
					return "Air Filter";

				case ItemEnumeration.CleanAirFilter:
					return "Clean Air Filter";

				case ItemEnumeration.VibroScourer:
					return "Vibro-Scourer";

				case ItemEnumeration.DrawerKey:
					return "Drawer Key";

				case ItemEnumeration.Rug:
					return "Rug";

				case ItemEnumeration.Locker:
					return "Locker";

				case ItemEnumeration.SeveredHand:
					return "Severed Hand";

				case ItemEnumeration.SunBed:
					return "Sun Bed";

				case ItemEnumeration.Biosensor2:
					return "Biosensor";

				case ItemEnumeration.PaperClip:
					return "Paper Clip";

				case ItemEnumeration.Notes:
					return "Notes";

				case ItemEnumeration.Notes2:
					return "Notes";

				case ItemEnumeration.SilverBell:
					return "Silver Bell";

				case ItemEnumeration.PalmtopComputer:
					return "Palmtop Computer";

				case ItemEnumeration.DoorControl:
					return "Door Control";

				case ItemEnumeration.Babybug:
					return "Babybug";

				case ItemEnumeration.BabybugRemote:
					return "Babybug Remote";

				case ItemEnumeration.Resonator:
					return "Resonator";

				case ItemEnumeration.Crate:
					return "Crate";

				case ItemEnumeration.Explosives:
					return "Explosives";

				case ItemEnumeration.DillingerAmmo:
					return "Dillinger Ammo";

				case ItemEnumeration.ChemicalExtractor:
					return "Chemical Extractor";

				case ItemEnumeration.Antitoxin:
					return "Antitoxin";

				case ItemEnumeration.HealthBoost:
					return "Health Boost";

				case ItemEnumeration.Lichen:
					return "Lichen";

				case ItemEnumeration.NeedleAndThread:
					return "Needle And Thread";

				case ItemEnumeration.BenGunnsHead:
					return "Ben Gunn's Head";

				case ItemEnumeration.TrimorphedAnimals:
					return "Trimorphed Animals";

				case ItemEnumeration.Switch:
					return "Switch";

				case ItemEnumeration.HourglassEmpty:
					return "Hourglass (Empty)";

				case ItemEnumeration.Keypad:
					return "Keypad";

				case ItemEnumeration.Button:
					return "Button";

				case ItemEnumeration.Instrument:
					return "Instrument";

				case ItemEnumeration.GreenTagDorm2:
					return "Green Tag (Dorm 2)";

				case ItemEnumeration.GreenTagDorm3:
					return "Green Tag (Dorm 3)";

				case ItemEnumeration.GreenTagDorm4:
					return "Green Tag (Dorm 4)";

				case ItemEnumeration.MetalCase:
					return "Metal Case";

				case ItemEnumeration.Psionara:
					return "Psionara";

				case ItemEnumeration.PasscodeArboretum:
					return "Passcode (Arboretum)";

				case ItemEnumeration.Desk:
					return "Desk";

				case ItemEnumeration.LaserScalpel:
					return "Laser Scalpel";

				case ItemEnumeration.Storeroom1Key:
					return "Storeroom 1 Key";

				case ItemEnumeration.Spectrometer:
					return "Spectrometer";

				case ItemEnumeration.Clothes:
					return "Clothes";

				case ItemEnumeration.NoteMrOba:
					return "Note (Mr. Oba)";

				case ItemEnumeration.CleaningFluid:
					return "Cleaning Fluid";

				case ItemEnumeration.Jar:
					return "Jar";

				case ItemEnumeration.Wool:
					return "Wool";

				case ItemEnumeration.DiabolusAmmo:
					return "Diabolus Ammo";

				case ItemEnumeration.RemoteControlSpiritus:
					return "Remote Control (Spiritus)";

				case ItemEnumeration.DeflatedMarsHopper:
					return "Deflated Mars Hopper";

				case ItemEnumeration.DeskKey:
					return "Desk Key";

				case ItemEnumeration.SprayCartridgeEmpty:
					return "Spray Cartridge (Empty)";

				case ItemEnumeration.Respirator:
					return "Respirator";

				case ItemEnumeration.BicyclePump:
					return "Bicycle Pump";

				case ItemEnumeration.Screwdriver:
					return "Screwdriver";

				case ItemEnumeration.NotesPickman:
					return "Notes (Pickman)";

				case ItemEnumeration.Diary:
					return "Diary";

				case ItemEnumeration.GreenTag:
					return "Green Tag";

				case ItemEnumeration.StudyDrawerKey:
					return "Study Drawer Key";

				case ItemEnumeration.HorribleArm:
					return "Horrible Arm";

				case ItemEnumeration.AirlockDoor:
					return "Airlock Door";

				case ItemEnumeration.Detonator:
					return "Detonator";

				case ItemEnumeration.PrimedExplosives:
					return "Primed Explosives";

				case ItemEnumeration.Fissure:
					return "Fissure";

				case ItemEnumeration.HeartOfStone:
					return "Heart Of Stone";

				case ItemEnumeration.Wire:
					return "Wire";

				case ItemEnumeration.Rock:
					return "Rock";

				case ItemEnumeration.StorageBox1:
					return "Storage Box 1";

				case ItemEnumeration.Artifact:
					return "Artifact";

				case ItemEnumeration.KeyMed:
					return "Key (Med)";

				case ItemEnumeration.EvaSuit2:
					return "Eva Suit";

				case ItemEnumeration.ElevatorDrive:
					return "Elevator Drive";

				case ItemEnumeration.SilverPlatter:
					return "Silver Platter";

				case ItemEnumeration.RedSand:
					return "Red Sand";

				case ItemEnumeration.BaconSlicer:
					return "Bacon Slicer";

				case ItemEnumeration.Graffiti:
					return "Graffiti";

				case ItemEnumeration.ElevatorShaft:
					return "Elevator Shaft";

				case ItemEnumeration.ElevatorButton:
					return "Elevator Button";

				case ItemEnumeration.TrimorphSample:
					return "Trimorph Sample";

				case ItemEnumeration.Centrifuge:
					return "Centrifuge";

				case ItemEnumeration.Thermaliser:
					return "Thermaliser";

				case ItemEnumeration.BloodSampleKarne:
					return "Blood Sample (Karne)";

				case ItemEnumeration.BloodSampleKenzo:
					return "Blood Sample (Kenzo)";

				case ItemEnumeration.BloodSampleMatlock:
					return "Blood Sample (Matlock)";

				case ItemEnumeration.VirusAntidote:
					return "Virus Antidote";

				case ItemEnumeration.MilkBloodSample:
					return "Milk Blood Sample";

				case ItemEnumeration.FleshHeart:
					return "Flesh Heart";

				case ItemEnumeration.Obelisk:
					return "Obelisk";

				case ItemEnumeration.ChargedChorus:
					return "Charged Chorus";

				case ItemEnumeration.Altar:
					return "Altar";

				case ItemEnumeration.ArkhamTag:
					return "Arkham Tag";

				case ItemEnumeration.Crane:
					return "Crane";

				case ItemEnumeration.HydraulicPlatform:
					return "Hydraulic Platform";

				case ItemEnumeration.AlienBreastSac:
					return "Alien Breast Sac";

				case ItemEnumeration.Generator:
					return "Generator";

				case ItemEnumeration.GeneratorCable:
					return "Generator Cable";

				case ItemEnumeration.DeadBattery:
					return "Dead Battery";

				case ItemEnumeration.ChargedBattery:
					return "Charged Battery";

				case ItemEnumeration.AlienMilk:
					return "Alien Milk";

				case ItemEnumeration.Breach:
					return "Breach";

				case ItemEnumeration.MarsHopper:
					return "Mars Hopper";

				case ItemEnumeration.Nails:
					return "Nails";

				case ItemEnumeration.Checker:
					return "Checker";

				case ItemEnumeration.Winch:
					return "Winch";

				case ItemEnumeration.Niche:
					return "Niche";

				case ItemEnumeration.Membrane:
					return "Membrane";

				case ItemEnumeration.Radio2:
					return "Radio";

				case ItemEnumeration.SmallHatch:
					return "Small Hatch";

				case ItemEnumeration.PsionCell:
					return "Psion Cell";

				case ItemEnumeration.Locker2:
					return "Locker";

				case ItemEnumeration.Hole:
					return "Hole";

				case ItemEnumeration.Statue:
					return "Statue";

				case ItemEnumeration.Vent:
					return "Vent";

				case ItemEnumeration.Pit:
					return "Pit";

				case ItemEnumeration.Screen:
					return "Screen";

				case ItemEnumeration.Dartboard:
					return "Dartboard";

				case ItemEnumeration.MorgueSlab:
					return "Morgue Slab";

				case ItemEnumeration.Grave:
					return "Grave";

				case ItemEnumeration.BloodTransfuser:
					return "Blood Transfuser";

				case ItemEnumeration.Painting:
					return "Painting";

				case ItemEnumeration.Books:
					return "Books";

				case ItemEnumeration.PasscodeAirlock5:
					return "Passcode (Airlock 5)";

				case ItemEnumeration.PasscodeMainHatch:
					return "Passcode (Main Hatch)";

				case ItemEnumeration.LockerKey:
					return "Locker Key";

				case ItemEnumeration.Bible:
					return "Bible";

				case ItemEnumeration.MicrocorderDietaMentz:
					return "Microcorder (Dieta Mentz)";

				case ItemEnumeration.Lockers:
					return "Lockers";

				case ItemEnumeration.NoteHolmes:
					return "Note (Holmes)";

				case ItemEnumeration.MicrocorderAndreivitch:
					return "Microcorder (Andreivitch)";

				case ItemEnumeration.NoteBenGunn1:
					return "Note (Ben Gunn #1)";

				case ItemEnumeration.DormLocker:
					return "Dorm Locker";

				case ItemEnumeration.MicrocorderTierney:
					return "Microcorder (Tierney)";

				case ItemEnumeration.NoteSuicide:
					return "Note (Suicide)";

				case ItemEnumeration.ErebusPasscode:
					return "Erebus Passcode";

				case ItemEnumeration.NoteAltar:
					return "Note (Altar)";

				case ItemEnumeration.NotePowerCoreRequest:
					return "Note (Power Core Request)";

				case ItemEnumeration.NoteChorus:
					return "Note (Chorus)";

				case ItemEnumeration.PasscodeDowningStreet:
					return "Passcode (Downing Street)";

				case ItemEnumeration.PumiceStone:
					return "Pumice Stone";

				case ItemEnumeration.BakerStreetHatch:
					return "Baker Street Hatch";

				case ItemEnumeration.DowningStreetHatch:
					return "Downing Street Hatch";

				case ItemEnumeration.BroadwayHatch:
					return "Broadway Hatch";

				case ItemEnumeration.LonelyStreetHatch:
					return "Lonely Street Hatch";

				case ItemEnumeration.ParkLaneHatch:
					return "Park Lane Hatch";

				case ItemEnumeration.WallStreetHatch:
					return "Wall Street Hatch";

				case ItemEnumeration.BoulevardStMichelHatch:
					return "Boulevard St Michel Hatch";

				case ItemEnumeration.ShuttleBayHatch:
					return "Shuttle Bay Hatch";

				case ItemEnumeration.KremlinMainHatch:
					return "Kremlin Main Hatch";

				case ItemEnumeration.KremlinEmergencyHatch:
					return "Kremlin Emergency Hatch";

				case ItemEnumeration.Printout:
					return "Printout";

				case ItemEnumeration.MicrocorderFelicci:
					return "Microcorder (Felicci)";

				case ItemEnumeration.NoteBenGunn2:
					return "Note (Ben Gunn #2)";

				case ItemEnumeration.NoteBenGunn3:
					return "Note (Ben Gunn #3)";

				case ItemEnumeration.NoteBenGunn4:
					return "Note (Ben Gunn #4)";

				case ItemEnumeration.InvitiationBenGunn:
					return "Invitiation (Ben Gunn)";

				case ItemEnumeration.MicrocorderMaiLin:
					return "Microcorder (Mai Lin)";

				case ItemEnumeration.MicrocorderDeborahTrask:
					return "Microcorder (Deborah Trask)";

				case ItemEnumeration.MicrocorderIzumiKeiko:
					return "Microcorder (Izumi Keiko)";

				case ItemEnumeration.MicrocorderJeanMerrow:
					return "Microcorder (Jean Merrow)";

				case ItemEnumeration.MicrocorderJonathonDarnley1:
					return "Microcorder (Jonathon Darnley 1)";

				case ItemEnumeration.MicrocorderJonathonDarnley2:
					return "Microcorder (Jonathon Darnley 2)";

				case ItemEnumeration.MicrocorderLukeBarton:
					return "Microcorder (Luke Barton)";

				case ItemEnumeration.MicrocorderNadjaKerenski:
					return "Microcorder (Nadja Kerenski)";

				case ItemEnumeration.MicrocorderRobertSeager:
					return "Microcorder (Robert Seager)";

				case ItemEnumeration.MicrocorderSimonFellner:
					return "Microcorder (Simon Fellner)";

				case ItemEnumeration.PhotoKenzo:
					return "Photo (Kenzo)";

				case ItemEnumeration.HourglassFull:
					return "Hourglass (Full)";

				case ItemEnumeration.Sand:
					return "Sand";

				case ItemEnumeration.PasscodeStoreroom2:
					return "Passcode (Storeroom 2)";

				case ItemEnumeration.PalmtopComputer2:
					return "Palmtop Computer";

				case ItemEnumeration.BloodSampleBenGunn:
					return "Blood Sample (Ben Gunn)";

				case ItemEnumeration.StorageBox2:
					return "Storage Box 2";

				case ItemEnumeration.StorageBox3:
					return "Storage Box 3";

				case ItemEnumeration.StorageBox4:
					return "Storage Box 4";

				case ItemEnumeration.None:
                default:
                    return "None";
            }
        }

        public void Clear()
        {
            if (IsEmpty) return;

            IsEmpty = true;

            _type = 0;
            _name = GetName();
            _quantity = 0;
            _isEquipped = true;

            OnPropertyChanged("Type");
            OnPropertyChanged("Name");
            OnPropertyChanged("Quantity");
            OnPropertyChanged("HasQuantity");
            OnPropertyChanged("IsEquipped");
            OnPropertyChanged("IsEmpty");
        }

        public override bool Equals(object obj) =>
            Equals(obj as InventoryEntry);

        public bool Equals(InventoryEntry other) =>
            other != null &&
            Index == other.Index;

        public override int GetHashCode() =>
            -2005450498 + Index.GetHashCode();
    }
}