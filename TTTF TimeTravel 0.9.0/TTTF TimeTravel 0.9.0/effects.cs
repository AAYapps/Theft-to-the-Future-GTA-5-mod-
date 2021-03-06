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
            Function.Call(Hash.REMOVE_PARTICLE_FX_IN_RANGE, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 10 });
            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, new InputArgument[] { root });
            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, new InputArgument[] { root });
            Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, new InputArgument[] { effect, delorean.Handle, 0.0, 3.0, 0.5, 0.0, 0.0, 0.0, 3.0, 0, 0, 0 });
        }
        public static void make_effect(string root, string effect, Vector3 pos, Vector3 rot, float scale, bool axisX, bool axisY, bool axisZ, Vehicle delorean)
        {
            Function.Call(Hash.REMOVE_PARTICLE_FX_IN_RANGE, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 10 });
            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, new InputArgument[] { root });
            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, new InputArgument[] { root });
            Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, new InputArgument[] { effect, delorean.Handle, pos.X, pos.Y, pos.Z, rot.X, rot.Y, rot.Z, scale, axisX, axisY, axisZ });
        }
        public static void reseteffects(int id)
        {
            //Function.Call(Hash.REMOVE_PARTICLE_FX_IN_RANGE, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 200f });
            //Function.Call(Hash.REMOVE_PARTICLE_FX_IN_RANGE, new InputArgument[] { delorean.Position.X, delorean.Position.Y, delorean.Position.Z, 200f });
            Function.Call(Hash.REMOVE_PARTICLE_FX, id, 0);
        }
        //int id = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_AT_COORD, EffectName, Position.X, Position.Y, Position.Z, Rotation.X, Rotation.Y, Rotation.Z, Size, false, false, false);

        //Function.Call(Hash.SET_PARTICLE_FX_LOOPED_EVOLUTION, id, "parameter_name", 1f, 0);
        public static int make_effect(string root, string effect, string evolution,string evolution2,string evolution3, Vector3 pos, Vector3 rot, float scale, bool axisX, bool axisY, bool axisZ, Vehicle delorean)
        {
            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, root);

            while (!Function.Call<bool>(Hash.HAS_NAMED_PTFX_ASSET_LOADED, root))
            {
                Script.Wait(50);
            }
            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, new InputArgument[] { root });
            Vector3 carposition = delorean.GetOffsetInWorldCoords(pos);
            Vector3 carRotation = delorean.Rotation;
            int id = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_AT_COORD, effect, carposition.X, carposition.Y, carposition.Z, carRotation.X + rot.X, carRotation.Y + rot.Y, carRotation.Z + rot.Z, scale, axisX, axisY, axisZ);
            if (!evolution.Equals(""))
                Function.Call(Hash.SET_PARTICLE_FX_LOOPED_EVOLUTION, id, evolution, 0.1f, 0);
            if (!evolution.Equals(""))
                Function.Call(Hash.SET_PARTICLE_FX_LOOPED_EVOLUTION, id, evolution2, 1f, 0);
            if (!evolution.Equals(""))
                Function.Call(Hash.SET_PARTICLE_FX_LOOPED_EVOLUTION, id, evolution3, 1f, 0);
            return id;
        }

        public static void make_effect_smoke(string root, string effect, Vehicle delorean)
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
                    }

                    if (fluxanim == 6)
                    {

                    }

                    if (fluxanim == 4)
                    {

                    }

                    if (fluxanim == 2)
                    {

                    }

                    if (fluxanim == 0)
                    {

                    }
                    timeonce = true;
                }
            }
            else
            {
                timeonce = false;
            }
        }

        public bool below84 = false;
        bool past84 = false;
        public void wormhole(Vehicle delorean, int tempspeed, bool refilltimecurcuits)
        {
            if (delorean.Model == "bttf3" || delorean.Model == "bttf3rr")
            {
                if (tempspeed > 64 && tempspeed < 88)
                {
                    if (refilltimecurcuits)
                    {
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_family4", "scr_fam4_trailer_sparks", new Vector3(0, 2.5f, 1.7f), new Vector3(100, 0, 0), 0.9f, false, false, false, delorean);

                        if (delorean.Model == "bttf3")
                            World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                        else if (delorean.Model == "bttf3rr")
                            World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                        else
                            World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.DodgerBlue, (float)1.2, 400);

                        if (!past84)
                        {
                            Sounds.sparksbttf3.Play();
                            Sounds.sparksfeul.Play();
                            past84 = true;
                        }
                    }
                    else
                    {
                        if (!past84)
                        {
                            Sounds.sparksbttf3.Play();
                            past84 = true;
                        }
                    }
                }
            }
            else
            {
                if (tempspeed > 84 && tempspeed < 88)
                {
                    if (refilltimecurcuits)
                    {
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                        //make_effect("core", "blood_stungun", delorean);
                        timecurcuitssystem.effectProps[delorean.NumberPlate.Trim()].traveling = true;

                        if (delorean.Model == "bttf3")
                            World.DrawLightWithRange(delorean.GetOffsetInWorldCoords(new Vector3(0, (float)2.2, (float)0.5)), Color.Orange, (float)1.2, 400);
                        else if (delorean.Model == "bttf3rr")
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
                else
                    timecurcuitssystem.effectProps[delorean.NumberPlate.Trim()].traveling = false;
            }
        }

        public void resetwormhole()
        {
            below84 = false;
            past84 = false;
        }

        public void wormholeAndTravel(Vehicle delorean, int tempspeed, bool refilltimecurcuits)
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
            if (refilltimecurcuits)
            {
                make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, 1.1f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(-0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                make_effect("scr_mp_house", "scr_sh_lighter_sparks", new Vector3(0.8f, -1.4f, -0.5f), new Vector3(100, 0, 0), 2.9f, false, false, false, delorean);
                if (delorean.Model == "bttf3" || delorean.Model == "bttf3rr")
                    make_effect("scr_family4", "scr_fam4_trailer_sparks", new Vector3(0, 2.5f, 1.7f), new Vector3(100, 0, 0), 0.9f, false, false, false, delorean);
                //else
                    //make_effect("core", "blood_stungun", delorean);
                timecurcuitssystem.effectProps[delorean.NumberPlate.Trim()].traveling = true;
                //make_effect("scr_martin1", "scr_sol1_sniper_impact", delorean);
            }
        }
        #endregion
    }
}
