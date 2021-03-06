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
    class bttf1TimeTravel : TimeTravel
    {
        public const double movie = 1;

        #region Delorean functions

        void removePedsandVehicles(Vehicle Delorean)
        {
            //Start Ped Despawning
            Ped[] peds = World.GetNearbyPeds(Game.Player.Character, 100);
            Vehicle[] pedVehicles = World.GetNearbyVehicles(Game.Player.Character, 100);
            for (int i = 0; i < peds.Length; i++)
            {
                try
                {
                    if (!peds[i].IsInVehicle(timecurcuitssystem.bttfList[Delorean.NumberPlate.Trim()].getDelorean()))
                    {
                        if (peds[i].IsInVehicle())
                        {
                            if (!peds[i].IsInVehicle(timecurcuitssystem.bttfList[peds[i].CurrentVehicle.NumberPlate.Trim()].getDelorean()))
                            {
                                if (!Game.MissionFlag)
                                    peds[i].Delete();
                            }

                        }
                        else if (!peds[i].IsInVehicle())
                        {
                            if (!Game.MissionFlag)
                                peds[i].Delete();
                        }
                    }
                }
                catch
                {
                    if (!Game.MissionFlag)
                        peds[i].Delete();
                }
            }
            Array.Clear(peds, 0, peds.Length);
            for (int i = 0; i < pedVehicles.Length; i++)
            {
                try
                {
                    if (pedVehicles[i] != timecurcuitssystem.bttfList[Delorean.NumberPlate.Trim()].getDelorean())
                        if (pedVehicles[i] != timecurcuitssystem.bttfList[pedVehicles[i].NumberPlate.Trim()].getDelorean())
                            if (!Game.MissionFlag)
                                pedVehicles[i].Delete();
                }
                catch
                {
                    if (!Game.MissionFlag)
                        pedVehicles[i].Delete();
                }
            }
            Array.Clear(pedVehicles, 0, pedVehicles.Length);
            //End Ped Despawning
            Function.Call(GTA.Native.Hash.SET_RANDOM_WEATHER_TYPE);
        }

        void insteantTravel(Delorean delorean, effects worm)
        {
            Vehicle DMC = delorean.getDelorean();
            DMC.DirtLevel = 12;
            Function.Call(Hash.SET_CLOCK_TIME, ((delorean.fh1 * 10) + delorean.fh2), ((delorean.fm1 * 10) + delorean.fm2), 0);
            if (delorean.refilltimecurcuits)
            {
                Sounds.sparksfeul.Stop();
            }
            Script.Wait(10);
            delorean.timetravelentry();
            Script.Wait(10);
            Sounds.Timetravelreentery.Play();
            Script.Wait(10);
            if (!delorean.RCmode)
                removePedsandVehicles(DMC);
            else
            {
                TTTFmenu.RCmode = false;
                TTTFmenu.rcmodel = "";
                mainsystem.TTTF.setmenu(true);
                delorean.ToggleRCmode();
            }
            Script.Wait(10);
            Game.Player.WantedLevel = 0;
            Script.Wait(10);
            delorean.refilltimecurcuits = false;
            Script.Wait(10);
        }

        int[] flameTrails = new int[16];
        void cutScene(Delorean delorean, effects worm)
        {
            Vehicle DMC = delorean.getDelorean();
            DMC.IsInvincible = true;
            if (delorean.refilltimecurcuits)
                Sounds.sparksfeul.Stop();
            int temp = effects.make_effect("core", "veh_exhaust_spacecraft", "", "", "", new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            Sounds.Timetravelreenterycutscene.Play();
            if (!stoponce)
            {
                DMC.FreezePosition = true;
                DMC.HasCollision = false;
                stoponce = true;
            }

            DMC.IsVisible = false;
            DMC.EngineRunning = false;
            timecurcuitssystem.effectProps[DMC.NumberPlate.Trim()].wormholeHide(DMC);
            float fireY = -0.1f;
            flameTrails[0] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[1] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[2] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 1.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[3] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 1.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[4] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 3f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[5] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 3f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[6] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 4.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[7] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 4.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[8] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 6f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[9] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 6f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[10] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 7.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[11] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 7.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[12] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 9f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[13] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 9f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            flameTrails[14] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(1f, fireY + 10.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);
            flameTrails[15] = effects.make_effect("core", "fire_petrol_two", "dist", "strength", "fadein", new Vector3(-1f, fireY + 10.5f, 0.2f), new Vector3(0, 0, 0), 1f, false, false, false, DMC);

            //if (Function.Call<bool>(Hash.IS_VEHICLE_EXTRA_TURNED_ON, new InputArgument[] { DMC, 10 }))
            //{
            //    Function.Call(Hash.SET_VEHICLE_EXTRA, new InputArgument[] { DMC, 10, -1 });
            //}
            effects.reseteffects(temp);
            if (Game.Player.Character.IsInVehicle(DMC))
            {
                if (Game.Player.WantedLevel > 0)
                {
                    Game.Player.WantedLevel = 0;
                }
                delorean.timetravelentry();
                CharacterTravel(delorean);
                reentry(DMC);
            }
            else
            {
                delorean.timetravelentry();
                delorean.refilltimecurcuits = false;
                Script.Wait(10);
            }
        }

        void to88(int speed, Delorean delorean, effects worm)
        {
            if (delorean.refilltimecurcuits)
            {
                double time = 0;
                time = Sounds.sparksfeul.gettime();
                if (time < 4000)
                {
                    worm.wormholeAndTravel(delorean.getDelorean(), speed, delorean.refilltimecurcuits);
                }
                else
                {
                    delorean.timeTraveled = true;
                    if (Function.Call<int>(Hash.GET_FOLLOW_VEHICLE_CAM_VIEW_MODE) == 4)
                    {
                        insteantTravel(delorean, worm);
                    }
                    else
                    {
                        cutScene(delorean, worm);
                    }
                }
            }
        }

        void below88(Delorean delorean, effects worm)
        {
            stoponce = false;
            if (worm.below84)
            {
                Sounds.sparks.Stop();
                Sounds.sparksfeul.Stop();
                worm.resetwormhole();
            }
        }

        void resetTravel(int speed ,Delorean delorean, effects worm)
        {
            if (speed < 84 && delorean.timeTraveled && !worm.below84)
            {
                Sounds.sparksfeul.Stop();
                Sounds.sparks.Stop();
                worm.resetwormhole();
                delorean.timeTraveled = false;
            }
        }

        public override void runningCircuits(Delorean delorean, effects worm)
        {
            if (delorean != null)
            {
                Vehicle Deloreancar = delorean.getDelorean();
                #region functions

                #endregion
                int tempspeed = (int)((Deloreancar.Speed / .27777) / 1.60934);
                if (delorean.toggletimecurcuits)
                {
                    worm.wormhole(Deloreancar, tempspeed, delorean.refilltimecurcuits);

                    if (tempspeed > 84)
                    {
                        worm.below84 = true;
                    }
                    if (tempspeed >= 88)
                    {
                        to88(tempspeed, delorean, worm);
                    }
                    else if (tempspeed < 84)
                    {
                        errorbool = false;
                        below88(delorean, worm);
                    }
                }
                else
                {
                    resetTravel(tempspeed, delorean, worm);
                }
            }
        }

        //static bool timeentry = false;
        //static bool timeenter = false;
        void enterEffect(Vehicle car)
        {
            int temp = 0;
            World.DrawSpotLight(timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Position, timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Rotation, Color.SkyBlue, 80, 100, 60, 100, 5);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsVisible = true;
            temp = effects.make_effect("cut_lester1b", "scr_camera_flash", "", "", "", new Vector3(0f, -2f, 0.7f), new Vector3(10, 0, 180), 2f, false, false, false, car);
            Script.Wait(10);
            effects.reseteffects(temp);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsVisible = false;
            Script.Wait(50);
            World.DrawSpotLight(timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Position, timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Rotation, Color.SkyBlue, 80, 100, 60, 100, 5);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsVisible = true;
            temp = effects.make_effect("cut_lester1b", "scr_camera_flash", "", "", "", new Vector3(0f, -2f, 0f), new Vector3(10, 0, 180), 2f, false, false, false, car);
            Script.Wait(10);
            effects.reseteffects(temp);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsVisible = false;
            Script.Wait(50);
            World.DrawSpotLight(timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Position, timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Rotation, Color.SkyBlue, 80, 100, 60, 100, 5);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsVisible = true;
            temp = effects.make_effect("cut_lester1b", "scr_camera_flash", "", "", "", new Vector3(0f, -2f, 0f), new Vector3(10, 0, 180), 2f, false, false, false, car);
            Script.Wait(10);
            effects.reseteffects(temp);
        }

        public override void reentry(Vehicle car)
        {
            Libeads.timejump = true;
            Script.Wait(2000);
            Game.FadeScreenOut(600);
            timeenter = true;
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].timetravelentry();
            car.OpenDoor(VehicleDoor.Hood, false, false);
            //Function.Call(Hash.REMOVE_PARTICLE_FX_IN_RANGE, new InputArgument[] { car.Position.X, car.Position.Y, car.Position.Z, 200f });

            Script.Wait(1000);
            flameTrails.ToList().ForEach(x => effects.reseteffects(x));
            if (!timeentry)
            {
                //Function.Call(Hash.SET_CLOCK_DATE, getmonth(), getday(), getyear());
                Function.Call(Hash.SET_CLOCK_TIME, ((timecurcuitssystem.bttfList[car.NumberPlate.Trim()].fh1 * 10) + timecurcuitssystem.bttfList[car.NumberPlate.Trim()].fh2),
                    ((timecurcuitssystem.bttfList[car.NumberPlate.Trim()].fm1 * 10) + timecurcuitssystem.bttfList[car.NumberPlate.Trim()].fm2), 0);
                timeentry = true;
            }
            Script.Wait(2000);
            removePedsandVehicles(car);
            Script.Wait(2000);
            timeentry = false;
            car.CloseDoor(VehicleDoor.Hood, false);
            Game.FadeScreenIn(300);
            Script.Wait(1000);
            Sounds.reenterybttf1.Play();

            enterEffect(car);
            Script.Wait(10);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().FreezePosition = false;
            Script.Wait(10);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().HasCollision = true;
            Script.Wait(10);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsVisible = true;
            Script.Wait(10);
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().ApplyForceRelative(new Vector3(0, 55, 0));
            if (timeenter)
            {
                if (timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsDoorBroken(VehicleDoor.FrontLeftDoor))
                {
                    timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().GetPedOnSeat(VehicleSeat.Driver).Kill();
                    timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().GetPedOnSeat(VehicleSeat.Passenger).Kill();
                }
                if (timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsDoorBroken(VehicleDoor.FrontRightDoor))
                {
                    timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().GetPedOnSeat(VehicleSeat.Driver).Kill();
                    timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().GetPedOnSeat(VehicleSeat.Passenger).Kill();
                }
            }
            //Mrfusionpower -= 3;
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].refilltimecurcuits = false;
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().IsInvincible = false;
            Game.Player.CanControlCharacter = true;

            startfreeze();
        }

        void startfreeze()
        {
            Sounds.cold.Play();
            freeze = true;
            smokedelay = 0;
            highpressure = false;
            Doors.iceDoor();
        }


        bool playonce = false;
        int smokedelay = 0;
        bool highpressure = false;
        List<int> steam = new List<int>();
        public override void tickfreeze(Vehicle car)
        {
            if (!Sounds.cold.gettimeend() && freeze)
            {
                if (Sounds.Vent.gettime() > 1500 && Sounds.Vent.gettime() < 4300)
                {
                    if (smokedelay == 0)
                    {
                        smokedelay = 5000;
                        //effects.make_effect("des_prologue_door", "ent_ray_pro_door_steam", new Vector3(0.5f, -2f, 0.7f), new Vector3(15, 0, 180), 4f, false, false, false, car);
                        //effects.make_effect("des_prologue_door", "ent_ray_pro_door_steam", new Vector3(-0.5f, -2f, 0.7f), new Vector3(15, 0, 180), 4f, false, false, false, car);

                        if (!highpressure)
                        {
                            steam.Add(effects.make_effect("scr_mp_creator", "scr_mp_plane_landing_tyre_smoke", "", "", "", new Vector3(0.5f, -2f, 0.7f), new Vector3(0, 0, 90), 2f, false, false, false, car));
                            steam.Add(effects.make_effect("scr_mp_creator", "scr_mp_plane_landing_tyre_smoke", "", "", "", new Vector3(-0.5f, -2f, 0.7f), new Vector3(0, 0, 90), 2f, false, false, false, car));

                            steam.Add(effects.make_effect("scr_ar_planes", "scr_ar_trail_smoke_slow","dist","strength", "fadein", new Vector3(0.5f, -2f, 0.7f), new Vector3(0, 0, 90), 2f, false, false, false, car));
                            steam.Add(effects.make_effect("scr_ar_planes", "scr_ar_trail_smoke_slow","dist","strength", "fadein", new Vector3(-0.5f, -2f, 0.7f), new Vector3(0, 0, 90), 2f, false, false, false, car));
                            highpressure = true;
                        }
                        //effects.make_effect("cut_trevor1", "cs_meth_pipe_smoke", new Vector3(0.5f, -2f, 0.7f), new Vector3(15, 0, 180), 5f, false, false, false, car);
                        //effects.make_effect("cut_trevor1", "cs_meth_pipe_smoke", new Vector3(-0.5f, -2f, 0.7f), new Vector3(15, 0, 180), 5f, false, false, false, car);
                    }
                    smokedelay--;
                    
                }
                UI.ShowSubtitle("time: " + Sounds.cold.gettime());
                if (Sounds.cold.gettime() <= 7400)
                {
                    playonce = false;
                }
                else if (Sounds.cold.gettime() <= 8500)
                {
                    Sounds.Vent.Play();
                }
                else if (Sounds.cold.gettime() < 9000)
                {
                    if (!playonce)
                    {
                        Sounds.inputoff.Play();
                        playonce = true;
                        Doors.uniceDoor();
                        foreach (int i in steam)
                        {
                            effects.reseteffects(i);
                        }
                        UI.Notify("effects clean");
                    }
                }
            }
            else
            {
                if (playonce && !timecurcuitssystem.bttfList[car.NumberPlate.Trim()].refilltimecurcuits)
                {
                    Sounds.cold.Play();
                    playonce = false;
                    steam.Add(effects.make_effect("core", "ent_amb_dry_ice_area", "dist", "strength", "fadein", car.GetOffsetInWorldCoords(new Vector3(0.2f, 0f, 0f)), new Vector3(0, 0, 0), 4f, false, false, false, car));
                }
                freeze = false;
            }
        }
        #endregion

    }
}
