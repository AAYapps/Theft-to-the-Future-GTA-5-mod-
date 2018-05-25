﻿using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTF_TimeTravel_0._9._0
{
    class effects
    {
        public static void make_effect(string root, string effect, Vehicle delorean)
        {
            Function.Call(Hash._0xDD19FA1C6D657305, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 10 });
            Function.Call(Hash._0xB80D8756B4668AB6, new InputArgument[] { root });
            Function.Call(Hash._0x6C38AF3693A69A91, new InputArgument[] { root });
            Function.Call(Hash._0x0D53A3B8DA0809D2, new InputArgument[] { effect, delorean.Handle, 0.0, 3.0, 0.5, 0.0, 0.0, 0.0, 3.0, 0, 0, 0 });
        }
        public static void make_effect(string root, string effect, Vector3 pos, Vector3 rot, float scale, bool axisX, bool axisY, bool axisZ, Vehicle delorean)
        {
            Function.Call(Hash._0xDD19FA1C6D657305, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 10 });
            Function.Call(Hash._0xB80D8756B4668AB6, new InputArgument[] { root });
            Function.Call(Hash._0x6C38AF3693A69A91, new InputArgument[] { root });
            Function.Call(Hash._0x0D53A3B8DA0809D2, new InputArgument[] { effect, delorean.Handle, pos.X, pos.Y, pos.Z, rot.X, rot.Y, rot.Z, scale, axisX, axisY, axisZ });
        }
        static List<Prop> prop = new List<Prop>();
        public static void make_effecttimetravel(double x, double y, Vehicle delorean)
        {
            var model = new Model("prop_beach_fire");
            model.Request(250);
            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);

                // Create the prop in the world
                prop.Add(World.CreateProp(model, delorean.GetOffsetInWorldCoords(new Vector3((float)x, (float)y, (float)-1.3)), true, false));
                prop[prop.Count - 1].Alpha = 0;
            }
        }
        static List<Prop> prop2 = new List<Prop>();
        public static void make_effecttimetravel2(double x, double y, Vehicle delorean)
        {
            var model = new Model("prop_beach_fire");
            model.Request(250);
            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);

                // Create the prop in the world
                prop2.Add(World.CreateProp(model, delorean.GetOffsetInWorldCoords(new Vector3((float)x, (float)y, (float)-1.3)), true, false));
                prop2[prop2.Count - 1].Alpha = 0;
            }
        }
        public static void reseteffect()
        {
            for (int i = prop.Count - 1; i >= 0; i--)
            {
                prop[i].Delete();
                prop.Remove(prop[i]);
                prop2[i].Delete();
                prop2.Remove(prop2[i]);
            }
        }
        public static void make_effect_smoke(string root, string effect, int index, Vehicle delorean)
        {
            Function.Call(Hash._0xDD19FA1C6D657305, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 10 });
            Function.Call(Hash._0xB80D8756B4668AB6, new InputArgument[] { root });
            Function.Call(Hash._0x6C38AF3693A69A91, new InputArgument[] { root });
            Function.Call(Hash._0x0D53A3B8DA0809D2, new InputArgument[] { effect, delorean.Handle, 0.0, -2.5, 0.5, 0.0, 0.0, 0.0, 3.0, 0, 0, 0 });
        }
        #region flux capcitor
        static int fluxanim = 0;
        static bool timeonce = false;
        public static void flux_capcitor(Vehicle car)
        {
            if (DateTime.Now.Millisecond % 60 > 30 && DateTime.Now.Millisecond % 60 <= 60)
            {
                Function.Call(Hash.SET_VEHICLE_MOD_KIT, car.Handle, 0);

                if (!timeonce)
                {
                    fluxanim++;

                    if (fluxanim == 8)
                    {
                        fluxanim = 0;
                        car.SetMod(VehicleMod.Frame, 4, true);
                    }

                    if (fluxanim == 6)
                    {
                        car.SetMod(VehicleMod.Frame, 3, true);
                    }

                    if (fluxanim == 4)
                    {
                        car.SetMod(VehicleMod.Frame, 2, true);
                    }

                    if (fluxanim == 2)
                    {
                        car.SetMod(VehicleMod.Frame, 1, true);
                    }

                    if (fluxanim == 0)
                    {
                        car.SetMod(VehicleMod.Frame, 0, true);
                    }
                    timeonce = true;
                }
            }
            else
            {
                timeonce = false;
            }
        }

        static bool below84 = false;
        static bool past84 = false;
        public static void wormhole(Vehicle delorean, int tempspeed, bool refilltimecurcuits)
        {
            if (delorean.Model == "bttf3" || delorean.Model == "bttf3rr")
            {
                if (tempspeed > 64 && tempspeed < 88)
                {
                    if (!below84)
                    {
                        Function.Call(Hash.SET_VEHICLE_MOD_KIT, delorean.Handle, 0);
                        delorean.SetMod(VehicleMod.Spoilers, 0, true);
                        delorean.SetMod(VehicleMod.Frame, 5, true);
                        if (refilltimecurcuits)
                        {
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_family4", "scr_fam4_trailer_sparks", new Vector3(0, 2.5f, 1.7f), new Vector3(100, 0, 0), 0.9f, false, false, false, delorean);

                            if (Game.Player.Character.CurrentVehicle.Model == "bttf3")
                                World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                            else if (Game.Player.Character.CurrentVehicle.Model == "bttf3rr")
                                World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                            else
                                World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.DodgerBlue, (float)1.2, 400);

                            if (!past84)
                            {
                                Sounds.sparksbttf3.Play();
                                Sounds.sparksfeul.Play();
                                delorean.IsInvincible = true;
                                past84 = true;
                            }
                        }
                        else
                        {
                            if (!past84)
                            {
                                Sounds.sparksbttf3.Play();
                                Sounds.sparks.Play();
                                past84 = true;
                            }
                        }
                    }
                }
            }
            else
            {
                if (tempspeed > 84 && tempspeed < 88)
                {
                    if (!below84)
                    {
                        Function.Call(Hash.SET_VEHICLE_MOD_KIT, delorean.Handle, 0);
                        delorean.SetMod(VehicleMod.Spoilers, 0, true);
                        delorean.SetMod(VehicleMod.Frame, 5, true);
                        if (refilltimecurcuits)
                        {
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                            make_effect("core", "blood_stungun", delorean);
                            if (Game.Player.Character.CurrentVehicle.Model == "bttf3")
                                World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                            else if (Game.Player.Character.CurrentVehicle.Model == "bttf3rr")
                                World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                            else
                                World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.DodgerBlue, (float)1.2, 400);

                            if (!past84)
                            {
                                Sounds.sparksfeul.Play();
                                past84 = true;
                            }
                        }
                        else
                        {
                            if (!past84)
                            {
                                Sounds.sparks.Play();
                                past84 = true;
                            }
                        }
                    }
                }
            }
        }

        public static void resetwormhole()
        {
            below84 = false;
            past84 = false;
        }

        public static void wormholeAndTravel(Vehicle delorean, int tempspeed, bool refilltimecurcuits)
        {
            if (!past84)
            {
                if (refilltimecurcuits)
                {
                    if (delorean.Model == "bttf3" || delorean.Model == "bttf3rr")
                    {
                        Sounds.sparksbttf3.Play();
                    }
                    Sounds.sparksfeul.Play();
                }
                else
                {
                    if (delorean.Model == "bttf3" || delorean.Model == "bttf3rr")
                    {
                        Sounds.sparksbttf3.Play();
                    }
                    Sounds.sparks.Play();
                }
                past84 = true;
            }

            if (!below84)
            {
                Function.Call(Hash.SET_VEHICLE_MOD_KIT, delorean.Handle, 0);
                delorean.SetMod(VehicleMod.Spoilers, 0, true);
                delorean.SetMod(VehicleMod.Frame, 5, true);
                if (refilltimecurcuits)
                {
                    make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                    make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                    make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                    make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                    if (delorean.Model == "bttf3" || delorean.Model == "bttf3rr")
                        make_effect("scr_family4", "scr_fam4_trailer_sparks", new Vector3(0, 2.5f, 1.7f), new Vector3(100, 0, 0), 0.9f, false, false, false, delorean);
                    else
                        make_effect("core", "blood_stungun", delorean);

                    //make_effect("scr_martin1", "scr_sol1_sniper_impact", delorean);
                }
            }
        }
        #endregion
    }
}