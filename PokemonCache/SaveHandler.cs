using PKHeX.Core;
using PKHeX.Core.Searching;

namespace PKCache
{
    internal class SaveHandler
    {

        public enum PKMTransfer
        {
            FromSavToSav,
            FromSavToCache,
            FromCacheToSav
        }

        public static PKM[]? pkmList;
        private static SaveFile? savFile;
        public static void LoadSave(string savPath)
        {
            byte[] savBytes = File.ReadAllBytes(savPath);
            savFile = SaveUtil.GetVariantSAV(savBytes);
            if (savFile == null)
            {
                Program.errorOccured = Tuple.Create(true, "Could not load save");
                return;
            }
            PKM[] locPkmList = savFile.GetAllPKM().ToArray();
            if (locPkmList == null)
            {
                Program.errorOccured = Tuple.Create(true, "PKM could not be loaded");
            }
            else if (locPkmList.Length == 0)
            {
                Program.errorOccured = Tuple.Create(true, "No pokemon found");
            }
            else
            {
                pkmList = locPkmList;
            }
        }

        public static Tuple<int, int> GetIndexPKM(PKM pkm)
        {
            string selectedHash = SearchUtil.HashByDetails(pkm);
            for (int i = 0; i < savFile.PartyCount; i++)
            {
                if (SearchUtil.HashByDetails(savFile.GetPartySlotAtIndex(i)) == selectedHash)
                {
                    return new(-1, i);
                }
            }
            for (int i = 0; i < savFile.BoxCount; i++)
            {
                for (int j = 0; j < savFile.BoxSlotCount; j++)
                {
                    if (SearchUtil.HashByDetails(savFile.GetBoxData(i)[j]) == selectedHash)
                    {
                        return new(i, j);
                    }
                }
            }
            return new(-1, -1);
        }

        public static bool RemovePKMFromSav(Tuple<int, int> tuple)
        {
            if (tuple.Item2 == -1 | savFile == null)
            {
                Program.errorOccured = new(true, "Unable to remove pokemon from save");
                return true;

            }
            else if (tuple.Item1 == -1)
            {
                savFile.DeletePartySlot(tuple.Item2);
                return false;
            }
            else
            {
                savFile.SetBoxSlotAtIndex(savFile.BlankPKM, tuple.Item1, tuple.Item2);
                return false;
            }
        }

        public static void WritePKMFile(PKM pkm)
        {
            string pkmPath = Program.basePath + "cache/" + $"{pkm.Species}_{pkm.PID}.pkm";
            File.WriteAllBytes(pkmPath, pkm.Data);
        }

        public static void RemovePKMFile(PKM pkm)
        {
            string pkmPath = Program.basePath + "cache/" + $"{pkm.Species}_{pkm.PID}.pkm";
            File.Delete(pkmPath);
        }

        public static bool AddPKMToSav(string path, PKM pkm)
        {
            byte[] savBytes = File.ReadAllBytes(path);
            SaveFile? outSav = SaveUtil.GetVariantSAV(savBytes);
            if (outSav == null)
            {
                Program.errorOccured = Tuple.Create(true, "Could not load save");
                return true;
            }
            else if (outSav.PartyCount >= 6)
            {
                int nextOpenSlot = outSav.NextOpenBoxSlot();
                if (nextOpenSlot == -1)
                {
                    Program.errorOccured = Tuple.Create(true, "No space to store PKM");
                    return true;
                }
                else
                {
                    outSav.SetBoxSlotAtIndex(pkm, nextOpenSlot);
                    return false;
                }
            }
            else
            {
                outSav.SetPartySlotAtIndex(pkm, outSav.PartyCount);
                return false;
            }

        }

        public static void MovePKM(PKMTransfer direction, PKM pkm, string? toPath = null)
        {
            switch (direction)
            {
                case PKMTransfer.FromSavToSav:
                    if ( toPath == null)
                    {
                        Program.errorOccured = Tuple.Create(true, "Must have destination path to write to save");
                    }
                    else
                    {
                        RemovePKMFromSav(GetIndexPKM(pkm));
                        AddPKMToSav(toPath, pkm);
                    }
                    break;
                case PKMTransfer.FromCacheToSav:
                    if (toPath == null)
                    {
                        Program.errorOccured = Tuple.Create(true, "Must have destination path to write to save");
                    }
                    else
                    {
                        RemovePKMFile(pkm);
                        AddPKMToSav(toPath, pkm);
                    }
                    break;
                case PKMTransfer.FromSavToCache:
                    RemovePKMFromSav(GetIndexPKM(pkm));
                    WritePKMFile(pkm);
                    break;
            }
        }
    }
}
