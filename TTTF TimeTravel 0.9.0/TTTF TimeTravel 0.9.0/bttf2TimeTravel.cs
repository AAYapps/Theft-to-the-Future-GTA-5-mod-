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
    class bttf2TimeTravel : TimeTravel
    {
        public const double movie = 2;

        #region Delorean functions
        //bool stoponce = false;

        void startMalfunction(Vehicle car)
        {
            if (Sounds.cirerror.getPlayStateStopped())
                Sounds.cirerror.Play();
        }

        void malfunction(Delorean Delorean, bool refilltimecircuits, bool toggle)
        {
            if (!Sounds.cirerror.getPlayStateStopped())
            {
                if (Sounds.cirerror.gettime() > 0 && Sounds.cirerror.gettime() < 0.023)
                {
                    Delorean.bug = false;
                }
                else if (Sounds.cirerror.gettime() > 0.189 && Sounds.cirerror.gettime() < 0.858)
                {
                    Delorean.bug = true;
                }
                else if (Sounds.cirerror.gettime() > 1.023 && Sounds.cirerror.gettime() < 1.692)
                {
                    Delorean.bug = false;
                }
                else if (Sounds.cirerror.gettime() > 1.858 && Sounds.cirerror.gettime() < 2.567)
                {
                    Delorean.bug = true;
                }
                else if (Sounds.cirerror.gettime() > 2.733 && Sounds.cirerror.gettime() < 3.624)
                {
                    Delorean.bug = false;
                }
                else if (Sounds.cirerror.gettime() > 2.733 && Sounds.cirerror.gettime() < 3.624)
                {
                    Delorean.bug = true;
                }
                else if (Sounds.cirerror.gettimeend())
                {
                    Delorean.bug = false;
                }
            }
        }

        void body(int BodyHealth, Delorean delorean)
        {
            if (BodyHealth == 0)
            {
                malfunction(delorean, delorean.refilltimecurcuits, delorean.toggletimecurcuits);
            }
            else if (BodyHealth < 700 && BodyHealth > 0)
            {
                int n = 0;
                if (DateTime.Now.Millisecond % 180 >= 90 && DateTime.Now.Millisecond % 180 <= 180)
                {
                    if (!tickbool)
                    {
                        n = rand.Next(1, BodyHealth);
                        tickbool = true;
                    }
                }
                else
                {
                    if (!delorean.bug)
                        tickbool = false;
                }

                if (n == 1)
                {
                    n++;
                    malfunction(delorean, delorean.refilltimecurcuits, delorean.toggletimecurcuits);
                }
            }
        }

        void displaymodelOnOff(Delorean delorean, bool on)
        {
            if (delorean.refilltimecurcuits)
            {
                body((int)delorean.getDelorean().BodyHealth, delorean);
            }
            else
            {

            }
        }

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

        void insteantTravel(Delorean delorean)
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
            if (DMC.Model == "bttf2")
            {
                Sounds.Timetravelreentery2.Play();
            }
            else if (DMC.Model == "bttf2f")
            {
                Sounds.Timetravelreentery2f.Play();
            }
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

        void cutScene(Delorean delorean)
        {
            Vehicle DMC = delorean.getDelorean();
            DMC.IsInvincible = true;
            if (delorean.refilltimecurcuits)
                Sounds.sparksfeul.Stop();
            effects.make_effect("scr_rcpaparazzo1", "scr_rcpap1_camera", DMC);
            Sounds.Timetravelreenterycutscene.Play();
            if (!stoponce)
            {
                DMC.FreezePosition = true;
                DMC.HasCollision = false;
                stoponce = true;
            }
            DMC.IsVisible = false;
            DMC.EngineRunning = false;
            // TODO - replace flame trail effects for bttf2
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
                if (time < 3000)
                    worm.wormholeAndTravel(delorean.getDelorean(), speed, delorean.refilltimecurcuits);
                else
                {
                    delorean.timeTraveled = true;
                    if (Function.Call<int>(Hash.GET_FOLLOW_VEHICLE_CAM_VIEW_MODE) == 4)
                    {
                        insteantTravel(delorean);
                    }
                    else
                    {
                        cutScene(delorean);
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

        void resetTravel(int speed, Delorean delorean, effects worm)
        {
            if (speed < 84 && delorean.timeTraveled && !worm.below84)
            {
                Sounds.sparksfeul.Stop();
                Sounds.sparks.Stop();
                worm.resetwormhole();
                delorean.timeTraveled = false;
            }
        }

        Vector3 flyingrotation = new Vector3(0, 0, 0);
        bool flyingison = false;
        public override void runningCircuits(Delorean delorean, effects worm)
        {
            if (delorean != null)
            {
                Vehicle Deloreancar = delorean.getDelorean();
                #region functions
                if (timecurcuitssystem.effectProps[Deloreancar.NumberPlate.Trim()].ice.Alpha > 0)
                    timecurcuitssystem.effectProps[Deloreancar.NumberPlate.Trim()].ice.Alpha -= 1;

                if (Deloreancar.Model == new Model("BTTF2") && flyingison)
                {
                    if (Deloreancar.HeightAboveGround > 1)
                    {
                        Deloreancar.ApplyForce(new Vector3(0, 0, 0.17f));
                    }
                    else
                    {
                        flyingison = false;
                    }
                }
                else if (Deloreancar.Model == new Model("BTTF2F"))
                {
                    if (Deloreancar.IsDoorBroken(VehicleDoor.BackLeftDoor) && Deloreancar.IsDoorBroken(VehicleDoor.BackRightDoor))
                    {
                        if (Deloreancar.EngineHealth > 0)
                            Deloreancar.EngineHealth--;
                    }
                    else if (Deloreancar.IsDoorBroken(VehicleDoor.BackLeftDoor) || Deloreancar.IsDoorBroken(VehicleDoor.BackRightDoor))
                    {
                        if (Deloreancar.EngineHealth > 500)
                            Deloreancar.EngineHealth--;
                        else
                            Deloreancar.EngineHealth = 500;
                    }
                    flyingison = true;
                }
                #endregion
                int tempspeed = (int)((Deloreancar.Speed / .27777) / 1.60934);

                if (delorean.toggletimecurcuits)
                {
                    displaymodelOnOff(delorean, delorean.toggletimecurcuits);
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

        void enterEffect(Vehicle car)
        {
            World.DrawSpotLight(timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Position, timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Rotation, Color.DeepSkyBlue, 80, 100, 60, 100, 5);
            Script.Wait(700);
            World.DrawSpotLight(timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Position, timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Rotation, Color.DeepSkyBlue, 80, 100, 60, 100, 5);
            Script.Wait(700);
            World.DrawSpotLight(timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Position, timecurcuitssystem.bttfList[car.NumberPlate.Trim()].getDelorean().Rotation, Color.DeepSkyBlue, 80, 100, 60, 100, 5);
        }

        public override void reentry(Vehicle car)
        {
            Libeads.timejump = true;
            Script.Wait(2000);
            Game.FadeScreenOut(600);
            timeenter = true;
            timecurcuitssystem.bttfList[car.NumberPlate.Trim()].timetravelentry();
            car.OpenDoor(VehicleDoor.Hood, false, false);
            Script.Wait(1000);
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

            Sounds.reenterybttf2.Play();

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
            timecurcuitssystem.effectProps[car.NumberPlate.Trim()].ice.Alpha = 255;

            startfreeze();
        }

        void startfreeze()
        {
            Sounds.cold.Play();
            freeze = true;
        }

        bool playonce = false;
        public override void tickfreeze(Vehicle car)
        {
            if (!Sounds.cold.gettimeend() && freeze)
            {
                if (Sounds.cold.gettime() <= 7400)
                {
                    playonce = false;
                }
                else if (Sounds.cold.gettime() < 9000)
                {
                    if (!playonce)
                    {
                        Sounds.inputoff.Play();
                        playonce = true;
                    }
                }
            }
            else
            {
                if (playonce && !timecurcuitssystem.bttfList[car.NumberPlate.Trim()].refilltimecurcuits)
                {
                    Sounds.cold.Play();
                    playonce = false;
                }
                freeze = false;
            }
        }
        #endregion
    }
}
