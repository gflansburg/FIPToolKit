using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XPlaneConnect
{
    public sealed class Commands
    {
        public static readonly Commands Instance;

        static Commands()
        {
            Instance = new Commands();
        }

        Commands()
        {
            CommandList = new Dictionary<XPlaneCommands, XPlaneCommand>();
            CommandList.Add(NoneNone.Id, NoneNone);
            CommandList.Add(OperationQuit.Id, OperationQuit);
            CommandList.Add(OperationScreenshot.Id, OperationScreenshot);
            CommandList.Add(OperationShowMenu.Id, OperationShowMenu);
            CommandList.Add(OperationMakeCurrentAircraftIcons.Id, OperationMakeCurrentAircraftIcons);
            CommandList.Add(OperationMakeSingleIcon.Id, OperationMakeSingleIcon);
            CommandList.Add(OperationMakeMissingIcons.Id, OperationMakeMissingIcons);
            CommandList.Add(OperationRegenWeather.Id, OperationRegenWeather);
            CommandList.Add(OperationCycleDump.Id, OperationCycleDump);
            CommandList.Add(OperationStabDerivPitch.Id, OperationStabDerivPitch);
            CommandList.Add(OperationStabDerivHeading.Id, OperationStabDerivHeading);
            CommandList.Add(OperationRecording.Id, OperationRecording);
            CommandList.Add(OperationCreateSnapMarker.Id, OperationCreateSnapMarker);
            CommandList.Add(OperationTestDataRef.Id, OperationTestDataRef);
            CommandList.Add(OperationShowFps.Id, OperationShowFps);
            CommandList.Add(OperationDevConsole.Id, OperationDevConsole);
            CommandList.Add(OperationToggleFullScreen.Id, OperationToggleFullScreen);
            CommandList.Add(OperationReloadAircraft.Id, OperationReloadAircraft);
            CommandList.Add(OperationReloadAircraftNoArt.Id, OperationReloadAircraftNoArt);
            CommandList.Add(OperationReloadScenery.Id, OperationReloadScenery);
            CommandList.Add(OperationLoadRealWeather.Id, OperationLoadRealWeather);
            CommandList.Add(OperationFailSystem.Id, OperationFailSystem);
            CommandList.Add(OperationMakePanelPreviews.Id, OperationMakePanelPreviews);
            CommandList.Add(OperationCloseWindows.Id, OperationCloseWindows);
            CommandList.Add(OperationLoadSituation1.Id, OperationLoadSituation1);
            CommandList.Add(OperationLoadSituation2.Id, OperationLoadSituation2);
            CommandList.Add(OperationLoadSituation3.Id, OperationLoadSituation3);
            CommandList.Add(ViewTrackIrToggle.Id, ViewTrackIrToggle);
            CommandList.Add(MapShowCurrent.Id, MapShowCurrent);
            CommandList.Add(MapShowInstructorOperatorStation.Id, MapShowInstructorOperatorStation);
            CommandList.Add(MapShowLowEnroute.Id, MapShowLowEnroute);
            CommandList.Add(MapShowHighEnroute.Id, MapShowHighEnroute);
            CommandList.Add(MapShowSectional.Id, MapShowSectional);
            CommandList.Add(OperationToggleFlightConfig.Id, OperationToggleFlightConfig);
            CommandList.Add(OperationToggleMainMenu.Id, OperationToggleMainMenu);
            CommandList.Add(OperationToggleSettingsWindow.Id, OperationToggleSettingsWindow);
            CommandList.Add(OperationToggleFlightSchoolWindow.Id, OperationToggleFlightSchoolWindow);
            CommandList.Add(OperationToggleKeyShortcutsWindow.Id, OperationToggleKeyShortcutsWindow);
            CommandList.Add(OperationOpenWeightAndBalanceWindow.Id, OperationOpenWeightAndBalanceWindow);
            CommandList.Add(OperationOpenFailuresWindow.Id, OperationOpenFailuresWindow);
            CommandList.Add(OperationToggleDataOutputGraph.Id, OperationToggleDataOutputGraph);
            CommandList.Add(OperationToggleDataOutputCockpit.Id, OperationToggleDataOutputCockpit);
            CommandList.Add(OperationToggleJoyProfilesWindow.Id, OperationToggleJoyProfilesWindow);
            CommandList.Add(OperationToggleCustomLocationWindow.Id, OperationToggleCustomLocationWindow);
            CommandList.Add(OperationToggleStyleGuide.Id, OperationToggleStyleGuide);
            CommandList.Add(OperationSlider01.Id, OperationSlider01);
            CommandList.Add(OperationSlider02.Id, OperationSlider02);
            CommandList.Add(OperationSlider03.Id, OperationSlider03);
            CommandList.Add(OperationSlider04.Id, OperationSlider04);
            CommandList.Add(OperationSlider05.Id, OperationSlider05);
            CommandList.Add(OperationSlider06.Id, OperationSlider06);
            CommandList.Add(OperationSlider07.Id, OperationSlider07);
            CommandList.Add(OperationSlider08.Id, OperationSlider08);
            CommandList.Add(OperationSlider09.Id, OperationSlider09);
            CommandList.Add(OperationSlider10.Id, OperationSlider10);
            CommandList.Add(OperationSlider11.Id, OperationSlider11);
            CommandList.Add(OperationSlider12.Id, OperationSlider12);
            CommandList.Add(OperationSlider13.Id, OperationSlider13);
            CommandList.Add(OperationSlider14.Id, OperationSlider14);
            CommandList.Add(OperationSlider15.Id, OperationSlider15);
            CommandList.Add(OperationSlider16.Id, OperationSlider16);
            CommandList.Add(OperationSlider17.Id, OperationSlider17);
            CommandList.Add(OperationSlider18.Id, OperationSlider18);
            CommandList.Add(OperationSlider19.Id, OperationSlider19);
            CommandList.Add(OperationSlider20.Id, OperationSlider20);
            CommandList.Add(OperationSlider21.Id, OperationSlider21);
            CommandList.Add(OperationSlider22.Id, OperationSlider22);
            CommandList.Add(OperationSlider23.Id, OperationSlider23);
            CommandList.Add(OperationSlider24.Id, OperationSlider24);
            CommandList.Add(OperationFixAllSystems.Id, OperationFixAllSystems);
            CommandList.Add(OperationAutoBoard.Id, OperationAutoBoard);
            CommandList.Add(OperationAutoStart.Id, OperationAutoStart);
            CommandList.Add(OperationQuickStart.Id, OperationQuickStart);
            CommandList.Add(MagnetosMagnetosOff.Id, MagnetosMagnetosOff);
            CommandList.Add(MagnetosMagnetosBoth.Id, MagnetosMagnetosBoth);
            CommandList.Add(EnginesEngageStarters.Id, EnginesEngageStarters);
            CommandList.Add(EnginesThrottleDown.Id, EnginesThrottleDown);
            CommandList.Add(EnginesThrottleUp.Id, EnginesThrottleUp);
            CommandList.Add(EnginesTOGAPower.Id, EnginesTOGAPower);
            CommandList.Add(EnginesPropDown.Id, EnginesPropDown);
            CommandList.Add(EnginesPropUp.Id, EnginesPropUp);
            CommandList.Add(EnginesMixtureMin.Id, EnginesMixtureMin);
            CommandList.Add(EnginesMixtureDown.Id, EnginesMixtureDown);
            CommandList.Add(EnginesMixtureUp.Id, EnginesMixtureUp);
            CommandList.Add(EnginesMixtureMax.Id, EnginesMixtureMax);
            CommandList.Add(EnginesCarbHeatOff.Id, EnginesCarbHeatOff);
            CommandList.Add(EnginesCarbHeatOn.Id, EnginesCarbHeatOn);
            CommandList.Add(EnginesCarbHeatToggle.Id, EnginesCarbHeatToggle);
            CommandList.Add(FlightControlsCowlFlapsOpen.Id, FlightControlsCowlFlapsOpen);
            CommandList.Add(FlightControlsCowlFlapsClosed.Id, FlightControlsCowlFlapsClosed);
            CommandList.Add(EnginesIdleHiLoToggle.Id, EnginesIdleHiLoToggle);
            CommandList.Add(EnginesIdleHiLoToggle1.Id, EnginesIdleHiLoToggle1);
            CommandList.Add(EnginesIdleHiLoToggle2.Id, EnginesIdleHiLoToggle2);
            CommandList.Add(EnginesIdleHiLoToggle3.Id, EnginesIdleHiLoToggle3);
            CommandList.Add(EnginesIdleHiLoToggle4.Id, EnginesIdleHiLoToggle4);
            CommandList.Add(EnginesIdleHiLoToggle5.Id, EnginesIdleHiLoToggle5);
            CommandList.Add(EnginesIdleHiLoToggle6.Id, EnginesIdleHiLoToggle6);
            CommandList.Add(EnginesIdleHiLoToggle7.Id, EnginesIdleHiLoToggle7);
            CommandList.Add(EnginesIdleHiLoToggle8.Id, EnginesIdleHiLoToggle8);
            CommandList.Add(FadecFadecToggle.Id, FadecFadecToggle);
            CommandList.Add(EnginesGovernorOn.Id, EnginesGovernorOn);
            CommandList.Add(EnginesGovernorOff.Id, EnginesGovernorOff);
            CommandList.Add(EnginesGovernorToggle.Id, EnginesGovernorToggle);
            CommandList.Add(EnginesClutchOn.Id, EnginesClutchOn);
            CommandList.Add(EnginesClutchOff.Id, EnginesClutchOff);
            CommandList.Add(EnginesClutchToggle.Id, EnginesClutchToggle);
            CommandList.Add(EnginesBetaToggle.Id, EnginesBetaToggle);
            CommandList.Add(EnginesThrustReverseToggle.Id, EnginesThrustReverseToggle);
            CommandList.Add(EnginesThrustReverseHold.Id, EnginesThrustReverseHold);
            CommandList.Add(StartersShutDown.Id, StartersShutDown);
            CommandList.Add(MagnetosMagnetosDown1.Id, MagnetosMagnetosDown1);
            CommandList.Add(MagnetosMagnetosDown2.Id, MagnetosMagnetosDown2);
            CommandList.Add(MagnetosMagnetosDown3.Id, MagnetosMagnetosDown3);
            CommandList.Add(MagnetosMagnetosDown4.Id, MagnetosMagnetosDown4);
            CommandList.Add(MagnetosMagnetosDown5.Id, MagnetosMagnetosDown5);
            CommandList.Add(MagnetosMagnetosDown6.Id, MagnetosMagnetosDown6);
            CommandList.Add(MagnetosMagnetosDown7.Id, MagnetosMagnetosDown7);
            CommandList.Add(MagnetosMagnetosDown8.Id, MagnetosMagnetosDown8);
            CommandList.Add(MagnetosMagnetosUp1.Id, MagnetosMagnetosUp1);
            CommandList.Add(MagnetosMagnetosUp2.Id, MagnetosMagnetosUp2);
            CommandList.Add(MagnetosMagnetosUp3.Id, MagnetosMagnetosUp3);
            CommandList.Add(MagnetosMagnetosUp4.Id, MagnetosMagnetosUp4);
            CommandList.Add(MagnetosMagnetosUp5.Id, MagnetosMagnetosUp5);
            CommandList.Add(MagnetosMagnetosUp6.Id, MagnetosMagnetosUp6);
            CommandList.Add(MagnetosMagnetosUp7.Id, MagnetosMagnetosUp7);
            CommandList.Add(MagnetosMagnetosUp8.Id, MagnetosMagnetosUp8);
            CommandList.Add(MagnetosMagnetosOff1.Id, MagnetosMagnetosOff1);
            CommandList.Add(MagnetosMagnetosOff2.Id, MagnetosMagnetosOff2);
            CommandList.Add(MagnetosMagnetosOff3.Id, MagnetosMagnetosOff3);
            CommandList.Add(MagnetosMagnetosOff4.Id, MagnetosMagnetosOff4);
            CommandList.Add(MagnetosMagnetosOff5.Id, MagnetosMagnetosOff5);
            CommandList.Add(MagnetosMagnetosOff6.Id, MagnetosMagnetosOff6);
            CommandList.Add(MagnetosMagnetosOff7.Id, MagnetosMagnetosOff7);
            CommandList.Add(MagnetosMagnetosOff8.Id, MagnetosMagnetosOff8);
            CommandList.Add(MagnetosMagnetosLeft1.Id, MagnetosMagnetosLeft1);
            CommandList.Add(MagnetosMagnetosLeft2.Id, MagnetosMagnetosLeft2);
            CommandList.Add(MagnetosMagnetosLeft3.Id, MagnetosMagnetosLeft3);
            CommandList.Add(MagnetosMagnetosLeft4.Id, MagnetosMagnetosLeft4);
            CommandList.Add(MagnetosMagnetosLeft5.Id, MagnetosMagnetosLeft5);
            CommandList.Add(MagnetosMagnetosLeft6.Id, MagnetosMagnetosLeft6);
            CommandList.Add(MagnetosMagnetosLeft7.Id, MagnetosMagnetosLeft7);
            CommandList.Add(MagnetosMagnetosLeft8.Id, MagnetosMagnetosLeft8);
            CommandList.Add(MagnetosMagnetosRight1.Id, MagnetosMagnetosRight1);
            CommandList.Add(MagnetosMagnetosRight2.Id, MagnetosMagnetosRight2);
            CommandList.Add(MagnetosMagnetosRight3.Id, MagnetosMagnetosRight3);
            CommandList.Add(MagnetosMagnetosRight4.Id, MagnetosMagnetosRight4);
            CommandList.Add(MagnetosMagnetosRight5.Id, MagnetosMagnetosRight5);
            CommandList.Add(MagnetosMagnetosRight6.Id, MagnetosMagnetosRight6);
            CommandList.Add(MagnetosMagnetosRight7.Id, MagnetosMagnetosRight7);
            CommandList.Add(MagnetosMagnetosRight8.Id, MagnetosMagnetosRight8);
            CommandList.Add(MagnetosMagnetosBoth1.Id, MagnetosMagnetosBoth1);
            CommandList.Add(MagnetosMagnetosBoth2.Id, MagnetosMagnetosBoth2);
            CommandList.Add(MagnetosMagnetosBoth3.Id, MagnetosMagnetosBoth3);
            CommandList.Add(MagnetosMagnetosBoth4.Id, MagnetosMagnetosBoth4);
            CommandList.Add(MagnetosMagnetosBoth5.Id, MagnetosMagnetosBoth5);
            CommandList.Add(MagnetosMagnetosBoth6.Id, MagnetosMagnetosBoth6);
            CommandList.Add(MagnetosMagnetosBoth7.Id, MagnetosMagnetosBoth7);
            CommandList.Add(MagnetosMagnetosBoth8.Id, MagnetosMagnetosBoth8);
            CommandList.Add(IgnitionIgnitionDown1.Id, IgnitionIgnitionDown1);
            CommandList.Add(IgnitionIgnitionDown2.Id, IgnitionIgnitionDown2);
            CommandList.Add(IgnitionIgnitionDown3.Id, IgnitionIgnitionDown3);
            CommandList.Add(IgnitionIgnitionDown4.Id, IgnitionIgnitionDown4);
            CommandList.Add(IgnitionIgnitionDown5.Id, IgnitionIgnitionDown5);
            CommandList.Add(IgnitionIgnitionDown6.Id, IgnitionIgnitionDown6);
            CommandList.Add(IgnitionIgnitionDown7.Id, IgnitionIgnitionDown7);
            CommandList.Add(IgnitionIgnitionDown8.Id, IgnitionIgnitionDown8);
            CommandList.Add(IgnitionIgnitionUp1.Id, IgnitionIgnitionUp1);
            CommandList.Add(IgnitionIgnitionUp2.Id, IgnitionIgnitionUp2);
            CommandList.Add(IgnitionIgnitionUp3.Id, IgnitionIgnitionUp3);
            CommandList.Add(IgnitionIgnitionUp4.Id, IgnitionIgnitionUp4);
            CommandList.Add(IgnitionIgnitionUp5.Id, IgnitionIgnitionUp5);
            CommandList.Add(IgnitionIgnitionUp6.Id, IgnitionIgnitionUp6);
            CommandList.Add(IgnitionIgnitionUp7.Id, IgnitionIgnitionUp7);
            CommandList.Add(IgnitionIgnitionUp8.Id, IgnitionIgnitionUp8);
            CommandList.Add(IgnitersIgniterArmOff1.Id, IgnitersIgniterArmOff1);
            CommandList.Add(IgnitersIgniterArmOff2.Id, IgnitersIgniterArmOff2);
            CommandList.Add(IgnitersIgniterArmOff3.Id, IgnitersIgniterArmOff3);
            CommandList.Add(IgnitersIgniterArmOff4.Id, IgnitersIgniterArmOff4);
            CommandList.Add(IgnitersIgniterArmOff5.Id, IgnitersIgniterArmOff5);
            CommandList.Add(IgnitersIgniterArmOff6.Id, IgnitersIgniterArmOff6);
            CommandList.Add(IgnitersIgniterArmOff7.Id, IgnitersIgniterArmOff7);
            CommandList.Add(IgnitersIgniterArmOff8.Id, IgnitersIgniterArmOff8);
            CommandList.Add(IgnitersIgniterArmOn1.Id, IgnitersIgniterArmOn1);
            CommandList.Add(IgnitersIgniterArmOn2.Id, IgnitersIgniterArmOn2);
            CommandList.Add(IgnitersIgniterArmOn3.Id, IgnitersIgniterArmOn3);
            CommandList.Add(IgnitersIgniterArmOn4.Id, IgnitersIgniterArmOn4);
            CommandList.Add(IgnitersIgniterArmOn5.Id, IgnitersIgniterArmOn5);
            CommandList.Add(IgnitersIgniterArmOn6.Id, IgnitersIgniterArmOn6);
            CommandList.Add(IgnitersIgniterArmOn7.Id, IgnitersIgniterArmOn7);
            CommandList.Add(IgnitersIgniterArmOn8.Id, IgnitersIgniterArmOn8);
            CommandList.Add(IgnitersIgniterContinOff1.Id, IgnitersIgniterContinOff1);
            CommandList.Add(IgnitersIgniterContinOff2.Id, IgnitersIgniterContinOff2);
            CommandList.Add(IgnitersIgniterContinOff3.Id, IgnitersIgniterContinOff3);
            CommandList.Add(IgnitersIgniterContinOff4.Id, IgnitersIgniterContinOff4);
            CommandList.Add(IgnitersIgniterContinOff5.Id, IgnitersIgniterContinOff5);
            CommandList.Add(IgnitersIgniterContinOff6.Id, IgnitersIgniterContinOff6);
            CommandList.Add(IgnitersIgniterContinOff7.Id, IgnitersIgniterContinOff7);
            CommandList.Add(IgnitersIgniterContinOff8.Id, IgnitersIgniterContinOff8);
            CommandList.Add(IgnitersIgniterContinOn1.Id, IgnitersIgniterContinOn1);
            CommandList.Add(IgnitersIgniterContinOn2.Id, IgnitersIgniterContinOn2);
            CommandList.Add(IgnitersIgniterContinOn3.Id, IgnitersIgniterContinOn3);
            CommandList.Add(IgnitersIgniterContinOn4.Id, IgnitersIgniterContinOn4);
            CommandList.Add(IgnitersIgniterContinOn5.Id, IgnitersIgniterContinOn5);
            CommandList.Add(IgnitersIgniterContinOn6.Id, IgnitersIgniterContinOn6);
            CommandList.Add(IgnitersIgniterContinOn7.Id, IgnitersIgniterContinOn7);
            CommandList.Add(IgnitersIgniterContinOn8.Id, IgnitersIgniterContinOn8);
            CommandList.Add(StartersEngageStarter1.Id, StartersEngageStarter1);
            CommandList.Add(StartersEngageStarter2.Id, StartersEngageStarter2);
            CommandList.Add(StartersEngageStarter3.Id, StartersEngageStarter3);
            CommandList.Add(StartersEngageStarter4.Id, StartersEngageStarter4);
            CommandList.Add(StartersEngageStarter5.Id, StartersEngageStarter5);
            CommandList.Add(StartersEngageStarter6.Id, StartersEngageStarter6);
            CommandList.Add(StartersEngageStarter7.Id, StartersEngageStarter7);
            CommandList.Add(StartersEngageStarter8.Id, StartersEngageStarter8);
            CommandList.Add(EnginesThrottleDown1.Id, EnginesThrottleDown1);
            CommandList.Add(EnginesThrottleDown2.Id, EnginesThrottleDown2);
            CommandList.Add(EnginesThrottleDown3.Id, EnginesThrottleDown3);
            CommandList.Add(EnginesThrottleDown4.Id, EnginesThrottleDown4);
            CommandList.Add(EnginesThrottleDown5.Id, EnginesThrottleDown5);
            CommandList.Add(EnginesThrottleDown6.Id, EnginesThrottleDown6);
            CommandList.Add(EnginesThrottleDown7.Id, EnginesThrottleDown7);
            CommandList.Add(EnginesThrottleDown8.Id, EnginesThrottleDown8);
            CommandList.Add(EnginesThrottleUp1.Id, EnginesThrottleUp1);
            CommandList.Add(EnginesThrottleUp2.Id, EnginesThrottleUp2);
            CommandList.Add(EnginesThrottleUp3.Id, EnginesThrottleUp3);
            CommandList.Add(EnginesThrottleUp4.Id, EnginesThrottleUp4);
            CommandList.Add(EnginesThrottleUp5.Id, EnginesThrottleUp5);
            CommandList.Add(EnginesThrottleUp6.Id, EnginesThrottleUp6);
            CommandList.Add(EnginesThrottleUp7.Id, EnginesThrottleUp7);
            CommandList.Add(EnginesThrottleUp8.Id, EnginesThrottleUp8);
            CommandList.Add(EnginesPropDown1.Id, EnginesPropDown1);
            CommandList.Add(EnginesPropDown2.Id, EnginesPropDown2);
            CommandList.Add(EnginesPropDown3.Id, EnginesPropDown3);
            CommandList.Add(EnginesPropDown4.Id, EnginesPropDown4);
            CommandList.Add(EnginesPropDown5.Id, EnginesPropDown5);
            CommandList.Add(EnginesPropDown6.Id, EnginesPropDown6);
            CommandList.Add(EnginesPropDown7.Id, EnginesPropDown7);
            CommandList.Add(EnginesPropDown8.Id, EnginesPropDown8);
            CommandList.Add(EnginesPropUp1.Id, EnginesPropUp1);
            CommandList.Add(EnginesPropUp2.Id, EnginesPropUp2);
            CommandList.Add(EnginesPropUp3.Id, EnginesPropUp3);
            CommandList.Add(EnginesPropUp4.Id, EnginesPropUp4);
            CommandList.Add(EnginesPropUp5.Id, EnginesPropUp5);
            CommandList.Add(EnginesPropUp6.Id, EnginesPropUp6);
            CommandList.Add(EnginesPropUp7.Id, EnginesPropUp7);
            CommandList.Add(EnginesPropUp8.Id, EnginesPropUp8);
            CommandList.Add(EnginesMixtureDown1.Id, EnginesMixtureDown1);
            CommandList.Add(EnginesMixtureDown2.Id, EnginesMixtureDown2);
            CommandList.Add(EnginesMixtureDown3.Id, EnginesMixtureDown3);
            CommandList.Add(EnginesMixtureDown4.Id, EnginesMixtureDown4);
            CommandList.Add(EnginesMixtureDown5.Id, EnginesMixtureDown5);
            CommandList.Add(EnginesMixtureDown6.Id, EnginesMixtureDown6);
            CommandList.Add(EnginesMixtureDown7.Id, EnginesMixtureDown7);
            CommandList.Add(EnginesMixtureDown8.Id, EnginesMixtureDown8);
            CommandList.Add(EnginesMixtureUp1.Id, EnginesMixtureUp1);
            CommandList.Add(EnginesMixtureUp2.Id, EnginesMixtureUp2);
            CommandList.Add(EnginesMixtureUp3.Id, EnginesMixtureUp3);
            CommandList.Add(EnginesMixtureUp4.Id, EnginesMixtureUp4);
            CommandList.Add(EnginesMixtureUp5.Id, EnginesMixtureUp5);
            CommandList.Add(EnginesMixtureUp6.Id, EnginesMixtureUp6);
            CommandList.Add(EnginesMixtureUp7.Id, EnginesMixtureUp7);
            CommandList.Add(EnginesMixtureUp8.Id, EnginesMixtureUp8);
            CommandList.Add(EnginesBetaToggle1.Id, EnginesBetaToggle1);
            CommandList.Add(EnginesBetaToggle2.Id, EnginesBetaToggle2);
            CommandList.Add(EnginesBetaToggle3.Id, EnginesBetaToggle3);
            CommandList.Add(EnginesBetaToggle4.Id, EnginesBetaToggle4);
            CommandList.Add(EnginesBetaToggle5.Id, EnginesBetaToggle5);
            CommandList.Add(EnginesBetaToggle6.Id, EnginesBetaToggle6);
            CommandList.Add(EnginesBetaToggle7.Id, EnginesBetaToggle7);
            CommandList.Add(EnginesBetaToggle8.Id, EnginesBetaToggle8);
            CommandList.Add(EnginesThrustReverseToggle1.Id, EnginesThrustReverseToggle1);
            CommandList.Add(EnginesThrustReverseToggle2.Id, EnginesThrustReverseToggle2);
            CommandList.Add(EnginesThrustReverseToggle3.Id, EnginesThrustReverseToggle3);
            CommandList.Add(EnginesThrustReverseToggle4.Id, EnginesThrustReverseToggle4);
            CommandList.Add(EnginesThrustReverseToggle5.Id, EnginesThrustReverseToggle5);
            CommandList.Add(EnginesThrustReverseToggle6.Id, EnginesThrustReverseToggle6);
            CommandList.Add(EnginesThrustReverseToggle7.Id, EnginesThrustReverseToggle7);
            CommandList.Add(EnginesThrustReverseToggle8.Id, EnginesThrustReverseToggle8);
            CommandList.Add(EnginesThrustReverseHold1.Id, EnginesThrustReverseHold1);
            CommandList.Add(EnginesThrustReverseHold2.Id, EnginesThrustReverseHold2);
            CommandList.Add(EnginesThrustReverseHold3.Id, EnginesThrustReverseHold3);
            CommandList.Add(EnginesThrustReverseHold4.Id, EnginesThrustReverseHold4);
            CommandList.Add(EnginesThrustReverseHold5.Id, EnginesThrustReverseHold5);
            CommandList.Add(EnginesThrustReverseHold6.Id, EnginesThrustReverseHold6);
            CommandList.Add(EnginesThrustReverseHold7.Id, EnginesThrustReverseHold7);
            CommandList.Add(EnginesThrustReverseHold8.Id, EnginesThrustReverseHold8);
            CommandList.Add(StartersShutDown1.Id, StartersShutDown1);
            CommandList.Add(StartersShutDown2.Id, StartersShutDown2);
            CommandList.Add(StartersShutDown3.Id, StartersShutDown3);
            CommandList.Add(StartersShutDown4.Id, StartersShutDown4);
            CommandList.Add(StartersShutDown5.Id, StartersShutDown5);
            CommandList.Add(StartersShutDown6.Id, StartersShutDown6);
            CommandList.Add(StartersShutDown7.Id, StartersShutDown7);
            CommandList.Add(StartersShutDown8.Id, StartersShutDown8);
            CommandList.Add(FlightControlsCowlFlapsClosed1.Id, FlightControlsCowlFlapsClosed1);
            CommandList.Add(FlightControlsCowlFlapsClosed2.Id, FlightControlsCowlFlapsClosed2);
            CommandList.Add(FlightControlsCowlFlapsClosed3.Id, FlightControlsCowlFlapsClosed3);
            CommandList.Add(FlightControlsCowlFlapsClosed4.Id, FlightControlsCowlFlapsClosed4);
            CommandList.Add(FlightControlsCowlFlapsClosed5.Id, FlightControlsCowlFlapsClosed5);
            CommandList.Add(FlightControlsCowlFlapsClosed6.Id, FlightControlsCowlFlapsClosed6);
            CommandList.Add(FlightControlsCowlFlapsClosed7.Id, FlightControlsCowlFlapsClosed7);
            CommandList.Add(FlightControlsCowlFlapsClosed8.Id, FlightControlsCowlFlapsClosed8);
            CommandList.Add(FlightControlsCowlFlapsOpen1.Id, FlightControlsCowlFlapsOpen1);
            CommandList.Add(FlightControlsCowlFlapsOpen2.Id, FlightControlsCowlFlapsOpen2);
            CommandList.Add(FlightControlsCowlFlapsOpen3.Id, FlightControlsCowlFlapsOpen3);
            CommandList.Add(FlightControlsCowlFlapsOpen4.Id, FlightControlsCowlFlapsOpen4);
            CommandList.Add(FlightControlsCowlFlapsOpen5.Id, FlightControlsCowlFlapsOpen5);
            CommandList.Add(FlightControlsCowlFlapsOpen6.Id, FlightControlsCowlFlapsOpen6);
            CommandList.Add(FlightControlsCowlFlapsOpen7.Id, FlightControlsCowlFlapsOpen7);
            CommandList.Add(FlightControlsCowlFlapsOpen8.Id, FlightControlsCowlFlapsOpen8);
            CommandList.Add(FadecFadec1Off.Id, FadecFadec1Off);
            CommandList.Add(FadecFadec2Off.Id, FadecFadec2Off);
            CommandList.Add(FadecFadec3Off.Id, FadecFadec3Off);
            CommandList.Add(FadecFadec4Off.Id, FadecFadec4Off);
            CommandList.Add(FadecFadec5Off.Id, FadecFadec5Off);
            CommandList.Add(FadecFadec6Off.Id, FadecFadec6Off);
            CommandList.Add(FadecFadec7Off.Id, FadecFadec7Off);
            CommandList.Add(FadecFadec8Off.Id, FadecFadec8Off);
            CommandList.Add(FadecFadec1On.Id, FadecFadec1On);
            CommandList.Add(FadecFadec2On.Id, FadecFadec2On);
            CommandList.Add(FadecFadec3On.Id, FadecFadec3On);
            CommandList.Add(FadecFadec4On.Id, FadecFadec4On);
            CommandList.Add(FadecFadec5On.Id, FadecFadec5On);
            CommandList.Add(FadecFadec6On.Id, FadecFadec6On);
            CommandList.Add(FadecFadec7On.Id, FadecFadec7On);
            CommandList.Add(FadecFadec8On.Id, FadecFadec8On);
            CommandList.Add(AltairAlternateAirOff1.Id, AltairAlternateAirOff1);
            CommandList.Add(AltairAlternateAirOff2.Id, AltairAlternateAirOff2);
            CommandList.Add(AltairAlternateAirOff3.Id, AltairAlternateAirOff3);
            CommandList.Add(AltairAlternateAirOff4.Id, AltairAlternateAirOff4);
            CommandList.Add(AltairAlternateAirOff5.Id, AltairAlternateAirOff5);
            CommandList.Add(AltairAlternateAirOff6.Id, AltairAlternateAirOff6);
            CommandList.Add(AltairAlternateAirOff7.Id, AltairAlternateAirOff7);
            CommandList.Add(AltairAlternateAirOff8.Id, AltairAlternateAirOff8);
            CommandList.Add(AltairAlternateAirOn1.Id, AltairAlternateAirOn1);
            CommandList.Add(AltairAlternateAirOn2.Id, AltairAlternateAirOn2);
            CommandList.Add(AltairAlternateAirOn3.Id, AltairAlternateAirOn3);
            CommandList.Add(AltairAlternateAirOn4.Id, AltairAlternateAirOn4);
            CommandList.Add(AltairAlternateAirOn5.Id, AltairAlternateAirOn5);
            CommandList.Add(AltairAlternateAirOn6.Id, AltairAlternateAirOn6);
            CommandList.Add(AltairAlternateAirOn7.Id, AltairAlternateAirOn7);
            CommandList.Add(AltairAlternateAirOn8.Id, AltairAlternateAirOn8);
            CommandList.Add(AltairAlternateAirBackupOff1.Id, AltairAlternateAirBackupOff1);
            CommandList.Add(AltairAlternateAirBackupOff2.Id, AltairAlternateAirBackupOff2);
            CommandList.Add(AltairAlternateAirBackupOff3.Id, AltairAlternateAirBackupOff3);
            CommandList.Add(AltairAlternateAirBackupOff4.Id, AltairAlternateAirBackupOff4);
            CommandList.Add(AltairAlternateAirBackupOff5.Id, AltairAlternateAirBackupOff5);
            CommandList.Add(AltairAlternateAirBackupOff6.Id, AltairAlternateAirBackupOff6);
            CommandList.Add(AltairAlternateAirBackupOff7.Id, AltairAlternateAirBackupOff7);
            CommandList.Add(AltairAlternateAirBackupOff8.Id, AltairAlternateAirBackupOff8);
            CommandList.Add(AltairAlternateAirBackupOn1.Id, AltairAlternateAirBackupOn1);
            CommandList.Add(AltairAlternateAirBackupOn2.Id, AltairAlternateAirBackupOn2);
            CommandList.Add(AltairAlternateAirBackupOn3.Id, AltairAlternateAirBackupOn3);
            CommandList.Add(AltairAlternateAirBackupOn4.Id, AltairAlternateAirBackupOn4);
            CommandList.Add(AltairAlternateAirBackupOn5.Id, AltairAlternateAirBackupOn5);
            CommandList.Add(AltairAlternateAirBackupOn6.Id, AltairAlternateAirBackupOn6);
            CommandList.Add(AltairAlternateAirBackupOn7.Id, AltairAlternateAirBackupOn7);
            CommandList.Add(AltairAlternateAirBackupOn8.Id, AltairAlternateAirBackupOn8);
            CommandList.Add(EnginesFireExt1Off.Id, EnginesFireExt1Off);
            CommandList.Add(EnginesFireExt2Off.Id, EnginesFireExt2Off);
            CommandList.Add(EnginesFireExt3Off.Id, EnginesFireExt3Off);
            CommandList.Add(EnginesFireExt4Off.Id, EnginesFireExt4Off);
            CommandList.Add(EnginesFireExt5Off.Id, EnginesFireExt5Off);
            CommandList.Add(EnginesFireExt6Off.Id, EnginesFireExt6Off);
            CommandList.Add(EnginesFireExt7Off.Id, EnginesFireExt7Off);
            CommandList.Add(EnginesFireExt8Off.Id, EnginesFireExt8Off);
            CommandList.Add(EnginesFireExt1On.Id, EnginesFireExt1On);
            CommandList.Add(EnginesFireExt2On.Id, EnginesFireExt2On);
            CommandList.Add(EnginesFireExt3On.Id, EnginesFireExt3On);
            CommandList.Add(EnginesFireExt4On.Id, EnginesFireExt4On);
            CommandList.Add(EnginesFireExt5On.Id, EnginesFireExt5On);
            CommandList.Add(EnginesFireExt6On.Id, EnginesFireExt6On);
            CommandList.Add(EnginesFireExt7On.Id, EnginesFireExt7On);
            CommandList.Add(EnginesFireExt8On.Id, EnginesFireExt8On);
            CommandList.Add(FlightControlsFlapsUp.Id, FlightControlsFlapsUp);
            CommandList.Add(FlightControlsFlapsDown.Id, FlightControlsFlapsDown);
            CommandList.Add(FlightControlsVectorSweepAft.Id, FlightControlsVectorSweepAft);
            CommandList.Add(FlightControlsVectorSweepForward.Id, FlightControlsVectorSweepForward);
            CommandList.Add(FlightControlsBlimpLiftDown.Id, FlightControlsBlimpLiftDown);
            CommandList.Add(FlightControlsBlimpLiftUp.Id, FlightControlsBlimpLiftUp);
            CommandList.Add(FlightControlsSpeedBrakesDownOne.Id, FlightControlsSpeedBrakesDownOne);
            CommandList.Add(FlightControlsSpeedBrakesUpOne.Id, FlightControlsSpeedBrakesUpOne);
            CommandList.Add(FlightControlsSpeedBrakesDownAll.Id, FlightControlsSpeedBrakesDownAll);
            CommandList.Add(FlightControlsSpeedBrakesUpAll.Id, FlightControlsSpeedBrakesUpAll);
            CommandList.Add(FlightControlsSpeedBrakesToggle.Id, FlightControlsSpeedBrakesToggle);
            CommandList.Add(FlightControlsLandingGearDown.Id, FlightControlsLandingGearDown);
            CommandList.Add(FlightControlsLandingGearUp.Id, FlightControlsLandingGearUp);
            CommandList.Add(FlightControlsLandingGearToggle.Id, FlightControlsLandingGearToggle);
            CommandList.Add(FlightControlsLandingGearEmerOn.Id, FlightControlsLandingGearEmerOn);
            CommandList.Add(FlightControlsLandingGearEmerOff.Id, FlightControlsLandingGearEmerOff);
            CommandList.Add(FlightControlsNwheelSteerToggle.Id, FlightControlsNwheelSteerToggle);
            CommandList.Add(FlightControlsTailWheelLockToggle.Id, FlightControlsTailWheelLockToggle);
            CommandList.Add(FlightControlsTailWheelLockEngage.Id, FlightControlsTailWheelLockEngage);
            CommandList.Add(FlightControlsWaterRudderDown.Id, FlightControlsWaterRudderDown);
            CommandList.Add(FlightControlsWaterRudderUp.Id, FlightControlsWaterRudderUp);
            CommandList.Add(FlightControlsWaterRudderToggle.Id, FlightControlsWaterRudderToggle);
            CommandList.Add(FlightControlsLeftBrake.Id, FlightControlsLeftBrake);
            CommandList.Add(FlightControlsRightBrake.Id, FlightControlsRightBrake);
            CommandList.Add(FlightControlsBrakesToggleRegular.Id, FlightControlsBrakesToggleRegular);
            CommandList.Add(FlightControlsBrakesToggleMax.Id, FlightControlsBrakesToggleMax);
            CommandList.Add(FlightControlsBrakesRegular.Id, FlightControlsBrakesRegular);
            CommandList.Add(FlightControlsBrakesMax.Id, FlightControlsBrakesMax);
            CommandList.Add(FlightControlsBrakesToggleAuto.Id, FlightControlsBrakesToggleAuto);
            CommandList.Add(FlightControlsBrakesDnAuto.Id, FlightControlsBrakesDnAuto);
            CommandList.Add(FlightControlsBrakesUpAuto.Id, FlightControlsBrakesUpAuto);
            CommandList.Add(FlightControlsBrakesOffAuto.Id, FlightControlsBrakesOffAuto);
            CommandList.Add(FlightControlsBrakesRtoAuto.Id, FlightControlsBrakesRtoAuto);
            CommandList.Add(FlightControlsBrakes1Auto.Id, FlightControlsBrakes1Auto);
            CommandList.Add(FlightControlsBrakes2Auto.Id, FlightControlsBrakes2Auto);
            CommandList.Add(FlightControlsBrakes3Auto.Id, FlightControlsBrakes3Auto);
            CommandList.Add(FlightControlsBrakesMaxAuto.Id, FlightControlsBrakesMaxAuto);
            CommandList.Add(SystemsYawDamperOn.Id, SystemsYawDamperOn);
            CommandList.Add(SystemsYawDamperOff.Id, SystemsYawDamperOff);
            CommandList.Add(SystemsYawDamperToggle.Id, SystemsYawDamperToggle);
            CommandList.Add(SystemsPropSyncOn.Id, SystemsPropSyncOn);
            CommandList.Add(SystemsPropSyncOff.Id, SystemsPropSyncOff);
            CommandList.Add(SystemsPropSyncToggle.Id, SystemsPropSyncToggle);
            CommandList.Add(SystemsFeatherModeDown.Id, SystemsFeatherModeDown);
            CommandList.Add(SystemsFeatherModeUp.Id, SystemsFeatherModeUp);
            CommandList.Add(SystemsFeatherModeOff.Id, SystemsFeatherModeOff);
            CommandList.Add(SystemsFeatherModeArm.Id, SystemsFeatherModeArm);
            CommandList.Add(SystemsFeatherModeTest.Id, SystemsFeatherModeTest);
            CommandList.Add(FlightControlsHydraulicOn.Id, FlightControlsHydraulicOn);
            CommandList.Add(FlightControlsHydraulicOff.Id, FlightControlsHydraulicOff);
            CommandList.Add(FlightControlsHydraulicTog.Id, FlightControlsHydraulicTog);
            CommandList.Add(FlightControlsTailhookDown.Id, FlightControlsTailhookDown);
            CommandList.Add(FlightControlsTailhookUp.Id, FlightControlsTailhookUp);
            CommandList.Add(FlightControlsTailhookToggle.Id, FlightControlsTailhookToggle);
            CommandList.Add(FlightControlsCanopyOpen.Id, FlightControlsCanopyOpen);
            CommandList.Add(FlightControlsCanopyClose.Id, FlightControlsCanopyClose);
            CommandList.Add(FlightControlsCanopyToggle.Id, FlightControlsCanopyToggle);
            CommandList.Add(FlightControlsRotorBrakeToggle.Id, FlightControlsRotorBrakeToggle);
            CommandList.Add(FlightControlsHotelModeToggle.Id, FlightControlsHotelModeToggle);
            CommandList.Add(SystemsArtificialStabilityToggle.Id, SystemsArtificialStabilityToggle);
            CommandList.Add(FlightControlsPuffersToggle.Id, FlightControlsPuffersToggle);
            CommandList.Add(EnginesRocketsUp.Id, EnginesRocketsUp);
            CommandList.Add(EnginesRocketsDown.Id, EnginesRocketsDown);
            CommandList.Add(EnginesRocketsLeft.Id, EnginesRocketsLeft);
            CommandList.Add(EnginesRocketsRight.Id, EnginesRocketsRight);
            CommandList.Add(EnginesRocketsForward.Id, EnginesRocketsForward);
            CommandList.Add(EnginesRocketsAft.Id, EnginesRocketsAft);
            CommandList.Add(FuelFuelTankSelectorLftOne.Id, FuelFuelTankSelectorLftOne);
            CommandList.Add(FuelFuelTankSelectorRgtOne.Id, FuelFuelTankSelectorRgtOne);
            CommandList.Add(FuelFuelTankPump1On.Id, FuelFuelTankPump1On);
            CommandList.Add(FuelFuelTankPump2On.Id, FuelFuelTankPump2On);
            CommandList.Add(FuelFuelTankPump3On.Id, FuelFuelTankPump3On);
            CommandList.Add(FuelFuelTankPump4On.Id, FuelFuelTankPump4On);
            CommandList.Add(FuelFuelTankPump5On.Id, FuelFuelTankPump5On);
            CommandList.Add(FuelFuelTankPump6On.Id, FuelFuelTankPump6On);
            CommandList.Add(FuelFuelTankPump7On.Id, FuelFuelTankPump7On);
            CommandList.Add(FuelFuelTankPump8On.Id, FuelFuelTankPump8On);
            CommandList.Add(FuelFuelTankPump9On.Id, FuelFuelTankPump9On);
            CommandList.Add(FuelFuelTankPump1Off.Id, FuelFuelTankPump1Off);
            CommandList.Add(FuelFuelTankPump2Off.Id, FuelFuelTankPump2Off);
            CommandList.Add(FuelFuelTankPump3Off.Id, FuelFuelTankPump3Off);
            CommandList.Add(FuelFuelTankPump4Off.Id, FuelFuelTankPump4Off);
            CommandList.Add(FuelFuelTankPump5Off.Id, FuelFuelTankPump5Off);
            CommandList.Add(FuelFuelTankPump6Off.Id, FuelFuelTankPump6Off);
            CommandList.Add(FuelFuelTankPump7Off.Id, FuelFuelTankPump7Off);
            CommandList.Add(FuelFuelTankPump8Off.Id, FuelFuelTankPump8Off);
            CommandList.Add(FuelFuelTankPump9Off.Id, FuelFuelTankPump9Off);
            CommandList.Add(FuelFuelSelectorNone.Id, FuelFuelSelectorNone);
            CommandList.Add(FuelFuelSelectorLft.Id, FuelFuelSelectorLft);
            CommandList.Add(FuelFuelSelectorCtr.Id, FuelFuelSelectorCtr);
            CommandList.Add(FuelFuelSelectorRgt.Id, FuelFuelSelectorRgt);
            CommandList.Add(FuelFuelSelectorAll.Id, FuelFuelSelectorAll);
            CommandList.Add(FuelLeftFuelSelectorNone.Id, FuelLeftFuelSelectorNone);
            CommandList.Add(FuelLeftFuelSelectorLft.Id, FuelLeftFuelSelectorLft);
            CommandList.Add(FuelLeftFuelSelectorCtr.Id, FuelLeftFuelSelectorCtr);
            CommandList.Add(FuelLeftFuelSelectorRgt.Id, FuelLeftFuelSelectorRgt);
            CommandList.Add(FuelLeftFuelSelectorAll.Id, FuelLeftFuelSelectorAll);
            CommandList.Add(FuelRightFuelSelectorNone.Id, FuelRightFuelSelectorNone);
            CommandList.Add(FuelRightFuelSelectorLft.Id, FuelRightFuelSelectorLft);
            CommandList.Add(FuelRightFuelSelectorCtr.Id, FuelRightFuelSelectorCtr);
            CommandList.Add(FuelRightFuelSelectorRgt.Id, FuelRightFuelSelectorRgt);
            CommandList.Add(FuelRightFuelSelectorAll.Id, FuelRightFuelSelectorAll);
            CommandList.Add(FuelFuelTransferToLft.Id, FuelFuelTransferToLft);
            CommandList.Add(FuelFuelTransferToCtr.Id, FuelFuelTransferToCtr);
            CommandList.Add(FuelFuelTransferToRgt.Id, FuelFuelTransferToRgt);
            CommandList.Add(FuelFuelTransferToAft.Id, FuelFuelTransferToAft);
            CommandList.Add(FuelFuelTransferToOff.Id, FuelFuelTransferToOff);
            CommandList.Add(FuelFuelTransferFromLft.Id, FuelFuelTransferFromLft);
            CommandList.Add(FuelFuelTransferFromCtr.Id, FuelFuelTransferFromCtr);
            CommandList.Add(FuelFuelTransferFromRgt.Id, FuelFuelTransferFromRgt);
            CommandList.Add(FuelFuelTransferFromAft.Id, FuelFuelTransferFromAft);
            CommandList.Add(FuelFuelTransferFromOff.Id, FuelFuelTransferFromOff);
            CommandList.Add(FuelFuelCrossfeedFromLftTank.Id, FuelFuelCrossfeedFromLftTank);
            CommandList.Add(FuelFuelCrossfeedOff.Id, FuelFuelCrossfeedOff);
            CommandList.Add(FuelFuelCrossfeedFromRgtTank.Id, FuelFuelCrossfeedFromRgtTank);
            CommandList.Add(FuelFuelFirewallValveLftOpen.Id, FuelFuelFirewallValveLftOpen);
            CommandList.Add(FuelFuelFirewallValveLftClosed.Id, FuelFuelFirewallValveLftClosed);
            CommandList.Add(FuelFuelFirewallValveRgtOpen.Id, FuelFuelFirewallValveRgtOpen);
            CommandList.Add(FuelFuelFirewallValveRgtClosed.Id, FuelFuelFirewallValveRgtClosed);
            CommandList.Add(FuelLeftXferOverride.Id, FuelLeftXferOverride);
            CommandList.Add(FuelLeftXferOn.Id, FuelLeftXferOn);
            CommandList.Add(FuelLeftXferOff.Id, FuelLeftXferOff);
            CommandList.Add(FuelLeftXferUp.Id, FuelLeftXferUp);
            CommandList.Add(FuelLeftXferDn.Id, FuelLeftXferDn);
            CommandList.Add(FuelRightXferOverride.Id, FuelRightXferOverride);
            CommandList.Add(FuelRightXferOn.Id, FuelRightXferOn);
            CommandList.Add(FuelRightXferOff.Id, FuelRightXferOff);
            CommandList.Add(FuelRightXferUp.Id, FuelRightXferUp);
            CommandList.Add(FuelRightXferDn.Id, FuelRightXferDn);
            CommandList.Add(FuelLeftXferTest.Id, FuelLeftXferTest);
            CommandList.Add(FuelRightXferTest.Id, FuelRightXferTest);
            CommandList.Add(FuelAutoCrossfeedOnOpen.Id, FuelAutoCrossfeedOnOpen);
            CommandList.Add(FuelAutoCrossfeedAuto.Id, FuelAutoCrossfeedAuto);
            CommandList.Add(FuelAutoCrossfeedOff.Id, FuelAutoCrossfeedOff);
            CommandList.Add(FuelAutoCrossfeedUp.Id, FuelAutoCrossfeedUp);
            CommandList.Add(FuelAutoCrossfeedDown.Id, FuelAutoCrossfeedDown);
            CommandList.Add(FuelFuelPumpsOn.Id, FuelFuelPumpsOn);
            CommandList.Add(FuelFuelPumpsOff.Id, FuelFuelPumpsOff);
            CommandList.Add(FuelFuelPumpsTog.Id, FuelFuelPumpsTog);
            CommandList.Add(FuelFuelPump1On.Id, FuelFuelPump1On);
            CommandList.Add(FuelFuelPump2On.Id, FuelFuelPump2On);
            CommandList.Add(FuelFuelPump3On.Id, FuelFuelPump3On);
            CommandList.Add(FuelFuelPump4On.Id, FuelFuelPump4On);
            CommandList.Add(FuelFuelPump5On.Id, FuelFuelPump5On);
            CommandList.Add(FuelFuelPump6On.Id, FuelFuelPump6On);
            CommandList.Add(FuelFuelPump7On.Id, FuelFuelPump7On);
            CommandList.Add(FuelFuelPump8On.Id, FuelFuelPump8On);
            CommandList.Add(FuelFuelPump1Off.Id, FuelFuelPump1Off);
            CommandList.Add(FuelFuelPump2Off.Id, FuelFuelPump2Off);
            CommandList.Add(FuelFuelPump3Off.Id, FuelFuelPump3Off);
            CommandList.Add(FuelFuelPump4Off.Id, FuelFuelPump4Off);
            CommandList.Add(FuelFuelPump5Off.Id, FuelFuelPump5Off);
            CommandList.Add(FuelFuelPump6Off.Id, FuelFuelPump6Off);
            CommandList.Add(FuelFuelPump7Off.Id, FuelFuelPump7Off);
            CommandList.Add(FuelFuelPump8Off.Id, FuelFuelPump8Off);
            CommandList.Add(FuelFuelPump1Tog.Id, FuelFuelPump1Tog);
            CommandList.Add(FuelFuelPump2Tog.Id, FuelFuelPump2Tog);
            CommandList.Add(FuelFuelPump3Tog.Id, FuelFuelPump3Tog);
            CommandList.Add(FuelFuelPump4Tog.Id, FuelFuelPump4Tog);
            CommandList.Add(FuelFuelPump5Tog.Id, FuelFuelPump5Tog);
            CommandList.Add(FuelFuelPump6Tog.Id, FuelFuelPump6Tog);
            CommandList.Add(FuelFuelPump7Tog.Id, FuelFuelPump7Tog);
            CommandList.Add(FuelFuelPump8Tog.Id, FuelFuelPump8Tog);
            CommandList.Add(FuelFuelPump1Prime.Id, FuelFuelPump1Prime);
            CommandList.Add(FuelFuelPump2Prime.Id, FuelFuelPump2Prime);
            CommandList.Add(FuelFuelPump3Prime.Id, FuelFuelPump3Prime);
            CommandList.Add(FuelFuelPump4Prime.Id, FuelFuelPump4Prime);
            CommandList.Add(FuelFuelPump5Prime.Id, FuelFuelPump5Prime);
            CommandList.Add(FuelFuelPump6Prime.Id, FuelFuelPump6Prime);
            CommandList.Add(FuelFuelPump7Prime.Id, FuelFuelPump7Prime);
            CommandList.Add(FuelFuelPump8Prime.Id, FuelFuelPump8Prime);
            CommandList.Add(ElectricalCrossTieOn.Id, ElectricalCrossTieOn);
            CommandList.Add(ElectricalCrossTieOff.Id, ElectricalCrossTieOff);
            CommandList.Add(ElectricalCrossTieToggle.Id, ElectricalCrossTieToggle);
            CommandList.Add(ElectricalInvertersOn.Id, ElectricalInvertersOn);
            CommandList.Add(ElectricalInvertersOff.Id, ElectricalInvertersOff);
            CommandList.Add(ElectricalInvertersToggle.Id, ElectricalInvertersToggle);
            CommandList.Add(ElectricalInverter1On.Id, ElectricalInverter1On);
            CommandList.Add(ElectricalInverter1Off.Id, ElectricalInverter1Off);
            CommandList.Add(ElectricalInverter1Toggle.Id, ElectricalInverter1Toggle);
            CommandList.Add(ElectricalInverter2On.Id, ElectricalInverter2On);
            CommandList.Add(ElectricalInverter2Off.Id, ElectricalInverter2Off);
            CommandList.Add(ElectricalInverter2Toggle.Id, ElectricalInverter2Toggle);
            CommandList.Add(ElectricalBatteriesToggle.Id, ElectricalBatteriesToggle);
            CommandList.Add(ElectricalBattery1On.Id, ElectricalBattery1On);
            CommandList.Add(ElectricalBattery2On.Id, ElectricalBattery2On);
            CommandList.Add(ElectricalBattery1Off.Id, ElectricalBattery1Off);
            CommandList.Add(ElectricalBattery2Off.Id, ElectricalBattery2Off);
            CommandList.Add(ElectricalBattery1Toggle.Id, ElectricalBattery1Toggle);
            CommandList.Add(ElectricalBattery2Toggle.Id, ElectricalBattery2Toggle);
            CommandList.Add(ElectricalGeneratorsToggle.Id, ElectricalGeneratorsToggle);
            CommandList.Add(ElectricalGenerator1Off.Id, ElectricalGenerator1Off);
            CommandList.Add(ElectricalGenerator2Off.Id, ElectricalGenerator2Off);
            CommandList.Add(ElectricalGenerator3Off.Id, ElectricalGenerator3Off);
            CommandList.Add(ElectricalGenerator4Off.Id, ElectricalGenerator4Off);
            CommandList.Add(ElectricalGenerator5Off.Id, ElectricalGenerator5Off);
            CommandList.Add(ElectricalGenerator6Off.Id, ElectricalGenerator6Off);
            CommandList.Add(ElectricalGenerator7Off.Id, ElectricalGenerator7Off);
            CommandList.Add(ElectricalGenerator8Off.Id, ElectricalGenerator8Off);
            CommandList.Add(ElectricalGenerator1On.Id, ElectricalGenerator1On);
            CommandList.Add(ElectricalGenerator2On.Id, ElectricalGenerator2On);
            CommandList.Add(ElectricalGenerator3On.Id, ElectricalGenerator3On);
            CommandList.Add(ElectricalGenerator4On.Id, ElectricalGenerator4On);
            CommandList.Add(ElectricalGenerator5On.Id, ElectricalGenerator5On);
            CommandList.Add(ElectricalGenerator6On.Id, ElectricalGenerator6On);
            CommandList.Add(ElectricalGenerator7On.Id, ElectricalGenerator7On);
            CommandList.Add(ElectricalGenerator8On.Id, ElectricalGenerator8On);
            CommandList.Add(ElectricalGenerator1Toggle.Id, ElectricalGenerator1Toggle);
            CommandList.Add(ElectricalGenerator2Toggle.Id, ElectricalGenerator2Toggle);
            CommandList.Add(ElectricalGenerator3Toggle.Id, ElectricalGenerator3Toggle);
            CommandList.Add(ElectricalGenerator4Toggle.Id, ElectricalGenerator4Toggle);
            CommandList.Add(ElectricalGenerator5Toggle.Id, ElectricalGenerator5Toggle);
            CommandList.Add(ElectricalGenerator6Toggle.Id, ElectricalGenerator6Toggle);
            CommandList.Add(ElectricalGenerator7Toggle.Id, ElectricalGenerator7Toggle);
            CommandList.Add(ElectricalGenerator8Toggle.Id, ElectricalGenerator8Toggle);
            CommandList.Add(ElectricalGenerator1Reset.Id, ElectricalGenerator1Reset);
            CommandList.Add(ElectricalGenerator2Reset.Id, ElectricalGenerator2Reset);
            CommandList.Add(ElectricalGenerator3Reset.Id, ElectricalGenerator3Reset);
            CommandList.Add(ElectricalGenerator4Reset.Id, ElectricalGenerator4Reset);
            CommandList.Add(ElectricalGenerator5Reset.Id, ElectricalGenerator5Reset);
            CommandList.Add(ElectricalGenerator6Reset.Id, ElectricalGenerator6Reset);
            CommandList.Add(ElectricalGenerator7Reset.Id, ElectricalGenerator7Reset);
            CommandList.Add(ElectricalGenerator8Reset.Id, ElectricalGenerator8Reset);
            CommandList.Add(ElectricalAPUStart.Id, ElectricalAPUStart);
            CommandList.Add(ElectricalAPUOn.Id, ElectricalAPUOn);
            CommandList.Add(ElectricalAPUOff.Id, ElectricalAPUOff);
            CommandList.Add(ElectricalAPUFireShutoff.Id, ElectricalAPUFireShutoff);
            CommandList.Add(ElectricalAPUGeneratorOn.Id, ElectricalAPUGeneratorOn);
            CommandList.Add(ElectricalAPUGeneratorOff.Id, ElectricalAPUGeneratorOff);
            CommandList.Add(ElectricalGPUOn.Id, ElectricalGPUOn);
            CommandList.Add(ElectricalGPUOff.Id, ElectricalGPUOff);
            CommandList.Add(ElectricalGPUToggle.Id, ElectricalGPUToggle);
            CommandList.Add(ElectricalRecharge.Id, ElectricalRecharge);
            CommandList.Add(LightsNavLightsOn.Id, LightsNavLightsOn);
            CommandList.Add(LightsNavLightsOff.Id, LightsNavLightsOff);
            CommandList.Add(LightsNavLightsToggle.Id, LightsNavLightsToggle);
            CommandList.Add(LightsBeaconLightsOn.Id, LightsBeaconLightsOn);
            CommandList.Add(LightsBeaconLightsOff.Id, LightsBeaconLightsOff);
            CommandList.Add(LightsBeaconLightsToggle.Id, LightsBeaconLightsToggle);
            CommandList.Add(LightsStrobeLightsOn.Id, LightsStrobeLightsOn);
            CommandList.Add(LightsStrobeLightsOff.Id, LightsStrobeLightsOff);
            CommandList.Add(LightsStrobeLightsToggle.Id, LightsStrobeLightsToggle);
            CommandList.Add(LightsTaxiLightsOn.Id, LightsTaxiLightsOn);
            CommandList.Add(LightsTaxiLightsOff.Id, LightsTaxiLightsOff);
            CommandList.Add(LightsTaxiLightsToggle.Id, LightsTaxiLightsToggle);
            CommandList.Add(LightsLandingLightsOn.Id, LightsLandingLightsOn);
            CommandList.Add(LightsLandingLightsOff.Id, LightsLandingLightsOff);
            CommandList.Add(LightsLandingLightsToggle.Id, LightsLandingLightsToggle);
            CommandList.Add(LightsLanding01LightOn.Id, LightsLanding01LightOn);
            CommandList.Add(LightsLanding02LightOn.Id, LightsLanding02LightOn);
            CommandList.Add(LightsLanding03LightOn.Id, LightsLanding03LightOn);
            CommandList.Add(LightsLanding04LightOn.Id, LightsLanding04LightOn);
            CommandList.Add(LightsLanding05LightOn.Id, LightsLanding05LightOn);
            CommandList.Add(LightsLanding06LightOn.Id, LightsLanding06LightOn);
            CommandList.Add(LightsLanding07LightOn.Id, LightsLanding07LightOn);
            CommandList.Add(LightsLanding08LightOn.Id, LightsLanding08LightOn);
            CommandList.Add(LightsLanding09LightOn.Id, LightsLanding09LightOn);
            CommandList.Add(LightsLanding10LightOn.Id, LightsLanding10LightOn);
            CommandList.Add(LightsLanding11LightOn.Id, LightsLanding11LightOn);
            CommandList.Add(LightsLanding12LightOn.Id, LightsLanding12LightOn);
            CommandList.Add(LightsLanding13LightOn.Id, LightsLanding13LightOn);
            CommandList.Add(LightsLanding14LightOn.Id, LightsLanding14LightOn);
            CommandList.Add(LightsLanding15LightOn.Id, LightsLanding15LightOn);
            CommandList.Add(LightsLanding16LightOn.Id, LightsLanding16LightOn);
            CommandList.Add(LightsLanding01LightOff.Id, LightsLanding01LightOff);
            CommandList.Add(LightsLanding02LightOff.Id, LightsLanding02LightOff);
            CommandList.Add(LightsLanding03LightOff.Id, LightsLanding03LightOff);
            CommandList.Add(LightsLanding04LightOff.Id, LightsLanding04LightOff);
            CommandList.Add(LightsLanding05LightOff.Id, LightsLanding05LightOff);
            CommandList.Add(LightsLanding06LightOff.Id, LightsLanding06LightOff);
            CommandList.Add(LightsLanding07LightOff.Id, LightsLanding07LightOff);
            CommandList.Add(LightsLanding08LightOff.Id, LightsLanding08LightOff);
            CommandList.Add(LightsLanding09LightOff.Id, LightsLanding09LightOff);
            CommandList.Add(LightsLanding10LightOff.Id, LightsLanding10LightOff);
            CommandList.Add(LightsLanding11LightOff.Id, LightsLanding11LightOff);
            CommandList.Add(LightsLanding12LightOff.Id, LightsLanding12LightOff);
            CommandList.Add(LightsLanding13LightOff.Id, LightsLanding13LightOff);
            CommandList.Add(LightsLanding14LightOff.Id, LightsLanding14LightOff);
            CommandList.Add(LightsLanding15LightOff.Id, LightsLanding15LightOff);
            CommandList.Add(LightsLanding16LightOff.Id, LightsLanding16LightOff);
            CommandList.Add(LightsLanding01LightTog.Id, LightsLanding01LightTog);
            CommandList.Add(LightsLanding02LightTog.Id, LightsLanding02LightTog);
            CommandList.Add(LightsLanding03LightTog.Id, LightsLanding03LightTog);
            CommandList.Add(LightsLanding04LightTog.Id, LightsLanding04LightTog);
            CommandList.Add(LightsLanding05LightTog.Id, LightsLanding05LightTog);
            CommandList.Add(LightsLanding06LightTog.Id, LightsLanding06LightTog);
            CommandList.Add(LightsLanding07LightTog.Id, LightsLanding07LightTog);
            CommandList.Add(LightsLanding08LightTog.Id, LightsLanding08LightTog);
            CommandList.Add(LightsLanding09LightTog.Id, LightsLanding09LightTog);
            CommandList.Add(LightsLanding10LightTog.Id, LightsLanding10LightTog);
            CommandList.Add(LightsLanding11LightTog.Id, LightsLanding11LightTog);
            CommandList.Add(LightsLanding12LightTog.Id, LightsLanding12LightTog);
            CommandList.Add(LightsLanding13LightTog.Id, LightsLanding13LightTog);
            CommandList.Add(LightsLanding14LightTog.Id, LightsLanding14LightTog);
            CommandList.Add(LightsLanding15LightTog.Id, LightsLanding15LightTog);
            CommandList.Add(LightsLanding16LightTog.Id, LightsLanding16LightTog);
            CommandList.Add(LightsGeneric01LightTog.Id, LightsGeneric01LightTog);
            CommandList.Add(LightsGeneric02LightTog.Id, LightsGeneric02LightTog);
            CommandList.Add(LightsGeneric03LightTog.Id, LightsGeneric03LightTog);
            CommandList.Add(LightsGeneric04LightTog.Id, LightsGeneric04LightTog);
            CommandList.Add(LightsGeneric05LightTog.Id, LightsGeneric05LightTog);
            CommandList.Add(LightsGeneric06LightTog.Id, LightsGeneric06LightTog);
            CommandList.Add(LightsGeneric07LightTog.Id, LightsGeneric07LightTog);
            CommandList.Add(LightsGeneric08LightTog.Id, LightsGeneric08LightTog);
            CommandList.Add(LightsGeneric09LightTog.Id, LightsGeneric09LightTog);
            CommandList.Add(LightsGeneric10LightTog.Id, LightsGeneric10LightTog);
            CommandList.Add(LightsGeneric11LightTog.Id, LightsGeneric11LightTog);
            CommandList.Add(LightsGeneric12LightTog.Id, LightsGeneric12LightTog);
            CommandList.Add(LightsGeneric13LightTog.Id, LightsGeneric13LightTog);
            CommandList.Add(LightsGeneric14LightTog.Id, LightsGeneric14LightTog);
            CommandList.Add(LightsGeneric15LightTog.Id, LightsGeneric15LightTog);
            CommandList.Add(LightsGeneric16LightTog.Id, LightsGeneric16LightTog);
            CommandList.Add(LightsGeneric17LightTog.Id, LightsGeneric17LightTog);
            CommandList.Add(LightsGeneric18LightTog.Id, LightsGeneric18LightTog);
            CommandList.Add(LightsGeneric19LightTog.Id, LightsGeneric19LightTog);
            CommandList.Add(LightsGeneric20LightTog.Id, LightsGeneric20LightTog);
            CommandList.Add(LightsGeneric21LightTog.Id, LightsGeneric21LightTog);
            CommandList.Add(LightsGeneric22LightTog.Id, LightsGeneric22LightTog);
            CommandList.Add(LightsGeneric23LightTog.Id, LightsGeneric23LightTog);
            CommandList.Add(LightsGeneric24LightTog.Id, LightsGeneric24LightTog);
            CommandList.Add(LightsGeneric25LightTog.Id, LightsGeneric25LightTog);
            CommandList.Add(LightsGeneric26LightTog.Id, LightsGeneric26LightTog);
            CommandList.Add(LightsGeneric27LightTog.Id, LightsGeneric27LightTog);
            CommandList.Add(LightsGeneric28LightTog.Id, LightsGeneric28LightTog);
            CommandList.Add(LightsGeneric29LightTog.Id, LightsGeneric29LightTog);
            CommandList.Add(LightsGeneric30LightTog.Id, LightsGeneric30LightTog);
            CommandList.Add(LightsGeneric31LightTog.Id, LightsGeneric31LightTog);
            CommandList.Add(LightsGeneric32LightTog.Id, LightsGeneric32LightTog);
            CommandList.Add(LightsGeneric33LightTog.Id, LightsGeneric33LightTog);
            CommandList.Add(LightsGeneric34LightTog.Id, LightsGeneric34LightTog);
            CommandList.Add(LightsGeneric35LightTog.Id, LightsGeneric35LightTog);
            CommandList.Add(LightsGeneric36LightTog.Id, LightsGeneric36LightTog);
            CommandList.Add(LightsGeneric37LightTog.Id, LightsGeneric37LightTog);
            CommandList.Add(LightsGeneric38LightTog.Id, LightsGeneric38LightTog);
            CommandList.Add(LightsGeneric39LightTog.Id, LightsGeneric39LightTog);
            CommandList.Add(LightsGeneric40LightTog.Id, LightsGeneric40LightTog);
            CommandList.Add(LightsGeneric41LightTog.Id, LightsGeneric41LightTog);
            CommandList.Add(LightsGeneric42LightTog.Id, LightsGeneric42LightTog);
            CommandList.Add(LightsGeneric43LightTog.Id, LightsGeneric43LightTog);
            CommandList.Add(LightsGeneric44LightTog.Id, LightsGeneric44LightTog);
            CommandList.Add(LightsGeneric45LightTog.Id, LightsGeneric45LightTog);
            CommandList.Add(LightsGeneric46LightTog.Id, LightsGeneric46LightTog);
            CommandList.Add(LightsGeneric47LightTog.Id, LightsGeneric47LightTog);
            CommandList.Add(LightsGeneric48LightTog.Id, LightsGeneric48LightTog);
            CommandList.Add(LightsGeneric49LightTog.Id, LightsGeneric49LightTog);
            CommandList.Add(LightsGeneric50LightTog.Id, LightsGeneric50LightTog);
            CommandList.Add(LightsGeneric51LightTog.Id, LightsGeneric51LightTog);
            CommandList.Add(LightsGeneric52LightTog.Id, LightsGeneric52LightTog);
            CommandList.Add(LightsGeneric53LightTog.Id, LightsGeneric53LightTog);
            CommandList.Add(LightsGeneric54LightTog.Id, LightsGeneric54LightTog);
            CommandList.Add(LightsGeneric55LightTog.Id, LightsGeneric55LightTog);
            CommandList.Add(LightsGeneric56LightTog.Id, LightsGeneric56LightTog);
            CommandList.Add(LightsGeneric57LightTog.Id, LightsGeneric57LightTog);
            CommandList.Add(LightsGeneric58LightTog.Id, LightsGeneric58LightTog);
            CommandList.Add(LightsGeneric59LightTog.Id, LightsGeneric59LightTog);
            CommandList.Add(LightsGeneric60LightTog.Id, LightsGeneric60LightTog);
            CommandList.Add(LightsGeneric61LightTog.Id, LightsGeneric61LightTog);
            CommandList.Add(LightsGeneric62LightTog.Id, LightsGeneric62LightTog);
            CommandList.Add(LightsGeneric63LightTog.Id, LightsGeneric63LightTog);
            CommandList.Add(LightsGeneric64LightTog.Id, LightsGeneric64LightTog);
            CommandList.Add(LightsSpotLightsOn.Id, LightsSpotLightsOn);
            CommandList.Add(LightsSpotLightsOff.Id, LightsSpotLightsOff);
            CommandList.Add(LightsSpotLightsToggle.Id, LightsSpotLightsToggle);
            CommandList.Add(SystemsAvionicsOn.Id, SystemsAvionicsOn);
            CommandList.Add(SystemsAvionicsOff.Id, SystemsAvionicsOff);
            CommandList.Add(SystemsAvionicsToggle.Id, SystemsAvionicsToggle);
            CommandList.Add(BleedAirBleedAirDown.Id, BleedAirBleedAirDown);
            CommandList.Add(BleedAirBleedAirUp.Id, BleedAirBleedAirUp);
            CommandList.Add(BleedAirBleedAirOff.Id, BleedAirBleedAirOff);
            CommandList.Add(BleedAirBleedAirLeft.Id, BleedAirBleedAirLeft);
            CommandList.Add(BleedAirBleedAirBoth.Id, BleedAirBleedAirBoth);
            CommandList.Add(BleedAirBleedAirRight.Id, BleedAirBleedAirRight);
            CommandList.Add(BleedAirBleedAirApu.Id, BleedAirBleedAirApu);
            CommandList.Add(BleedAirBleedAirAuto.Id, BleedAirBleedAirAuto);
            CommandList.Add(BleedAirBleedAirLeftOn.Id, BleedAirBleedAirLeftOn);
            CommandList.Add(BleedAirBleedAirLeftInsOnly.Id, BleedAirBleedAirLeftInsOnly);
            CommandList.Add(BleedAirBleedAirLeftOff.Id, BleedAirBleedAirLeftOff);
            CommandList.Add(BleedAirBleedAirRightOn.Id, BleedAirBleedAirRightOn);
            CommandList.Add(BleedAirBleedAirRightInsOnly.Id, BleedAirBleedAirRightInsOnly);
            CommandList.Add(BleedAirBleedAirRightOff.Id, BleedAirBleedAirRightOff);
            CommandList.Add(BleedAirEngine1Off.Id, BleedAirEngine1Off);
            CommandList.Add(BleedAirEngine2Off.Id, BleedAirEngine2Off);
            CommandList.Add(BleedAirEngine3Off.Id, BleedAirEngine3Off);
            CommandList.Add(BleedAirEngine4Off.Id, BleedAirEngine4Off);
            CommandList.Add(BleedAirEngine5Off.Id, BleedAirEngine5Off);
            CommandList.Add(BleedAirEngine6Off.Id, BleedAirEngine6Off);
            CommandList.Add(BleedAirEngine7Off.Id, BleedAirEngine7Off);
            CommandList.Add(BleedAirEngine8Off.Id, BleedAirEngine8Off);
            CommandList.Add(BleedAirEngine1On.Id, BleedAirEngine1On);
            CommandList.Add(BleedAirEngine2On.Id, BleedAirEngine2On);
            CommandList.Add(BleedAirEngine3On.Id, BleedAirEngine3On);
            CommandList.Add(BleedAirEngine4On.Id, BleedAirEngine4On);
            CommandList.Add(BleedAirEngine5On.Id, BleedAirEngine5On);
            CommandList.Add(BleedAirEngine6On.Id, BleedAirEngine6On);
            CommandList.Add(BleedAirEngine7On.Id, BleedAirEngine7On);
            CommandList.Add(BleedAirEngine8On.Id, BleedAirEngine8On);
            CommandList.Add(BleedAirEngine1Toggle.Id, BleedAirEngine1Toggle);
            CommandList.Add(BleedAirEngine2Toggle.Id, BleedAirEngine2Toggle);
            CommandList.Add(BleedAirEngine3Toggle.Id, BleedAirEngine3Toggle);
            CommandList.Add(BleedAirEngine4Toggle.Id, BleedAirEngine4Toggle);
            CommandList.Add(BleedAirEngine5Toggle.Id, BleedAirEngine5Toggle);
            CommandList.Add(BleedAirEngine6Toggle.Id, BleedAirEngine6Toggle);
            CommandList.Add(BleedAirEngine7Toggle.Id, BleedAirEngine7Toggle);
            CommandList.Add(BleedAirEngine8Toggle.Id, BleedAirEngine8Toggle);
            CommandList.Add(BleedAirGpuOff.Id, BleedAirGpuOff);
            CommandList.Add(BleedAirGpuOn.Id, BleedAirGpuOn);
            CommandList.Add(BleedAirGpuToggle.Id, BleedAirGpuToggle);
            CommandList.Add(BleedAirApuOff.Id, BleedAirApuOff);
            CommandList.Add(BleedAirApuOn.Id, BleedAirApuOn);
            CommandList.Add(BleedAirApuToggle.Id, BleedAirApuToggle);
            CommandList.Add(BleedAirIsolationLeftShut.Id, BleedAirIsolationLeftShut);
            CommandList.Add(BleedAirIsolationLeftOpen.Id, BleedAirIsolationLeftOpen);
            CommandList.Add(BleedAirIsolationLeftToggle.Id, BleedAirIsolationLeftToggle);
            CommandList.Add(BleedAirIsolationRightShut.Id, BleedAirIsolationRightShut);
            CommandList.Add(BleedAirIsolationRightOpen.Id, BleedAirIsolationRightOpen);
            CommandList.Add(BleedAirIsolationRightToggle.Id, BleedAirIsolationRightToggle);
            CommandList.Add(BleedAirPackLeftOff.Id, BleedAirPackLeftOff);
            CommandList.Add(BleedAirPackLeftOn.Id, BleedAirPackLeftOn);
            CommandList.Add(BleedAirPackLeftToggle.Id, BleedAirPackLeftToggle);
            CommandList.Add(BleedAirPackCenterOff.Id, BleedAirPackCenterOff);
            CommandList.Add(BleedAirPackCenterOn.Id, BleedAirPackCenterOn);
            CommandList.Add(BleedAirPackCenterToggle.Id, BleedAirPackCenterToggle);
            CommandList.Add(BleedAirPackRightOff.Id, BleedAirPackRightOff);
            CommandList.Add(BleedAirPackRightOn.Id, BleedAirPackRightOn);
            CommandList.Add(BleedAirPackRightToggle.Id, BleedAirPackRightToggle);
            CommandList.Add(PressurizationTest.Id, PressurizationTest);
            CommandList.Add(PressurizationDumpOn.Id, PressurizationDumpOn);
            CommandList.Add(PressurizationDumpOff.Id, PressurizationDumpOff);
            CommandList.Add(PressurizationVviDown.Id, PressurizationVviDown);
            CommandList.Add(PressurizationVviUp.Id, PressurizationVviUp);
            CommandList.Add(PressurizationCabinAltDown.Id, PressurizationCabinAltDown);
            CommandList.Add(PressurizationCabinAltUp.Id, PressurizationCabinAltUp);
            CommandList.Add(PressurizationAircondOn.Id, PressurizationAircondOn);
            CommandList.Add(PressurizationAircondOff.Id, PressurizationAircondOff);
            CommandList.Add(PressurizationHeaterOn.Id, PressurizationHeaterOn);
            CommandList.Add(PressurizationHeaterGrdMax.Id, PressurizationHeaterGrdMax);
            CommandList.Add(PressurizationHeaterOff.Id, PressurizationHeaterOff);
            CommandList.Add(PressurizationHeaterUp.Id, PressurizationHeaterUp);
            CommandList.Add(PressurizationHeaterDn.Id, PressurizationHeaterDn);
            CommandList.Add(PressurizationFanAuto.Id, PressurizationFanAuto);
            CommandList.Add(PressurizationFanLow.Id, PressurizationFanLow);
            CommandList.Add(PressurizationFanHigh.Id, PressurizationFanHigh);
            CommandList.Add(PressurizationFanUp.Id, PressurizationFanUp);
            CommandList.Add(PressurizationFanDown.Id, PressurizationFanDown);
            CommandList.Add(IceAntiIceToggle.Id, IceAntiIceToggle);
            CommandList.Add(IceAlternateStaticPort.Id, IceAlternateStaticPort);
            CommandList.Add(IcePitotHeat0On.Id, IcePitotHeat0On);
            CommandList.Add(IcePitotHeat1On.Id, IcePitotHeat1On);
            CommandList.Add(IcePitotHeat0Off.Id, IcePitotHeat0Off);
            CommandList.Add(IcePitotHeat1Off.Id, IcePitotHeat1Off);
            CommandList.Add(IcePitotHeat0Tog.Id, IcePitotHeat0Tog);
            CommandList.Add(IcePitotHeat1Tog.Id, IcePitotHeat1Tog);
            CommandList.Add(IceStaticHeat0On.Id, IceStaticHeat0On);
            CommandList.Add(IceStaticHeat1On.Id, IceStaticHeat1On);
            CommandList.Add(IceStaticHeat0Off.Id, IceStaticHeat0Off);
            CommandList.Add(IceStaticHeat1Off.Id, IceStaticHeat1Off);
            CommandList.Add(IceStaticHeat0Tog.Id, IceStaticHeat0Tog);
            CommandList.Add(IceStaticHeat1Tog.Id, IceStaticHeat1Tog);
            CommandList.Add(IceAOAHeat0On.Id, IceAOAHeat0On);
            CommandList.Add(IceAOAHeat1On.Id, IceAOAHeat1On);
            CommandList.Add(IceAOAHeat0Off.Id, IceAOAHeat0Off);
            CommandList.Add(IceAOAHeat1Off.Id, IceAOAHeat1Off);
            CommandList.Add(IceAOAHeat0Tog.Id, IceAOAHeat0Tog);
            CommandList.Add(IceAOAHeat1Tog.Id, IceAOAHeat1Tog);
            CommandList.Add(IceWindowHeatOn.Id, IceWindowHeatOn);
            CommandList.Add(IceWindowHeatOff.Id, IceWindowHeatOff);
            CommandList.Add(IceWindowHeatTog.Id, IceWindowHeatTog);
            CommandList.Add(IceWingHeatOn.Id, IceWingHeatOn);
            CommandList.Add(IceWingHeat0On.Id, IceWingHeat0On);
            CommandList.Add(IceWingHeat1On.Id, IceWingHeat1On);
            CommandList.Add(IceWingHeatOff.Id, IceWingHeatOff);
            CommandList.Add(IceWingHeat0Off.Id, IceWingHeat0Off);
            CommandList.Add(IceWingHeat1Off.Id, IceWingHeat1Off);
            CommandList.Add(IceWingHeatTog.Id, IceWingHeatTog);
            CommandList.Add(IceWingHeat0Tog.Id, IceWingHeat0Tog);
            CommandList.Add(IceWingHeat1Tog.Id, IceWingHeat1Tog);
            CommandList.Add(IceWingBootOn.Id, IceWingBootOn);
            CommandList.Add(IceWingBoot0On.Id, IceWingBoot0On);
            CommandList.Add(IceWingBoot1On.Id, IceWingBoot1On);
            CommandList.Add(IceWingBootOff.Id, IceWingBootOff);
            CommandList.Add(IceWingBoot0Off.Id, IceWingBoot0Off);
            CommandList.Add(IceWingBoot1Off.Id, IceWingBoot1Off);
            CommandList.Add(IceWingBootTog.Id, IceWingBootTog);
            CommandList.Add(IceWingBoot0Tog.Id, IceWingBoot0Tog);
            CommandList.Add(IceWingBoot1Tog.Id, IceWingBoot1Tog);
            CommandList.Add(IceWingTaiOn.Id, IceWingTaiOn);
            CommandList.Add(IceWingTai0On.Id, IceWingTai0On);
            CommandList.Add(IceWingTai1On.Id, IceWingTai1On);
            CommandList.Add(IceWingTaiOff.Id, IceWingTaiOff);
            CommandList.Add(IceWingTai0Off.Id, IceWingTai0Off);
            CommandList.Add(IceWingTai1Off.Id, IceWingTai1Off);
            CommandList.Add(IceWingTaiTog.Id, IceWingTaiTog);
            CommandList.Add(IceWingTai0Tog.Id, IceWingTai0Tog);
            CommandList.Add(IceWingTai1Tog.Id, IceWingTai1Tog);
            CommandList.Add(IceWingTksOn.Id, IceWingTksOn);
            CommandList.Add(IceWingTks0On.Id, IceWingTks0On);
            CommandList.Add(IceWingTks1On.Id, IceWingTks1On);
            CommandList.Add(IceWingTksHigh.Id, IceWingTksHigh);
            CommandList.Add(IceWingTks0High.Id, IceWingTks0High);
            CommandList.Add(IceWingTks1High.Id, IceWingTks1High);
            CommandList.Add(IceWingTksOff.Id, IceWingTksOff);
            CommandList.Add(IceWingTks0Off.Id, IceWingTks0Off);
            CommandList.Add(IceWingTks1Off.Id, IceWingTks1Off);
            CommandList.Add(IceWingTksTog.Id, IceWingTksTog);
            CommandList.Add(IceWingTks0Tog.Id, IceWingTks0Tog);
            CommandList.Add(IceWingTks1Tog.Id, IceWingTks1Tog);
            CommandList.Add(IceInletHeatOn.Id, IceInletHeatOn);
            CommandList.Add(IceInletHeatOff.Id, IceInletHeatOff);
            CommandList.Add(IceInletHeatTog.Id, IceInletHeatTog);
            CommandList.Add(IceInletHeat0On.Id, IceInletHeat0On);
            CommandList.Add(IceInletHeat1On.Id, IceInletHeat1On);
            CommandList.Add(IceInletHeat2On.Id, IceInletHeat2On);
            CommandList.Add(IceInletHeat3On.Id, IceInletHeat3On);
            CommandList.Add(IceInletHeat4On.Id, IceInletHeat4On);
            CommandList.Add(IceInletHeat5On.Id, IceInletHeat5On);
            CommandList.Add(IceInletHeat6On.Id, IceInletHeat6On);
            CommandList.Add(IceInletHeat7On.Id, IceInletHeat7On);
            CommandList.Add(IceInletHeat0Off.Id, IceInletHeat0Off);
            CommandList.Add(IceInletHeat1Off.Id, IceInletHeat1Off);
            CommandList.Add(IceInletHeat2Off.Id, IceInletHeat2Off);
            CommandList.Add(IceInletHeat3Off.Id, IceInletHeat3Off);
            CommandList.Add(IceInletHeat4Off.Id, IceInletHeat4Off);
            CommandList.Add(IceInletHeat5Off.Id, IceInletHeat5Off);
            CommandList.Add(IceInletHeat6Off.Id, IceInletHeat6Off);
            CommandList.Add(IceInletHeat7Off.Id, IceInletHeat7Off);
            CommandList.Add(IceInletHeat0Tog.Id, IceInletHeat0Tog);
            CommandList.Add(IceInletHeat1Tog.Id, IceInletHeat1Tog);
            CommandList.Add(IceInletHeat2Tog.Id, IceInletHeat2Tog);
            CommandList.Add(IceInletHeat3Tog.Id, IceInletHeat3Tog);
            CommandList.Add(IceInletHeat4Tog.Id, IceInletHeat4Tog);
            CommandList.Add(IceInletHeat5Tog.Id, IceInletHeat5Tog);
            CommandList.Add(IceInletHeat6Tog.Id, IceInletHeat6Tog);
            CommandList.Add(IceInletHeat7Tog.Id, IceInletHeat7Tog);
            CommandList.Add(IceInletEai0On.Id, IceInletEai0On);
            CommandList.Add(IceInletEai1On.Id, IceInletEai1On);
            CommandList.Add(IceInletEai2On.Id, IceInletEai2On);
            CommandList.Add(IceInletEai3On.Id, IceInletEai3On);
            CommandList.Add(IceInletEai4On.Id, IceInletEai4On);
            CommandList.Add(IceInletEai5On.Id, IceInletEai5On);
            CommandList.Add(IceInletEai6On.Id, IceInletEai6On);
            CommandList.Add(IceInletEai7On.Id, IceInletEai7On);
            CommandList.Add(IceInletEai0Off.Id, IceInletEai0Off);
            CommandList.Add(IceInletEai1Off.Id, IceInletEai1Off);
            CommandList.Add(IceInletEai2Off.Id, IceInletEai2Off);
            CommandList.Add(IceInletEai3Off.Id, IceInletEai3Off);
            CommandList.Add(IceInletEai4Off.Id, IceInletEai4Off);
            CommandList.Add(IceInletEai5Off.Id, IceInletEai5Off);
            CommandList.Add(IceInletEai6Off.Id, IceInletEai6Off);
            CommandList.Add(IceInletEai7Off.Id, IceInletEai7Off);
            CommandList.Add(IceInletEai0Tog.Id, IceInletEai0Tog);
            CommandList.Add(IceInletEai1Tog.Id, IceInletEai1Tog);
            CommandList.Add(IceInletEai2Tog.Id, IceInletEai2Tog);
            CommandList.Add(IceInletEai3Tog.Id, IceInletEai3Tog);
            CommandList.Add(IceInletEai4Tog.Id, IceInletEai4Tog);
            CommandList.Add(IceInletEai5Tog.Id, IceInletEai5Tog);
            CommandList.Add(IceInletEai6Tog.Id, IceInletEai6Tog);
            CommandList.Add(IceInletEai7Tog.Id, IceInletEai7Tog);
            CommandList.Add(IcePropHeatOn.Id, IcePropHeatOn);
            CommandList.Add(IcePropHeatOff.Id, IcePropHeatOff);
            CommandList.Add(IcePropHeatTog.Id, IcePropHeatTog);
            CommandList.Add(IcePropHeat0On.Id, IcePropHeat0On);
            CommandList.Add(IcePropHeat1On.Id, IcePropHeat1On);
            CommandList.Add(IcePropHeat2On.Id, IcePropHeat2On);
            CommandList.Add(IcePropHeat3On.Id, IcePropHeat3On);
            CommandList.Add(IcePropHeat4On.Id, IcePropHeat4On);
            CommandList.Add(IcePropHeat5On.Id, IcePropHeat5On);
            CommandList.Add(IcePropHeat6On.Id, IcePropHeat6On);
            CommandList.Add(IcePropHeat7On.Id, IcePropHeat7On);
            CommandList.Add(IcePropHeat0Off.Id, IcePropHeat0Off);
            CommandList.Add(IcePropHeat1Off.Id, IcePropHeat1Off);
            CommandList.Add(IcePropHeat2Off.Id, IcePropHeat2Off);
            CommandList.Add(IcePropHeat3Off.Id, IcePropHeat3Off);
            CommandList.Add(IcePropHeat4Off.Id, IcePropHeat4Off);
            CommandList.Add(IcePropHeat5Off.Id, IcePropHeat5Off);
            CommandList.Add(IcePropHeat6Off.Id, IcePropHeat6Off);
            CommandList.Add(IcePropHeat7Off.Id, IcePropHeat7Off);
            CommandList.Add(IcePropHeat0Tog.Id, IcePropHeat0Tog);
            CommandList.Add(IcePropHeat1Tog.Id, IcePropHeat1Tog);
            CommandList.Add(IcePropHeat2Tog.Id, IcePropHeat2Tog);
            CommandList.Add(IcePropHeat3Tog.Id, IcePropHeat3Tog);
            CommandList.Add(IcePropHeat4Tog.Id, IcePropHeat4Tog);
            CommandList.Add(IcePropHeat5Tog.Id, IcePropHeat5Tog);
            CommandList.Add(IcePropHeat6Tog.Id, IcePropHeat6Tog);
            CommandList.Add(IcePropHeat7Tog.Id, IcePropHeat7Tog);
            CommandList.Add(IcePropTksOn.Id, IcePropTksOn);
            CommandList.Add(IcePropTksHigh.Id, IcePropTksHigh);
            CommandList.Add(IcePropTksOff.Id, IcePropTksOff);
            CommandList.Add(IcePropTksTog.Id, IcePropTksTog);
            CommandList.Add(IcePropTks0On.Id, IcePropTks0On);
            CommandList.Add(IcePropTks1On.Id, IcePropTks1On);
            CommandList.Add(IcePropTks2On.Id, IcePropTks2On);
            CommandList.Add(IcePropTks3On.Id, IcePropTks3On);
            CommandList.Add(IcePropTks4On.Id, IcePropTks4On);
            CommandList.Add(IcePropTks5On.Id, IcePropTks5On);
            CommandList.Add(IcePropTks6On.Id, IcePropTks6On);
            CommandList.Add(IcePropTks7On.Id, IcePropTks7On);
            CommandList.Add(IcePropTks0High.Id, IcePropTks0High);
            CommandList.Add(IcePropTks1High.Id, IcePropTks1High);
            CommandList.Add(IcePropTks2High.Id, IcePropTks2High);
            CommandList.Add(IcePropTks3High.Id, IcePropTks3High);
            CommandList.Add(IcePropTks4High.Id, IcePropTks4High);
            CommandList.Add(IcePropTks5High.Id, IcePropTks5High);
            CommandList.Add(IcePropTks6High.Id, IcePropTks6High);
            CommandList.Add(IcePropTks7High.Id, IcePropTks7High);
            CommandList.Add(IcePropTks0Off.Id, IcePropTks0Off);
            CommandList.Add(IcePropTks1Off.Id, IcePropTks1Off);
            CommandList.Add(IcePropTks2Off.Id, IcePropTks2Off);
            CommandList.Add(IcePropTks3Off.Id, IcePropTks3Off);
            CommandList.Add(IcePropTks4Off.Id, IcePropTks4Off);
            CommandList.Add(IcePropTks5Off.Id, IcePropTks5Off);
            CommandList.Add(IcePropTks6Off.Id, IcePropTks6Off);
            CommandList.Add(IcePropTks7Off.Id, IcePropTks7Off);
            CommandList.Add(IcePropTks0Tog.Id, IcePropTks0Tog);
            CommandList.Add(IcePropTks1Tog.Id, IcePropTks1Tog);
            CommandList.Add(IcePropTks2Tog.Id, IcePropTks2Tog);
            CommandList.Add(IcePropTks3Tog.Id, IcePropTks3Tog);
            CommandList.Add(IcePropTks4Tog.Id, IcePropTks4Tog);
            CommandList.Add(IcePropTks5Tog.Id, IcePropTks5Tog);
            CommandList.Add(IcePropTks6Tog.Id, IcePropTks6Tog);
            CommandList.Add(IcePropTks7Tog.Id, IcePropTks7Tog);
            CommandList.Add(IceDetectOn.Id, IceDetectOn);
            CommandList.Add(IceDetectOff.Id, IceDetectOff);
            CommandList.Add(OxyCrewValveOn.Id, OxyCrewValveOn);
            CommandList.Add(OxyCrewValveOff.Id, OxyCrewValveOff);
            CommandList.Add(OxyCrewValveToggle.Id, OxyCrewValveToggle);
            CommandList.Add(OxyCrewRegulatorUp.Id, OxyCrewRegulatorUp);
            CommandList.Add(OxyCrewRegulatorDown.Id, OxyCrewRegulatorDown);
            CommandList.Add(OxyPassengerO2On.Id, OxyPassengerO2On);
            CommandList.Add(FlightControlsParachuteFlares.Id, FlightControlsParachuteFlares);
            CommandList.Add(FlightControlsSmokeToggle.Id, FlightControlsSmokeToggle);
            CommandList.Add(FlightControlsWaterScoopToggle.Id, FlightControlsWaterScoopToggle);
            CommandList.Add(FlightControlsBoost.Id, FlightControlsBoost);
            CommandList.Add(FlightControlsIgniteJato.Id, FlightControlsIgniteJato);
            CommandList.Add(FlightControlsJettisonPayload.Id, FlightControlsJettisonPayload);
            CommandList.Add(FlightControlsDumpFuelOn.Id, FlightControlsDumpFuelOn);
            CommandList.Add(FlightControlsDumpFuelOff.Id, FlightControlsDumpFuelOff);
            CommandList.Add(FlightControlsDumpFuelToggle.Id, FlightControlsDumpFuelToggle);
            CommandList.Add(FlightControlsDeployParachute.Id, FlightControlsDeployParachute);
            CommandList.Add(FlightControlsEject.Id, FlightControlsEject);
            CommandList.Add(FlightControlsDropTank.Id, FlightControlsDropTank);
            CommandList.Add(WeaponsReArmAircraft.Id, WeaponsReArmAircraft);
            CommandList.Add(WeaponsMasterArmOn.Id, WeaponsMasterArmOn);
            CommandList.Add(WeaponsMasterArmOff.Id, WeaponsMasterArmOff);
            CommandList.Add(WeaponsFireModeDown.Id, WeaponsFireModeDown);
            CommandList.Add(WeaponsFireModeUp.Id, WeaponsFireModeUp);
            CommandList.Add(WeaponsFireRateDown.Id, WeaponsFireRateDown);
            CommandList.Add(WeaponsFireRateUp.Id, WeaponsFireRateUp);
            CommandList.Add(WeaponsWeaponSelectDown.Id, WeaponsWeaponSelectDown);
            CommandList.Add(WeaponsWeaponSelectUp.Id, WeaponsWeaponSelectUp);
            CommandList.Add(WeaponsFireGuns.Id, WeaponsFireGuns);
            CommandList.Add(WeaponsFireAirToAir.Id, WeaponsFireAirToAir);
            CommandList.Add(WeaponsFireAirToGround.Id, WeaponsFireAirToGround);
            CommandList.Add(WeaponsFireAnyArmed.Id, WeaponsFireAnyArmed);
            CommandList.Add(WeaponsFireAnyShell.Id, WeaponsFireAnyShell);
            CommandList.Add(WeaponsGPSLockHere.Id, WeaponsGPSLockHere);
            CommandList.Add(WeaponsWeaponTargetDown.Id, WeaponsWeaponTargetDown);
            CommandList.Add(WeaponsWeaponTargetUp.Id, WeaponsWeaponTargetUp);
            CommandList.Add(WeaponsDeployChaff.Id, WeaponsDeployChaff);
            CommandList.Add(WeaponsDeployFlares.Id, WeaponsDeployFlares);
            CommandList.Add(OperationPrevLivery.Id, OperationPrevLivery);
            CommandList.Add(OperationNextLivery.Id, OperationNextLivery);
            CommandList.Add(SystemsSeatbeltSignToggle.Id, SystemsSeatbeltSignToggle);
            CommandList.Add(SystemsNoSmokingToggle.Id, SystemsNoSmokingToggle);
            CommandList.Add(SystemsWipersDn.Id, SystemsWipersDn);
            CommandList.Add(SystemsWipersUp.Id, SystemsWipersUp);
            CommandList.Add(LightsSpotLightLeft.Id, LightsSpotLightLeft);
            CommandList.Add(LightsSpotLightRight.Id, LightsSpotLightRight);
            CommandList.Add(LightsSpotLightUp.Id, LightsSpotLightUp);
            CommandList.Add(LightsSpotLightDown.Id, LightsSpotLightDown);
            CommandList.Add(LightsSpotLightCenter.Id, LightsSpotLightCenter);
            CommandList.Add(FlightControlsDoorOpen1.Id, FlightControlsDoorOpen1);
            CommandList.Add(FlightControlsDoorOpen2.Id, FlightControlsDoorOpen2);
            CommandList.Add(FlightControlsDoorOpen3.Id, FlightControlsDoorOpen3);
            CommandList.Add(FlightControlsDoorOpen4.Id, FlightControlsDoorOpen4);
            CommandList.Add(FlightControlsDoorOpen5.Id, FlightControlsDoorOpen5);
            CommandList.Add(FlightControlsDoorOpen6.Id, FlightControlsDoorOpen6);
            CommandList.Add(FlightControlsDoorOpen7.Id, FlightControlsDoorOpen7);
            CommandList.Add(FlightControlsDoorOpen8.Id, FlightControlsDoorOpen8);
            CommandList.Add(FlightControlsDoorOpen9.Id, FlightControlsDoorOpen9);
            CommandList.Add(FlightControlsDoorOpen10.Id, FlightControlsDoorOpen10);
            CommandList.Add(FlightControlsDoorClose1.Id, FlightControlsDoorClose1);
            CommandList.Add(FlightControlsDoorClose2.Id, FlightControlsDoorClose2);
            CommandList.Add(FlightControlsDoorClose3.Id, FlightControlsDoorClose3);
            CommandList.Add(FlightControlsDoorClose4.Id, FlightControlsDoorClose4);
            CommandList.Add(FlightControlsDoorClose5.Id, FlightControlsDoorClose5);
            CommandList.Add(FlightControlsDoorClose6.Id, FlightControlsDoorClose6);
            CommandList.Add(FlightControlsDoorClose7.Id, FlightControlsDoorClose7);
            CommandList.Add(FlightControlsDoorClose8.Id, FlightControlsDoorClose8);
            CommandList.Add(FlightControlsDoorClose9.Id, FlightControlsDoorClose9);
            CommandList.Add(FlightControlsDoorClose10.Id, FlightControlsDoorClose10);
            CommandList.Add(GeneralAction.Id, GeneralAction);
            CommandList.Add(FlightControlsGliderTowRelease.Id, FlightControlsGliderTowRelease);
            CommandList.Add(FlightControlsWinchRelease.Id, FlightControlsWinchRelease);
            CommandList.Add(FlightControlsGliderAllRelease.Id, FlightControlsGliderAllRelease);
            CommandList.Add(FlightControlsEngageCatShot.Id, FlightControlsEngageCatShot);
            CommandList.Add(FlightControlsGliderTowLeft.Id, FlightControlsGliderTowLeft);
            CommandList.Add(FlightControlsGliderTowStraight.Id, FlightControlsGliderTowStraight);
            CommandList.Add(FlightControlsGliderTowRight.Id, FlightControlsGliderTowRight);
            CommandList.Add(FlightControlsWinchFaster.Id, FlightControlsWinchFaster);
            CommandList.Add(FlightControlsWinchSlower.Id, FlightControlsWinchSlower);
            CommandList.Add(GroundOpsServicePlane.Id, GroundOpsServicePlane);
            CommandList.Add(GroundOpsPushbackLeft.Id, GroundOpsPushbackLeft);
            CommandList.Add(GroundOpsPushbackStraight.Id, GroundOpsPushbackStraight);
            CommandList.Add(GroundOpsPushbackRight.Id, GroundOpsPushbackRight);
            CommandList.Add(GroundOpsPushbackStop.Id, GroundOpsPushbackStop);
            CommandList.Add(GroundOpsToggleWindow.Id, GroundOpsToggleWindow);
            CommandList.Add(RadiosPowerNav1Off.Id, RadiosPowerNav1Off);
            CommandList.Add(RadiosPowerNav1On.Id, RadiosPowerNav1On);
            CommandList.Add(RadiosPowerNav2Off.Id, RadiosPowerNav2Off);
            CommandList.Add(RadiosPowerNav2On.Id, RadiosPowerNav2On);
            CommandList.Add(RadiosPowerCom1Off.Id, RadiosPowerCom1Off);
            CommandList.Add(RadiosPowerCom1On.Id, RadiosPowerCom1On);
            CommandList.Add(RadiosPowerCom2Off.Id, RadiosPowerCom2Off);
            CommandList.Add(RadiosPowerCom2On.Id, RadiosPowerCom2On);
            CommandList.Add(RadiosPowerAdf1Dn.Id, RadiosPowerAdf1Dn);
            CommandList.Add(RadiosPowerAdf1Up.Id, RadiosPowerAdf1Up);
            CommandList.Add(RadiosPowerAdf2Dn.Id, RadiosPowerAdf2Dn);
            CommandList.Add(RadiosPowerAdf2Up.Id, RadiosPowerAdf2Up);
            CommandList.Add(RadiosAdf1PowerMode0.Id, RadiosAdf1PowerMode0);
            CommandList.Add(RadiosAdf1PowerMode1.Id, RadiosAdf1PowerMode1);
            CommandList.Add(RadiosAdf1PowerMode2.Id, RadiosAdf1PowerMode2);
            CommandList.Add(RadiosAdf1PowerMode3.Id, RadiosAdf1PowerMode3);
            CommandList.Add(RadiosAdf1PowerMode4.Id, RadiosAdf1PowerMode4);
            CommandList.Add(RadiosAdf2PowerMode0.Id, RadiosAdf2PowerMode0);
            CommandList.Add(RadiosAdf2PowerMode1.Id, RadiosAdf2PowerMode1);
            CommandList.Add(RadiosAdf2PowerMode2.Id, RadiosAdf2PowerMode2);
            CommandList.Add(RadiosAdf2PowerMode3.Id, RadiosAdf2PowerMode3);
            CommandList.Add(RadiosAdf2PowerMode4.Id, RadiosAdf2PowerMode4);
            CommandList.Add(RadiosActvCom1CoarseDown.Id, RadiosActvCom1CoarseDown);
            CommandList.Add(RadiosActvCom1CoarseUp.Id, RadiosActvCom1CoarseUp);
            CommandList.Add(RadiosActvCom1FineDown.Id, RadiosActvCom1FineDown);
            CommandList.Add(RadiosActvCom1FineUp.Id, RadiosActvCom1FineUp);
            CommandList.Add(RadiosActvCom1CoarseDown833.Id, RadiosActvCom1CoarseDown833);
            CommandList.Add(RadiosActvCom1CoarseUp833.Id, RadiosActvCom1CoarseUp833);
            CommandList.Add(RadiosActvCom1FineDown833.Id, RadiosActvCom1FineDown833);
            CommandList.Add(RadiosActvCom1FineUp833.Id, RadiosActvCom1FineUp833);
            CommandList.Add(RadiosStbyCom1CoarseDown.Id, RadiosStbyCom1CoarseDown);
            CommandList.Add(RadiosStbyCom1CoarseUp.Id, RadiosStbyCom1CoarseUp);
            CommandList.Add(RadiosStbyCom1FineDown.Id, RadiosStbyCom1FineDown);
            CommandList.Add(RadiosStbyCom1FineUp.Id, RadiosStbyCom1FineUp);
            CommandList.Add(RadiosStbyCom1CoarseDown833.Id, RadiosStbyCom1CoarseDown833);
            CommandList.Add(RadiosStbyCom1CoarseUp833.Id, RadiosStbyCom1CoarseUp833);
            CommandList.Add(RadiosStbyCom1FineDown833.Id, RadiosStbyCom1FineDown833);
            CommandList.Add(RadiosStbyCom1FineUp833.Id, RadiosStbyCom1FineUp833);
            CommandList.Add(RadiosActvCom2CoarseDown.Id, RadiosActvCom2CoarseDown);
            CommandList.Add(RadiosActvCom2CoarseUp.Id, RadiosActvCom2CoarseUp);
            CommandList.Add(RadiosActvCom2FineDown.Id, RadiosActvCom2FineDown);
            CommandList.Add(RadiosActvCom2FineUp.Id, RadiosActvCom2FineUp);
            CommandList.Add(RadiosActvCom2CoarseDown833.Id, RadiosActvCom2CoarseDown833);
            CommandList.Add(RadiosActvCom2CoarseUp833.Id, RadiosActvCom2CoarseUp833);
            CommandList.Add(RadiosActvCom2FineDown833.Id, RadiosActvCom2FineDown833);
            CommandList.Add(RadiosActvCom2FineUp833.Id, RadiosActvCom2FineUp833);
            CommandList.Add(RadiosStbyCom2CoarseDown.Id, RadiosStbyCom2CoarseDown);
            CommandList.Add(RadiosStbyCom2CoarseUp.Id, RadiosStbyCom2CoarseUp);
            CommandList.Add(RadiosStbyCom2FineDown.Id, RadiosStbyCom2FineDown);
            CommandList.Add(RadiosStbyCom2FineUp.Id, RadiosStbyCom2FineUp);
            CommandList.Add(RadiosStbyCom2CoarseDown833.Id, RadiosStbyCom2CoarseDown833);
            CommandList.Add(RadiosStbyCom2CoarseUp833.Id, RadiosStbyCom2CoarseUp833);
            CommandList.Add(RadiosStbyCom2FineDown833.Id, RadiosStbyCom2FineDown833);
            CommandList.Add(RadiosStbyCom2FineUp833.Id, RadiosStbyCom2FineUp833);
            CommandList.Add(RadiosActvNav1CoarseDown.Id, RadiosActvNav1CoarseDown);
            CommandList.Add(RadiosActvNav1CoarseUp.Id, RadiosActvNav1CoarseUp);
            CommandList.Add(RadiosActvNav1FineDown.Id, RadiosActvNav1FineDown);
            CommandList.Add(RadiosActvNav1FineUp.Id, RadiosActvNav1FineUp);
            CommandList.Add(RadiosStbyNav1CoarseDown.Id, RadiosStbyNav1CoarseDown);
            CommandList.Add(RadiosStbyNav1CoarseUp.Id, RadiosStbyNav1CoarseUp);
            CommandList.Add(RadiosStbyNav1FineDown.Id, RadiosStbyNav1FineDown);
            CommandList.Add(RadiosStbyNav1FineUp.Id, RadiosStbyNav1FineUp);
            CommandList.Add(RadiosActvNav2CoarseDown.Id, RadiosActvNav2CoarseDown);
            CommandList.Add(RadiosActvNav2CoarseUp.Id, RadiosActvNav2CoarseUp);
            CommandList.Add(RadiosActvNav2FineDown.Id, RadiosActvNav2FineDown);
            CommandList.Add(RadiosActvNav2FineUp.Id, RadiosActvNav2FineUp);
            CommandList.Add(RadiosStbyNav2CoarseDown.Id, RadiosStbyNav2CoarseDown);
            CommandList.Add(RadiosStbyNav2CoarseUp.Id, RadiosStbyNav2CoarseUp);
            CommandList.Add(RadiosStbyNav2FineDown.Id, RadiosStbyNav2FineDown);
            CommandList.Add(RadiosStbyNav2FineUp.Id, RadiosStbyNav2FineUp);
            CommandList.Add(RadiosActvDmeCoarseDown.Id, RadiosActvDmeCoarseDown);
            CommandList.Add(RadiosActvDmeCoarseUp.Id, RadiosActvDmeCoarseUp);
            CommandList.Add(RadiosActvDmeFineDown.Id, RadiosActvDmeFineDown);
            CommandList.Add(RadiosActvDmeFineUp.Id, RadiosActvDmeFineUp);
            CommandList.Add(RadiosStbyDmeCoarseDown.Id, RadiosStbyDmeCoarseDown);
            CommandList.Add(RadiosStbyDmeCoarseUp.Id, RadiosStbyDmeCoarseUp);
            CommandList.Add(RadiosStbyDmeFineDown.Id, RadiosStbyDmeFineDown);
            CommandList.Add(RadiosStbyDmeFineUp.Id, RadiosStbyDmeFineUp);
            CommandList.Add(RadiosActvAdf1HundredsDown.Id, RadiosActvAdf1HundredsDown);
            CommandList.Add(RadiosActvAdf1HundredsUp.Id, RadiosActvAdf1HundredsUp);
            CommandList.Add(RadiosActvAdf1TensDown.Id, RadiosActvAdf1TensDown);
            CommandList.Add(RadiosActvAdf1TensUp.Id, RadiosActvAdf1TensUp);
            CommandList.Add(RadiosActvAdf1OnesDown.Id, RadiosActvAdf1OnesDown);
            CommandList.Add(RadiosActvAdf1OnesUp.Id, RadiosActvAdf1OnesUp);
            CommandList.Add(RadiosActvAdf1OnesTensDown.Id, RadiosActvAdf1OnesTensDown);
            CommandList.Add(RadiosActvAdf1OnesTensUp.Id, RadiosActvAdf1OnesTensUp);
            CommandList.Add(RadiosActvAdf1HundredsThousDown.Id, RadiosActvAdf1HundredsThousDown);
            CommandList.Add(RadiosActvAdf1HundredsThousUp.Id, RadiosActvAdf1HundredsThousUp);
            CommandList.Add(RadiosActvAdf14DigHundredsDown.Id, RadiosActvAdf14DigHundredsDown);
            CommandList.Add(RadiosActvAdf14DigHundredsUp.Id, RadiosActvAdf14DigHundredsUp);
            CommandList.Add(RadiosActvAdf14DigTensDown.Id, RadiosActvAdf14DigTensDown);
            CommandList.Add(RadiosActvAdf14DigTensUp.Id, RadiosActvAdf14DigTensUp);
            CommandList.Add(RadiosActvAdf14DigOnesDown.Id, RadiosActvAdf14DigOnesDown);
            CommandList.Add(RadiosActvAdf14DigOnesUp.Id, RadiosActvAdf14DigOnesUp);
            CommandList.Add(RadiosStbyAdf1HundredsDown.Id, RadiosStbyAdf1HundredsDown);
            CommandList.Add(RadiosStbyAdf1HundredsUp.Id, RadiosStbyAdf1HundredsUp);
            CommandList.Add(RadiosStbyAdf1TensDown.Id, RadiosStbyAdf1TensDown);
            CommandList.Add(RadiosStbyAdf1TensUp.Id, RadiosStbyAdf1TensUp);
            CommandList.Add(RadiosStbyAdf1OnesDown.Id, RadiosStbyAdf1OnesDown);
            CommandList.Add(RadiosStbyAdf1OnesUp.Id, RadiosStbyAdf1OnesUp);
            CommandList.Add(RadiosStbyAdf1OnesTensDown.Id, RadiosStbyAdf1OnesTensDown);
            CommandList.Add(RadiosStbyAdf1OnesTensUp.Id, RadiosStbyAdf1OnesTensUp);
            CommandList.Add(RadiosStbyAdf1HundredsThousDown.Id, RadiosStbyAdf1HundredsThousDown);
            CommandList.Add(RadiosStbyAdf1HundredsThousUp.Id, RadiosStbyAdf1HundredsThousUp);
            CommandList.Add(RadiosStbyAdf14DigHundredsDown.Id, RadiosStbyAdf14DigHundredsDown);
            CommandList.Add(RadiosStbyAdf14DigHundredsUp.Id, RadiosStbyAdf14DigHundredsUp);
            CommandList.Add(RadiosStbyAdf14DigTensDown.Id, RadiosStbyAdf14DigTensDown);
            CommandList.Add(RadiosStbyAdf14DigTensUp.Id, RadiosStbyAdf14DigTensUp);
            CommandList.Add(RadiosStbyAdf14DigOnesDown.Id, RadiosStbyAdf14DigOnesDown);
            CommandList.Add(RadiosStbyAdf14DigOnesUp.Id, RadiosStbyAdf14DigOnesUp);
            CommandList.Add(RadiosActvAdf2HundredsDown.Id, RadiosActvAdf2HundredsDown);
            CommandList.Add(RadiosActvAdf2HundredsUp.Id, RadiosActvAdf2HundredsUp);
            CommandList.Add(RadiosActvAdf2TensDown.Id, RadiosActvAdf2TensDown);
            CommandList.Add(RadiosActvAdf2TensUp.Id, RadiosActvAdf2TensUp);
            CommandList.Add(RadiosActvAdf2OnesDown.Id, RadiosActvAdf2OnesDown);
            CommandList.Add(RadiosActvAdf2OnesUp.Id, RadiosActvAdf2OnesUp);
            CommandList.Add(RadiosActvAdf2OnesTensDown.Id, RadiosActvAdf2OnesTensDown);
            CommandList.Add(RadiosActvAdf2OnesTensUp.Id, RadiosActvAdf2OnesTensUp);
            CommandList.Add(RadiosActvAdf2HundredsThousDown.Id, RadiosActvAdf2HundredsThousDown);
            CommandList.Add(RadiosActvAdf2HundredsThousUp.Id, RadiosActvAdf2HundredsThousUp);
            CommandList.Add(RadiosActvAdf24DigHundredsDown.Id, RadiosActvAdf24DigHundredsDown);
            CommandList.Add(RadiosActvAdf24DigHundredsUp.Id, RadiosActvAdf24DigHundredsUp);
            CommandList.Add(RadiosActvAdf24DigTensDown.Id, RadiosActvAdf24DigTensDown);
            CommandList.Add(RadiosActvAdf24DigTensUp.Id, RadiosActvAdf24DigTensUp);
            CommandList.Add(RadiosActvAdf24DigOnesDown.Id, RadiosActvAdf24DigOnesDown);
            CommandList.Add(RadiosActvAdf24DigOnesUp.Id, RadiosActvAdf24DigOnesUp);
            CommandList.Add(RadiosStbyAdf2HundredsDown.Id, RadiosStbyAdf2HundredsDown);
            CommandList.Add(RadiosStbyAdf2HundredsUp.Id, RadiosStbyAdf2HundredsUp);
            CommandList.Add(RadiosStbyAdf2TensDown.Id, RadiosStbyAdf2TensDown);
            CommandList.Add(RadiosStbyAdf2TensUp.Id, RadiosStbyAdf2TensUp);
            CommandList.Add(RadiosStbyAdf2OnesDown.Id, RadiosStbyAdf2OnesDown);
            CommandList.Add(RadiosStbyAdf2OnesUp.Id, RadiosStbyAdf2OnesUp);
            CommandList.Add(RadiosStbyAdf2OnesTensDown.Id, RadiosStbyAdf2OnesTensDown);
            CommandList.Add(RadiosStbyAdf2OnesTensUp.Id, RadiosStbyAdf2OnesTensUp);
            CommandList.Add(RadiosStbyAdf2HundredsThousDown.Id, RadiosStbyAdf2HundredsThousDown);
            CommandList.Add(RadiosStbyAdf2HundredsThousUp.Id, RadiosStbyAdf2HundredsThousUp);
            CommandList.Add(RadiosStbyAdf24DigHundredsDown.Id, RadiosStbyAdf24DigHundredsDown);
            CommandList.Add(RadiosStbyAdf24DigHundredsUp.Id, RadiosStbyAdf24DigHundredsUp);
            CommandList.Add(RadiosStbyAdf24DigTensDown.Id, RadiosStbyAdf24DigTensDown);
            CommandList.Add(RadiosStbyAdf24DigTensUp.Id, RadiosStbyAdf24DigTensUp);
            CommandList.Add(RadiosStbyAdf24DigOnesDown.Id, RadiosStbyAdf24DigOnesDown);
            CommandList.Add(RadiosStbyAdf24DigOnesUp.Id, RadiosStbyAdf24DigOnesUp);
            CommandList.Add(TransponderTransponderDigit0.Id, TransponderTransponderDigit0);
            CommandList.Add(TransponderTransponderDigit1.Id, TransponderTransponderDigit1);
            CommandList.Add(TransponderTransponderDigit2.Id, TransponderTransponderDigit2);
            CommandList.Add(TransponderTransponderDigit3.Id, TransponderTransponderDigit3);
            CommandList.Add(TransponderTransponderDigit4.Id, TransponderTransponderDigit4);
            CommandList.Add(TransponderTransponderDigit5.Id, TransponderTransponderDigit5);
            CommandList.Add(TransponderTransponderDigit6.Id, TransponderTransponderDigit6);
            CommandList.Add(TransponderTransponderDigit7.Id, TransponderTransponderDigit7);
            CommandList.Add(TransponderTransponderCLR.Id, TransponderTransponderCLR);
            CommandList.Add(TransponderTransponderThousandsDown.Id, TransponderTransponderThousandsDown);
            CommandList.Add(TransponderTransponderThousandsUp.Id, TransponderTransponderThousandsUp);
            CommandList.Add(TransponderTransponderHundredsDown.Id, TransponderTransponderHundredsDown);
            CommandList.Add(TransponderTransponderHundredsUp.Id, TransponderTransponderHundredsUp);
            CommandList.Add(TransponderTransponderTensDown.Id, TransponderTransponderTensDown);
            CommandList.Add(TransponderTransponderTensUp.Id, TransponderTransponderTensUp);
            CommandList.Add(TransponderTransponderOnesDown.Id, TransponderTransponderOnesDown);
            CommandList.Add(TransponderTransponderOnesUp.Id, TransponderTransponderOnesUp);
            CommandList.Add(TransponderTransponder12Down.Id, TransponderTransponder12Down);
            CommandList.Add(TransponderTransponder12Up.Id, TransponderTransponder12Up);
            CommandList.Add(TransponderTransponder34Down.Id, TransponderTransponder34Down);
            CommandList.Add(TransponderTransponder34Up.Id, TransponderTransponder34Up);
            CommandList.Add(AudioPanelTransmitAudioCom1.Id, AudioPanelTransmitAudioCom1);
            CommandList.Add(AudioPanelTransmitAudioCom2.Id, AudioPanelTransmitAudioCom2);
            CommandList.Add(AudioPanelMonitorAudioComAuto.Id, AudioPanelMonitorAudioComAuto);
            CommandList.Add(AudioPanelMonitorAudioCom1.Id, AudioPanelMonitorAudioCom1);
            CommandList.Add(AudioPanelMonitorAudioCom2.Id, AudioPanelMonitorAudioCom2);
            CommandList.Add(AudioPanelMonitorAudioNav1.Id, AudioPanelMonitorAudioNav1);
            CommandList.Add(AudioPanelMonitorAudioNav2.Id, AudioPanelMonitorAudioNav2);
            CommandList.Add(AudioPanelMonitorAudioAdf1.Id, AudioPanelMonitorAudioAdf1);
            CommandList.Add(AudioPanelMonitorAudioAdf2.Id, AudioPanelMonitorAudioAdf2);
            CommandList.Add(AudioPanelMonitorAudioDme.Id, AudioPanelMonitorAudioDme);
            CommandList.Add(AudioPanelMonitorAudioMkr.Id, AudioPanelMonitorAudioMkr);
            CommandList.Add(AudioPanelTransmitAudioCom1Man.Id, AudioPanelTransmitAudioCom1Man);
            CommandList.Add(AudioPanelTransmitAudioCom2Man.Id, AudioPanelTransmitAudioCom2Man);
            CommandList.Add(AudioPanelMonitorAudioComAutoOff.Id, AudioPanelMonitorAudioComAutoOff);
            CommandList.Add(AudioPanelMonitorAudioCom1Off.Id, AudioPanelMonitorAudioCom1Off);
            CommandList.Add(AudioPanelMonitorAudioCom2Off.Id, AudioPanelMonitorAudioCom2Off);
            CommandList.Add(AudioPanelMonitorAudioNav1Off.Id, AudioPanelMonitorAudioNav1Off);
            CommandList.Add(AudioPanelMonitorAudioNav2Off.Id, AudioPanelMonitorAudioNav2Off);
            CommandList.Add(AudioPanelMonitorAudioAdf1Off.Id, AudioPanelMonitorAudioAdf1Off);
            CommandList.Add(AudioPanelMonitorAudioAdf2Off.Id, AudioPanelMonitorAudioAdf2Off);
            CommandList.Add(AudioPanelMonitorAudioDmeOff.Id, AudioPanelMonitorAudioDmeOff);
            CommandList.Add(AudioPanelMonitorAudioMkrOff.Id, AudioPanelMonitorAudioMkrOff);
            CommandList.Add(AudioPanelMonitorAudioComAutoOn.Id, AudioPanelMonitorAudioComAutoOn);
            CommandList.Add(AudioPanelMonitorAudioCom1On.Id, AudioPanelMonitorAudioCom1On);
            CommandList.Add(AudioPanelMonitorAudioCom2On.Id, AudioPanelMonitorAudioCom2On);
            CommandList.Add(AudioPanelMonitorAudioNav1On.Id, AudioPanelMonitorAudioNav1On);
            CommandList.Add(AudioPanelMonitorAudioNav2On.Id, AudioPanelMonitorAudioNav2On);
            CommandList.Add(AudioPanelMonitorAudioAdf1On.Id, AudioPanelMonitorAudioAdf1On);
            CommandList.Add(AudioPanelMonitorAudioAdf2On.Id, AudioPanelMonitorAudioAdf2On);
            CommandList.Add(AudioPanelMonitorAudioDmeOn.Id, AudioPanelMonitorAudioDmeOn);
            CommandList.Add(AudioPanelMonitorAudioMkrOn.Id, AudioPanelMonitorAudioMkrOn);
            CommandList.Add(TransponderTransponderIdent.Id, TransponderTransponderIdent);
            CommandList.Add(TransponderTransponderOff.Id, TransponderTransponderOff);
            CommandList.Add(TransponderTransponderStandby.Id, TransponderTransponderStandby);
            CommandList.Add(TransponderTransponderOn.Id, TransponderTransponderOn);
            CommandList.Add(TransponderTransponderAlt.Id, TransponderTransponderAlt);
            CommandList.Add(TransponderTransponderTest.Id, TransponderTransponderTest);
            CommandList.Add(TransponderTransponderGround.Id, TransponderTransponderGround);
            CommandList.Add(TransponderTransponderDn.Id, TransponderTransponderDn);
            CommandList.Add(TransponderTransponderUp.Id, TransponderTransponderUp);
            CommandList.Add(RadiosNav1StandyFlip.Id, RadiosNav1StandyFlip);
            CommandList.Add(RadiosNav2StandyFlip.Id, RadiosNav2StandyFlip);
            CommandList.Add(RadiosCom1StandyFlip.Id, RadiosCom1StandyFlip);
            CommandList.Add(RadiosCom2StandyFlip.Id, RadiosCom2StandyFlip);
            CommandList.Add(RadiosAdf1StandyFlip.Id, RadiosAdf1StandyFlip);
            CommandList.Add(RadiosAdf2StandyFlip.Id, RadiosAdf2StandyFlip);
            CommandList.Add(RadiosDmeStandbyFlip.Id, RadiosDmeStandbyFlip);
            CommandList.Add(RadiosRMILTog.Id, RadiosRMILTog);
            CommandList.Add(RadiosRMIRTog.Id, RadiosRMIRTog);
            CommandList.Add(RadiosCopilotRMILTogCop.Id, RadiosCopilotRMILTogCop);
            CommandList.Add(RadiosCopilotRMIRTogCop.Id, RadiosCopilotRMIRTogCop);
            CommandList.Add(InstrumentsECAMModeUp.Id, InstrumentsECAMModeUp);
            CommandList.Add(InstrumentsECAMModeDown.Id, InstrumentsECAMModeDown);
            CommandList.Add(InstrumentsMapZoomIn.Id, InstrumentsMapZoomIn);
            CommandList.Add(InstrumentsMapZoomOut.Id, InstrumentsMapZoomOut);
            CommandList.Add(InstrumentsEFISWxr.Id, InstrumentsEFISWxr);
            CommandList.Add(InstrumentsEFISTcas.Id, InstrumentsEFISTcas);
            CommandList.Add(InstrumentsEFISApt.Id, InstrumentsEFISApt);
            CommandList.Add(InstrumentsEFISFix.Id, InstrumentsEFISFix);
            CommandList.Add(InstrumentsEFISVor.Id, InstrumentsEFISVor);
            CommandList.Add(InstrumentsEFISNdb.Id, InstrumentsEFISNdb);
            CommandList.Add(InstrumentsEFISPageUp.Id, InstrumentsEFISPageUp);
            CommandList.Add(InstrumentsEFISPageDn.Id, InstrumentsEFISPageDn);
            CommandList.Add(InstrumentsEFISModeUp.Id, InstrumentsEFISModeUp);
            CommandList.Add(InstrumentsEFISModeDn.Id, InstrumentsEFISModeDn);
            CommandList.Add(InstrumentsEFIS1PilotSelDn.Id, InstrumentsEFIS1PilotSelDn);
            CommandList.Add(InstrumentsEFIS1PilotSelUp.Id, InstrumentsEFIS1PilotSelUp);
            CommandList.Add(InstrumentsEFIS1CopilotSelDn.Id, InstrumentsEFIS1CopilotSelDn);
            CommandList.Add(InstrumentsEFIS1CopilotSelUp.Id, InstrumentsEFIS1CopilotSelUp);
            CommandList.Add(InstrumentsEFIS2PilotSelDn.Id, InstrumentsEFIS2PilotSelDn);
            CommandList.Add(InstrumentsEFIS2PilotSelUp.Id, InstrumentsEFIS2PilotSelUp);
            CommandList.Add(InstrumentsEFIS2CopilotSelDn.Id, InstrumentsEFIS2CopilotSelDn);
            CommandList.Add(InstrumentsEFIS2CopilotSelUp.Id, InstrumentsEFIS2CopilotSelUp);
            CommandList.Add(RadiosObs1Down.Id, RadiosObs1Down);
            CommandList.Add(RadiosObs1Up.Id, RadiosObs1Up);
            CommandList.Add(RadiosObs2Down.Id, RadiosObs2Down);
            CommandList.Add(RadiosObs2Up.Id, RadiosObs2Up);
            CommandList.Add(RadiosObsHSIDown.Id, RadiosObsHSIDown);
            CommandList.Add(RadiosObsHSIUp.Id, RadiosObsHSIUp);
            CommandList.Add(RadiosObsHSIDirect.Id, RadiosObsHSIDirect);
            CommandList.Add(RadiosAdf1CardDown.Id, RadiosAdf1CardDown);
            CommandList.Add(RadiosAdf1CardUp.Id, RadiosAdf1CardUp);
            CommandList.Add(RadiosAdf2CardDown.Id, RadiosAdf2CardDown);
            CommandList.Add(RadiosAdf2CardUp.Id, RadiosAdf2CardUp);
            CommandList.Add(RadiosCopilotObs1Down.Id, RadiosCopilotObs1Down);
            CommandList.Add(RadiosCopilotObs1Up.Id, RadiosCopilotObs1Up);
            CommandList.Add(RadiosCopilotObs2Down.Id, RadiosCopilotObs2Down);
            CommandList.Add(RadiosCopilotObs2Up.Id, RadiosCopilotObs2Up);
            CommandList.Add(RadiosCopilotObsHSIDown.Id, RadiosCopilotObsHSIDown);
            CommandList.Add(RadiosCopilotObsHSIUp.Id, RadiosCopilotObsHSIUp);
            CommandList.Add(RadiosCopilotObsHSIDirect.Id, RadiosCopilotObsHSIDirect);
            CommandList.Add(RadiosCopilotAdf1CardDown.Id, RadiosCopilotAdf1CardDown);
            CommandList.Add(RadiosCopilotAdf1CardUp.Id, RadiosCopilotAdf1CardUp);
            CommandList.Add(RadiosCopilotAdf2CardDown.Id, RadiosCopilotAdf2CardDown);
            CommandList.Add(RadiosCopilotAdf2CardUp.Id, RadiosCopilotAdf2CardUp);
            CommandList.Add(AutopilotHsiSelectDown.Id, AutopilotHsiSelectDown);
            CommandList.Add(AutopilotHsiSelectUp.Id, AutopilotHsiSelectUp);
            CommandList.Add(AutopilotHsiSelectNav1.Id, AutopilotHsiSelectNav1);
            CommandList.Add(AutopilotHsiSelectNav2.Id, AutopilotHsiSelectNav2);
            CommandList.Add(AutopilotHsiSelectGps.Id, AutopilotHsiSelectGps);
            CommandList.Add(AutopilotHsiSelectCopilotDown.Id, AutopilotHsiSelectCopilotDown);
            CommandList.Add(AutopilotHsiSelectCopilotUp.Id, AutopilotHsiSelectCopilotUp);
            CommandList.Add(AutopilotHsiSelectCopilotNav1.Id, AutopilotHsiSelectCopilotNav1);
            CommandList.Add(AutopilotHsiSelectCopilotNav2.Id, AutopilotHsiSelectCopilotNav2);
            CommandList.Add(AutopilotHsiSelectCopilotGps.Id, AutopilotHsiSelectCopilotGps);
            CommandList.Add(FlightControlsCarrierILS.Id, FlightControlsCarrierILS);
            CommandList.Add(AutopilotSource01.Id, AutopilotSource01);
            CommandList.Add(AutopilotFdirOn.Id, AutopilotFdirOn);
            CommandList.Add(AutopilotFdirToggle.Id, AutopilotFdirToggle);
            CommandList.Add(AutopilotServosOn.Id, AutopilotServosOn);
            CommandList.Add(AutopilotServosToggle.Id, AutopilotServosToggle);
            CommandList.Add(AutopilotFdirServosDownOne.Id, AutopilotFdirServosDownOne);
            CommandList.Add(AutopilotFdirServosUpOne.Id, AutopilotFdirServosUpOne);
            CommandList.Add(AutopilotServosFdirOff.Id, AutopilotServosFdirOff);
            CommandList.Add(AutopilotServosFdirYawdOff.Id, AutopilotServosFdirYawdOff);
            CommandList.Add(AutopilotServosFdirYawdTrimOff.Id, AutopilotServosFdirYawdTrimOff);
            CommandList.Add(AutopilotControlWheelSteer.Id, AutopilotControlWheelSteer);
            CommandList.Add(AutopilotFdir2On.Id, AutopilotFdir2On);
            CommandList.Add(AutopilotFdir2Toggle.Id, AutopilotFdir2Toggle);
            CommandList.Add(AutopilotServos2On.Id, AutopilotServos2On);
            CommandList.Add(AutopilotServos2Toggle.Id, AutopilotServos2Toggle);
            CommandList.Add(AutopilotFdir2ServosDownOne.Id, AutopilotFdir2ServosDownOne);
            CommandList.Add(AutopilotFdir2ServosUpOne.Id, AutopilotFdir2ServosUpOne);
            CommandList.Add(AutopilotServosFdir2Off.Id, AutopilotServosFdir2Off);
            CommandList.Add(AutopilotCWSA.Id, AutopilotCWSA);
            CommandList.Add(AutopilotCWSB.Id, AutopilotCWSB);
            CommandList.Add(AutopilotServos3On.Id, AutopilotServos3On);
            CommandList.Add(AutopilotServos3Toggle.Id, AutopilotServos3Toggle);
            CommandList.Add(AutopilotServosFdir3Off.Id, AutopilotServosFdir3Off);
            CommandList.Add(AutopilotServosOffAny.Id, AutopilotServosOffAny);
            CommandList.Add(AutopilotServosYawdOffAny.Id, AutopilotServosYawdOffAny);
            CommandList.Add(AutopilotServosYawdTrimOffAny.Id, AutopilotServosYawdTrimOffAny);
            CommandList.Add(AutopilotAutothrottleOn.Id, AutopilotAutothrottleOn);
            CommandList.Add(AutopilotAutothrottleOff.Id, AutopilotAutothrottleOff);
            CommandList.Add(AutopilotAutothrottleToggle.Id, AutopilotAutothrottleToggle);
            CommandList.Add(AutopilotAutothrottleN1epr.Id, AutopilotAutothrottleN1epr);
            CommandList.Add(AutopilotAutothrottleN1eprToggle.Id, AutopilotAutothrottleN1eprToggle);
            CommandList.Add(AutopilotHeading.Id, AutopilotHeading);
            CommandList.Add(AutopilotTrack.Id, AutopilotTrack);
            CommandList.Add(AutopilotHeadingHold.Id, AutopilotHeadingHold);
            CommandList.Add(AutopilotWingLeveler.Id, AutopilotWingLeveler);
            CommandList.Add(AutopilotRateHold.Id, AutopilotRateHold);
            CommandList.Add(AutopilotHdgNav.Id, AutopilotHdgNav);
            CommandList.Add(AutopilotNAV.Id, AutopilotNAV);
            CommandList.Add(AutopilotVerticalSpeed.Id, AutopilotVerticalSpeed);
            CommandList.Add(AutopilotFpa.Id, AutopilotFpa);
            CommandList.Add(AutopilotAltVs.Id, AutopilotAltVs);
            CommandList.Add(AutopilotVerticalSpeedPreSel.Id, AutopilotVerticalSpeedPreSel);
            CommandList.Add(AutopilotPitchSync.Id, AutopilotPitchSync);
            CommandList.Add(AutopilotLevelChange.Id, AutopilotLevelChange);
            CommandList.Add(AutopilotAltitudeHold.Id, AutopilotAltitudeHold);
            CommandList.Add(AutopilotTerrainFollowing.Id, AutopilotTerrainFollowing);
            CommandList.Add(AutopilotTakeOffGoAround.Id, AutopilotTakeOffGoAround);
            CommandList.Add(AutopilotReentry.Id, AutopilotReentry);
            CommandList.Add(AutopilotGlideSlope.Id, AutopilotGlideSlope);
            CommandList.Add(AutopilotVnav.Id, AutopilotVnav);
            CommandList.Add(AutopilotGpss.Id, AutopilotGpss);
            CommandList.Add(AutopilotClimb.Id, AutopilotClimb);
            CommandList.Add(AutopilotDescend.Id, AutopilotDescend);
            CommandList.Add(AutopilotTrkfpa.Id, AutopilotTrkfpa);
            CommandList.Add(AutopilotAirspeedSync.Id, AutopilotAirspeedSync);
            CommandList.Add(AutopilotHeadingSync.Id, AutopilotHeadingSync);
            CommandList.Add(AutopilotVerticalSpeedSync.Id, AutopilotVerticalSpeedSync);
            CommandList.Add(AutopilotAltitudeSync.Id, AutopilotAltitudeSync);
            CommandList.Add(AutopilotHeadingDown.Id, AutopilotHeadingDown);
            CommandList.Add(AutopilotHeadingUp.Id, AutopilotHeadingUp);
            CommandList.Add(AutopilotHeadingCopilotDown.Id, AutopilotHeadingCopilotDown);
            CommandList.Add(AutopilotHeadingCopilotUp.Id, AutopilotHeadingCopilotUp);
            CommandList.Add(AutopilotAirspeedDown.Id, AutopilotAirspeedDown);
            CommandList.Add(AutopilotAirspeedUp.Id, AutopilotAirspeedUp);
            CommandList.Add(AutopilotVerticalSpeedDown.Id, AutopilotVerticalSpeedDown);
            CommandList.Add(AutopilotVerticalSpeedUp.Id, AutopilotVerticalSpeedUp);
            CommandList.Add(AutopilotAltitudeDown.Id, AutopilotAltitudeDown);
            CommandList.Add(AutopilotAltitudeUp.Id, AutopilotAltitudeUp);
            CommandList.Add(AutopilotNoseDown.Id, AutopilotNoseDown);
            CommandList.Add(AutopilotNoseUp.Id, AutopilotNoseUp);
            CommandList.Add(AutopilotNoseDownPitchMode.Id, AutopilotNoseDownPitchMode);
            CommandList.Add(AutopilotNoseUpPitchMode.Id, AutopilotNoseUpPitchMode);
            CommandList.Add(AutopilotOverrideLeft.Id, AutopilotOverrideLeft);
            CommandList.Add(AutopilotOverrideRight.Id, AutopilotOverrideRight);
            CommandList.Add(AutopilotOverrideCenter.Id, AutopilotOverrideCenter);
            CommandList.Add(AutopilotOverrideUp.Id, AutopilotOverrideUp);
            CommandList.Add(AutopilotOverrideDown.Id, AutopilotOverrideDown);
            CommandList.Add(AutopilotAltitudeArm.Id, AutopilotAltitudeArm);
            CommandList.Add(AutopilotApproach.Id, AutopilotApproach);
            CommandList.Add(AutopilotBackCourse.Id, AutopilotBackCourse);
            CommandList.Add(AutopilotKnotsMachToggle.Id, AutopilotKnotsMachToggle);
            CommandList.Add(AutopilotFMS.Id, AutopilotFMS);
            CommandList.Add(AutopilotBankLimitDown.Id, AutopilotBankLimitDown);
            CommandList.Add(AutopilotBankLimitUp.Id, AutopilotBankLimitUp);
            CommandList.Add(AutopilotBankLimitToggle.Id, AutopilotBankLimitToggle);
            CommandList.Add(AutopilotSoftRideToggle.Id, AutopilotSoftRideToggle);
            CommandList.Add(ElectricalDcVoltDn.Id, ElectricalDcVoltDn);
            CommandList.Add(ElectricalDcVoltUp.Id, ElectricalDcVoltUp);
            CommandList.Add(ElectricalDcVoltExt.Id, ElectricalDcVoltExt);
            CommandList.Add(ElectricalDcVoltCtr.Id, ElectricalDcVoltCtr);
            CommandList.Add(ElectricalDcVoltLft.Id, ElectricalDcVoltLft);
            CommandList.Add(ElectricalDcVoltRgt.Id, ElectricalDcVoltRgt);
            CommandList.Add(ElectricalDcVoltTpl.Id, ElectricalDcVoltTpl);
            CommandList.Add(ElectricalDcVoltBat.Id, ElectricalDcVoltBat);
            CommandList.Add(HUDPowerToggle.Id, HUDPowerToggle);
            CommandList.Add(HUDBrightnessToggle.Id, HUDBrightnessToggle);
            CommandList.Add(SystemsTotalEnergyAudioToggle.Id, SystemsTotalEnergyAudioToggle);
            CommandList.Add(InstrumentsThermoUnitsToggle.Id, InstrumentsThermoUnitsToggle);
            CommandList.Add(InstrumentsBarometer2992.Id, InstrumentsBarometer2992);
            CommandList.Add(InstrumentsDGSyncDown.Id, InstrumentsDGSyncDown);
            CommandList.Add(InstrumentsDGSyncUp.Id, InstrumentsDGSyncUp);
            CommandList.Add(InstrumentsDGSyncMag.Id, InstrumentsDGSyncMag);
            CommandList.Add(InstrumentsCopilotDGSyncDown.Id, InstrumentsCopilotDGSyncDown);
            CommandList.Add(InstrumentsCopilotDGSyncUp.Id, InstrumentsCopilotDGSyncUp);
            CommandList.Add(InstrumentsCopilotDGSyncMag.Id, InstrumentsCopilotDGSyncMag);
            CommandList.Add(InstrumentsFreeGyro.Id, InstrumentsFreeGyro);
            CommandList.Add(InstrumentsSlaveGyro.Id, InstrumentsSlaveGyro);
            CommandList.Add(InstrumentsCopilotFreeGyro.Id, InstrumentsCopilotFreeGyro);
            CommandList.Add(InstrumentsCopilotSlaveGyro.Id, InstrumentsCopilotSlaveGyro);
            CommandList.Add(InstrumentsFreeGyroDown.Id, InstrumentsFreeGyroDown);
            CommandList.Add(InstrumentsFreeGyroUp.Id, InstrumentsFreeGyroUp);
            CommandList.Add(InstrumentsCopilotFreeGyroDown.Id, InstrumentsCopilotFreeGyroDown);
            CommandList.Add(InstrumentsCopilotFreeGyroUp.Id, InstrumentsCopilotFreeGyroUp);
            CommandList.Add(InstrumentsAhRefDown.Id, InstrumentsAhRefDown);
            CommandList.Add(InstrumentsAhRefUp.Id, InstrumentsAhRefUp);
            CommandList.Add(InstrumentsAhRefCopilotDown.Id, InstrumentsAhRefCopilotDown);
            CommandList.Add(InstrumentsAhRefCopilotUp.Id, InstrumentsAhRefCopilotUp);
            CommandList.Add(InstrumentsAhFastErect.Id, InstrumentsAhFastErect);
            CommandList.Add(InstrumentsAhCage.Id, InstrumentsAhCage);
            CommandList.Add(InstrumentsAhFastErectCopilot.Id, InstrumentsAhFastErectCopilot);
            CommandList.Add(InstrumentsAhCageCopilot.Id, InstrumentsAhCageCopilot);
            CommandList.Add(InstrumentsBarometerDown.Id, InstrumentsBarometerDown);
            CommandList.Add(InstrumentsBarometerUp.Id, InstrumentsBarometerUp);
            CommandList.Add(InstrumentsBarometerCopilotDown.Id, InstrumentsBarometerCopilotDown);
            CommandList.Add(InstrumentsBarometerCopilotUp.Id, InstrumentsBarometerCopilotUp);
            CommandList.Add(InstrumentsBarometerStbyDown.Id, InstrumentsBarometerStbyDown);
            CommandList.Add(InstrumentsBarometerStbyUp.Id, InstrumentsBarometerStbyUp);
            CommandList.Add(InstrumentsBarometerApDown.Id, InstrumentsBarometerApDown);
            CommandList.Add(InstrumentsBarometerApUp.Id, InstrumentsBarometerApUp);
            CommandList.Add(InstrumentsDhRefDown.Id, InstrumentsDhRefDown);
            CommandList.Add(InstrumentsDhRefUp.Id, InstrumentsDhRefUp);
            CommandList.Add(InstrumentsDhRefCopilotDown.Id, InstrumentsDhRefCopilotDown);
            CommandList.Add(InstrumentsDhRefCopilotUp.Id, InstrumentsDhRefCopilotUp);
            CommandList.Add(InstrumentsMdaRefDown.Id, InstrumentsMdaRefDown);
            CommandList.Add(InstrumentsMdaRefUp.Id, InstrumentsMdaRefUp);
            CommandList.Add(InstrumentsMdaRefCopilotDown.Id, InstrumentsMdaRefCopilotDown);
            CommandList.Add(InstrumentsMdaRefCopilotUp.Id, InstrumentsMdaRefCopilotUp);
            CommandList.Add(InstrumentsBaroAltAlertCancel.Id, InstrumentsBaroAltAlertCancel);
            CommandList.Add(InstrumentsMdaAlertCancel.Id, InstrumentsMdaAlertCancel);
            CommandList.Add(InstrumentsPanelBrightDown.Id, InstrumentsPanelBrightDown);
            CommandList.Add(InstrumentsPanelBrightUp.Id, InstrumentsPanelBrightUp);
            CommandList.Add(InstrumentsInstrumentBrightDown.Id, InstrumentsInstrumentBrightDown);
            CommandList.Add(InstrumentsInstrumentBrightUp.Id, InstrumentsInstrumentBrightUp);
            CommandList.Add(AnnunciatorTestAllAnnunciators.Id, AnnunciatorTestAllAnnunciators);
            CommandList.Add(AnnunciatorTestStall.Id, AnnunciatorTestStall);
            CommandList.Add(AnnunciatorTestFire1Annun.Id, AnnunciatorTestFire1Annun);
            CommandList.Add(AnnunciatorTestFire2Annun.Id, AnnunciatorTestFire2Annun);
            CommandList.Add(AnnunciatorTestFire3Annun.Id, AnnunciatorTestFire3Annun);
            CommandList.Add(AnnunciatorTestFire4Annun.Id, AnnunciatorTestFire4Annun);
            CommandList.Add(AnnunciatorTestFire5Annun.Id, AnnunciatorTestFire5Annun);
            CommandList.Add(AnnunciatorTestFire6Annun.Id, AnnunciatorTestFire6Annun);
            CommandList.Add(AnnunciatorTestFire7Annun.Id, AnnunciatorTestFire7Annun);
            CommandList.Add(AnnunciatorTestFire8Annun.Id, AnnunciatorTestFire8Annun);
            CommandList.Add(AnnunciatorClearMasterCaution.Id, AnnunciatorClearMasterCaution);
            CommandList.Add(AnnunciatorClearMasterWarning.Id, AnnunciatorClearMasterWarning);
            CommandList.Add(AnnunciatorClearMasterAccept.Id, AnnunciatorClearMasterAccept);
            CommandList.Add(FMSLs1L.Id, FMSLs1L);
            CommandList.Add(FMSLs2L.Id, FMSLs2L);
            CommandList.Add(FMSLs3L.Id, FMSLs3L);
            CommandList.Add(FMSLs4L.Id, FMSLs4L);
            CommandList.Add(FMSLs5L.Id, FMSLs5L);
            CommandList.Add(FMSLs6L.Id, FMSLs6L);
            CommandList.Add(FMSLs1R.Id, FMSLs1R);
            CommandList.Add(FMSLs2R.Id, FMSLs2R);
            CommandList.Add(FMSLs3R.Id, FMSLs3R);
            CommandList.Add(FMSLs4R.Id, FMSLs4R);
            CommandList.Add(FMSLs5R.Id, FMSLs5R);
            CommandList.Add(FMSLs6R.Id, FMSLs6R);
            CommandList.Add(FMSIndex.Id, FMSIndex);
            CommandList.Add(FMSFpln.Id, FMSFpln);
            CommandList.Add(FMSClb.Id, FMSClb);
            CommandList.Add(FMSCrz.Id, FMSCrz);
            CommandList.Add(FMSDes.Id, FMSDes);
            CommandList.Add(FMSDirIntc.Id, FMSDirIntc);
            CommandList.Add(FMSLegs.Id, FMSLegs);
            CommandList.Add(FMSDepArr.Id, FMSDepArr);
            CommandList.Add(FMSHold.Id, FMSHold);
            CommandList.Add(FMSProg.Id, FMSProg);
            CommandList.Add(FMSExec.Id, FMSExec);
            CommandList.Add(FMSFix.Id, FMSFix);
            CommandList.Add(FMSNavrad.Id, FMSNavrad);
            CommandList.Add(FMSInit.Id, FMSInit);
            CommandList.Add(FMSPrev.Id, FMSPrev);
            CommandList.Add(FMSNext.Id, FMSNext);
            CommandList.Add(FMSClear.Id, FMSClear);
            CommandList.Add(FMSDirect.Id, FMSDirect);
            CommandList.Add(FMSSign.Id, FMSSign);
            CommandList.Add(FMSTypeApt.Id, FMSTypeApt);
            CommandList.Add(FMSTypeVor.Id, FMSTypeVor);
            CommandList.Add(FMSTypeNdb.Id, FMSTypeNdb);
            CommandList.Add(FMSTypeFix.Id, FMSTypeFix);
            CommandList.Add(FMSTypeLatlon.Id, FMSTypeLatlon);
            CommandList.Add(FMSKey0.Id, FMSKey0);
            CommandList.Add(FMSKey1.Id, FMSKey1);
            CommandList.Add(FMSKey2.Id, FMSKey2);
            CommandList.Add(FMSKey3.Id, FMSKey3);
            CommandList.Add(FMSKey4.Id, FMSKey4);
            CommandList.Add(FMSKey5.Id, FMSKey5);
            CommandList.Add(FMSKey6.Id, FMSKey6);
            CommandList.Add(FMSKey7.Id, FMSKey7);
            CommandList.Add(FMSKey8.Id, FMSKey8);
            CommandList.Add(FMSKey9.Id, FMSKey9);
            CommandList.Add(FMSKeyA.Id, FMSKeyA);
            CommandList.Add(FMSKeyB.Id, FMSKeyB);
            CommandList.Add(FMSKeyC.Id, FMSKeyC);
            CommandList.Add(FMSKeyD.Id, FMSKeyD);
            CommandList.Add(FMSKeyE.Id, FMSKeyE);
            CommandList.Add(FMSKeyF.Id, FMSKeyF);
            CommandList.Add(FMSKeyG.Id, FMSKeyG);
            CommandList.Add(FMSKeyH.Id, FMSKeyH);
            CommandList.Add(FMSKeyI.Id, FMSKeyI);
            CommandList.Add(FMSKeyJ.Id, FMSKeyJ);
            CommandList.Add(FMSKeyK.Id, FMSKeyK);
            CommandList.Add(FMSKeyL.Id, FMSKeyL);
            CommandList.Add(FMSKeyM.Id, FMSKeyM);
            CommandList.Add(FMSKeyN.Id, FMSKeyN);
            CommandList.Add(FMSKeyO.Id, FMSKeyO);
            CommandList.Add(FMSKeyP.Id, FMSKeyP);
            CommandList.Add(FMSKeyQ.Id, FMSKeyQ);
            CommandList.Add(FMSKeyR.Id, FMSKeyR);
            CommandList.Add(FMSKeyS.Id, FMSKeyS);
            CommandList.Add(FMSKeyT.Id, FMSKeyT);
            CommandList.Add(FMSKeyU.Id, FMSKeyU);
            CommandList.Add(FMSKeyV.Id, FMSKeyV);
            CommandList.Add(FMSKeyW.Id, FMSKeyW);
            CommandList.Add(FMSKeyX.Id, FMSKeyX);
            CommandList.Add(FMSKeyY.Id, FMSKeyY);
            CommandList.Add(FMSKeyZ.Id, FMSKeyZ);
            CommandList.Add(FMSKeyPeriod.Id, FMSKeyPeriod);
            CommandList.Add(FMSKeyMinus.Id, FMSKeyMinus);
            CommandList.Add(FMSKeySlash.Id, FMSKeySlash);
            CommandList.Add(FMSKeyBack.Id, FMSKeyBack);
            CommandList.Add(FMSKeySpace.Id, FMSKeySpace);
            CommandList.Add(FMSKeyLoad.Id, FMSKeyLoad);
            CommandList.Add(FMSKeySave.Id, FMSKeySave);
            CommandList.Add(FMSKeyDelete.Id, FMSKeyDelete);
            CommandList.Add(FMSKeyClear.Id, FMSKeyClear);
            CommandList.Add(FMSCDUPopup.Id, FMSCDUPopup);
            CommandList.Add(FMSCDUPopout.Id, FMSCDUPopout);
            CommandList.Add(FMSFixNext.Id, FMSFixNext);
            CommandList.Add(FMSFixPrev.Id, FMSFixPrev);
            CommandList.Add(FMS2Ls1L.Id, FMS2Ls1L);
            CommandList.Add(FMS2Ls2L.Id, FMS2Ls2L);
            CommandList.Add(FMS2Ls3L.Id, FMS2Ls3L);
            CommandList.Add(FMS2Ls4L.Id, FMS2Ls4L);
            CommandList.Add(FMS2Ls5L.Id, FMS2Ls5L);
            CommandList.Add(FMS2Ls6L.Id, FMS2Ls6L);
            CommandList.Add(FMS2Ls1R.Id, FMS2Ls1R);
            CommandList.Add(FMS2Ls2R.Id, FMS2Ls2R);
            CommandList.Add(FMS2Ls3R.Id, FMS2Ls3R);
            CommandList.Add(FMS2Ls4R.Id, FMS2Ls4R);
            CommandList.Add(FMS2Ls5R.Id, FMS2Ls5R);
            CommandList.Add(FMS2Ls6R.Id, FMS2Ls6R);
            CommandList.Add(FMS2Index.Id, FMS2Index);
            CommandList.Add(FMS2Fpln.Id, FMS2Fpln);
            CommandList.Add(FMS2Clb.Id, FMS2Clb);
            CommandList.Add(FMS2Crz.Id, FMS2Crz);
            CommandList.Add(FMS2Des.Id, FMS2Des);
            CommandList.Add(FMS2DirIntc.Id, FMS2DirIntc);
            CommandList.Add(FMS2Legs.Id, FMS2Legs);
            CommandList.Add(FMS2DepArr.Id, FMS2DepArr);
            CommandList.Add(FMS2Hold.Id, FMS2Hold);
            CommandList.Add(FMS2Prog.Id, FMS2Prog);
            CommandList.Add(FMS2Exec.Id, FMS2Exec);
            CommandList.Add(FMS2Fix.Id, FMS2Fix);
            CommandList.Add(FMS2Navrad.Id, FMS2Navrad);
            CommandList.Add(FMS2Prev.Id, FMS2Prev);
            CommandList.Add(FMS2Next.Id, FMS2Next);
            CommandList.Add(FMS2Key0.Id, FMS2Key0);
            CommandList.Add(FMS2Key1.Id, FMS2Key1);
            CommandList.Add(FMS2Key2.Id, FMS2Key2);
            CommandList.Add(FMS2Key3.Id, FMS2Key3);
            CommandList.Add(FMS2Key4.Id, FMS2Key4);
            CommandList.Add(FMS2Key5.Id, FMS2Key5);
            CommandList.Add(FMS2Key6.Id, FMS2Key6);
            CommandList.Add(FMS2Key7.Id, FMS2Key7);
            CommandList.Add(FMS2Key8.Id, FMS2Key8);
            CommandList.Add(FMS2Key9.Id, FMS2Key9);
            CommandList.Add(FMS2KeyA.Id, FMS2KeyA);
            CommandList.Add(FMS2KeyB.Id, FMS2KeyB);
            CommandList.Add(FMS2KeyC.Id, FMS2KeyC);
            CommandList.Add(FMS2KeyD.Id, FMS2KeyD);
            CommandList.Add(FMS2KeyE.Id, FMS2KeyE);
            CommandList.Add(FMS2KeyF.Id, FMS2KeyF);
            CommandList.Add(FMS2KeyG.Id, FMS2KeyG);
            CommandList.Add(FMS2KeyH.Id, FMS2KeyH);
            CommandList.Add(FMS2KeyI.Id, FMS2KeyI);
            CommandList.Add(FMS2KeyJ.Id, FMS2KeyJ);
            CommandList.Add(FMS2KeyK.Id, FMS2KeyK);
            CommandList.Add(FMS2KeyL.Id, FMS2KeyL);
            CommandList.Add(FMS2KeyM.Id, FMS2KeyM);
            CommandList.Add(FMS2KeyN.Id, FMS2KeyN);
            CommandList.Add(FMS2KeyO.Id, FMS2KeyO);
            CommandList.Add(FMS2KeyP.Id, FMS2KeyP);
            CommandList.Add(FMS2KeyQ.Id, FMS2KeyQ);
            CommandList.Add(FMS2KeyR.Id, FMS2KeyR);
            CommandList.Add(FMS2KeyS.Id, FMS2KeyS);
            CommandList.Add(FMS2KeyT.Id, FMS2KeyT);
            CommandList.Add(FMS2KeyU.Id, FMS2KeyU);
            CommandList.Add(FMS2KeyV.Id, FMS2KeyV);
            CommandList.Add(FMS2KeyW.Id, FMS2KeyW);
            CommandList.Add(FMS2KeyX.Id, FMS2KeyX);
            CommandList.Add(FMS2KeyY.Id, FMS2KeyY);
            CommandList.Add(FMS2KeyZ.Id, FMS2KeyZ);
            CommandList.Add(FMS2KeyPeriod.Id, FMS2KeyPeriod);
            CommandList.Add(FMS2KeyMinus.Id, FMS2KeyMinus);
            CommandList.Add(FMS2KeySlash.Id, FMS2KeySlash);
            CommandList.Add(FMS2KeyBack.Id, FMS2KeyBack);
            CommandList.Add(FMS2KeySpace.Id, FMS2KeySpace);
            CommandList.Add(FMS2KeyDelete.Id, FMS2KeyDelete);
            CommandList.Add(FMS2KeyClear.Id, FMS2KeyClear);
            CommandList.Add(FMS2CDUPopout.Id, FMS2CDUPopout);
            CommandList.Add(FMS2CDUPopup.Id, FMS2CDUPopup);
            CommandList.Add(AnnunciatorGearWarningMute.Id, AnnunciatorGearWarningMute);
            CommandList.Add(AnnunciatorMarkerBeaconMute.Id, AnnunciatorMarkerBeaconMute);
            CommandList.Add(AnnunciatorMarkerBeaconMuteOrOff.Id, AnnunciatorMarkerBeaconMuteOrOff);
            CommandList.Add(AnnunciatorMarkerBeaconSensHi.Id, AnnunciatorMarkerBeaconSensHi);
            CommandList.Add(AnnunciatorMarkerBeaconSensLo.Id, AnnunciatorMarkerBeaconSensLo);
            CommandList.Add(AnnunciatorMarkerBeaconSensToggle.Id, AnnunciatorMarkerBeaconSensToggle);
            CommandList.Add(SystemsPreRotateToggle.Id, SystemsPreRotateToggle);
            CommandList.Add(FlightControlsPumpFlaps.Id, FlightControlsPumpFlaps);
            CommandList.Add(FlightControlsPumpGear.Id, FlightControlsPumpGear);
            CommandList.Add(GPSModeAirport.Id, GPSModeAirport);
            CommandList.Add(GPSModeVOR.Id, GPSModeVOR);
            CommandList.Add(GPSModeNDB.Id, GPSModeNDB);
            CommandList.Add(GPSModeWaypoint.Id, GPSModeWaypoint);
            CommandList.Add(GPSFineSelectDown.Id, GPSFineSelectDown);
            CommandList.Add(GPSFineSelectUp.Id, GPSFineSelectUp);
            CommandList.Add(GPSCoarseSelectDown.Id, GPSCoarseSelectDown);
            CommandList.Add(GPSCoarseSelectUp.Id, GPSCoarseSelectUp);
            CommandList.Add(GPSG430n1CoarseDown.Id, GPSG430n1CoarseDown);
            CommandList.Add(GPSG430n1CoarseUp.Id, GPSG430n1CoarseUp);
            CommandList.Add(GPSG430n1FineDown.Id, GPSG430n1FineDown);
            CommandList.Add(GPSG430n1FineUp.Id, GPSG430n1FineUp);
            CommandList.Add(GPSG430n1ChapterUp.Id, GPSG430n1ChapterUp);
            CommandList.Add(GPSG430n1ChapterDn.Id, GPSG430n1ChapterDn);
            CommandList.Add(GPSG430n1PageUp.Id, GPSG430n1PageUp);
            CommandList.Add(GPSG430n1PageDn.Id, GPSG430n1PageDn);
            CommandList.Add(GPSG430n1ZoomIn.Id, GPSG430n1ZoomIn);
            CommandList.Add(GPSG430n1ZoomOut.Id, GPSG430n1ZoomOut);
            CommandList.Add(GPSG430n1NavComTog.Id, GPSG430n1NavComTog);
            CommandList.Add(GPSG430n1Cdi.Id, GPSG430n1Cdi);
            CommandList.Add(GPSG430n1Obs.Id, GPSG430n1Obs);
            CommandList.Add(GPSG430n1Msg.Id, GPSG430n1Msg);
            CommandList.Add(GPSG430n1Fpl.Id, GPSG430n1Fpl);
            CommandList.Add(GPSG430n1Proc.Id, GPSG430n1Proc);
            CommandList.Add(GPSG430n1Vnav.Id, GPSG430n1Vnav);
            CommandList.Add(GPSG430n1Direct.Id, GPSG430n1Direct);
            CommandList.Add(GPSG430n1Menu.Id, GPSG430n1Menu);
            CommandList.Add(GPSG430n1Clr.Id, GPSG430n1Clr);
            CommandList.Add(GPSG430n1Ent.Id, GPSG430n1Ent);
            CommandList.Add(GPSG430n1ComFf.Id, GPSG430n1ComFf);
            CommandList.Add(GPSG430n1NavFf.Id, GPSG430n1NavFf);
            CommandList.Add(GPSG430n1Cursor.Id, GPSG430n1Cursor);
            CommandList.Add(GPSG430n1Popout.Id, GPSG430n1Popout);
            CommandList.Add(GPSG430n1Popup.Id, GPSG430n1Popup);
            CommandList.Add(GPSG430n1Cvol.Id, GPSG430n1Cvol);
            CommandList.Add(GPSG430n1Vvol.Id, GPSG430n1Vvol);
            CommandList.Add(GPSG430n1CvolUp.Id, GPSG430n1CvolUp);
            CommandList.Add(GPSG430n1CvolDn.Id, GPSG430n1CvolDn);
            CommandList.Add(GPSG430n1VvolUp.Id, GPSG430n1VvolUp);
            CommandList.Add(GPSG430n1VvolDn.Id, GPSG430n1VvolDn);
            CommandList.Add(GPSG430n2CoarseDown.Id, GPSG430n2CoarseDown);
            CommandList.Add(GPSG430n2CoarseUp.Id, GPSG430n2CoarseUp);
            CommandList.Add(GPSG430n2FineDown.Id, GPSG430n2FineDown);
            CommandList.Add(GPSG430n2FineUp.Id, GPSG430n2FineUp);
            CommandList.Add(GPSG430n2ChapterUp.Id, GPSG430n2ChapterUp);
            CommandList.Add(GPSG430n2ChapterDn.Id, GPSG430n2ChapterDn);
            CommandList.Add(GPSG430n2PageUp.Id, GPSG430n2PageUp);
            CommandList.Add(GPSG430n2PageDn.Id, GPSG430n2PageDn);
            CommandList.Add(GPSG430n2ZoomIn.Id, GPSG430n2ZoomIn);
            CommandList.Add(GPSG430n2ZoomOut.Id, GPSG430n2ZoomOut);
            CommandList.Add(GPSG430n2NavComTog.Id, GPSG430n2NavComTog);
            CommandList.Add(GPSG430n2Cdi.Id, GPSG430n2Cdi);
            CommandList.Add(GPSG430n2Obs.Id, GPSG430n2Obs);
            CommandList.Add(GPSG430n2Msg.Id, GPSG430n2Msg);
            CommandList.Add(GPSG430n2Fpl.Id, GPSG430n2Fpl);
            CommandList.Add(GPSG430n2Proc.Id, GPSG430n2Proc);
            CommandList.Add(GPSG430n2Vnav.Id, GPSG430n2Vnav);
            CommandList.Add(GPSG430n2Direct.Id, GPSG430n2Direct);
            CommandList.Add(GPSG430n2Menu.Id, GPSG430n2Menu);
            CommandList.Add(GPSG430n2Clr.Id, GPSG430n2Clr);
            CommandList.Add(GPSG430n2Ent.Id, GPSG430n2Ent);
            CommandList.Add(GPSG430n2ComFf.Id, GPSG430n2ComFf);
            CommandList.Add(GPSG430n2NavFf.Id, GPSG430n2NavFf);
            CommandList.Add(GPSG430n2Cursor.Id, GPSG430n2Cursor);
            CommandList.Add(GPSG430n2Popout.Id, GPSG430n2Popout);
            CommandList.Add(GPSG430n2Popup.Id, GPSG430n2Popup);
            CommandList.Add(GPSG430n2Cvol.Id, GPSG430n2Cvol);
            CommandList.Add(GPSG430n2Vvol.Id, GPSG430n2Vvol);
            CommandList.Add(GPSG430n2CvolUp.Id, GPSG430n2CvolUp);
            CommandList.Add(GPSG430n2CvolDn.Id, GPSG430n2CvolDn);
            CommandList.Add(GPSG430n2VvolUp.Id, GPSG430n2VvolUp);
            CommandList.Add(GPSG430n2VvolDn.Id, GPSG430n2VvolDn);
            CommandList.Add(GPSG1000n1Nvol.Id, GPSG1000n1Nvol);
            CommandList.Add(GPSG1000n1NvolUp.Id, GPSG1000n1NvolUp);
            CommandList.Add(GPSG1000n1NvolDn.Id, GPSG1000n1NvolDn);
            CommandList.Add(GPSG1000n1NavFf.Id, GPSG1000n1NavFf);
            CommandList.Add(GPSG1000n1NavOuterUp.Id, GPSG1000n1NavOuterUp);
            CommandList.Add(GPSG1000n1NavOuterDown.Id, GPSG1000n1NavOuterDown);
            CommandList.Add(GPSG1000n1NavInnerUp.Id, GPSG1000n1NavInnerUp);
            CommandList.Add(GPSG1000n1NavInnerDown.Id, GPSG1000n1NavInnerDown);
            CommandList.Add(GPSG1000n1Nav12.Id, GPSG1000n1Nav12);
            CommandList.Add(GPSG1000n1HdgUp.Id, GPSG1000n1HdgUp);
            CommandList.Add(GPSG1000n1HdgDown.Id, GPSG1000n1HdgDown);
            CommandList.Add(GPSG1000n1HdgSync.Id, GPSG1000n1HdgSync);
            CommandList.Add(GPSG1000n1Ap.Id, GPSG1000n1Ap);
            CommandList.Add(GPSG1000n1Fd.Id, GPSG1000n1Fd);
            CommandList.Add(GPSG1000n1Yd.Id, GPSG1000n1Yd);
            CommandList.Add(GPSG1000n1Hdg.Id, GPSG1000n1Hdg);
            CommandList.Add(GPSG1000n1Alt.Id, GPSG1000n1Alt);
            CommandList.Add(GPSG1000n1Nav.Id, GPSG1000n1Nav);
            CommandList.Add(GPSG1000n1Vnv.Id, GPSG1000n1Vnv);
            CommandList.Add(GPSG1000n1Apr.Id, GPSG1000n1Apr);
            CommandList.Add(GPSG1000n1Bc.Id, GPSG1000n1Bc);
            CommandList.Add(GPSG1000n1Vs.Id, GPSG1000n1Vs);
            CommandList.Add(GPSG1000n1Flc.Id, GPSG1000n1Flc);
            CommandList.Add(GPSG1000n1NoseUp.Id, GPSG1000n1NoseUp);
            CommandList.Add(GPSG1000n1NoseDown.Id, GPSG1000n1NoseDown);
            CommandList.Add(GPSG1000n1AltOuterUp.Id, GPSG1000n1AltOuterUp);
            CommandList.Add(GPSG1000n1AltOuterDown.Id, GPSG1000n1AltOuterDown);
            CommandList.Add(GPSG1000n1AltInnerUp.Id, GPSG1000n1AltInnerUp);
            CommandList.Add(GPSG1000n1AltInnerDown.Id, GPSG1000n1AltInnerDown);
            CommandList.Add(GPSG1000n1Softkey1.Id, GPSG1000n1Softkey1);
            CommandList.Add(GPSG1000n1Softkey2.Id, GPSG1000n1Softkey2);
            CommandList.Add(GPSG1000n1Softkey3.Id, GPSG1000n1Softkey3);
            CommandList.Add(GPSG1000n1Softkey4.Id, GPSG1000n1Softkey4);
            CommandList.Add(GPSG1000n1Softkey5.Id, GPSG1000n1Softkey5);
            CommandList.Add(GPSG1000n1Softkey6.Id, GPSG1000n1Softkey6);
            CommandList.Add(GPSG1000n1Softkey7.Id, GPSG1000n1Softkey7);
            CommandList.Add(GPSG1000n1Softkey8.Id, GPSG1000n1Softkey8);
            CommandList.Add(GPSG1000n1Softkey9.Id, GPSG1000n1Softkey9);
            CommandList.Add(GPSG1000n1Softkey10.Id, GPSG1000n1Softkey10);
            CommandList.Add(GPSG1000n1Softkey11.Id, GPSG1000n1Softkey11);
            CommandList.Add(GPSG1000n1Softkey12.Id, GPSG1000n1Softkey12);
            CommandList.Add(GPSG1000n1Cvol.Id, GPSG1000n1Cvol);
            CommandList.Add(GPSG1000n1CvolUp.Id, GPSG1000n1CvolUp);
            CommandList.Add(GPSG1000n1CvolDn.Id, GPSG1000n1CvolDn);
            CommandList.Add(GPSG1000n1ComFf.Id, GPSG1000n1ComFf);
            CommandList.Add(GPSG1000n1ComOuterUp.Id, GPSG1000n1ComOuterUp);
            CommandList.Add(GPSG1000n1ComOuterDown.Id, GPSG1000n1ComOuterDown);
            CommandList.Add(GPSG1000n1ComInnerUp.Id, GPSG1000n1ComInnerUp);
            CommandList.Add(GPSG1000n1ComInnerDown.Id, GPSG1000n1ComInnerDown);
            CommandList.Add(GPSG1000n1Com12.Id, GPSG1000n1Com12);
            CommandList.Add(GPSG1000n1CrsUp.Id, GPSG1000n1CrsUp);
            CommandList.Add(GPSG1000n1CrsDown.Id, GPSG1000n1CrsDown);
            CommandList.Add(GPSG1000n1CrsSync.Id, GPSG1000n1CrsSync);
            CommandList.Add(GPSG1000n1BaroUp.Id, GPSG1000n1BaroUp);
            CommandList.Add(GPSG1000n1BaroDown.Id, GPSG1000n1BaroDown);
            CommandList.Add(GPSG1000n1RangeUp.Id, GPSG1000n1RangeUp);
            CommandList.Add(GPSG1000n1RangeDown.Id, GPSG1000n1RangeDown);
            CommandList.Add(GPSG1000n1PanUp.Id, GPSG1000n1PanUp);
            CommandList.Add(GPSG1000n1PanDown.Id, GPSG1000n1PanDown);
            CommandList.Add(GPSG1000n1PanLeft.Id, GPSG1000n1PanLeft);
            CommandList.Add(GPSG1000n1PanRight.Id, GPSG1000n1PanRight);
            CommandList.Add(GPSG1000n1PanUpLeft.Id, GPSG1000n1PanUpLeft);
            CommandList.Add(GPSG1000n1PanDownLeft.Id, GPSG1000n1PanDownLeft);
            CommandList.Add(GPSG1000n1PanUpRight.Id, GPSG1000n1PanUpRight);
            CommandList.Add(GPSG1000n1PanDownRight.Id, GPSG1000n1PanDownRight);
            CommandList.Add(GPSG1000n1PanPush.Id, GPSG1000n1PanPush);
            CommandList.Add(GPSG1000n1Direct.Id, GPSG1000n1Direct);
            CommandList.Add(GPSG1000n1Menu.Id, GPSG1000n1Menu);
            CommandList.Add(GPSG1000n1Fpl.Id, GPSG1000n1Fpl);
            CommandList.Add(GPSG1000n1Proc.Id, GPSG1000n1Proc);
            CommandList.Add(GPSG1000n1Clr.Id, GPSG1000n1Clr);
            CommandList.Add(GPSG1000n1Ent.Id, GPSG1000n1Ent);
            CommandList.Add(GPSG1000n1FmsOuterUp.Id, GPSG1000n1FmsOuterUp);
            CommandList.Add(GPSG1000n1FmsOuterDown.Id, GPSG1000n1FmsOuterDown);
            CommandList.Add(GPSG1000n1FmsInnerUp.Id, GPSG1000n1FmsInnerUp);
            CommandList.Add(GPSG1000n1FmsInnerDown.Id, GPSG1000n1FmsInnerDown);
            CommandList.Add(GPSG1000n1Cursor.Id, GPSG1000n1Cursor);
            CommandList.Add(GPSG1000n1Popout.Id, GPSG1000n1Popout);
            CommandList.Add(GPSG1000n1Popup.Id, GPSG1000n1Popup);
            CommandList.Add(GPSG1000n2Nvol.Id, GPSG1000n2Nvol);
            CommandList.Add(GPSG1000n2NvolUp.Id, GPSG1000n2NvolUp);
            CommandList.Add(GPSG1000n2NvolDn.Id, GPSG1000n2NvolDn);
            CommandList.Add(GPSG1000n2NavFf.Id, GPSG1000n2NavFf);
            CommandList.Add(GPSG1000n2NavOuterUp.Id, GPSG1000n2NavOuterUp);
            CommandList.Add(GPSG1000n2NavOuterDown.Id, GPSG1000n2NavOuterDown);
            CommandList.Add(GPSG1000n2NavInnerUp.Id, GPSG1000n2NavInnerUp);
            CommandList.Add(GPSG1000n2NavInnerDown.Id, GPSG1000n2NavInnerDown);
            CommandList.Add(GPSG1000n2Nav12.Id, GPSG1000n2Nav12);
            CommandList.Add(GPSG1000n2HdgUp.Id, GPSG1000n2HdgUp);
            CommandList.Add(GPSG1000n2HdgDown.Id, GPSG1000n2HdgDown);
            CommandList.Add(GPSG1000n2HdgSync.Id, GPSG1000n2HdgSync);
            CommandList.Add(GPSG1000n2Ap.Id, GPSG1000n2Ap);
            CommandList.Add(GPSG1000n2Fd.Id, GPSG1000n2Fd);
            CommandList.Add(GPSG1000n2Yd.Id, GPSG1000n2Yd);
            CommandList.Add(GPSG1000n2Hdg.Id, GPSG1000n2Hdg);
            CommandList.Add(GPSG1000n2Alt.Id, GPSG1000n2Alt);
            CommandList.Add(GPSG1000n2Nav.Id, GPSG1000n2Nav);
            CommandList.Add(GPSG1000n2Vnv.Id, GPSG1000n2Vnv);
            CommandList.Add(GPSG1000n2Apr.Id, GPSG1000n2Apr);
            CommandList.Add(GPSG1000n2Bc.Id, GPSG1000n2Bc);
            CommandList.Add(GPSG1000n2Vs.Id, GPSG1000n2Vs);
            CommandList.Add(GPSG1000n2Flc.Id, GPSG1000n2Flc);
            CommandList.Add(GPSG1000n2NoseUp.Id, GPSG1000n2NoseUp);
            CommandList.Add(GPSG1000n2NoseDown.Id, GPSG1000n2NoseDown);
            CommandList.Add(GPSG1000n2AltOuterUp.Id, GPSG1000n2AltOuterUp);
            CommandList.Add(GPSG1000n2AltOuterDown.Id, GPSG1000n2AltOuterDown);
            CommandList.Add(GPSG1000n2AltInnerUp.Id, GPSG1000n2AltInnerUp);
            CommandList.Add(GPSG1000n2AltInnerDown.Id, GPSG1000n2AltInnerDown);
            CommandList.Add(GPSG1000n2Softkey1.Id, GPSG1000n2Softkey1);
            CommandList.Add(GPSG1000n2Softkey2.Id, GPSG1000n2Softkey2);
            CommandList.Add(GPSG1000n2Softkey3.Id, GPSG1000n2Softkey3);
            CommandList.Add(GPSG1000n2Softkey4.Id, GPSG1000n2Softkey4);
            CommandList.Add(GPSG1000n2Softkey5.Id, GPSG1000n2Softkey5);
            CommandList.Add(GPSG1000n2Softkey6.Id, GPSG1000n2Softkey6);
            CommandList.Add(GPSG1000n2Softkey7.Id, GPSG1000n2Softkey7);
            CommandList.Add(GPSG1000n2Softkey8.Id, GPSG1000n2Softkey8);
            CommandList.Add(GPSG1000n2Softkey9.Id, GPSG1000n2Softkey9);
            CommandList.Add(GPSG1000n2Softkey10.Id, GPSG1000n2Softkey10);
            CommandList.Add(GPSG1000n2Softkey11.Id, GPSG1000n2Softkey11);
            CommandList.Add(GPSG1000n2Softkey12.Id, GPSG1000n2Softkey12);
            CommandList.Add(GPSG1000n2Cvol.Id, GPSG1000n2Cvol);
            CommandList.Add(GPSG1000n2CvolUp.Id, GPSG1000n2CvolUp);
            CommandList.Add(GPSG1000n2CvolDn.Id, GPSG1000n2CvolDn);
            CommandList.Add(GPSG1000n2ComFf.Id, GPSG1000n2ComFf);
            CommandList.Add(GPSG1000n2ComOuterUp.Id, GPSG1000n2ComOuterUp);
            CommandList.Add(GPSG1000n2ComOuterDown.Id, GPSG1000n2ComOuterDown);
            CommandList.Add(GPSG1000n2ComInnerUp.Id, GPSG1000n2ComInnerUp);
            CommandList.Add(GPSG1000n2ComInnerDown.Id, GPSG1000n2ComInnerDown);
            CommandList.Add(GPSG1000n2Com12.Id, GPSG1000n2Com12);
            CommandList.Add(GPSG1000n2CrsUp.Id, GPSG1000n2CrsUp);
            CommandList.Add(GPSG1000n2CrsDown.Id, GPSG1000n2CrsDown);
            CommandList.Add(GPSG1000n2CrsSync.Id, GPSG1000n2CrsSync);
            CommandList.Add(GPSG1000n2BaroUp.Id, GPSG1000n2BaroUp);
            CommandList.Add(GPSG1000n2BaroDown.Id, GPSG1000n2BaroDown);
            CommandList.Add(GPSG1000n2RangeUp.Id, GPSG1000n2RangeUp);
            CommandList.Add(GPSG1000n2RangeDown.Id, GPSG1000n2RangeDown);
            CommandList.Add(GPSG1000n2PanUp.Id, GPSG1000n2PanUp);
            CommandList.Add(GPSG1000n2PanDown.Id, GPSG1000n2PanDown);
            CommandList.Add(GPSG1000n2PanLeft.Id, GPSG1000n2PanLeft);
            CommandList.Add(GPSG1000n2PanRight.Id, GPSG1000n2PanRight);
            CommandList.Add(GPSG1000n2PanUpLeft.Id, GPSG1000n2PanUpLeft);
            CommandList.Add(GPSG1000n2PanDownLeft.Id, GPSG1000n2PanDownLeft);
            CommandList.Add(GPSG1000n2PanUpRight.Id, GPSG1000n2PanUpRight);
            CommandList.Add(GPSG1000n2PanDownRight.Id, GPSG1000n2PanDownRight);
            CommandList.Add(GPSG1000n2PanPush.Id, GPSG1000n2PanPush);
            CommandList.Add(GPSG1000n2Direct.Id, GPSG1000n2Direct);
            CommandList.Add(GPSG1000n2Menu.Id, GPSG1000n2Menu);
            CommandList.Add(GPSG1000n2Fpl.Id, GPSG1000n2Fpl);
            CommandList.Add(GPSG1000n2Proc.Id, GPSG1000n2Proc);
            CommandList.Add(GPSG1000n2Clr.Id, GPSG1000n2Clr);
            CommandList.Add(GPSG1000n2Ent.Id, GPSG1000n2Ent);
            CommandList.Add(GPSG1000n2FmsOuterUp.Id, GPSG1000n2FmsOuterUp);
            CommandList.Add(GPSG1000n2FmsOuterDown.Id, GPSG1000n2FmsOuterDown);
            CommandList.Add(GPSG1000n2FmsInnerUp.Id, GPSG1000n2FmsInnerUp);
            CommandList.Add(GPSG1000n2FmsInnerDown.Id, GPSG1000n2FmsInnerDown);
            CommandList.Add(GPSG1000n2Cursor.Id, GPSG1000n2Cursor);
            CommandList.Add(GPSG1000n2Popout.Id, GPSG1000n2Popout);
            CommandList.Add(GPSG1000n2Popup.Id, GPSG1000n2Popup);
            CommandList.Add(GPSG1000n3Nvol.Id, GPSG1000n3Nvol);
            CommandList.Add(GPSG1000n3NvolUp.Id, GPSG1000n3NvolUp);
            CommandList.Add(GPSG1000n3NvolDn.Id, GPSG1000n3NvolDn);
            CommandList.Add(GPSG1000n3NavFf.Id, GPSG1000n3NavFf);
            CommandList.Add(GPSG1000n3NavOuterUp.Id, GPSG1000n3NavOuterUp);
            CommandList.Add(GPSG1000n3NavOuterDown.Id, GPSG1000n3NavOuterDown);
            CommandList.Add(GPSG1000n3NavInnerUp.Id, GPSG1000n3NavInnerUp);
            CommandList.Add(GPSG1000n3NavInnerDown.Id, GPSG1000n3NavInnerDown);
            CommandList.Add(GPSG1000n3Nav12.Id, GPSG1000n3Nav12);
            CommandList.Add(GPSG1000n3HdgUp.Id, GPSG1000n3HdgUp);
            CommandList.Add(GPSG1000n3HdgDown.Id, GPSG1000n3HdgDown);
            CommandList.Add(GPSG1000n3HdgSync.Id, GPSG1000n3HdgSync);
            CommandList.Add(GPSG1000n3Ap.Id, GPSG1000n3Ap);
            CommandList.Add(GPSG1000n3Fd.Id, GPSG1000n3Fd);
            CommandList.Add(GPSG1000n3Yd.Id, GPSG1000n3Yd);
            CommandList.Add(GPSG1000n3Hdg.Id, GPSG1000n3Hdg);
            CommandList.Add(GPSG1000n3Alt.Id, GPSG1000n3Alt);
            CommandList.Add(GPSG1000n3Nav.Id, GPSG1000n3Nav);
            CommandList.Add(GPSG1000n3Vnv.Id, GPSG1000n3Vnv);
            CommandList.Add(GPSG1000n3Apr.Id, GPSG1000n3Apr);
            CommandList.Add(GPSG1000n3Bc.Id, GPSG1000n3Bc);
            CommandList.Add(GPSG1000n3Vs.Id, GPSG1000n3Vs);
            CommandList.Add(GPSG1000n3Flc.Id, GPSG1000n3Flc);
            CommandList.Add(GPSG1000n3NoseUp.Id, GPSG1000n3NoseUp);
            CommandList.Add(GPSG1000n3NoseDown.Id, GPSG1000n3NoseDown);
            CommandList.Add(GPSG1000n3AltOuterUp.Id, GPSG1000n3AltOuterUp);
            CommandList.Add(GPSG1000n3AltOuterDown.Id, GPSG1000n3AltOuterDown);
            CommandList.Add(GPSG1000n3AltInnerUp.Id, GPSG1000n3AltInnerUp);
            CommandList.Add(GPSG1000n3AltInnerDown.Id, GPSG1000n3AltInnerDown);
            CommandList.Add(GPSG1000n3Softkey1.Id, GPSG1000n3Softkey1);
            CommandList.Add(GPSG1000n3Softkey2.Id, GPSG1000n3Softkey2);
            CommandList.Add(GPSG1000n3Softkey3.Id, GPSG1000n3Softkey3);
            CommandList.Add(GPSG1000n3Softkey4.Id, GPSG1000n3Softkey4);
            CommandList.Add(GPSG1000n3Softkey5.Id, GPSG1000n3Softkey5);
            CommandList.Add(GPSG1000n3Softkey6.Id, GPSG1000n3Softkey6);
            CommandList.Add(GPSG1000n3Softkey7.Id, GPSG1000n3Softkey7);
            CommandList.Add(GPSG1000n3Softkey8.Id, GPSG1000n3Softkey8);
            CommandList.Add(GPSG1000n3Softkey9.Id, GPSG1000n3Softkey9);
            CommandList.Add(GPSG1000n3Softkey10.Id, GPSG1000n3Softkey10);
            CommandList.Add(GPSG1000n3Softkey11.Id, GPSG1000n3Softkey11);
            CommandList.Add(GPSG1000n3Softkey12.Id, GPSG1000n3Softkey12);
            CommandList.Add(GPSG1000n3Cvol.Id, GPSG1000n3Cvol);
            CommandList.Add(GPSG1000n3CvolUp.Id, GPSG1000n3CvolUp);
            CommandList.Add(GPSG1000n3CvolDn.Id, GPSG1000n3CvolDn);
            CommandList.Add(GPSG1000n3ComFf.Id, GPSG1000n3ComFf);
            CommandList.Add(GPSG1000n3ComOuterUp.Id, GPSG1000n3ComOuterUp);
            CommandList.Add(GPSG1000n3ComOuterDown.Id, GPSG1000n3ComOuterDown);
            CommandList.Add(GPSG1000n3ComInnerUp.Id, GPSG1000n3ComInnerUp);
            CommandList.Add(GPSG1000n3ComInnerDown.Id, GPSG1000n3ComInnerDown);
            CommandList.Add(GPSG1000n3Com12.Id, GPSG1000n3Com12);
            CommandList.Add(GPSG1000n3CrsUp.Id, GPSG1000n3CrsUp);
            CommandList.Add(GPSG1000n3CrsDown.Id, GPSG1000n3CrsDown);
            CommandList.Add(GPSG1000n3CrsSync.Id, GPSG1000n3CrsSync);
            CommandList.Add(GPSG1000n3BaroUp.Id, GPSG1000n3BaroUp);
            CommandList.Add(GPSG1000n3BaroDown.Id, GPSG1000n3BaroDown);
            CommandList.Add(GPSG1000n3RangeUp.Id, GPSG1000n3RangeUp);
            CommandList.Add(GPSG1000n3RangeDown.Id, GPSG1000n3RangeDown);
            CommandList.Add(GPSG1000n3PanUp.Id, GPSG1000n3PanUp);
            CommandList.Add(GPSG1000n3PanDown.Id, GPSG1000n3PanDown);
            CommandList.Add(GPSG1000n3PanLeft.Id, GPSG1000n3PanLeft);
            CommandList.Add(GPSG1000n3PanRight.Id, GPSG1000n3PanRight);
            CommandList.Add(GPSG1000n3PanUpLeft.Id, GPSG1000n3PanUpLeft);
            CommandList.Add(GPSG1000n3PanDownLeft.Id, GPSG1000n3PanDownLeft);
            CommandList.Add(GPSG1000n3PanUpRight.Id, GPSG1000n3PanUpRight);
            CommandList.Add(GPSG1000n3PanDownRight.Id, GPSG1000n3PanDownRight);
            CommandList.Add(GPSG1000n3PanPush.Id, GPSG1000n3PanPush);
            CommandList.Add(GPSG1000n3Direct.Id, GPSG1000n3Direct);
            CommandList.Add(GPSG1000n3Menu.Id, GPSG1000n3Menu);
            CommandList.Add(GPSG1000n3Fpl.Id, GPSG1000n3Fpl);
            CommandList.Add(GPSG1000n3Proc.Id, GPSG1000n3Proc);
            CommandList.Add(GPSG1000n3Clr.Id, GPSG1000n3Clr);
            CommandList.Add(GPSG1000n3Ent.Id, GPSG1000n3Ent);
            CommandList.Add(GPSG1000n3FmsOuterUp.Id, GPSG1000n3FmsOuterUp);
            CommandList.Add(GPSG1000n3FmsOuterDown.Id, GPSG1000n3FmsOuterDown);
            CommandList.Add(GPSG1000n3FmsInnerUp.Id, GPSG1000n3FmsInnerUp);
            CommandList.Add(GPSG1000n3FmsInnerDown.Id, GPSG1000n3FmsInnerDown);
            CommandList.Add(GPSG1000n3Cursor.Id, GPSG1000n3Cursor);
            CommandList.Add(GPSG1000n3Popout.Id, GPSG1000n3Popout);
            CommandList.Add(GPSG1000n3Popup.Id, GPSG1000n3Popup);
            CommandList.Add(GPSGcu478A.Id, GPSGcu478A);
            CommandList.Add(GPSGcu478B.Id, GPSGcu478B);
            CommandList.Add(GPSGcu478C.Id, GPSGcu478C);
            CommandList.Add(GPSGcu478D.Id, GPSGcu478D);
            CommandList.Add(GPSGcu478E.Id, GPSGcu478E);
            CommandList.Add(GPSGcu478F.Id, GPSGcu478F);
            CommandList.Add(GPSGcu478G.Id, GPSGcu478G);
            CommandList.Add(GPSGcu478H.Id, GPSGcu478H);
            CommandList.Add(GPSGcu478I.Id, GPSGcu478I);
            CommandList.Add(GPSGcu478J.Id, GPSGcu478J);
            CommandList.Add(GPSGcu478K.Id, GPSGcu478K);
            CommandList.Add(GPSGcu478L.Id, GPSGcu478L);
            CommandList.Add(GPSGcu478M.Id, GPSGcu478M);
            CommandList.Add(GPSGcu478N.Id, GPSGcu478N);
            CommandList.Add(GPSGcu478O.Id, GPSGcu478O);
            CommandList.Add(GPSGcu478P.Id, GPSGcu478P);
            CommandList.Add(GPSGcu478Q.Id, GPSGcu478Q);
            CommandList.Add(GPSGcu478R.Id, GPSGcu478R);
            CommandList.Add(GPSGcu478S.Id, GPSGcu478S);
            CommandList.Add(GPSGcu478T.Id, GPSGcu478T);
            CommandList.Add(GPSGcu478U.Id, GPSGcu478U);
            CommandList.Add(GPSGcu478V.Id, GPSGcu478V);
            CommandList.Add(GPSGcu478W.Id, GPSGcu478W);
            CommandList.Add(GPSGcu478X.Id, GPSGcu478X);
            CommandList.Add(GPSGcu478Y.Id, GPSGcu478Y);
            CommandList.Add(GPSGcu478Z.Id, GPSGcu478Z);
            CommandList.Add(GPSGcu4780.Id, GPSGcu4780);
            CommandList.Add(GPSGcu4781.Id, GPSGcu4781);
            CommandList.Add(GPSGcu4782.Id, GPSGcu4782);
            CommandList.Add(GPSGcu4783.Id, GPSGcu4783);
            CommandList.Add(GPSGcu4784.Id, GPSGcu4784);
            CommandList.Add(GPSGcu4785.Id, GPSGcu4785);
            CommandList.Add(GPSGcu4786.Id, GPSGcu4786);
            CommandList.Add(GPSGcu4787.Id, GPSGcu4787);
            CommandList.Add(GPSGcu4788.Id, GPSGcu4788);
            CommandList.Add(GPSGcu4789.Id, GPSGcu4789);
            CommandList.Add(GPSGcu478Dot.Id, GPSGcu478Dot);
            CommandList.Add(GPSGcu478Minus.Id, GPSGcu478Minus);
            CommandList.Add(GPSGcu478Spc.Id, GPSGcu478Spc);
            CommandList.Add(GPSGcu478Bksp.Id, GPSGcu478Bksp);
            CommandList.Add(GPSGcu478HdgUp.Id, GPSGcu478HdgUp);
            CommandList.Add(GPSGcu478HdgDown.Id, GPSGcu478HdgDown);
            CommandList.Add(GPSGcu478HdgSync.Id, GPSGcu478HdgSync);
            CommandList.Add(GPSGcu478CrsUp.Id, GPSGcu478CrsUp);
            CommandList.Add(GPSGcu478CrsDown.Id, GPSGcu478CrsDown);
            CommandList.Add(GPSGcu478CrsSync.Id, GPSGcu478CrsSync);
            CommandList.Add(GPSGcu478AltUp.Id, GPSGcu478AltUp);
            CommandList.Add(GPSGcu478AltDown.Id, GPSGcu478AltDown);
            CommandList.Add(GPSGcu478AltSync.Id, GPSGcu478AltSync);
            CommandList.Add(GPSGcu478RangeUp.Id, GPSGcu478RangeUp);
            CommandList.Add(GPSGcu478RangeDown.Id, GPSGcu478RangeDown);
            CommandList.Add(GPSGcu478PanUp.Id, GPSGcu478PanUp);
            CommandList.Add(GPSGcu478PanDown.Id, GPSGcu478PanDown);
            CommandList.Add(GPSGcu478PanLeft.Id, GPSGcu478PanLeft);
            CommandList.Add(GPSGcu478PanRight.Id, GPSGcu478PanRight);
            CommandList.Add(GPSGcu478PanUpLeft.Id, GPSGcu478PanUpLeft);
            CommandList.Add(GPSGcu478PanDownLeft.Id, GPSGcu478PanDownLeft);
            CommandList.Add(GPSGcu478PanUpRight.Id, GPSGcu478PanUpRight);
            CommandList.Add(GPSGcu478PanDownRight.Id, GPSGcu478PanDownRight);
            CommandList.Add(GPSGcu478PanPush.Id, GPSGcu478PanPush);
            CommandList.Add(GPSGcu478Fms.Id, GPSGcu478Fms);
            CommandList.Add(GPSGcu478Xpdr.Id, GPSGcu478Xpdr);
            CommandList.Add(GPSGcu478Com.Id, GPSGcu478Com);
            CommandList.Add(GPSGcu478Nav.Id, GPSGcu478Nav);
            CommandList.Add(GPSGcu478Ff.Id, GPSGcu478Ff);
            CommandList.Add(GPSGcu478Direct.Id, GPSGcu478Direct);
            CommandList.Add(GPSGcu478Menu.Id, GPSGcu478Menu);
            CommandList.Add(GPSGcu478Fpl.Id, GPSGcu478Fpl);
            CommandList.Add(GPSGcu478Proc.Id, GPSGcu478Proc);
            CommandList.Add(GPSGcu478Clr.Id, GPSGcu478Clr);
            CommandList.Add(GPSGcu478Ent.Id, GPSGcu478Ent);
            CommandList.Add(GPSGcu478OuterUp.Id, GPSGcu478OuterUp);
            CommandList.Add(GPSGcu478OuterDown.Id, GPSGcu478OuterDown);
            CommandList.Add(GPSGcu478InnerUp.Id, GPSGcu478InnerUp);
            CommandList.Add(GPSGcu478InnerDown.Id, GPSGcu478InnerDown);
            CommandList.Add(GPSGcu478Cursor.Id, GPSGcu478Cursor);
            CommandList.Add(GPSG1000DisplayReversion.Id, GPSG1000DisplayReversion);
            CommandList.Add(SystemsOverspeedTest.Id, SystemsOverspeedTest);
            CommandList.Add(FuelIndicateAux.Id, FuelIndicateAux);
            CommandList.Add(FuelIndicateAll.Id, FuelIndicateAll);
            CommandList.Add(FuelIndicateNacelle.Id, FuelIndicateNacelle);
            CommandList.Add(AutopilotTestAutoAnnunciators.Id, AutopilotTestAutoAnnunciators);
            CommandList.Add(FlightControlsPitchTrimaUp.Id, FlightControlsPitchTrimaUp);
            CommandList.Add(FlightControlsPitchTrimaDown.Id, FlightControlsPitchTrimaDown);
            CommandList.Add(FlightControlsPitchTrimbUp.Id, FlightControlsPitchTrimbUp);
            CommandList.Add(FlightControlsPitchTrimbDown.Id, FlightControlsPitchTrimbDown);
            CommandList.Add(FlightControlsPitchTrimUp.Id, FlightControlsPitchTrimUp);
            CommandList.Add(FlightControlsPitchTrimDown.Id, FlightControlsPitchTrimDown);
            CommandList.Add(FlightControlsPitchTrimUpMech.Id, FlightControlsPitchTrimUpMech);
            CommandList.Add(FlightControlsPitchTrimDownMech.Id, FlightControlsPitchTrimDownMech);
            CommandList.Add(FlightControlsAileronTrimLeft.Id, FlightControlsAileronTrimLeft);
            CommandList.Add(FlightControlsAileronTrimRight.Id, FlightControlsAileronTrimRight);
            CommandList.Add(FlightControlsRudderTrimLeft.Id, FlightControlsRudderTrimLeft);
            CommandList.Add(FlightControlsRudderTrimRight.Id, FlightControlsRudderTrimRight);
            CommandList.Add(FlightControlsGyroRotorTrimUp.Id, FlightControlsGyroRotorTrimUp);
            CommandList.Add(FlightControlsGyroRotorTrimDown.Id, FlightControlsGyroRotorTrimDown);
            CommandList.Add(FlightControlsRotorRpmTrimUp.Id, FlightControlsRotorRpmTrimUp);
            CommandList.Add(FlightControlsRotorRpmTrimDown.Id, FlightControlsRotorRpmTrimDown);
            CommandList.Add(FlightControlsMagneticLock.Id, FlightControlsMagneticLock);
            CommandList.Add(FlightControlsPitchTrimTakeoff.Id, FlightControlsPitchTrimTakeoff);
            CommandList.Add(FlightControlsAileronTrimCenter.Id, FlightControlsAileronTrimCenter);
            CommandList.Add(FlightControlsRudderTrimCenter.Id, FlightControlsRudderTrimCenter);
            CommandList.Add(FlightControlsRudderLft.Id, FlightControlsRudderLft);
            CommandList.Add(FlightControlsRudderCtr.Id, FlightControlsRudderCtr);
            CommandList.Add(FlightControlsRudderRgt.Id, FlightControlsRudderRgt);
            CommandList.Add(AutopilotSetOttSeldispALTVVIVvi.Id, AutopilotSetOttSeldispALTVVIVvi);
            CommandList.Add(AutopilotSetOttSeldispALTVVIAlt.Id, AutopilotSetOttSeldispALTVVIAlt);
            CommandList.Add(OperationGroundSpeedChange.Id, OperationGroundSpeedChange);
            CommandList.Add(OperationFreezeToggle.Id, OperationFreezeToggle);
            CommandList.Add(InstrumentsTimerStartStop.Id, InstrumentsTimerStartStop);
            CommandList.Add(InstrumentsTimerReset.Id, InstrumentsTimerReset);
            CommandList.Add(InstrumentsTimerShowDate.Id, InstrumentsTimerShowDate);
            CommandList.Add(InstrumentsTimerMode.Id, InstrumentsTimerMode);
            CommandList.Add(InstrumentsTimerCycle.Id, InstrumentsTimerCycle);
            CommandList.Add(OperationTimeDown.Id, OperationTimeDown);
            CommandList.Add(OperationTimeUp.Id, OperationTimeUp);
            CommandList.Add(OperationTimeDownLots.Id, OperationTimeDownLots);
            CommandList.Add(OperationTimeUpLots.Id, OperationTimeUpLots);
            CommandList.Add(OperationDateDown.Id, OperationDateDown);
            CommandList.Add(OperationDateUp.Id, OperationDateUp);
            CommandList.Add(InstrumentsTimerIsGMT.Id, InstrumentsTimerIsGMT);
            CommandList.Add(OperationFlightmodelSpeedChange.Id, OperationFlightmodelSpeedChange);
            CommandList.Add(OperationPauseToggle.Id, OperationPauseToggle);
            CommandList.Add(OperationVideoRecordToggle.Id, OperationVideoRecordToggle);
            CommandList.Add(OperationConfigureVideoRecording.Id, OperationConfigureVideoRecording);
            CommandList.Add(ReplayReplayToggle.Id, ReplayReplayToggle);
            CommandList.Add(ReplayReplayOff.Id, ReplayReplayOff);
            CommandList.Add(ReplayReplayControlsToggle.Id, ReplayReplayControlsToggle);
            CommandList.Add(ReplayRepBegin.Id, ReplayRepBegin);
            CommandList.Add(ReplayRepPlayFr.Id, ReplayRepPlayFr);
            CommandList.Add(ReplayRepPlayRr.Id, ReplayRepPlayRr);
            CommandList.Add(ReplayRepPlaySr.Id, ReplayRepPlaySr);
            CommandList.Add(ReplayRepPause.Id, ReplayRepPause);
            CommandList.Add(ReplayRepPlaySf.Id, ReplayRepPlaySf);
            CommandList.Add(ReplayRepPlayRf.Id, ReplayRepPlayRf);
            CommandList.Add(ReplayRepPlayFf.Id, ReplayRepPlayFf);
            CommandList.Add(ReplayRepEnd.Id, ReplayRepEnd);
            CommandList.Add(OperationToggleLogbook.Id, OperationToggleLogbook);
            CommandList.Add(OperationSaveFlight.Id, OperationSaveFlight);
            CommandList.Add(OperationLoadFlight.Id, OperationLoadFlight);
            CommandList.Add(OperationTextFileToggle.Id, OperationTextFileToggle);
            CommandList.Add(OperationChecklistToggle.Id, OperationChecklistToggle);
            CommandList.Add(OperationChecklistNext.Id, OperationChecklistNext);
            CommandList.Add(OperationChecklistPrevious.Id, OperationChecklistPrevious);
            CommandList.Add(OperationContactAtc.Id, OperationContactAtc);
            CommandList.Add(OperationToggleAiFlies.Id, OperationToggleAiFlies);
            CommandList.Add(OperationToggleYoke.Id, OperationToggleYoke);
            CommandList.Add(OperationResetFlight.Id, OperationResetFlight);
            CommandList.Add(OperationGoToDefault.Id, OperationGoToDefault);
            CommandList.Add(OperationResetToRunway.Id, OperationResetToRunway);
            CommandList.Add(OperationGoNextRunway.Id, OperationGoNextRunway);
            CommandList.Add(OperationGrassFieldTakeoff.Id, OperationGrassFieldTakeoff);
            CommandList.Add(OperationDirtFieldTakeoff.Id, OperationDirtFieldTakeoff);
            CommandList.Add(OperationGravelFieldTakeoff.Id, OperationGravelFieldTakeoff);
            CommandList.Add(OperationWaterWayTakeoff.Id, OperationWaterWayTakeoff);
            CommandList.Add(OperationHelipadTakeoff.Id, OperationHelipadTakeoff);
            CommandList.Add(OperationCarrierCatshot.Id, OperationCarrierCatshot);
            CommandList.Add(OperationGliderWinch.Id, OperationGliderWinch);
            CommandList.Add(OperationGliderTow.Id, OperationGliderTow);
            CommandList.Add(OperationAirDropFromB52.Id, OperationAirDropFromB52);
            CommandList.Add(OperationStartCarried.Id, OperationStartCarried);
            CommandList.Add(OperationPiggybackShuttleOn747.Id, OperationPiggybackShuttleOn747);
            CommandList.Add(OperationCarryOtherAircraft.Id, OperationCarryOtherAircraft);
            CommandList.Add(OperationFormationFlying.Id, OperationFormationFlying);
            CommandList.Add(OperationAirRefuelBoom.Id, OperationAirRefuelBoom);
            CommandList.Add(OperationAirRefuelBasket.Id, OperationAirRefuelBasket);
            CommandList.Add(OperationAircraftCarrierApproach.Id, OperationAircraftCarrierApproach);
            CommandList.Add(OperationFrigateApproach.Id, OperationFrigateApproach);
            CommandList.Add(OperationMediumOilRigApproach.Id, OperationMediumOilRigApproach);
            CommandList.Add(OperationLargeOilPlatformApproach.Id, OperationLargeOilPlatformApproach);
            CommandList.Add(OperationForestFireApproach.Id, OperationForestFireApproach);
            CommandList.Add(OperationSpaceShuttleFullReEntry.Id, OperationSpaceShuttleFullReEntry);
            CommandList.Add(OperationSpaceShuttleFinalReEntry.Id, OperationSpaceShuttleFinalReEntry);
            CommandList.Add(OperationSpaceShuttleFullApproach.Id, OperationSpaceShuttleFullApproach);
            CommandList.Add(OperationSpaceShuttleFinalApproach.Id, OperationSpaceShuttleFinalApproach);
            CommandList.Add(ViewAiControlsViews.Id, ViewAiControlsViews);
            CommandList.Add(ViewFreeCamera.Id, ViewFreeCamera);
            CommandList.Add(ViewDefaultView.Id, ViewDefaultView);
            CommandList.Add(ViewForwardWith2DPanel.Id, ViewForwardWith2DPanel);
            CommandList.Add(ViewForwardWithHud.Id, ViewForwardWithHud);
            CommandList.Add(ViewForwardWithNothing.Id, ViewForwardWithNothing);
            CommandList.Add(ViewLinearSpot.Id, ViewLinearSpot);
            CommandList.Add(ViewStillSpot.Id, ViewStillSpot);
            CommandList.Add(ViewRunway.Id, ViewRunway);
            CommandList.Add(ViewCircle.Id, ViewCircle);
            CommandList.Add(ViewTower.Id, ViewTower);
            CommandList.Add(ViewRidealong.Id, ViewRidealong);
            CommandList.Add(ViewTrackWeapon.Id, ViewTrackWeapon);
            CommandList.Add(ViewChase.Id, ViewChase);
            CommandList.Add(View3DCockpitCmndLook.Id, View3DCockpitCmndLook);
            CommandList.Add(View3DCockpitToggle.Id, View3DCockpitToggle);
            CommandList.Add(ViewLockGeo.Id, ViewLockGeo);
            CommandList.Add(ViewCinemaVerite.Id, ViewCinemaVerite);
            CommandList.Add(ViewSunglasses.Id, ViewSunglasses);
            CommandList.Add(ViewNightVision.Id, ViewNightVision);
            CommandList.Add(ViewFlashlightRed.Id, ViewFlashlightRed);
            CommandList.Add(ViewFlashlightWht.Id, ViewFlashlightWht);
            CommandList.Add(ViewGlanceLeft.Id, ViewGlanceLeft);
            CommandList.Add(ViewGlanceRight.Id, ViewGlanceRight);
            CommandList.Add(ViewUpLeft.Id, ViewUpLeft);
            CommandList.Add(ViewUpRight.Id, ViewUpRight);
            CommandList.Add(ViewStraightUp.Id, ViewStraightUp);
            CommandList.Add(ViewStraightDown.Id, ViewStraightDown);
            CommandList.Add(ViewLeft45.Id, ViewLeft45);
            CommandList.Add(ViewRight45.Id, ViewRight45);
            CommandList.Add(ViewLeft90.Id, ViewLeft90);
            CommandList.Add(ViewRight90.Id, ViewRight90);
            CommandList.Add(ViewLeft135.Id, ViewLeft135);
            CommandList.Add(ViewRight135.Id, ViewRight135);
            CommandList.Add(ViewBack.Id, ViewBack);
            CommandList.Add(View3DPathToggle.Id, View3DPathToggle);
            CommandList.Add(View3DPathReset.Id, View3DPathReset);
            CommandList.Add(ViewShowPhysicsModel.Id, ViewShowPhysicsModel);
            CommandList.Add(ViewMouseClickRegionsToggle.Id, ViewMouseClickRegionsToggle);
            CommandList.Add(ViewInstrumentDescriptionsToggle.Id, ViewInstrumentDescriptionsToggle);
            CommandList.Add(ViewQuickLook0.Id, ViewQuickLook0);
            CommandList.Add(ViewQuickLook1.Id, ViewQuickLook1);
            CommandList.Add(ViewQuickLook2.Id, ViewQuickLook2);
            CommandList.Add(ViewQuickLook3.Id, ViewQuickLook3);
            CommandList.Add(ViewQuickLook4.Id, ViewQuickLook4);
            CommandList.Add(ViewQuickLook5.Id, ViewQuickLook5);
            CommandList.Add(ViewQuickLook6.Id, ViewQuickLook6);
            CommandList.Add(ViewQuickLook7.Id, ViewQuickLook7);
            CommandList.Add(ViewQuickLook8.Id, ViewQuickLook8);
            CommandList.Add(ViewQuickLook9.Id, ViewQuickLook9);
            CommandList.Add(ViewQuickLook10.Id, ViewQuickLook10);
            CommandList.Add(ViewQuickLook11.Id, ViewQuickLook11);
            CommandList.Add(ViewQuickLook12.Id, ViewQuickLook12);
            CommandList.Add(ViewQuickLook13.Id, ViewQuickLook13);
            CommandList.Add(ViewQuickLook14.Id, ViewQuickLook14);
            CommandList.Add(ViewQuickLook15.Id, ViewQuickLook15);
            CommandList.Add(ViewQuickLook16.Id, ViewQuickLook16);
            CommandList.Add(ViewQuickLook17.Id, ViewQuickLook17);
            CommandList.Add(ViewQuickLook18.Id, ViewQuickLook18);
            CommandList.Add(ViewQuickLook19.Id, ViewQuickLook19);
            CommandList.Add(ViewQuickLook0Mem.Id, ViewQuickLook0Mem);
            CommandList.Add(ViewQuickLook1Mem.Id, ViewQuickLook1Mem);
            CommandList.Add(ViewQuickLook2Mem.Id, ViewQuickLook2Mem);
            CommandList.Add(ViewQuickLook3Mem.Id, ViewQuickLook3Mem);
            CommandList.Add(ViewQuickLook4Mem.Id, ViewQuickLook4Mem);
            CommandList.Add(ViewQuickLook5Mem.Id, ViewQuickLook5Mem);
            CommandList.Add(ViewQuickLook6Mem.Id, ViewQuickLook6Mem);
            CommandList.Add(ViewQuickLook7Mem.Id, ViewQuickLook7Mem);
            CommandList.Add(ViewQuickLook8Mem.Id, ViewQuickLook8Mem);
            CommandList.Add(ViewQuickLook9Mem.Id, ViewQuickLook9Mem);
            CommandList.Add(ViewQuickLook10Mem.Id, ViewQuickLook10Mem);
            CommandList.Add(ViewQuickLook11Mem.Id, ViewQuickLook11Mem);
            CommandList.Add(ViewQuickLook12Mem.Id, ViewQuickLook12Mem);
            CommandList.Add(ViewQuickLook13Mem.Id, ViewQuickLook13Mem);
            CommandList.Add(ViewQuickLook14Mem.Id, ViewQuickLook14Mem);
            CommandList.Add(ViewQuickLook15Mem.Id, ViewQuickLook15Mem);
            CommandList.Add(ViewQuickLook16Mem.Id, ViewQuickLook16Mem);
            CommandList.Add(ViewQuickLook17Mem.Id, ViewQuickLook17Mem);
            CommandList.Add(ViewQuickLook18Mem.Id, ViewQuickLook18Mem);
            CommandList.Add(ViewQuickLook19Mem.Id, ViewQuickLook19Mem);
            CommandList.Add(GeneralLeft.Id, GeneralLeft);
            CommandList.Add(GeneralRight.Id, GeneralRight);
            CommandList.Add(GeneralUp.Id, GeneralUp);
            CommandList.Add(GeneralDown.Id, GeneralDown);
            CommandList.Add(GeneralForward.Id, GeneralForward);
            CommandList.Add(GeneralBackward.Id, GeneralBackward);
            CommandList.Add(GeneralZoomIn.Id, GeneralZoomIn);
            CommandList.Add(GeneralZoomOut.Id, GeneralZoomOut);
            CommandList.Add(GeneralHatSwitchLeft.Id, GeneralHatSwitchLeft);
            CommandList.Add(GeneralHatSwitchRight.Id, GeneralHatSwitchRight);
            CommandList.Add(GeneralHatSwitchUp.Id, GeneralHatSwitchUp);
            CommandList.Add(GeneralHatSwitchDown.Id, GeneralHatSwitchDown);
            CommandList.Add(GeneralHatSwitchUpLeft.Id, GeneralHatSwitchUpLeft);
            CommandList.Add(GeneralHatSwitchUpRight.Id, GeneralHatSwitchUpRight);
            CommandList.Add(GeneralHatSwitchDownLeft.Id, GeneralHatSwitchDownLeft);
            CommandList.Add(GeneralHatSwitchDownRight.Id, GeneralHatSwitchDownRight);
            CommandList.Add(GeneralLeftFast.Id, GeneralLeftFast);
            CommandList.Add(GeneralRightFast.Id, GeneralRightFast);
            CommandList.Add(GeneralUpFast.Id, GeneralUpFast);
            CommandList.Add(GeneralDownFast.Id, GeneralDownFast);
            CommandList.Add(GeneralForwardFast.Id, GeneralForwardFast);
            CommandList.Add(GeneralBackwardFast.Id, GeneralBackwardFast);
            CommandList.Add(GeneralZoomInFast.Id, GeneralZoomInFast);
            CommandList.Add(GeneralZoomOutFast.Id, GeneralZoomOutFast);
            CommandList.Add(GeneralLeftSlow.Id, GeneralLeftSlow);
            CommandList.Add(GeneralRightSlow.Id, GeneralRightSlow);
            CommandList.Add(GeneralUpSlow.Id, GeneralUpSlow);
            CommandList.Add(GeneralDownSlow.Id, GeneralDownSlow);
            CommandList.Add(GeneralForwardSlow.Id, GeneralForwardSlow);
            CommandList.Add(GeneralBackwardSlow.Id, GeneralBackwardSlow);
            CommandList.Add(GeneralZoomInSlow.Id, GeneralZoomInSlow);
            CommandList.Add(GeneralZoomOutSlow.Id, GeneralZoomOutSlow);
            CommandList.Add(GeneralRotUp.Id, GeneralRotUp);
            CommandList.Add(GeneralRotDown.Id, GeneralRotDown);
            CommandList.Add(GeneralRotLeft.Id, GeneralRotLeft);
            CommandList.Add(GeneralRotRight.Id, GeneralRotRight);
            CommandList.Add(GeneralRotUpFast.Id, GeneralRotUpFast);
            CommandList.Add(GeneralRotDownFast.Id, GeneralRotDownFast);
            CommandList.Add(GeneralRotLeftFast.Id, GeneralRotLeftFast);
            CommandList.Add(GeneralRotRightFast.Id, GeneralRotRightFast);
            CommandList.Add(GeneralRotUpSlow.Id, GeneralRotUpSlow);
            CommandList.Add(GeneralRotDownSlow.Id, GeneralRotDownSlow);
            CommandList.Add(GeneralRotLeftSlow.Id, GeneralRotLeftSlow);
            CommandList.Add(GeneralRotRightSlow.Id, GeneralRotRightSlow);
            CommandList.Add(GeneralTrackP0.Id, GeneralTrackP0);
            CommandList.Add(GeneralTrackPNext.Id, GeneralTrackPNext);
            CommandList.Add(GeneralTrackPPrev.Id, GeneralTrackPPrev);
            CommandList.Add(GeneralToggleArtificialStabWin.Id, GeneralToggleArtificialStabWin);
            CommandList.Add(GeneralToggleTrafficPaths.Id, GeneralToggleTrafficPaths);
            CommandList.Add(GeneralToggleAutopilotConstantsWin.Id, GeneralToggleAutopilotConstantsWin);
            CommandList.Add(GeneralToggleFadecWin.Id, GeneralToggleFadecWin);
            CommandList.Add(GeneralToggleControlDeflectionsWin.Id, GeneralToggleControlDeflectionsWin);
            CommandList.Add(GeneralToggleWeaponGuidanceWin.Id, GeneralToggleWeaponGuidanceWin);
            CommandList.Add(DeveloperToggleTextureBrowser.Id, DeveloperToggleTextureBrowser);
            CommandList.Add(DeveloperToggleParticleBrowser.Id, DeveloperToggleParticleBrowser);
            CommandList.Add(GeneralToggleProjectionWin.Id, GeneralToggleProjectionWin);
            CommandList.Add(DeveloperToggleMicroprofiler.Id, DeveloperToggleMicroprofiler);
            CommandList.Add(DeveloperToggleVramProfiler.Id, DeveloperToggleVramProfiler);
            CommandList.Add(DeveloperTogglePluginAdmin.Id, DeveloperTogglePluginAdmin);
            CommandList.Add(OperationToggleSkyColorsWin.Id, OperationToggleSkyColorsWin);
            CommandList.Add(VRXpadHomeButton.Id, VRXpadHomeButton);
            CommandList.Add(VRToggle3DMouseCursor.Id, VRToggle3DMouseCursor);
            CommandList.Add(VRToggleVr.Id, VRToggleVr);
            CommandList.Add(VRGeneralResetView.Id, VRGeneralResetView);
            CommandList.Add(VRQuickZoomView.Id, VRQuickZoomView);
            CommandList.Add(VRReservedSelect.Id, VRReservedSelect);
            CommandList.Add(VRReservedMenu.Id, VRReservedMenu);
            CommandList.Add(VRReservedTouchpad.Id, VRReservedTouchpad);
        }

        private string ToRegularCase(string name)
        {
            return Regex.Replace(name, "(\\B[A-Z])", " $1").Replace("A P U", "APU").Replace("E C A", "ECA").Replace(" A ", " a ").Replace(" And ", " and ").Replace("G P S", "GPS").Replace("F M S", "FMS").Replace("Vr", "VR").Replace("V R ", "VR ").Replace("3 D", "3D").Replace("A D F", "ADF").Replace("G P U", "GPU").Replace("C W S A", "CWSA").Replace("C W S B", "CWSB").Replace("L T V V", "LTVV").Replace("Gps", "GPS").Replace("I L S", "ILS").Replace("T O G", "TOG").Replace("Gpu", "GPU").Replace("E F I S", "EFIS").Replace("Dc ", "DC ").Replace("D G", "DG").Replace("Obs", "OBS").Replace("Adf", "ADF").Replace("Re Arm", "Rearm");
        }

        public Dictionary<XPlaneCommands, XPlaneCommand> CommandList { get; private set; }
        private XPlaneCommand NoneNone { get { return new XPlaneCommand("sim/none/none", "Do nothing.", "None None", XPlaneCommands.NoneNone); } }
        private XPlaneCommand OperationQuit { get { return new XPlaneCommand("sim/operation/quit", "Quit X-Plane.", "Operation Quit", XPlaneCommands.OperationQuit); } }
        private XPlaneCommand OperationScreenshot { get { return new XPlaneCommand("sim/operation/screenshot", "Take a screenshot.", "Operation Screenshot", XPlaneCommands.OperationScreenshot); } }
        private XPlaneCommand OperationShowMenu { get { return new XPlaneCommand("sim/operation/show_menu", "Show the in-sim menu.", "Operation Show Menu", XPlaneCommands.OperationShowMenu); } }
        private XPlaneCommand OperationMakeCurrentAircraftIcons { get { return new XPlaneCommand("sim/operation/make_current_aircraft_icons", "(Re)generate all icons for the current aircraft.", "Operation Make Current Aircraft Icons", XPlaneCommands.OperationMakeCurrentAircraftIcons); } }
        private XPlaneCommand OperationMakeSingleIcon { get { return new XPlaneCommand("sim/operation/make_single_icon", "(Re)generate the icon for the current aircraft & livery.", "Operation Make Single Icon", XPlaneCommands.OperationMakeSingleIcon); } }
        private XPlaneCommand OperationMakeMissingIcons { get { return new XPlaneCommand("sim/operation/make_missing_icons", "Generate all missing aircraft & livery icons.", "Operation Make Missing Icons", XPlaneCommands.OperationMakeMissingIcons); } }
        private XPlaneCommand OperationRegenWeather { get { return new XPlaneCommand("sim/operation/regen_weather", "Regenerate weather.", "Operation Regen Weather", XPlaneCommands.OperationRegenWeather); } }
        private XPlaneCommand OperationCycleDump { get { return new XPlaneCommand("sim/operation/cycle_dump", "Print out a cycle dump for this frame.", "Operation Cycle Dump", XPlaneCommands.OperationCycleDump); } }
        private XPlaneCommand OperationStabDerivPitch { get { return new XPlaneCommand("sim/operation/stab_deriv_pitch", "Print longitudinal stability derivative.", "Operation Stab Deriv Pitch", XPlaneCommands.OperationStabDerivPitch); } }
        private XPlaneCommand OperationStabDerivHeading { get { return new XPlaneCommand("sim/operation/stab_deriv_heading", "Print lateral stability derivative.", "Operation Stab Deriv Heading", XPlaneCommands.OperationStabDerivHeading); } }
        private XPlaneCommand OperationRecording { get { return new XPlaneCommand("sim/operation/recording", "Toggle user interaction recording pane.", "Operation Recording", XPlaneCommands.OperationRecording); } }
        private XPlaneCommand OperationCreateSnapMarker { get { return new XPlaneCommand("sim/operation/create_snap_marker", "Drop a snapshot marker for replay mode.", "Operation Create Snap Marker", XPlaneCommands.OperationCreateSnapMarker); } }
        private XPlaneCommand OperationTestDataRef { get { return new XPlaneCommand("sim/operation/test_data_ref", "Test dataref: Run from 0 to 1 and back.", "Operation Test Data Ref", XPlaneCommands.OperationTestDataRef); } }
        private XPlaneCommand OperationShowFps { get { return new XPlaneCommand("sim/operation/show_fps", "Toggle on-screen frame-rate display.", "Operation Show Fps", XPlaneCommands.OperationShowFps); } }
        private XPlaneCommand OperationDevConsole { get { return new XPlaneCommand("sim/operation/dev_console", "Toggle dev console.", "Operation Dev Console", XPlaneCommands.OperationDevConsole); } }
        private XPlaneCommand OperationToggleFullScreen { get { return new XPlaneCommand("sim/operation/toggle_full_screen", "Toggle full-screen mode.", "Operation Toggle Full Screen", XPlaneCommands.OperationToggleFullScreen); } }
        private XPlaneCommand OperationReloadAircraft { get { return new XPlaneCommand("sim/operation/reload_aircraft", "Force reloading the current aircraft (including art).", "Operation Reload Aircraft", XPlaneCommands.OperationReloadAircraft); } }
        private XPlaneCommand OperationReloadAircraftNoArt { get { return new XPlaneCommand("sim/operation/reload_aircraft_no_art", "Force reloading the current aircraft (skip art reload).", "Operation Reload Aircraft No Art", XPlaneCommands.OperationReloadAircraftNoArt); } }
        private XPlaneCommand OperationReloadScenery { get { return new XPlaneCommand("sim/operation/reload_scenery", "Force reloading the current scenery.", "Operation Reload Scenery", XPlaneCommands.OperationReloadScenery); } }
        private XPlaneCommand OperationLoadRealWeather { get { return new XPlaneCommand("sim/operation/load_real_weather", "Scan real weather files.", "Operation Load Real Weather", XPlaneCommands.OperationLoadRealWeather); } }
        private XPlaneCommand OperationFailSystem { get { return new XPlaneCommand("sim/operation/fail_system", "Fail selected in failures screen.", "Operation Fail System", XPlaneCommands.OperationFailSystem); } }
        private XPlaneCommand OperationMakePanelPreviews { get { return new XPlaneCommand("sim/operation/make_panel_previews", "Make screenshots of your panels.", "Operation Make Panel Previews", XPlaneCommands.OperationMakePanelPreviews); } }
        private XPlaneCommand OperationCloseWindows { get { return new XPlaneCommand("sim/operation/close_windows", "Close any windows to get back to cockpit.", "Operation Close Windows", XPlaneCommands.OperationCloseWindows); } }
        private XPlaneCommand OperationLoadSituation1 { get { return new XPlaneCommand("sim/operation/load_situation_1", "Load situation.", "Operation Load Situation1", XPlaneCommands.OperationLoadSituation1); } }
        private XPlaneCommand OperationLoadSituation2 { get { return new XPlaneCommand("sim/operation/load_situation_2", "Load situation.", "Operation Load Situation2", XPlaneCommands.OperationLoadSituation2); } }
        private XPlaneCommand OperationLoadSituation3 { get { return new XPlaneCommand("sim/operation/load_situation_3", "Load situation.", "Operation Load Situation3", XPlaneCommands.OperationLoadSituation3); } }
        private XPlaneCommand ViewTrackIrToggle { get { return new XPlaneCommand("sim/view/track-ir_toggle", "Toggle TrackIR control.", "View Track Ir Toggle", XPlaneCommands.ViewTrackIrToggle); } }
        private XPlaneCommand MapShowCurrent { get { return new XPlaneCommand("sim/map/show_current", "Toggle the map window.", "Map Show Current", XPlaneCommands.MapShowCurrent); } }
        private XPlaneCommand MapShowInstructorOperatorStation { get { return new XPlaneCommand("sim/map/show_instructor_operator_station", "Toggle the instructor operator station (IOS) window.", "Map Show Instructor Operator Station", XPlaneCommands.MapShowInstructorOperatorStation); } }
        private XPlaneCommand MapShowLowEnroute { get { return new XPlaneCommand("sim/map/show_low_enroute", "Toggle the low enroute map.", "Map Show Low Enroute", XPlaneCommands.MapShowLowEnroute); } }
        private XPlaneCommand MapShowHighEnroute { get { return new XPlaneCommand("sim/map/show_high_enroute", "Toggle the high enroute map.", "Map Show High Enroute", XPlaneCommands.MapShowHighEnroute); } }
        private XPlaneCommand MapShowSectional { get { return new XPlaneCommand("sim/map/show_sectional", "Toggle the sectional map.", "Map Show Sectional", XPlaneCommands.MapShowSectional); } }
        private XPlaneCommand OperationToggleFlightConfig { get { return new XPlaneCommand("sim/operation/toggle_flight_config", "Toggle the Flight Configuration window.", "Operation Toggle Flight Config", XPlaneCommands.OperationToggleFlightConfig); } }
        private XPlaneCommand OperationToggleMainMenu { get { return new XPlaneCommand("sim/operation/toggle_main_menu", "Toggle the Main Menu screen.", "Operation Toggle Main Menu", XPlaneCommands.OperationToggleMainMenu); } }
        private XPlaneCommand OperationToggleSettingsWindow { get { return new XPlaneCommand("sim/operation/toggle_settings_window", "Toggle the Settings window.", "Operation Toggle Settings Window", XPlaneCommands.OperationToggleSettingsWindow); } }
        private XPlaneCommand OperationToggleFlightSchoolWindow { get { return new XPlaneCommand("sim/operation/toggle_flight_school_window", "Toggle the Flight School window.", "Operation Toggle Flight School Window", XPlaneCommands.OperationToggleFlightSchoolWindow); } }
        private XPlaneCommand OperationToggleKeyShortcutsWindow { get { return new XPlaneCommand("sim/operation/toggle_key_shortcuts_window", "Toggle the Keyboard Shortcuts window.", "Operation Toggle Key Shortcuts Window", XPlaneCommands.OperationToggleKeyShortcutsWindow); } }
        private XPlaneCommand OperationOpenWeightAndBalanceWindow { get { return new XPlaneCommand("sim/operation/open_weight_and_balance_window", "Open the Weight & Balance window.", "Operation Open Weight and Balance Window", XPlaneCommands.OperationOpenWeightAndBalanceWindow); } }
        private XPlaneCommand OperationOpenFailuresWindow { get { return new XPlaneCommand("sim/operation/open_failures_window", "Open the Failures window.", "Operation Open Failures Window", XPlaneCommands.OperationOpenFailuresWindow); } }
        private XPlaneCommand OperationToggleDataOutputGraph { get { return new XPlaneCommand("sim/operation/toggle_data_output_graph", "Toggle display of the data output graph.", "Operation Toggle Data Output Graph", XPlaneCommands.OperationToggleDataOutputGraph); } }
        private XPlaneCommand OperationToggleDataOutputCockpit { get { return new XPlaneCommand("sim/operation/toggle_data_output_cockpit", "Toggle display of the cockpit data output.", "Operation Toggle Data Output Cockpit", XPlaneCommands.OperationToggleDataOutputCockpit); } }
        private XPlaneCommand OperationToggleJoyProfilesWindow { get { return new XPlaneCommand("sim/operation/toggle_joy_profiles_window", "Toggle the keyboard & joystick profiles window.", "Operation Toggle Joy Profiles Window", XPlaneCommands.OperationToggleJoyProfilesWindow); } }
        private XPlaneCommand OperationToggleCustomLocationWindow { get { return new XPlaneCommand("sim/operation/toggle_custom_location_window", "Toggle the Location details window.", "Operation Toggle Custom Location Window", XPlaneCommands.OperationToggleCustomLocationWindow); } }
        private XPlaneCommand OperationToggleStyleGuide { get { return new XPlaneCommand("sim/operation/toggle_style_guide", "Toggle display of the V11 UI style guide.", "Operation Toggle Style Guide", XPlaneCommands.OperationToggleStyleGuide); } }
        private XPlaneCommand OperationSlider01 { get { return new XPlaneCommand("sim/operation/slider_01", "Slider #1 On/Off control.", "Operation Slider01", XPlaneCommands.OperationSlider01); } }
        private XPlaneCommand OperationSlider02 { get { return new XPlaneCommand("sim/operation/slider_02", "Slider #2 On/Off control.", "Operation Slider02", XPlaneCommands.OperationSlider02); } }
        private XPlaneCommand OperationSlider03 { get { return new XPlaneCommand("sim/operation/slider_03", "Slider #3 On/Off control.", "Operation Slider03", XPlaneCommands.OperationSlider03); } }
        private XPlaneCommand OperationSlider04 { get { return new XPlaneCommand("sim/operation/slider_04", "Slider #4 On/Off control.", "Operation Slider04", XPlaneCommands.OperationSlider04); } }
        private XPlaneCommand OperationSlider05 { get { return new XPlaneCommand("sim/operation/slider_05", "Slider #5 On/Off control.", "Operation Slider05", XPlaneCommands.OperationSlider05); } }
        private XPlaneCommand OperationSlider06 { get { return new XPlaneCommand("sim/operation/slider_06", "Slider #6 On/Off control.", "Operation Slider06", XPlaneCommands.OperationSlider06); } }
        private XPlaneCommand OperationSlider07 { get { return new XPlaneCommand("sim/operation/slider_07", "Slider #7 On/Off control.", "Operation Slider07", XPlaneCommands.OperationSlider07); } }
        private XPlaneCommand OperationSlider08 { get { return new XPlaneCommand("sim/operation/slider_08", "Slider #8 On/Off control.", "Operation Slider08", XPlaneCommands.OperationSlider08); } }
        private XPlaneCommand OperationSlider09 { get { return new XPlaneCommand("sim/operation/slider_09", "Slider #9 On/Off control.", "Operation Slider09", XPlaneCommands.OperationSlider09); } }
        private XPlaneCommand OperationSlider10 { get { return new XPlaneCommand("sim/operation/slider_10", "Slider #10 On/Off control.", "Operation Slider10", XPlaneCommands.OperationSlider10); } }
        private XPlaneCommand OperationSlider11 { get { return new XPlaneCommand("sim/operation/slider_11", "Slider #11 On/Off control.", "Operation Slider11", XPlaneCommands.OperationSlider11); } }
        private XPlaneCommand OperationSlider12 { get { return new XPlaneCommand("sim/operation/slider_12", "Slider #12 On/Off control.", "Operation Slider12", XPlaneCommands.OperationSlider12); } }
        private XPlaneCommand OperationSlider13 { get { return new XPlaneCommand("sim/operation/slider_13", "Slider #13 On/Off control.", "Operation Slider13", XPlaneCommands.OperationSlider13); } }
        private XPlaneCommand OperationSlider14 { get { return new XPlaneCommand("sim/operation/slider_14", "Slider #14 On/Off control.", "Operation Slider14", XPlaneCommands.OperationSlider14); } }
        private XPlaneCommand OperationSlider15 { get { return new XPlaneCommand("sim/operation/slider_15", "Slider #15 On/Off control.", "Operation Slider15", XPlaneCommands.OperationSlider15); } }
        private XPlaneCommand OperationSlider16 { get { return new XPlaneCommand("sim/operation/slider_16", "Slider #16 On/Off control.", "Operation Slider16", XPlaneCommands.OperationSlider16); } }
        private XPlaneCommand OperationSlider17 { get { return new XPlaneCommand("sim/operation/slider_17", "Slider #17 On/Off control.", "Operation Slider17", XPlaneCommands.OperationSlider17); } }
        private XPlaneCommand OperationSlider18 { get { return new XPlaneCommand("sim/operation/slider_18", "Slider #18 On/Off control.", "Operation Slider18", XPlaneCommands.OperationSlider18); } }
        private XPlaneCommand OperationSlider19 { get { return new XPlaneCommand("sim/operation/slider_19", "Slider #19 On/Off control.", "Operation Slider19", XPlaneCommands.OperationSlider19); } }
        private XPlaneCommand OperationSlider20 { get { return new XPlaneCommand("sim/operation/slider_20", "Slider #20 On/Off control.", "Operation Slider20", XPlaneCommands.OperationSlider20); } }
        private XPlaneCommand OperationSlider21 { get { return new XPlaneCommand("sim/operation/slider_21", "Slider #21 On/Off control.", "Operation Slider21", XPlaneCommands.OperationSlider21); } }
        private XPlaneCommand OperationSlider22 { get { return new XPlaneCommand("sim/operation/slider_22", "Slider #22 On/Off control.", "Operation Slider22", XPlaneCommands.OperationSlider22); } }
        private XPlaneCommand OperationSlider23 { get { return new XPlaneCommand("sim/operation/slider_23", "Slider #23 On/Off control.", "Operation Slider23", XPlaneCommands.OperationSlider23); } }
        private XPlaneCommand OperationSlider24 { get { return new XPlaneCommand("sim/operation/slider_24", "Slider #24 On/Off control.", "Operation Slider24", XPlaneCommands.OperationSlider24); } }
        private XPlaneCommand OperationFixAllSystems { get { return new XPlaneCommand("sim/operation/fix_all_systems", "Fix all failed systems.", "Operation Fix All Systems", XPlaneCommands.OperationFixAllSystems); } }
        private XPlaneCommand OperationAutoBoard { get { return new XPlaneCommand("sim/operation/auto_board", "Auto-set electrical system for boarding.", "Operation Auto Board", XPlaneCommands.OperationAutoBoard); } }
        private XPlaneCommand OperationAutoStart { get { return new XPlaneCommand("sim/operation/auto_start", "Auto-start engines to running, real-time.", "Operation Auto Start", XPlaneCommands.OperationAutoStart); } }
        private XPlaneCommand OperationQuickStart { get { return new XPlaneCommand("sim/operation/quick_start", "Quick-start engines to running.", "Operation Quick Start", XPlaneCommands.OperationQuickStart); } }
        private XPlaneCommand MagnetosMagnetosOff { get { return new XPlaneCommand("sim/magnetos/magnetos_off", "Magnetos off.", "Magnetos Magnetos Off", XPlaneCommands.MagnetosMagnetosOff); } }
        private XPlaneCommand MagnetosMagnetosBoth { get { return new XPlaneCommand("sim/magnetos/magnetos_both", "Magnetos both.", "Magnetos Magnetos Both", XPlaneCommands.MagnetosMagnetosBoth); } }
        private XPlaneCommand EnginesEngageStarters { get { return new XPlaneCommand("sim/engines/engage_starters", "Engage starters.", "Engines Engage Starters", XPlaneCommands.EnginesEngageStarters); } }
        private XPlaneCommand EnginesThrottleDown { get { return new XPlaneCommand("sim/engines/throttle_down", "Throttle down a bit.", "Engines Throttle Down", XPlaneCommands.EnginesThrottleDown); } }
        private XPlaneCommand EnginesThrottleUp { get { return new XPlaneCommand("sim/engines/throttle_up", "Throttle up a bit.", "Engines Throttle Up", XPlaneCommands.EnginesThrottleUp); } }
        private XPlaneCommand EnginesTOGAPower { get { return new XPlaneCommand("sim/engines/TOGA_power", "Engage TOGA power.", "Engines TOGA Power", XPlaneCommands.EnginesTOGAPower); } }
        private XPlaneCommand EnginesPropDown { get { return new XPlaneCommand("sim/engines/prop_down", "Prop coarse a bit.", "Engines Prop Down", XPlaneCommands.EnginesPropDown); } }
        private XPlaneCommand EnginesPropUp { get { return new XPlaneCommand("sim/engines/prop_up", "Prop fine a bit.", "Engines Prop Up", XPlaneCommands.EnginesPropUp); } }
        private XPlaneCommand EnginesMixtureMin { get { return new XPlaneCommand("sim/engines/mixture_min", "Mixture to cut off.", "Engines Mixture Min", XPlaneCommands.EnginesMixtureMin); } }
        private XPlaneCommand EnginesMixtureDown { get { return new XPlaneCommand("sim/engines/mixture_down", "Mixture lean a bit.", "Engines Mixture Down", XPlaneCommands.EnginesMixtureDown); } }
        private XPlaneCommand EnginesMixtureUp { get { return new XPlaneCommand("sim/engines/mixture_up", "Mixture rich a bit.", "Engines Mixture Up", XPlaneCommands.EnginesMixtureUp); } }
        private XPlaneCommand EnginesMixtureMax { get { return new XPlaneCommand("sim/engines/mixture_max", "Mixture to full rich.", "Engines Mixture Max", XPlaneCommands.EnginesMixtureMax); } }
        private XPlaneCommand EnginesCarbHeatOff { get { return new XPlaneCommand("sim/engines/carb_heat_off", "Carb heat off.", "Engines Carb Heat Off", XPlaneCommands.EnginesCarbHeatOff); } }
        private XPlaneCommand EnginesCarbHeatOn { get { return new XPlaneCommand("sim/engines/carb_heat_on", "Carb heat on.", "Engines Carb Heat On", XPlaneCommands.EnginesCarbHeatOn); } }
        private XPlaneCommand EnginesCarbHeatToggle { get { return new XPlaneCommand("sim/engines/carb_heat_toggle", "Carb heat toggle.", "Engines Carb Heat Toggle", XPlaneCommands.EnginesCarbHeatToggle); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open", "Move cowl flaps open a bit.", "Flight Controls Cowl Flaps Open", XPlaneCommands.FlightControlsCowlFlapsOpen); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed", "Move cowl flaps to closed a bit.", "Flight Controls Cowl Flaps Closed", XPlaneCommands.FlightControlsCowlFlapsClosed); } }
        private XPlaneCommand EnginesIdleHiLoToggle { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle", "Idle high/low toggle.", "Engines Idle Hi Lo Toggle", XPlaneCommands.EnginesIdleHiLoToggle); } }
        private XPlaneCommand EnginesIdleHiLoToggle1 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_1", "Idle high/low toggle Engine 1.", "Engines Idle Hi Lo Toggle1", XPlaneCommands.EnginesIdleHiLoToggle1); } }
        private XPlaneCommand EnginesIdleHiLoToggle2 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_2", "Idle high/low toggle Engine 2.", "Engines Idle Hi Lo Toggle2", XPlaneCommands.EnginesIdleHiLoToggle2); } }
        private XPlaneCommand EnginesIdleHiLoToggle3 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_3", "Idle high/low toggle Engine 3.", "Engines Idle Hi Lo Toggle3", XPlaneCommands.EnginesIdleHiLoToggle3); } }
        private XPlaneCommand EnginesIdleHiLoToggle4 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_4", "Idle high/low toggle Engine 4.", "Engines Idle Hi Lo Toggle4", XPlaneCommands.EnginesIdleHiLoToggle4); } }
        private XPlaneCommand EnginesIdleHiLoToggle5 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_5", "Idle high/low toggle Engine 5.", "Engines Idle Hi Lo Toggle5", XPlaneCommands.EnginesIdleHiLoToggle5); } }
        private XPlaneCommand EnginesIdleHiLoToggle6 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_6", "Idle high/low toggle Engine 6.", "Engines Idle Hi Lo Toggle6", XPlaneCommands.EnginesIdleHiLoToggle6); } }
        private XPlaneCommand EnginesIdleHiLoToggle7 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_7", "Idle high/low toggle Engine 7.", "Engines Idle Hi Lo Toggle7", XPlaneCommands.EnginesIdleHiLoToggle7); } }
        private XPlaneCommand EnginesIdleHiLoToggle8 { get { return new XPlaneCommand("sim/engines/idle_hi_lo_toggle_8", "Idle high/low toggle Engine 8.", "Engines Idle Hi Lo Toggle8", XPlaneCommands.EnginesIdleHiLoToggle8); } }
        private XPlaneCommand FadecFadecToggle { get { return new XPlaneCommand("sim/fadec/fadec_toggle", "FADEC toggle.", "Fadec Fadec Toggle", XPlaneCommands.FadecFadecToggle); } }
        private XPlaneCommand EnginesGovernorOn { get { return new XPlaneCommand("sim/engines/governor_on", "Throttle governor on.", "Engines Governor On", XPlaneCommands.EnginesGovernorOn); } }
        private XPlaneCommand EnginesGovernorOff { get { return new XPlaneCommand("sim/engines/governor_off", "Throttle governor off.", "Engines Governor Off", XPlaneCommands.EnginesGovernorOff); } }
        private XPlaneCommand EnginesGovernorToggle { get { return new XPlaneCommand("sim/engines/governor_toggle", "Throttle governor toggle.", "Engines Governor Toggle", XPlaneCommands.EnginesGovernorToggle); } }
        private XPlaneCommand EnginesClutchOn { get { return new XPlaneCommand("sim/engines/clutch_on", "Clutch on.", "Engines Clutch On", XPlaneCommands.EnginesClutchOn); } }
        private XPlaneCommand EnginesClutchOff { get { return new XPlaneCommand("sim/engines/clutch_off", "Clutch off.", "Engines Clutch Off", XPlaneCommands.EnginesClutchOff); } }
        private XPlaneCommand EnginesClutchToggle { get { return new XPlaneCommand("sim/engines/clutch_toggle", "Clutch toggle.", "Engines Clutch Toggle", XPlaneCommands.EnginesClutchToggle); } }
        private XPlaneCommand EnginesBetaToggle { get { return new XPlaneCommand("sim/engines/beta_toggle", "Toggle Beta prop.", "Engines Beta Toggle", XPlaneCommands.EnginesBetaToggle); } }
        private XPlaneCommand EnginesThrustReverseToggle { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle", "Toggle thrust reversers.", "Engines Thrust Reverse Toggle", XPlaneCommands.EnginesThrustReverseToggle); } }
        private XPlaneCommand EnginesThrustReverseHold { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold", "Hold thrust reverse at max.", "Engines Thrust Reverse Hold", XPlaneCommands.EnginesThrustReverseHold); } }
        private XPlaneCommand StartersShutDown { get { return new XPlaneCommand("sim/starters/shut_down", "Pull fuel and mags for shutdown.", "Starters Shut Down", XPlaneCommands.StartersShutDown); } }
        private XPlaneCommand MagnetosMagnetosDown1 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_1", "Magnetos down one notch for engine #1.", "Magnetos Magnetos Down1", XPlaneCommands.MagnetosMagnetosDown1); } }
        private XPlaneCommand MagnetosMagnetosDown2 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_2", "Magnetos down one notch for engine #2.", "Magnetos Magnetos Down2", XPlaneCommands.MagnetosMagnetosDown2); } }
        private XPlaneCommand MagnetosMagnetosDown3 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_3", "Magnetos down one notch for engine #3.", "Magnetos Magnetos Down3", XPlaneCommands.MagnetosMagnetosDown3); } }
        private XPlaneCommand MagnetosMagnetosDown4 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_4", "Magnetos down one notch for engine #4.", "Magnetos Magnetos Down4", XPlaneCommands.MagnetosMagnetosDown4); } }
        private XPlaneCommand MagnetosMagnetosDown5 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_5", "Magnetos down one notch for engine #5.", "Magnetos Magnetos Down5", XPlaneCommands.MagnetosMagnetosDown5); } }
        private XPlaneCommand MagnetosMagnetosDown6 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_6", "Magnetos down one notch for engine #6.", "Magnetos Magnetos Down6", XPlaneCommands.MagnetosMagnetosDown6); } }
        private XPlaneCommand MagnetosMagnetosDown7 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_7", "Magnetos down one notch for engine #7.", "Magnetos Magnetos Down7", XPlaneCommands.MagnetosMagnetosDown7); } }
        private XPlaneCommand MagnetosMagnetosDown8 { get { return new XPlaneCommand("sim/magnetos/magnetos_down_8", "Magnetos down one notch for engine #8.", "Magnetos Magnetos Down8", XPlaneCommands.MagnetosMagnetosDown8); } }
        private XPlaneCommand MagnetosMagnetosUp1 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_1", "Magnetos up one notch for engine #1.", "Magnetos Magnetos Up1", XPlaneCommands.MagnetosMagnetosUp1); } }
        private XPlaneCommand MagnetosMagnetosUp2 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_2", "Magnetos up one notch for engine #2.", "Magnetos Magnetos Up2", XPlaneCommands.MagnetosMagnetosUp2); } }
        private XPlaneCommand MagnetosMagnetosUp3 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_3", "Magnetos up one notch for engine #3.", "Magnetos Magnetos Up3", XPlaneCommands.MagnetosMagnetosUp3); } }
        private XPlaneCommand MagnetosMagnetosUp4 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_4", "Magnetos up one notch for engine #4.", "Magnetos Magnetos Up4", XPlaneCommands.MagnetosMagnetosUp4); } }
        private XPlaneCommand MagnetosMagnetosUp5 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_5", "Magnetos up one notch for engine #5.", "Magnetos Magnetos Up5", XPlaneCommands.MagnetosMagnetosUp5); } }
        private XPlaneCommand MagnetosMagnetosUp6 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_6", "Magnetos up one notch for engine #6.", "Magnetos Magnetos Up6", XPlaneCommands.MagnetosMagnetosUp6); } }
        private XPlaneCommand MagnetosMagnetosUp7 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_7", "Magnetos up one notch for engine #7.", "Magnetos Magnetos Up7", XPlaneCommands.MagnetosMagnetosUp7); } }
        private XPlaneCommand MagnetosMagnetosUp8 { get { return new XPlaneCommand("sim/magnetos/magnetos_up_8", "Magnetos up one notch for engine #8.", "Magnetos Magnetos Up8", XPlaneCommands.MagnetosMagnetosUp8); } }
        private XPlaneCommand MagnetosMagnetosOff1 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_1", "Magnetos off for engine #1.", "Magnetos Magnetos Off1", XPlaneCommands.MagnetosMagnetosOff1); } }
        private XPlaneCommand MagnetosMagnetosOff2 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_2", "Magnetos off for engine #2.", "Magnetos Magnetos Off2", XPlaneCommands.MagnetosMagnetosOff2); } }
        private XPlaneCommand MagnetosMagnetosOff3 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_3", "Magnetos off for engine #3.", "Magnetos Magnetos Off3", XPlaneCommands.MagnetosMagnetosOff3); } }
        private XPlaneCommand MagnetosMagnetosOff4 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_4", "Magnetos off for engine #4.", "Magnetos Magnetos Off4", XPlaneCommands.MagnetosMagnetosOff4); } }
        private XPlaneCommand MagnetosMagnetosOff5 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_5", "Magnetos off for engine #5.", "Magnetos Magnetos Off5", XPlaneCommands.MagnetosMagnetosOff5); } }
        private XPlaneCommand MagnetosMagnetosOff6 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_6", "Magnetos off for engine #6.", "Magnetos Magnetos Off6", XPlaneCommands.MagnetosMagnetosOff6); } }
        private XPlaneCommand MagnetosMagnetosOff7 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_7", "Magnetos off for engine #7.", "Magnetos Magnetos Off7", XPlaneCommands.MagnetosMagnetosOff7); } }
        private XPlaneCommand MagnetosMagnetosOff8 { get { return new XPlaneCommand("sim/magnetos/magnetos_off_8", "Magnetos off for engine #8.", "Magnetos Magnetos Off8", XPlaneCommands.MagnetosMagnetosOff8); } }
        private XPlaneCommand MagnetosMagnetosLeft1 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_1", "Magnetos left for engine #1.", "Magnetos Magnetos Left1", XPlaneCommands.MagnetosMagnetosLeft1); } }
        private XPlaneCommand MagnetosMagnetosLeft2 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_2", "Magnetos left for engine #2.", "Magnetos Magnetos Left2", XPlaneCommands.MagnetosMagnetosLeft2); } }
        private XPlaneCommand MagnetosMagnetosLeft3 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_3", "Magnetos left for engine #3.", "Magnetos Magnetos Left3", XPlaneCommands.MagnetosMagnetosLeft3); } }
        private XPlaneCommand MagnetosMagnetosLeft4 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_4", "Magnetos left for engine #4.", "Magnetos Magnetos Left4", XPlaneCommands.MagnetosMagnetosLeft4); } }
        private XPlaneCommand MagnetosMagnetosLeft5 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_5", "Magnetos left for engine #5.", "Magnetos Magnetos Left5", XPlaneCommands.MagnetosMagnetosLeft5); } }
        private XPlaneCommand MagnetosMagnetosLeft6 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_6", "Magnetos left for engine #6.", "Magnetos Magnetos Left6", XPlaneCommands.MagnetosMagnetosLeft6); } }
        private XPlaneCommand MagnetosMagnetosLeft7 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_7", "Magnetos left for engine #7.", "Magnetos Magnetos Left7", XPlaneCommands.MagnetosMagnetosLeft7); } }
        private XPlaneCommand MagnetosMagnetosLeft8 { get { return new XPlaneCommand("sim/magnetos/magnetos_left_8", "Magnetos left for engine #8.", "Magnetos Magnetos Left8", XPlaneCommands.MagnetosMagnetosLeft8); } }
        private XPlaneCommand MagnetosMagnetosRight1 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_1", "Magnetos right for engine #1.", "Magnetos Magnetos Right1", XPlaneCommands.MagnetosMagnetosRight1); } }
        private XPlaneCommand MagnetosMagnetosRight2 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_2", "Magnetos right for engine #2.", "Magnetos Magnetos Right2", XPlaneCommands.MagnetosMagnetosRight2); } }
        private XPlaneCommand MagnetosMagnetosRight3 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_3", "Magnetos right for engine #3.", "Magnetos Magnetos Right3", XPlaneCommands.MagnetosMagnetosRight3); } }
        private XPlaneCommand MagnetosMagnetosRight4 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_4", "Magnetos right for engine #4.", "Magnetos Magnetos Right4", XPlaneCommands.MagnetosMagnetosRight4); } }
        private XPlaneCommand MagnetosMagnetosRight5 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_5", "Magnetos right for engine #5.", "Magnetos Magnetos Right5", XPlaneCommands.MagnetosMagnetosRight5); } }
        private XPlaneCommand MagnetosMagnetosRight6 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_6", "Magnetos right for engine #6.", "Magnetos Magnetos Right6", XPlaneCommands.MagnetosMagnetosRight6); } }
        private XPlaneCommand MagnetosMagnetosRight7 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_7", "Magnetos right for engine #7.", "Magnetos Magnetos Right7", XPlaneCommands.MagnetosMagnetosRight7); } }
        private XPlaneCommand MagnetosMagnetosRight8 { get { return new XPlaneCommand("sim/magnetos/magnetos_right_8", "Magnetos right for engine #8.", "Magnetos Magnetos Right8", XPlaneCommands.MagnetosMagnetosRight8); } }
        private XPlaneCommand MagnetosMagnetosBoth1 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_1", "Magnetos both for engine #1.", "Magnetos Magnetos Both1", XPlaneCommands.MagnetosMagnetosBoth1); } }
        private XPlaneCommand MagnetosMagnetosBoth2 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_2", "Magnetos both for engine #2.", "Magnetos Magnetos Both2", XPlaneCommands.MagnetosMagnetosBoth2); } }
        private XPlaneCommand MagnetosMagnetosBoth3 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_3", "Magnetos both for engine #3.", "Magnetos Magnetos Both3", XPlaneCommands.MagnetosMagnetosBoth3); } }
        private XPlaneCommand MagnetosMagnetosBoth4 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_4", "Magnetos both for engine #4.", "Magnetos Magnetos Both4", XPlaneCommands.MagnetosMagnetosBoth4); } }
        private XPlaneCommand MagnetosMagnetosBoth5 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_5", "Magnetos both for engine #5.", "Magnetos Magnetos Both5", XPlaneCommands.MagnetosMagnetosBoth5); } }
        private XPlaneCommand MagnetosMagnetosBoth6 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_6", "Magnetos both for engine #6.", "Magnetos Magnetos Both6", XPlaneCommands.MagnetosMagnetosBoth6); } }
        private XPlaneCommand MagnetosMagnetosBoth7 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_7", "Magnetos both for engine #7.", "Magnetos Magnetos Both7", XPlaneCommands.MagnetosMagnetosBoth7); } }
        private XPlaneCommand MagnetosMagnetosBoth8 { get { return new XPlaneCommand("sim/magnetos/magnetos_both_8", "Magnetos both for engine #8.", "Magnetos Magnetos Both8", XPlaneCommands.MagnetosMagnetosBoth8); } }
        private XPlaneCommand IgnitionIgnitionDown1 { get { return new XPlaneCommand("sim/ignition/ignition_down_1", "Ignition key down one notch for engine #1.", "Ignition Ignition Down1", XPlaneCommands.IgnitionIgnitionDown1); } }
        private XPlaneCommand IgnitionIgnitionDown2 { get { return new XPlaneCommand("sim/ignition/ignition_down_2", "Ignition key down one notch for engine #2.", "Ignition Ignition Down2", XPlaneCommands.IgnitionIgnitionDown2); } }
        private XPlaneCommand IgnitionIgnitionDown3 { get { return new XPlaneCommand("sim/ignition/ignition_down_3", "Ignition key down one notch for engine #3.", "Ignition Ignition Down3", XPlaneCommands.IgnitionIgnitionDown3); } }
        private XPlaneCommand IgnitionIgnitionDown4 { get { return new XPlaneCommand("sim/ignition/ignition_down_4", "Ignition key down one notch for engine #4.", "Ignition Ignition Down4", XPlaneCommands.IgnitionIgnitionDown4); } }
        private XPlaneCommand IgnitionIgnitionDown5 { get { return new XPlaneCommand("sim/ignition/ignition_down_5", "Ignition key down one notch for engine #5.", "Ignition Ignition Down5", XPlaneCommands.IgnitionIgnitionDown5); } }
        private XPlaneCommand IgnitionIgnitionDown6 { get { return new XPlaneCommand("sim/ignition/ignition_down_6", "Ignition key down one notch for engine #6.", "Ignition Ignition Down6", XPlaneCommands.IgnitionIgnitionDown6); } }
        private XPlaneCommand IgnitionIgnitionDown7 { get { return new XPlaneCommand("sim/ignition/ignition_down_7", "Ignition key down one notch for engine #7.", "Ignition Ignition Down7", XPlaneCommands.IgnitionIgnitionDown7); } }
        private XPlaneCommand IgnitionIgnitionDown8 { get { return new XPlaneCommand("sim/ignition/ignition_down_8", "Ignition key down one notch for engine #8.", "Ignition Ignition Down8", XPlaneCommands.IgnitionIgnitionDown8); } }
        private XPlaneCommand IgnitionIgnitionUp1 { get { return new XPlaneCommand("sim/ignition/ignition_up_1", "Ignition key up one notch for engine #1.", "Ignition Ignition Up1", XPlaneCommands.IgnitionIgnitionUp1); } }
        private XPlaneCommand IgnitionIgnitionUp2 { get { return new XPlaneCommand("sim/ignition/ignition_up_2", "Ignition key up one notch for engine #2.", "Ignition Ignition Up2", XPlaneCommands.IgnitionIgnitionUp2); } }
        private XPlaneCommand IgnitionIgnitionUp3 { get { return new XPlaneCommand("sim/ignition/ignition_up_3", "Ignition key up one notch for engine #3.", "Ignition Ignition Up3", XPlaneCommands.IgnitionIgnitionUp3); } }
        private XPlaneCommand IgnitionIgnitionUp4 { get { return new XPlaneCommand("sim/ignition/ignition_up_4", "Ignition key up one notch for engine #4.", "Ignition Ignition Up4", XPlaneCommands.IgnitionIgnitionUp4); } }
        private XPlaneCommand IgnitionIgnitionUp5 { get { return new XPlaneCommand("sim/ignition/ignition_up_5", "Ignition key up one notch for engine #5.", "Ignition Ignition Up5", XPlaneCommands.IgnitionIgnitionUp5); } }
        private XPlaneCommand IgnitionIgnitionUp6 { get { return new XPlaneCommand("sim/ignition/ignition_up_6", "Ignition key up one notch for engine #6.", "Ignition Ignition Up6", XPlaneCommands.IgnitionIgnitionUp6); } }
        private XPlaneCommand IgnitionIgnitionUp7 { get { return new XPlaneCommand("sim/ignition/ignition_up_7", "Ignition key up one notch for engine #7.", "Ignition Ignition Up7", XPlaneCommands.IgnitionIgnitionUp7); } }
        private XPlaneCommand IgnitionIgnitionUp8 { get { return new XPlaneCommand("sim/ignition/ignition_up_8", "Ignition key up one notch for engine #8.", "Ignition Ignition Up8", XPlaneCommands.IgnitionIgnitionUp8); } }
        private XPlaneCommand IgnitersIgniterArmOff1 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_1", "Igniter #1 arm off.", "Igniters Igniter Arm Off1", XPlaneCommands.IgnitersIgniterArmOff1); } }
        private XPlaneCommand IgnitersIgniterArmOff2 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_2", "Igniter #2 arm off.", "Igniters Igniter Arm Off2", XPlaneCommands.IgnitersIgniterArmOff2); } }
        private XPlaneCommand IgnitersIgniterArmOff3 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_3", "Igniter #3 arm off.", "Igniters Igniter Arm Off3", XPlaneCommands.IgnitersIgniterArmOff3); } }
        private XPlaneCommand IgnitersIgniterArmOff4 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_4", "Igniter #4 arm off.", "Igniters Igniter Arm Off4", XPlaneCommands.IgnitersIgniterArmOff4); } }
        private XPlaneCommand IgnitersIgniterArmOff5 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_5", "Igniter #5 arm off.", "Igniters Igniter Arm Off5", XPlaneCommands.IgnitersIgniterArmOff5); } }
        private XPlaneCommand IgnitersIgniterArmOff6 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_6", "Igniter #6 arm off.", "Igniters Igniter Arm Off6", XPlaneCommands.IgnitersIgniterArmOff6); } }
        private XPlaneCommand IgnitersIgniterArmOff7 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_7", "Igniter #7 arm off.", "Igniters Igniter Arm Off7", XPlaneCommands.IgnitersIgniterArmOff7); } }
        private XPlaneCommand IgnitersIgniterArmOff8 { get { return new XPlaneCommand("sim/igniters/igniter_arm_off_8", "Igniter #8 arm off.", "Igniters Igniter Arm Off8", XPlaneCommands.IgnitersIgniterArmOff8); } }
        private XPlaneCommand IgnitersIgniterArmOn1 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_1", "Igniter #1 arm on.", "Igniters Igniter Arm On1", XPlaneCommands.IgnitersIgniterArmOn1); } }
        private XPlaneCommand IgnitersIgniterArmOn2 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_2", "Igniter #2 arm on.", "Igniters Igniter Arm On2", XPlaneCommands.IgnitersIgniterArmOn2); } }
        private XPlaneCommand IgnitersIgniterArmOn3 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_3", "Igniter #3 arm on.", "Igniters Igniter Arm On3", XPlaneCommands.IgnitersIgniterArmOn3); } }
        private XPlaneCommand IgnitersIgniterArmOn4 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_4", "Igniter #4 arm on.", "Igniters Igniter Arm On4", XPlaneCommands.IgnitersIgniterArmOn4); } }
        private XPlaneCommand IgnitersIgniterArmOn5 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_5", "Igniter #5 arm on.", "Igniters Igniter Arm On5", XPlaneCommands.IgnitersIgniterArmOn5); } }
        private XPlaneCommand IgnitersIgniterArmOn6 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_6", "Igniter #6 arm on.", "Igniters Igniter Arm On6", XPlaneCommands.IgnitersIgniterArmOn6); } }
        private XPlaneCommand IgnitersIgniterArmOn7 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_7", "Igniter #7 arm on.", "Igniters Igniter Arm On7", XPlaneCommands.IgnitersIgniterArmOn7); } }
        private XPlaneCommand IgnitersIgniterArmOn8 { get { return new XPlaneCommand("sim/igniters/igniter_arm_on_8", "Igniter #8 arm on.", "Igniters Igniter Arm On8", XPlaneCommands.IgnitersIgniterArmOn8); } }
        private XPlaneCommand IgnitersIgniterContinOff1 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_1", "Igniter #1 contin-ignition off.", "Igniters Igniter Contin Off1", XPlaneCommands.IgnitersIgniterContinOff1); } }
        private XPlaneCommand IgnitersIgniterContinOff2 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_2", "Igniter #2 contin-ignition off.", "Igniters Igniter Contin Off2", XPlaneCommands.IgnitersIgniterContinOff2); } }
        private XPlaneCommand IgnitersIgniterContinOff3 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_3", "Igniter #3 contin-ignition off.", "Igniters Igniter Contin Off3", XPlaneCommands.IgnitersIgniterContinOff3); } }
        private XPlaneCommand IgnitersIgniterContinOff4 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_4", "Igniter #4 contin-ignition off.", "Igniters Igniter Contin Off4", XPlaneCommands.IgnitersIgniterContinOff4); } }
        private XPlaneCommand IgnitersIgniterContinOff5 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_5", "Igniter #5 contin-ignition off.", "Igniters Igniter Contin Off5", XPlaneCommands.IgnitersIgniterContinOff5); } }
        private XPlaneCommand IgnitersIgniterContinOff6 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_6", "Igniter #6 contin-ignition off.", "Igniters Igniter Contin Off6", XPlaneCommands.IgnitersIgniterContinOff6); } }
        private XPlaneCommand IgnitersIgniterContinOff7 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_7", "Igniter #7 contin-ignition off.", "Igniters Igniter Contin Off7", XPlaneCommands.IgnitersIgniterContinOff7); } }
        private XPlaneCommand IgnitersIgniterContinOff8 { get { return new XPlaneCommand("sim/igniters/igniter_contin_off_8", "Igniter #8 contin-ignition off.", "Igniters Igniter Contin Off8", XPlaneCommands.IgnitersIgniterContinOff8); } }
        private XPlaneCommand IgnitersIgniterContinOn1 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_1", "Igniter #1 contin-ignition on.", "Igniters Igniter Contin On1", XPlaneCommands.IgnitersIgniterContinOn1); } }
        private XPlaneCommand IgnitersIgniterContinOn2 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_2", "Igniter #2 contin-ignition on.", "Igniters Igniter Contin On2", XPlaneCommands.IgnitersIgniterContinOn2); } }
        private XPlaneCommand IgnitersIgniterContinOn3 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_3", "Igniter #3 contin-ignition on.", "Igniters Igniter Contin On3", XPlaneCommands.IgnitersIgniterContinOn3); } }
        private XPlaneCommand IgnitersIgniterContinOn4 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_4", "Igniter #4 contin-ignition on.", "Igniters Igniter Contin On4", XPlaneCommands.IgnitersIgniterContinOn4); } }
        private XPlaneCommand IgnitersIgniterContinOn5 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_5", "Igniter #5 contin-ignition on.", "Igniters Igniter Contin On5", XPlaneCommands.IgnitersIgniterContinOn5); } }
        private XPlaneCommand IgnitersIgniterContinOn6 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_6", "Igniter #6 contin-ignition on.", "Igniters Igniter Contin On6", XPlaneCommands.IgnitersIgniterContinOn6); } }
        private XPlaneCommand IgnitersIgniterContinOn7 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_7", "Igniter #7 contin-ignition on.", "Igniters Igniter Contin On7", XPlaneCommands.IgnitersIgniterContinOn7); } }
        private XPlaneCommand IgnitersIgniterContinOn8 { get { return new XPlaneCommand("sim/igniters/igniter_contin_on_8", "Igniter #8 contin-ignition on.", "Igniters Igniter Contin On8", XPlaneCommands.IgnitersIgniterContinOn8); } }
        private XPlaneCommand StartersEngageStarter1 { get { return new XPlaneCommand("sim/starters/engage_starter_1", "Engage starter #1.", "Starters Engage Starter1", XPlaneCommands.StartersEngageStarter1); } }
        private XPlaneCommand StartersEngageStarter2 { get { return new XPlaneCommand("sim/starters/engage_starter_2", "Engage starter #2.", "Starters Engage Starter2", XPlaneCommands.StartersEngageStarter2); } }
        private XPlaneCommand StartersEngageStarter3 { get { return new XPlaneCommand("sim/starters/engage_starter_3", "Engage starter #3.", "Starters Engage Starter3", XPlaneCommands.StartersEngageStarter3); } }
        private XPlaneCommand StartersEngageStarter4 { get { return new XPlaneCommand("sim/starters/engage_starter_4", "Engage starter #4.", "Starters Engage Starter4", XPlaneCommands.StartersEngageStarter4); } }
        private XPlaneCommand StartersEngageStarter5 { get { return new XPlaneCommand("sim/starters/engage_starter_5", "Engage starter #5.", "Starters Engage Starter5", XPlaneCommands.StartersEngageStarter5); } }
        private XPlaneCommand StartersEngageStarter6 { get { return new XPlaneCommand("sim/starters/engage_starter_6", "Engage starter #6.", "Starters Engage Starter6", XPlaneCommands.StartersEngageStarter6); } }
        private XPlaneCommand StartersEngageStarter7 { get { return new XPlaneCommand("sim/starters/engage_starter_7", "Engage starter #7.", "Starters Engage Starter7", XPlaneCommands.StartersEngageStarter7); } }
        private XPlaneCommand StartersEngageStarter8 { get { return new XPlaneCommand("sim/starters/engage_starter_8", "Engage starter #8.", "Starters Engage Starter8", XPlaneCommands.StartersEngageStarter8); } }
        private XPlaneCommand EnginesThrottleDown1 { get { return new XPlaneCommand("sim/engines/throttle_down_1", "Throttle down a bit for engine #1.", "Engines Throttle Down1", XPlaneCommands.EnginesThrottleDown1); } }
        private XPlaneCommand EnginesThrottleDown2 { get { return new XPlaneCommand("sim/engines/throttle_down_2", "Throttle down a bit for engine #2.", "Engines Throttle Down2", XPlaneCommands.EnginesThrottleDown2); } }
        private XPlaneCommand EnginesThrottleDown3 { get { return new XPlaneCommand("sim/engines/throttle_down_3", "Throttle down a bit for engine #3.", "Engines Throttle Down3", XPlaneCommands.EnginesThrottleDown3); } }
        private XPlaneCommand EnginesThrottleDown4 { get { return new XPlaneCommand("sim/engines/throttle_down_4", "Throttle down a bit for engine #4.", "Engines Throttle Down4", XPlaneCommands.EnginesThrottleDown4); } }
        private XPlaneCommand EnginesThrottleDown5 { get { return new XPlaneCommand("sim/engines/throttle_down_5", "Throttle down a bit for engine #5.", "Engines Throttle Down5", XPlaneCommands.EnginesThrottleDown5); } }
        private XPlaneCommand EnginesThrottleDown6 { get { return new XPlaneCommand("sim/engines/throttle_down_6", "Throttle down a bit for engine #6.", "Engines Throttle Down6", XPlaneCommands.EnginesThrottleDown6); } }
        private XPlaneCommand EnginesThrottleDown7 { get { return new XPlaneCommand("sim/engines/throttle_down_7", "Throttle down a bit for engine #7.", "Engines Throttle Down7", XPlaneCommands.EnginesThrottleDown7); } }
        private XPlaneCommand EnginesThrottleDown8 { get { return new XPlaneCommand("sim/engines/throttle_down_8", "Throttle down a bit for engine #8.", "Engines Throttle Down8", XPlaneCommands.EnginesThrottleDown8); } }
        private XPlaneCommand EnginesThrottleUp1 { get { return new XPlaneCommand("sim/engines/throttle_up_1", "Throttle up a bit for engine #1.", "Engines Throttle Up1", XPlaneCommands.EnginesThrottleUp1); } }
        private XPlaneCommand EnginesThrottleUp2 { get { return new XPlaneCommand("sim/engines/throttle_up_2", "Throttle up a bit for engine #2.", "Engines Throttle Up2", XPlaneCommands.EnginesThrottleUp2); } }
        private XPlaneCommand EnginesThrottleUp3 { get { return new XPlaneCommand("sim/engines/throttle_up_3", "Throttle up a bit for engine #3.", "Engines Throttle Up3", XPlaneCommands.EnginesThrottleUp3); } }
        private XPlaneCommand EnginesThrottleUp4 { get { return new XPlaneCommand("sim/engines/throttle_up_4", "Throttle up a bit for engine #4.", "Engines Throttle Up4", XPlaneCommands.EnginesThrottleUp4); } }
        private XPlaneCommand EnginesThrottleUp5 { get { return new XPlaneCommand("sim/engines/throttle_up_5", "Throttle up a bit for engine #5.", "Engines Throttle Up5", XPlaneCommands.EnginesThrottleUp5); } }
        private XPlaneCommand EnginesThrottleUp6 { get { return new XPlaneCommand("sim/engines/throttle_up_6", "Throttle up a bit for engine #6.", "Engines Throttle Up6", XPlaneCommands.EnginesThrottleUp6); } }
        private XPlaneCommand EnginesThrottleUp7 { get { return new XPlaneCommand("sim/engines/throttle_up_7", "Throttle up a bit for engine #7.", "Engines Throttle Up7", XPlaneCommands.EnginesThrottleUp7); } }
        private XPlaneCommand EnginesThrottleUp8 { get { return new XPlaneCommand("sim/engines/throttle_up_8", "Throttle up a bit for engine #8.", "Engines Throttle Up8", XPlaneCommands.EnginesThrottleUp8); } }
        private XPlaneCommand EnginesPropDown1 { get { return new XPlaneCommand("sim/engines/prop_down_1", "Prop down a bit for engine #1.", "Engines Prop Down1", XPlaneCommands.EnginesPropDown1); } }
        private XPlaneCommand EnginesPropDown2 { get { return new XPlaneCommand("sim/engines/prop_down_2", "Prop down a bit for engine #2.", "Engines Prop Down2", XPlaneCommands.EnginesPropDown2); } }
        private XPlaneCommand EnginesPropDown3 { get { return new XPlaneCommand("sim/engines/prop_down_3", "Prop down a bit for engine #3.", "Engines Prop Down3", XPlaneCommands.EnginesPropDown3); } }
        private XPlaneCommand EnginesPropDown4 { get { return new XPlaneCommand("sim/engines/prop_down_4", "Prop down a bit for engine #4.", "Engines Prop Down4", XPlaneCommands.EnginesPropDown4); } }
        private XPlaneCommand EnginesPropDown5 { get { return new XPlaneCommand("sim/engines/prop_down_5", "Prop down a bit for engine #5.", "Engines Prop Down5", XPlaneCommands.EnginesPropDown5); } }
        private XPlaneCommand EnginesPropDown6 { get { return new XPlaneCommand("sim/engines/prop_down_6", "Prop down a bit for engine #6.", "Engines Prop Down6", XPlaneCommands.EnginesPropDown6); } }
        private XPlaneCommand EnginesPropDown7 { get { return new XPlaneCommand("sim/engines/prop_down_7", "Prop down a bit for engine #7.", "Engines Prop Down7", XPlaneCommands.EnginesPropDown7); } }
        private XPlaneCommand EnginesPropDown8 { get { return new XPlaneCommand("sim/engines/prop_down_8", "Prop down a bit for engine #8.", "Engines Prop Down8", XPlaneCommands.EnginesPropDown8); } }
        private XPlaneCommand EnginesPropUp1 { get { return new XPlaneCommand("sim/engines/prop_up_1", "Prop up a bit for engine #1.", "Engines Prop Up1", XPlaneCommands.EnginesPropUp1); } }
        private XPlaneCommand EnginesPropUp2 { get { return new XPlaneCommand("sim/engines/prop_up_2", "Prop up a bit for engine #2.", "Engines Prop Up2", XPlaneCommands.EnginesPropUp2); } }
        private XPlaneCommand EnginesPropUp3 { get { return new XPlaneCommand("sim/engines/prop_up_3", "Prop up a bit for engine #3.", "Engines Prop Up3", XPlaneCommands.EnginesPropUp3); } }
        private XPlaneCommand EnginesPropUp4 { get { return new XPlaneCommand("sim/engines/prop_up_4", "Prop up a bit for engine #4.", "Engines Prop Up4", XPlaneCommands.EnginesPropUp4); } }
        private XPlaneCommand EnginesPropUp5 { get { return new XPlaneCommand("sim/engines/prop_up_5", "Prop up a bit for engine #5.", "Engines Prop Up5", XPlaneCommands.EnginesPropUp5); } }
        private XPlaneCommand EnginesPropUp6 { get { return new XPlaneCommand("sim/engines/prop_up_6", "Prop up a bit for engine #6.", "Engines Prop Up6", XPlaneCommands.EnginesPropUp6); } }
        private XPlaneCommand EnginesPropUp7 { get { return new XPlaneCommand("sim/engines/prop_up_7", "Prop up a bit for engine #7.", "Engines Prop Up7", XPlaneCommands.EnginesPropUp7); } }
        private XPlaneCommand EnginesPropUp8 { get { return new XPlaneCommand("sim/engines/prop_up_8", "Prop up a bit for engine #8.", "Engines Prop Up8", XPlaneCommands.EnginesPropUp8); } }
        private XPlaneCommand EnginesMixtureDown1 { get { return new XPlaneCommand("sim/engines/mixture_down_1", "Mixture down a bit for engine #1.", "Engines Mixture Down1", XPlaneCommands.EnginesMixtureDown1); } }
        private XPlaneCommand EnginesMixtureDown2 { get { return new XPlaneCommand("sim/engines/mixture_down_2", "Mixture down a bit for engine #2.", "Engines Mixture Down2", XPlaneCommands.EnginesMixtureDown2); } }
        private XPlaneCommand EnginesMixtureDown3 { get { return new XPlaneCommand("sim/engines/mixture_down_3", "Mixture down a bit for engine #3.", "Engines Mixture Down3", XPlaneCommands.EnginesMixtureDown3); } }
        private XPlaneCommand EnginesMixtureDown4 { get { return new XPlaneCommand("sim/engines/mixture_down_4", "Mixture down a bit for engine #4.", "Engines Mixture Down4", XPlaneCommands.EnginesMixtureDown4); } }
        private XPlaneCommand EnginesMixtureDown5 { get { return new XPlaneCommand("sim/engines/mixture_down_5", "Mixture down a bit for engine #5.", "Engines Mixture Down5", XPlaneCommands.EnginesMixtureDown5); } }
        private XPlaneCommand EnginesMixtureDown6 { get { return new XPlaneCommand("sim/engines/mixture_down_6", "Mixture down a bit for engine #6.", "Engines Mixture Down6", XPlaneCommands.EnginesMixtureDown6); } }
        private XPlaneCommand EnginesMixtureDown7 { get { return new XPlaneCommand("sim/engines/mixture_down_7", "Mixture down a bit for engine #7.", "Engines Mixture Down7", XPlaneCommands.EnginesMixtureDown7); } }
        private XPlaneCommand EnginesMixtureDown8 { get { return new XPlaneCommand("sim/engines/mixture_down_8", "Mixture down a bit for engine #8.", "Engines Mixture Down8", XPlaneCommands.EnginesMixtureDown8); } }
        private XPlaneCommand EnginesMixtureUp1 { get { return new XPlaneCommand("sim/engines/mixture_up_1", "Mixture up a bit for engine #1.", "Engines Mixture Up1", XPlaneCommands.EnginesMixtureUp1); } }
        private XPlaneCommand EnginesMixtureUp2 { get { return new XPlaneCommand("sim/engines/mixture_up_2", "Mixture up a bit for engine #2.", "Engines Mixture Up2", XPlaneCommands.EnginesMixtureUp2); } }
        private XPlaneCommand EnginesMixtureUp3 { get { return new XPlaneCommand("sim/engines/mixture_up_3", "Mixture up a bit for engine #3.", "Engines Mixture Up3", XPlaneCommands.EnginesMixtureUp3); } }
        private XPlaneCommand EnginesMixtureUp4 { get { return new XPlaneCommand("sim/engines/mixture_up_4", "Mixture up a bit for engine #4.", "Engines Mixture Up4", XPlaneCommands.EnginesMixtureUp4); } }
        private XPlaneCommand EnginesMixtureUp5 { get { return new XPlaneCommand("sim/engines/mixture_up_5", "Mixture up a bit for engine #5.", "Engines Mixture Up5", XPlaneCommands.EnginesMixtureUp5); } }
        private XPlaneCommand EnginesMixtureUp6 { get { return new XPlaneCommand("sim/engines/mixture_up_6", "Mixture up a bit for engine #6.", "Engines Mixture Up6", XPlaneCommands.EnginesMixtureUp6); } }
        private XPlaneCommand EnginesMixtureUp7 { get { return new XPlaneCommand("sim/engines/mixture_up_7", "Mixture up a bit for engine #7.", "Engines Mixture Up7", XPlaneCommands.EnginesMixtureUp7); } }
        private XPlaneCommand EnginesMixtureUp8 { get { return new XPlaneCommand("sim/engines/mixture_up_8", "Mixture up a bit for engine #8.", "Engines Mixture Up8", XPlaneCommands.EnginesMixtureUp8); } }
        private XPlaneCommand EnginesBetaToggle1 { get { return new XPlaneCommand("sim/engines/beta_toggle_1", "Toggle beta prop #1.", "Engines Beta Toggle1", XPlaneCommands.EnginesBetaToggle1); } }
        private XPlaneCommand EnginesBetaToggle2 { get { return new XPlaneCommand("sim/engines/beta_toggle_2", "Toggle beta prop #2.", "Engines Beta Toggle2", XPlaneCommands.EnginesBetaToggle2); } }
        private XPlaneCommand EnginesBetaToggle3 { get { return new XPlaneCommand("sim/engines/beta_toggle_3", "Toggle beta prop #3.", "Engines Beta Toggle3", XPlaneCommands.EnginesBetaToggle3); } }
        private XPlaneCommand EnginesBetaToggle4 { get { return new XPlaneCommand("sim/engines/beta_toggle_4", "Toggle beta prop #4.", "Engines Beta Toggle4", XPlaneCommands.EnginesBetaToggle4); } }
        private XPlaneCommand EnginesBetaToggle5 { get { return new XPlaneCommand("sim/engines/beta_toggle_5", "Toggle beta prop #5.", "Engines Beta Toggle5", XPlaneCommands.EnginesBetaToggle5); } }
        private XPlaneCommand EnginesBetaToggle6 { get { return new XPlaneCommand("sim/engines/beta_toggle_6", "Toggle beta prop #6.", "Engines Beta Toggle6", XPlaneCommands.EnginesBetaToggle6); } }
        private XPlaneCommand EnginesBetaToggle7 { get { return new XPlaneCommand("sim/engines/beta_toggle_7", "Toggle beta prop #7.", "Engines Beta Toggle7", XPlaneCommands.EnginesBetaToggle7); } }
        private XPlaneCommand EnginesBetaToggle8 { get { return new XPlaneCommand("sim/engines/beta_toggle_8", "Toggle beta prop #8.", "Engines Beta Toggle8", XPlaneCommands.EnginesBetaToggle8); } }
        private XPlaneCommand EnginesThrustReverseToggle1 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_1", "Toggle thrust reversers #1.", "Engines Thrust Reverse Toggle1", XPlaneCommands.EnginesThrustReverseToggle1); } }
        private XPlaneCommand EnginesThrustReverseToggle2 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_2", "Toggle thrust reversers #2.", "Engines Thrust Reverse Toggle2", XPlaneCommands.EnginesThrustReverseToggle2); } }
        private XPlaneCommand EnginesThrustReverseToggle3 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_3", "Toggle thrust reversers #3.", "Engines Thrust Reverse Toggle3", XPlaneCommands.EnginesThrustReverseToggle3); } }
        private XPlaneCommand EnginesThrustReverseToggle4 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_4", "Toggle thrust reversers #4.", "Engines Thrust Reverse Toggle4", XPlaneCommands.EnginesThrustReverseToggle4); } }
        private XPlaneCommand EnginesThrustReverseToggle5 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_5", "Toggle thrust reversers #5.", "Engines Thrust Reverse Toggle5", XPlaneCommands.EnginesThrustReverseToggle5); } }
        private XPlaneCommand EnginesThrustReverseToggle6 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_6", "Toggle thrust reversers #6.", "Engines Thrust Reverse Toggle6", XPlaneCommands.EnginesThrustReverseToggle6); } }
        private XPlaneCommand EnginesThrustReverseToggle7 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_7", "Toggle thrust reversers #7.", "Engines Thrust Reverse Toggle7", XPlaneCommands.EnginesThrustReverseToggle7); } }
        private XPlaneCommand EnginesThrustReverseToggle8 { get { return new XPlaneCommand("sim/engines/thrust_reverse_toggle_8", "Toggle thrust reversers #8.", "Engines Thrust Reverse Toggle8", XPlaneCommands.EnginesThrustReverseToggle8); } }
        private XPlaneCommand EnginesThrustReverseHold1 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_1", "Hold thrust reverse at max #1.", "Engines Thrust Reverse Hold1", XPlaneCommands.EnginesThrustReverseHold1); } }
        private XPlaneCommand EnginesThrustReverseHold2 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_2", "Hold thrust reverse at max #2.", "Engines Thrust Reverse Hold2", XPlaneCommands.EnginesThrustReverseHold2); } }
        private XPlaneCommand EnginesThrustReverseHold3 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_3", "Hold thrust reverse at max #3.", "Engines Thrust Reverse Hold3", XPlaneCommands.EnginesThrustReverseHold3); } }
        private XPlaneCommand EnginesThrustReverseHold4 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_4", "Hold thrust reverse at max #4.", "Engines Thrust Reverse Hold4", XPlaneCommands.EnginesThrustReverseHold4); } }
        private XPlaneCommand EnginesThrustReverseHold5 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_5", "Hold thrust reverse at max #5.", "Engines Thrust Reverse Hold5", XPlaneCommands.EnginesThrustReverseHold5); } }
        private XPlaneCommand EnginesThrustReverseHold6 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_6", "Hold thrust reverse at max #6.", "Engines Thrust Reverse Hold6", XPlaneCommands.EnginesThrustReverseHold6); } }
        private XPlaneCommand EnginesThrustReverseHold7 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_7", "Hold thrust reverse at max #7.", "Engines Thrust Reverse Hold7", XPlaneCommands.EnginesThrustReverseHold7); } }
        private XPlaneCommand EnginesThrustReverseHold8 { get { return new XPlaneCommand("sim/engines/thrust_reverse_hold_8", "Hold thrust reverse at max #8.", "Engines Thrust Reverse Hold8", XPlaneCommands.EnginesThrustReverseHold8); } }
        private XPlaneCommand StartersShutDown1 { get { return new XPlaneCommand("sim/starters/shut_down_1", "Pull fuel and mags for shut-down #1.", "Starters Shut Down1", XPlaneCommands.StartersShutDown1); } }
        private XPlaneCommand StartersShutDown2 { get { return new XPlaneCommand("sim/starters/shut_down_2", "Pull fuel and mags for shut-down #2.", "Starters Shut Down2", XPlaneCommands.StartersShutDown2); } }
        private XPlaneCommand StartersShutDown3 { get { return new XPlaneCommand("sim/starters/shut_down_3", "Pull fuel and mags for shut-down #3.", "Starters Shut Down3", XPlaneCommands.StartersShutDown3); } }
        private XPlaneCommand StartersShutDown4 { get { return new XPlaneCommand("sim/starters/shut_down_4", "Pull fuel and mags for shut-down #4.", "Starters Shut Down4", XPlaneCommands.StartersShutDown4); } }
        private XPlaneCommand StartersShutDown5 { get { return new XPlaneCommand("sim/starters/shut_down_5", "Pull fuel and mags for shut-down #5.", "Starters Shut Down5", XPlaneCommands.StartersShutDown5); } }
        private XPlaneCommand StartersShutDown6 { get { return new XPlaneCommand("sim/starters/shut_down_6", "Pull fuel and mags for shut-down #6.", "Starters Shut Down6", XPlaneCommands.StartersShutDown6); } }
        private XPlaneCommand StartersShutDown7 { get { return new XPlaneCommand("sim/starters/shut_down_7", "Pull fuel and mags for shut-down #7.", "Starters Shut Down7", XPlaneCommands.StartersShutDown7); } }
        private XPlaneCommand StartersShutDown8 { get { return new XPlaneCommand("sim/starters/shut_down_8", "Pull fuel and mags for shut-down #8.", "Starters Shut Down8", XPlaneCommands.StartersShutDown8); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed1 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_1", "Move cowl flaps #1 closed a bit.", "Flight Controls Cowl Flaps Closed1", XPlaneCommands.FlightControlsCowlFlapsClosed1); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed2 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_2", "Move cowl flaps #2 closed a bit.", "Flight Controls Cowl Flaps Closed2", XPlaneCommands.FlightControlsCowlFlapsClosed2); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed3 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_3", "Move cowl flaps #3 closed a bit.", "Flight Controls Cowl Flaps Closed3", XPlaneCommands.FlightControlsCowlFlapsClosed3); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed4 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_4", "Move cowl flaps #4 closed a bit.", "Flight Controls Cowl Flaps Closed4", XPlaneCommands.FlightControlsCowlFlapsClosed4); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed5 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_5", "Move cowl flaps #5 closed a bit.", "Flight Controls Cowl Flaps Closed5", XPlaneCommands.FlightControlsCowlFlapsClosed5); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed6 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_6", "Move cowl flaps #6 closed a bit.", "Flight Controls Cowl Flaps Closed6", XPlaneCommands.FlightControlsCowlFlapsClosed6); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed7 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_7", "Move cowl flaps #7 closed a bit.", "Flight Controls Cowl Flaps Closed7", XPlaneCommands.FlightControlsCowlFlapsClosed7); } }
        private XPlaneCommand FlightControlsCowlFlapsClosed8 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_closed_8", "Move cowl flaps #8 closed a bit.", "Flight Controls Cowl Flaps Closed8", XPlaneCommands.FlightControlsCowlFlapsClosed8); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen1 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_1", "Move cowl flaps #1 open a bit.", "Flight Controls Cowl Flaps Open1", XPlaneCommands.FlightControlsCowlFlapsOpen1); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen2 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_2", "Move cowl flaps #2 open a bit.", "Flight Controls Cowl Flaps Open2", XPlaneCommands.FlightControlsCowlFlapsOpen2); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen3 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_3", "Move cowl flaps #3 open a bit.", "Flight Controls Cowl Flaps Open3", XPlaneCommands.FlightControlsCowlFlapsOpen3); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen4 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_4", "Move cowl flaps #4 open a bit.", "Flight Controls Cowl Flaps Open4", XPlaneCommands.FlightControlsCowlFlapsOpen4); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen5 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_5", "Move cowl flaps #5 open a bit.", "Flight Controls Cowl Flaps Open5", XPlaneCommands.FlightControlsCowlFlapsOpen5); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen6 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_6", "Move cowl flaps #6 open a bit.", "Flight Controls Cowl Flaps Open6", XPlaneCommands.FlightControlsCowlFlapsOpen6); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen7 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_7", "Move cowl flaps #7 open a bit.", "Flight Controls Cowl Flaps Open7", XPlaneCommands.FlightControlsCowlFlapsOpen7); } }
        private XPlaneCommand FlightControlsCowlFlapsOpen8 { get { return new XPlaneCommand("sim/flight_controls/cowl_flaps_open_8", "Move cowl flaps #8 open a bit.", "Flight Controls Cowl Flaps Open8", XPlaneCommands.FlightControlsCowlFlapsOpen8); } }
        private XPlaneCommand FadecFadec1Off { get { return new XPlaneCommand("sim/fadec/fadec_1_off", "FADEC #1 off.", "Fadec Fadec1off", XPlaneCommands.FadecFadec1Off); } }
        private XPlaneCommand FadecFadec2Off { get { return new XPlaneCommand("sim/fadec/fadec_2_off", "FADEC #2 off.", "Fadec Fadec2off", XPlaneCommands.FadecFadec2Off); } }
        private XPlaneCommand FadecFadec3Off { get { return new XPlaneCommand("sim/fadec/fadec_3_off", "FADEC #3 off.", "Fadec Fadec3off", XPlaneCommands.FadecFadec3Off); } }
        private XPlaneCommand FadecFadec4Off { get { return new XPlaneCommand("sim/fadec/fadec_4_off", "FADEC #4 off.", "Fadec Fadec4off", XPlaneCommands.FadecFadec4Off); } }
        private XPlaneCommand FadecFadec5Off { get { return new XPlaneCommand("sim/fadec/fadec_5_off", "FADEC #5 off.", "Fadec Fadec5off", XPlaneCommands.FadecFadec5Off); } }
        private XPlaneCommand FadecFadec6Off { get { return new XPlaneCommand("sim/fadec/fadec_6_off", "FADEC #6 off.", "Fadec Fadec6off", XPlaneCommands.FadecFadec6Off); } }
        private XPlaneCommand FadecFadec7Off { get { return new XPlaneCommand("sim/fadec/fadec_7_off", "FADEC #7 off.", "Fadec Fadec7off", XPlaneCommands.FadecFadec7Off); } }
        private XPlaneCommand FadecFadec8Off { get { return new XPlaneCommand("sim/fadec/fadec_8_off", "FADEC #8 off.", "Fadec Fadec8off", XPlaneCommands.FadecFadec8Off); } }
        private XPlaneCommand FadecFadec1On { get { return new XPlaneCommand("sim/fadec/fadec_1_on", "FADEC #1 on.", "Fadec Fadec1on", XPlaneCommands.FadecFadec1On); } }
        private XPlaneCommand FadecFadec2On { get { return new XPlaneCommand("sim/fadec/fadec_2_on", "FADEC #2 on.", "Fadec Fadec2on", XPlaneCommands.FadecFadec2On); } }
        private XPlaneCommand FadecFadec3On { get { return new XPlaneCommand("sim/fadec/fadec_3_on", "FADEC #3 on.", "Fadec Fadec3on", XPlaneCommands.FadecFadec3On); } }
        private XPlaneCommand FadecFadec4On { get { return new XPlaneCommand("sim/fadec/fadec_4_on", "FADEC #4 on.", "Fadec Fadec4on", XPlaneCommands.FadecFadec4On); } }
        private XPlaneCommand FadecFadec5On { get { return new XPlaneCommand("sim/fadec/fadec_5_on", "FADEC #5 on.", "Fadec Fadec5on", XPlaneCommands.FadecFadec5On); } }
        private XPlaneCommand FadecFadec6On { get { return new XPlaneCommand("sim/fadec/fadec_6_on", "FADEC #6 on.", "Fadec Fadec6on", XPlaneCommands.FadecFadec6On); } }
        private XPlaneCommand FadecFadec7On { get { return new XPlaneCommand("sim/fadec/fadec_7_on", "FADEC #7 on.", "Fadec Fadec7on", XPlaneCommands.FadecFadec7On); } }
        private XPlaneCommand FadecFadec8On { get { return new XPlaneCommand("sim/fadec/fadec_8_on", "FADEC #8 on.", "Fadec Fadec8on", XPlaneCommands.FadecFadec8On); } }
        private XPlaneCommand AltairAlternateAirOff1 { get { return new XPlaneCommand("sim/altair/alternate_air_off_1", "Alternate air #1 off.", "Altair Alternate Air Off1", XPlaneCommands.AltairAlternateAirOff1); } }
        private XPlaneCommand AltairAlternateAirOff2 { get { return new XPlaneCommand("sim/altair/alternate_air_off_2", "Alternate air #2 off.", "Altair Alternate Air Off2", XPlaneCommands.AltairAlternateAirOff2); } }
        private XPlaneCommand AltairAlternateAirOff3 { get { return new XPlaneCommand("sim/altair/alternate_air_off_3", "Alternate air #3 off.", "Altair Alternate Air Off3", XPlaneCommands.AltairAlternateAirOff3); } }
        private XPlaneCommand AltairAlternateAirOff4 { get { return new XPlaneCommand("sim/altair/alternate_air_off_4", "Alternate air #4 off.", "Altair Alternate Air Off4", XPlaneCommands.AltairAlternateAirOff4); } }
        private XPlaneCommand AltairAlternateAirOff5 { get { return new XPlaneCommand("sim/altair/alternate_air_off_5", "Alternate air #5 off.", "Altair Alternate Air Off5", XPlaneCommands.AltairAlternateAirOff5); } }
        private XPlaneCommand AltairAlternateAirOff6 { get { return new XPlaneCommand("sim/altair/alternate_air_off_6", "Alternate air #6 off.", "Altair Alternate Air Off6", XPlaneCommands.AltairAlternateAirOff6); } }
        private XPlaneCommand AltairAlternateAirOff7 { get { return new XPlaneCommand("sim/altair/alternate_air_off_7", "Alternate air #7 off.", "Altair Alternate Air Off7", XPlaneCommands.AltairAlternateAirOff7); } }
        private XPlaneCommand AltairAlternateAirOff8 { get { return new XPlaneCommand("sim/altair/alternate_air_off_8", "Alternate air #8 off.", "Altair Alternate Air Off8", XPlaneCommands.AltairAlternateAirOff8); } }
        private XPlaneCommand AltairAlternateAirOn1 { get { return new XPlaneCommand("sim/altair/alternate_air_on_1", "Alternate air #1 on.", "Altair Alternate Air On1", XPlaneCommands.AltairAlternateAirOn1); } }
        private XPlaneCommand AltairAlternateAirOn2 { get { return new XPlaneCommand("sim/altair/alternate_air_on_2", "Alternate air #2 on.", "Altair Alternate Air On2", XPlaneCommands.AltairAlternateAirOn2); } }
        private XPlaneCommand AltairAlternateAirOn3 { get { return new XPlaneCommand("sim/altair/alternate_air_on_3", "Alternate air #3 on.", "Altair Alternate Air On3", XPlaneCommands.AltairAlternateAirOn3); } }
        private XPlaneCommand AltairAlternateAirOn4 { get { return new XPlaneCommand("sim/altair/alternate_air_on_4", "Alternate air #4 on.", "Altair Alternate Air On4", XPlaneCommands.AltairAlternateAirOn4); } }
        private XPlaneCommand AltairAlternateAirOn5 { get { return new XPlaneCommand("sim/altair/alternate_air_on_5", "Alternate air #5 on.", "Altair Alternate Air On5", XPlaneCommands.AltairAlternateAirOn5); } }
        private XPlaneCommand AltairAlternateAirOn6 { get { return new XPlaneCommand("sim/altair/alternate_air_on_6", "Alternate air #6 on.", "Altair Alternate Air On6", XPlaneCommands.AltairAlternateAirOn6); } }
        private XPlaneCommand AltairAlternateAirOn7 { get { return new XPlaneCommand("sim/altair/alternate_air_on_7", "Alternate air #7 on.", "Altair Alternate Air On7", XPlaneCommands.AltairAlternateAirOn7); } }
        private XPlaneCommand AltairAlternateAirOn8 { get { return new XPlaneCommand("sim/altair/alternate_air_on_8", "Alternate air #8 on.", "Altair Alternate Air On8", XPlaneCommands.AltairAlternateAirOn8); } }
        private XPlaneCommand AltairAlternateAirBackupOff1 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_1", "Alternate air backup #1 off.", "Altair Alternate Air Backup Off1", XPlaneCommands.AltairAlternateAirBackupOff1); } }
        private XPlaneCommand AltairAlternateAirBackupOff2 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_2", "Alternate air backup #2 off.", "Altair Alternate Air Backup Off2", XPlaneCommands.AltairAlternateAirBackupOff2); } }
        private XPlaneCommand AltairAlternateAirBackupOff3 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_3", "Alternate air backup #3 off.", "Altair Alternate Air Backup Off3", XPlaneCommands.AltairAlternateAirBackupOff3); } }
        private XPlaneCommand AltairAlternateAirBackupOff4 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_4", "Alternate air backup #4 off.", "Altair Alternate Air Backup Off4", XPlaneCommands.AltairAlternateAirBackupOff4); } }
        private XPlaneCommand AltairAlternateAirBackupOff5 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_5", "Alternate air backup #5 off.", "Altair Alternate Air Backup Off5", XPlaneCommands.AltairAlternateAirBackupOff5); } }
        private XPlaneCommand AltairAlternateAirBackupOff6 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_6", "Alternate air backup #6 off.", "Altair Alternate Air Backup Off6", XPlaneCommands.AltairAlternateAirBackupOff6); } }
        private XPlaneCommand AltairAlternateAirBackupOff7 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_7", "Alternate air backup #7 off.", "Altair Alternate Air Backup Off7", XPlaneCommands.AltairAlternateAirBackupOff7); } }
        private XPlaneCommand AltairAlternateAirBackupOff8 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_off_8", "Alternate air backup #8 off.", "Altair Alternate Air Backup Off8", XPlaneCommands.AltairAlternateAirBackupOff8); } }
        private XPlaneCommand AltairAlternateAirBackupOn1 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_1", "Alternate air backup #1 on.", "Altair Alternate Air Backup On1", XPlaneCommands.AltairAlternateAirBackupOn1); } }
        private XPlaneCommand AltairAlternateAirBackupOn2 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_2", "Alternate air backup #2 on.", "Altair Alternate Air Backup On2", XPlaneCommands.AltairAlternateAirBackupOn2); } }
        private XPlaneCommand AltairAlternateAirBackupOn3 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_3", "Alternate air backup #3 on.", "Altair Alternate Air Backup On3", XPlaneCommands.AltairAlternateAirBackupOn3); } }
        private XPlaneCommand AltairAlternateAirBackupOn4 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_4", "Alternate air backup #4 on.", "Altair Alternate Air Backup On4", XPlaneCommands.AltairAlternateAirBackupOn4); } }
        private XPlaneCommand AltairAlternateAirBackupOn5 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_5", "Alternate air backup #5 on.", "Altair Alternate Air Backup On5", XPlaneCommands.AltairAlternateAirBackupOn5); } }
        private XPlaneCommand AltairAlternateAirBackupOn6 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_6", "Alternate air backup #6 on.", "Altair Alternate Air Backup On6", XPlaneCommands.AltairAlternateAirBackupOn6); } }
        private XPlaneCommand AltairAlternateAirBackupOn7 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_7", "Alternate air backup #7 on.", "Altair Alternate Air Backup On7", XPlaneCommands.AltairAlternateAirBackupOn7); } }
        private XPlaneCommand AltairAlternateAirBackupOn8 { get { return new XPlaneCommand("sim/altair/alternate_air_backup_on_8", "Alternate air backup #8 on.", "Altair Alternate Air Backup On8", XPlaneCommands.AltairAlternateAirBackupOn8); } }
        private XPlaneCommand EnginesFireExt1Off { get { return new XPlaneCommand("sim/engines/fire_ext_1_off", "Fire extinguisher #1 off.", "Engines Fire Ext1off", XPlaneCommands.EnginesFireExt1Off); } }
        private XPlaneCommand EnginesFireExt2Off { get { return new XPlaneCommand("sim/engines/fire_ext_2_off", "Fire extinguisher #2 off.", "Engines Fire Ext2off", XPlaneCommands.EnginesFireExt2Off); } }
        private XPlaneCommand EnginesFireExt3Off { get { return new XPlaneCommand("sim/engines/fire_ext_3_off", "Fire extinguisher #3 off.", "Engines Fire Ext3off", XPlaneCommands.EnginesFireExt3Off); } }
        private XPlaneCommand EnginesFireExt4Off { get { return new XPlaneCommand("sim/engines/fire_ext_4_off", "Fire extinguisher #4 off.", "Engines Fire Ext4off", XPlaneCommands.EnginesFireExt4Off); } }
        private XPlaneCommand EnginesFireExt5Off { get { return new XPlaneCommand("sim/engines/fire_ext_5_off", "Fire extinguisher #5 off.", "Engines Fire Ext5off", XPlaneCommands.EnginesFireExt5Off); } }
        private XPlaneCommand EnginesFireExt6Off { get { return new XPlaneCommand("sim/engines/fire_ext_6_off", "Fire extinguisher #6 off.", "Engines Fire Ext6off", XPlaneCommands.EnginesFireExt6Off); } }
        private XPlaneCommand EnginesFireExt7Off { get { return new XPlaneCommand("sim/engines/fire_ext_7_off", "Fire extinguisher #7 off.", "Engines Fire Ext7off", XPlaneCommands.EnginesFireExt7Off); } }
        private XPlaneCommand EnginesFireExt8Off { get { return new XPlaneCommand("sim/engines/fire_ext_8_off", "Fire extinguisher #8 off.", "Engines Fire Ext8off", XPlaneCommands.EnginesFireExt8Off); } }
        private XPlaneCommand EnginesFireExt1On { get { return new XPlaneCommand("sim/engines/fire_ext_1_on", "Fire extinguisher #1 on.", "Engines Fire Ext1on", XPlaneCommands.EnginesFireExt1On); } }
        private XPlaneCommand EnginesFireExt2On { get { return new XPlaneCommand("sim/engines/fire_ext_2_on", "Fire extinguisher #2 on.", "Engines Fire Ext2on", XPlaneCommands.EnginesFireExt2On); } }
        private XPlaneCommand EnginesFireExt3On { get { return new XPlaneCommand("sim/engines/fire_ext_3_on", "Fire extinguisher #3 on.", "Engines Fire Ext3on", XPlaneCommands.EnginesFireExt3On); } }
        private XPlaneCommand EnginesFireExt4On { get { return new XPlaneCommand("sim/engines/fire_ext_4_on", "Fire extinguisher #4 on.", "Engines Fire Ext4on", XPlaneCommands.EnginesFireExt4On); } }
        private XPlaneCommand EnginesFireExt5On { get { return new XPlaneCommand("sim/engines/fire_ext_5_on", "Fire extinguisher #5 on.", "Engines Fire Ext5on", XPlaneCommands.EnginesFireExt5On); } }
        private XPlaneCommand EnginesFireExt6On { get { return new XPlaneCommand("sim/engines/fire_ext_6_on", "Fire extinguisher #6 on.", "Engines Fire Ext6on", XPlaneCommands.EnginesFireExt6On); } }
        private XPlaneCommand EnginesFireExt7On { get { return new XPlaneCommand("sim/engines/fire_ext_7_on", "Fire extinguisher #7 on.", "Engines Fire Ext7on", XPlaneCommands.EnginesFireExt7On); } }
        private XPlaneCommand EnginesFireExt8On { get { return new XPlaneCommand("sim/engines/fire_ext_8_on", "Fire extinguisher #8 on.", "Engines Fire Ext8on", XPlaneCommands.EnginesFireExt8On); } }
        private XPlaneCommand FlightControlsFlapsUp { get { return new XPlaneCommand("sim/flight_controls/flaps_up", "Flaps up a notch.", "Flight Controls Flaps Up", XPlaneCommands.FlightControlsFlapsUp); } }
        private XPlaneCommand FlightControlsFlapsDown { get { return new XPlaneCommand("sim/flight_controls/flaps_down", "Flaps down a notch.", "Flight Controls Flaps Down", XPlaneCommands.FlightControlsFlapsDown); } }
        private XPlaneCommand FlightControlsVectorSweepAft { get { return new XPlaneCommand("sim/flight_controls/vector_sweep_aft", "Vector or sweep aft.", "Flight Controls Vector Sweep Aft", XPlaneCommands.FlightControlsVectorSweepAft); } }
        private XPlaneCommand FlightControlsVectorSweepForward { get { return new XPlaneCommand("sim/flight_controls/vector_sweep_forward", "Vector or sweep forward.", "Flight Controls Vector Sweep Forward", XPlaneCommands.FlightControlsVectorSweepForward); } }
        private XPlaneCommand FlightControlsBlimpLiftDown { get { return new XPlaneCommand("sim/flight_controls/blimp_lift_down", "Blimp lift down a bit.", "Flight Controls Blimp Lift Down", XPlaneCommands.FlightControlsBlimpLiftDown); } }
        private XPlaneCommand FlightControlsBlimpLiftUp { get { return new XPlaneCommand("sim/flight_controls/blimp_lift_up", "Blimp lift up a bit.", "Flight Controls Blimp Lift Up", XPlaneCommands.FlightControlsBlimpLiftUp); } }
        private XPlaneCommand FlightControlsSpeedBrakesDownOne { get { return new XPlaneCommand("sim/flight_controls/speed_brakes_down_one", "Speedbrakes extend one.", "Flight Controls Speed Brakes Down One", XPlaneCommands.FlightControlsSpeedBrakesDownOne); } }
        private XPlaneCommand FlightControlsSpeedBrakesUpOne { get { return new XPlaneCommand("sim/flight_controls/speed_brakes_up_one", "Speedbrakes retract one.", "Flight Controls Speed Brakes Up One", XPlaneCommands.FlightControlsSpeedBrakesUpOne); } }
        private XPlaneCommand FlightControlsSpeedBrakesDownAll { get { return new XPlaneCommand("sim/flight_controls/speed_brakes_down_all", "Speedbrakes extend full.", "Flight Controls Speed Brakes Down All", XPlaneCommands.FlightControlsSpeedBrakesDownAll); } }
        private XPlaneCommand FlightControlsSpeedBrakesUpAll { get { return new XPlaneCommand("sim/flight_controls/speed_brakes_up_all", "Speedbrakes retract full.", "Flight Controls Speed Brakes Up All", XPlaneCommands.FlightControlsSpeedBrakesUpAll); } }
        private XPlaneCommand FlightControlsSpeedBrakesToggle { get { return new XPlaneCommand("sim/flight_controls/speed_brakes_toggle", "Speedbrakes toggle.", "Flight Controls Speed Brakes Toggle", XPlaneCommands.FlightControlsSpeedBrakesToggle); } }
        private XPlaneCommand FlightControlsLandingGearDown { get { return new XPlaneCommand("sim/flight_controls/landing_gear_down", "Landing gear down.", "Flight Controls Landing Gear Down", XPlaneCommands.FlightControlsLandingGearDown); } }
        private XPlaneCommand FlightControlsLandingGearUp { get { return new XPlaneCommand("sim/flight_controls/landing_gear_up", "Landing gear up.", "Flight Controls Landing Gear Up", XPlaneCommands.FlightControlsLandingGearUp); } }
        private XPlaneCommand FlightControlsLandingGearToggle { get { return new XPlaneCommand("sim/flight_controls/landing_gear_toggle", "Landing gear toggle.", "Flight Controls Landing Gear Toggle", XPlaneCommands.FlightControlsLandingGearToggle); } }
        private XPlaneCommand FlightControlsLandingGearEmerOn { get { return new XPlaneCommand("sim/flight_controls/landing_gear_emer_on", "Landing gear emergency override down.", "Flight Controls Landing Gear Emer On", XPlaneCommands.FlightControlsLandingGearEmerOn); } }
        private XPlaneCommand FlightControlsLandingGearEmerOff { get { return new XPlaneCommand("sim/flight_controls/landing_gear_emer_off", "Landing gear emergency override off.", "Flight Controls Landing Gear Emer Off", XPlaneCommands.FlightControlsLandingGearEmerOff); } }
        private XPlaneCommand FlightControlsNwheelSteerToggle { get { return new XPlaneCommand("sim/flight_controls/nwheel_steer_toggle", "Nosewheel steer toggle.", "Flight Controls Nwheel Steer Toggle", XPlaneCommands.FlightControlsNwheelSteerToggle); } }
        private XPlaneCommand FlightControlsTailWheelLockToggle { get { return new XPlaneCommand("sim/flight_controls/tail_wheel_lock_toggle", "Toggle tailwheel lock.", "Flight Controls Tail Wheel Lock Toggle", XPlaneCommands.FlightControlsTailWheelLockToggle); } }
        private XPlaneCommand FlightControlsTailWheelLockEngage { get { return new XPlaneCommand("sim/flight_controls/tail_wheel_lock_engage", "Engage tailwheel lock.", "Flight Controls Tail Wheel Lock Engage", XPlaneCommands.FlightControlsTailWheelLockEngage); } }
        private XPlaneCommand FlightControlsWaterRudderDown { get { return new XPlaneCommand("sim/flight_controls/water_rudder_down", "Water rudder down (engage).", "Flight Controls Water Rudder Down", XPlaneCommands.FlightControlsWaterRudderDown); } }
        private XPlaneCommand FlightControlsWaterRudderUp { get { return new XPlaneCommand("sim/flight_controls/water_rudder_up", "Water rudder up (disengage).", "Flight Controls Water Rudder Up", XPlaneCommands.FlightControlsWaterRudderUp); } }
        private XPlaneCommand FlightControlsWaterRudderToggle { get { return new XPlaneCommand("sim/flight_controls/water_rudder_toggle", "Toggle water rudder.", "Flight Controls Water Rudder Toggle", XPlaneCommands.FlightControlsWaterRudderToggle); } }
        private XPlaneCommand FlightControlsLeftBrake { get { return new XPlaneCommand("sim/flight_controls/left_brake", "Hold brake left.", "Flight Controls Left Brake", XPlaneCommands.FlightControlsLeftBrake); } }
        private XPlaneCommand FlightControlsRightBrake { get { return new XPlaneCommand("sim/flight_controls/right_brake", "Hold brake right.", "Flight Controls Right Brake", XPlaneCommands.FlightControlsRightBrake); } }
        private XPlaneCommand FlightControlsBrakesToggleRegular { get { return new XPlaneCommand("sim/flight_controls/brakes_toggle_regular", "Toggle brakes regular effort.", "Flight Controls Brakes Toggle Regular", XPlaneCommands.FlightControlsBrakesToggleRegular); } }
        private XPlaneCommand FlightControlsBrakesToggleMax { get { return new XPlaneCommand("sim/flight_controls/brakes_toggle_max", "Toggle brakes max effort.", "Flight Controls Brakes Toggle Max", XPlaneCommands.FlightControlsBrakesToggleMax); } }
        private XPlaneCommand FlightControlsBrakesRegular { get { return new XPlaneCommand("sim/flight_controls/brakes_regular", "Hold brakes regular.", "Flight Controls Brakes Regular", XPlaneCommands.FlightControlsBrakesRegular); } }
        private XPlaneCommand FlightControlsBrakesMax { get { return new XPlaneCommand("sim/flight_controls/brakes_max", "Hold brakes maximum.", "Flight Controls Brakes Max", XPlaneCommands.FlightControlsBrakesMax); } }
        private XPlaneCommand FlightControlsBrakesToggleAuto { get { return new XPlaneCommand("sim/flight_controls/brakes_toggle_auto", "Toggle auto-brakes.", "Flight Controls Brakes Toggle Auto", XPlaneCommands.FlightControlsBrakesToggleAuto); } }
        private XPlaneCommand FlightControlsBrakesDnAuto { get { return new XPlaneCommand("sim/flight_controls/brakes_dn_auto", "Auto-brakes down.", "Flight Controls Brakes Dn Auto", XPlaneCommands.FlightControlsBrakesDnAuto); } }
        private XPlaneCommand FlightControlsBrakesUpAuto { get { return new XPlaneCommand("sim/flight_controls/brakes_up_auto", "Auto-brakes up.", "Flight Controls Brakes Up Auto", XPlaneCommands.FlightControlsBrakesUpAuto); } }
        private XPlaneCommand FlightControlsBrakesOffAuto { get { return new XPlaneCommand("sim/flight_controls/brakes_off_auto", "Auto-brakes off/disarm.", "Flight Controls Brakes Off Auto", XPlaneCommands.FlightControlsBrakesOffAuto); } }
        private XPlaneCommand FlightControlsBrakesRtoAuto { get { return new XPlaneCommand("sim/flight_controls/brakes_rto_auto", "Auto-brakes RTO.", "Flight Controls Brakes Rto Auto", XPlaneCommands.FlightControlsBrakesRtoAuto); } }
        private XPlaneCommand FlightControlsBrakes1Auto { get { return new XPlaneCommand("sim/flight_controls/brakes_1_auto", "Auto-brakes 1.", "Flight Controls Brakes1auto", XPlaneCommands.FlightControlsBrakes1Auto); } }
        private XPlaneCommand FlightControlsBrakes2Auto { get { return new XPlaneCommand("sim/flight_controls/brakes_2_auto", "Auto-brakes 2.", "Flight Controls Brakes2auto", XPlaneCommands.FlightControlsBrakes2Auto); } }
        private XPlaneCommand FlightControlsBrakes3Auto { get { return new XPlaneCommand("sim/flight_controls/brakes_3_auto", "Auto-brakes 3.", "Flight Controls Brakes3auto", XPlaneCommands.FlightControlsBrakes3Auto); } }
        private XPlaneCommand FlightControlsBrakesMaxAuto { get { return new XPlaneCommand("sim/flight_controls/brakes_max_auto", "Auto-brakes max.", "Flight Controls Brakes Max Auto", XPlaneCommands.FlightControlsBrakesMaxAuto); } }
        private XPlaneCommand SystemsYawDamperOn { get { return new XPlaneCommand("sim/systems/yaw_damper_on", "Yaw damper on.", "Systems Yaw Damper On", XPlaneCommands.SystemsYawDamperOn); } }
        private XPlaneCommand SystemsYawDamperOff { get { return new XPlaneCommand("sim/systems/yaw_damper_off", "Yaw damper off.", "Systems Yaw Damper Off", XPlaneCommands.SystemsYawDamperOff); } }
        private XPlaneCommand SystemsYawDamperToggle { get { return new XPlaneCommand("sim/systems/yaw_damper_toggle", "Toggle yaw damper.", "Systems Yaw Damper Toggle", XPlaneCommands.SystemsYawDamperToggle); } }
        private XPlaneCommand SystemsPropSyncOn { get { return new XPlaneCommand("sim/systems/prop_sync_on", "Prop sync on.", "Systems Prop Sync On", XPlaneCommands.SystemsPropSyncOn); } }
        private XPlaneCommand SystemsPropSyncOff { get { return new XPlaneCommand("sim/systems/prop_sync_off", "Prop sync off.", "Systems Prop Sync Off", XPlaneCommands.SystemsPropSyncOff); } }
        private XPlaneCommand SystemsPropSyncToggle { get { return new XPlaneCommand("sim/systems/prop_sync_toggle", "Prop sync toggle.", "Systems Prop Sync Toggle", XPlaneCommands.SystemsPropSyncToggle); } }
        private XPlaneCommand SystemsFeatherModeDown { get { return new XPlaneCommand("sim/systems/feather_mode_down", "Auto-feather mode down.", "Systems Feather Mode Down", XPlaneCommands.SystemsFeatherModeDown); } }
        private XPlaneCommand SystemsFeatherModeUp { get { return new XPlaneCommand("sim/systems/feather_mode_up", "Auto-feather mode up.", "Systems Feather Mode Up", XPlaneCommands.SystemsFeatherModeUp); } }
        private XPlaneCommand SystemsFeatherModeOff { get { return new XPlaneCommand("sim/systems/feather_mode_off", "Auto-feather off.", "Systems Feather Mode Off", XPlaneCommands.SystemsFeatherModeOff); } }
        private XPlaneCommand SystemsFeatherModeArm { get { return new XPlaneCommand("sim/systems/feather_mode_arm", "Auto-feather on.", "Systems Feather Mode Arm", XPlaneCommands.SystemsFeatherModeArm); } }
        private XPlaneCommand SystemsFeatherModeTest { get { return new XPlaneCommand("sim/systems/feather_mode_test", "Auto-feather test.", "Systems Feather Mode Test", XPlaneCommands.SystemsFeatherModeTest); } }
        private XPlaneCommand FlightControlsHydraulicOn { get { return new XPlaneCommand("sim/flight_controls/hydraulic_on", "Engine-driven hydraulic pumps on", "Flight Controls Hydraulic On", XPlaneCommands.FlightControlsHydraulicOn); } }
        private XPlaneCommand FlightControlsHydraulicOff { get { return new XPlaneCommand("sim/flight_controls/hydraulic_off", "Engine-driven hydraulic pumps off", "Flight Controls Hydraulic Off", XPlaneCommands.FlightControlsHydraulicOff); } }
        private XPlaneCommand FlightControlsHydraulicTog { get { return new XPlaneCommand("sim/flight_controls/hydraulic_tog", "Engine-driven hydraulic pumps tog", "Flight Controls Hydraulic Tog", XPlaneCommands.FlightControlsHydraulicTog); } }
        private XPlaneCommand FlightControlsTailhookDown { get { return new XPlaneCommand("sim/flight_controls/tailhook_down", "Tailhook down.", "Flight Controls Tailhook Down", XPlaneCommands.FlightControlsTailhookDown); } }
        private XPlaneCommand FlightControlsTailhookUp { get { return new XPlaneCommand("sim/flight_controls/tailhook_up", "Tailhook up.", "Flight Controls Tailhook Up", XPlaneCommands.FlightControlsTailhookUp); } }
        private XPlaneCommand FlightControlsTailhookToggle { get { return new XPlaneCommand("sim/flight_controls/tailhook_toggle", "Toggle the tailhook.", "Flight Controls Tailhook Toggle", XPlaneCommands.FlightControlsTailhookToggle); } }
        private XPlaneCommand FlightControlsCanopyOpen { get { return new XPlaneCommand("sim/flight_controls/canopy_open", "Canopy open.", "Flight Controls Canopy Open", XPlaneCommands.FlightControlsCanopyOpen); } }
        private XPlaneCommand FlightControlsCanopyClose { get { return new XPlaneCommand("sim/flight_controls/canopy_close", "Canopy close.", "Flight Controls Canopy Close", XPlaneCommands.FlightControlsCanopyClose); } }
        private XPlaneCommand FlightControlsCanopyToggle { get { return new XPlaneCommand("sim/flight_controls/canopy_toggle", "Canopy toggle.", "Flight Controls Canopy Toggle", XPlaneCommands.FlightControlsCanopyToggle); } }
        private XPlaneCommand FlightControlsRotorBrakeToggle { get { return new XPlaneCommand("sim/flight_controls/rotor_brake_toggle", "Toggle rotor brake.", "Flight Controls Rotor Brake Toggle", XPlaneCommands.FlightControlsRotorBrakeToggle); } }
        private XPlaneCommand FlightControlsHotelModeToggle { get { return new XPlaneCommand("sim/flight_controls/hotel_mode_toggle", "Toggle hotel mode.", "Flight Controls Hotel Mode Toggle", XPlaneCommands.FlightControlsHotelModeToggle); } }
        private XPlaneCommand SystemsArtificialStabilityToggle { get { return new XPlaneCommand("sim/systems/artificial_stability_toggle", "Toggle artificial stability power.", "Systems Artificial Stability Toggle", XPlaneCommands.SystemsArtificialStabilityToggle); } }
        private XPlaneCommand FlightControlsPuffersToggle { get { return new XPlaneCommand("sim/flight_controls/puffers_toggle", "Toggle puffers.", "Flight Controls Puffers Toggle", XPlaneCommands.FlightControlsPuffersToggle); } }
        private XPlaneCommand EnginesRocketsUp { get { return new XPlaneCommand("sim/engines/rockets_up", "Orbital maneuver rockets up.", "Engines Rockets Up", XPlaneCommands.EnginesRocketsUp); } }
        private XPlaneCommand EnginesRocketsDown { get { return new XPlaneCommand("sim/engines/rockets_down", "Orbital maneuver rockets down.", "Engines Rockets Down", XPlaneCommands.EnginesRocketsDown); } }
        private XPlaneCommand EnginesRocketsLeft { get { return new XPlaneCommand("sim/engines/rockets_left", "Orbital maneuver rockets left.", "Engines Rockets Left", XPlaneCommands.EnginesRocketsLeft); } }
        private XPlaneCommand EnginesRocketsRight { get { return new XPlaneCommand("sim/engines/rockets_right", "Orbital maneuver rockets right.", "Engines Rockets Right", XPlaneCommands.EnginesRocketsRight); } }
        private XPlaneCommand EnginesRocketsForward { get { return new XPlaneCommand("sim/engines/rockets_forward", "Orbital maneuver rockets fore.", "Engines Rockets Forward", XPlaneCommands.EnginesRocketsForward); } }
        private XPlaneCommand EnginesRocketsAft { get { return new XPlaneCommand("sim/engines/rockets_aft", "Orbital maneuver rockets aft.", "Engines Rockets Aft", XPlaneCommands.EnginesRocketsAft); } }
        private XPlaneCommand FuelFuelTankSelectorLftOne { get { return new XPlaneCommand("sim/fuel/fuel_tank_selector_lft_one", "Select fuel tank left one.", "Fuel Fuel Tank Selector Lft One", XPlaneCommands.FuelFuelTankSelectorLftOne); } }
        private XPlaneCommand FuelFuelTankSelectorRgtOne { get { return new XPlaneCommand("sim/fuel/fuel_tank_selector_rgt_one", "Select fuel tank right one.", "Fuel Fuel Tank Selector Rgt One", XPlaneCommands.FuelFuelTankSelectorRgtOne); } }
        private XPlaneCommand FuelFuelTankPump1On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_1_on", "Fuel pump for tank #1 on.", "Fuel Fuel Tank Pump1on", XPlaneCommands.FuelFuelTankPump1On); } }
        private XPlaneCommand FuelFuelTankPump2On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_2_on", "Fuel pump for tank #2 on.", "Fuel Fuel Tank Pump2on", XPlaneCommands.FuelFuelTankPump2On); } }
        private XPlaneCommand FuelFuelTankPump3On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_3_on", "Fuel pump for tank #3 on.", "Fuel Fuel Tank Pump3on", XPlaneCommands.FuelFuelTankPump3On); } }
        private XPlaneCommand FuelFuelTankPump4On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_4_on", "Fuel pump for tank #4 on.", "Fuel Fuel Tank Pump4on", XPlaneCommands.FuelFuelTankPump4On); } }
        private XPlaneCommand FuelFuelTankPump5On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_5_on", "Fuel pump for tank #5 on.", "Fuel Fuel Tank Pump5on", XPlaneCommands.FuelFuelTankPump5On); } }
        private XPlaneCommand FuelFuelTankPump6On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_6_on", "Fuel pump for tank #6 on.", "Fuel Fuel Tank Pump6on", XPlaneCommands.FuelFuelTankPump6On); } }
        private XPlaneCommand FuelFuelTankPump7On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_7_on", "Fuel pump for tank #7 on.", "Fuel Fuel Tank Pump7on", XPlaneCommands.FuelFuelTankPump7On); } }
        private XPlaneCommand FuelFuelTankPump8On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_8_on", "Fuel pump for tank #8 on.", "Fuel Fuel Tank Pump8on", XPlaneCommands.FuelFuelTankPump8On); } }
        private XPlaneCommand FuelFuelTankPump9On { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_9_on", "Fuel pump for tank #9 on.", "Fuel Fuel Tank Pump9on", XPlaneCommands.FuelFuelTankPump9On); } }
        private XPlaneCommand FuelFuelTankPump1Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_1_off", "Fuel pump for tank #1 off.", "Fuel Fuel Tank Pump1off", XPlaneCommands.FuelFuelTankPump1Off); } }
        private XPlaneCommand FuelFuelTankPump2Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_2_off", "Fuel pump for tank #2 off.", "Fuel Fuel Tank Pump2off", XPlaneCommands.FuelFuelTankPump2Off); } }
        private XPlaneCommand FuelFuelTankPump3Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_3_off", "Fuel pump for tank #3 off.", "Fuel Fuel Tank Pump3off", XPlaneCommands.FuelFuelTankPump3Off); } }
        private XPlaneCommand FuelFuelTankPump4Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_4_off", "Fuel pump for tank #4 off.", "Fuel Fuel Tank Pump4off", XPlaneCommands.FuelFuelTankPump4Off); } }
        private XPlaneCommand FuelFuelTankPump5Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_5_off", "Fuel pump for tank #5 off.", "Fuel Fuel Tank Pump5off", XPlaneCommands.FuelFuelTankPump5Off); } }
        private XPlaneCommand FuelFuelTankPump6Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_6_off", "Fuel pump for tank #6 off.", "Fuel Fuel Tank Pump6off", XPlaneCommands.FuelFuelTankPump6Off); } }
        private XPlaneCommand FuelFuelTankPump7Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_7_off", "Fuel pump for tank #7 off.", "Fuel Fuel Tank Pump7off", XPlaneCommands.FuelFuelTankPump7Off); } }
        private XPlaneCommand FuelFuelTankPump8Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_8_off", "Fuel pump for tank #8 off.", "Fuel Fuel Tank Pump8off", XPlaneCommands.FuelFuelTankPump8Off); } }
        private XPlaneCommand FuelFuelTankPump9Off { get { return new XPlaneCommand("sim/fuel/fuel_tank_pump_9_off", "Fuel pump for tank #9 off.", "Fuel Fuel Tank Pump9off", XPlaneCommands.FuelFuelTankPump9Off); } }
        private XPlaneCommand FuelFuelSelectorNone { get { return new XPlaneCommand("sim/fuel/fuel_selector_none", "Set fuel selector to none (shut off).", "Fuel Fuel Selector None", XPlaneCommands.FuelFuelSelectorNone); } }
        private XPlaneCommand FuelFuelSelectorLft { get { return new XPlaneCommand("sim/fuel/fuel_selector_lft", "Set fuel selector to left tanks.", "Fuel Fuel Selector Lft", XPlaneCommands.FuelFuelSelectorLft); } }
        private XPlaneCommand FuelFuelSelectorCtr { get { return new XPlaneCommand("sim/fuel/fuel_selector_ctr", "Set fuel selector to center tanks.", "Fuel Fuel Selector Ctr", XPlaneCommands.FuelFuelSelectorCtr); } }
        private XPlaneCommand FuelFuelSelectorRgt { get { return new XPlaneCommand("sim/fuel/fuel_selector_rgt", "Set fuel selector to right tanks.", "Fuel Fuel Selector Rgt", XPlaneCommands.FuelFuelSelectorRgt); } }
        private XPlaneCommand FuelFuelSelectorAll { get { return new XPlaneCommand("sim/fuel/fuel_selector_all", "Set fuel selector to all tanks.", "Fuel Fuel Selector All", XPlaneCommands.FuelFuelSelectorAll); } }
        private XPlaneCommand FuelLeftFuelSelectorNone { get { return new XPlaneCommand("sim/fuel/left_fuel_selector_none", "Set fuel selector for left engine to none (shut off).", "Fuel Left Fuel Selector None", XPlaneCommands.FuelLeftFuelSelectorNone); } }
        private XPlaneCommand FuelLeftFuelSelectorLft { get { return new XPlaneCommand("sim/fuel/left_fuel_selector_lft", "Set fuel selector for left engine to left tanks.", "Fuel Left Fuel Selector Lft", XPlaneCommands.FuelLeftFuelSelectorLft); } }
        private XPlaneCommand FuelLeftFuelSelectorCtr { get { return new XPlaneCommand("sim/fuel/left_fuel_selector_ctr", "Set fuel selector for left engine to center tanks.", "Fuel Left Fuel Selector Ctr", XPlaneCommands.FuelLeftFuelSelectorCtr); } }
        private XPlaneCommand FuelLeftFuelSelectorRgt { get { return new XPlaneCommand("sim/fuel/left_fuel_selector_rgt", "Set fuel selector for left engine to right tanks.", "Fuel Left Fuel Selector Rgt", XPlaneCommands.FuelLeftFuelSelectorRgt); } }
        private XPlaneCommand FuelLeftFuelSelectorAll { get { return new XPlaneCommand("sim/fuel/left_fuel_selector_all", "Set fuel selector for left engine to all tanks.", "Fuel Left Fuel Selector All", XPlaneCommands.FuelLeftFuelSelectorAll); } }
        private XPlaneCommand FuelRightFuelSelectorNone { get { return new XPlaneCommand("sim/fuel/right_fuel_selector_none", "Set fuel selector for right engine to none (shut off).", "Fuel Right Fuel Selector None", XPlaneCommands.FuelRightFuelSelectorNone); } }
        private XPlaneCommand FuelRightFuelSelectorLft { get { return new XPlaneCommand("sim/fuel/right_fuel_selector_lft", "Set fuel selector for right engine to left tanks.", "Fuel Right Fuel Selector Lft", XPlaneCommands.FuelRightFuelSelectorLft); } }
        private XPlaneCommand FuelRightFuelSelectorCtr { get { return new XPlaneCommand("sim/fuel/right_fuel_selector_ctr", "Set fuel selector for right engine to center tanks.", "Fuel Right Fuel Selector Ctr", XPlaneCommands.FuelRightFuelSelectorCtr); } }
        private XPlaneCommand FuelRightFuelSelectorRgt { get { return new XPlaneCommand("sim/fuel/right_fuel_selector_rgt", "Set fuel selector for right engine to right tanks.", "Fuel Right Fuel Selector Rgt", XPlaneCommands.FuelRightFuelSelectorRgt); } }
        private XPlaneCommand FuelRightFuelSelectorAll { get { return new XPlaneCommand("sim/fuel/right_fuel_selector_all", "Set fuel selector for right engine to all tanks.", "Fuel Right Fuel Selector All", XPlaneCommands.FuelRightFuelSelectorAll); } }
        private XPlaneCommand FuelFuelTransferToLft { get { return new XPlaneCommand("sim/fuel/fuel_transfer_to_lft", "Transfer fuel to left.", "Fuel Fuel Transfer To Lft", XPlaneCommands.FuelFuelTransferToLft); } }
        private XPlaneCommand FuelFuelTransferToCtr { get { return new XPlaneCommand("sim/fuel/fuel_transfer_to_ctr", "Transfer fuel to center.", "Fuel Fuel Transfer To Ctr", XPlaneCommands.FuelFuelTransferToCtr); } }
        private XPlaneCommand FuelFuelTransferToRgt { get { return new XPlaneCommand("sim/fuel/fuel_transfer_to_rgt", "Transfer fuel to right.", "Fuel Fuel Transfer To Rgt", XPlaneCommands.FuelFuelTransferToRgt); } }
        private XPlaneCommand FuelFuelTransferToAft { get { return new XPlaneCommand("sim/fuel/fuel_transfer_to_aft", "Transfer fuel to aft.", "Fuel Fuel Transfer To Aft", XPlaneCommands.FuelFuelTransferToAft); } }
        private XPlaneCommand FuelFuelTransferToOff { get { return new XPlaneCommand("sim/fuel/fuel_transfer_to_off", "Transfer fuel to none.", "Fuel Fuel Transfer To Off", XPlaneCommands.FuelFuelTransferToOff); } }
        private XPlaneCommand FuelFuelTransferFromLft { get { return new XPlaneCommand("sim/fuel/fuel_transfer_from_lft", "Transfer fuel from left.", "Fuel Fuel Transfer From Lft", XPlaneCommands.FuelFuelTransferFromLft); } }
        private XPlaneCommand FuelFuelTransferFromCtr { get { return new XPlaneCommand("sim/fuel/fuel_transfer_from_ctr", "Transfer fuel from center.", "Fuel Fuel Transfer From Ctr", XPlaneCommands.FuelFuelTransferFromCtr); } }
        private XPlaneCommand FuelFuelTransferFromRgt { get { return new XPlaneCommand("sim/fuel/fuel_transfer_from_rgt", "Transfer fuel from right.", "Fuel Fuel Transfer From Rgt", XPlaneCommands.FuelFuelTransferFromRgt); } }
        private XPlaneCommand FuelFuelTransferFromAft { get { return new XPlaneCommand("sim/fuel/fuel_transfer_from_aft", "Transfer fuel from aft.", "Fuel Fuel Transfer From Aft", XPlaneCommands.FuelFuelTransferFromAft); } }
        private XPlaneCommand FuelFuelTransferFromOff { get { return new XPlaneCommand("sim/fuel/fuel_transfer_from_off", "Transfer fuel from none.", "Fuel Fuel Transfer From Off", XPlaneCommands.FuelFuelTransferFromOff); } }
        private XPlaneCommand FuelFuelCrossfeedFromLftTank { get { return new XPlaneCommand("sim/fuel/fuel_crossfeed_from_lft_tank", "Cross-feed fuel from left tank.", "Fuel Fuel Crossfeed From Lft Tank", XPlaneCommands.FuelFuelCrossfeedFromLftTank); } }
        private XPlaneCommand FuelFuelCrossfeedOff { get { return new XPlaneCommand("sim/fuel/fuel_crossfeed_off", "Cross-feed fuel off.", "Fuel Fuel Crossfeed Off", XPlaneCommands.FuelFuelCrossfeedOff); } }
        private XPlaneCommand FuelFuelCrossfeedFromRgtTank { get { return new XPlaneCommand("sim/fuel/fuel_crossfeed_from_rgt_tank", "Cross-feed fuel from right tank.", "Fuel Fuel Crossfeed From Rgt Tank", XPlaneCommands.FuelFuelCrossfeedFromRgtTank); } }
        private XPlaneCommand FuelFuelFirewallValveLftOpen { get { return new XPlaneCommand("sim/fuel/fuel_firewall_valve_lft_open", "Firewall fuel valve left open.", "Fuel Fuel Firewall Valve Lft Open", XPlaneCommands.FuelFuelFirewallValveLftOpen); } }
        private XPlaneCommand FuelFuelFirewallValveLftClosed { get { return new XPlaneCommand("sim/fuel/fuel_firewall_valve_lft_closed", "Firewall fuel valve left closed.", "Fuel Fuel Firewall Valve Lft Closed", XPlaneCommands.FuelFuelFirewallValveLftClosed); } }
        private XPlaneCommand FuelFuelFirewallValveRgtOpen { get { return new XPlaneCommand("sim/fuel/fuel_firewall_valve_rgt_open", "Firewall fuel valve right open.", "Fuel Fuel Firewall Valve Rgt Open", XPlaneCommands.FuelFuelFirewallValveRgtOpen); } }
        private XPlaneCommand FuelFuelFirewallValveRgtClosed { get { return new XPlaneCommand("sim/fuel/fuel_firewall_valve_rgt_closed", "Firewall fuel valve right closed.", "Fuel Fuel Firewall Valve Rgt Closed", XPlaneCommands.FuelFuelFirewallValveRgtClosed); } }
        private XPlaneCommand FuelLeftXferOverride { get { return new XPlaneCommand("sim/fuel/left_xfer_override", "Aux to feeder transfer left override.", "Fuel Left Xfer Override", XPlaneCommands.FuelLeftXferOverride); } }
        private XPlaneCommand FuelLeftXferOn { get { return new XPlaneCommand("sim/fuel/left_xfer_on", "Aux to feeder transfer left on.", "Fuel Left Xfer On", XPlaneCommands.FuelLeftXferOn); } }
        private XPlaneCommand FuelLeftXferOff { get { return new XPlaneCommand("sim/fuel/left_xfer_off", "Aux to feeder transfer left off.", "Fuel Left Xfer Off", XPlaneCommands.FuelLeftXferOff); } }
        private XPlaneCommand FuelLeftXferUp { get { return new XPlaneCommand("sim/fuel/left_xfer_up", "Aux to feeder transfer left off->on->overide.", "Fuel Left Xfer Up", XPlaneCommands.FuelLeftXferUp); } }
        private XPlaneCommand FuelLeftXferDn { get { return new XPlaneCommand("sim/fuel/left_xfer_dn", "Aux to feeder transfer left override->on->off.", "Fuel Left Xfer Dn", XPlaneCommands.FuelLeftXferDn); } }
        private XPlaneCommand FuelRightXferOverride { get { return new XPlaneCommand("sim/fuel/right_xfer_override", "Aux to feeder transfer right override.", "Fuel Right Xfer Override", XPlaneCommands.FuelRightXferOverride); } }
        private XPlaneCommand FuelRightXferOn { get { return new XPlaneCommand("sim/fuel/right_xfer_on", "Aux to feeder transfer right on.", "Fuel Right Xfer On", XPlaneCommands.FuelRightXferOn); } }
        private XPlaneCommand FuelRightXferOff { get { return new XPlaneCommand("sim/fuel/right_xfer_off", "Aux to feeder transfer right off.", "Fuel Right Xfer Off", XPlaneCommands.FuelRightXferOff); } }
        private XPlaneCommand FuelRightXferUp { get { return new XPlaneCommand("sim/fuel/right_xfer_up", "Aux to feeder transfer right off->on->overide.", "Fuel Right Xfer Up", XPlaneCommands.FuelRightXferUp); } }
        private XPlaneCommand FuelRightXferDn { get { return new XPlaneCommand("sim/fuel/right_xfer_dn", "Aux to feeder transfer right override->on->off.", "Fuel Right Xfer Dn", XPlaneCommands.FuelRightXferDn); } }
        private XPlaneCommand FuelLeftXferTest { get { return new XPlaneCommand("sim/fuel/left_xfer_test", "Aux to feeder transfer test left.", "Fuel Left Xfer Test", XPlaneCommands.FuelLeftXferTest); } }
        private XPlaneCommand FuelRightXferTest { get { return new XPlaneCommand("sim/fuel/right_xfer_test", "Aux to feeder transfer test right.", "Fuel Right Xfer Test", XPlaneCommands.FuelRightXferTest); } }
        private XPlaneCommand FuelAutoCrossfeedOnOpen { get { return new XPlaneCommand("sim/fuel/auto_crossfeed_on_open", "Crossfeed valve open.", "Fuel Auto Crossfeed On Open", XPlaneCommands.FuelAutoCrossfeedOnOpen); } }
        private XPlaneCommand FuelAutoCrossfeedAuto { get { return new XPlaneCommand("sim/fuel/auto_crossfeed_auto", "Open crossfeed valve when pressure difference detected.", "Fuel Auto Crossfeed Auto", XPlaneCommands.FuelAutoCrossfeedAuto); } }
        private XPlaneCommand FuelAutoCrossfeedOff { get { return new XPlaneCommand("sim/fuel/auto_crossfeed_off", "Close crossfeed valve and turn off auto-crossfeed.", "Fuel Auto Crossfeed Off", XPlaneCommands.FuelAutoCrossfeedOff); } }
        private XPlaneCommand FuelAutoCrossfeedUp { get { return new XPlaneCommand("sim/fuel/auto_crossfeed_up", "Auto-crossfeed off->auto->on.", "Fuel Auto Crossfeed Up", XPlaneCommands.FuelAutoCrossfeedUp); } }
        private XPlaneCommand FuelAutoCrossfeedDown { get { return new XPlaneCommand("sim/fuel/auto_crossfeed_down", "Auto-crossfeed on->auto->off.", "Fuel Auto Crossfeed Down", XPlaneCommands.FuelAutoCrossfeedDown); } }
        private XPlaneCommand FuelFuelPumpsOn { get { return new XPlaneCommand("sim/fuel/fuel_pumps_on", "Fuel pumps on.", "Fuel Fuel Pumps On", XPlaneCommands.FuelFuelPumpsOn); } }
        private XPlaneCommand FuelFuelPumpsOff { get { return new XPlaneCommand("sim/fuel/fuel_pumps_off", "Fuel pumps off.", "Fuel Fuel Pumps Off", XPlaneCommands.FuelFuelPumpsOff); } }
        private XPlaneCommand FuelFuelPumpsTog { get { return new XPlaneCommand("sim/fuel/fuel_pumps_tog", "Fuel pumps toggle.", "Fuel Fuel Pumps Tog", XPlaneCommands.FuelFuelPumpsTog); } }
        private XPlaneCommand FuelFuelPump1On { get { return new XPlaneCommand("sim/fuel/fuel_pump_1_on", "Fuel pump for engine #1 on.", "Fuel Fuel Pump1on", XPlaneCommands.FuelFuelPump1On); } }
        private XPlaneCommand FuelFuelPump2On { get { return new XPlaneCommand("sim/fuel/fuel_pump_2_on", "Fuel pump for engine #2 on.", "Fuel Fuel Pump2on", XPlaneCommands.FuelFuelPump2On); } }
        private XPlaneCommand FuelFuelPump3On { get { return new XPlaneCommand("sim/fuel/fuel_pump_3_on", "Fuel pump for engine #3 on.", "Fuel Fuel Pump3on", XPlaneCommands.FuelFuelPump3On); } }
        private XPlaneCommand FuelFuelPump4On { get { return new XPlaneCommand("sim/fuel/fuel_pump_4_on", "Fuel pump for engine #4 on.", "Fuel Fuel Pump4on", XPlaneCommands.FuelFuelPump4On); } }
        private XPlaneCommand FuelFuelPump5On { get { return new XPlaneCommand("sim/fuel/fuel_pump_5_on", "Fuel pump for engine #5 on.", "Fuel Fuel Pump5on", XPlaneCommands.FuelFuelPump5On); } }
        private XPlaneCommand FuelFuelPump6On { get { return new XPlaneCommand("sim/fuel/fuel_pump_6_on", "Fuel pump for engine #6 on.", "Fuel Fuel Pump6on", XPlaneCommands.FuelFuelPump6On); } }
        private XPlaneCommand FuelFuelPump7On { get { return new XPlaneCommand("sim/fuel/fuel_pump_7_on", "Fuel pump for engine #7 on.", "Fuel Fuel Pump7on", XPlaneCommands.FuelFuelPump7On); } }
        private XPlaneCommand FuelFuelPump8On { get { return new XPlaneCommand("sim/fuel/fuel_pump_8_on", "Fuel pump for engine #8 on.", "Fuel Fuel Pump8on", XPlaneCommands.FuelFuelPump8On); } }
        private XPlaneCommand FuelFuelPump1Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_1_off", "Fuel pump for engine #1 off.", "Fuel Fuel Pump1off", XPlaneCommands.FuelFuelPump1Off); } }
        private XPlaneCommand FuelFuelPump2Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_2_off", "Fuel pump for engine #2 off.", "Fuel Fuel Pump2off", XPlaneCommands.FuelFuelPump2Off); } }
        private XPlaneCommand FuelFuelPump3Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_3_off", "Fuel pump for engine #3 off.", "Fuel Fuel Pump3off", XPlaneCommands.FuelFuelPump3Off); } }
        private XPlaneCommand FuelFuelPump4Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_4_off", "Fuel pump for engine #4 off.", "Fuel Fuel Pump4off", XPlaneCommands.FuelFuelPump4Off); } }
        private XPlaneCommand FuelFuelPump5Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_5_off", "Fuel pump for engine #5 off.", "Fuel Fuel Pump5off", XPlaneCommands.FuelFuelPump5Off); } }
        private XPlaneCommand FuelFuelPump6Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_6_off", "Fuel pump for engine #6 off.", "Fuel Fuel Pump6off", XPlaneCommands.FuelFuelPump6Off); } }
        private XPlaneCommand FuelFuelPump7Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_7_off", "Fuel pump for engine #7 off.", "Fuel Fuel Pump7off", XPlaneCommands.FuelFuelPump7Off); } }
        private XPlaneCommand FuelFuelPump8Off { get { return new XPlaneCommand("sim/fuel/fuel_pump_8_off", "Fuel pump for engine #8 off.", "Fuel Fuel Pump8off", XPlaneCommands.FuelFuelPump8Off); } }
        private XPlaneCommand FuelFuelPump1Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_1_tog", "Fuel pump for engine #1 toggle.", "Fuel Fuel Pump1tog", XPlaneCommands.FuelFuelPump1Tog); } }
        private XPlaneCommand FuelFuelPump2Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_2_tog", "Fuel pump for engine #2 toggle.", "Fuel Fuel Pump2tog", XPlaneCommands.FuelFuelPump2Tog); } }
        private XPlaneCommand FuelFuelPump3Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_3_tog", "Fuel pump for engine #3 toggle.", "Fuel Fuel Pump3tog", XPlaneCommands.FuelFuelPump3Tog); } }
        private XPlaneCommand FuelFuelPump4Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_4_tog", "Fuel pump for engine #4 toggle.", "Fuel Fuel Pump4tog", XPlaneCommands.FuelFuelPump4Tog); } }
        private XPlaneCommand FuelFuelPump5Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_5_tog", "Fuel pump for engine #5 toggle.", "Fuel Fuel Pump5tog", XPlaneCommands.FuelFuelPump5Tog); } }
        private XPlaneCommand FuelFuelPump6Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_6_tog", "Fuel pump for engine #6 toggle.", "Fuel Fuel Pump6tog", XPlaneCommands.FuelFuelPump6Tog); } }
        private XPlaneCommand FuelFuelPump7Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_7_tog", "Fuel pump for engine #7 toggle.", "Fuel Fuel Pump7tog", XPlaneCommands.FuelFuelPump7Tog); } }
        private XPlaneCommand FuelFuelPump8Tog { get { return new XPlaneCommand("sim/fuel/fuel_pump_8_tog", "Fuel pump for engine #8 toggle.", "Fuel Fuel Pump8tog", XPlaneCommands.FuelFuelPump8Tog); } }
        private XPlaneCommand FuelFuelPump1Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_1_prime", "Fuel pump prime for engine #1 on.", "Fuel Fuel Pump1prime", XPlaneCommands.FuelFuelPump1Prime); } }
        private XPlaneCommand FuelFuelPump2Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_2_prime", "Fuel pump prime for engine #2 on.", "Fuel Fuel Pump2prime", XPlaneCommands.FuelFuelPump2Prime); } }
        private XPlaneCommand FuelFuelPump3Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_3_prime", "Fuel pump prime for engine #3 on.", "Fuel Fuel Pump3prime", XPlaneCommands.FuelFuelPump3Prime); } }
        private XPlaneCommand FuelFuelPump4Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_4_prime", "Fuel pump prime for engine #4 on.", "Fuel Fuel Pump4prime", XPlaneCommands.FuelFuelPump4Prime); } }
        private XPlaneCommand FuelFuelPump5Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_5_prime", "Fuel pump prime for engine #5 on.", "Fuel Fuel Pump5prime", XPlaneCommands.FuelFuelPump5Prime); } }
        private XPlaneCommand FuelFuelPump6Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_6_prime", "Fuel pump prime for engine #6 on.", "Fuel Fuel Pump6prime", XPlaneCommands.FuelFuelPump6Prime); } }
        private XPlaneCommand FuelFuelPump7Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_7_prime", "Fuel pump prime for engine #7 on.", "Fuel Fuel Pump7prime", XPlaneCommands.FuelFuelPump7Prime); } }
        private XPlaneCommand FuelFuelPump8Prime { get { return new XPlaneCommand("sim/fuel/fuel_pump_8_prime", "Fuel pump prime for engine #8 on.", "Fuel Fuel Pump8prime", XPlaneCommands.FuelFuelPump8Prime); } }
        private XPlaneCommand ElectricalCrossTieOn { get { return new XPlaneCommand("sim/electrical/cross_tie_on", "Cross-tie on.", "Electrical Cross Tie On", XPlaneCommands.ElectricalCrossTieOn); } }
        private XPlaneCommand ElectricalCrossTieOff { get { return new XPlaneCommand("sim/electrical/cross_tie_off", "Cross-tie off.", "Electrical Cross Tie Off", XPlaneCommands.ElectricalCrossTieOff); } }
        private XPlaneCommand ElectricalCrossTieToggle { get { return new XPlaneCommand("sim/electrical/cross_tie_toggle", "Cross-tie toggle.", "Electrical Cross Tie Toggle", XPlaneCommands.ElectricalCrossTieToggle); } }
        private XPlaneCommand ElectricalInvertersOn { get { return new XPlaneCommand("sim/electrical/inverters_on", "Inverters on.", "Electrical Inverters On", XPlaneCommands.ElectricalInvertersOn); } }
        private XPlaneCommand ElectricalInvertersOff { get { return new XPlaneCommand("sim/electrical/inverters_off", "Inverters off.", "Electrical Inverters Off", XPlaneCommands.ElectricalInvertersOff); } }
        private XPlaneCommand ElectricalInvertersToggle { get { return new XPlaneCommand("sim/electrical/inverters_toggle", "Inverters toggle.", "Electrical Inverters Toggle", XPlaneCommands.ElectricalInvertersToggle); } }
        private XPlaneCommand ElectricalInverter1On { get { return new XPlaneCommand("sim/electrical/inverter_1_on", "Inverter 1 on.", "Electrical Inverter1on", XPlaneCommands.ElectricalInverter1On); } }
        private XPlaneCommand ElectricalInverter1Off { get { return new XPlaneCommand("sim/electrical/inverter_1_off", "Inverter 1 off.", "Electrical Inverter1off", XPlaneCommands.ElectricalInverter1Off); } }
        private XPlaneCommand ElectricalInverter1Toggle { get { return new XPlaneCommand("sim/electrical/inverter_1_toggle", "Inverter 1 toggle.", "Electrical Inverter1toggle", XPlaneCommands.ElectricalInverter1Toggle); } }
        private XPlaneCommand ElectricalInverter2On { get { return new XPlaneCommand("sim/electrical/inverter_2_on", "Inverter 2 on.", "Electrical Inverter2on", XPlaneCommands.ElectricalInverter2On); } }
        private XPlaneCommand ElectricalInverter2Off { get { return new XPlaneCommand("sim/electrical/inverter_2_off", "Inverter 2 off.", "Electrical Inverter2off", XPlaneCommands.ElectricalInverter2Off); } }
        private XPlaneCommand ElectricalInverter2Toggle { get { return new XPlaneCommand("sim/electrical/inverter_2_toggle", "Inverter 2 toggle.", "Electrical Inverter2toggle", XPlaneCommands.ElectricalInverter2Toggle); } }
        private XPlaneCommand ElectricalBatteriesToggle { get { return new XPlaneCommand("sim/electrical/batteries_toggle", "Batteries all toggle.", "Electrical Batteries Toggle", XPlaneCommands.ElectricalBatteriesToggle); } }
        private XPlaneCommand ElectricalBattery1On { get { return new XPlaneCommand("sim/electrical/battery_1_on", "Battery 1 on.", "Electrical Battery1on", XPlaneCommands.ElectricalBattery1On); } }
        private XPlaneCommand ElectricalBattery2On { get { return new XPlaneCommand("sim/electrical/battery_2_on", "Battery 2 on.", "Electrical Battery2on", XPlaneCommands.ElectricalBattery2On); } }
        private XPlaneCommand ElectricalBattery1Off { get { return new XPlaneCommand("sim/electrical/battery_1_off", "Battery 1 off.", "Electrical Battery1off", XPlaneCommands.ElectricalBattery1Off); } }
        private XPlaneCommand ElectricalBattery2Off { get { return new XPlaneCommand("sim/electrical/battery_2_off", "Battery 2 off.", "Electrical Battery2off", XPlaneCommands.ElectricalBattery2Off); } }
        private XPlaneCommand ElectricalBattery1Toggle { get { return new XPlaneCommand("sim/electrical/battery_1_toggle", "Battery 1 toggle.", "Electrical Battery1toggle", XPlaneCommands.ElectricalBattery1Toggle); } }
        private XPlaneCommand ElectricalBattery2Toggle { get { return new XPlaneCommand("sim/electrical/battery_2_toggle", "Battery 2 toggle.", "Electrical Battery2toggle", XPlaneCommands.ElectricalBattery2Toggle); } }
        private XPlaneCommand ElectricalGeneratorsToggle { get { return new XPlaneCommand("sim/electrical/generators_toggle", "Generators all toggle.", "Electrical Generators Toggle", XPlaneCommands.ElectricalGeneratorsToggle); } }
        private XPlaneCommand ElectricalGenerator1Off { get { return new XPlaneCommand("sim/electrical/generator_1_off", "Generator #1 off.", "Electrical Generator1off", XPlaneCommands.ElectricalGenerator1Off); } }
        private XPlaneCommand ElectricalGenerator2Off { get { return new XPlaneCommand("sim/electrical/generator_2_off", "Generator #2 off.", "Electrical Generator2off", XPlaneCommands.ElectricalGenerator2Off); } }
        private XPlaneCommand ElectricalGenerator3Off { get { return new XPlaneCommand("sim/electrical/generator_3_off", "Generator #3 off.", "Electrical Generator3off", XPlaneCommands.ElectricalGenerator3Off); } }
        private XPlaneCommand ElectricalGenerator4Off { get { return new XPlaneCommand("sim/electrical/generator_4_off", "Generator #4 off.", "Electrical Generator4off", XPlaneCommands.ElectricalGenerator4Off); } }
        private XPlaneCommand ElectricalGenerator5Off { get { return new XPlaneCommand("sim/electrical/generator_5_off", "Generator #5 off.", "Electrical Generator5off", XPlaneCommands.ElectricalGenerator5Off); } }
        private XPlaneCommand ElectricalGenerator6Off { get { return new XPlaneCommand("sim/electrical/generator_6_off", "Generator #6 off.", "Electrical Generator6off", XPlaneCommands.ElectricalGenerator6Off); } }
        private XPlaneCommand ElectricalGenerator7Off { get { return new XPlaneCommand("sim/electrical/generator_7_off", "Generator #7 off.", "Electrical Generator7off", XPlaneCommands.ElectricalGenerator7Off); } }
        private XPlaneCommand ElectricalGenerator8Off { get { return new XPlaneCommand("sim/electrical/generator_8_off", "Generator #8 off.", "Electrical Generator8off", XPlaneCommands.ElectricalGenerator8Off); } }
        private XPlaneCommand ElectricalGenerator1On { get { return new XPlaneCommand("sim/electrical/generator_1_on", "Generator #1 on.", "Electrical Generator1on", XPlaneCommands.ElectricalGenerator1On); } }
        private XPlaneCommand ElectricalGenerator2On { get { return new XPlaneCommand("sim/electrical/generator_2_on", "Generator #2 on.", "Electrical Generator2on", XPlaneCommands.ElectricalGenerator2On); } }
        private XPlaneCommand ElectricalGenerator3On { get { return new XPlaneCommand("sim/electrical/generator_3_on", "Generator #3 on.", "Electrical Generator3on", XPlaneCommands.ElectricalGenerator3On); } }
        private XPlaneCommand ElectricalGenerator4On { get { return new XPlaneCommand("sim/electrical/generator_4_on", "Generator #4 on.", "Electrical Generator4on", XPlaneCommands.ElectricalGenerator4On); } }
        private XPlaneCommand ElectricalGenerator5On { get { return new XPlaneCommand("sim/electrical/generator_5_on", "Generator #5 on.", "Electrical Generator5on", XPlaneCommands.ElectricalGenerator5On); } }
        private XPlaneCommand ElectricalGenerator6On { get { return new XPlaneCommand("sim/electrical/generator_6_on", "Generator #6 on.", "Electrical Generator6on", XPlaneCommands.ElectricalGenerator6On); } }
        private XPlaneCommand ElectricalGenerator7On { get { return new XPlaneCommand("sim/electrical/generator_7_on", "Generator #7 on.", "Electrical Generator7on", XPlaneCommands.ElectricalGenerator7On); } }
        private XPlaneCommand ElectricalGenerator8On { get { return new XPlaneCommand("sim/electrical/generator_8_on", "Generator #8 on.", "Electrical Generator8on", XPlaneCommands.ElectricalGenerator8On); } }
        private XPlaneCommand ElectricalGenerator1Toggle { get { return new XPlaneCommand("sim/electrical/generator_1_toggle", "Generator #1 toggle.", "Electrical Generator1toggle", XPlaneCommands.ElectricalGenerator1Toggle); } }
        private XPlaneCommand ElectricalGenerator2Toggle { get { return new XPlaneCommand("sim/electrical/generator_2_toggle", "Generator #2 toggle.", "Electrical Generator2toggle", XPlaneCommands.ElectricalGenerator2Toggle); } }
        private XPlaneCommand ElectricalGenerator3Toggle { get { return new XPlaneCommand("sim/electrical/generator_3_toggle", "Generator #3 toggle.", "Electrical Generator3toggle", XPlaneCommands.ElectricalGenerator3Toggle); } }
        private XPlaneCommand ElectricalGenerator4Toggle { get { return new XPlaneCommand("sim/electrical/generator_4_toggle", "Generator #4 toggle.", "Electrical Generator4toggle", XPlaneCommands.ElectricalGenerator4Toggle); } }
        private XPlaneCommand ElectricalGenerator5Toggle { get { return new XPlaneCommand("sim/electrical/generator_5_toggle", "Generator #5 toggle.", "Electrical Generator5toggle", XPlaneCommands.ElectricalGenerator5Toggle); } }
        private XPlaneCommand ElectricalGenerator6Toggle { get { return new XPlaneCommand("sim/electrical/generator_6_toggle", "Generator #6 toggle.", "Electrical Generator6toggle", XPlaneCommands.ElectricalGenerator6Toggle); } }
        private XPlaneCommand ElectricalGenerator7Toggle { get { return new XPlaneCommand("sim/electrical/generator_7_toggle", "Generator #7 toggle.", "Electrical Generator7toggle", XPlaneCommands.ElectricalGenerator7Toggle); } }
        private XPlaneCommand ElectricalGenerator8Toggle { get { return new XPlaneCommand("sim/electrical/generator_8_toggle", "Generator #8 toggle.", "Electrical Generator8toggle", XPlaneCommands.ElectricalGenerator8Toggle); } }
        private XPlaneCommand ElectricalGenerator1Reset { get { return new XPlaneCommand("sim/electrical/generator_1_reset", "Generator #1 reset.", "Electrical Generator1reset", XPlaneCommands.ElectricalGenerator1Reset); } }
        private XPlaneCommand ElectricalGenerator2Reset { get { return new XPlaneCommand("sim/electrical/generator_2_reset", "Generator #2 reset.", "Electrical Generator2reset", XPlaneCommands.ElectricalGenerator2Reset); } }
        private XPlaneCommand ElectricalGenerator3Reset { get { return new XPlaneCommand("sim/electrical/generator_3_reset", "Generator #3 reset.", "Electrical Generator3reset", XPlaneCommands.ElectricalGenerator3Reset); } }
        private XPlaneCommand ElectricalGenerator4Reset { get { return new XPlaneCommand("sim/electrical/generator_4_reset", "Generator #4 reset.", "Electrical Generator4reset", XPlaneCommands.ElectricalGenerator4Reset); } }
        private XPlaneCommand ElectricalGenerator5Reset { get { return new XPlaneCommand("sim/electrical/generator_5_reset", "Generator #5 reset.", "Electrical Generator5reset", XPlaneCommands.ElectricalGenerator5Reset); } }
        private XPlaneCommand ElectricalGenerator6Reset { get { return new XPlaneCommand("sim/electrical/generator_6_reset", "Generator #6 reset.", "Electrical Generator6reset", XPlaneCommands.ElectricalGenerator6Reset); } }
        private XPlaneCommand ElectricalGenerator7Reset { get { return new XPlaneCommand("sim/electrical/generator_7_reset", "Generator #7 reset.", "Electrical Generator7reset", XPlaneCommands.ElectricalGenerator7Reset); } }
        private XPlaneCommand ElectricalGenerator8Reset { get { return new XPlaneCommand("sim/electrical/generator_8_reset", "Generator #8 reset.", "Electrical Generator8reset", XPlaneCommands.ElectricalGenerator8Reset); } }
        private XPlaneCommand ElectricalAPUStart { get { return new XPlaneCommand("sim/electrical/APU_start", "APU start.", "Electrical APU Start", XPlaneCommands.ElectricalAPUStart); } }
        private XPlaneCommand ElectricalAPUOn { get { return new XPlaneCommand("sim/electrical/APU_on", "APU on.", "Electrical APU On", XPlaneCommands.ElectricalAPUOn); } }
        private XPlaneCommand ElectricalAPUOff { get { return new XPlaneCommand("sim/electrical/APU_off", "APU off.", "Electrical APU Off", XPlaneCommands.ElectricalAPUOff); } }
        private XPlaneCommand ElectricalAPUFireShutoff { get { return new XPlaneCommand("sim/electrical/APU_fire_shutoff", "APU emergency shutoff with fire handle.", "Electrical APU Fire Shutoff", XPlaneCommands.ElectricalAPUFireShutoff); } }
        private XPlaneCommand ElectricalAPUGeneratorOn { get { return new XPlaneCommand("sim/electrical/APU_generator_on", "APU generator on.", "Electrical APU Generator On", XPlaneCommands.ElectricalAPUGeneratorOn); } }
        private XPlaneCommand ElectricalAPUGeneratorOff { get { return new XPlaneCommand("sim/electrical/APU_generator_off", "APU generator off.", "Electrical APU Generator Off", XPlaneCommands.ElectricalAPUGeneratorOff); } }
        private XPlaneCommand ElectricalGPUOn { get { return new XPlaneCommand("sim/electrical/GPU_on", "GPU on.", "Electrical GPU On", XPlaneCommands.ElectricalGPUOn); } }
        private XPlaneCommand ElectricalGPUOff { get { return new XPlaneCommand("sim/electrical/GPU_off", "GPU off.", "Electrical GPU Off", XPlaneCommands.ElectricalGPUOff); } }
        private XPlaneCommand ElectricalGPUToggle { get { return new XPlaneCommand("sim/electrical/GPU_toggle", "GPU toggle.", "Electrical GPU Toggle", XPlaneCommands.ElectricalGPUToggle); } }
        private XPlaneCommand ElectricalRecharge { get { return new XPlaneCommand("sim/electrical/recharge", "Re-charge batteries.", "Electrical Recharge", XPlaneCommands.ElectricalRecharge); } }
        private XPlaneCommand LightsNavLightsOn { get { return new XPlaneCommand("sim/lights/nav_lights_on", "Nav lights on.", "Lights Nav Lights On", XPlaneCommands.LightsNavLightsOn); } }
        private XPlaneCommand LightsNavLightsOff { get { return new XPlaneCommand("sim/lights/nav_lights_off", "Nav lights off.", "Lights Nav Lights Off", XPlaneCommands.LightsNavLightsOff); } }
        private XPlaneCommand LightsNavLightsToggle { get { return new XPlaneCommand("sim/lights/nav_lights_toggle", "Nav lights toggle.", "Lights Nav Lights Toggle", XPlaneCommands.LightsNavLightsToggle); } }
        private XPlaneCommand LightsBeaconLightsOn { get { return new XPlaneCommand("sim/lights/beacon_lights_on", "Beacon lights on.", "Lights Beacon Lights On", XPlaneCommands.LightsBeaconLightsOn); } }
        private XPlaneCommand LightsBeaconLightsOff { get { return new XPlaneCommand("sim/lights/beacon_lights_off", "Beacon lights off.", "Lights Beacon Lights Off", XPlaneCommands.LightsBeaconLightsOff); } }
        private XPlaneCommand LightsBeaconLightsToggle { get { return new XPlaneCommand("sim/lights/beacon_lights_toggle", "Beacon lights toggle.", "Lights Beacon Lights Toggle", XPlaneCommands.LightsBeaconLightsToggle); } }
        private XPlaneCommand LightsStrobeLightsOn { get { return new XPlaneCommand("sim/lights/strobe_lights_on", "Strobe lights on.", "Lights Strobe Lights On", XPlaneCommands.LightsStrobeLightsOn); } }
        private XPlaneCommand LightsStrobeLightsOff { get { return new XPlaneCommand("sim/lights/strobe_lights_off", "Strobe lights off.", "Lights Strobe Lights Off", XPlaneCommands.LightsStrobeLightsOff); } }
        private XPlaneCommand LightsStrobeLightsToggle { get { return new XPlaneCommand("sim/lights/strobe_lights_toggle", "Strobe lights toggle.", "Lights Strobe Lights Toggle", XPlaneCommands.LightsStrobeLightsToggle); } }
        private XPlaneCommand LightsTaxiLightsOn { get { return new XPlaneCommand("sim/lights/taxi_lights_on", "Taxi lights on.", "Lights Taxi Lights On", XPlaneCommands.LightsTaxiLightsOn); } }
        private XPlaneCommand LightsTaxiLightsOff { get { return new XPlaneCommand("sim/lights/taxi_lights_off", "Taxi lights off.", "Lights Taxi Lights Off", XPlaneCommands.LightsTaxiLightsOff); } }
        private XPlaneCommand LightsTaxiLightsToggle { get { return new XPlaneCommand("sim/lights/taxi_lights_toggle", "Taxi lights toggle.", "Lights Taxi Lights Toggle", XPlaneCommands.LightsTaxiLightsToggle); } }
        private XPlaneCommand LightsLandingLightsOn { get { return new XPlaneCommand("sim/lights/landing_lights_on", "Landing lights on.", "Lights Landing Lights On", XPlaneCommands.LightsLandingLightsOn); } }
        private XPlaneCommand LightsLandingLightsOff { get { return new XPlaneCommand("sim/lights/landing_lights_off", "Landing lights off.", "Lights Landing Lights Off", XPlaneCommands.LightsLandingLightsOff); } }
        private XPlaneCommand LightsLandingLightsToggle { get { return new XPlaneCommand("sim/lights/landing_lights_toggle", "Landing lights toggle.", "Lights Landing Lights Toggle", XPlaneCommands.LightsLandingLightsToggle); } }
        private XPlaneCommand LightsLanding01LightOn { get { return new XPlaneCommand("sim/lights/landing_01_light_on", "Landing light 01 on.", "Lights Landing01light On", XPlaneCommands.LightsLanding01LightOn); } }
        private XPlaneCommand LightsLanding02LightOn { get { return new XPlaneCommand("sim/lights/landing_02_light_on", "Landing light 02 on.", "Lights Landing02light On", XPlaneCommands.LightsLanding02LightOn); } }
        private XPlaneCommand LightsLanding03LightOn { get { return new XPlaneCommand("sim/lights/landing_03_light_on", "Landing light 03 on.", "Lights Landing03light On", XPlaneCommands.LightsLanding03LightOn); } }
        private XPlaneCommand LightsLanding04LightOn { get { return new XPlaneCommand("sim/lights/landing_04_light_on", "Landing light 04 on.", "Lights Landing04light On", XPlaneCommands.LightsLanding04LightOn); } }
        private XPlaneCommand LightsLanding05LightOn { get { return new XPlaneCommand("sim/lights/landing_05_light_on", "Landing light 05 on.", "Lights Landing05light On", XPlaneCommands.LightsLanding05LightOn); } }
        private XPlaneCommand LightsLanding06LightOn { get { return new XPlaneCommand("sim/lights/landing_06_light_on", "Landing light 06 on.", "Lights Landing06light On", XPlaneCommands.LightsLanding06LightOn); } }
        private XPlaneCommand LightsLanding07LightOn { get { return new XPlaneCommand("sim/lights/landing_07_light_on", "Landing light 07 on.", "Lights Landing07light On", XPlaneCommands.LightsLanding07LightOn); } }
        private XPlaneCommand LightsLanding08LightOn { get { return new XPlaneCommand("sim/lights/landing_08_light_on", "Landing light 08 on.", "Lights Landing08light On", XPlaneCommands.LightsLanding08LightOn); } }
        private XPlaneCommand LightsLanding09LightOn { get { return new XPlaneCommand("sim/lights/landing_09_light_on", "Landing light 09 on.", "Lights Landing09light On", XPlaneCommands.LightsLanding09LightOn); } }
        private XPlaneCommand LightsLanding10LightOn { get { return new XPlaneCommand("sim/lights/landing_10_light_on", "Landing light 10 on.", "Lights Landing10light On", XPlaneCommands.LightsLanding10LightOn); } }
        private XPlaneCommand LightsLanding11LightOn { get { return new XPlaneCommand("sim/lights/landing_11_light_on", "Landing light 11 on.", "Lights Landing11light On", XPlaneCommands.LightsLanding11LightOn); } }
        private XPlaneCommand LightsLanding12LightOn { get { return new XPlaneCommand("sim/lights/landing_12_light_on", "Landing light 12 on.", "Lights Landing12light On", XPlaneCommands.LightsLanding12LightOn); } }
        private XPlaneCommand LightsLanding13LightOn { get { return new XPlaneCommand("sim/lights/landing_13_light_on", "Landing light 13 on.", "Lights Landing13light On", XPlaneCommands.LightsLanding13LightOn); } }
        private XPlaneCommand LightsLanding14LightOn { get { return new XPlaneCommand("sim/lights/landing_14_light_on", "Landing light 14 on.", "Lights Landing14light On", XPlaneCommands.LightsLanding14LightOn); } }
        private XPlaneCommand LightsLanding15LightOn { get { return new XPlaneCommand("sim/lights/landing_15_light_on", "Landing light 15 on.", "Lights Landing15light On", XPlaneCommands.LightsLanding15LightOn); } }
        private XPlaneCommand LightsLanding16LightOn { get { return new XPlaneCommand("sim/lights/landing_16_light_on", "Landing light 16 on.", "Lights Landing16light On", XPlaneCommands.LightsLanding16LightOn); } }
        private XPlaneCommand LightsLanding01LightOff { get { return new XPlaneCommand("sim/lights/landing_01_light_off", "Landing light 01 off.", "Lights Landing01light Off", XPlaneCommands.LightsLanding01LightOff); } }
        private XPlaneCommand LightsLanding02LightOff { get { return new XPlaneCommand("sim/lights/landing_02_light_off", "Landing light 02 off.", "Lights Landing02light Off", XPlaneCommands.LightsLanding02LightOff); } }
        private XPlaneCommand LightsLanding03LightOff { get { return new XPlaneCommand("sim/lights/landing_03_light_off", "Landing light 03 off.", "Lights Landing03light Off", XPlaneCommands.LightsLanding03LightOff); } }
        private XPlaneCommand LightsLanding04LightOff { get { return new XPlaneCommand("sim/lights/landing_04_light_off", "Landing light 04 off.", "Lights Landing04light Off", XPlaneCommands.LightsLanding04LightOff); } }
        private XPlaneCommand LightsLanding05LightOff { get { return new XPlaneCommand("sim/lights/landing_05_light_off", "Landing light 05 off.", "Lights Landing05light Off", XPlaneCommands.LightsLanding05LightOff); } }
        private XPlaneCommand LightsLanding06LightOff { get { return new XPlaneCommand("sim/lights/landing_06_light_off", "Landing light 06 off.", "Lights Landing06light Off", XPlaneCommands.LightsLanding06LightOff); } }
        private XPlaneCommand LightsLanding07LightOff { get { return new XPlaneCommand("sim/lights/landing_07_light_off", "Landing light 07 off.", "Lights Landing07light Off", XPlaneCommands.LightsLanding07LightOff); } }
        private XPlaneCommand LightsLanding08LightOff { get { return new XPlaneCommand("sim/lights/landing_08_light_off", "Landing light 08 off.", "Lights Landing08light Off", XPlaneCommands.LightsLanding08LightOff); } }
        private XPlaneCommand LightsLanding09LightOff { get { return new XPlaneCommand("sim/lights/landing_09_light_off", "Landing light 09 off.", "Lights Landing09light Off", XPlaneCommands.LightsLanding09LightOff); } }
        private XPlaneCommand LightsLanding10LightOff { get { return new XPlaneCommand("sim/lights/landing_10_light_off", "Landing light 10 off.", "Lights Landing10light Off", XPlaneCommands.LightsLanding10LightOff); } }
        private XPlaneCommand LightsLanding11LightOff { get { return new XPlaneCommand("sim/lights/landing_11_light_off", "Landing light 11 off.", "Lights Landing11light Off", XPlaneCommands.LightsLanding11LightOff); } }
        private XPlaneCommand LightsLanding12LightOff { get { return new XPlaneCommand("sim/lights/landing_12_light_off", "Landing light 12 off.", "Lights Landing12light Off", XPlaneCommands.LightsLanding12LightOff); } }
        private XPlaneCommand LightsLanding13LightOff { get { return new XPlaneCommand("sim/lights/landing_13_light_off", "Landing light 13 off.", "Lights Landing13light Off", XPlaneCommands.LightsLanding13LightOff); } }
        private XPlaneCommand LightsLanding14LightOff { get { return new XPlaneCommand("sim/lights/landing_14_light_off", "Landing light 14 off.", "Lights Landing14light Off", XPlaneCommands.LightsLanding14LightOff); } }
        private XPlaneCommand LightsLanding15LightOff { get { return new XPlaneCommand("sim/lights/landing_15_light_off", "Landing light 15 off.", "Lights Landing15light Off", XPlaneCommands.LightsLanding15LightOff); } }
        private XPlaneCommand LightsLanding16LightOff { get { return new XPlaneCommand("sim/lights/landing_16_light_off", "Landing light 16 off.", "Lights Landing16light Off", XPlaneCommands.LightsLanding16LightOff); } }
        private XPlaneCommand LightsLanding01LightTog { get { return new XPlaneCommand("sim/lights/landing_01_light_tog", "Landing light 01 toggle.", "Lights Landing01light Tog", XPlaneCommands.LightsLanding01LightTog); } }
        private XPlaneCommand LightsLanding02LightTog { get { return new XPlaneCommand("sim/lights/landing_02_light_tog", "Landing light 02 toggle.", "Lights Landing02light Tog", XPlaneCommands.LightsLanding02LightTog); } }
        private XPlaneCommand LightsLanding03LightTog { get { return new XPlaneCommand("sim/lights/landing_03_light_tog", "Landing light 03 toggle.", "Lights Landing03light Tog", XPlaneCommands.LightsLanding03LightTog); } }
        private XPlaneCommand LightsLanding04LightTog { get { return new XPlaneCommand("sim/lights/landing_04_light_tog", "Landing light 04 toggle.", "Lights Landing04light Tog", XPlaneCommands.LightsLanding04LightTog); } }
        private XPlaneCommand LightsLanding05LightTog { get { return new XPlaneCommand("sim/lights/landing_05_light_tog", "Landing light 05 toggle.", "Lights Landing05light Tog", XPlaneCommands.LightsLanding05LightTog); } }
        private XPlaneCommand LightsLanding06LightTog { get { return new XPlaneCommand("sim/lights/landing_06_light_tog", "Landing light 06 toggle.", "Lights Landing06light Tog", XPlaneCommands.LightsLanding06LightTog); } }
        private XPlaneCommand LightsLanding07LightTog { get { return new XPlaneCommand("sim/lights/landing_07_light_tog", "Landing light 07 toggle.", "Lights Landing07light Tog", XPlaneCommands.LightsLanding07LightTog); } }
        private XPlaneCommand LightsLanding08LightTog { get { return new XPlaneCommand("sim/lights/landing_08_light_tog", "Landing light 08 toggle.", "Lights Landing08light Tog", XPlaneCommands.LightsLanding08LightTog); } }
        private XPlaneCommand LightsLanding09LightTog { get { return new XPlaneCommand("sim/lights/landing_09_light_tog", "Landing light 09 toggle.", "Lights Landing09light Tog", XPlaneCommands.LightsLanding09LightTog); } }
        private XPlaneCommand LightsLanding10LightTog { get { return new XPlaneCommand("sim/lights/landing_10_light_tog", "Landing light 10 toggle.", "Lights Landing10light Tog", XPlaneCommands.LightsLanding10LightTog); } }
        private XPlaneCommand LightsLanding11LightTog { get { return new XPlaneCommand("sim/lights/landing_11_light_tog", "Landing light 11 toggle.", "Lights Landing11light Tog", XPlaneCommands.LightsLanding11LightTog); } }
        private XPlaneCommand LightsLanding12LightTog { get { return new XPlaneCommand("sim/lights/landing_12_light_tog", "Landing light 12 toggle.", "Lights Landing12light Tog", XPlaneCommands.LightsLanding12LightTog); } }
        private XPlaneCommand LightsLanding13LightTog { get { return new XPlaneCommand("sim/lights/landing_13_light_tog", "Landing light 13 toggle.", "Lights Landing13light Tog", XPlaneCommands.LightsLanding13LightTog); } }
        private XPlaneCommand LightsLanding14LightTog { get { return new XPlaneCommand("sim/lights/landing_14_light_tog", "Landing light 14 toggle.", "Lights Landing14light Tog", XPlaneCommands.LightsLanding14LightTog); } }
        private XPlaneCommand LightsLanding15LightTog { get { return new XPlaneCommand("sim/lights/landing_15_light_tog", "Landing light 15 toggle.", "Lights Landing15light Tog", XPlaneCommands.LightsLanding15LightTog); } }
        private XPlaneCommand LightsLanding16LightTog { get { return new XPlaneCommand("sim/lights/landing_16_light_tog", "Landing light 16 toggle.", "Lights Landing16light Tog", XPlaneCommands.LightsLanding16LightTog); } }
        private XPlaneCommand LightsGeneric01LightTog { get { return new XPlaneCommand("sim/lights/generic_01_light_tog", "Generic light 01 toggle.", "Lights Generic01light Tog", XPlaneCommands.LightsGeneric01LightTog); } }
        private XPlaneCommand LightsGeneric02LightTog { get { return new XPlaneCommand("sim/lights/generic_02_light_tog", "Generic light 02 toggle.", "Lights Generic02light Tog", XPlaneCommands.LightsGeneric02LightTog); } }
        private XPlaneCommand LightsGeneric03LightTog { get { return new XPlaneCommand("sim/lights/generic_03_light_tog", "Generic light 03 toggle.", "Lights Generic03light Tog", XPlaneCommands.LightsGeneric03LightTog); } }
        private XPlaneCommand LightsGeneric04LightTog { get { return new XPlaneCommand("sim/lights/generic_04_light_tog", "Generic light 04 toggle.", "Lights Generic04light Tog", XPlaneCommands.LightsGeneric04LightTog); } }
        private XPlaneCommand LightsGeneric05LightTog { get { return new XPlaneCommand("sim/lights/generic_05_light_tog", "Generic light 05 toggle.", "Lights Generic05light Tog", XPlaneCommands.LightsGeneric05LightTog); } }
        private XPlaneCommand LightsGeneric06LightTog { get { return new XPlaneCommand("sim/lights/generic_06_light_tog", "Generic light 06 toggle.", "Lights Generic06light Tog", XPlaneCommands.LightsGeneric06LightTog); } }
        private XPlaneCommand LightsGeneric07LightTog { get { return new XPlaneCommand("sim/lights/generic_07_light_tog", "Generic light 07 toggle.", "Lights Generic07light Tog", XPlaneCommands.LightsGeneric07LightTog); } }
        private XPlaneCommand LightsGeneric08LightTog { get { return new XPlaneCommand("sim/lights/generic_08_light_tog", "Generic light 08 toggle.", "Lights Generic08light Tog", XPlaneCommands.LightsGeneric08LightTog); } }
        private XPlaneCommand LightsGeneric09LightTog { get { return new XPlaneCommand("sim/lights/generic_09_light_tog", "Generic light 09 toggle.", "Lights Generic09light Tog", XPlaneCommands.LightsGeneric09LightTog); } }
        private XPlaneCommand LightsGeneric10LightTog { get { return new XPlaneCommand("sim/lights/generic_10_light_tog", "Generic light 10 toggle.", "Lights Generic10light Tog", XPlaneCommands.LightsGeneric10LightTog); } }
        private XPlaneCommand LightsGeneric11LightTog { get { return new XPlaneCommand("sim/lights/generic_11_light_tog", "Generic light 11 toggle.", "Lights Generic11light Tog", XPlaneCommands.LightsGeneric11LightTog); } }
        private XPlaneCommand LightsGeneric12LightTog { get { return new XPlaneCommand("sim/lights/generic_12_light_tog", "Generic light 12 toggle.", "Lights Generic12light Tog", XPlaneCommands.LightsGeneric12LightTog); } }
        private XPlaneCommand LightsGeneric13LightTog { get { return new XPlaneCommand("sim/lights/generic_13_light_tog", "Generic light 13 toggle.", "Lights Generic13light Tog", XPlaneCommands.LightsGeneric13LightTog); } }
        private XPlaneCommand LightsGeneric14LightTog { get { return new XPlaneCommand("sim/lights/generic_14_light_tog", "Generic light 14 toggle.", "Lights Generic14light Tog", XPlaneCommands.LightsGeneric14LightTog); } }
        private XPlaneCommand LightsGeneric15LightTog { get { return new XPlaneCommand("sim/lights/generic_15_light_tog", "Generic light 15 toggle.", "Lights Generic15light Tog", XPlaneCommands.LightsGeneric15LightTog); } }
        private XPlaneCommand LightsGeneric16LightTog { get { return new XPlaneCommand("sim/lights/generic_16_light_tog", "Generic light 16 toggle.", "Lights Generic16light Tog", XPlaneCommands.LightsGeneric16LightTog); } }
        private XPlaneCommand LightsGeneric17LightTog { get { return new XPlaneCommand("sim/lights/generic_17_light_tog", "Generic light 17 toggle.", "Lights Generic17light Tog", XPlaneCommands.LightsGeneric17LightTog); } }
        private XPlaneCommand LightsGeneric18LightTog { get { return new XPlaneCommand("sim/lights/generic_18_light_tog", "Generic light 18 toggle.", "Lights Generic18light Tog", XPlaneCommands.LightsGeneric18LightTog); } }
        private XPlaneCommand LightsGeneric19LightTog { get { return new XPlaneCommand("sim/lights/generic_19_light_tog", "Generic light 19 toggle.", "Lights Generic19light Tog", XPlaneCommands.LightsGeneric19LightTog); } }
        private XPlaneCommand LightsGeneric20LightTog { get { return new XPlaneCommand("sim/lights/generic_20_light_tog", "Generic light 20 toggle.", "Lights Generic20light Tog", XPlaneCommands.LightsGeneric20LightTog); } }
        private XPlaneCommand LightsGeneric21LightTog { get { return new XPlaneCommand("sim/lights/generic_21_light_tog", "Generic light 21 toggle.", "Lights Generic21light Tog", XPlaneCommands.LightsGeneric21LightTog); } }
        private XPlaneCommand LightsGeneric22LightTog { get { return new XPlaneCommand("sim/lights/generic_22_light_tog", "Generic light 22 toggle.", "Lights Generic22light Tog", XPlaneCommands.LightsGeneric22LightTog); } }
        private XPlaneCommand LightsGeneric23LightTog { get { return new XPlaneCommand("sim/lights/generic_23_light_tog", "Generic light 23 toggle.", "Lights Generic23light Tog", XPlaneCommands.LightsGeneric23LightTog); } }
        private XPlaneCommand LightsGeneric24LightTog { get { return new XPlaneCommand("sim/lights/generic_24_light_tog", "Generic light 24 toggle.", "Lights Generic24light Tog", XPlaneCommands.LightsGeneric24LightTog); } }
        private XPlaneCommand LightsGeneric25LightTog { get { return new XPlaneCommand("sim/lights/generic_25_light_tog", "Generic light 25 toggle.", "Lights Generic25light Tog", XPlaneCommands.LightsGeneric25LightTog); } }
        private XPlaneCommand LightsGeneric26LightTog { get { return new XPlaneCommand("sim/lights/generic_26_light_tog", "Generic light 26 toggle.", "Lights Generic26light Tog", XPlaneCommands.LightsGeneric26LightTog); } }
        private XPlaneCommand LightsGeneric27LightTog { get { return new XPlaneCommand("sim/lights/generic_27_light_tog", "Generic light 27 toggle.", "Lights Generic27light Tog", XPlaneCommands.LightsGeneric27LightTog); } }
        private XPlaneCommand LightsGeneric28LightTog { get { return new XPlaneCommand("sim/lights/generic_28_light_tog", "Generic light 28 toggle.", "Lights Generic28light Tog", XPlaneCommands.LightsGeneric28LightTog); } }
        private XPlaneCommand LightsGeneric29LightTog { get { return new XPlaneCommand("sim/lights/generic_29_light_tog", "Generic light 29 toggle.", "Lights Generic29light Tog", XPlaneCommands.LightsGeneric29LightTog); } }
        private XPlaneCommand LightsGeneric30LightTog { get { return new XPlaneCommand("sim/lights/generic_30_light_tog", "Generic light 30 toggle.", "Lights Generic30light Tog", XPlaneCommands.LightsGeneric30LightTog); } }
        private XPlaneCommand LightsGeneric31LightTog { get { return new XPlaneCommand("sim/lights/generic_31_light_tog", "Generic light 31 toggle.", "Lights Generic31light Tog", XPlaneCommands.LightsGeneric31LightTog); } }
        private XPlaneCommand LightsGeneric32LightTog { get { return new XPlaneCommand("sim/lights/generic_32_light_tog", "Generic light 32 toggle.", "Lights Generic32light Tog", XPlaneCommands.LightsGeneric32LightTog); } }
        private XPlaneCommand LightsGeneric33LightTog { get { return new XPlaneCommand("sim/lights/generic_33_light_tog", "Generic light 33 toggle.", "Lights Generic33light Tog", XPlaneCommands.LightsGeneric33LightTog); } }
        private XPlaneCommand LightsGeneric34LightTog { get { return new XPlaneCommand("sim/lights/generic_34_light_tog", "Generic light 34 toggle.", "Lights Generic34light Tog", XPlaneCommands.LightsGeneric34LightTog); } }
        private XPlaneCommand LightsGeneric35LightTog { get { return new XPlaneCommand("sim/lights/generic_35_light_tog", "Generic light 35 toggle.", "Lights Generic35light Tog", XPlaneCommands.LightsGeneric35LightTog); } }
        private XPlaneCommand LightsGeneric36LightTog { get { return new XPlaneCommand("sim/lights/generic_36_light_tog", "Generic light 36 toggle.", "Lights Generic36light Tog", XPlaneCommands.LightsGeneric36LightTog); } }
        private XPlaneCommand LightsGeneric37LightTog { get { return new XPlaneCommand("sim/lights/generic_37_light_tog", "Generic light 37 toggle.", "Lights Generic37light Tog", XPlaneCommands.LightsGeneric37LightTog); } }
        private XPlaneCommand LightsGeneric38LightTog { get { return new XPlaneCommand("sim/lights/generic_38_light_tog", "Generic light 38 toggle.", "Lights Generic38light Tog", XPlaneCommands.LightsGeneric38LightTog); } }
        private XPlaneCommand LightsGeneric39LightTog { get { return new XPlaneCommand("sim/lights/generic_39_light_tog", "Generic light 39 toggle.", "Lights Generic39light Tog", XPlaneCommands.LightsGeneric39LightTog); } }
        private XPlaneCommand LightsGeneric40LightTog { get { return new XPlaneCommand("sim/lights/generic_40_light_tog", "Generic light 40 toggle.", "Lights Generic40light Tog", XPlaneCommands.LightsGeneric40LightTog); } }
        private XPlaneCommand LightsGeneric41LightTog { get { return new XPlaneCommand("sim/lights/generic_41_light_tog", "Generic light 41 toggle.", "Lights Generic41light Tog", XPlaneCommands.LightsGeneric41LightTog); } }
        private XPlaneCommand LightsGeneric42LightTog { get { return new XPlaneCommand("sim/lights/generic_42_light_tog", "Generic light 42 toggle.", "Lights Generic42light Tog", XPlaneCommands.LightsGeneric42LightTog); } }
        private XPlaneCommand LightsGeneric43LightTog { get { return new XPlaneCommand("sim/lights/generic_43_light_tog", "Generic light 43 toggle.", "Lights Generic43light Tog", XPlaneCommands.LightsGeneric43LightTog); } }
        private XPlaneCommand LightsGeneric44LightTog { get { return new XPlaneCommand("sim/lights/generic_44_light_tog", "Generic light 44 toggle.", "Lights Generic44light Tog", XPlaneCommands.LightsGeneric44LightTog); } }
        private XPlaneCommand LightsGeneric45LightTog { get { return new XPlaneCommand("sim/lights/generic_45_light_tog", "Generic light 45 toggle.", "Lights Generic45light Tog", XPlaneCommands.LightsGeneric45LightTog); } }
        private XPlaneCommand LightsGeneric46LightTog { get { return new XPlaneCommand("sim/lights/generic_46_light_tog", "Generic light 46 toggle.", "Lights Generic46light Tog", XPlaneCommands.LightsGeneric46LightTog); } }
        private XPlaneCommand LightsGeneric47LightTog { get { return new XPlaneCommand("sim/lights/generic_47_light_tog", "Generic light 47 toggle.", "Lights Generic47light Tog", XPlaneCommands.LightsGeneric47LightTog); } }
        private XPlaneCommand LightsGeneric48LightTog { get { return new XPlaneCommand("sim/lights/generic_48_light_tog", "Generic light 48 toggle.", "Lights Generic48light Tog", XPlaneCommands.LightsGeneric48LightTog); } }
        private XPlaneCommand LightsGeneric49LightTog { get { return new XPlaneCommand("sim/lights/generic_49_light_tog", "Generic light 49 toggle.", "Lights Generic49light Tog", XPlaneCommands.LightsGeneric49LightTog); } }
        private XPlaneCommand LightsGeneric50LightTog { get { return new XPlaneCommand("sim/lights/generic_50_light_tog", "Generic light 50 toggle.", "Lights Generic50light Tog", XPlaneCommands.LightsGeneric50LightTog); } }
        private XPlaneCommand LightsGeneric51LightTog { get { return new XPlaneCommand("sim/lights/generic_51_light_tog", "Generic light 51 toggle.", "Lights Generic51light Tog", XPlaneCommands.LightsGeneric51LightTog); } }
        private XPlaneCommand LightsGeneric52LightTog { get { return new XPlaneCommand("sim/lights/generic_52_light_tog", "Generic light 52 toggle.", "Lights Generic52light Tog", XPlaneCommands.LightsGeneric52LightTog); } }
        private XPlaneCommand LightsGeneric53LightTog { get { return new XPlaneCommand("sim/lights/generic_53_light_tog", "Generic light 53 toggle.", "Lights Generic53light Tog", XPlaneCommands.LightsGeneric53LightTog); } }
        private XPlaneCommand LightsGeneric54LightTog { get { return new XPlaneCommand("sim/lights/generic_54_light_tog", "Generic light 54 toggle.", "Lights Generic54light Tog", XPlaneCommands.LightsGeneric54LightTog); } }
        private XPlaneCommand LightsGeneric55LightTog { get { return new XPlaneCommand("sim/lights/generic_55_light_tog", "Generic light 55 toggle.", "Lights Generic55light Tog", XPlaneCommands.LightsGeneric55LightTog); } }
        private XPlaneCommand LightsGeneric56LightTog { get { return new XPlaneCommand("sim/lights/generic_56_light_tog", "Generic light 56 toggle.", "Lights Generic56light Tog", XPlaneCommands.LightsGeneric56LightTog); } }
        private XPlaneCommand LightsGeneric57LightTog { get { return new XPlaneCommand("sim/lights/generic_57_light_tog", "Generic light 57 toggle.", "Lights Generic57light Tog", XPlaneCommands.LightsGeneric57LightTog); } }
        private XPlaneCommand LightsGeneric58LightTog { get { return new XPlaneCommand("sim/lights/generic_58_light_tog", "Generic light 58 toggle.", "Lights Generic58light Tog", XPlaneCommands.LightsGeneric58LightTog); } }
        private XPlaneCommand LightsGeneric59LightTog { get { return new XPlaneCommand("sim/lights/generic_59_light_tog", "Generic light 59 toggle.", "Lights Generic59light Tog", XPlaneCommands.LightsGeneric59LightTog); } }
        private XPlaneCommand LightsGeneric60LightTog { get { return new XPlaneCommand("sim/lights/generic_60_light_tog", "Generic light 60 toggle.", "Lights Generic60light Tog", XPlaneCommands.LightsGeneric60LightTog); } }
        private XPlaneCommand LightsGeneric61LightTog { get { return new XPlaneCommand("sim/lights/generic_61_light_tog", "Generic light 61 toggle.", "Lights Generic61light Tog", XPlaneCommands.LightsGeneric61LightTog); } }
        private XPlaneCommand LightsGeneric62LightTog { get { return new XPlaneCommand("sim/lights/generic_62_light_tog", "Generic light 62 toggle.", "Lights Generic62light Tog", XPlaneCommands.LightsGeneric62LightTog); } }
        private XPlaneCommand LightsGeneric63LightTog { get { return new XPlaneCommand("sim/lights/generic_63_light_tog", "Generic light 63 toggle.", "Lights Generic63light Tog", XPlaneCommands.LightsGeneric63LightTog); } }
        private XPlaneCommand LightsGeneric64LightTog { get { return new XPlaneCommand("sim/lights/generic_64_light_tog", "Generic light 64 toggle.", "Lights Generic64light Tog", XPlaneCommands.LightsGeneric64LightTog); } }
        private XPlaneCommand LightsSpotLightsOn { get { return new XPlaneCommand("sim/lights/spot_lights_on", "Spot lights on.", "Lights Spot Lights On", XPlaneCommands.LightsSpotLightsOn); } }
        private XPlaneCommand LightsSpotLightsOff { get { return new XPlaneCommand("sim/lights/spot_lights_off", "Spot lights off.", "Lights Spot Lights Off", XPlaneCommands.LightsSpotLightsOff); } }
        private XPlaneCommand LightsSpotLightsToggle { get { return new XPlaneCommand("sim/lights/spot_lights_toggle", "Spot lights toggle.", "Lights Spot Lights Toggle", XPlaneCommands.LightsSpotLightsToggle); } }
        private XPlaneCommand SystemsAvionicsOn { get { return new XPlaneCommand("sim/systems/avionics_on", "Avionics on.", "Systems Avionics On", XPlaneCommands.SystemsAvionicsOn); } }
        private XPlaneCommand SystemsAvionicsOff { get { return new XPlaneCommand("sim/systems/avionics_off", "Avionics off.", "Systems Avionics Off", XPlaneCommands.SystemsAvionicsOff); } }
        private XPlaneCommand SystemsAvionicsToggle { get { return new XPlaneCommand("sim/systems/avionics_toggle", "Avionics toggle.", "Systems Avionics Toggle", XPlaneCommands.SystemsAvionicsToggle); } }
        private XPlaneCommand BleedAirBleedAirDown { get { return new XPlaneCommand("sim/bleed_air/bleed_air_down", "Bleed air mode down.", "Bleed Air Bleed Air Down", XPlaneCommands.BleedAirBleedAirDown); } }
        private XPlaneCommand BleedAirBleedAirUp { get { return new XPlaneCommand("sim/bleed_air/bleed_air_up", "Bleed air mode up.", "Bleed Air Bleed Air Up", XPlaneCommands.BleedAirBleedAirUp); } }
        private XPlaneCommand BleedAirBleedAirOff { get { return new XPlaneCommand("sim/bleed_air/bleed_air_off", "Bleed air off.", "Bleed Air Bleed Air Off", XPlaneCommands.BleedAirBleedAirOff); } }
        private XPlaneCommand BleedAirBleedAirLeft { get { return new XPlaneCommand("sim/bleed_air/bleed_air_left", "Bleed air from left engine.", "Bleed Air Bleed Air Left", XPlaneCommands.BleedAirBleedAirLeft); } }
        private XPlaneCommand BleedAirBleedAirBoth { get { return new XPlaneCommand("sim/bleed_air/bleed_air_both", "Bleed air from either engine.", "Bleed Air Bleed Air Both", XPlaneCommands.BleedAirBleedAirBoth); } }
        private XPlaneCommand BleedAirBleedAirRight { get { return new XPlaneCommand("sim/bleed_air/bleed_air_right", "Bleed air from right engine.", "Bleed Air Bleed Air Right", XPlaneCommands.BleedAirBleedAirRight); } }
        private XPlaneCommand BleedAirBleedAirApu { get { return new XPlaneCommand("sim/bleed_air/bleed_air_apu", "Bleed air from APU.", "Bleed Air Bleed Air Apu", XPlaneCommands.BleedAirBleedAirApu); } }
        private XPlaneCommand BleedAirBleedAirAuto { get { return new XPlaneCommand("sim/bleed_air/bleed_air_auto", "Bleed air from any engine or APU.", "Bleed Air Bleed Air Auto", XPlaneCommands.BleedAirBleedAirAuto); } }
        private XPlaneCommand BleedAirBleedAirLeftOn { get { return new XPlaneCommand("sim/bleed_air/bleed_air_left_on", "Bleed air left engine on.", "Bleed Air Bleed Air Left On", XPlaneCommands.BleedAirBleedAirLeftOn); } }
        private XPlaneCommand BleedAirBleedAirLeftInsOnly { get { return new XPlaneCommand("sim/bleed_air/bleed_air_left_ins_only", "Bleed air left engine instruments only.", "Bleed Air Bleed Air Left Ins Only", XPlaneCommands.BleedAirBleedAirLeftInsOnly); } }
        private XPlaneCommand BleedAirBleedAirLeftOff { get { return new XPlaneCommand("sim/bleed_air/bleed_air_left_off", "Bleed air left engine off.", "Bleed Air Bleed Air Left Off", XPlaneCommands.BleedAirBleedAirLeftOff); } }
        private XPlaneCommand BleedAirBleedAirRightOn { get { return new XPlaneCommand("sim/bleed_air/bleed_air_right_on", "Bleed air right engine on.", "Bleed Air Bleed Air Right On", XPlaneCommands.BleedAirBleedAirRightOn); } }
        private XPlaneCommand BleedAirBleedAirRightInsOnly { get { return new XPlaneCommand("sim/bleed_air/bleed_air_right_ins_only", "Bleed air right engine instruments only.", "Bleed Air Bleed Air Right Ins Only", XPlaneCommands.BleedAirBleedAirRightInsOnly); } }
        private XPlaneCommand BleedAirBleedAirRightOff { get { return new XPlaneCommand("sim/bleed_air/bleed_air_right_off", "Bleed air right engine off.", "Bleed Air Bleed Air Right Off", XPlaneCommands.BleedAirBleedAirRightOff); } }
        private XPlaneCommand BleedAirEngine1Off { get { return new XPlaneCommand("sim/bleed_air/engine_1_off", "Bleed air shut off engine 1.", "Bleed Air Engine1off", XPlaneCommands.BleedAirEngine1Off); } }
        private XPlaneCommand BleedAirEngine2Off { get { return new XPlaneCommand("sim/bleed_air/engine_2_off", "Bleed air shut off engine 2.", "Bleed Air Engine2off", XPlaneCommands.BleedAirEngine2Off); } }
        private XPlaneCommand BleedAirEngine3Off { get { return new XPlaneCommand("sim/bleed_air/engine_3_off", "Bleed air shut off engine 3.", "Bleed Air Engine3off", XPlaneCommands.BleedAirEngine3Off); } }
        private XPlaneCommand BleedAirEngine4Off { get { return new XPlaneCommand("sim/bleed_air/engine_4_off", "Bleed air shut off engine 4.", "Bleed Air Engine4off", XPlaneCommands.BleedAirEngine4Off); } }
        private XPlaneCommand BleedAirEngine5Off { get { return new XPlaneCommand("sim/bleed_air/engine_5_off", "Bleed air shut off engine 5.", "Bleed Air Engine5off", XPlaneCommands.BleedAirEngine5Off); } }
        private XPlaneCommand BleedAirEngine6Off { get { return new XPlaneCommand("sim/bleed_air/engine_6_off", "Bleed air shut off engine 6.", "Bleed Air Engine6off", XPlaneCommands.BleedAirEngine6Off); } }
        private XPlaneCommand BleedAirEngine7Off { get { return new XPlaneCommand("sim/bleed_air/engine_7_off", "Bleed air shut off engine 7.", "Bleed Air Engine7off", XPlaneCommands.BleedAirEngine7Off); } }
        private XPlaneCommand BleedAirEngine8Off { get { return new XPlaneCommand("sim/bleed_air/engine_8_off", "Bleed air shut off engine 8.", "Bleed Air Engine8off", XPlaneCommands.BleedAirEngine8Off); } }
        private XPlaneCommand BleedAirEngine1On { get { return new XPlaneCommand("sim/bleed_air/engine_1_on", "Bleed air on engine 1.", "Bleed Air Engine1on", XPlaneCommands.BleedAirEngine1On); } }
        private XPlaneCommand BleedAirEngine2On { get { return new XPlaneCommand("sim/bleed_air/engine_2_on", "Bleed air on engine 2.", "Bleed Air Engine2on", XPlaneCommands.BleedAirEngine2On); } }
        private XPlaneCommand BleedAirEngine3On { get { return new XPlaneCommand("sim/bleed_air/engine_3_on", "Bleed air on engine 3.", "Bleed Air Engine3on", XPlaneCommands.BleedAirEngine3On); } }
        private XPlaneCommand BleedAirEngine4On { get { return new XPlaneCommand("sim/bleed_air/engine_4_on", "Bleed air on engine 4.", "Bleed Air Engine4on", XPlaneCommands.BleedAirEngine4On); } }
        private XPlaneCommand BleedAirEngine5On { get { return new XPlaneCommand("sim/bleed_air/engine_5_on", "Bleed air on engine 5.", "Bleed Air Engine5on", XPlaneCommands.BleedAirEngine5On); } }
        private XPlaneCommand BleedAirEngine6On { get { return new XPlaneCommand("sim/bleed_air/engine_6_on", "Bleed air on engine 6.", "Bleed Air Engine6on", XPlaneCommands.BleedAirEngine6On); } }
        private XPlaneCommand BleedAirEngine7On { get { return new XPlaneCommand("sim/bleed_air/engine_7_on", "Bleed air on engine 7.", "Bleed Air Engine7on", XPlaneCommands.BleedAirEngine7On); } }
        private XPlaneCommand BleedAirEngine8On { get { return new XPlaneCommand("sim/bleed_air/engine_8_on", "Bleed air on engine 8.", "Bleed Air Engine8on", XPlaneCommands.BleedAirEngine8On); } }
        private XPlaneCommand BleedAirEngine1Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_1_toggle", "Bleed air toggle engine 1.", "Bleed Air Engine1toggle", XPlaneCommands.BleedAirEngine1Toggle); } }
        private XPlaneCommand BleedAirEngine2Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_2_toggle", "Bleed air toggle engine 2.", "Bleed Air Engine2toggle", XPlaneCommands.BleedAirEngine2Toggle); } }
        private XPlaneCommand BleedAirEngine3Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_3_toggle", "Bleed air toggle engine 3.", "Bleed Air Engine3toggle", XPlaneCommands.BleedAirEngine3Toggle); } }
        private XPlaneCommand BleedAirEngine4Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_4_toggle", "Bleed air toggle engine 4.", "Bleed Air Engine4toggle", XPlaneCommands.BleedAirEngine4Toggle); } }
        private XPlaneCommand BleedAirEngine5Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_5_toggle", "Bleed air toggle engine 5.", "Bleed Air Engine5toggle", XPlaneCommands.BleedAirEngine5Toggle); } }
        private XPlaneCommand BleedAirEngine6Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_6_toggle", "Bleed air toggle engine 6.", "Bleed Air Engine6toggle", XPlaneCommands.BleedAirEngine6Toggle); } }
        private XPlaneCommand BleedAirEngine7Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_7_toggle", "Bleed air toggle engine 7.", "Bleed Air Engine7toggle", XPlaneCommands.BleedAirEngine7Toggle); } }
        private XPlaneCommand BleedAirEngine8Toggle { get { return new XPlaneCommand("sim/bleed_air/engine_8_toggle", "Bleed air toggle engine 8.", "Bleed Air Engine8toggle", XPlaneCommands.BleedAirEngine8Toggle); } }
        private XPlaneCommand BleedAirGpuOff { get { return new XPlaneCommand("sim/bleed_air/gpu_off", "Bleed air shut off GPU.", "Bleed Air Gpu Off", XPlaneCommands.BleedAirGpuOff); } }
        private XPlaneCommand BleedAirGpuOn { get { return new XPlaneCommand("sim/bleed_air/gpu_on", "Bleed air on GPU.", "Bleed Air Gpu On", XPlaneCommands.BleedAirGpuOn); } }
        private XPlaneCommand BleedAirGpuToggle { get { return new XPlaneCommand("sim/bleed_air/gpu_toggle", "Bleed air toggle GPU.", "Bleed Air Gpu Toggle", XPlaneCommands.BleedAirGpuToggle); } }
        private XPlaneCommand BleedAirApuOff { get { return new XPlaneCommand("sim/bleed_air/apu_off", "Bleed air shut off APU.", "Bleed Air Apu Off", XPlaneCommands.BleedAirApuOff); } }
        private XPlaneCommand BleedAirApuOn { get { return new XPlaneCommand("sim/bleed_air/apu_on", "Bleed air on APU.", "Bleed Air Apu On", XPlaneCommands.BleedAirApuOn); } }
        private XPlaneCommand BleedAirApuToggle { get { return new XPlaneCommand("sim/bleed_air/apu_toggle", "Bleed air toggle APU.", "Bleed Air Apu Toggle", XPlaneCommands.BleedAirApuToggle); } }
        private XPlaneCommand BleedAirIsolationLeftShut { get { return new XPlaneCommand("sim/bleed_air/isolation_left_shut", "Bleed air shut left isolation.", "Bleed Air Isolation Left Shut", XPlaneCommands.BleedAirIsolationLeftShut); } }
        private XPlaneCommand BleedAirIsolationLeftOpen { get { return new XPlaneCommand("sim/bleed_air/isolation_left_open", "Bleed air open left isolation.", "Bleed Air Isolation Left Open", XPlaneCommands.BleedAirIsolationLeftOpen); } }
        private XPlaneCommand BleedAirIsolationLeftToggle { get { return new XPlaneCommand("sim/bleed_air/isolation_left_toggle", "Bleed air toggle left isolation.", "Bleed Air Isolation Left Toggle", XPlaneCommands.BleedAirIsolationLeftToggle); } }
        private XPlaneCommand BleedAirIsolationRightShut { get { return new XPlaneCommand("sim/bleed_air/isolation_right_shut", "Bleed air shut right isolation.", "Bleed Air Isolation Right Shut", XPlaneCommands.BleedAirIsolationRightShut); } }
        private XPlaneCommand BleedAirIsolationRightOpen { get { return new XPlaneCommand("sim/bleed_air/isolation_right_open", "Bleed air open right isolation.", "Bleed Air Isolation Right Open", XPlaneCommands.BleedAirIsolationRightOpen); } }
        private XPlaneCommand BleedAirIsolationRightToggle { get { return new XPlaneCommand("sim/bleed_air/isolation_right_toggle", "Bleed air toggle right isolation.", "Bleed Air Isolation Right Toggle", XPlaneCommands.BleedAirIsolationRightToggle); } }
        private XPlaneCommand BleedAirPackLeftOff { get { return new XPlaneCommand("sim/bleed_air/pack_left_off", "Bleed air left pack off.", "Bleed Air Pack Left Off", XPlaneCommands.BleedAirPackLeftOff); } }
        private XPlaneCommand BleedAirPackLeftOn { get { return new XPlaneCommand("sim/bleed_air/pack_left_on", "Bleed air left pack on.", "Bleed Air Pack Left On", XPlaneCommands.BleedAirPackLeftOn); } }
        private XPlaneCommand BleedAirPackLeftToggle { get { return new XPlaneCommand("sim/bleed_air/pack_left_toggle", "Bleed air left pack toggle.", "Bleed Air Pack Left Toggle", XPlaneCommands.BleedAirPackLeftToggle); } }
        private XPlaneCommand BleedAirPackCenterOff { get { return new XPlaneCommand("sim/bleed_air/pack_center_off", "Bleed air center pack off.", "Bleed Air Pack Center Off", XPlaneCommands.BleedAirPackCenterOff); } }
        private XPlaneCommand BleedAirPackCenterOn { get { return new XPlaneCommand("sim/bleed_air/pack_center_on", "Bleed air center pack on.", "Bleed Air Pack Center On", XPlaneCommands.BleedAirPackCenterOn); } }
        private XPlaneCommand BleedAirPackCenterToggle { get { return new XPlaneCommand("sim/bleed_air/pack_center_toggle", "Bleed air center pack toggle.", "Bleed Air Pack Center Toggle", XPlaneCommands.BleedAirPackCenterToggle); } }
        private XPlaneCommand BleedAirPackRightOff { get { return new XPlaneCommand("sim/bleed_air/pack_right_off", "Bleed air right pack off.", "Bleed Air Pack Right Off", XPlaneCommands.BleedAirPackRightOff); } }
        private XPlaneCommand BleedAirPackRightOn { get { return new XPlaneCommand("sim/bleed_air/pack_right_on", "Bleed air right pack on.", "Bleed Air Pack Right On", XPlaneCommands.BleedAirPackRightOn); } }
        private XPlaneCommand BleedAirPackRightToggle { get { return new XPlaneCommand("sim/bleed_air/pack_right_toggle", "Bleed air right pack toggle.", "Bleed Air Pack Right Toggle", XPlaneCommands.BleedAirPackRightToggle); } }
        private XPlaneCommand PressurizationTest { get { return new XPlaneCommand("sim/pressurization/test", "Pressurization test.", "Pressurization Test", XPlaneCommands.PressurizationTest); } }
        private XPlaneCommand PressurizationDumpOn { get { return new XPlaneCommand("sim/pressurization/dump_on", "Dump pressurization on.", "Pressurization Dump On", XPlaneCommands.PressurizationDumpOn); } }
        private XPlaneCommand PressurizationDumpOff { get { return new XPlaneCommand("sim/pressurization/dump_off", "Dump pressurization off.", "Pressurization Dump Off", XPlaneCommands.PressurizationDumpOff); } }
        private XPlaneCommand PressurizationVviDown { get { return new XPlaneCommand("sim/pressurization/vvi_down", "Cabin vertical speed down.", "Pressurization VVI Down", XPlaneCommands.PressurizationVviDown); } }
        private XPlaneCommand PressurizationVviUp { get { return new XPlaneCommand("sim/pressurization/vvi_up", "Cabin vertical speed up.", "Pressurization VVI Up", XPlaneCommands.PressurizationVviUp); } }
        private XPlaneCommand PressurizationCabinAltDown { get { return new XPlaneCommand("sim/pressurization/cabin_alt_down", "Cabin altitude down.", "Pressurization Cabin Alt Down", XPlaneCommands.PressurizationCabinAltDown); } }
        private XPlaneCommand PressurizationCabinAltUp { get { return new XPlaneCommand("sim/pressurization/cabin_alt_up", "Cabin altitude up.", "Pressurization Cabin Alt Up", XPlaneCommands.PressurizationCabinAltUp); } }
        private XPlaneCommand PressurizationAircondOn { get { return new XPlaneCommand("sim/pressurization/aircond_on", "Air conditioning on.", "Pressurization Aircond On", XPlaneCommands.PressurizationAircondOn); } }
        private XPlaneCommand PressurizationAircondOff { get { return new XPlaneCommand("sim/pressurization/aircond_off", "Air conditioning off.", "Pressurization Aircond Off", XPlaneCommands.PressurizationAircondOff); } }
        private XPlaneCommand PressurizationHeaterOn { get { return new XPlaneCommand("sim/pressurization/heater_on", "Electric heater on.", "Pressurization Heater On", XPlaneCommands.PressurizationHeaterOn); } }
        private XPlaneCommand PressurizationHeaterGrdMax { get { return new XPlaneCommand("sim/pressurization/heater_grd_max", "Electric heater on ground max.", "Pressurization Heater Grd Max", XPlaneCommands.PressurizationHeaterGrdMax); } }
        private XPlaneCommand PressurizationHeaterOff { get { return new XPlaneCommand("sim/pressurization/heater_off", "Electric heater off.", "Pressurization Heater Off", XPlaneCommands.PressurizationHeaterOff); } }
        private XPlaneCommand PressurizationHeaterUp { get { return new XPlaneCommand("sim/pressurization/heater_up", "Electric heater off->on->grd max.", "Pressurization Heater Up", XPlaneCommands.PressurizationHeaterUp); } }
        private XPlaneCommand PressurizationHeaterDn { get { return new XPlaneCommand("sim/pressurization/heater_dn", "Electric heater grd max->on->off.", "Pressurization Heater Dn", XPlaneCommands.PressurizationHeaterDn); } }
        private XPlaneCommand PressurizationFanAuto { get { return new XPlaneCommand("sim/pressurization/fan_auto", "Vent fan auto (on only with AC or heat).", "Pressurization Fan Auto", XPlaneCommands.PressurizationFanAuto); } }
        private XPlaneCommand PressurizationFanLow { get { return new XPlaneCommand("sim/pressurization/fan_low", "Vent fan on low.", "Pressurization Fan Low", XPlaneCommands.PressurizationFanLow); } }
        private XPlaneCommand PressurizationFanHigh { get { return new XPlaneCommand("sim/pressurization/fan_high", "Vent fan high.", "Pressurization Fan High", XPlaneCommands.PressurizationFanHigh); } }
        private XPlaneCommand PressurizationFanUp { get { return new XPlaneCommand("sim/pressurization/fan_up", "Vent fan setting up.", "Pressurization Fan Up", XPlaneCommands.PressurizationFanUp); } }
        private XPlaneCommand PressurizationFanDown { get { return new XPlaneCommand("sim/pressurization/fan_down", "Vent fan setting down.", "Pressurization Fan Down", XPlaneCommands.PressurizationFanDown); } }
        private XPlaneCommand IceAntiIceToggle { get { return new XPlaneCommand("sim/ice/anti_ice_toggle", "Anti-ice: toggle all.", "Ice Anti Ice Toggle", XPlaneCommands.IceAntiIceToggle); } }
        private XPlaneCommand IceAlternateStaticPort { get { return new XPlaneCommand("sim/ice/alternate_static_port", "Toggle alternate port.", "Ice Alternate Static Port", XPlaneCommands.IceAlternateStaticPort); } }
        private XPlaneCommand IcePitotHeat0On { get { return new XPlaneCommand("sim/ice/pitot_heat0_on", "Anti-ice: left pitot heat on.", "Ice Pitot Heat0on", XPlaneCommands.IcePitotHeat0On); } }
        private XPlaneCommand IcePitotHeat1On { get { return new XPlaneCommand("sim/ice/pitot_heat1_on", "Anti-ice: right pitot heat on.", "Ice Pitot Heat1on", XPlaneCommands.IcePitotHeat1On); } }
        private XPlaneCommand IcePitotHeat0Off { get { return new XPlaneCommand("sim/ice/pitot_heat0_off", "Anti-ice: left pitot heat off.", "Ice Pitot Heat0off", XPlaneCommands.IcePitotHeat0Off); } }
        private XPlaneCommand IcePitotHeat1Off { get { return new XPlaneCommand("sim/ice/pitot_heat1_off", "Anti-ice: right pitot heat off.", "Ice Pitot Heat1off", XPlaneCommands.IcePitotHeat1Off); } }
        private XPlaneCommand IcePitotHeat0Tog { get { return new XPlaneCommand("sim/ice/pitot_heat0_tog", "Anti-ice: left pitot heat toggle.", "Ice Pitot Heat0tog", XPlaneCommands.IcePitotHeat0Tog); } }
        private XPlaneCommand IcePitotHeat1Tog { get { return new XPlaneCommand("sim/ice/pitot_heat1_tog", "Anti-ice: right pitot heat toggle.", "Ice Pitot Heat1tog", XPlaneCommands.IcePitotHeat1Tog); } }
        private XPlaneCommand IceStaticHeat0On { get { return new XPlaneCommand("sim/ice/static_heat0_on", "Anti-ice: left heat on.", "Ice Static Heat0on", XPlaneCommands.IceStaticHeat0On); } }
        private XPlaneCommand IceStaticHeat1On { get { return new XPlaneCommand("sim/ice/static_heat1_on", "Anti-ice: right heat on.", "Ice Static Heat1on", XPlaneCommands.IceStaticHeat1On); } }
        private XPlaneCommand IceStaticHeat0Off { get { return new XPlaneCommand("sim/ice/static_heat0_off", "Anti-ice: left heat off.", "Ice Static Heat0off", XPlaneCommands.IceStaticHeat0Off); } }
        private XPlaneCommand IceStaticHeat1Off { get { return new XPlaneCommand("sim/ice/static_heat1_off", "Anti-ice: right heat off.", "Ice Static Heat1off", XPlaneCommands.IceStaticHeat1Off); } }
        private XPlaneCommand IceStaticHeat0Tog { get { return new XPlaneCommand("sim/ice/static_heat0_tog", "Anti-ice: left heat toggle.", "Ice Static Heat0tog", XPlaneCommands.IceStaticHeat0Tog); } }
        private XPlaneCommand IceStaticHeat1Tog { get { return new XPlaneCommand("sim/ice/static_heat1_tog", "Anti-ice: right heat toggle.", "Ice Static Heat1tog", XPlaneCommands.IceStaticHeat1Tog); } }
        private XPlaneCommand IceAOAHeat0On { get { return new XPlaneCommand("sim/ice/AOA_heat0_on", "Anti-ice: AOA on.", "Ice AOA Heat0on", XPlaneCommands.IceAOAHeat0On); } }
        private XPlaneCommand IceAOAHeat1On { get { return new XPlaneCommand("sim/ice/AOA_heat1_on", "Anti-ice: AOA on.", "Ice AOA Heat1on", XPlaneCommands.IceAOAHeat1On); } }
        private XPlaneCommand IceAOAHeat0Off { get { return new XPlaneCommand("sim/ice/AOA_heat0_off", "Anti-ice: AOA off.", "Ice AOA Heat0off", XPlaneCommands.IceAOAHeat0Off); } }
        private XPlaneCommand IceAOAHeat1Off { get { return new XPlaneCommand("sim/ice/AOA_heat1_off", "Anti-ice: AOA off.", "Ice AOA Heat1off", XPlaneCommands.IceAOAHeat1Off); } }
        private XPlaneCommand IceAOAHeat0Tog { get { return new XPlaneCommand("sim/ice/AOA_heat0_tog", "Anti-ice: AOA toggle.", "Ice AOA Heat0tog", XPlaneCommands.IceAOAHeat0Tog); } }
        private XPlaneCommand IceAOAHeat1Tog { get { return new XPlaneCommand("sim/ice/AOA_heat1_tog", "Anti-ice: AOA toggle.", "Ice AOA Heat1tog", XPlaneCommands.IceAOAHeat1Tog); } }
        private XPlaneCommand IceWindowHeatOn { get { return new XPlaneCommand("sim/ice/window_heat_on", "Anti-ice: window heat on.", "Ice Window Heat On", XPlaneCommands.IceWindowHeatOn); } }
        private XPlaneCommand IceWindowHeatOff { get { return new XPlaneCommand("sim/ice/window_heat_off", "Anti-ice: window heat off.", "Ice Window Heat Off", XPlaneCommands.IceWindowHeatOff); } }
        private XPlaneCommand IceWindowHeatTog { get { return new XPlaneCommand("sim/ice/window_heat_tog", "Anti-ice: window heat toggle.", "Ice Window Heat Tog", XPlaneCommands.IceWindowHeatTog); } }
        private XPlaneCommand IceWingHeatOn { get { return new XPlaneCommand("sim/ice/wing_heat_on", "Anti-ice: all wing, anti-ice heat on.", "Ice Wing Heat On", XPlaneCommands.IceWingHeatOn); } }
        private XPlaneCommand IceWingHeat0On { get { return new XPlaneCommand("sim/ice/wing_heat0_on", "Anti-ice: left wing, anti-ice heat on.", "Ice Wing Heat0on", XPlaneCommands.IceWingHeat0On); } }
        private XPlaneCommand IceWingHeat1On { get { return new XPlaneCommand("sim/ice/wing_heat1_on", "Anti-ice: right wing, anti-ice heat on.", "Ice Wing Heat1on", XPlaneCommands.IceWingHeat1On); } }
        private XPlaneCommand IceWingHeatOff { get { return new XPlaneCommand("sim/ice/wing_heat_off", "Anti-ice: all wing, anti-ice heat off.", "Ice Wing Heat Off", XPlaneCommands.IceWingHeatOff); } }
        private XPlaneCommand IceWingHeat0Off { get { return new XPlaneCommand("sim/ice/wing_heat0_off", "Anti-ice: left wing, anti-ice heat off.", "Ice Wing Heat0off", XPlaneCommands.IceWingHeat0Off); } }
        private XPlaneCommand IceWingHeat1Off { get { return new XPlaneCommand("sim/ice/wing_heat1_off", "Anti-ice: right wing, anti-ice heat off.", "Ice Wing Heat1off", XPlaneCommands.IceWingHeat1Off); } }
        private XPlaneCommand IceWingHeatTog { get { return new XPlaneCommand("sim/ice/wing_heat_tog", "Anti-ice: all wing, anti-ice heat toggle.", "Ice Wing Heat Tog", XPlaneCommands.IceWingHeatTog); } }
        private XPlaneCommand IceWingHeat0Tog { get { return new XPlaneCommand("sim/ice/wing_heat0_tog", "Anti-ice: left wing, anti-ice heat toggle.", "Ice Wing Heat0tog", XPlaneCommands.IceWingHeat0Tog); } }
        private XPlaneCommand IceWingHeat1Tog { get { return new XPlaneCommand("sim/ice/wing_heat1_tog", "Anti-ice: right wing, anti-ice heat toggle.", "Ice Wing Heat1tog", XPlaneCommands.IceWingHeat1Tog); } }
        private XPlaneCommand IceWingBootOn { get { return new XPlaneCommand("sim/ice/wing_boot_on", "Anti-ice: all wing, de-icing boots on schedule.", "Ice Wing Boot On", XPlaneCommands.IceWingBootOn); } }
        private XPlaneCommand IceWingBoot0On { get { return new XPlaneCommand("sim/ice/wing_boot0_on", "Anti-ice: left wing, de-icing boots on schedule.", "Ice Wing Boot0on", XPlaneCommands.IceWingBoot0On); } }
        private XPlaneCommand IceWingBoot1On { get { return new XPlaneCommand("sim/ice/wing_boot1_on", "Anti-ice: right wing, de-icing boots on schedule.", "Ice Wing Boot1on", XPlaneCommands.IceWingBoot1On); } }
        private XPlaneCommand IceWingBootOff { get { return new XPlaneCommand("sim/ice/wing_boot_off", "Anti-ice: all wing, de-icing boots off.", "Ice Wing Boot Off", XPlaneCommands.IceWingBootOff); } }
        private XPlaneCommand IceWingBoot0Off { get { return new XPlaneCommand("sim/ice/wing_boot0_off", "Anti-ice: left wing, de-icing boots off.", "Ice Wing Boot0off", XPlaneCommands.IceWingBoot0Off); } }
        private XPlaneCommand IceWingBoot1Off { get { return new XPlaneCommand("sim/ice/wing_boot1_off", "Anti-ice: right wing, de-icing boots off.", "Ice Wing Boot1off", XPlaneCommands.IceWingBoot1Off); } }
        private XPlaneCommand IceWingBootTog { get { return new XPlaneCommand("sim/ice/wing_boot_tog", "Anti-ice: all wing, de-icing boots toggle.", "Ice Wing Boot Tog", XPlaneCommands.IceWingBootTog); } }
        private XPlaneCommand IceWingBoot0Tog { get { return new XPlaneCommand("sim/ice/wing_boot0_tog", "Anti-ice: left wing, de-icing boots toggle.", "Ice Wing Boot0tog", XPlaneCommands.IceWingBoot0Tog); } }
        private XPlaneCommand IceWingBoot1Tog { get { return new XPlaneCommand("sim/ice/wing_boot1_tog", "Anti-ice: right wing, de-icing boots toggle.", "Ice Wing Boot1tog", XPlaneCommands.IceWingBoot1Tog); } }
        private XPlaneCommand IceWingTaiOn { get { return new XPlaneCommand("sim/ice/wing_tai_on", "Anti-ice: all wing, bleed anti-ice on.", "Ice Wing Tai On", XPlaneCommands.IceWingTaiOn); } }
        private XPlaneCommand IceWingTai0On { get { return new XPlaneCommand("sim/ice/wing_tai0_on", "Anti-ice: left wing, bleed anti-ice on.", "Ice Wing Tai0on", XPlaneCommands.IceWingTai0On); } }
        private XPlaneCommand IceWingTai1On { get { return new XPlaneCommand("sim/ice/wing_tai1_on", "Anti-ice: right wing, bleed anti-ice on.", "Ice Wing Tai1on", XPlaneCommands.IceWingTai1On); } }
        private XPlaneCommand IceWingTaiOff { get { return new XPlaneCommand("sim/ice/wing_tai_off", "Anti-ice: all wing, bleed anti-ice off.", "Ice Wing Tai Off", XPlaneCommands.IceWingTaiOff); } }
        private XPlaneCommand IceWingTai0Off { get { return new XPlaneCommand("sim/ice/wing_tai0_off", "Anti-ice: left wing, bleed anti-ice off.", "Ice Wing Tai0off", XPlaneCommands.IceWingTai0Off); } }
        private XPlaneCommand IceWingTai1Off { get { return new XPlaneCommand("sim/ice/wing_tai1_off", "Anti-ice: right wing, bleed anti-ice off.", "Ice Wing Tai1off", XPlaneCommands.IceWingTai1Off); } }
        private XPlaneCommand IceWingTaiTog { get { return new XPlaneCommand("sim/ice/wing_tai_tog", "Anti-ice: all wing, bleed anti-ice toggle.", "Ice Wing Tai Tog", XPlaneCommands.IceWingTaiTog); } }
        private XPlaneCommand IceWingTai0Tog { get { return new XPlaneCommand("sim/ice/wing_tai0_tog", "Anti-ice: left wing, bleed anti-ice toggle.", "Ice Wing Tai0tog", XPlaneCommands.IceWingTai0Tog); } }
        private XPlaneCommand IceWingTai1Tog { get { return new XPlaneCommand("sim/ice/wing_tai1_tog", "Anti-ice: right wing, bleed anti-ice toggle.", "Ice Wing Tai1tog", XPlaneCommands.IceWingTai1Tog); } }
        private XPlaneCommand IceWingTksOn { get { return new XPlaneCommand("sim/ice/wing_tks_on", "Anti-ice: all wing, TKS de-ice normal.", "Ice Wing Tks On", XPlaneCommands.IceWingTksOn); } }
        private XPlaneCommand IceWingTks0On { get { return new XPlaneCommand("sim/ice/wing_tks0_on", "Anti-ice: left wing, TKS de-ice normal.", "Ice Wing Tks0on", XPlaneCommands.IceWingTks0On); } }
        private XPlaneCommand IceWingTks1On { get { return new XPlaneCommand("sim/ice/wing_tks1_on", "Anti-ice: right wing, TKS de-ice normal.", "Ice Wing Tks1on", XPlaneCommands.IceWingTks1On); } }
        private XPlaneCommand IceWingTksHigh { get { return new XPlaneCommand("sim/ice/wing_tks_high", "Anti-ice: all wing, TKS de-ice high.", "Ice Wing Tks High", XPlaneCommands.IceWingTksHigh); } }
        private XPlaneCommand IceWingTks0High { get { return new XPlaneCommand("sim/ice/wing_tks0_high", "Anti-ice: left wing, TKS de-ice high.", "Ice Wing Tks0high", XPlaneCommands.IceWingTks0High); } }
        private XPlaneCommand IceWingTks1High { get { return new XPlaneCommand("sim/ice/wing_tks1_high", "Anti-ice: right wing, TKS de-ice high.", "Ice Wing Tks1high", XPlaneCommands.IceWingTks1High); } }
        private XPlaneCommand IceWingTksOff { get { return new XPlaneCommand("sim/ice/wing_tks_off", "Anti-ice: all wing, TKS de-ice off.", "Ice Wing Tks Off", XPlaneCommands.IceWingTksOff); } }
        private XPlaneCommand IceWingTks0Off { get { return new XPlaneCommand("sim/ice/wing_tks0_off", "Anti-ice: left wing, TKS de-ice off.", "Ice Wing Tks0off", XPlaneCommands.IceWingTks0Off); } }
        private XPlaneCommand IceWingTks1Off { get { return new XPlaneCommand("sim/ice/wing_tks1_off", "Anti-ice: right wing, TKS de-ice off.", "Ice Wing Tks1off", XPlaneCommands.IceWingTks1Off); } }
        private XPlaneCommand IceWingTksTog { get { return new XPlaneCommand("sim/ice/wing_tks_tog", "Anti-ice: all wing, TKS de-ice toggle.", "Ice Wing Tks Tog", XPlaneCommands.IceWingTksTog); } }
        private XPlaneCommand IceWingTks0Tog { get { return new XPlaneCommand("sim/ice/wing_tks0_tog", "Anti-ice: left wing, TKS de-ice toggle.", "Ice Wing Tks0tog", XPlaneCommands.IceWingTks0Tog); } }
        private XPlaneCommand IceWingTks1Tog { get { return new XPlaneCommand("sim/ice/wing_tks1_tog", "Anti-ice: right wing, TKS de-ice toggle.", "Ice Wing Tks1tog", XPlaneCommands.IceWingTks1Tog); } }
        private XPlaneCommand IceInletHeatOn { get { return new XPlaneCommand("sim/ice/inlet_heat_on", "Anti-ice: all engines inlet heat on.", "Ice Inlet Heat On", XPlaneCommands.IceInletHeatOn); } }
        private XPlaneCommand IceInletHeatOff { get { return new XPlaneCommand("sim/ice/inlet_heat_off", "Anti-ice: all engines inlet heat off.", "Ice Inlet Heat Off", XPlaneCommands.IceInletHeatOff); } }
        private XPlaneCommand IceInletHeatTog { get { return new XPlaneCommand("sim/ice/inlet_heat_tog", "Anti-ice: all engines inlet heat toggle.", "Ice Inlet Heat Tog", XPlaneCommands.IceInletHeatTog); } }
        private XPlaneCommand IceInletHeat0On { get { return new XPlaneCommand("sim/ice/inlet_heat0_on", "Anti-ice: engine #1 inlet heat on.", "Ice Inlet Heat0on", XPlaneCommands.IceInletHeat0On); } }
        private XPlaneCommand IceInletHeat1On { get { return new XPlaneCommand("sim/ice/inlet_heat1_on", "Anti-ice: engine #2 inlet heat on.", "Ice Inlet Heat1on", XPlaneCommands.IceInletHeat1On); } }
        private XPlaneCommand IceInletHeat2On { get { return new XPlaneCommand("sim/ice/inlet_heat2_on", "Anti-ice: engine #3 inlet heat on.", "Ice Inlet Heat2on", XPlaneCommands.IceInletHeat2On); } }
        private XPlaneCommand IceInletHeat3On { get { return new XPlaneCommand("sim/ice/inlet_heat3_on", "Anti-ice: engine #4 inlet heat on.", "Ice Inlet Heat3on", XPlaneCommands.IceInletHeat3On); } }
        private XPlaneCommand IceInletHeat4On { get { return new XPlaneCommand("sim/ice/inlet_heat4_on", "Anti-ice: engine #5 inlet heat on.", "Ice Inlet Heat4on", XPlaneCommands.IceInletHeat4On); } }
        private XPlaneCommand IceInletHeat5On { get { return new XPlaneCommand("sim/ice/inlet_heat5_on", "Anti-ice: engine #6 inlet heat on.", "Ice Inlet Heat5on", XPlaneCommands.IceInletHeat5On); } }
        private XPlaneCommand IceInletHeat6On { get { return new XPlaneCommand("sim/ice/inlet_heat6_on", "Anti-ice: engine #7 inlet heat on.", "Ice Inlet Heat6on", XPlaneCommands.IceInletHeat6On); } }
        private XPlaneCommand IceInletHeat7On { get { return new XPlaneCommand("sim/ice/inlet_heat7_on", "Anti-ice: engine #8 inlet heat on.", "Ice Inlet Heat7on", XPlaneCommands.IceInletHeat7On); } }
        private XPlaneCommand IceInletHeat0Off { get { return new XPlaneCommand("sim/ice/inlet_heat0_off", "Anti-ice: engine #1 inlet heat off.", "Ice Inlet Heat0off", XPlaneCommands.IceInletHeat0Off); } }
        private XPlaneCommand IceInletHeat1Off { get { return new XPlaneCommand("sim/ice/inlet_heat1_off", "Anti-ice: engine #2 inlet heat off.", "Ice Inlet Heat1off", XPlaneCommands.IceInletHeat1Off); } }
        private XPlaneCommand IceInletHeat2Off { get { return new XPlaneCommand("sim/ice/inlet_heat2_off", "Anti-ice: engine #3 inlet heat off.", "Ice Inlet Heat2off", XPlaneCommands.IceInletHeat2Off); } }
        private XPlaneCommand IceInletHeat3Off { get { return new XPlaneCommand("sim/ice/inlet_heat3_off", "Anti-ice: engine #4 inlet heat off.", "Ice Inlet Heat3off", XPlaneCommands.IceInletHeat3Off); } }
        private XPlaneCommand IceInletHeat4Off { get { return new XPlaneCommand("sim/ice/inlet_heat4_off", "Anti-ice: engine #5 inlet heat off.", "Ice Inlet Heat4off", XPlaneCommands.IceInletHeat4Off); } }
        private XPlaneCommand IceInletHeat5Off { get { return new XPlaneCommand("sim/ice/inlet_heat5_off", "Anti-ice: engine #6 inlet heat off.", "Ice Inlet Heat5off", XPlaneCommands.IceInletHeat5Off); } }
        private XPlaneCommand IceInletHeat6Off { get { return new XPlaneCommand("sim/ice/inlet_heat6_off", "Anti-ice: engine #7 inlet heat off.", "Ice Inlet Heat6off", XPlaneCommands.IceInletHeat6Off); } }
        private XPlaneCommand IceInletHeat7Off { get { return new XPlaneCommand("sim/ice/inlet_heat7_off", "Anti-ice: engine #8 inlet heat off.", "Ice Inlet Heat7off", XPlaneCommands.IceInletHeat7Off); } }
        private XPlaneCommand IceInletHeat0Tog { get { return new XPlaneCommand("sim/ice/inlet_heat0_tog", "Anti-ice: engine #1 inlet heat toggle.", "Ice Inlet Heat0tog", XPlaneCommands.IceInletHeat0Tog); } }
        private XPlaneCommand IceInletHeat1Tog { get { return new XPlaneCommand("sim/ice/inlet_heat1_tog", "Anti-ice: engine #2 inlet heat toggle.", "Ice Inlet Heat1tog", XPlaneCommands.IceInletHeat1Tog); } }
        private XPlaneCommand IceInletHeat2Tog { get { return new XPlaneCommand("sim/ice/inlet_heat2_tog", "Anti-ice: engine #3 inlet heat toggle.", "Ice Inlet Heat2tog", XPlaneCommands.IceInletHeat2Tog); } }
        private XPlaneCommand IceInletHeat3Tog { get { return new XPlaneCommand("sim/ice/inlet_heat3_tog", "Anti-ice: engine #4 inlet heat toggle.", "Ice Inlet Heat3tog", XPlaneCommands.IceInletHeat3Tog); } }
        private XPlaneCommand IceInletHeat4Tog { get { return new XPlaneCommand("sim/ice/inlet_heat4_tog", "Anti-ice: engine #5 inlet heat toggle.", "Ice Inlet Heat4tog", XPlaneCommands.IceInletHeat4Tog); } }
        private XPlaneCommand IceInletHeat5Tog { get { return new XPlaneCommand("sim/ice/inlet_heat5_tog", "Anti-ice: engine #6 inlet heat toggle.", "Ice Inlet Heat5tog", XPlaneCommands.IceInletHeat5Tog); } }
        private XPlaneCommand IceInletHeat6Tog { get { return new XPlaneCommand("sim/ice/inlet_heat6_tog", "Anti-ice: engine #7 inlet heat toggle.", "Ice Inlet Heat6tog", XPlaneCommands.IceInletHeat6Tog); } }
        private XPlaneCommand IceInletHeat7Tog { get { return new XPlaneCommand("sim/ice/inlet_heat7_tog", "Anti-ice: engine #8 inlet heat toggle.", "Ice Inlet Heat7tog", XPlaneCommands.IceInletHeat7Tog); } }
        private XPlaneCommand IceInletEai0On { get { return new XPlaneCommand("sim/ice/inlet_eai0_on", "Anti-ice: engine #1 anti-ice on.", "Ice Inlet Eai0on", XPlaneCommands.IceInletEai0On); } }
        private XPlaneCommand IceInletEai1On { get { return new XPlaneCommand("sim/ice/inlet_eai1_on", "Anti-ice: engine #2 anti-ice on.", "Ice Inlet Eai1on", XPlaneCommands.IceInletEai1On); } }
        private XPlaneCommand IceInletEai2On { get { return new XPlaneCommand("sim/ice/inlet_eai2_on", "Anti-ice: engine #3 anti-ice on.", "Ice Inlet Eai2on", XPlaneCommands.IceInletEai2On); } }
        private XPlaneCommand IceInletEai3On { get { return new XPlaneCommand("sim/ice/inlet_eai3_on", "Anti-ice: engine #4 anti-ice on.", "Ice Inlet Eai3on", XPlaneCommands.IceInletEai3On); } }
        private XPlaneCommand IceInletEai4On { get { return new XPlaneCommand("sim/ice/inlet_eai4_on", "Anti-ice: engine #5 anti-ice on.", "Ice Inlet Eai4on", XPlaneCommands.IceInletEai4On); } }
        private XPlaneCommand IceInletEai5On { get { return new XPlaneCommand("sim/ice/inlet_eai5_on", "Anti-ice: engine #6 anti-ice on.", "Ice Inlet Eai5on", XPlaneCommands.IceInletEai5On); } }
        private XPlaneCommand IceInletEai6On { get { return new XPlaneCommand("sim/ice/inlet_eai6_on", "Anti-ice: engine #7 anti-ice on.", "Ice Inlet Eai6on", XPlaneCommands.IceInletEai6On); } }
        private XPlaneCommand IceInletEai7On { get { return new XPlaneCommand("sim/ice/inlet_eai7_on", "Anti-ice: engine #8 anti-ice on.", "Ice Inlet Eai7on", XPlaneCommands.IceInletEai7On); } }
        private XPlaneCommand IceInletEai0Off { get { return new XPlaneCommand("sim/ice/inlet_eai0_off", "Anti-ice: engine #1 anti-ice off.", "Ice Inlet Eai0off", XPlaneCommands.IceInletEai0Off); } }
        private XPlaneCommand IceInletEai1Off { get { return new XPlaneCommand("sim/ice/inlet_eai1_off", "Anti-ice: engine #2 anti-ice off.", "Ice Inlet Eai1off", XPlaneCommands.IceInletEai1Off); } }
        private XPlaneCommand IceInletEai2Off { get { return new XPlaneCommand("sim/ice/inlet_eai2_off", "Anti-ice: engine #3 anti-ice off.", "Ice Inlet Eai2off", XPlaneCommands.IceInletEai2Off); } }
        private XPlaneCommand IceInletEai3Off { get { return new XPlaneCommand("sim/ice/inlet_eai3_off", "Anti-ice: engine #4 anti-ice off.", "Ice Inlet Eai3off", XPlaneCommands.IceInletEai3Off); } }
        private XPlaneCommand IceInletEai4Off { get { return new XPlaneCommand("sim/ice/inlet_eai4_off", "Anti-ice: engine #5 anti-ice off.", "Ice Inlet Eai4off", XPlaneCommands.IceInletEai4Off); } }
        private XPlaneCommand IceInletEai5Off { get { return new XPlaneCommand("sim/ice/inlet_eai5_off", "Anti-ice: engine #6 anti-ice off.", "Ice Inlet Eai5off", XPlaneCommands.IceInletEai5Off); } }
        private XPlaneCommand IceInletEai6Off { get { return new XPlaneCommand("sim/ice/inlet_eai6_off", "Anti-ice: engine #7 anti-ice off.", "Ice Inlet Eai6off", XPlaneCommands.IceInletEai6Off); } }
        private XPlaneCommand IceInletEai7Off { get { return new XPlaneCommand("sim/ice/inlet_eai7_off", "Anti-ice: engine #8 anti-ice off.", "Ice Inlet Eai7off", XPlaneCommands.IceInletEai7Off); } }
        private XPlaneCommand IceInletEai0Tog { get { return new XPlaneCommand("sim/ice/inlet_eai0_tog", "Anti-ice: engine #1 anti-ice toggle.", "Ice Inlet Eai0tog", XPlaneCommands.IceInletEai0Tog); } }
        private XPlaneCommand IceInletEai1Tog { get { return new XPlaneCommand("sim/ice/inlet_eai1_tog", "Anti-ice: engine #2 anti-ice toggle.", "Ice Inlet Eai1tog", XPlaneCommands.IceInletEai1Tog); } }
        private XPlaneCommand IceInletEai2Tog { get { return new XPlaneCommand("sim/ice/inlet_eai2_tog", "Anti-ice: engine #3 anti-ice toggle.", "Ice Inlet Eai2tog", XPlaneCommands.IceInletEai2Tog); } }
        private XPlaneCommand IceInletEai3Tog { get { return new XPlaneCommand("sim/ice/inlet_eai3_tog", "Anti-ice: engine #4 anti-ice toggle.", "Ice Inlet Eai3tog", XPlaneCommands.IceInletEai3Tog); } }
        private XPlaneCommand IceInletEai4Tog { get { return new XPlaneCommand("sim/ice/inlet_eai4_tog", "Anti-ice: engine #5 anti-ice toggle.", "Ice Inlet Eai4tog", XPlaneCommands.IceInletEai4Tog); } }
        private XPlaneCommand IceInletEai5Tog { get { return new XPlaneCommand("sim/ice/inlet_eai5_tog", "Anti-ice: engine #6 anti-ice toggle.", "Ice Inlet Eai5tog", XPlaneCommands.IceInletEai5Tog); } }
        private XPlaneCommand IceInletEai6Tog { get { return new XPlaneCommand("sim/ice/inlet_eai6_tog", "Anti-ice: engine #7 anti-ice toggle.", "Ice Inlet Eai6tog", XPlaneCommands.IceInletEai6Tog); } }
        private XPlaneCommand IceInletEai7Tog { get { return new XPlaneCommand("sim/ice/inlet_eai7_tog", "Anti-ice: engine #8 anti-ice toggle.", "Ice Inlet Eai7tog", XPlaneCommands.IceInletEai7Tog); } }
        private XPlaneCommand IcePropHeatOn { get { return new XPlaneCommand("sim/ice/prop_heat_on", "Anti-ice: all prop heat on.", "Ice Prop Heat On", XPlaneCommands.IcePropHeatOn); } }
        private XPlaneCommand IcePropHeatOff { get { return new XPlaneCommand("sim/ice/prop_heat_off", "Anti-ice: all prop heat off.", "Ice Prop Heat Off", XPlaneCommands.IcePropHeatOff); } }
        private XPlaneCommand IcePropHeatTog { get { return new XPlaneCommand("sim/ice/prop_heat_tog", "Anti-ice: all prop heat toggle.", "Ice Prop Heat Tog", XPlaneCommands.IcePropHeatTog); } }
        private XPlaneCommand IcePropHeat0On { get { return new XPlaneCommand("sim/ice/prop_heat0_on", "Anti-ice: engine #1 prop heat on.", "Ice Prop Heat0on", XPlaneCommands.IcePropHeat0On); } }
        private XPlaneCommand IcePropHeat1On { get { return new XPlaneCommand("sim/ice/prop_heat1_on", "Anti-ice: engine #2 prop heat on.", "Ice Prop Heat1on", XPlaneCommands.IcePropHeat1On); } }
        private XPlaneCommand IcePropHeat2On { get { return new XPlaneCommand("sim/ice/prop_heat2_on", "Anti-ice: engine #3 prop heat on.", "Ice Prop Heat2on", XPlaneCommands.IcePropHeat2On); } }
        private XPlaneCommand IcePropHeat3On { get { return new XPlaneCommand("sim/ice/prop_heat3_on", "Anti-ice: engine #4 prop heat on.", "Ice Prop Heat3on", XPlaneCommands.IcePropHeat3On); } }
        private XPlaneCommand IcePropHeat4On { get { return new XPlaneCommand("sim/ice/prop_heat4_on", "Anti-ice: engine #5 prop heat on.", "Ice Prop Heat4on", XPlaneCommands.IcePropHeat4On); } }
        private XPlaneCommand IcePropHeat5On { get { return new XPlaneCommand("sim/ice/prop_heat5_on", "Anti-ice: engine #6 prop heat on.", "Ice Prop Heat5on", XPlaneCommands.IcePropHeat5On); } }
        private XPlaneCommand IcePropHeat6On { get { return new XPlaneCommand("sim/ice/prop_heat6_on", "Anti-ice: engine #7 prop heat on.", "Ice Prop Heat6on", XPlaneCommands.IcePropHeat6On); } }
        private XPlaneCommand IcePropHeat7On { get { return new XPlaneCommand("sim/ice/prop_heat7_on", "Anti-ice: engine #8 prop heat on.", "Ice Prop Heat7on", XPlaneCommands.IcePropHeat7On); } }
        private XPlaneCommand IcePropHeat0Off { get { return new XPlaneCommand("sim/ice/prop_heat0_off", "Anti-ice: engine #1 prop heat off.", "Ice Prop Heat0off", XPlaneCommands.IcePropHeat0Off); } }
        private XPlaneCommand IcePropHeat1Off { get { return new XPlaneCommand("sim/ice/prop_heat1_off", "Anti-ice: engine #2 prop heat off.", "Ice Prop Heat1off", XPlaneCommands.IcePropHeat1Off); } }
        private XPlaneCommand IcePropHeat2Off { get { return new XPlaneCommand("sim/ice/prop_heat2_off", "Anti-ice: engine #3 prop heat off.", "Ice Prop Heat2off", XPlaneCommands.IcePropHeat2Off); } }
        private XPlaneCommand IcePropHeat3Off { get { return new XPlaneCommand("sim/ice/prop_heat3_off", "Anti-ice: engine #4 prop heat off.", "Ice Prop Heat3off", XPlaneCommands.IcePropHeat3Off); } }
        private XPlaneCommand IcePropHeat4Off { get { return new XPlaneCommand("sim/ice/prop_heat4_off", "Anti-ice: engine #5 prop heat off.", "Ice Prop Heat4off", XPlaneCommands.IcePropHeat4Off); } }
        private XPlaneCommand IcePropHeat5Off { get { return new XPlaneCommand("sim/ice/prop_heat5_off", "Anti-ice: engine #6 prop heat off.", "Ice Prop Heat5off", XPlaneCommands.IcePropHeat5Off); } }
        private XPlaneCommand IcePropHeat6Off { get { return new XPlaneCommand("sim/ice/prop_heat6_off", "Anti-ice: engine #7 prop heat off.", "Ice Prop Heat6off", XPlaneCommands.IcePropHeat6Off); } }
        private XPlaneCommand IcePropHeat7Off { get { return new XPlaneCommand("sim/ice/prop_heat7_off", "Anti-ice: engine #8 prop heat off.", "Ice Prop Heat7off", XPlaneCommands.IcePropHeat7Off); } }
        private XPlaneCommand IcePropHeat0Tog { get { return new XPlaneCommand("sim/ice/prop_heat0_tog", "Anti-ice: engine #1 prop heat toggle.", "Ice Prop Heat0tog", XPlaneCommands.IcePropHeat0Tog); } }
        private XPlaneCommand IcePropHeat1Tog { get { return new XPlaneCommand("sim/ice/prop_heat1_tog", "Anti-ice: engine #2 prop heat toggle.", "Ice Prop Heat1tog", XPlaneCommands.IcePropHeat1Tog); } }
        private XPlaneCommand IcePropHeat2Tog { get { return new XPlaneCommand("sim/ice/prop_heat2_tog", "Anti-ice: engine #3 prop heat toggle.", "Ice Prop Heat2tog", XPlaneCommands.IcePropHeat2Tog); } }
        private XPlaneCommand IcePropHeat3Tog { get { return new XPlaneCommand("sim/ice/prop_heat3_tog", "Anti-ice: engine #4 prop heat toggle.", "Ice Prop Heat3tog", XPlaneCommands.IcePropHeat3Tog); } }
        private XPlaneCommand IcePropHeat4Tog { get { return new XPlaneCommand("sim/ice/prop_heat4_tog", "Anti-ice: engine #5 prop heat toggle.", "Ice Prop Heat4tog", XPlaneCommands.IcePropHeat4Tog); } }
        private XPlaneCommand IcePropHeat5Tog { get { return new XPlaneCommand("sim/ice/prop_heat5_tog", "Anti-ice: engine #6 prop heat toggle.", "Ice Prop Heat5tog", XPlaneCommands.IcePropHeat5Tog); } }
        private XPlaneCommand IcePropHeat6Tog { get { return new XPlaneCommand("sim/ice/prop_heat6_tog", "Anti-ice: engine #7 prop heat toggle.", "Ice Prop Heat6tog", XPlaneCommands.IcePropHeat6Tog); } }
        private XPlaneCommand IcePropHeat7Tog { get { return new XPlaneCommand("sim/ice/prop_heat7_tog", "Anti-ice: engine #8 prop heat toggle.", "Ice Prop Heat7tog", XPlaneCommands.IcePropHeat7Tog); } }
        private XPlaneCommand IcePropTksOn { get { return new XPlaneCommand("sim/ice/prop_tks_on", "Anti-ice: all prop TKS norm.", "Ice Prop Tks On", XPlaneCommands.IcePropTksOn); } }
        private XPlaneCommand IcePropTksHigh { get { return new XPlaneCommand("sim/ice/prop_tks_high", "Anti-ice: all prop TKS high.", "Ice Prop Tks High", XPlaneCommands.IcePropTksHigh); } }
        private XPlaneCommand IcePropTksOff { get { return new XPlaneCommand("sim/ice/prop_tks_off", "Anti-ice: all prop TKS off.", "Ice Prop Tks Off", XPlaneCommands.IcePropTksOff); } }
        private XPlaneCommand IcePropTksTog { get { return new XPlaneCommand("sim/ice/prop_tks_tog", "Anti-ice: all prop TKS toggle.", "Ice Prop Tks Tog", XPlaneCommands.IcePropTksTog); } }
        private XPlaneCommand IcePropTks0On { get { return new XPlaneCommand("sim/ice/prop_tks0_on", "Anti-ice: engine #1 prop TKS norm.", "Ice Prop Tks0on", XPlaneCommands.IcePropTks0On); } }
        private XPlaneCommand IcePropTks1On { get { return new XPlaneCommand("sim/ice/prop_tks1_on", "Anti-ice: engine #2 prop TKS norm.", "Ice Prop Tks1on", XPlaneCommands.IcePropTks1On); } }
        private XPlaneCommand IcePropTks2On { get { return new XPlaneCommand("sim/ice/prop_tks2_on", "Anti-ice: engine #3 prop TKS norm.", "Ice Prop Tks2on", XPlaneCommands.IcePropTks2On); } }
        private XPlaneCommand IcePropTks3On { get { return new XPlaneCommand("sim/ice/prop_tks3_on", "Anti-ice: engine #4 prop TKS norm.", "Ice Prop Tks3on", XPlaneCommands.IcePropTks3On); } }
        private XPlaneCommand IcePropTks4On { get { return new XPlaneCommand("sim/ice/prop_tks4_on", "Anti-ice: engine #5 prop TKS norm.", "Ice Prop Tks4on", XPlaneCommands.IcePropTks4On); } }
        private XPlaneCommand IcePropTks5On { get { return new XPlaneCommand("sim/ice/prop_tks5_on", "Anti-ice: engine #6 prop TKS norm.", "Ice Prop Tks5on", XPlaneCommands.IcePropTks5On); } }
        private XPlaneCommand IcePropTks6On { get { return new XPlaneCommand("sim/ice/prop_tks6_on", "Anti-ice: engine #7 prop TKS norm.", "Ice Prop Tks6on", XPlaneCommands.IcePropTks6On); } }
        private XPlaneCommand IcePropTks7On { get { return new XPlaneCommand("sim/ice/prop_tks7_on", "Anti-ice: engine #8 prop TKS norm.", "Ice Prop Tks7on", XPlaneCommands.IcePropTks7On); } }
        private XPlaneCommand IcePropTks0High { get { return new XPlaneCommand("sim/ice/prop_tks0_high", "Anti-ice: engine #1 prop TKS high.", "Ice Prop Tks0high", XPlaneCommands.IcePropTks0High); } }
        private XPlaneCommand IcePropTks1High { get { return new XPlaneCommand("sim/ice/prop_tks1_high", "Anti-ice: engine #2 prop TKS high.", "Ice Prop Tks1high", XPlaneCommands.IcePropTks1High); } }
        private XPlaneCommand IcePropTks2High { get { return new XPlaneCommand("sim/ice/prop_tks2_high", "Anti-ice: engine #3 prop TKS high.", "Ice Prop Tks2high", XPlaneCommands.IcePropTks2High); } }
        private XPlaneCommand IcePropTks3High { get { return new XPlaneCommand("sim/ice/prop_tks3_high", "Anti-ice: engine #4 prop TKS high.", "Ice Prop Tks3high", XPlaneCommands.IcePropTks3High); } }
        private XPlaneCommand IcePropTks4High { get { return new XPlaneCommand("sim/ice/prop_tks4_high", "Anti-ice: engine #5 prop TKS high.", "Ice Prop Tks4high", XPlaneCommands.IcePropTks4High); } }
        private XPlaneCommand IcePropTks5High { get { return new XPlaneCommand("sim/ice/prop_tks5_high", "Anti-ice: engine #6 prop TKS high.", "Ice Prop Tks5high", XPlaneCommands.IcePropTks5High); } }
        private XPlaneCommand IcePropTks6High { get { return new XPlaneCommand("sim/ice/prop_tks6_high", "Anti-ice: engine #7 prop TKS high.", "Ice Prop Tks6high", XPlaneCommands.IcePropTks6High); } }
        private XPlaneCommand IcePropTks7High { get { return new XPlaneCommand("sim/ice/prop_tks7_high", "Anti-ice: engine #8 prop TKS high.", "Ice Prop Tks7high", XPlaneCommands.IcePropTks7High); } }
        private XPlaneCommand IcePropTks0Off { get { return new XPlaneCommand("sim/ice/prop_tks0_off", "Anti-ice: engine #1 prop TKS off.", "Ice Prop Tks0off", XPlaneCommands.IcePropTks0Off); } }
        private XPlaneCommand IcePropTks1Off { get { return new XPlaneCommand("sim/ice/prop_tks1_off", "Anti-ice: engine #2 prop TKS off.", "Ice Prop Tks1off", XPlaneCommands.IcePropTks1Off); } }
        private XPlaneCommand IcePropTks2Off { get { return new XPlaneCommand("sim/ice/prop_tks2_off", "Anti-ice: engine #3 prop TKS off.", "Ice Prop Tks2off", XPlaneCommands.IcePropTks2Off); } }
        private XPlaneCommand IcePropTks3Off { get { return new XPlaneCommand("sim/ice/prop_tks3_off", "Anti-ice: engine #4 prop TKS off.", "Ice Prop Tks3off", XPlaneCommands.IcePropTks3Off); } }
        private XPlaneCommand IcePropTks4Off { get { return new XPlaneCommand("sim/ice/prop_tks4_off", "Anti-ice: engine #5 prop TKS off.", "Ice Prop Tks4off", XPlaneCommands.IcePropTks4Off); } }
        private XPlaneCommand IcePropTks5Off { get { return new XPlaneCommand("sim/ice/prop_tks5_off", "Anti-ice: engine #6 prop TKS off.", "Ice Prop Tks5off", XPlaneCommands.IcePropTks5Off); } }
        private XPlaneCommand IcePropTks6Off { get { return new XPlaneCommand("sim/ice/prop_tks6_off", "Anti-ice: engine #7 prop TKS off.", "Ice Prop Tks6off", XPlaneCommands.IcePropTks6Off); } }
        private XPlaneCommand IcePropTks7Off { get { return new XPlaneCommand("sim/ice/prop_tks7_off", "Anti-ice: engine #8 prop TKS off.", "Ice Prop Tks7off", XPlaneCommands.IcePropTks7Off); } }
        private XPlaneCommand IcePropTks0Tog { get { return new XPlaneCommand("sim/ice/prop_tks0_tog", "Anti-ice: engine #1 prop TKS toggle.", "Ice Prop Tks0tog", XPlaneCommands.IcePropTks0Tog); } }
        private XPlaneCommand IcePropTks1Tog { get { return new XPlaneCommand("sim/ice/prop_tks1_tog", "Anti-ice: engine #2 prop TKS toggle.", "Ice Prop Tks1tog", XPlaneCommands.IcePropTks1Tog); } }
        private XPlaneCommand IcePropTks2Tog { get { return new XPlaneCommand("sim/ice/prop_tks2_tog", "Anti-ice: engine #3 prop TKS toggle.", "Ice Prop Tks2tog", XPlaneCommands.IcePropTks2Tog); } }
        private XPlaneCommand IcePropTks3Tog { get { return new XPlaneCommand("sim/ice/prop_tks3_tog", "Anti-ice: engine #4 prop TKS toggle.", "Ice Prop Tks3tog", XPlaneCommands.IcePropTks3Tog); } }
        private XPlaneCommand IcePropTks4Tog { get { return new XPlaneCommand("sim/ice/prop_tks4_tog", "Anti-ice: engine #5 prop TKS toggle.", "Ice Prop Tks4tog", XPlaneCommands.IcePropTks4Tog); } }
        private XPlaneCommand IcePropTks5Tog { get { return new XPlaneCommand("sim/ice/prop_tks5_tog", "Anti-ice: engine #6 prop TKS toggle.", "Ice Prop Tks5tog", XPlaneCommands.IcePropTks5Tog); } }
        private XPlaneCommand IcePropTks6Tog { get { return new XPlaneCommand("sim/ice/prop_tks6_tog", "Anti-ice: engine #7 prop TKS toggle.", "Ice Prop Tks6tog", XPlaneCommands.IcePropTks6Tog); } }
        private XPlaneCommand IcePropTks7Tog { get { return new XPlaneCommand("sim/ice/prop_tks7_tog", "Anti-ice: engine #8 prop TKS toggle.", "Ice Prop Tks7tog", XPlaneCommands.IcePropTks7Tog); } }
        private XPlaneCommand IceDetectOn { get { return new XPlaneCommand("sim/ice/detect_on", "Anti-ice: ice detection on.", "Ice Detect On", XPlaneCommands.IceDetectOn); } }
        private XPlaneCommand IceDetectOff { get { return new XPlaneCommand("sim/ice/detect_off", "Anti-ice: ice detection off.", "Ice Detect Off", XPlaneCommands.IceDetectOff); } }
        private XPlaneCommand OxyCrewValveOn { get { return new XPlaneCommand("sim/oxy/crew_valve_on", "Crew oxygen: master valve open/on.", "Oxy Crew Valve On", XPlaneCommands.OxyCrewValveOn); } }
        private XPlaneCommand OxyCrewValveOff { get { return new XPlaneCommand("sim/oxy/crew_valve_off", "Crew oxygen: master valve closed/off.", "Oxy Crew Valve Off", XPlaneCommands.OxyCrewValveOff); } }
        private XPlaneCommand OxyCrewValveToggle { get { return new XPlaneCommand("sim/oxy/crew_valve_toggle", "Crew oxygen: master valve toggle", "Oxy Crew Valve Toggle", XPlaneCommands.OxyCrewValveToggle); } }
        private XPlaneCommand OxyCrewRegulatorUp { get { return new XPlaneCommand("sim/oxy/crew_regulator_up", "Crew oxygen: demand regulator up.", "Oxy Crew Regulator Up", XPlaneCommands.OxyCrewRegulatorUp); } }
        private XPlaneCommand OxyCrewRegulatorDown { get { return new XPlaneCommand("sim/oxy/crew_regulator_down", "Crew oxygen: demand regulator down.", "Oxy Crew Regulator Down", XPlaneCommands.OxyCrewRegulatorDown); } }
        private XPlaneCommand OxyPassengerO2On { get { return new XPlaneCommand("sim/oxy/passenger_o2_on", "Passenger oxygen: drop masks.", "Oxy Passenger O2on", XPlaneCommands.OxyPassengerO2On); } }
        private XPlaneCommand FlightControlsParachuteFlares { get { return new XPlaneCommand("sim/flight_controls/parachute_flares", "Deploy parachute flares.", "Flight Controls Parachute Flares", XPlaneCommands.FlightControlsParachuteFlares); } }
        private XPlaneCommand FlightControlsSmokeToggle { get { return new XPlaneCommand("sim/flight_controls/smoke_toggle", "Toggle smoke puffing.", "Flight Controls Smoke Toggle", XPlaneCommands.FlightControlsSmokeToggle); } }
        private XPlaneCommand FlightControlsWaterScoopToggle { get { return new XPlaneCommand("sim/flight_controls/water_scoop_toggle", "Toggle water scoop.", "Flight Controls Water Scoop Toggle", XPlaneCommands.FlightControlsWaterScoopToggle); } }
        private XPlaneCommand FlightControlsBoost { get { return new XPlaneCommand("sim/flight_controls/boost", "Water or Nitrous engine BOOST.", "Flight Controls Boost", XPlaneCommands.FlightControlsBoost); } }
        private XPlaneCommand FlightControlsIgniteJato { get { return new XPlaneCommand("sim/flight_controls/ignite_jato", "Ignite JATO.", "Flight Controls Ignite Jato", XPlaneCommands.FlightControlsIgniteJato); } }
        private XPlaneCommand FlightControlsJettisonPayload { get { return new XPlaneCommand("sim/flight_controls/jettison_payload", "Jettison the payload.", "Flight Controls Jettison Payload", XPlaneCommands.FlightControlsJettisonPayload); } }
        private XPlaneCommand FlightControlsDumpFuelOn { get { return new XPlaneCommand("sim/flight_controls/dump_fuel_on", "Dump fuel on.", "Flight Controls Dump Fuel On", XPlaneCommands.FlightControlsDumpFuelOn); } }
        private XPlaneCommand FlightControlsDumpFuelOff { get { return new XPlaneCommand("sim/flight_controls/dump_fuel_off", "Dump fuel off.", "Flight Controls Dump Fuel Off", XPlaneCommands.FlightControlsDumpFuelOff); } }
        private XPlaneCommand FlightControlsDumpFuelToggle { get { return new XPlaneCommand("sim/flight_controls/dump_fuel_toggle", "Dump fuel toggle.", "Flight Controls Dump Fuel Toggle", XPlaneCommands.FlightControlsDumpFuelToggle); } }
        private XPlaneCommand FlightControlsDeployParachute { get { return new XPlaneCommand("sim/flight_controls/deploy_parachute", "Deploy/jettison chute.", "Flight Controls Deploy Parachute", XPlaneCommands.FlightControlsDeployParachute); } }
        private XPlaneCommand FlightControlsEject { get { return new XPlaneCommand("sim/flight_controls/eject", "Eject.", "Flight Controls Eject", XPlaneCommands.FlightControlsEject); } }
        private XPlaneCommand FlightControlsDropTank { get { return new XPlaneCommand("sim/flight_controls/drop_tank", "Drop all drop tanks.", "Flight Controls Drop Tank", XPlaneCommands.FlightControlsDropTank); } }
        private XPlaneCommand WeaponsReArmAircraft { get { return new XPlaneCommand("sim/weapons/re_arm_aircraft", "Re-arm aircraft to default specs.", "Weapons Re Arm Aircraft", XPlaneCommands.WeaponsReArmAircraft); } }
        private XPlaneCommand WeaponsMasterArmOn { get { return new XPlaneCommand("sim/weapons/master_arm_on", "Master arm on.", "Weapons Master Arm On", XPlaneCommands.WeaponsMasterArmOn); } }
        private XPlaneCommand WeaponsMasterArmOff { get { return new XPlaneCommand("sim/weapons/master_arm_off", "Master arm off.", "Weapons Master Arm Off", XPlaneCommands.WeaponsMasterArmOff); } }
        private XPlaneCommand WeaponsFireModeDown { get { return new XPlaneCommand("sim/weapons/fire_mode_down", "Weapon single/pair/ripple/salvo down.", "Weapons Fire Mode Down", XPlaneCommands.WeaponsFireModeDown); } }
        private XPlaneCommand WeaponsFireModeUp { get { return new XPlaneCommand("sim/weapons/fire_mode_up", "Weapon single/pair/ripple/salvo up.", "Weapons Fire Mode Up", XPlaneCommands.WeaponsFireModeUp); } }
        private XPlaneCommand WeaponsFireRateDown { get { return new XPlaneCommand("sim/weapons/fire_rate_down", "Weapon fire rate down.", "Weapons Fire Rate Down", XPlaneCommands.WeaponsFireRateDown); } }
        private XPlaneCommand WeaponsFireRateUp { get { return new XPlaneCommand("sim/weapons/fire_rate_up", "Weapon fire rate up.", "Weapons Fire Rate Up", XPlaneCommands.WeaponsFireRateUp); } }
        private XPlaneCommand WeaponsWeaponSelectDown { get { return new XPlaneCommand("sim/weapons/weapon_select_down", "Weapon select down.", "Weapons Weapon Select Down", XPlaneCommands.WeaponsWeaponSelectDown); } }
        private XPlaneCommand WeaponsWeaponSelectUp { get { return new XPlaneCommand("sim/weapons/weapon_select_up", "Weapon select up.", "Weapons Weapon Select Up", XPlaneCommands.WeaponsWeaponSelectUp); } }
        private XPlaneCommand WeaponsFireGuns { get { return new XPlaneCommand("sim/weapons/fire_guns", "Fire guns.", "Weapons Fire Guns", XPlaneCommands.WeaponsFireGuns); } }
        private XPlaneCommand WeaponsFireAirToAir { get { return new XPlaneCommand("sim/weapons/fire_air_to_air", "Fire air-to-air selection.", "Weapons Fire Air To Air", XPlaneCommands.WeaponsFireAirToAir); } }
        private XPlaneCommand WeaponsFireAirToGround { get { return new XPlaneCommand("sim/weapons/fire_air_to_ground", "Fire air-to-ground selection.", "Weapons Fire Air To Ground", XPlaneCommands.WeaponsFireAirToGround); } }
        private XPlaneCommand WeaponsFireAnyArmed { get { return new XPlaneCommand("sim/weapons/fire_any_armed", "Fire all armed selections.", "Weapons Fire Any Armed", XPlaneCommands.WeaponsFireAnyArmed); } }
        private XPlaneCommand WeaponsFireAnyShell { get { return new XPlaneCommand("sim/weapons/fire_any_shell", "Fire selected weapon.", "Weapons Fire Any Shell", XPlaneCommands.WeaponsFireAnyShell); } }
        private XPlaneCommand WeaponsGPSLockHere { get { return new XPlaneCommand("sim/weapons/GPS_lock_here", "Target camera pointer in GPS.", "Weapons GPS Lock Here", XPlaneCommands.WeaponsGPSLockHere); } }
        private XPlaneCommand WeaponsWeaponTargetDown { get { return new XPlaneCommand("sim/weapons/weapon_target_down", "Target select down.", "Weapons Weapon Target Down", XPlaneCommands.WeaponsWeaponTargetDown); } }
        private XPlaneCommand WeaponsWeaponTargetUp { get { return new XPlaneCommand("sim/weapons/weapon_target_up", "Target select up.", "Weapons Weapon Target Up", XPlaneCommands.WeaponsWeaponTargetUp); } }
        private XPlaneCommand WeaponsDeployChaff { get { return new XPlaneCommand("sim/weapons/deploy_chaff", "Deploy chaff.", "Weapons Deploy Chaff", XPlaneCommands.WeaponsDeployChaff); } }
        private XPlaneCommand WeaponsDeployFlares { get { return new XPlaneCommand("sim/weapons/deploy_flares", "Deploy flares.", "Weapons Deploy Flares", XPlaneCommands.WeaponsDeployFlares); } }
        private XPlaneCommand OperationPrevLivery { get { return new XPlaneCommand("sim/operation/prev_livery", "Load previous livery.", "Operation Prev Livery", XPlaneCommands.OperationPrevLivery); } }
        private XPlaneCommand OperationNextLivery { get { return new XPlaneCommand("sim/operation/next_livery", "Load next livery.", "Operation Next Livery", XPlaneCommands.OperationNextLivery); } }
        private XPlaneCommand SystemsSeatbeltSignToggle { get { return new XPlaneCommand("sim/systems/seatbelt_sign_toggle", "Toggle seatbelt sign.", "Systems Seatbelt Sign Toggle", XPlaneCommands.SystemsSeatbeltSignToggle); } }
        private XPlaneCommand SystemsNoSmokingToggle { get { return new XPlaneCommand("sim/systems/no_smoking_toggle", "Toggle smoking sign.", "Systems No Smoking Toggle", XPlaneCommands.SystemsNoSmokingToggle); } }
        private XPlaneCommand SystemsWipersDn { get { return new XPlaneCommand("sim/systems/wipers_dn", "Windshield wipers down.", "Systems Wipers Dn", XPlaneCommands.SystemsWipersDn); } }
        private XPlaneCommand SystemsWipersUp { get { return new XPlaneCommand("sim/systems/wipers_up", "Windshield wipers up.", "Systems Wipers Up", XPlaneCommands.SystemsWipersUp); } }
        private XPlaneCommand LightsSpotLightLeft { get { return new XPlaneCommand("sim/lights/spot_light_left", "Aim spotlight left.", "Lights Spot Light Left", XPlaneCommands.LightsSpotLightLeft); } }
        private XPlaneCommand LightsSpotLightRight { get { return new XPlaneCommand("sim/lights/spot_light_right", "Aim spotlight right.", "Lights Spot Light Right", XPlaneCommands.LightsSpotLightRight); } }
        private XPlaneCommand LightsSpotLightUp { get { return new XPlaneCommand("sim/lights/spot_light_up", "Aim spotlight up.", "Lights Spot Light Up", XPlaneCommands.LightsSpotLightUp); } }
        private XPlaneCommand LightsSpotLightDown { get { return new XPlaneCommand("sim/lights/spot_light_down", "Aim spotlight down.", "Lights Spot Light Down", XPlaneCommands.LightsSpotLightDown); } }
        private XPlaneCommand LightsSpotLightCenter { get { return new XPlaneCommand("sim/lights/spot_light_center", "Aim spotlight to center.", "Lights Spot Light Center", XPlaneCommands.LightsSpotLightCenter); } }
        private XPlaneCommand FlightControlsDoorOpen1 { get { return new XPlaneCommand("sim/flight_controls/door_open_1", "Door #1 open.", "Flight Controls Door Open1", XPlaneCommands.FlightControlsDoorOpen1); } }
        private XPlaneCommand FlightControlsDoorOpen2 { get { return new XPlaneCommand("sim/flight_controls/door_open_2", "Door #2 open.", "Flight Controls Door Open2", XPlaneCommands.FlightControlsDoorOpen2); } }
        private XPlaneCommand FlightControlsDoorOpen3 { get { return new XPlaneCommand("sim/flight_controls/door_open_3", "Door #3 open.", "Flight Controls Door Open3", XPlaneCommands.FlightControlsDoorOpen3); } }
        private XPlaneCommand FlightControlsDoorOpen4 { get { return new XPlaneCommand("sim/flight_controls/door_open_4", "Door #4 open.", "Flight Controls Door Open4", XPlaneCommands.FlightControlsDoorOpen4); } }
        private XPlaneCommand FlightControlsDoorOpen5 { get { return new XPlaneCommand("sim/flight_controls/door_open_5", "Door #5 open.", "Flight Controls Door Open5", XPlaneCommands.FlightControlsDoorOpen5); } }
        private XPlaneCommand FlightControlsDoorOpen6 { get { return new XPlaneCommand("sim/flight_controls/door_open_6", "Door #6 open.", "Flight Controls Door Open6", XPlaneCommands.FlightControlsDoorOpen6); } }
        private XPlaneCommand FlightControlsDoorOpen7 { get { return new XPlaneCommand("sim/flight_controls/door_open_7", "Door #7 open.", "Flight Controls Door Open7", XPlaneCommands.FlightControlsDoorOpen7); } }
        private XPlaneCommand FlightControlsDoorOpen8 { get { return new XPlaneCommand("sim/flight_controls/door_open_8", "Door #8 open.", "Flight Controls Door Open8", XPlaneCommands.FlightControlsDoorOpen8); } }
        private XPlaneCommand FlightControlsDoorOpen9 { get { return new XPlaneCommand("sim/flight_controls/door_open_9", "Door #9 open.", "Flight Controls Door Open9", XPlaneCommands.FlightControlsDoorOpen9); } }
        private XPlaneCommand FlightControlsDoorOpen10 { get { return new XPlaneCommand("sim/flight_controls/door_open_10", "Door #10 open.", "Flight Controls Door Open10", XPlaneCommands.FlightControlsDoorOpen10); } }
        private XPlaneCommand FlightControlsDoorClose1 { get { return new XPlaneCommand("sim/flight_controls/door_close_1", "Door #1 close.", "Flight Controls Door Close1", XPlaneCommands.FlightControlsDoorClose1); } }
        private XPlaneCommand FlightControlsDoorClose2 { get { return new XPlaneCommand("sim/flight_controls/door_close_2", "Door #2 close.", "Flight Controls Door Close2", XPlaneCommands.FlightControlsDoorClose2); } }
        private XPlaneCommand FlightControlsDoorClose3 { get { return new XPlaneCommand("sim/flight_controls/door_close_3", "Door #3 close.", "Flight Controls Door Close3", XPlaneCommands.FlightControlsDoorClose3); } }
        private XPlaneCommand FlightControlsDoorClose4 { get { return new XPlaneCommand("sim/flight_controls/door_close_4", "Door #4 close.", "Flight Controls Door Close4", XPlaneCommands.FlightControlsDoorClose4); } }
        private XPlaneCommand FlightControlsDoorClose5 { get { return new XPlaneCommand("sim/flight_controls/door_close_5", "Door #5 close.", "Flight Controls Door Close5", XPlaneCommands.FlightControlsDoorClose5); } }
        private XPlaneCommand FlightControlsDoorClose6 { get { return new XPlaneCommand("sim/flight_controls/door_close_6", "Door #6 close.", "Flight Controls Door Close6", XPlaneCommands.FlightControlsDoorClose6); } }
        private XPlaneCommand FlightControlsDoorClose7 { get { return new XPlaneCommand("sim/flight_controls/door_close_7", "Door #7 close.", "Flight Controls Door Close7", XPlaneCommands.FlightControlsDoorClose7); } }
        private XPlaneCommand FlightControlsDoorClose8 { get { return new XPlaneCommand("sim/flight_controls/door_close_8", "Door #8 close.", "Flight Controls Door Close8", XPlaneCommands.FlightControlsDoorClose8); } }
        private XPlaneCommand FlightControlsDoorClose9 { get { return new XPlaneCommand("sim/flight_controls/door_close_9", "Door #9 close.", "Flight Controls Door Close9", XPlaneCommands.FlightControlsDoorClose9); } }
        private XPlaneCommand FlightControlsDoorClose10 { get { return new XPlaneCommand("sim/flight_controls/door_close_10", "Door #10 close.", "Flight Controls Door Close10", XPlaneCommands.FlightControlsDoorClose10); } }
        private XPlaneCommand GeneralAction { get { return new XPlaneCommand("sim/general/action", "General action command.", "General Action", XPlaneCommands.GeneralAction); } }
        private XPlaneCommand FlightControlsGliderTowRelease { get { return new XPlaneCommand("sim/flight_controls/glider_tow_release", "Release tow-plane cable for gliders.", "Flight Controls Glider Tow Release", XPlaneCommands.FlightControlsGliderTowRelease); } }
        private XPlaneCommand FlightControlsWinchRelease { get { return new XPlaneCommand("sim/flight_controls/winch_release", "Release winch (for gliders).", "Flight Controls Winch Release", XPlaneCommands.FlightControlsWinchRelease); } }
        private XPlaneCommand FlightControlsGliderAllRelease { get { return new XPlaneCommand("sim/flight_controls/glider_all_release", "Release tow-plane and winch for gliders.", "Flight Controls Glider All Release", XPlaneCommands.FlightControlsGliderAllRelease); } }
        private XPlaneCommand FlightControlsEngageCatShot { get { return new XPlaneCommand("sim/flight_controls/engage_cat_shot", "Engage catapault.", "Flight Controls Engage Cat Shot", XPlaneCommands.FlightControlsEngageCatShot); } }
        private XPlaneCommand FlightControlsGliderTowLeft { get { return new XPlaneCommand("sim/flight_controls/glider_tow_left", "Tow-plane for gliders: Take me left.", "Flight Controls Glider Tow Left", XPlaneCommands.FlightControlsGliderTowLeft); } }
        private XPlaneCommand FlightControlsGliderTowStraight { get { return new XPlaneCommand("sim/flight_controls/glider_tow_straight", "Tow-plane for gliders: Take me straight.", "Flight Controls Glider Tow Straight", XPlaneCommands.FlightControlsGliderTowStraight); } }
        private XPlaneCommand FlightControlsGliderTowRight { get { return new XPlaneCommand("sim/flight_controls/glider_tow_right", "Tow-plane for gliders: Take me right.", "Flight Controls Glider Tow Right", XPlaneCommands.FlightControlsGliderTowRight); } }
        private XPlaneCommand FlightControlsWinchFaster { get { return new XPlaneCommand("sim/flight_controls/winch_faster", "Winch faster. (for gliders).", "Flight Controls Winch Faster", XPlaneCommands.FlightControlsWinchFaster); } }
        private XPlaneCommand FlightControlsWinchSlower { get { return new XPlaneCommand("sim/flight_controls/winch_slower", "Winch slower. (for gliders).", "Flight Controls Winch Slower", XPlaneCommands.FlightControlsWinchSlower); } }
        private XPlaneCommand GroundOpsServicePlane { get { return new XPlaneCommand("sim/ground_ops/service_plane", "Service the airplane with ground trucks.", "Ground Ops Service Plane", XPlaneCommands.GroundOpsServicePlane); } }
        private XPlaneCommand GroundOpsPushbackLeft { get { return new XPlaneCommand("sim/ground_ops/pushback_left", "Push-back for airliners: Nose 90 left.", "Ground Ops Pushback Left", XPlaneCommands.GroundOpsPushbackLeft); } }
        private XPlaneCommand GroundOpsPushbackStraight { get { return new XPlaneCommand("sim/ground_ops/pushback_straight", "Push-back for airliners: Straight back.", "Ground Ops Pushback Straight", XPlaneCommands.GroundOpsPushbackStraight); } }
        private XPlaneCommand GroundOpsPushbackRight { get { return new XPlaneCommand("sim/ground_ops/pushback_right", "Push-back for airliners: Nose 90 right.", "Ground Ops Pushback Right", XPlaneCommands.GroundOpsPushbackRight); } }
        private XPlaneCommand GroundOpsPushbackStop { get { return new XPlaneCommand("sim/ground_ops/pushback_stop", "Push-back for airliners: Stop and let go.", "Ground Ops Pushback Stop", XPlaneCommands.GroundOpsPushbackStop); } }
        private XPlaneCommand GroundOpsToggleWindow { get { return new XPlaneCommand("sim/ground_ops/toggle_window", "Toggle the ground handling window.", "Ground Ops Toggle Window", XPlaneCommands.GroundOpsToggleWindow); } }
        private XPlaneCommand RadiosPowerNav1Off { get { return new XPlaneCommand("sim/radios/power_nav1_off", "Power NAV1 off.", "Radios Power Nav1off", XPlaneCommands.RadiosPowerNav1Off); } }
        private XPlaneCommand RadiosPowerNav1On { get { return new XPlaneCommand("sim/radios/power_nav1_on", "Power NAV1 on.", "Radios Power Nav1on", XPlaneCommands.RadiosPowerNav1On); } }
        private XPlaneCommand RadiosPowerNav2Off { get { return new XPlaneCommand("sim/radios/power_nav2_off", "Power NAV2 off.", "Radios Power Nav2off", XPlaneCommands.RadiosPowerNav2Off); } }
        private XPlaneCommand RadiosPowerNav2On { get { return new XPlaneCommand("sim/radios/power_nav2_on", "Power NAV2 on.", "Radios Power Nav2on", XPlaneCommands.RadiosPowerNav2On); } }
        private XPlaneCommand RadiosPowerCom1Off { get { return new XPlaneCommand("sim/radios/power_com1_off", "Power COM1 off.", "Radios Power Com1off", XPlaneCommands.RadiosPowerCom1Off); } }
        private XPlaneCommand RadiosPowerCom1On { get { return new XPlaneCommand("sim/radios/power_com1_on", "Power COM1 on.", "Radios Power Com1on", XPlaneCommands.RadiosPowerCom1On); } }
        private XPlaneCommand RadiosPowerCom2Off { get { return new XPlaneCommand("sim/radios/power_com2_off", "Power COM2 off.", "Radios Power Com2off", XPlaneCommands.RadiosPowerCom2Off); } }
        private XPlaneCommand RadiosPowerCom2On { get { return new XPlaneCommand("sim/radios/power_com2_on", "Power COM2 on.", "Radios Power Com2on", XPlaneCommands.RadiosPowerCom2On); } }
        private XPlaneCommand RadiosPowerAdf1Dn { get { return new XPlaneCommand("sim/radios/power_adf1_dn", "Power ADF1 dn.", "Radios Power Adf1dn", XPlaneCommands.RadiosPowerAdf1Dn); } }
        private XPlaneCommand RadiosPowerAdf1Up { get { return new XPlaneCommand("sim/radios/power_adf1_up", "Power ADF1 up.", "Radios Power Adf1up", XPlaneCommands.RadiosPowerAdf1Up); } }
        private XPlaneCommand RadiosPowerAdf2Dn { get { return new XPlaneCommand("sim/radios/power_adf2_dn", "Power ADF2 dn.", "Radios Power Adf2dn", XPlaneCommands.RadiosPowerAdf2Dn); } }
        private XPlaneCommand RadiosPowerAdf2Up { get { return new XPlaneCommand("sim/radios/power_adf2_up", "Power ADF2 up.", "Radios Power Adf2up", XPlaneCommands.RadiosPowerAdf2Up); } }
        private XPlaneCommand RadiosAdf1PowerMode0 { get { return new XPlaneCommand("sim/radios/adf1_power_mode_0", "ADF 1 off.", "Radios Adf1power Mode0", XPlaneCommands.RadiosAdf1PowerMode0); } }
        private XPlaneCommand RadiosAdf1PowerMode1 { get { return new XPlaneCommand("sim/radios/adf1_power_mode_1", "ADF 1 antenna.", "Radios Adf1power Mode1", XPlaneCommands.RadiosAdf1PowerMode1); } }
        private XPlaneCommand RadiosAdf1PowerMode2 { get { return new XPlaneCommand("sim/radios/adf1_power_mode_2", "ADF 1 on.", "Radios Adf1power Mode2", XPlaneCommands.RadiosAdf1PowerMode2); } }
        private XPlaneCommand RadiosAdf1PowerMode3 { get { return new XPlaneCommand("sim/radios/adf1_power_mode_3", "ADF 1 tone.", "Radios Adf1power Mode3", XPlaneCommands.RadiosAdf1PowerMode3); } }
        private XPlaneCommand RadiosAdf1PowerMode4 { get { return new XPlaneCommand("sim/radios/adf1_power_mode_4", "ADF 1 test.", "Radios Adf1power Mode4", XPlaneCommands.RadiosAdf1PowerMode4); } }
        private XPlaneCommand RadiosAdf2PowerMode0 { get { return new XPlaneCommand("sim/radios/adf2_power_mode_0", "ADF 2 off.", "Radios Adf2power Mode0", XPlaneCommands.RadiosAdf2PowerMode0); } }
        private XPlaneCommand RadiosAdf2PowerMode1 { get { return new XPlaneCommand("sim/radios/adf2_power_mode_1", "ADF 2 antenna.", "Radios Adf2power Mode1", XPlaneCommands.RadiosAdf2PowerMode1); } }
        private XPlaneCommand RadiosAdf2PowerMode2 { get { return new XPlaneCommand("sim/radios/adf2_power_mode_2", "ADF 2 on.", "Radios Adf2power Mode2", XPlaneCommands.RadiosAdf2PowerMode2); } }
        private XPlaneCommand RadiosAdf2PowerMode3 { get { return new XPlaneCommand("sim/radios/adf2_power_mode_3", "ADF 2 tone.", "Radios Adf2power Mode3", XPlaneCommands.RadiosAdf2PowerMode3); } }
        private XPlaneCommand RadiosAdf2PowerMode4 { get { return new XPlaneCommand("sim/radios/adf2_power_mode_4", "ADF 2 test.", "Radios Adf2power Mode4", XPlaneCommands.RadiosAdf2PowerMode4); } }
        private XPlaneCommand RadiosActvCom1CoarseDown { get { return new XPlaneCommand("sim/radios/actv_com1_coarse_down", "COM 1 coarse down.", "Radios Actv Com1coarse Down", XPlaneCommands.RadiosActvCom1CoarseDown); } }
        private XPlaneCommand RadiosActvCom1CoarseUp { get { return new XPlaneCommand("sim/radios/actv_com1_coarse_up", "COM 1 coarse up.", "Radios Actv Com1coarse Up", XPlaneCommands.RadiosActvCom1CoarseUp); } }
        private XPlaneCommand RadiosActvCom1FineDown { get { return new XPlaneCommand("sim/radios/actv_com1_fine_down", "COM 1 fine down (25kHz).", "Radios Actv Com1fine Down", XPlaneCommands.RadiosActvCom1FineDown); } }
        private XPlaneCommand RadiosActvCom1FineUp { get { return new XPlaneCommand("sim/radios/actv_com1_fine_up", "COM 1 fine up (25kHz).", "Radios Actv Com1fine Up", XPlaneCommands.RadiosActvCom1FineUp); } }
        private XPlaneCommand RadiosActvCom1CoarseDown833 { get { return new XPlaneCommand("sim/radios/actv_com1_coarse_down_833", "COM 1 coarse down (8.33 kHz).", "Radios Actv Com1coarse Down833", XPlaneCommands.RadiosActvCom1CoarseDown833); } }
        private XPlaneCommand RadiosActvCom1CoarseUp833 { get { return new XPlaneCommand("sim/radios/actv_com1_coarse_up_833", "COM 1 coarse up (8.33 kHz).", "Radios Actv Com1coarse Up833", XPlaneCommands.RadiosActvCom1CoarseUp833); } }
        private XPlaneCommand RadiosActvCom1FineDown833 { get { return new XPlaneCommand("sim/radios/actv_com1_fine_down_833", "COM 1 fine down (8.33 kHz).", "Radios Actv Com1fine Down833", XPlaneCommands.RadiosActvCom1FineDown833); } }
        private XPlaneCommand RadiosActvCom1FineUp833 { get { return new XPlaneCommand("sim/radios/actv_com1_fine_up_833", "COM 1 fine up (8.33 kHz).", "Radios Actv Com1fine Up833", XPlaneCommands.RadiosActvCom1FineUp833); } }
        private XPlaneCommand RadiosStbyCom1CoarseDown { get { return new XPlaneCommand("sim/radios/stby_com1_coarse_down", "COM 1 standby coarse down.", "Radios Stby Com1coarse Down", XPlaneCommands.RadiosStbyCom1CoarseDown); } }
        private XPlaneCommand RadiosStbyCom1CoarseUp { get { return new XPlaneCommand("sim/radios/stby_com1_coarse_up", "COM 1 standby coarse up.", "Radios Stby Com1coarse Up", XPlaneCommands.RadiosStbyCom1CoarseUp); } }
        private XPlaneCommand RadiosStbyCom1FineDown { get { return new XPlaneCommand("sim/radios/stby_com1_fine_down", "COM 1 standby fine down (25kHz).", "Radios Stby Com1fine Down", XPlaneCommands.RadiosStbyCom1FineDown); } }
        private XPlaneCommand RadiosStbyCom1FineUp { get { return new XPlaneCommand("sim/radios/stby_com1_fine_up", "COM 1 standby fine up (25kHz).", "Radios Stby Com1fine Up", XPlaneCommands.RadiosStbyCom1FineUp); } }
        private XPlaneCommand RadiosStbyCom1CoarseDown833 { get { return new XPlaneCommand("sim/radios/stby_com1_coarse_down_833", "COM 1 standby coarse down (8.33kHz).", "Radios Stby Com1coarse Down833", XPlaneCommands.RadiosStbyCom1CoarseDown833); } }
        private XPlaneCommand RadiosStbyCom1CoarseUp833 { get { return new XPlaneCommand("sim/radios/stby_com1_coarse_up_833", "COM 1 standby coarse up (8.33kHz).", "Radios Stby Com1coarse Up833", XPlaneCommands.RadiosStbyCom1CoarseUp833); } }
        private XPlaneCommand RadiosStbyCom1FineDown833 { get { return new XPlaneCommand("sim/radios/stby_com1_fine_down_833", "COM 1 standby fine down (8.33kHz).", "Radios Stby Com1fine Down833", XPlaneCommands.RadiosStbyCom1FineDown833); } }
        private XPlaneCommand RadiosStbyCom1FineUp833 { get { return new XPlaneCommand("sim/radios/stby_com1_fine_up_833", "COM 1 standby fine up (8.33kHz).", "Radios Stby Com1fine Up833", XPlaneCommands.RadiosStbyCom1FineUp833); } }
        private XPlaneCommand RadiosActvCom2CoarseDown { get { return new XPlaneCommand("sim/radios/actv_com2_coarse_down", "COM 2 coarse down.", "Radios Actv Com2coarse Down", XPlaneCommands.RadiosActvCom2CoarseDown); } }
        private XPlaneCommand RadiosActvCom2CoarseUp { get { return new XPlaneCommand("sim/radios/actv_com2_coarse_up", "COM 2 coarse up.", "Radios Actv Com2coarse Up", XPlaneCommands.RadiosActvCom2CoarseUp); } }
        private XPlaneCommand RadiosActvCom2FineDown { get { return new XPlaneCommand("sim/radios/actv_com2_fine_down", "COM 2 fine down (25kHz).", "Radios Actv Com2fine Down", XPlaneCommands.RadiosActvCom2FineDown); } }
        private XPlaneCommand RadiosActvCom2FineUp { get { return new XPlaneCommand("sim/radios/actv_com2_fine_up", "COM 2 fine up (25kHz).", "Radios Actv Com2fine Up", XPlaneCommands.RadiosActvCom2FineUp); } }
        private XPlaneCommand RadiosActvCom2CoarseDown833 { get { return new XPlaneCommand("sim/radios/actv_com2_coarse_down_833", "COM 2 coarse down (8.33kHz).", "Radios Actv Com2coarse Down833", XPlaneCommands.RadiosActvCom2CoarseDown833); } }
        private XPlaneCommand RadiosActvCom2CoarseUp833 { get { return new XPlaneCommand("sim/radios/actv_com2_coarse_up_833", "COM 2 coarse up (8.33kHz).", "Radios Actv Com2coarse Up833", XPlaneCommands.RadiosActvCom2CoarseUp833); } }
        private XPlaneCommand RadiosActvCom2FineDown833 { get { return new XPlaneCommand("sim/radios/actv_com2_fine_down_833", "COM 2 fine down (8.33kHz).", "Radios Actv Com2fine Down833", XPlaneCommands.RadiosActvCom2FineDown833); } }
        private XPlaneCommand RadiosActvCom2FineUp833 { get { return new XPlaneCommand("sim/radios/actv_com2_fine_up_833", "COM 2 fine up (8.33kHz).", "Radios Actv Com2fine Up833", XPlaneCommands.RadiosActvCom2FineUp833); } }
        private XPlaneCommand RadiosStbyCom2CoarseDown { get { return new XPlaneCommand("sim/radios/stby_com2_coarse_down", "COM 2 standby coarse down.", "Radios Stby Com2coarse Down", XPlaneCommands.RadiosStbyCom2CoarseDown); } }
        private XPlaneCommand RadiosStbyCom2CoarseUp { get { return new XPlaneCommand("sim/radios/stby_com2_coarse_up", "COM 2 standby coarse up.", "Radios Stby Com2coarse Up", XPlaneCommands.RadiosStbyCom2CoarseUp); } }
        private XPlaneCommand RadiosStbyCom2FineDown { get { return new XPlaneCommand("sim/radios/stby_com2_fine_down", "COM 2 standby fine down (25kHz).", "Radios Stby Com2fine Down", XPlaneCommands.RadiosStbyCom2FineDown); } }
        private XPlaneCommand RadiosStbyCom2FineUp { get { return new XPlaneCommand("sim/radios/stby_com2_fine_up", "COM 2 standby fine up (25kHz).", "Radios Stby Com2fine Up", XPlaneCommands.RadiosStbyCom2FineUp); } }
        private XPlaneCommand RadiosStbyCom2CoarseDown833 { get { return new XPlaneCommand("sim/radios/stby_com2_coarse_down_833", "COM 2 standby coarse down (8.33kHz).", "Radios Stby Com2coarse Down833", XPlaneCommands.RadiosStbyCom2CoarseDown833); } }
        private XPlaneCommand RadiosStbyCom2CoarseUp833 { get { return new XPlaneCommand("sim/radios/stby_com2_coarse_up_833", "COM 2 standby coarse up (8.33kHz).", "Radios Stby Com2coarse Up833", XPlaneCommands.RadiosStbyCom2CoarseUp833); } }
        private XPlaneCommand RadiosStbyCom2FineDown833 { get { return new XPlaneCommand("sim/radios/stby_com2_fine_down_833", "COM 2 standby fine down (8.33kHz).", "Radios Stby Com2fine Down833", XPlaneCommands.RadiosStbyCom2FineDown833); } }
        private XPlaneCommand RadiosStbyCom2FineUp833 { get { return new XPlaneCommand("sim/radios/stby_com2_fine_up_833", "COM 2 standby fine up (8.33kHz).", "Radios Stby Com2fine Up833", XPlaneCommands.RadiosStbyCom2FineUp833); } }
        private XPlaneCommand RadiosActvNav1CoarseDown { get { return new XPlaneCommand("sim/radios/actv_nav1_coarse_down", "NAV 1 coarse down.", "Radios Actv Nav1coarse Down", XPlaneCommands.RadiosActvNav1CoarseDown); } }
        private XPlaneCommand RadiosActvNav1CoarseUp { get { return new XPlaneCommand("sim/radios/actv_nav1_coarse_up", "NAV 1 coarse up.", "Radios Actv Nav1coarse Up", XPlaneCommands.RadiosActvNav1CoarseUp); } }
        private XPlaneCommand RadiosActvNav1FineDown { get { return new XPlaneCommand("sim/radios/actv_nav1_fine_down", "NAV 1 fine down.", "Radios Actv Nav1fine Down", XPlaneCommands.RadiosActvNav1FineDown); } }
        private XPlaneCommand RadiosActvNav1FineUp { get { return new XPlaneCommand("sim/radios/actv_nav1_fine_up", "NAV 1 fine up.", "Radios Actv Nav1fine Up", XPlaneCommands.RadiosActvNav1FineUp); } }
        private XPlaneCommand RadiosStbyNav1CoarseDown { get { return new XPlaneCommand("sim/radios/stby_nav1_coarse_down", "NAV 1 standby coarse down.", "Radios Stby Nav1coarse Down", XPlaneCommands.RadiosStbyNav1CoarseDown); } }
        private XPlaneCommand RadiosStbyNav1CoarseUp { get { return new XPlaneCommand("sim/radios/stby_nav1_coarse_up", "NAV 1 standby coarse up.", "Radios Stby Nav1coarse Up", XPlaneCommands.RadiosStbyNav1CoarseUp); } }
        private XPlaneCommand RadiosStbyNav1FineDown { get { return new XPlaneCommand("sim/radios/stby_nav1_fine_down", "NAV 1 standby fine down.", "Radios Stby Nav1fine Down", XPlaneCommands.RadiosStbyNav1FineDown); } }
        private XPlaneCommand RadiosStbyNav1FineUp { get { return new XPlaneCommand("sim/radios/stby_nav1_fine_up", "NAV 1 standby fine up.", "Radios Stby Nav1fine Up", XPlaneCommands.RadiosStbyNav1FineUp); } }
        private XPlaneCommand RadiosActvNav2CoarseDown { get { return new XPlaneCommand("sim/radios/actv_nav2_coarse_down", "NAV 2 coarse down.", "Radios Actv Nav2coarse Down", XPlaneCommands.RadiosActvNav2CoarseDown); } }
        private XPlaneCommand RadiosActvNav2CoarseUp { get { return new XPlaneCommand("sim/radios/actv_nav2_coarse_up", "NAV 2 coarse up.", "Radios Actv Nav2coarse Up", XPlaneCommands.RadiosActvNav2CoarseUp); } }
        private XPlaneCommand RadiosActvNav2FineDown { get { return new XPlaneCommand("sim/radios/actv_nav2_fine_down", "NAV 2 fine down.", "Radios Actv Nav2fine Down", XPlaneCommands.RadiosActvNav2FineDown); } }
        private XPlaneCommand RadiosActvNav2FineUp { get { return new XPlaneCommand("sim/radios/actv_nav2_fine_up", "NAV 2 fine up.", "Radios Actv Nav2fine Up", XPlaneCommands.RadiosActvNav2FineUp); } }
        private XPlaneCommand RadiosStbyNav2CoarseDown { get { return new XPlaneCommand("sim/radios/stby_nav2_coarse_down", "NAV 2 standby coarse down.", "Radios Stby Nav2coarse Down", XPlaneCommands.RadiosStbyNav2CoarseDown); } }
        private XPlaneCommand RadiosStbyNav2CoarseUp { get { return new XPlaneCommand("sim/radios/stby_nav2_coarse_up", "NAV 2 standby coarse up.", "Radios Stby Nav2coarse Up", XPlaneCommands.RadiosStbyNav2CoarseUp); } }
        private XPlaneCommand RadiosStbyNav2FineDown { get { return new XPlaneCommand("sim/radios/stby_nav2_fine_down", "NAV 2 standby fine down.", "Radios Stby Nav2fine Down", XPlaneCommands.RadiosStbyNav2FineDown); } }
        private XPlaneCommand RadiosStbyNav2FineUp { get { return new XPlaneCommand("sim/radios/stby_nav2_fine_up", "NAV 2 standby fine up.", "Radios Stby Nav2fine Up", XPlaneCommands.RadiosStbyNav2FineUp); } }
        private XPlaneCommand RadiosActvDmeCoarseDown { get { return new XPlaneCommand("sim/radios/actv_dme_coarse_down", "DME coarse down.", "Radios Actv Dme Coarse Down", XPlaneCommands.RadiosActvDmeCoarseDown); } }
        private XPlaneCommand RadiosActvDmeCoarseUp { get { return new XPlaneCommand("sim/radios/actv_dme_coarse_up", "DME coarse up.", "Radios Actv Dme Coarse Up", XPlaneCommands.RadiosActvDmeCoarseUp); } }
        private XPlaneCommand RadiosActvDmeFineDown { get { return new XPlaneCommand("sim/radios/actv_dme_fine_down", "DME fine down.", "Radios Actv Dme Fine Down", XPlaneCommands.RadiosActvDmeFineDown); } }
        private XPlaneCommand RadiosActvDmeFineUp { get { return new XPlaneCommand("sim/radios/actv_dme_fine_up", "DME fine up.", "Radios Actv Dme Fine Up", XPlaneCommands.RadiosActvDmeFineUp); } }
        private XPlaneCommand RadiosStbyDmeCoarseDown { get { return new XPlaneCommand("sim/radios/stby_dme_coarse_down", "DME standby coarse down.", "Radios Stby Dme Coarse Down", XPlaneCommands.RadiosStbyDmeCoarseDown); } }
        private XPlaneCommand RadiosStbyDmeCoarseUp { get { return new XPlaneCommand("sim/radios/stby_dme_coarse_up", "DME standby coarse up.", "Radios Stby Dme Coarse Up", XPlaneCommands.RadiosStbyDmeCoarseUp); } }
        private XPlaneCommand RadiosStbyDmeFineDown { get { return new XPlaneCommand("sim/radios/stby_dme_fine_down", "DME standby fine down.", "Radios Stby Dme Fine Down", XPlaneCommands.RadiosStbyDmeFineDown); } }
        private XPlaneCommand RadiosStbyDmeFineUp { get { return new XPlaneCommand("sim/radios/stby_dme_fine_up", "DME standby fine up.", "Radios Stby Dme Fine Up", XPlaneCommands.RadiosStbyDmeFineUp); } }
        private XPlaneCommand RadiosActvAdf1HundredsDown { get { return new XPlaneCommand("sim/radios/actv_adf1_hundreds_down", "ADF active 1 hundreds down.", "Radios Actv Adf1hundreds Down", XPlaneCommands.RadiosActvAdf1HundredsDown); } }
        private XPlaneCommand RadiosActvAdf1HundredsUp { get { return new XPlaneCommand("sim/radios/actv_adf1_hundreds_up", "ADF active 1 hundreds up.", "Radios Actv Adf1hundreds Up", XPlaneCommands.RadiosActvAdf1HundredsUp); } }
        private XPlaneCommand RadiosActvAdf1TensDown { get { return new XPlaneCommand("sim/radios/actv_adf1_tens_down", "ADF active 1 tens down.", "Radios Actv Adf1tens Down", XPlaneCommands.RadiosActvAdf1TensDown); } }
        private XPlaneCommand RadiosActvAdf1TensUp { get { return new XPlaneCommand("sim/radios/actv_adf1_tens_up", "ADF active 1 tens up.", "Radios Actv Adf1tens Up", XPlaneCommands.RadiosActvAdf1TensUp); } }
        private XPlaneCommand RadiosActvAdf1OnesDown { get { return new XPlaneCommand("sim/radios/actv_adf1_ones_down", "ADF active 1 ones down.", "Radios Actv Adf1ones Down", XPlaneCommands.RadiosActvAdf1OnesDown); } }
        private XPlaneCommand RadiosActvAdf1OnesUp { get { return new XPlaneCommand("sim/radios/actv_adf1_ones_up", "ADF active 1 ones up.", "Radios Actv Adf1ones Up", XPlaneCommands.RadiosActvAdf1OnesUp); } }
        private XPlaneCommand RadiosActvAdf1OnesTensDown { get { return new XPlaneCommand("sim/radios/actv_adf1_ones_tens_down", "ADF active 1 ones and tens down.", "Radios Actv Adf1ones Tens Down", XPlaneCommands.RadiosActvAdf1OnesTensDown); } }
        private XPlaneCommand RadiosActvAdf1OnesTensUp { get { return new XPlaneCommand("sim/radios/actv_adf1_ones_tens_up", "ADF active 1 ones and tens up.", "Radios Actv Adf1ones Tens Up", XPlaneCommands.RadiosActvAdf1OnesTensUp); } }
        private XPlaneCommand RadiosActvAdf1HundredsThousDown { get { return new XPlaneCommand("sim/radios/actv_adf1_hundreds_thous_down", "ADF active 1 hundreds and thousands down.", "Radios Actv Adf1hundreds Thous Down", XPlaneCommands.RadiosActvAdf1HundredsThousDown); } }
        private XPlaneCommand RadiosActvAdf1HundredsThousUp { get { return new XPlaneCommand("sim/radios/actv_adf1_hundreds_thous_up", "ADF active 1 hundreds and thousands up.", "Radios Actv Adf1hundreds Thous Up", XPlaneCommands.RadiosActvAdf1HundredsThousUp); } }
        private XPlaneCommand RadiosActvAdf14DigHundredsDown { get { return new XPlaneCommand("sim/radios/actv_adf1_4dig_hundreds_down", "4-digit ADF active 1 hundreds down.", "Radios Actv Adf14dig Hundreds Down", XPlaneCommands.RadiosActvAdf14DigHundredsDown); } }
        private XPlaneCommand RadiosActvAdf14DigHundredsUp { get { return new XPlaneCommand("sim/radios/actv_adf1_4dig_hundreds_up", "4-digit ADF active 1 hundreds up.", "Radios Actv Adf14dig Hundreds Up", XPlaneCommands.RadiosActvAdf14DigHundredsUp); } }
        private XPlaneCommand RadiosActvAdf14DigTensDown { get { return new XPlaneCommand("sim/radios/actv_adf1_4dig_tens_down", "4-digit ADF active 1 tens down.", "Radios Actv Adf14dig Tens Down", XPlaneCommands.RadiosActvAdf14DigTensDown); } }
        private XPlaneCommand RadiosActvAdf14DigTensUp { get { return new XPlaneCommand("sim/radios/actv_adf1_4dig_tens_up", "4-digit ADF active 1 tens up.", "Radios Actv Adf14dig Tens Up", XPlaneCommands.RadiosActvAdf14DigTensUp); } }
        private XPlaneCommand RadiosActvAdf14DigOnesDown { get { return new XPlaneCommand("sim/radios/actv_adf1_4dig_ones_down", "4-digit ADF active 1 ones down.", "Radios Actv Adf14dig Ones Down", XPlaneCommands.RadiosActvAdf14DigOnesDown); } }
        private XPlaneCommand RadiosActvAdf14DigOnesUp { get { return new XPlaneCommand("sim/radios/actv_adf1_4dig_ones_up", "4-digit ADF active 1 ones up.", "Radios Actv Adf14dig Ones Up", XPlaneCommands.RadiosActvAdf14DigOnesUp); } }
        private XPlaneCommand RadiosStbyAdf1HundredsDown { get { return new XPlaneCommand("sim/radios/stby_adf1_hundreds_down", "ADF standby 1 hundreds down.", "Radios Stby Adf1hundreds Down", XPlaneCommands.RadiosStbyAdf1HundredsDown); } }
        private XPlaneCommand RadiosStbyAdf1HundredsUp { get { return new XPlaneCommand("sim/radios/stby_adf1_hundreds_up", "ADF standby 1 hundreds up.", "Radios Stby Adf1hundreds Up", XPlaneCommands.RadiosStbyAdf1HundredsUp); } }
        private XPlaneCommand RadiosStbyAdf1TensDown { get { return new XPlaneCommand("sim/radios/stby_adf1_tens_down", "ADF standby 1 tens down.", "Radios Stby Adf1tens Down", XPlaneCommands.RadiosStbyAdf1TensDown); } }
        private XPlaneCommand RadiosStbyAdf1TensUp { get { return new XPlaneCommand("sim/radios/stby_adf1_tens_up", "ADF standby 1 tens up.", "Radios Stby Adf1tens Up", XPlaneCommands.RadiosStbyAdf1TensUp); } }
        private XPlaneCommand RadiosStbyAdf1OnesDown { get { return new XPlaneCommand("sim/radios/stby_adf1_ones_down", "ADF standby 1 ones down.", "Radios Stby Adf1ones Down", XPlaneCommands.RadiosStbyAdf1OnesDown); } }
        private XPlaneCommand RadiosStbyAdf1OnesUp { get { return new XPlaneCommand("sim/radios/stby_adf1_ones_up", "ADF standby 1 ones up.", "Radios Stby Adf1ones Up", XPlaneCommands.RadiosStbyAdf1OnesUp); } }
        private XPlaneCommand RadiosStbyAdf1OnesTensDown { get { return new XPlaneCommand("sim/radios/stby_adf1_ones_tens_down", "ADF standby 1 ones and tens down.", "Radios Stby Adf1ones Tens Down", XPlaneCommands.RadiosStbyAdf1OnesTensDown); } }
        private XPlaneCommand RadiosStbyAdf1OnesTensUp { get { return new XPlaneCommand("sim/radios/stby_adf1_ones_tens_up", "ADF standby 1 ones and tens up.", "Radios Stby Adf1ones Tens Up", XPlaneCommands.RadiosStbyAdf1OnesTensUp); } }
        private XPlaneCommand RadiosStbyAdf1HundredsThousDown { get { return new XPlaneCommand("sim/radios/stby_adf1_hundreds_thous_down", "ADF standby 1 hundreds and thousands down.", "Radios Stby Adf1hundreds Thous Down", XPlaneCommands.RadiosStbyAdf1HundredsThousDown); } }
        private XPlaneCommand RadiosStbyAdf1HundredsThousUp { get { return new XPlaneCommand("sim/radios/stby_adf1_hundreds_thous_up", "ADF standby 1 hundreds and thousands up.", "Radios Stby Adf1hundreds Thous Up", XPlaneCommands.RadiosStbyAdf1HundredsThousUp); } }
        private XPlaneCommand RadiosStbyAdf14DigHundredsDown { get { return new XPlaneCommand("sim/radios/stby_adf1_4dig_hundreds_down", "4-digit ADF standby 1 hundreds down.", "Radios Stby Adf14dig Hundreds Down", XPlaneCommands.RadiosStbyAdf14DigHundredsDown); } }
        private XPlaneCommand RadiosStbyAdf14DigHundredsUp { get { return new XPlaneCommand("sim/radios/stby_adf1_4dig_hundreds_up", "4-digit ADF standby 1 hundreds up.", "Radios Stby Adf14dig Hundreds Up", XPlaneCommands.RadiosStbyAdf14DigHundredsUp); } }
        private XPlaneCommand RadiosStbyAdf14DigTensDown { get { return new XPlaneCommand("sim/radios/stby_adf1_4dig_tens_down", "4-digit ADF standby 1 tens down.", "Radios Stby Adf14dig Tens Down", XPlaneCommands.RadiosStbyAdf14DigTensDown); } }
        private XPlaneCommand RadiosStbyAdf14DigTensUp { get { return new XPlaneCommand("sim/radios/stby_adf1_4dig_tens_up", "4-digit ADF standby 1 tens up.", "Radios Stby Adf14dig Tens Up", XPlaneCommands.RadiosStbyAdf14DigTensUp); } }
        private XPlaneCommand RadiosStbyAdf14DigOnesDown { get { return new XPlaneCommand("sim/radios/stby_adf1_4dig_ones_down", "4-digit ADF standby 1 ones down.", "Radios Stby Adf14dig Ones Down", XPlaneCommands.RadiosStbyAdf14DigOnesDown); } }
        private XPlaneCommand RadiosStbyAdf14DigOnesUp { get { return new XPlaneCommand("sim/radios/stby_adf1_4dig_ones_up", "4-digit ADF standby 1 ones up.", "Radios Stby Adf14dig Ones Up", XPlaneCommands.RadiosStbyAdf14DigOnesUp); } }
        private XPlaneCommand RadiosActvAdf2HundredsDown { get { return new XPlaneCommand("sim/radios/actv_adf2_hundreds_down", "ADF active 2 hundreds down.", "Radios Actv Adf2hundreds Down", XPlaneCommands.RadiosActvAdf2HundredsDown); } }
        private XPlaneCommand RadiosActvAdf2HundredsUp { get { return new XPlaneCommand("sim/radios/actv_adf2_hundreds_up", "ADF active 2 hundreds up.", "Radios Actv Adf2hundreds Up", XPlaneCommands.RadiosActvAdf2HundredsUp); } }
        private XPlaneCommand RadiosActvAdf2TensDown { get { return new XPlaneCommand("sim/radios/actv_adf2_tens_down", "ADF active 2 tens down.", "Radios Actv Adf2tens Down", XPlaneCommands.RadiosActvAdf2TensDown); } }
        private XPlaneCommand RadiosActvAdf2TensUp { get { return new XPlaneCommand("sim/radios/actv_adf2_tens_up", "ADF active 2 tens up.", "Radios Actv Adf2tens Up", XPlaneCommands.RadiosActvAdf2TensUp); } }
        private XPlaneCommand RadiosActvAdf2OnesDown { get { return new XPlaneCommand("sim/radios/actv_adf2_ones_down", "ADF active 2 ones down.", "Radios Actv Adf2ones Down", XPlaneCommands.RadiosActvAdf2OnesDown); } }
        private XPlaneCommand RadiosActvAdf2OnesUp { get { return new XPlaneCommand("sim/radios/actv_adf2_ones_up", "ADF active 2 ones up.", "Radios Actv Adf2ones Up", XPlaneCommands.RadiosActvAdf2OnesUp); } }
        private XPlaneCommand RadiosActvAdf2OnesTensDown { get { return new XPlaneCommand("sim/radios/actv_adf2_ones_tens_down", "ADF active 2 ones and tens down.", "Radios Actv Adf2ones Tens Down", XPlaneCommands.RadiosActvAdf2OnesTensDown); } }
        private XPlaneCommand RadiosActvAdf2OnesTensUp { get { return new XPlaneCommand("sim/radios/actv_adf2_ones_tens_up", "ADF active 2 ones and tens up.", "Radios Actv Adf2ones Tens Up", XPlaneCommands.RadiosActvAdf2OnesTensUp); } }
        private XPlaneCommand RadiosActvAdf2HundredsThousDown { get { return new XPlaneCommand("sim/radios/actv_adf2_hundreds_thous_down", "ADF active 2 hundreds and thousands down.", "Radios Actv Adf2hundreds Thous Down", XPlaneCommands.RadiosActvAdf2HundredsThousDown); } }
        private XPlaneCommand RadiosActvAdf2HundredsThousUp { get { return new XPlaneCommand("sim/radios/actv_adf2_hundreds_thous_up", "ADF active 2 hundreds and thousands up.", "Radios Actv Adf2hundreds Thous Up", XPlaneCommands.RadiosActvAdf2HundredsThousUp); } }
        private XPlaneCommand RadiosActvAdf24DigHundredsDown { get { return new XPlaneCommand("sim/radios/actv_adf2_4dig_hundreds_down", "4-digit ADF active 2 hundreds down.", "Radios Actv Adf24dig Hundreds Down", XPlaneCommands.RadiosActvAdf24DigHundredsDown); } }
        private XPlaneCommand RadiosActvAdf24DigHundredsUp { get { return new XPlaneCommand("sim/radios/actv_adf2_4dig_hundreds_up", "4-digit ADF active 2 hundreds up.", "Radios Actv Adf24dig Hundreds Up", XPlaneCommands.RadiosActvAdf24DigHundredsUp); } }
        private XPlaneCommand RadiosActvAdf24DigTensDown { get { return new XPlaneCommand("sim/radios/actv_adf2_4dig_tens_down", "4-digit ADF active 2 tens down.", "Radios Actv Adf24dig Tens Down", XPlaneCommands.RadiosActvAdf24DigTensDown); } }
        private XPlaneCommand RadiosActvAdf24DigTensUp { get { return new XPlaneCommand("sim/radios/actv_adf2_4dig_tens_up", "4-digit ADF active 2 tens up.", "Radios Actv Adf24dig Tens Up", XPlaneCommands.RadiosActvAdf24DigTensUp); } }
        private XPlaneCommand RadiosActvAdf24DigOnesDown { get { return new XPlaneCommand("sim/radios/actv_adf2_4dig_ones_down", "4-digit ADF active 2 ones down.", "Radios Actv Adf24dig Ones Down", XPlaneCommands.RadiosActvAdf24DigOnesDown); } }
        private XPlaneCommand RadiosActvAdf24DigOnesUp { get { return new XPlaneCommand("sim/radios/actv_adf2_4dig_ones_up", "4-digit ADF active 2 ones up.", "Radios Actv Adf24dig Ones Up", XPlaneCommands.RadiosActvAdf24DigOnesUp); } }
        private XPlaneCommand RadiosStbyAdf2HundredsDown { get { return new XPlaneCommand("sim/radios/stby_adf2_hundreds_down", "ADF standby 2 hundreds down.", "Radios Stby Adf2hundreds Down", XPlaneCommands.RadiosStbyAdf2HundredsDown); } }
        private XPlaneCommand RadiosStbyAdf2HundredsUp { get { return new XPlaneCommand("sim/radios/stby_adf2_hundreds_up", "ADF standby 2 hundreds up.", "Radios Stby Adf2hundreds Up", XPlaneCommands.RadiosStbyAdf2HundredsUp); } }
        private XPlaneCommand RadiosStbyAdf2TensDown { get { return new XPlaneCommand("sim/radios/stby_adf2_tens_down", "ADF standby 2 tens down.", "Radios Stby Adf2tens Down", XPlaneCommands.RadiosStbyAdf2TensDown); } }
        private XPlaneCommand RadiosStbyAdf2TensUp { get { return new XPlaneCommand("sim/radios/stby_adf2_tens_up", "ADF standby 2 tens up.", "Radios Stby Adf2tens Up", XPlaneCommands.RadiosStbyAdf2TensUp); } }
        private XPlaneCommand RadiosStbyAdf2OnesDown { get { return new XPlaneCommand("sim/radios/stby_adf2_ones_down", "ADF standby 2 ones down.", "Radios Stby Adf2ones Down", XPlaneCommands.RadiosStbyAdf2OnesDown); } }
        private XPlaneCommand RadiosStbyAdf2OnesUp { get { return new XPlaneCommand("sim/radios/stby_adf2_ones_up", "ADF standby 2 ones up.", "Radios Stby Adf2ones Up", XPlaneCommands.RadiosStbyAdf2OnesUp); } }
        private XPlaneCommand RadiosStbyAdf2OnesTensDown { get { return new XPlaneCommand("sim/radios/stby_adf2_ones_tens_down", "ADF standby 2 ones and tens down.", "Radios Stby Adf2ones Tens Down", XPlaneCommands.RadiosStbyAdf2OnesTensDown); } }
        private XPlaneCommand RadiosStbyAdf2OnesTensUp { get { return new XPlaneCommand("sim/radios/stby_adf2_ones_tens_up", "ADF standby 2 ones and tens up.", "Radios Stby Adf2ones Tens Up", XPlaneCommands.RadiosStbyAdf2OnesTensUp); } }
        private XPlaneCommand RadiosStbyAdf2HundredsThousDown { get { return new XPlaneCommand("sim/radios/stby_adf2_hundreds_thous_down", "ADF standby 2 hundreds and thousands down.", "Radios Stby Adf2hundreds Thous Down", XPlaneCommands.RadiosStbyAdf2HundredsThousDown); } }
        private XPlaneCommand RadiosStbyAdf2HundredsThousUp { get { return new XPlaneCommand("sim/radios/stby_adf2_hundreds_thous_up", "ADF standby 2 hundreds and thousands up.", "Radios Stby Adf2hundreds Thous Up", XPlaneCommands.RadiosStbyAdf2HundredsThousUp); } }
        private XPlaneCommand RadiosStbyAdf24DigHundredsDown { get { return new XPlaneCommand("sim/radios/stby_adf2_4dig_hundreds_down", "4-digit ADF standby 2 hundreds down.", "Radios Stby Adf24dig Hundreds Down", XPlaneCommands.RadiosStbyAdf24DigHundredsDown); } }
        private XPlaneCommand RadiosStbyAdf24DigHundredsUp { get { return new XPlaneCommand("sim/radios/stby_adf2_4dig_hundreds_up", "4-digit ADF standby 2 hundreds up.", "Radios Stby Adf24dig Hundreds Up", XPlaneCommands.RadiosStbyAdf24DigHundredsUp); } }
        private XPlaneCommand RadiosStbyAdf24DigTensDown { get { return new XPlaneCommand("sim/radios/stby_adf2_4dig_tens_down", "4-digit ADF standby 2 tens down.", "Radios Stby Adf24dig Tens Down", XPlaneCommands.RadiosStbyAdf24DigTensDown); } }
        private XPlaneCommand RadiosStbyAdf24DigTensUp { get { return new XPlaneCommand("sim/radios/stby_adf2_4dig_tens_up", "4-digit ADF standby 2 tens up.", "Radios Stby Adf24dig Tens Up", XPlaneCommands.RadiosStbyAdf24DigTensUp); } }
        private XPlaneCommand RadiosStbyAdf24DigOnesDown { get { return new XPlaneCommand("sim/radios/stby_adf2_4dig_ones_down", "4-digit ADF standby 2 ones down.", "Radios Stby Adf24dig Ones Down", XPlaneCommands.RadiosStbyAdf24DigOnesDown); } }
        private XPlaneCommand RadiosStbyAdf24DigOnesUp { get { return new XPlaneCommand("sim/radios/stby_adf2_4dig_ones_up", "4-digit ADF standby 2 ones up.", "Radios Stby Adf24dig Ones Up", XPlaneCommands.RadiosStbyAdf24DigOnesUp); } }
        private XPlaneCommand TransponderTransponderDigit0 { get { return new XPlaneCommand("sim/transponder/transponder_digit_0", "Transponder digit to 0.", "Transponder Transponder Digit0", XPlaneCommands.TransponderTransponderDigit0); } }
        private XPlaneCommand TransponderTransponderDigit1 { get { return new XPlaneCommand("sim/transponder/transponder_digit_1", "Transponder digit to 1.", "Transponder Transponder Digit1", XPlaneCommands.TransponderTransponderDigit1); } }
        private XPlaneCommand TransponderTransponderDigit2 { get { return new XPlaneCommand("sim/transponder/transponder_digit_2", "Transponder digit to 2.", "Transponder Transponder Digit2", XPlaneCommands.TransponderTransponderDigit2); } }
        private XPlaneCommand TransponderTransponderDigit3 { get { return new XPlaneCommand("sim/transponder/transponder_digit_3", "Transponder digit to 3.", "Transponder Transponder Digit3", XPlaneCommands.TransponderTransponderDigit3); } }
        private XPlaneCommand TransponderTransponderDigit4 { get { return new XPlaneCommand("sim/transponder/transponder_digit_4", "Transponder digit to 4.", "Transponder Transponder Digit4", XPlaneCommands.TransponderTransponderDigit4); } }
        private XPlaneCommand TransponderTransponderDigit5 { get { return new XPlaneCommand("sim/transponder/transponder_digit_5", "Transponder digit to 5.", "Transponder Transponder Digit5", XPlaneCommands.TransponderTransponderDigit5); } }
        private XPlaneCommand TransponderTransponderDigit6 { get { return new XPlaneCommand("sim/transponder/transponder_digit_6", "Transponder digit to 6.", "Transponder Transponder Digit6", XPlaneCommands.TransponderTransponderDigit6); } }
        private XPlaneCommand TransponderTransponderDigit7 { get { return new XPlaneCommand("sim/transponder/transponder_digit_7", "Transponder digit to 7.", "Transponder Transponder Digit7", XPlaneCommands.TransponderTransponderDigit7); } }
        private XPlaneCommand TransponderTransponderCLR { get { return new XPlaneCommand("sim/transponder/transponder_CLR", "Transponder reset to first digit.", "Transponder Transponder CLR", XPlaneCommands.TransponderTransponderCLR); } }
        private XPlaneCommand TransponderTransponderThousandsDown { get { return new XPlaneCommand("sim/transponder/transponder_thousands_down", "Transponder thousands down.", "Transponder Transponder Thousands Down", XPlaneCommands.TransponderTransponderThousandsDown); } }
        private XPlaneCommand TransponderTransponderThousandsUp { get { return new XPlaneCommand("sim/transponder/transponder_thousands_up", "Transponder thousands up.", "Transponder Transponder Thousands Up", XPlaneCommands.TransponderTransponderThousandsUp); } }
        private XPlaneCommand TransponderTransponderHundredsDown { get { return new XPlaneCommand("sim/transponder/transponder_hundreds_down", "Transponder hundreds down.", "Transponder Transponder Hundreds Down", XPlaneCommands.TransponderTransponderHundredsDown); } }
        private XPlaneCommand TransponderTransponderHundredsUp { get { return new XPlaneCommand("sim/transponder/transponder_hundreds_up", "Transponder hundreds up.", "Transponder Transponder Hundreds Up", XPlaneCommands.TransponderTransponderHundredsUp); } }
        private XPlaneCommand TransponderTransponderTensDown { get { return new XPlaneCommand("sim/transponder/transponder_tens_down", "Transponder tens down.", "Transponder Transponder Tens Down", XPlaneCommands.TransponderTransponderTensDown); } }
        private XPlaneCommand TransponderTransponderTensUp { get { return new XPlaneCommand("sim/transponder/transponder_tens_up", "Transponder tens up.", "Transponder Transponder Tens Up", XPlaneCommands.TransponderTransponderTensUp); } }
        private XPlaneCommand TransponderTransponderOnesDown { get { return new XPlaneCommand("sim/transponder/transponder_ones_down", "Transponder ones down.", "Transponder Transponder Ones Down", XPlaneCommands.TransponderTransponderOnesDown); } }
        private XPlaneCommand TransponderTransponderOnesUp { get { return new XPlaneCommand("sim/transponder/transponder_ones_up", "Transponder ones up.", "Transponder Transponder Ones Up", XPlaneCommands.TransponderTransponderOnesUp); } }
        private XPlaneCommand TransponderTransponder12Down { get { return new XPlaneCommand("sim/transponder/transponder_12_down", "Transponder digits 1 and 2 down.", "Transponder Transponder12down", XPlaneCommands.TransponderTransponder12Down); } }
        private XPlaneCommand TransponderTransponder12Up { get { return new XPlaneCommand("sim/transponder/transponder_12_up", "Transponder digits 1 and 2 up.", "Transponder Transponder12up", XPlaneCommands.TransponderTransponder12Up); } }
        private XPlaneCommand TransponderTransponder34Down { get { return new XPlaneCommand("sim/transponder/transponder_34_down", "Transponder digits 3 and 4 down.", "Transponder Transponder34down", XPlaneCommands.TransponderTransponder34Down); } }
        private XPlaneCommand TransponderTransponder34Up { get { return new XPlaneCommand("sim/transponder/transponder_34_up", "Transponder digits 3 and 4 up.", "Transponder Transponder34up", XPlaneCommands.TransponderTransponder34Up); } }
        private XPlaneCommand AudioPanelTransmitAudioCom1 { get { return new XPlaneCommand("sim/audio_panel/transmit_audio_com1", "Transmit audio: COM1.", "Audio Panel Transmit Audio Com1", XPlaneCommands.AudioPanelTransmitAudioCom1); } }
        private XPlaneCommand AudioPanelTransmitAudioCom2 { get { return new XPlaneCommand("sim/audio_panel/transmit_audio_com2", "Transmit audio: COM2.", "Audio Panel Transmit Audio Com2", XPlaneCommands.AudioPanelTransmitAudioCom2); } }
        private XPlaneCommand AudioPanelMonitorAudioComAuto { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com_auto", "Monitor audio: COM auto (same as transmit) toggle.", "Audio Panel Monitor Audio Com Auto", XPlaneCommands.AudioPanelMonitorAudioComAuto); } }
        private XPlaneCommand AudioPanelMonitorAudioCom1 { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com1", "Monitor audio: COM1 toggle.", "Audio Panel Monitor Audio Com1", XPlaneCommands.AudioPanelMonitorAudioCom1); } }
        private XPlaneCommand AudioPanelMonitorAudioCom2 { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com2", "Monitor audio: COM2 toggle.", "Audio Panel Monitor Audio Com2", XPlaneCommands.AudioPanelMonitorAudioCom2); } }
        private XPlaneCommand AudioPanelMonitorAudioNav1 { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_nav1", "Monitor audio: NAV1 toggle.", "Audio Panel Monitor Audio Nav1", XPlaneCommands.AudioPanelMonitorAudioNav1); } }
        private XPlaneCommand AudioPanelMonitorAudioNav2 { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_nav2", "Monitor audio: NAV2 toggle.", "Audio Panel Monitor Audio Nav2", XPlaneCommands.AudioPanelMonitorAudioNav2); } }
        private XPlaneCommand AudioPanelMonitorAudioAdf1 { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_adf1", "Monitor audio: ADF1 toggle.", "Audio Panel Monitor Audio Adf1", XPlaneCommands.AudioPanelMonitorAudioAdf1); } }
        private XPlaneCommand AudioPanelMonitorAudioAdf2 { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_adf2", "Monitor audio: ADF2 toggle.", "Audio Panel Monitor Audio Adf2", XPlaneCommands.AudioPanelMonitorAudioAdf2); } }
        private XPlaneCommand AudioPanelMonitorAudioDme { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_dme", "Monitor audio: DME toggle.", "Audio Panel Monitor Audio Dme", XPlaneCommands.AudioPanelMonitorAudioDme); } }
        private XPlaneCommand AudioPanelMonitorAudioMkr { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_mkr", "Monitor audio: MKR toggle.", "Audio Panel Monitor Audio Mkr", XPlaneCommands.AudioPanelMonitorAudioMkr); } }
        private XPlaneCommand AudioPanelTransmitAudioCom1Man { get { return new XPlaneCommand("sim/audio_panel/transmit_audio_com1_man", "Transmit audio: COM1 - old panel, doesn't change listener.", "Audio Panel Transmit Audio Com1man", XPlaneCommands.AudioPanelTransmitAudioCom1Man); } }
        private XPlaneCommand AudioPanelTransmitAudioCom2Man { get { return new XPlaneCommand("sim/audio_panel/transmit_audio_com2_man", "Transmit audio: COM2 - old panel, doesn't change listener.", "Audio Panel Transmit Audio Com2man", XPlaneCommands.AudioPanelTransmitAudioCom2Man); } }
        private XPlaneCommand AudioPanelMonitorAudioComAutoOff { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com_auto_off", "Monitor audio: COM auto (same as transmit) off.", "Audio Panel Monitor Audio Com Auto Off", XPlaneCommands.AudioPanelMonitorAudioComAutoOff); } }
        private XPlaneCommand AudioPanelMonitorAudioCom1Off { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com1_off", "Monitor audio: COM1 off.", "Audio Panel Monitor Audio Com1off", XPlaneCommands.AudioPanelMonitorAudioCom1Off); } }
        private XPlaneCommand AudioPanelMonitorAudioCom2Off { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com2_off", "Monitor audio: COM2 off.", "Audio Panel Monitor Audio Com2off", XPlaneCommands.AudioPanelMonitorAudioCom2Off); } }
        private XPlaneCommand AudioPanelMonitorAudioNav1Off { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_nav1_off", "Monitor audio: NAV1 off.", "Audio Panel Monitor Audio Nav1off", XPlaneCommands.AudioPanelMonitorAudioNav1Off); } }
        private XPlaneCommand AudioPanelMonitorAudioNav2Off { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_nav2_off", "Monitor audio: NAV2 off.", "Audio Panel Monitor Audio Nav2off", XPlaneCommands.AudioPanelMonitorAudioNav2Off); } }
        private XPlaneCommand AudioPanelMonitorAudioAdf1Off { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_adf1_off", "Monitor audio: ADF1 off.", "Audio Panel Monitor Audio Adf1off", XPlaneCommands.AudioPanelMonitorAudioAdf1Off); } }
        private XPlaneCommand AudioPanelMonitorAudioAdf2Off { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_adf2_off", "Monitor audio: ADF2 off.", "Audio Panel Monitor Audio Adf2off", XPlaneCommands.AudioPanelMonitorAudioAdf2Off); } }
        private XPlaneCommand AudioPanelMonitorAudioDmeOff { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_dme_off", "Monitor audio: DME off.", "Audio Panel Monitor Audio Dme Off", XPlaneCommands.AudioPanelMonitorAudioDmeOff); } }
        private XPlaneCommand AudioPanelMonitorAudioMkrOff { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_mkr_off", "Monitor audio: MKR off.", "Audio Panel Monitor Audio Mkr Off", XPlaneCommands.AudioPanelMonitorAudioMkrOff); } }
        private XPlaneCommand AudioPanelMonitorAudioComAutoOn { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com_auto_on", "Monitor audio: COM auto (same as transmit) on.", "Audio Panel Monitor Audio Com Auto On", XPlaneCommands.AudioPanelMonitorAudioComAutoOn); } }
        private XPlaneCommand AudioPanelMonitorAudioCom1On { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com1_on", "Monitor audio: COM1 on.", "Audio Panel Monitor Audio Com1on", XPlaneCommands.AudioPanelMonitorAudioCom1On); } }
        private XPlaneCommand AudioPanelMonitorAudioCom2On { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_com2_on", "Monitor audio: COM2 on.", "Audio Panel Monitor Audio Com2on", XPlaneCommands.AudioPanelMonitorAudioCom2On); } }
        private XPlaneCommand AudioPanelMonitorAudioNav1On { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_nav1_on", "Monitor audio: NAV1 on.", "Audio Panel Monitor Audio Nav1on", XPlaneCommands.AudioPanelMonitorAudioNav1On); } }
        private XPlaneCommand AudioPanelMonitorAudioNav2On { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_nav2_on", "Monitor audio: NAV2 on.", "Audio Panel Monitor Audio Nav2on", XPlaneCommands.AudioPanelMonitorAudioNav2On); } }
        private XPlaneCommand AudioPanelMonitorAudioAdf1On { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_adf1_on", "Monitor audio: ADF1 on.", "Audio Panel Monitor Audio Adf1on", XPlaneCommands.AudioPanelMonitorAudioAdf1On); } }
        private XPlaneCommand AudioPanelMonitorAudioAdf2On { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_adf2_on", "Monitor audio: ADF2 on.", "Audio Panel Monitor Audio Adf2on", XPlaneCommands.AudioPanelMonitorAudioAdf2On); } }
        private XPlaneCommand AudioPanelMonitorAudioDmeOn { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_dme_on", "Monitor audio: DME on.", "Audio Panel Monitor Audio Dme On", XPlaneCommands.AudioPanelMonitorAudioDmeOn); } }
        private XPlaneCommand AudioPanelMonitorAudioMkrOn { get { return new XPlaneCommand("sim/audio_panel/monitor_audio_mkr_on", "Monitor audio: MKR on.", "Audio Panel Monitor Audio Mkr On", XPlaneCommands.AudioPanelMonitorAudioMkrOn); } }
        private XPlaneCommand TransponderTransponderIdent { get { return new XPlaneCommand("sim/transponder/transponder_ident", "Transponder ID.", "Transponder Transponder Ident", XPlaneCommands.TransponderTransponderIdent); } }
        private XPlaneCommand TransponderTransponderOff { get { return new XPlaneCommand("sim/transponder/transponder_off", "Transponder off.", "Transponder Transponder Off", XPlaneCommands.TransponderTransponderOff); } }
        private XPlaneCommand TransponderTransponderStandby { get { return new XPlaneCommand("sim/transponder/transponder_standby", "Transponder standby.", "Transponder Transponder Standby", XPlaneCommands.TransponderTransponderStandby); } }
        private XPlaneCommand TransponderTransponderOn { get { return new XPlaneCommand("sim/transponder/transponder_on", "Transponder on.", "Transponder Transponder On", XPlaneCommands.TransponderTransponderOn); } }
        private XPlaneCommand TransponderTransponderAlt { get { return new XPlaneCommand("sim/transponder/transponder_alt", "Transponder alt.", "Transponder Transponder Alt", XPlaneCommands.TransponderTransponderAlt); } }
        private XPlaneCommand TransponderTransponderTest { get { return new XPlaneCommand("sim/transponder/transponder_test", "Transponder test.", "Transponder Transponder Test", XPlaneCommands.TransponderTransponderTest); } }
        private XPlaneCommand TransponderTransponderGround { get { return new XPlaneCommand("sim/transponder/transponder_ground", "Transponder ground.", "Transponder Transponder Ground", XPlaneCommands.TransponderTransponderGround); } }
        private XPlaneCommand TransponderTransponderDn { get { return new XPlaneCommand("sim/transponder/transponder_dn", "Transponder mode down.", "Transponder Transponder Dn", XPlaneCommands.TransponderTransponderDn); } }
        private XPlaneCommand TransponderTransponderUp { get { return new XPlaneCommand("sim/transponder/transponder_up", "Transponder mode up.", "Transponder Transponder Up", XPlaneCommands.TransponderTransponderUp); } }
        private XPlaneCommand RadiosNav1StandyFlip { get { return new XPlaneCommand("sim/radios/nav1_standy_flip", "NAV 1 flip standby.", "Radios Nav1standy Flip", XPlaneCommands.RadiosNav1StandyFlip); } }
        private XPlaneCommand RadiosNav2StandyFlip { get { return new XPlaneCommand("sim/radios/nav2_standy_flip", "NAV 2 flip standby.", "Radios Nav2standy Flip", XPlaneCommands.RadiosNav2StandyFlip); } }
        private XPlaneCommand RadiosCom1StandyFlip { get { return new XPlaneCommand("sim/radios/com1_standy_flip", "COM 1 flip standby.", "Radios Com1standy Flip", XPlaneCommands.RadiosCom1StandyFlip); } }
        private XPlaneCommand RadiosCom2StandyFlip { get { return new XPlaneCommand("sim/radios/com2_standy_flip", "COM 2 flip standby.", "Radios Com2standy Flip", XPlaneCommands.RadiosCom2StandyFlip); } }
        private XPlaneCommand RadiosAdf1StandyFlip { get { return new XPlaneCommand("sim/radios/adf1_standy_flip", "ADF 1 flip standby.", "Radios Adf1standy Flip", XPlaneCommands.RadiosAdf1StandyFlip); } }
        private XPlaneCommand RadiosAdf2StandyFlip { get { return new XPlaneCommand("sim/radios/adf2_standy_flip", "ADF 2 flip standby.", "Radios Adf2standy Flip", XPlaneCommands.RadiosAdf2StandyFlip); } }
        private XPlaneCommand RadiosDmeStandbyFlip { get { return new XPlaneCommand("sim/radios/dme_standby_flip", "DME flip standby.", "Radios Dme Standby Flip", XPlaneCommands.RadiosDmeStandbyFlip); } }
        private XPlaneCommand RadiosRMILTog { get { return new XPlaneCommand("sim/radios/RMI_L_tog", "RMI left NAV toggle VOR/NDB.", "Radios RMIL Tog", XPlaneCommands.RadiosRMILTog); } }
        private XPlaneCommand RadiosRMIRTog { get { return new XPlaneCommand("sim/radios/RMI_R_tog", "RMI right NAV toggle VOR/NDB.", "Radios RMIR Tog", XPlaneCommands.RadiosRMIRTog); } }
        private XPlaneCommand RadiosCopilotRMILTogCop { get { return new XPlaneCommand("sim/radios/copilot_RMI_L_tog_cop", "Copilot RMI left NAV toggle VOR/NDB.", "Radios Copilot RMIL Tog Cop", XPlaneCommands.RadiosCopilotRMILTogCop); } }
        private XPlaneCommand RadiosCopilotRMIRTogCop { get { return new XPlaneCommand("sim/radios/copilot_RMI_R_tog_cop", "Copilot RMI right NAV toggle VOR/NDB.", "Radios Copilot RMIR Tog Cop", XPlaneCommands.RadiosCopilotRMIRTogCop); } }
        private XPlaneCommand InstrumentsECAMModeUp { get { return new XPlaneCommand("sim/instruments/ECAM_mode_up", "ECAM mode up.", "Instruments ECAM Mode Up", XPlaneCommands.InstrumentsECAMModeUp); } }
        private XPlaneCommand InstrumentsECAMModeDown { get { return new XPlaneCommand("sim/instruments/ECAM_mode_down", "ECAM mode down.", "Instruments ECAM Mode Down", XPlaneCommands.InstrumentsECAMModeDown); } }
        private XPlaneCommand InstrumentsMapZoomIn { get { return new XPlaneCommand("sim/instruments/map_zoom_in", "Zoom in EFIS map.", "Instruments Map Zoom In", XPlaneCommands.InstrumentsMapZoomIn); } }
        private XPlaneCommand InstrumentsMapZoomOut { get { return new XPlaneCommand("sim/instruments/map_zoom_out", "Zoom out EFIS map.", "Instruments Map Zoom Out", XPlaneCommands.InstrumentsMapZoomOut); } }
        private XPlaneCommand InstrumentsEFISWxr { get { return new XPlaneCommand("sim/instruments/EFIS_wxr", "EFIS map weather.", "Instruments EFIS Wxr", XPlaneCommands.InstrumentsEFISWxr); } }
        private XPlaneCommand InstrumentsEFISTcas { get { return new XPlaneCommand("sim/instruments/EFIS_tcas", "EFIS map TCAS.", "Instruments EFIS Tcas", XPlaneCommands.InstrumentsEFISTcas); } }
        private XPlaneCommand InstrumentsEFISApt { get { return new XPlaneCommand("sim/instruments/EFIS_apt", "EFIS map airport.", "Instruments EFIS Apt", XPlaneCommands.InstrumentsEFISApt); } }
        private XPlaneCommand InstrumentsEFISFix { get { return new XPlaneCommand("sim/instruments/EFIS_fix", "EFIS map fix.", "Instruments EFIS Fix", XPlaneCommands.InstrumentsEFISFix); } }
        private XPlaneCommand InstrumentsEFISVor { get { return new XPlaneCommand("sim/instruments/EFIS_vor", "EFIS map VOR.", "Instruments EFIS Vor", XPlaneCommands.InstrumentsEFISVor); } }
        private XPlaneCommand InstrumentsEFISNdb { get { return new XPlaneCommand("sim/instruments/EFIS_ndb", "EFIS map NDB.", "Instruments EFIS Ndb", XPlaneCommands.InstrumentsEFISNdb); } }
        private XPlaneCommand InstrumentsEFISPageUp { get { return new XPlaneCommand("sim/instruments/EFIS_page_up", "EFIS page up.", "Instruments EFIS Page Up", XPlaneCommands.InstrumentsEFISPageUp); } }
        private XPlaneCommand InstrumentsEFISPageDn { get { return new XPlaneCommand("sim/instruments/EFIS_page_dn", "EFIS page down.", "Instruments EFIS Page Dn", XPlaneCommands.InstrumentsEFISPageDn); } }
        private XPlaneCommand InstrumentsEFISModeUp { get { return new XPlaneCommand("sim/instruments/EFIS_mode_up", "EFIS mode up (APP/VOR/MAP/PLN)", "Instruments EFIS Mode Up", XPlaneCommands.InstrumentsEFISModeUp); } }
        private XPlaneCommand InstrumentsEFISModeDn { get { return new XPlaneCommand("sim/instruments/EFIS_mode_dn", "EFIS mode down (PLN/MAP/VOR/APP)", "Instruments EFIS Mode Dn", XPlaneCommands.InstrumentsEFISModeDn); } }
        private XPlaneCommand InstrumentsEFIS1PilotSelDn { get { return new XPlaneCommand("sim/instruments/EFIS_1_pilot_sel_dn", "EFIS 1 selection pilot down (ADF/off/VOR)", "Instruments Efis1pilot Sel Dn", XPlaneCommands.InstrumentsEFIS1PilotSelDn); } }
        private XPlaneCommand InstrumentsEFIS1PilotSelUp { get { return new XPlaneCommand("sim/instruments/EFIS_1_pilot_sel_up", "EFIS 1 selection pilot up (ADF/off/VOR)", "Instruments Efis1pilot Sel Up", XPlaneCommands.InstrumentsEFIS1PilotSelUp); } }
        private XPlaneCommand InstrumentsEFIS1CopilotSelDn { get { return new XPlaneCommand("sim/instruments/EFIS_1_copilot_sel_dn", "EFIS 1 selection copilot down (ADF/off/VOR)", "Instruments Efis1copilot Sel Dn", XPlaneCommands.InstrumentsEFIS1CopilotSelDn); } }
        private XPlaneCommand InstrumentsEFIS1CopilotSelUp { get { return new XPlaneCommand("sim/instruments/EFIS_1_copilot_sel_up", "EFIS 1 selection copilot up (ADF/off/VOR)", "Instruments Efis1copilot Sel Up", XPlaneCommands.InstrumentsEFIS1CopilotSelUp); } }
        private XPlaneCommand InstrumentsEFIS2PilotSelDn { get { return new XPlaneCommand("sim/instruments/EFIS_2_pilot_sel_dn", "EFIS 2 selection pilot down (ADF/off/VOR)", "Instruments Efis2pilot Sel Dn", XPlaneCommands.InstrumentsEFIS2PilotSelDn); } }
        private XPlaneCommand InstrumentsEFIS2PilotSelUp { get { return new XPlaneCommand("sim/instruments/EFIS_2_pilot_sel_up", "EFIS 2 selection pilot up (ADF/off/VOR)", "Instruments Efis2pilot Sel Up", XPlaneCommands.InstrumentsEFIS2PilotSelUp); } }
        private XPlaneCommand InstrumentsEFIS2CopilotSelDn { get { return new XPlaneCommand("sim/instruments/EFIS_2_copilot_sel_dn", "EFIS 2 selection copilot down (ADF/off/VOR)", "Instruments Efis2copilot Sel Dn", XPlaneCommands.InstrumentsEFIS2CopilotSelDn); } }
        private XPlaneCommand InstrumentsEFIS2CopilotSelUp { get { return new XPlaneCommand("sim/instruments/EFIS_2_copilot_sel_up", "EFIS 2 selection copilot up (ADF/off/VOR)", "Instruments Efis2copilot Sel Up", XPlaneCommands.InstrumentsEFIS2CopilotSelUp); } }
        private XPlaneCommand RadiosObs1Down { get { return new XPlaneCommand("sim/radios/obs1_down", "OBS 1 down.", "Radios Obs1down", XPlaneCommands.RadiosObs1Down); } }
        private XPlaneCommand RadiosObs1Up { get { return new XPlaneCommand("sim/radios/obs1_up", "OBS 1 up.", "Radios Obs1up", XPlaneCommands.RadiosObs1Up); } }
        private XPlaneCommand RadiosObs2Down { get { return new XPlaneCommand("sim/radios/obs2_down", "OBS 2 down.", "Radios Obs2down", XPlaneCommands.RadiosObs2Down); } }
        private XPlaneCommand RadiosObs2Up { get { return new XPlaneCommand("sim/radios/obs2_up", "OBS 2 up.", "Radios Obs2up", XPlaneCommands.RadiosObs2Up); } }
        private XPlaneCommand RadiosObsHSIDown { get { return new XPlaneCommand("sim/radios/obs_HSI_down", "OBS HSI down.", "Radios Obs HSI Down", XPlaneCommands.RadiosObsHSIDown); } }
        private XPlaneCommand RadiosObsHSIUp { get { return new XPlaneCommand("sim/radios/obs_HSI_up", "OBS HSI up.", "Radios Obs HSI Up", XPlaneCommands.RadiosObsHSIUp); } }
        private XPlaneCommand RadiosObsHSIDirect { get { return new XPlaneCommand("sim/radios/obs_HSI_direct", "OBS HSI direct course.", "Radios Obs HSI Direct", XPlaneCommands.RadiosObsHSIDirect); } }
        private XPlaneCommand RadiosAdf1CardDown { get { return new XPlaneCommand("sim/radios/adf1_card_down", "ADF card 1 down.", "Radios Adf1card Down", XPlaneCommands.RadiosAdf1CardDown); } }
        private XPlaneCommand RadiosAdf1CardUp { get { return new XPlaneCommand("sim/radios/adf1_card_up", "ADF card 1 up.", "Radios Adf1card Up", XPlaneCommands.RadiosAdf1CardUp); } }
        private XPlaneCommand RadiosAdf2CardDown { get { return new XPlaneCommand("sim/radios/adf2_card_down", "ADF card 2 down.", "Radios Adf2card Down", XPlaneCommands.RadiosAdf2CardDown); } }
        private XPlaneCommand RadiosAdf2CardUp { get { return new XPlaneCommand("sim/radios/adf2_card_up", "ADF card 2 up.", "Radios Adf2card Up", XPlaneCommands.RadiosAdf2CardUp); } }
        private XPlaneCommand RadiosCopilotObs1Down { get { return new XPlaneCommand("sim/radios/copilot_obs1_down", "Copilot OBS 1 down.", "Radios Copilot Obs1down", XPlaneCommands.RadiosCopilotObs1Down); } }
        private XPlaneCommand RadiosCopilotObs1Up { get { return new XPlaneCommand("sim/radios/copilot_obs1_up", "Copilot OBS 1 up.", "Radios Copilot Obs1up", XPlaneCommands.RadiosCopilotObs1Up); } }
        private XPlaneCommand RadiosCopilotObs2Down { get { return new XPlaneCommand("sim/radios/copilot_obs2_down", "Copilot OBS 2 down.", "Radios Copilot Obs2down", XPlaneCommands.RadiosCopilotObs2Down); } }
        private XPlaneCommand RadiosCopilotObs2Up { get { return new XPlaneCommand("sim/radios/copilot_obs2_up", "Copilot OBS 2 up.", "Radios Copilot Obs2up", XPlaneCommands.RadiosCopilotObs2Up); } }
        private XPlaneCommand RadiosCopilotObsHSIDown { get { return new XPlaneCommand("sim/radios/copilot_obs_HSI_down", "Copilot OBS HSI down.", "Radios Copilot Obs HSI Down", XPlaneCommands.RadiosCopilotObsHSIDown); } }
        private XPlaneCommand RadiosCopilotObsHSIUp { get { return new XPlaneCommand("sim/radios/copilot_obs_HSI_up", "Copilot OBS HSI up.", "Radios Copilot Obs HSI Up", XPlaneCommands.RadiosCopilotObsHSIUp); } }
        private XPlaneCommand RadiosCopilotObsHSIDirect { get { return new XPlaneCommand("sim/radios/copilot_obs_HSI_direct", "Copilot OBS HSI direct course.", "Radios Copilot Obs HSI Direct", XPlaneCommands.RadiosCopilotObsHSIDirect); } }
        private XPlaneCommand RadiosCopilotAdf1CardDown { get { return new XPlaneCommand("sim/radios/copilot_adf1_card_down", "Copilot ADF card 1 down.", "Radios Copilot Adf1card Down", XPlaneCommands.RadiosCopilotAdf1CardDown); } }
        private XPlaneCommand RadiosCopilotAdf1CardUp { get { return new XPlaneCommand("sim/radios/copilot_adf1_card_up", "Copilot ADF card 1 up.", "Radios Copilot Adf1card Up", XPlaneCommands.RadiosCopilotAdf1CardUp); } }
        private XPlaneCommand RadiosCopilotAdf2CardDown { get { return new XPlaneCommand("sim/radios/copilot_adf2_card_down", "Copilot ADF card 2 down.", "Radios Copilot Adf2card Down", XPlaneCommands.RadiosCopilotAdf2CardDown); } }
        private XPlaneCommand RadiosCopilotAdf2CardUp { get { return new XPlaneCommand("sim/radios/copilot_adf2_card_up", "Copilot ADF card 2 up.", "Radios Copilot Adf2card Up", XPlaneCommands.RadiosCopilotAdf2CardUp); } }
        private XPlaneCommand AutopilotHsiSelectDown { get { return new XPlaneCommand("sim/autopilot/hsi_select_down", "HSI select down one.", "Autopilot Hsi Select Down", XPlaneCommands.AutopilotHsiSelectDown); } }
        private XPlaneCommand AutopilotHsiSelectUp { get { return new XPlaneCommand("sim/autopilot/hsi_select_up", "HSI select up one.", "Autopilot Hsi Select Up", XPlaneCommands.AutopilotHsiSelectUp); } }
        private XPlaneCommand AutopilotHsiSelectNav1 { get { return new XPlaneCommand("sim/autopilot/hsi_select_nav_1", "HSI shows NAV 1.", "Autopilot Hsi Select Nav1", XPlaneCommands.AutopilotHsiSelectNav1); } }
        private XPlaneCommand AutopilotHsiSelectNav2 { get { return new XPlaneCommand("sim/autopilot/hsi_select_nav_2", "HSI shows NAV 2.", "Autopilot Hsi Select Nav2", XPlaneCommands.AutopilotHsiSelectNav2); } }
        private XPlaneCommand AutopilotHsiSelectGps { get { return new XPlaneCommand("sim/autopilot/hsi_select_gps", "HSI shows GPS.", "Autopilot Hsi Select GPS", XPlaneCommands.AutopilotHsiSelectGps); } }
        private XPlaneCommand AutopilotHsiSelectCopilotDown { get { return new XPlaneCommand("sim/autopilot/hsi_select_copilot_down", "HSI select down one, copilot.", "Autopilot Hsi Select Copilot Down", XPlaneCommands.AutopilotHsiSelectCopilotDown); } }
        private XPlaneCommand AutopilotHsiSelectCopilotUp { get { return new XPlaneCommand("sim/autopilot/hsi_select_copilot_up", "HSI select up one, copilot.", "Autopilot Hsi Select Copilot Up", XPlaneCommands.AutopilotHsiSelectCopilotUp); } }
        private XPlaneCommand AutopilotHsiSelectCopilotNav1 { get { return new XPlaneCommand("sim/autopilot/hsi_select_copilot_nav_1", "HSI shows NAV 1, copilot.", "Autopilot Hsi Select Copilot Nav1", XPlaneCommands.AutopilotHsiSelectCopilotNav1); } }
        private XPlaneCommand AutopilotHsiSelectCopilotNav2 { get { return new XPlaneCommand("sim/autopilot/hsi_select_copilot_nav_2", "HSI shows NAV 2, copilot.", "Autopilot Hsi Select Copilot Nav2", XPlaneCommands.AutopilotHsiSelectCopilotNav2); } }
        private XPlaneCommand AutopilotHsiSelectCopilotGps { get { return new XPlaneCommand("sim/autopilot/hsi_select_copilot_gps", "HSI shows GPS, copilot.", "Autopilot Hsi Select Copilot GPS", XPlaneCommands.AutopilotHsiSelectCopilotGps); } }
        private XPlaneCommand FlightControlsCarrierILS { get { return new XPlaneCommand("sim/flight_controls/carrier_ILS", "Set the radios for the carrier ILS.", "Flight Controls Carrier ILS", XPlaneCommands.FlightControlsCarrierILS); } }
        private XPlaneCommand AutopilotSource01 { get { return new XPlaneCommand("sim/autopilot/source_01", "Change autopilot instrument source, 0 or 1", "Autopilot Source01", XPlaneCommands.AutopilotSource01); } }
        private XPlaneCommand AutopilotFdirOn { get { return new XPlaneCommand("sim/autopilot/fdir_on", "Flight director on.", "Autopilot Fdir On", XPlaneCommands.AutopilotFdirOn); } }
        private XPlaneCommand AutopilotFdirToggle { get { return new XPlaneCommand("sim/autopilot/fdir_toggle", "Flight director toggle.", "Autopilot Fdir Toggle", XPlaneCommands.AutopilotFdirToggle); } }
        private XPlaneCommand AutopilotServosOn { get { return new XPlaneCommand("sim/autopilot/servos_on", "Servos on.", "Autopilot Servos On", XPlaneCommands.AutopilotServosOn); } }
        private XPlaneCommand AutopilotServosToggle { get { return new XPlaneCommand("sim/autopilot/servos_toggle", "Servos toggle.", "Autopilot Servos Toggle", XPlaneCommands.AutopilotServosToggle); } }
        private XPlaneCommand AutopilotFdirServosDownOne { get { return new XPlaneCommand("sim/autopilot/fdir_servos_down_one", "Flight director down (on/FDIR/off).", "Autopilot Fdir Servos Down One", XPlaneCommands.AutopilotFdirServosDownOne); } }
        private XPlaneCommand AutopilotFdirServosUpOne { get { return new XPlaneCommand("sim/autopilot/fdir_servos_up_one", "Flight director up (off/FDIR/on).", "Autopilot Fdir Servos Up One", XPlaneCommands.AutopilotFdirServosUpOne); } }
        private XPlaneCommand AutopilotServosFdirOff { get { return new XPlaneCommand("sim/autopilot/servos_fdir_off", "Disco servos, flt dir.", "Autopilot Servos Fdir Off", XPlaneCommands.AutopilotServosFdirOff); } }
        private XPlaneCommand AutopilotServosFdirYawdOff { get { return new XPlaneCommand("sim/autopilot/servos_fdir_yawd_off", "Disco servos, flt dir, yaw damp.", "Autopilot Servos Fdir Yawd Off", XPlaneCommands.AutopilotServosFdirYawdOff); } }
        private XPlaneCommand AutopilotServosFdirYawdTrimOff { get { return new XPlaneCommand("sim/autopilot/servos_fdir_yawd_trim_off", "Disco servos, flt dir, yaw damp, trim.", "Autopilot Servos Fdir Yawd Trim Off", XPlaneCommands.AutopilotServosFdirYawdTrimOff); } }
        private XPlaneCommand AutopilotControlWheelSteer { get { return new XPlaneCommand("sim/autopilot/control_wheel_steer", "Control wheel steering mode.", "Autopilot Control Wheel Steer", XPlaneCommands.AutopilotControlWheelSteer); } }
        private XPlaneCommand AutopilotFdir2On { get { return new XPlaneCommand("sim/autopilot/fdir2_on", "Flight director 2 on.", "Autopilot Fdir2on", XPlaneCommands.AutopilotFdir2On); } }
        private XPlaneCommand AutopilotFdir2Toggle { get { return new XPlaneCommand("sim/autopilot/fdir2_toggle", "Flight director 2 toggle.", "Autopilot Fdir2toggle", XPlaneCommands.AutopilotFdir2Toggle); } }
        private XPlaneCommand AutopilotServos2On { get { return new XPlaneCommand("sim/autopilot/servos2_on", "Servos 2 on.", "Autopilot Servos2on", XPlaneCommands.AutopilotServos2On); } }
        private XPlaneCommand AutopilotServos2Toggle { get { return new XPlaneCommand("sim/autopilot/servos2_toggle", "Servos 2 toggle.", "Autopilot Servos2toggle", XPlaneCommands.AutopilotServos2Toggle); } }
        private XPlaneCommand AutopilotFdir2ServosDownOne { get { return new XPlaneCommand("sim/autopilot/fdir2_servos_down_one", "Flight director 2 down (on/FDIR/off).", "Autopilot Fdir2servos Down One", XPlaneCommands.AutopilotFdir2ServosDownOne); } }
        private XPlaneCommand AutopilotFdir2ServosUpOne { get { return new XPlaneCommand("sim/autopilot/fdir2_servos_up_one", "Flight director 2 up (off/FDIR/on).", "Autopilot Fdir2servos Up One", XPlaneCommands.AutopilotFdir2ServosUpOne); } }
        private XPlaneCommand AutopilotServosFdir2Off { get { return new XPlaneCommand("sim/autopilot/servos_fdir2_off", "Disco servos 2, flt dir 2.", "Autopilot Servos Fdir2off", XPlaneCommands.AutopilotServosFdir2Off); } }
        private XPlaneCommand AutopilotCWSA { get { return new XPlaneCommand("sim/autopilot/CWSA", "CWS A - Control wheel steering mode AP A.", "Autopilot CWSA", XPlaneCommands.AutopilotCWSA); } }
        private XPlaneCommand AutopilotCWSB { get { return new XPlaneCommand("sim/autopilot/CWSB", "CWS B - Control wheel steering mode AP B.", "Autopilot CWSB", XPlaneCommands.AutopilotCWSB); } }
        private XPlaneCommand AutopilotServos3On { get { return new XPlaneCommand("sim/autopilot/servos3_on", "Servos 3 on.", "Autopilot Servos3on", XPlaneCommands.AutopilotServos3On); } }
        private XPlaneCommand AutopilotServos3Toggle { get { return new XPlaneCommand("sim/autopilot/servos3_toggle", "Servos 3 toggle.", "Autopilot Servos3toggle", XPlaneCommands.AutopilotServos3Toggle); } }
        private XPlaneCommand AutopilotServosFdir3Off { get { return new XPlaneCommand("sim/autopilot/servos_fdir3_off", "Disco servos 3.", "Autopilot Servos Fdir3off", XPlaneCommands.AutopilotServosFdir3Off); } }
        private XPlaneCommand AutopilotServosOffAny { get { return new XPlaneCommand("sim/autopilot/servos_off_any", "Disco servos, which ever side is active.", "Autopilot Servos Off Any", XPlaneCommands.AutopilotServosOffAny); } }
        private XPlaneCommand AutopilotServosYawdOffAny { get { return new XPlaneCommand("sim/autopilot/servos_yawd_off_any", "Disco servos and yaw damp, any side.", "Autopilot Servos Yawd Off Any", XPlaneCommands.AutopilotServosYawdOffAny); } }
        private XPlaneCommand AutopilotServosYawdTrimOffAny { get { return new XPlaneCommand("sim/autopilot/servos_yawd_trim_off_any", "Disco servos, yaw damp and trim any side.", "Autopilot Servos Yawd Trim Off Any", XPlaneCommands.AutopilotServosYawdTrimOffAny); } }
        private XPlaneCommand AutopilotAutothrottleOn { get { return new XPlaneCommand("sim/autopilot/autothrottle_on", "Autopilot auto-throttle speed-hold on.", "Autopilot Autothrottle On", XPlaneCommands.AutopilotAutothrottleOn); } }
        private XPlaneCommand AutopilotAutothrottleOff { get { return new XPlaneCommand("sim/autopilot/autothrottle_off", "Autopilot auto-throttle all modes off.", "Autopilot Autothrottle Off", XPlaneCommands.AutopilotAutothrottleOff); } }
        private XPlaneCommand AutopilotAutothrottleToggle { get { return new XPlaneCommand("sim/autopilot/autothrottle_toggle", "Autopilot auto-throttle speed toggle.", "Autopilot Autothrottle Toggle", XPlaneCommands.AutopilotAutothrottleToggle); } }
        private XPlaneCommand AutopilotAutothrottleN1epr { get { return new XPlaneCommand("sim/autopilot/autothrottle_n1epr", "Autopilot auto-throttle EPR/N1 hold on.", "Autopilot Autothrottle N1epr", XPlaneCommands.AutopilotAutothrottleN1epr); } }
        private XPlaneCommand AutopilotAutothrottleN1eprToggle { get { return new XPlaneCommand("sim/autopilot/autothrottle_n1epr_toggle", "Autopilot auto-throttle EPR/N1 hold toggle.", "Autopilot Autothrottle N1epr Toggle", XPlaneCommands.AutopilotAutothrottleN1eprToggle); } }
        private XPlaneCommand AutopilotHeading { get { return new XPlaneCommand("sim/autopilot/heading", "Autopilot heading-select.", "Autopilot Heading", XPlaneCommands.AutopilotHeading); } }
        private XPlaneCommand AutopilotTrack { get { return new XPlaneCommand("sim/autopilot/track", "Autopilot track.", "Autopilot Track", XPlaneCommands.AutopilotTrack); } }
        private XPlaneCommand AutopilotHeadingHold { get { return new XPlaneCommand("sim/autopilot/heading_hold", "Autopilot heading-hold.", "Autopilot Heading Hold", XPlaneCommands.AutopilotHeadingHold); } }
        private XPlaneCommand AutopilotWingLeveler { get { return new XPlaneCommand("sim/autopilot/wing_leveler", "Autopilot wing-level / roll hold.", "Autopilot Wing Leveler", XPlaneCommands.AutopilotWingLeveler); } }
        private XPlaneCommand AutopilotRateHold { get { return new XPlaneCommand("sim/autopilot/rate_hold", "Autopilot wing-level / rate hold.", "Autopilot Rate Hold", XPlaneCommands.AutopilotRateHold); } }
        private XPlaneCommand AutopilotHdgNav { get { return new XPlaneCommand("sim/autopilot/hdg_nav", "Autopilot heading select and NAV arm.", "Autopilot Hdg Nav", XPlaneCommands.AutopilotHdgNav); } }
        private XPlaneCommand AutopilotNAV { get { return new XPlaneCommand("sim/autopilot/NAV", "Autopilot VOR/LOC arm.", "Autopilot NAV", XPlaneCommands.AutopilotNAV); } }
        private XPlaneCommand AutopilotVerticalSpeed { get { return new XPlaneCommand("sim/autopilot/vertical_speed", "Autopilot vertical speed, at current VSI.", "Autopilot Vertical Speed", XPlaneCommands.AutopilotVerticalSpeed); } }
        private XPlaneCommand AutopilotFpa { get { return new XPlaneCommand("sim/autopilot/fpa", "Autopilot flight path angle, current FPA.", "Autopilot Fpa", XPlaneCommands.AutopilotFpa); } }
        private XPlaneCommand AutopilotAltVs { get { return new XPlaneCommand("sim/autopilot/alt_vs", "Autopilot vertical speed, at current VSI, arm ALT.", "Autopilot Alt Vs", XPlaneCommands.AutopilotAltVs); } }
        private XPlaneCommand AutopilotVerticalSpeedPreSel { get { return new XPlaneCommand("sim/autopilot/vertical_speed_pre_sel", "Autopilot vertical speed, at pre-sel VSI.", "Autopilot Vertical Speed Pre Sel", XPlaneCommands.AutopilotVerticalSpeedPreSel); } }
        private XPlaneCommand AutopilotPitchSync { get { return new XPlaneCommand("sim/autopilot/pitch_sync", "Autopilot pitch-sync.", "Autopilot Pitch Sync", XPlaneCommands.AutopilotPitchSync); } }
        private XPlaneCommand AutopilotLevelChange { get { return new XPlaneCommand("sim/autopilot/level_change", "Autopilot level change.", "Autopilot Level Change", XPlaneCommands.AutopilotLevelChange); } }
        private XPlaneCommand AutopilotAltitudeHold { get { return new XPlaneCommand("sim/autopilot/altitude_hold", "Autopilot altitude select or hold.", "Autopilot Altitude Hold", XPlaneCommands.AutopilotAltitudeHold); } }
        private XPlaneCommand AutopilotTerrainFollowing { get { return new XPlaneCommand("sim/autopilot/terrain_following", "Autopilot terrain-mode following.", "Autopilot Terrain Following", XPlaneCommands.AutopilotTerrainFollowing); } }
        private XPlaneCommand AutopilotTakeOffGoAround { get { return new XPlaneCommand("sim/autopilot/take_off_go_around", "Autopilot take-off go-around.", "Autopilot Take Off Go Around", XPlaneCommands.AutopilotTakeOffGoAround); } }
        private XPlaneCommand AutopilotReentry { get { return new XPlaneCommand("sim/autopilot/reentry", "Autopilot re-entry.", "Autopilot Reentry", XPlaneCommands.AutopilotReentry); } }
        private XPlaneCommand AutopilotGlideSlope { get { return new XPlaneCommand("sim/autopilot/glide_slope", "Autopilot glideslope.", "Autopilot Glide Slope", XPlaneCommands.AutopilotGlideSlope); } }
        private XPlaneCommand AutopilotVnav { get { return new XPlaneCommand("sim/autopilot/vnav", "Autopilot VNAV for G1000.", "Autopilot Vnav", XPlaneCommands.AutopilotVnav); } }
        private XPlaneCommand AutopilotGpss { get { return new XPlaneCommand("sim/autopilot/gpss", "Autopilot GPS Steering.", "Autopilot GPSs", XPlaneCommands.AutopilotGpss); } }
        private XPlaneCommand AutopilotClimb { get { return new XPlaneCommand("sim/autopilot/climb", "Autopilot GPS Climb.", "Autopilot Climb", XPlaneCommands.AutopilotClimb); } }
        private XPlaneCommand AutopilotDescend { get { return new XPlaneCommand("sim/autopilot/descend", "Autopilot GPS Descend.", "Autopilot Descend", XPlaneCommands.AutopilotDescend); } }
        private XPlaneCommand AutopilotTrkfpa { get { return new XPlaneCommand("sim/autopilot/trkfpa", "Autopilot TRK/FPA vs HDG/VS toggle.", "Autopilot Trkfpa", XPlaneCommands.AutopilotTrkfpa); } }
        private XPlaneCommand AutopilotAirspeedSync { get { return new XPlaneCommand("sim/autopilot/airspeed_sync", "Autopilot airspeed sync.", "Autopilot Airspeed Sync", XPlaneCommands.AutopilotAirspeedSync); } }
        private XPlaneCommand AutopilotHeadingSync { get { return new XPlaneCommand("sim/autopilot/heading_sync", "Autopilot heading sync.", "Autopilot Heading Sync", XPlaneCommands.AutopilotHeadingSync); } }
        private XPlaneCommand AutopilotVerticalSpeedSync { get { return new XPlaneCommand("sim/autopilot/vertical_speed_sync", "Autopilot VVI sync.", "Autopilot Vertical Speed Sync", XPlaneCommands.AutopilotVerticalSpeedSync); } }
        private XPlaneCommand AutopilotAltitudeSync { get { return new XPlaneCommand("sim/autopilot/altitude_sync", "Autopilot altitude sync.", "Autopilot Altitude Sync", XPlaneCommands.AutopilotAltitudeSync); } }
        private XPlaneCommand AutopilotHeadingDown { get { return new XPlaneCommand("sim/autopilot/heading_down", "Autopilot heading down.", "Autopilot Heading Down", XPlaneCommands.AutopilotHeadingDown); } }
        private XPlaneCommand AutopilotHeadingUp { get { return new XPlaneCommand("sim/autopilot/heading_up", "Autopilot heading up.", "Autopilot Heading Up", XPlaneCommands.AutopilotHeadingUp); } }
        private XPlaneCommand AutopilotHeadingCopilotDown { get { return new XPlaneCommand("sim/autopilot/heading_copilot_down", "Autopilot heading copilot down.", "Autopilot Heading Copilot Down", XPlaneCommands.AutopilotHeadingCopilotDown); } }
        private XPlaneCommand AutopilotHeadingCopilotUp { get { return new XPlaneCommand("sim/autopilot/heading_copilot_up", "Autopilot heading copilot up.", "Autopilot Heading Copilot Up", XPlaneCommands.AutopilotHeadingCopilotUp); } }
        private XPlaneCommand AutopilotAirspeedDown { get { return new XPlaneCommand("sim/autopilot/airspeed_down", "Autopilot airspeed down.", "Autopilot Airspeed Down", XPlaneCommands.AutopilotAirspeedDown); } }
        private XPlaneCommand AutopilotAirspeedUp { get { return new XPlaneCommand("sim/autopilot/airspeed_up", "Autopilot airspeed up.", "Autopilot Airspeed Up", XPlaneCommands.AutopilotAirspeedUp); } }
        private XPlaneCommand AutopilotVerticalSpeedDown { get { return new XPlaneCommand("sim/autopilot/vertical_speed_down", "Autopilot VVI down.", "Autopilot Vertical Speed Down", XPlaneCommands.AutopilotVerticalSpeedDown); } }
        private XPlaneCommand AutopilotVerticalSpeedUp { get { return new XPlaneCommand("sim/autopilot/vertical_speed_up", "Autopilot VVI up.", "Autopilot Vertical Speed Up", XPlaneCommands.AutopilotVerticalSpeedUp); } }
        private XPlaneCommand AutopilotAltitudeDown { get { return new XPlaneCommand("sim/autopilot/altitude_down", "Autopilot altitude down.", "Autopilot Altitude Down", XPlaneCommands.AutopilotAltitudeDown); } }
        private XPlaneCommand AutopilotAltitudeUp { get { return new XPlaneCommand("sim/autopilot/altitude_up", "Autopilot altitude up.", "Autopilot Altitude Up", XPlaneCommands.AutopilotAltitudeUp); } }
        private XPlaneCommand AutopilotNoseDown { get { return new XPlaneCommand("sim/autopilot/nose_down", "Autopilot nose down.", "Autopilot Nose Down", XPlaneCommands.AutopilotNoseDown); } }
        private XPlaneCommand AutopilotNoseUp { get { return new XPlaneCommand("sim/autopilot/nose_up", "Autopilot nose up.", "Autopilot Nose Up", XPlaneCommands.AutopilotNoseUp); } }
        private XPlaneCommand AutopilotNoseDownPitchMode { get { return new XPlaneCommand("sim/autopilot/nose_down_pitch_mode", "Autopilot nose down, go into pitch mode.", "Autopilot Nose Down Pitch Mode", XPlaneCommands.AutopilotNoseDownPitchMode); } }
        private XPlaneCommand AutopilotNoseUpPitchMode { get { return new XPlaneCommand("sim/autopilot/nose_up_pitch_mode", "Autopilot nose up, go into pitch mode", "Autopilot Nose Up Pitch Mode", XPlaneCommands.AutopilotNoseUpPitchMode); } }
        private XPlaneCommand AutopilotOverrideLeft { get { return new XPlaneCommand("sim/autopilot/override_left", "Autopilot override left: Go to ROL mode.", "Autopilot Override Left", XPlaneCommands.AutopilotOverrideLeft); } }
        private XPlaneCommand AutopilotOverrideRight { get { return new XPlaneCommand("sim/autopilot/override_right", "Autopilot override right: Go to ROL mode.", "Autopilot Override Right", XPlaneCommands.AutopilotOverrideRight); } }
        private XPlaneCommand AutopilotOverrideCenter { get { return new XPlaneCommand("sim/autopilot/override_center", "Autopilot override center: Go to ROL mode, 0 turn rate roll angle.", "Autopilot Override Center", XPlaneCommands.AutopilotOverrideCenter); } }
        private XPlaneCommand AutopilotOverrideUp { get { return new XPlaneCommand("sim/autopilot/override_up", "Autopilot override up: Go to SYN mode.", "Autopilot Override Up", XPlaneCommands.AutopilotOverrideUp); } }
        private XPlaneCommand AutopilotOverrideDown { get { return new XPlaneCommand("sim/autopilot/override_down", "Autopilot override down: Go to SYN mode.", "Autopilot Override Down", XPlaneCommands.AutopilotOverrideDown); } }
        private XPlaneCommand AutopilotAltitudeArm { get { return new XPlaneCommand("sim/autopilot/altitude_arm", "Autopilot altitude hold ARM.", "Autopilot Altitude Arm", XPlaneCommands.AutopilotAltitudeArm); } }
        private XPlaneCommand AutopilotApproach { get { return new XPlaneCommand("sim/autopilot/approach", "Autopilot approach.", "Autopilot Approach", XPlaneCommands.AutopilotApproach); } }
        private XPlaneCommand AutopilotBackCourse { get { return new XPlaneCommand("sim/autopilot/back_course", "Autopilot back-course.", "Autopilot Back Course", XPlaneCommands.AutopilotBackCourse); } }
        private XPlaneCommand AutopilotKnotsMachToggle { get { return new XPlaneCommand("sim/autopilot/knots_mach_toggle", "Toggle knots-Mach airspeeed hold.", "Autopilot Knots Mach Toggle", XPlaneCommands.AutopilotKnotsMachToggle); } }
        private XPlaneCommand AutopilotFMS { get { return new XPlaneCommand("sim/autopilot/FMS", "Autopilot FMS altitude.", "Autopilot FMS", XPlaneCommands.AutopilotFMS); } }
        private XPlaneCommand AutopilotBankLimitDown { get { return new XPlaneCommand("sim/autopilot/bank_limit_down", "Bank angle limit down.", "Autopilot Bank Limit Down", XPlaneCommands.AutopilotBankLimitDown); } }
        private XPlaneCommand AutopilotBankLimitUp { get { return new XPlaneCommand("sim/autopilot/bank_limit_up", "Bank angle limit up.", "Autopilot Bank Limit Up", XPlaneCommands.AutopilotBankLimitUp); } }
        private XPlaneCommand AutopilotBankLimitToggle { get { return new XPlaneCommand("sim/autopilot/bank_limit_toggle", "Bank angle limit toggle.", "Autopilot Bank Limit Toggle", XPlaneCommands.AutopilotBankLimitToggle); } }
        private XPlaneCommand AutopilotSoftRideToggle { get { return new XPlaneCommand("sim/autopilot/soft_ride_toggle", "Soft ride toggle.", "Autopilot Soft Ride Toggle", XPlaneCommands.AutopilotSoftRideToggle); } }
        private XPlaneCommand ElectricalDcVoltDn { get { return new XPlaneCommand("sim/electrical/dc_volt_dn", "DC Voltmeter down.", "Electrical Dc Volt Dn", XPlaneCommands.ElectricalDcVoltDn); } }
        private XPlaneCommand ElectricalDcVoltUp { get { return new XPlaneCommand("sim/electrical/dc_volt_up", "DC Voltmeter up.", "Electrical Dc Volt Up", XPlaneCommands.ElectricalDcVoltUp); } }
        private XPlaneCommand ElectricalDcVoltExt { get { return new XPlaneCommand("sim/electrical/dc_volt_ext", "DC Voltmeter external power.", "Electrical Dc Volt Ext", XPlaneCommands.ElectricalDcVoltExt); } }
        private XPlaneCommand ElectricalDcVoltCtr { get { return new XPlaneCommand("sim/electrical/dc_volt_ctr", "DC Voltmeter center.", "Electrical Dc Volt Ctr", XPlaneCommands.ElectricalDcVoltCtr); } }
        private XPlaneCommand ElectricalDcVoltLft { get { return new XPlaneCommand("sim/electrical/dc_volt_lft", "DC Voltmeter left.", "Electrical Dc Volt Lft", XPlaneCommands.ElectricalDcVoltLft); } }
        private XPlaneCommand ElectricalDcVoltRgt { get { return new XPlaneCommand("sim/electrical/dc_volt_rgt", "DC Voltmeter right.", "Electrical Dc Volt Rgt", XPlaneCommands.ElectricalDcVoltRgt); } }
        private XPlaneCommand ElectricalDcVoltTpl { get { return new XPlaneCommand("sim/electrical/dc_volt_tpl", "DC Voltmeter TPL-fed.", "Electrical Dc Volt Tpl", XPlaneCommands.ElectricalDcVoltTpl); } }
        private XPlaneCommand ElectricalDcVoltBat { get { return new XPlaneCommand("sim/electrical/dc_volt_bat", "DC Voltmeter battery.", "Electrical Dc Volt Bat", XPlaneCommands.ElectricalDcVoltBat); } }
        private XPlaneCommand HUDPowerToggle { get { return new XPlaneCommand("sim/HUD/power_toggle", "HUD toggle power.", "HUD Power Toggle", XPlaneCommands.HUDPowerToggle); } }
        private XPlaneCommand HUDBrightnessToggle { get { return new XPlaneCommand("sim/HUD/brightness_toggle", "HUD toggle brightness.", "HUD Brightness Toggle", XPlaneCommands.HUDBrightnessToggle); } }
        private XPlaneCommand SystemsTotalEnergyAudioToggle { get { return new XPlaneCommand("sim/systems/total_energy_audio_toggle", "Toggle total-energy audio.", "Systems Total Energy Audio Toggle", XPlaneCommands.SystemsTotalEnergyAudioToggle); } }
        private XPlaneCommand InstrumentsThermoUnitsToggle { get { return new XPlaneCommand("sim/instruments/thermo_units_toggle", "Toggle english/metric thermometer.", "Instruments Thermo Units Toggle", XPlaneCommands.InstrumentsThermoUnitsToggle); } }
        private XPlaneCommand InstrumentsBarometer2992 { get { return new XPlaneCommand("sim/instruments/barometer_2992", "Baro pressure selection 2992.", "Instruments Barometer2992", XPlaneCommands.InstrumentsBarometer2992); } }
        private XPlaneCommand InstrumentsDGSyncDown { get { return new XPlaneCommand("sim/instruments/DG_sync_down", "vacuum DG sync down.", "Instruments DG Sync Down", XPlaneCommands.InstrumentsDGSyncDown); } }
        private XPlaneCommand InstrumentsDGSyncUp { get { return new XPlaneCommand("sim/instruments/DG_sync_up", "vacuum DG sync up.", "Instruments DG Sync Up", XPlaneCommands.InstrumentsDGSyncUp); } }
        private XPlaneCommand InstrumentsDGSyncMag { get { return new XPlaneCommand("sim/instruments/DG_sync_mag", "vacuum DG sync to magnetic north.", "Instruments DG Sync Mag", XPlaneCommands.InstrumentsDGSyncMag); } }
        private XPlaneCommand InstrumentsCopilotDGSyncDown { get { return new XPlaneCommand("sim/instruments/copilot_DG_sync_down", "Copilot vacuum DG sync down.", "Instruments Copilot DG Sync Down", XPlaneCommands.InstrumentsCopilotDGSyncDown); } }
        private XPlaneCommand InstrumentsCopilotDGSyncUp { get { return new XPlaneCommand("sim/instruments/copilot_DG_sync_up", "Copilot vacuum DG sync up.", "Instruments Copilot DG Sync Up", XPlaneCommands.InstrumentsCopilotDGSyncUp); } }
        private XPlaneCommand InstrumentsCopilotDGSyncMag { get { return new XPlaneCommand("sim/instruments/copilot_DG_sync_mag", "Copilot vacuum DG sync to magnetic north.", "Instruments Copilot DG Sync Mag", XPlaneCommands.InstrumentsCopilotDGSyncMag); } }
        private XPlaneCommand InstrumentsFreeGyro { get { return new XPlaneCommand("sim/instruments/free_gyro", "electric DG free.", "Instruments Free Gyro", XPlaneCommands.InstrumentsFreeGyro); } }
        private XPlaneCommand InstrumentsSlaveGyro { get { return new XPlaneCommand("sim/instruments/slave_gyro", "electric DG slave.", "Instruments Slave Gyro", XPlaneCommands.InstrumentsSlaveGyro); } }
        private XPlaneCommand InstrumentsCopilotFreeGyro { get { return new XPlaneCommand("sim/instruments/copilot_free_gyro", "Copilot electric DG free.", "Instruments Copilot Free Gyro", XPlaneCommands.InstrumentsCopilotFreeGyro); } }
        private XPlaneCommand InstrumentsCopilotSlaveGyro { get { return new XPlaneCommand("sim/instruments/copilot_slave_gyro", "Copilot electric DG slave.", "Instruments Copilot Slave Gyro", XPlaneCommands.InstrumentsCopilotSlaveGyro); } }
        private XPlaneCommand InstrumentsFreeGyroDown { get { return new XPlaneCommand("sim/instruments/free_gyro_down", "electric DG free sync down.", "Instruments Free Gyro Down", XPlaneCommands.InstrumentsFreeGyroDown); } }
        private XPlaneCommand InstrumentsFreeGyroUp { get { return new XPlaneCommand("sim/instruments/free_gyro_up", "electric DG free sync up.", "Instruments Free Gyro Up", XPlaneCommands.InstrumentsFreeGyroUp); } }
        private XPlaneCommand InstrumentsCopilotFreeGyroDown { get { return new XPlaneCommand("sim/instruments/copilot_free_gyro_down", "Copilot electric DG free sync down.", "Instruments Copilot Free Gyro Down", XPlaneCommands.InstrumentsCopilotFreeGyroDown); } }
        private XPlaneCommand InstrumentsCopilotFreeGyroUp { get { return new XPlaneCommand("sim/instruments/copilot_free_gyro_up", "Copilot electric DG free sync up.", "Instruments Copilot Free Gyro Up", XPlaneCommands.InstrumentsCopilotFreeGyroUp); } }
        private XPlaneCommand InstrumentsAhRefDown { get { return new XPlaneCommand("sim/instruments/ah_ref_down", "Horizon reference down a bit.", "Instruments Ah Ref Down", XPlaneCommands.InstrumentsAhRefDown); } }
        private XPlaneCommand InstrumentsAhRefUp { get { return new XPlaneCommand("sim/instruments/ah_ref_up", "Horizon reference up a bit.", "Instruments Ah Ref Up", XPlaneCommands.InstrumentsAhRefUp); } }
        private XPlaneCommand InstrumentsAhRefCopilotDown { get { return new XPlaneCommand("sim/instruments/ah_ref_copilot_down", "Horizon reference down a bit, copilot.", "Instruments Ah Ref Copilot Down", XPlaneCommands.InstrumentsAhRefCopilotDown); } }
        private XPlaneCommand InstrumentsAhRefCopilotUp { get { return new XPlaneCommand("sim/instruments/ah_ref_copilot_up", "Horizon reference down a bit, copilot.", "Instruments Ah Ref Copilot Up", XPlaneCommands.InstrumentsAhRefCopilotUp); } }
        private XPlaneCommand InstrumentsAhFastErect { get { return new XPlaneCommand("sim/instruments/ah_fast_erect", "Attitude vacuum gyro fast erect.", "Instruments Ah Fast Erect", XPlaneCommands.InstrumentsAhFastErect); } }
        private XPlaneCommand InstrumentsAhCage { get { return new XPlaneCommand("sim/instruments/ah_cage", "Attitude vacuum gyro cage toggle.", "Instruments Ah Cage", XPlaneCommands.InstrumentsAhCage); } }
        private XPlaneCommand InstrumentsAhFastErectCopilot { get { return new XPlaneCommand("sim/instruments/ah_fast_erect_copilot", "Attitude vacuum gyro fast erect, copilot.", "Instruments Ah Fast Erect Copilot", XPlaneCommands.InstrumentsAhFastErectCopilot); } }
        private XPlaneCommand InstrumentsAhCageCopilot { get { return new XPlaneCommand("sim/instruments/ah_cage_copilot", "Attitude vacuum gyro cage toggle, copilot", "Instruments Ah Cage Copilot", XPlaneCommands.InstrumentsAhCageCopilot); } }
        private XPlaneCommand InstrumentsBarometerDown { get { return new XPlaneCommand("sim/instruments/barometer_down", "Baro selection down a bit.", "Instruments Barometer Down", XPlaneCommands.InstrumentsBarometerDown); } }
        private XPlaneCommand InstrumentsBarometerUp { get { return new XPlaneCommand("sim/instruments/barometer_up", "Baro selection up a bit.", "Instruments Barometer Up", XPlaneCommands.InstrumentsBarometerUp); } }
        private XPlaneCommand InstrumentsBarometerCopilotDown { get { return new XPlaneCommand("sim/instruments/barometer_copilot_down", "Baro selection down a bit, copilot.", "Instruments Barometer Copilot Down", XPlaneCommands.InstrumentsBarometerCopilotDown); } }
        private XPlaneCommand InstrumentsBarometerCopilotUp { get { return new XPlaneCommand("sim/instruments/barometer_copilot_up", "Baro selection up a bit, copilot.", "Instruments Barometer Copilot Up", XPlaneCommands.InstrumentsBarometerCopilotUp); } }
        private XPlaneCommand InstrumentsBarometerStbyDown { get { return new XPlaneCommand("sim/instruments/barometer_stby_down", "Baro selection down a bit, standby alt.", "Instruments Barometer Stby Down", XPlaneCommands.InstrumentsBarometerStbyDown); } }
        private XPlaneCommand InstrumentsBarometerStbyUp { get { return new XPlaneCommand("sim/instruments/barometer_stby_up", "Baro selection up a bit, standby alt.", "Instruments Barometer Stby Up", XPlaneCommands.InstrumentsBarometerStbyUp); } }
        private XPlaneCommand InstrumentsBarometerApDown { get { return new XPlaneCommand("sim/instruments/barometer_ap_down", "Baro selection down a bit, AP presel.", "Instruments Barometer AP Down", XPlaneCommands.InstrumentsBarometerApDown); } }
        private XPlaneCommand InstrumentsBarometerApUp { get { return new XPlaneCommand("sim/instruments/barometer_ap_up", "Baro selection up a bit, AP presel.", "Instruments Barometer AP Up", XPlaneCommands.InstrumentsBarometerApUp); } }
        private XPlaneCommand InstrumentsDhRefDown { get { return new XPlaneCommand("sim/instruments/dh_ref_down", "Decision-height reference down.", "Instruments Dh Ref Down", XPlaneCommands.InstrumentsDhRefDown); } }
        private XPlaneCommand InstrumentsDhRefUp { get { return new XPlaneCommand("sim/instruments/dh_ref_up", "Decision-height reference up.", "Instruments Dh Ref Up", XPlaneCommands.InstrumentsDhRefUp); } }
        private XPlaneCommand InstrumentsDhRefCopilotDown { get { return new XPlaneCommand("sim/instruments/dh_ref_copilot_down", "Decision-height reference copilot down.", "Instruments Dh Ref Copilot Down", XPlaneCommands.InstrumentsDhRefCopilotDown); } }
        private XPlaneCommand InstrumentsDhRefCopilotUp { get { return new XPlaneCommand("sim/instruments/dh_ref_copilot_up", "Decision-height reference copilot up.", "Instruments Dh Ref Copilot Up", XPlaneCommands.InstrumentsDhRefCopilotUp); } }
        private XPlaneCommand InstrumentsMdaRefDown { get { return new XPlaneCommand("sim/instruments/mda_ref_down", "Minimum descend alt reference down.", "Instruments Mda Ref Down", XPlaneCommands.InstrumentsMdaRefDown); } }
        private XPlaneCommand InstrumentsMdaRefUp { get { return new XPlaneCommand("sim/instruments/mda_ref_up", "Minimum descend alt reference up.", "Instruments Mda Ref Up", XPlaneCommands.InstrumentsMdaRefUp); } }
        private XPlaneCommand InstrumentsMdaRefCopilotDown { get { return new XPlaneCommand("sim/instruments/mda_ref_copilot_down", "Minimum descend alt reference copilot down.", "Instruments Mda Ref Copilot Down", XPlaneCommands.InstrumentsMdaRefCopilotDown); } }
        private XPlaneCommand InstrumentsMdaRefCopilotUp { get { return new XPlaneCommand("sim/instruments/mda_ref_copilot_up", "Minimum descend alt reference copilot up.", "Instruments Mda Ref Copilot Up", XPlaneCommands.InstrumentsMdaRefCopilotUp); } }
        private XPlaneCommand InstrumentsBaroAltAlertCancel { get { return new XPlaneCommand("sim/instruments/baro_alt_alert_cancel", "Cancel altitude alert (preselector).", "Instruments Baro Alt Alert Cancel", XPlaneCommands.InstrumentsBaroAltAlertCancel); } }
        private XPlaneCommand InstrumentsMdaAlertCancel { get { return new XPlaneCommand("sim/instruments/mda_alert_cancel", "Cancel MDA alert.", "Instruments Mda Alert Cancel", XPlaneCommands.InstrumentsMdaAlertCancel); } }
        private XPlaneCommand InstrumentsPanelBrightDown { get { return new XPlaneCommand("sim/instruments/panel_bright_down", "Panel brightness down a bit.", "Instruments Panel Bright Down", XPlaneCommands.InstrumentsPanelBrightDown); } }
        private XPlaneCommand InstrumentsPanelBrightUp { get { return new XPlaneCommand("sim/instruments/panel_bright_up", "Panel brightness up a bit.", "Instruments Panel Bright Up", XPlaneCommands.InstrumentsPanelBrightUp); } }
        private XPlaneCommand InstrumentsInstrumentBrightDown { get { return new XPlaneCommand("sim/instruments/instrument_bright_down", "Instrument brightness down a bit.", "Instruments Instrument Bright Down", XPlaneCommands.InstrumentsInstrumentBrightDown); } }
        private XPlaneCommand InstrumentsInstrumentBrightUp { get { return new XPlaneCommand("sim/instruments/instrument_bright_up", "Instrument brightness up a bit.", "Instruments Instrument Bright Up", XPlaneCommands.InstrumentsInstrumentBrightUp); } }
        private XPlaneCommand AnnunciatorTestAllAnnunciators { get { return new XPlaneCommand("sim/annunciator/test_all_annunciators", "Test all annunciators.", "Annunciator Test All Annunciators", XPlaneCommands.AnnunciatorTestAllAnnunciators); } }
        private XPlaneCommand AnnunciatorTestStall { get { return new XPlaneCommand("sim/annunciator/test_stall", "Test stall warn.", "Annunciator Test Stall", XPlaneCommands.AnnunciatorTestStall); } }
        private XPlaneCommand AnnunciatorTestFire1Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_1_annun", "Test fire 1 annunciator.", "Annunciator Test Fire1annun", XPlaneCommands.AnnunciatorTestFire1Annun); } }
        private XPlaneCommand AnnunciatorTestFire2Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_2_annun", "Test fire 2 annunciator.", "Annunciator Test Fire2annun", XPlaneCommands.AnnunciatorTestFire2Annun); } }
        private XPlaneCommand AnnunciatorTestFire3Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_3_annun", "Test fire 3 annunciator.", "Annunciator Test Fire3annun", XPlaneCommands.AnnunciatorTestFire3Annun); } }
        private XPlaneCommand AnnunciatorTestFire4Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_4_annun", "Test fire 4 annunciator.", "Annunciator Test Fire4annun", XPlaneCommands.AnnunciatorTestFire4Annun); } }
        private XPlaneCommand AnnunciatorTestFire5Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_5_annun", "Test fire 5 annunciator.", "Annunciator Test Fire5annun", XPlaneCommands.AnnunciatorTestFire5Annun); } }
        private XPlaneCommand AnnunciatorTestFire6Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_6_annun", "Test fire 6 annunciator.", "Annunciator Test Fire6annun", XPlaneCommands.AnnunciatorTestFire6Annun); } }
        private XPlaneCommand AnnunciatorTestFire7Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_7_annun", "Test fire 7 annunciator.", "Annunciator Test Fire7annun", XPlaneCommands.AnnunciatorTestFire7Annun); } }
        private XPlaneCommand AnnunciatorTestFire8Annun { get { return new XPlaneCommand("sim/annunciator/test_fire_8_annun", "Test fire 8 annunciator.", "Annunciator Test Fire8annun", XPlaneCommands.AnnunciatorTestFire8Annun); } }
        private XPlaneCommand AnnunciatorClearMasterCaution { get { return new XPlaneCommand("sim/annunciator/clear_master_caution", "Clear master caution.", "Annunciator Clear Master Caution", XPlaneCommands.AnnunciatorClearMasterCaution); } }
        private XPlaneCommand AnnunciatorClearMasterWarning { get { return new XPlaneCommand("sim/annunciator/clear_master_warning", "Clear master warning.", "Annunciator Clear Master Warning", XPlaneCommands.AnnunciatorClearMasterWarning); } }
        private XPlaneCommand AnnunciatorClearMasterAccept { get { return new XPlaneCommand("sim/annunciator/clear_master_accept", "Clear master accept.", "Annunciator Clear Master Accept", XPlaneCommands.AnnunciatorClearMasterAccept); } }
        private XPlaneCommand FMSLs1L { get { return new XPlaneCommand("sim/FMS/ls_1l", "FMS: line select 1l", "FMS Ls1l", XPlaneCommands.FMSLs1L); } }
        private XPlaneCommand FMSLs2L { get { return new XPlaneCommand("sim/FMS/ls_2l", "FMS: line select 2l", "FMS Ls2l", XPlaneCommands.FMSLs2L); } }
        private XPlaneCommand FMSLs3L { get { return new XPlaneCommand("sim/FMS/ls_3l", "FMS: line select 3l", "FMS Ls3l", XPlaneCommands.FMSLs3L); } }
        private XPlaneCommand FMSLs4L { get { return new XPlaneCommand("sim/FMS/ls_4l", "FMS: line select 4l", "FMS Ls4l", XPlaneCommands.FMSLs4L); } }
        private XPlaneCommand FMSLs5L { get { return new XPlaneCommand("sim/FMS/ls_5l", "FMS: line select 5l", "FMS Ls5l", XPlaneCommands.FMSLs5L); } }
        private XPlaneCommand FMSLs6L { get { return new XPlaneCommand("sim/FMS/ls_6l", "FMS: line select 6l", "FMS Ls6l", XPlaneCommands.FMSLs6L); } }
        private XPlaneCommand FMSLs1R { get { return new XPlaneCommand("sim/FMS/ls_1r", "FMS: line select 1r", "FMS Ls1r", XPlaneCommands.FMSLs1R); } }
        private XPlaneCommand FMSLs2R { get { return new XPlaneCommand("sim/FMS/ls_2r", "FMS: line select 2r", "FMS Ls2r", XPlaneCommands.FMSLs2R); } }
        private XPlaneCommand FMSLs3R { get { return new XPlaneCommand("sim/FMS/ls_3r", "FMS: line select 3r", "FMS Ls3r", XPlaneCommands.FMSLs3R); } }
        private XPlaneCommand FMSLs4R { get { return new XPlaneCommand("sim/FMS/ls_4r", "FMS: line select 4r", "FMS Ls4r", XPlaneCommands.FMSLs4R); } }
        private XPlaneCommand FMSLs5R { get { return new XPlaneCommand("sim/FMS/ls_5r", "FMS: line select 5r", "FMS Ls5r", XPlaneCommands.FMSLs5R); } }
        private XPlaneCommand FMSLs6R { get { return new XPlaneCommand("sim/FMS/ls_6r", "FMS: line select 6r", "FMS Ls6r", XPlaneCommands.FMSLs6R); } }
        private XPlaneCommand FMSIndex { get { return new XPlaneCommand("sim/FMS/index", "FMS: INDEX", "FMS Index", XPlaneCommands.FMSIndex); } }
        private XPlaneCommand FMSFpln { get { return new XPlaneCommand("sim/FMS/fpln", "FMS: FPLN", "FMS Fpln", XPlaneCommands.FMSFpln); } }
        private XPlaneCommand FMSClb { get { return new XPlaneCommand("sim/FMS/clb", "FMS: CLB", "FMS Clb", XPlaneCommands.FMSClb); } }
        private XPlaneCommand FMSCrz { get { return new XPlaneCommand("sim/FMS/crz", "FMS: CRZ", "FMS Crz", XPlaneCommands.FMSCrz); } }
        private XPlaneCommand FMSDes { get { return new XPlaneCommand("sim/FMS/des", "FMS: DES", "FMS Des", XPlaneCommands.FMSDes); } }
        private XPlaneCommand FMSDirIntc { get { return new XPlaneCommand("sim/FMS/dir_intc", "FMS: DIR/INTC", "FMS Dir Intc", XPlaneCommands.FMSDirIntc); } }
        private XPlaneCommand FMSLegs { get { return new XPlaneCommand("sim/FMS/legs", "FMS: FPLN", "FMS Legs", XPlaneCommands.FMSLegs); } }
        private XPlaneCommand FMSDepArr { get { return new XPlaneCommand("sim/FMS/dep_arr", "FMS: DEP/ARR", "FMS Dep Arr", XPlaneCommands.FMSDepArr); } }
        private XPlaneCommand FMSHold { get { return new XPlaneCommand("sim/FMS/hold", "FMS: HOLD", "FMS Hold", XPlaneCommands.FMSHold); } }
        private XPlaneCommand FMSProg { get { return new XPlaneCommand("sim/FMS/prog", "FMS: PROG", "FMS Prog", XPlaneCommands.FMSProg); } }
        private XPlaneCommand FMSExec { get { return new XPlaneCommand("sim/FMS/exec", "FMS: EXEC", "FMS Exec", XPlaneCommands.FMSExec); } }
        private XPlaneCommand FMSFix { get { return new XPlaneCommand("sim/FMS/fix", "FMS: FIX", "FMS Fix", XPlaneCommands.FMSFix); } }
        private XPlaneCommand FMSNavrad { get { return new XPlaneCommand("sim/FMS/navrad", "FMS: NAVRAD", "FMS Navrad", XPlaneCommands.FMSNavrad); } }
        private XPlaneCommand FMSInit { get { return new XPlaneCommand("sim/FMS/init", "FMS: init", "FMS Init", XPlaneCommands.FMSInit); } }
        private XPlaneCommand FMSPrev { get { return new XPlaneCommand("sim/FMS/prev", "FMS: prev", "FMS Prev", XPlaneCommands.FMSPrev); } }
        private XPlaneCommand FMSNext { get { return new XPlaneCommand("sim/FMS/next", "FMS: next", "FMS Next", XPlaneCommands.FMSNext); } }
        private XPlaneCommand FMSClear { get { return new XPlaneCommand("sim/FMS/clear", "FMS: clear", "FMS Clear", XPlaneCommands.FMSClear); } }
        private XPlaneCommand FMSDirect { get { return new XPlaneCommand("sim/FMS/direct", "FMS: direct", "FMS Direct", XPlaneCommands.FMSDirect); } }
        private XPlaneCommand FMSSign { get { return new XPlaneCommand("sim/FMS/sign", "FMS: sign", "FMS Sign", XPlaneCommands.FMSSign); } }
        private XPlaneCommand FMSTypeApt { get { return new XPlaneCommand("sim/FMS/type_apt", "FMS: apt", "FMS Type Apt", XPlaneCommands.FMSTypeApt); } }
        private XPlaneCommand FMSTypeVor { get { return new XPlaneCommand("sim/FMS/type_vor", "FMS: vor", "FMS Type Vor", XPlaneCommands.FMSTypeVor); } }
        private XPlaneCommand FMSTypeNdb { get { return new XPlaneCommand("sim/FMS/type_ndb", "FMS: ndb", "FMS Type Ndb", XPlaneCommands.FMSTypeNdb); } }
        private XPlaneCommand FMSTypeFix { get { return new XPlaneCommand("sim/FMS/type_fix", "FMS: fix", "FMS Type Fix", XPlaneCommands.FMSTypeFix); } }
        private XPlaneCommand FMSTypeLatlon { get { return new XPlaneCommand("sim/FMS/type_latlon", "FMS: lat/lon", "FMS Type Latlon", XPlaneCommands.FMSTypeLatlon); } }
        private XPlaneCommand FMSKey0 { get { return new XPlaneCommand("sim/FMS/key_0", "FMS: key_0", "FMS Key0", XPlaneCommands.FMSKey0); } }
        private XPlaneCommand FMSKey1 { get { return new XPlaneCommand("sim/FMS/key_1", "FMS: key_1", "FMS Key1", XPlaneCommands.FMSKey1); } }
        private XPlaneCommand FMSKey2 { get { return new XPlaneCommand("sim/FMS/key_2", "FMS: key_2", "FMS Key2", XPlaneCommands.FMSKey2); } }
        private XPlaneCommand FMSKey3 { get { return new XPlaneCommand("sim/FMS/key_3", "FMS: key_3", "FMS Key3", XPlaneCommands.FMSKey3); } }
        private XPlaneCommand FMSKey4 { get { return new XPlaneCommand("sim/FMS/key_4", "FMS: key_4", "FMS Key4", XPlaneCommands.FMSKey4); } }
        private XPlaneCommand FMSKey5 { get { return new XPlaneCommand("sim/FMS/key_5", "FMS: key_5", "FMS Key5", XPlaneCommands.FMSKey5); } }
        private XPlaneCommand FMSKey6 { get { return new XPlaneCommand("sim/FMS/key_6", "FMS: key_6", "FMS Key6", XPlaneCommands.FMSKey6); } }
        private XPlaneCommand FMSKey7 { get { return new XPlaneCommand("sim/FMS/key_7", "FMS: key_7", "FMS Key7", XPlaneCommands.FMSKey7); } }
        private XPlaneCommand FMSKey8 { get { return new XPlaneCommand("sim/FMS/key_8", "FMS: key_8", "FMS Key8", XPlaneCommands.FMSKey8); } }
        private XPlaneCommand FMSKey9 { get { return new XPlaneCommand("sim/FMS/key_9", "FMS: key_9", "FMS Key9", XPlaneCommands.FMSKey9); } }
        private XPlaneCommand FMSKeyA { get { return new XPlaneCommand("sim/FMS/key_A", "FMS: key_A", "FMS Key A", XPlaneCommands.FMSKeyA); } }
        private XPlaneCommand FMSKeyB { get { return new XPlaneCommand("sim/FMS/key_B", "FMS: key_B", "FMS Key B", XPlaneCommands.FMSKeyB); } }
        private XPlaneCommand FMSKeyC { get { return new XPlaneCommand("sim/FMS/key_C", "FMS: key_C", "FMS Key C", XPlaneCommands.FMSKeyC); } }
        private XPlaneCommand FMSKeyD { get { return new XPlaneCommand("sim/FMS/key_D", "FMS: key_D", "FMS Key D", XPlaneCommands.FMSKeyD); } }
        private XPlaneCommand FMSKeyE { get { return new XPlaneCommand("sim/FMS/key_E", "FMS: key_E", "FMS Key E", XPlaneCommands.FMSKeyE); } }
        private XPlaneCommand FMSKeyF { get { return new XPlaneCommand("sim/FMS/key_F", "FMS: key_F", "FMS Key F", XPlaneCommands.FMSKeyF); } }
        private XPlaneCommand FMSKeyG { get { return new XPlaneCommand("sim/FMS/key_G", "FMS: key_G", "FMS Key G", XPlaneCommands.FMSKeyG); } }
        private XPlaneCommand FMSKeyH { get { return new XPlaneCommand("sim/FMS/key_H", "FMS: key_H", "FMS Key H", XPlaneCommands.FMSKeyH); } }
        private XPlaneCommand FMSKeyI { get { return new XPlaneCommand("sim/FMS/key_I", "FMS: key_I", "FMS Key I", XPlaneCommands.FMSKeyI); } }
        private XPlaneCommand FMSKeyJ { get { return new XPlaneCommand("sim/FMS/key_J", "FMS: key_J", "FMS Key J", XPlaneCommands.FMSKeyJ); } }
        private XPlaneCommand FMSKeyK { get { return new XPlaneCommand("sim/FMS/key_K", "FMS: key_K", "FMS Key K", XPlaneCommands.FMSKeyK); } }
        private XPlaneCommand FMSKeyL { get { return new XPlaneCommand("sim/FMS/key_L", "FMS: key_L", "FMS Key L", XPlaneCommands.FMSKeyL); } }
        private XPlaneCommand FMSKeyM { get { return new XPlaneCommand("sim/FMS/key_M", "FMS: key_M", "FMS Key M", XPlaneCommands.FMSKeyM); } }
        private XPlaneCommand FMSKeyN { get { return new XPlaneCommand("sim/FMS/key_N", "FMS: key_N", "FMS Key N", XPlaneCommands.FMSKeyN); } }
        private XPlaneCommand FMSKeyO { get { return new XPlaneCommand("sim/FMS/key_O", "FMS: key_O", "FMS Key O", XPlaneCommands.FMSKeyO); } }
        private XPlaneCommand FMSKeyP { get { return new XPlaneCommand("sim/FMS/key_P", "FMS: key_P", "FMS Key P", XPlaneCommands.FMSKeyP); } }
        private XPlaneCommand FMSKeyQ { get { return new XPlaneCommand("sim/FMS/key_Q", "FMS: key_Q", "FMS Key Q", XPlaneCommands.FMSKeyQ); } }
        private XPlaneCommand FMSKeyR { get { return new XPlaneCommand("sim/FMS/key_R", "FMS: key_R", "FMS Key R", XPlaneCommands.FMSKeyR); } }
        private XPlaneCommand FMSKeyS { get { return new XPlaneCommand("sim/FMS/key_S", "FMS: key_S", "FMS Key S", XPlaneCommands.FMSKeyS); } }
        private XPlaneCommand FMSKeyT { get { return new XPlaneCommand("sim/FMS/key_T", "FMS: key_T", "FMS Key T", XPlaneCommands.FMSKeyT); } }
        private XPlaneCommand FMSKeyU { get { return new XPlaneCommand("sim/FMS/key_U", "FMS: key_U", "FMS Key U", XPlaneCommands.FMSKeyU); } }
        private XPlaneCommand FMSKeyV { get { return new XPlaneCommand("sim/FMS/key_V", "FMS: key_V", "FMS Key V", XPlaneCommands.FMSKeyV); } }
        private XPlaneCommand FMSKeyW { get { return new XPlaneCommand("sim/FMS/key_W", "FMS: key_W", "FMS Key W", XPlaneCommands.FMSKeyW); } }
        private XPlaneCommand FMSKeyX { get { return new XPlaneCommand("sim/FMS/key_X", "FMS: key_X", "FMS Key X", XPlaneCommands.FMSKeyX); } }
        private XPlaneCommand FMSKeyY { get { return new XPlaneCommand("sim/FMS/key_Y", "FMS: key_Y", "FMS Key Y", XPlaneCommands.FMSKeyY); } }
        private XPlaneCommand FMSKeyZ { get { return new XPlaneCommand("sim/FMS/key_Z", "FMS: key_Z", "FMS Key Z", XPlaneCommands.FMSKeyZ); } }
        private XPlaneCommand FMSKeyPeriod { get { return new XPlaneCommand("sim/FMS/key_period", "FMS: key_period", "FMS Key Period", XPlaneCommands.FMSKeyPeriod); } }
        private XPlaneCommand FMSKeyMinus { get { return new XPlaneCommand("sim/FMS/key_minus", "FMS: key_minus", "FMS Key Minus", XPlaneCommands.FMSKeyMinus); } }
        private XPlaneCommand FMSKeySlash { get { return new XPlaneCommand("sim/FMS/key_slash", "FMS: key_slash", "FMS Key Slash", XPlaneCommands.FMSKeySlash); } }
        private XPlaneCommand FMSKeyBack { get { return new XPlaneCommand("sim/FMS/key_back", "FMS: key_back", "FMS Key Back", XPlaneCommands.FMSKeyBack); } }
        private XPlaneCommand FMSKeySpace { get { return new XPlaneCommand("sim/FMS/key_space", "FMS: key_space", "FMS Key Space", XPlaneCommands.FMSKeySpace); } }
        private XPlaneCommand FMSKeyLoad { get { return new XPlaneCommand("sim/FMS/key_load", "FMS: key_load", "FMS Key Load", XPlaneCommands.FMSKeyLoad); } }
        private XPlaneCommand FMSKeySave { get { return new XPlaneCommand("sim/FMS/key_save", "FMS: key_save", "FMS Key Save", XPlaneCommands.FMSKeySave); } }
        private XPlaneCommand FMSKeyDelete { get { return new XPlaneCommand("sim/FMS/key_delete", "FMS: key_delete", "FMS Key Delete", XPlaneCommands.FMSKeyDelete); } }
        private XPlaneCommand FMSKeyClear { get { return new XPlaneCommand("sim/FMS/key_clear", "FMS: key_clear", "FMS Key Clear", XPlaneCommands.FMSKeyClear); } }
        private XPlaneCommand FMSCDUPopup { get { return new XPlaneCommand("sim/FMS/CDU_popup", "FMS: CDU popup", "FMSCDU Popup", XPlaneCommands.FMSCDUPopup); } }
        private XPlaneCommand FMSCDUPopout { get { return new XPlaneCommand("sim/FMS/CDU_popout", "FMS: pop out CDU window", "FMSCDU Popout", XPlaneCommands.FMSCDUPopout); } }
        private XPlaneCommand FMSFixNext { get { return new XPlaneCommand("sim/FMS/fix_next", "FMS: next fix", "FMS Fix Next", XPlaneCommands.FMSFixNext); } }
        private XPlaneCommand FMSFixPrev { get { return new XPlaneCommand("sim/FMS/fix_prev", "FMS: prev fix", "FMS Fix Prev", XPlaneCommands.FMSFixPrev); } }
        private XPlaneCommand FMS2Ls1L { get { return new XPlaneCommand("sim/FMS2/ls_1l", "FMS2: line select 1l", "Fms2ls1l", XPlaneCommands.FMS2Ls1L); } }
        private XPlaneCommand FMS2Ls2L { get { return new XPlaneCommand("sim/FMS2/ls_2l", "FMS2: line select 2l", "Fms2ls2l", XPlaneCommands.FMS2Ls2L); } }
        private XPlaneCommand FMS2Ls3L { get { return new XPlaneCommand("sim/FMS2/ls_3l", "FMS2: line select 3l", "Fms2ls3l", XPlaneCommands.FMS2Ls3L); } }
        private XPlaneCommand FMS2Ls4L { get { return new XPlaneCommand("sim/FMS2/ls_4l", "FMS2: line select 4l", "Fms2ls4l", XPlaneCommands.FMS2Ls4L); } }
        private XPlaneCommand FMS2Ls5L { get { return new XPlaneCommand("sim/FMS2/ls_5l", "FMS2: line select 5l", "Fms2ls5l", XPlaneCommands.FMS2Ls5L); } }
        private XPlaneCommand FMS2Ls6L { get { return new XPlaneCommand("sim/FMS2/ls_6l", "FMS2: line select 6l", "Fms2ls6l", XPlaneCommands.FMS2Ls6L); } }
        private XPlaneCommand FMS2Ls1R { get { return new XPlaneCommand("sim/FMS2/ls_1r", "FMS2: line select 1r", "Fms2ls1r", XPlaneCommands.FMS2Ls1R); } }
        private XPlaneCommand FMS2Ls2R { get { return new XPlaneCommand("sim/FMS2/ls_2r", "FMS2: line select 2r", "Fms2ls2r", XPlaneCommands.FMS2Ls2R); } }
        private XPlaneCommand FMS2Ls3R { get { return new XPlaneCommand("sim/FMS2/ls_3r", "FMS2: line select 3r", "Fms2ls3r", XPlaneCommands.FMS2Ls3R); } }
        private XPlaneCommand FMS2Ls4R { get { return new XPlaneCommand("sim/FMS2/ls_4r", "FMS2: line select 4r", "Fms2ls4r", XPlaneCommands.FMS2Ls4R); } }
        private XPlaneCommand FMS2Ls5R { get { return new XPlaneCommand("sim/FMS2/ls_5r", "FMS2: line select 5r", "Fms2ls5r", XPlaneCommands.FMS2Ls5R); } }
        private XPlaneCommand FMS2Ls6R { get { return new XPlaneCommand("sim/FMS2/ls_6r", "FMS2: line select 6r", "Fms2ls6r", XPlaneCommands.FMS2Ls6R); } }
        private XPlaneCommand FMS2Index { get { return new XPlaneCommand("sim/FMS2/index", "FMS: INDEX", "Fms2index", XPlaneCommands.FMS2Index); } }
        private XPlaneCommand FMS2Fpln { get { return new XPlaneCommand("sim/FMS2/fpln", "FMS2: FPLN", "Fms2fpln", XPlaneCommands.FMS2Fpln); } }
        private XPlaneCommand FMS2Clb { get { return new XPlaneCommand("sim/FMS2/clb", "FMS2: CLB", "Fms2clb", XPlaneCommands.FMS2Clb); } }
        private XPlaneCommand FMS2Crz { get { return new XPlaneCommand("sim/FMS2/crz", "FMS2: CRZ", "Fms2crz", XPlaneCommands.FMS2Crz); } }
        private XPlaneCommand FMS2Des { get { return new XPlaneCommand("sim/FMS2/des", "FMS2: DES", "Fms2des", XPlaneCommands.FMS2Des); } }
        private XPlaneCommand FMS2DirIntc { get { return new XPlaneCommand("sim/FMS2/dir_intc", "FMS2: DIR/INTC", "Fms2dir Intc", XPlaneCommands.FMS2DirIntc); } }
        private XPlaneCommand FMS2Legs { get { return new XPlaneCommand("sim/FMS2/legs", "FMS2: FPLN", "Fms2legs", XPlaneCommands.FMS2Legs); } }
        private XPlaneCommand FMS2DepArr { get { return new XPlaneCommand("sim/FMS2/dep_arr", "FMS2: DEP/ARR", "Fms2dep Arr", XPlaneCommands.FMS2DepArr); } }
        private XPlaneCommand FMS2Hold { get { return new XPlaneCommand("sim/FMS2/hold", "FMS2: HOLD", "Fms2hold", XPlaneCommands.FMS2Hold); } }
        private XPlaneCommand FMS2Prog { get { return new XPlaneCommand("sim/FMS2/prog", "FMS2: PROG", "Fms2prog", XPlaneCommands.FMS2Prog); } }
        private XPlaneCommand FMS2Exec { get { return new XPlaneCommand("sim/FMS2/exec", "FMS2: EXEC", "Fms2exec", XPlaneCommands.FMS2Exec); } }
        private XPlaneCommand FMS2Fix { get { return new XPlaneCommand("sim/FMS2/fix", "FMS2: FIX", "Fms2fix", XPlaneCommands.FMS2Fix); } }
        private XPlaneCommand FMS2Navrad { get { return new XPlaneCommand("sim/FMS2/navrad", "FMS2: NAVRAD", "Fms2navrad", XPlaneCommands.FMS2Navrad); } }
        private XPlaneCommand FMS2Prev { get { return new XPlaneCommand("sim/FMS2/prev", "FMS2: prev", "Fms2prev", XPlaneCommands.FMS2Prev); } }
        private XPlaneCommand FMS2Next { get { return new XPlaneCommand("sim/FMS2/next", "FMS2: next", "Fms2next", XPlaneCommands.FMS2Next); } }
        private XPlaneCommand FMS2Key0 { get { return new XPlaneCommand("sim/FMS2/key_0", "FMS2: key_0", "Fms2key0", XPlaneCommands.FMS2Key0); } }
        private XPlaneCommand FMS2Key1 { get { return new XPlaneCommand("sim/FMS2/key_1", "FMS2: key_1", "Fms2key1", XPlaneCommands.FMS2Key1); } }
        private XPlaneCommand FMS2Key2 { get { return new XPlaneCommand("sim/FMS2/key_2", "FMS2: key_2", "Fms2key2", XPlaneCommands.FMS2Key2); } }
        private XPlaneCommand FMS2Key3 { get { return new XPlaneCommand("sim/FMS2/key_3", "FMS2: key_3", "Fms2key3", XPlaneCommands.FMS2Key3); } }
        private XPlaneCommand FMS2Key4 { get { return new XPlaneCommand("sim/FMS2/key_4", "FMS2: key_4", "Fms2key4", XPlaneCommands.FMS2Key4); } }
        private XPlaneCommand FMS2Key5 { get { return new XPlaneCommand("sim/FMS2/key_5", "FMS2: key_5", "Fms2key5", XPlaneCommands.FMS2Key5); } }
        private XPlaneCommand FMS2Key6 { get { return new XPlaneCommand("sim/FMS2/key_6", "FMS2: key_6", "Fms2key6", XPlaneCommands.FMS2Key6); } }
        private XPlaneCommand FMS2Key7 { get { return new XPlaneCommand("sim/FMS2/key_7", "FMS2: key_7", "Fms2key7", XPlaneCommands.FMS2Key7); } }
        private XPlaneCommand FMS2Key8 { get { return new XPlaneCommand("sim/FMS2/key_8", "FMS2: key_8", "Fms2key8", XPlaneCommands.FMS2Key8); } }
        private XPlaneCommand FMS2Key9 { get { return new XPlaneCommand("sim/FMS2/key_9", "FMS2: key_9", "Fms2key9", XPlaneCommands.FMS2Key9); } }
        private XPlaneCommand FMS2KeyA { get { return new XPlaneCommand("sim/FMS2/key_A", "FMS2: key_A", "Fms2key A", XPlaneCommands.FMS2KeyA); } }
        private XPlaneCommand FMS2KeyB { get { return new XPlaneCommand("sim/FMS2/key_B", "FMS2: key_B", "Fms2key B", XPlaneCommands.FMS2KeyB); } }
        private XPlaneCommand FMS2KeyC { get { return new XPlaneCommand("sim/FMS2/key_C", "FMS2: key_C", "Fms2key C", XPlaneCommands.FMS2KeyC); } }
        private XPlaneCommand FMS2KeyD { get { return new XPlaneCommand("sim/FMS2/key_D", "FMS2: key_D", "Fms2key D", XPlaneCommands.FMS2KeyD); } }
        private XPlaneCommand FMS2KeyE { get { return new XPlaneCommand("sim/FMS2/key_E", "FMS2: key_E", "Fms2key E", XPlaneCommands.FMS2KeyE); } }
        private XPlaneCommand FMS2KeyF { get { return new XPlaneCommand("sim/FMS2/key_F", "FMS2: key_F", "Fms2key F", XPlaneCommands.FMS2KeyF); } }
        private XPlaneCommand FMS2KeyG { get { return new XPlaneCommand("sim/FMS2/key_G", "FMS2: key_G", "Fms2key G", XPlaneCommands.FMS2KeyG); } }
        private XPlaneCommand FMS2KeyH { get { return new XPlaneCommand("sim/FMS2/key_H", "FMS2: key_H", "Fms2key H", XPlaneCommands.FMS2KeyH); } }
        private XPlaneCommand FMS2KeyI { get { return new XPlaneCommand("sim/FMS2/key_I", "FMS2: key_I", "Fms2key I", XPlaneCommands.FMS2KeyI); } }
        private XPlaneCommand FMS2KeyJ { get { return new XPlaneCommand("sim/FMS2/key_J", "FMS2: key_J", "Fms2key J", XPlaneCommands.FMS2KeyJ); } }
        private XPlaneCommand FMS2KeyK { get { return new XPlaneCommand("sim/FMS2/key_K", "FMS2: key_K", "Fms2key K", XPlaneCommands.FMS2KeyK); } }
        private XPlaneCommand FMS2KeyL { get { return new XPlaneCommand("sim/FMS2/key_L", "FMS2: key_L", "Fms2key L", XPlaneCommands.FMS2KeyL); } }
        private XPlaneCommand FMS2KeyM { get { return new XPlaneCommand("sim/FMS2/key_M", "FMS2: key_M", "Fms2key M", XPlaneCommands.FMS2KeyM); } }
        private XPlaneCommand FMS2KeyN { get { return new XPlaneCommand("sim/FMS2/key_N", "FMS2: key_N", "Fms2key N", XPlaneCommands.FMS2KeyN); } }
        private XPlaneCommand FMS2KeyO { get { return new XPlaneCommand("sim/FMS2/key_O", "FMS2: key_O", "Fms2key O", XPlaneCommands.FMS2KeyO); } }
        private XPlaneCommand FMS2KeyP { get { return new XPlaneCommand("sim/FMS2/key_P", "FMS2: key_P", "Fms2key P", XPlaneCommands.FMS2KeyP); } }
        private XPlaneCommand FMS2KeyQ { get { return new XPlaneCommand("sim/FMS2/key_Q", "FMS2: key_Q", "Fms2key Q", XPlaneCommands.FMS2KeyQ); } }
        private XPlaneCommand FMS2KeyR { get { return new XPlaneCommand("sim/FMS2/key_R", "FMS2: key_R", "Fms2key R", XPlaneCommands.FMS2KeyR); } }
        private XPlaneCommand FMS2KeyS { get { return new XPlaneCommand("sim/FMS2/key_S", "FMS2: key_S", "Fms2key S", XPlaneCommands.FMS2KeyS); } }
        private XPlaneCommand FMS2KeyT { get { return new XPlaneCommand("sim/FMS2/key_T", "FMS2: key_T", "Fms2key T", XPlaneCommands.FMS2KeyT); } }
        private XPlaneCommand FMS2KeyU { get { return new XPlaneCommand("sim/FMS2/key_U", "FMS2: key_U", "Fms2key U", XPlaneCommands.FMS2KeyU); } }
        private XPlaneCommand FMS2KeyV { get { return new XPlaneCommand("sim/FMS2/key_V", "FMS2: key_V", "Fms2key V", XPlaneCommands.FMS2KeyV); } }
        private XPlaneCommand FMS2KeyW { get { return new XPlaneCommand("sim/FMS2/key_W", "FMS2: key_W", "Fms2key W", XPlaneCommands.FMS2KeyW); } }
        private XPlaneCommand FMS2KeyX { get { return new XPlaneCommand("sim/FMS2/key_X", "FMS2: key_X", "Fms2key X", XPlaneCommands.FMS2KeyX); } }
        private XPlaneCommand FMS2KeyY { get { return new XPlaneCommand("sim/FMS2/key_Y", "FMS2: key_Y", "Fms2key Y", XPlaneCommands.FMS2KeyY); } }
        private XPlaneCommand FMS2KeyZ { get { return new XPlaneCommand("sim/FMS2/key_Z", "FMS2: key_Z", "Fms2key Z", XPlaneCommands.FMS2KeyZ); } }
        private XPlaneCommand FMS2KeyPeriod { get { return new XPlaneCommand("sim/FMS2/key_period", "FMS2: key_period", "Fms2key Period", XPlaneCommands.FMS2KeyPeriod); } }
        private XPlaneCommand FMS2KeyMinus { get { return new XPlaneCommand("sim/FMS2/key_minus", "FMS2: key_minus", "Fms2key Minus", XPlaneCommands.FMS2KeyMinus); } }
        private XPlaneCommand FMS2KeySlash { get { return new XPlaneCommand("sim/FMS2/key_slash", "FMS2: key_slash", "Fms2key Slash", XPlaneCommands.FMS2KeySlash); } }
        private XPlaneCommand FMS2KeyBack { get { return new XPlaneCommand("sim/FMS2/key_back", "FMS2: key_back", "Fms2key Back", XPlaneCommands.FMS2KeyBack); } }
        private XPlaneCommand FMS2KeySpace { get { return new XPlaneCommand("sim/FMS2/key_space", "FMS2: key_space", "Fms2key Space", XPlaneCommands.FMS2KeySpace); } }
        private XPlaneCommand FMS2KeyDelete { get { return new XPlaneCommand("sim/FMS2/key_delete", "FMS2: key_delete", "Fms2key Delete", XPlaneCommands.FMS2KeyDelete); } }
        private XPlaneCommand FMS2KeyClear { get { return new XPlaneCommand("sim/FMS2/key_clear", "FMS2: key_clear", "Fms2key Clear", XPlaneCommands.FMS2KeyClear); } }
        private XPlaneCommand FMS2CDUPopout { get { return new XPlaneCommand("sim/FMS2/CDU_popout", "FMS2: pop out CDU window", "FMS2CDU Popout", XPlaneCommands.FMS2CDUPopout); } }
        private XPlaneCommand FMS2CDUPopup { get { return new XPlaneCommand("sim/FMS2/CDU_popup", "FMS2: CDU popup", "FMS2CDU Popup", XPlaneCommands.FMS2CDUPopup); } }
        private XPlaneCommand AnnunciatorGearWarningMute { get { return new XPlaneCommand("sim/annunciator/gear_warning_mute", "Mute gear warning horn.", "Annunciator Gear Warning Mute", XPlaneCommands.AnnunciatorGearWarningMute); } }
        private XPlaneCommand AnnunciatorMarkerBeaconMute { get { return new XPlaneCommand("sim/annunciator/marker_beacon_mute", "Mute marker beacons until next marker is received.", "Annunciator Marker Beacon Mute", XPlaneCommands.AnnunciatorMarkerBeaconMute); } }
        private XPlaneCommand AnnunciatorMarkerBeaconMuteOrOff { get { return new XPlaneCommand("sim/annunciator/marker_beacon_mute_or_off", "Mute marker beacons until next marker is received or indefinitely if none is received right now.", "Annunciator Marker Beacon Mute Or Off", XPlaneCommands.AnnunciatorMarkerBeaconMuteOrOff); } }
        private XPlaneCommand AnnunciatorMarkerBeaconSensHi { get { return new XPlaneCommand("sim/annunciator/marker_beacon_sens_hi", "Marker beacon receiver hi sens.", "Annunciator Marker Beacon Sens Hi", XPlaneCommands.AnnunciatorMarkerBeaconSensHi); } }
        private XPlaneCommand AnnunciatorMarkerBeaconSensLo { get { return new XPlaneCommand("sim/annunciator/marker_beacon_sens_lo", "Marker beacon receiver lo sens.", "Annunciator Marker Beacon Sens Lo", XPlaneCommands.AnnunciatorMarkerBeaconSensLo); } }
        private XPlaneCommand AnnunciatorMarkerBeaconSensToggle { get { return new XPlaneCommand("sim/annunciator/marker_beacon_sens_toggle", "Marker beacon receiver sens toggle.", "Annunciator Marker Beacon Sens Toggle", XPlaneCommands.AnnunciatorMarkerBeaconSensToggle); } }
        private XPlaneCommand SystemsPreRotateToggle { get { return new XPlaneCommand("sim/systems/pre_rotate_toggle", "Toggle pre-rotate.", "Systems Pre Rotate Toggle", XPlaneCommands.SystemsPreRotateToggle); } }
        private XPlaneCommand FlightControlsPumpFlaps { get { return new XPlaneCommand("sim/flight_controls/pump_flaps", "Pump flaps up/down.", "Flight Controls Pump Flaps", XPlaneCommands.FlightControlsPumpFlaps); } }
        private XPlaneCommand FlightControlsPumpGear { get { return new XPlaneCommand("sim/flight_controls/pump_gear", "Pump gear up/down.", "Flight Controls Pump Gear", XPlaneCommands.FlightControlsPumpGear); } }
        private XPlaneCommand GPSModeAirport { get { return new XPlaneCommand("sim/GPS/mode_airport", "GPS mode: airports.", "GPS Mode Airport", XPlaneCommands.GPSModeAirport); } }
        private XPlaneCommand GPSModeVOR { get { return new XPlaneCommand("sim/GPS/mode_VOR", "GPS mode: VORs.", "GPS Mode VOR", XPlaneCommands.GPSModeVOR); } }
        private XPlaneCommand GPSModeNDB { get { return new XPlaneCommand("sim/GPS/mode_NDB", "GPS mode: NDBs.", "GPS Mode NDB", XPlaneCommands.GPSModeNDB); } }
        private XPlaneCommand GPSModeWaypoint { get { return new XPlaneCommand("sim/GPS/mode_waypoint", "GPS mode: waypoints.", "GPS Mode Waypoint", XPlaneCommands.GPSModeWaypoint); } }
        private XPlaneCommand GPSFineSelectDown { get { return new XPlaneCommand("sim/GPS/fine_select_down", "GPS fine select down.", "GPS Fine Select Down", XPlaneCommands.GPSFineSelectDown); } }
        private XPlaneCommand GPSFineSelectUp { get { return new XPlaneCommand("sim/GPS/fine_select_up", "GPS fine select up.", "GPS Fine Select Up", XPlaneCommands.GPSFineSelectUp); } }
        private XPlaneCommand GPSCoarseSelectDown { get { return new XPlaneCommand("sim/GPS/coarse_select_down", "GPS coarse select down.", "GPS Coarse Select Down", XPlaneCommands.GPSCoarseSelectDown); } }
        private XPlaneCommand GPSCoarseSelectUp { get { return new XPlaneCommand("sim/GPS/coarse_select_up", "GPS coarse select up.", "GPS Coarse Select Up", XPlaneCommands.GPSCoarseSelectUp); } }
        private XPlaneCommand GPSG430n1CoarseDown { get { return new XPlaneCommand("sim/GPS/g430n1_coarse_down", "GNS COM/NAV 1 coarse down.", "GPSg430n1coarse Down", XPlaneCommands.GPSG430n1CoarseDown); } }
        private XPlaneCommand GPSG430n1CoarseUp { get { return new XPlaneCommand("sim/GPS/g430n1_coarse_up", "GNS COM/NAV 1 coarse up.", "GPSg430n1coarse Up", XPlaneCommands.GPSG430n1CoarseUp); } }
        private XPlaneCommand GPSG430n1FineDown { get { return new XPlaneCommand("sim/GPS/g430n1_fine_down", "GNS COM/NAV 1 fine down.", "GPSg430n1fine Down", XPlaneCommands.GPSG430n1FineDown); } }
        private XPlaneCommand GPSG430n1FineUp { get { return new XPlaneCommand("sim/GPS/g430n1_fine_up", "GNS COM/NAV 1 fine up.", "GPSg430n1fine Up", XPlaneCommands.GPSG430n1FineUp); } }
        private XPlaneCommand GPSG430n1ChapterUp { get { return new XPlaneCommand("sim/GPS/g430n1_chapter_up", "GNS NAV 1 chapter up.", "GPSg430n1chapter Up", XPlaneCommands.GPSG430n1ChapterUp); } }
        private XPlaneCommand GPSG430n1ChapterDn { get { return new XPlaneCommand("sim/GPS/g430n1_chapter_dn", "GNS NAV 1 chapter dn.", "GPSg430n1chapter Dn", XPlaneCommands.GPSG430n1ChapterDn); } }
        private XPlaneCommand GPSG430n1PageUp { get { return new XPlaneCommand("sim/GPS/g430n1_page_up", "GNS NAV 1 page up.", "GPSg430n1page Up", XPlaneCommands.GPSG430n1PageUp); } }
        private XPlaneCommand GPSG430n1PageDn { get { return new XPlaneCommand("sim/GPS/g430n1_page_dn", "GNS NAV 1 page dn.", "GPSg430n1page Dn", XPlaneCommands.GPSG430n1PageDn); } }
        private XPlaneCommand GPSG430n1ZoomIn { get { return new XPlaneCommand("sim/GPS/g430n1_zoom_in", "GNS NAV 1 zoom in.", "GPSg430n1zoom In", XPlaneCommands.GPSG430n1ZoomIn); } }
        private XPlaneCommand GPSG430n1ZoomOut { get { return new XPlaneCommand("sim/GPS/g430n1_zoom_out", "GNS NAV 1 zoom out.", "GPSg430n1zoom Out", XPlaneCommands.GPSG430n1ZoomOut); } }
        private XPlaneCommand GPSG430n1NavComTog { get { return new XPlaneCommand("sim/GPS/g430n1_nav_com_tog", "GNS NAV 1 NAV COM toggle.", "GPSg430n1nav Com Tog", XPlaneCommands.GPSG430n1NavComTog); } }
        private XPlaneCommand GPSG430n1Cdi { get { return new XPlaneCommand("sim/GPS/g430n1_cdi", "GNS NAV 1 CDI.", "GPSg430n1cdi", XPlaneCommands.GPSG430n1Cdi); } }
        private XPlaneCommand GPSG430n1Obs { get { return new XPlaneCommand("sim/GPS/g430n1_obs", "GNS NAV 1 OBS.", "GPSg430n1obs", XPlaneCommands.GPSG430n1Obs); } }
        private XPlaneCommand GPSG430n1Msg { get { return new XPlaneCommand("sim/GPS/g430n1_msg", "GNS NAV 1 MSG.", "GPSg430n1msg", XPlaneCommands.GPSG430n1Msg); } }
        private XPlaneCommand GPSG430n1Fpl { get { return new XPlaneCommand("sim/GPS/g430n1_fpl", "GNS NAV 1 FPL.", "GPSg430n1fpl", XPlaneCommands.GPSG430n1Fpl); } }
        private XPlaneCommand GPSG430n1Proc { get { return new XPlaneCommand("sim/GPS/g430n1_proc", "GNS NAV 1 PROC.", "GPSg430n1proc", XPlaneCommands.GPSG430n1Proc); } }
        private XPlaneCommand GPSG430n1Vnav { get { return new XPlaneCommand("sim/GPS/g430n1_vnav", "GNS NAV 1 VNAV.", "GPSg430n1vnav", XPlaneCommands.GPSG430n1Vnav); } }
        private XPlaneCommand GPSG430n1Direct { get { return new XPlaneCommand("sim/GPS/g430n1_direct", "GNS NAV 1 Direct.", "GPSg430n1direct", XPlaneCommands.GPSG430n1Direct); } }
        private XPlaneCommand GPSG430n1Menu { get { return new XPlaneCommand("sim/GPS/g430n1_menu", "GNS NAV 1 Menu.", "GPSg430n1menu", XPlaneCommands.GPSG430n1Menu); } }
        private XPlaneCommand GPSG430n1Clr { get { return new XPlaneCommand("sim/GPS/g430n1_clr", "GNS NAV 1 CLR.", "GPSg430n1clr", XPlaneCommands.GPSG430n1Clr); } }
        private XPlaneCommand GPSG430n1Ent { get { return new XPlaneCommand("sim/GPS/g430n1_ent", "GNS NAV 1 ENT.", "GPSg430n1ent", XPlaneCommands.GPSG430n1Ent); } }
        private XPlaneCommand GPSG430n1ComFf { get { return new XPlaneCommand("sim/GPS/g430n1_com_ff", "GNS NAV 1 COM flip flop.", "GPSg430n1com Ff", XPlaneCommands.GPSG430n1ComFf); } }
        private XPlaneCommand GPSG430n1NavFf { get { return new XPlaneCommand("sim/GPS/g430n1_nav_ff", "GNS NAV 1 NAV flip flop.", "GPSg430n1nav Ff", XPlaneCommands.GPSG430n1NavFf); } }
        private XPlaneCommand GPSG430n1Cursor { get { return new XPlaneCommand("sim/GPS/g430n1_cursor", "GNS NAV 1 push cursor.", "GPSg430n1cursor", XPlaneCommands.GPSG430n1Cursor); } }
        private XPlaneCommand GPSG430n1Popout { get { return new XPlaneCommand("sim/GPS/g430n1_popout", "GNS NAV 1 pop out window.", "GPSg430n1popout", XPlaneCommands.GPSG430n1Popout); } }
        private XPlaneCommand GPSG430n1Popup { get { return new XPlaneCommand("sim/GPS/g430n1_popup", "GNS NAV 1 toggle popup.", "GPSg430n1popup", XPlaneCommands.GPSG430n1Popup); } }
        private XPlaneCommand GPSG430n1Cvol { get { return new XPlaneCommand("sim/GPS/g430n1_cvol", "GNS NAV 1 COM audio.", "GPSg430n1cvol", XPlaneCommands.GPSG430n1Cvol); } }
        private XPlaneCommand GPSG430n1Vvol { get { return new XPlaneCommand("sim/GPS/g430n1_vvol", "GNS NAV 1 NAV ID", "GPSg430n1vvol", XPlaneCommands.GPSG430n1Vvol); } }
        private XPlaneCommand GPSG430n1CvolUp { get { return new XPlaneCommand("sim/GPS/g430n1_cvol_up", "GNS NAV 1 COM audio volume up.", "GPSg430n1cvol Up", XPlaneCommands.GPSG430n1CvolUp); } }
        private XPlaneCommand GPSG430n1CvolDn { get { return new XPlaneCommand("sim/GPS/g430n1_cvol_dn", "GNS NAV 1 COM audio volume down.", "GPSg430n1cvol Dn", XPlaneCommands.GPSG430n1CvolDn); } }
        private XPlaneCommand GPSG430n1VvolUp { get { return new XPlaneCommand("sim/GPS/g430n1_vvol_up", "GNS NAV 1 NAV audio volume up.", "GPSg430n1vvol Up", XPlaneCommands.GPSG430n1VvolUp); } }
        private XPlaneCommand GPSG430n1VvolDn { get { return new XPlaneCommand("sim/GPS/g430n1_vvol_dn", "GNS NAV 1 NAV audio volume down.", "GPSg430n1vvol Dn", XPlaneCommands.GPSG430n1VvolDn); } }
        private XPlaneCommand GPSG430n2CoarseDown { get { return new XPlaneCommand("sim/GPS/g430n2_coarse_down", "GNS COM/NAV 2 coarse down.", "GPSg430n2coarse Down", XPlaneCommands.GPSG430n2CoarseDown); } }
        private XPlaneCommand GPSG430n2CoarseUp { get { return new XPlaneCommand("sim/GPS/g430n2_coarse_up", "GNS COM/NAV 2 coarse up.", "GPSg430n2coarse Up", XPlaneCommands.GPSG430n2CoarseUp); } }
        private XPlaneCommand GPSG430n2FineDown { get { return new XPlaneCommand("sim/GPS/g430n2_fine_down", "GNS COM/NAV 2 fine down.", "GPSg430n2fine Down", XPlaneCommands.GPSG430n2FineDown); } }
        private XPlaneCommand GPSG430n2FineUp { get { return new XPlaneCommand("sim/GPS/g430n2_fine_up", "GNS COM/NAV 2 fine up.", "GPSg430n2fine Up", XPlaneCommands.GPSG430n2FineUp); } }
        private XPlaneCommand GPSG430n2ChapterUp { get { return new XPlaneCommand("sim/GPS/g430n2_chapter_up", "GNS NAV 2 chapter up.", "GPSg430n2chapter Up", XPlaneCommands.GPSG430n2ChapterUp); } }
        private XPlaneCommand GPSG430n2ChapterDn { get { return new XPlaneCommand("sim/GPS/g430n2_chapter_dn", "GNS NAV 2 chapter dn.", "GPSg430n2chapter Dn", XPlaneCommands.GPSG430n2ChapterDn); } }
        private XPlaneCommand GPSG430n2PageUp { get { return new XPlaneCommand("sim/GPS/g430n2_page_up", "GNS NAV 2 page up.", "GPSg430n2page Up", XPlaneCommands.GPSG430n2PageUp); } }
        private XPlaneCommand GPSG430n2PageDn { get { return new XPlaneCommand("sim/GPS/g430n2_page_dn", "GNS NAV 2 page dn.", "GPSg430n2page Dn", XPlaneCommands.GPSG430n2PageDn); } }
        private XPlaneCommand GPSG430n2ZoomIn { get { return new XPlaneCommand("sim/GPS/g430n2_zoom_in", "GNS NAV 2 zoom in.", "GPSg430n2zoom In", XPlaneCommands.GPSG430n2ZoomIn); } }
        private XPlaneCommand GPSG430n2ZoomOut { get { return new XPlaneCommand("sim/GPS/g430n2_zoom_out", "GNS NAV 2 zoom out.", "GPSg430n2zoom Out", XPlaneCommands.GPSG430n2ZoomOut); } }
        private XPlaneCommand GPSG430n2NavComTog { get { return new XPlaneCommand("sim/GPS/g430n2_nav_com_tog", "GNS NAV 2 NAV COM toggle.", "GPSg430n2nav Com Tog", XPlaneCommands.GPSG430n2NavComTog); } }
        private XPlaneCommand GPSG430n2Cdi { get { return new XPlaneCommand("sim/GPS/g430n2_cdi", "GNS NAV 2 CDI.", "GPSg430n2cdi", XPlaneCommands.GPSG430n2Cdi); } }
        private XPlaneCommand GPSG430n2Obs { get { return new XPlaneCommand("sim/GPS/g430n2_obs", "GNS NAV 2 OBS.", "GPSg430n2obs", XPlaneCommands.GPSG430n2Obs); } }
        private XPlaneCommand GPSG430n2Msg { get { return new XPlaneCommand("sim/GPS/g430n2_msg", "GNS NAV 2 MSG.", "GPSg430n2msg", XPlaneCommands.GPSG430n2Msg); } }
        private XPlaneCommand GPSG430n2Fpl { get { return new XPlaneCommand("sim/GPS/g430n2_fpl", "GNS NAV 2 FPL.", "GPSg430n2fpl", XPlaneCommands.GPSG430n2Fpl); } }
        private XPlaneCommand GPSG430n2Proc { get { return new XPlaneCommand("sim/GPS/g430n2_proc", "GNS NAV 2 PROC.", "GPSg430n2proc", XPlaneCommands.GPSG430n2Proc); } }
        private XPlaneCommand GPSG430n2Vnav { get { return new XPlaneCommand("sim/GPS/g430n2_vnav", "GNS NAV 2 VNAV.", "GPSg430n2vnav", XPlaneCommands.GPSG430n2Vnav); } }
        private XPlaneCommand GPSG430n2Direct { get { return new XPlaneCommand("sim/GPS/g430n2_direct", "GNS NAV 2 Direct.", "GPSg430n2direct", XPlaneCommands.GPSG430n2Direct); } }
        private XPlaneCommand GPSG430n2Menu { get { return new XPlaneCommand("sim/GPS/g430n2_menu", "GNS NAV 2 Menu.", "GPSg430n2menu", XPlaneCommands.GPSG430n2Menu); } }
        private XPlaneCommand GPSG430n2Clr { get { return new XPlaneCommand("sim/GPS/g430n2_clr", "GNS NAV 2 CLR.", "GPSg430n2clr", XPlaneCommands.GPSG430n2Clr); } }
        private XPlaneCommand GPSG430n2Ent { get { return new XPlaneCommand("sim/GPS/g430n2_ent", "GNS NAV 2 ENT.", "GPSg430n2ent", XPlaneCommands.GPSG430n2Ent); } }
        private XPlaneCommand GPSG430n2ComFf { get { return new XPlaneCommand("sim/GPS/g430n2_com_ff", "GNS NAV 2 COM FF.", "GPSg430n2com Ff", XPlaneCommands.GPSG430n2ComFf); } }
        private XPlaneCommand GPSG430n2NavFf { get { return new XPlaneCommand("sim/GPS/g430n2_nav_ff", "GNS NAV 2 NAV FF.", "GPSg430n2nav Ff", XPlaneCommands.GPSG430n2NavFf); } }
        private XPlaneCommand GPSG430n2Cursor { get { return new XPlaneCommand("sim/GPS/g430n2_cursor", "GNS NAV 2 push cursor.", "GPSg430n2cursor", XPlaneCommands.GPSG430n2Cursor); } }
        private XPlaneCommand GPSG430n2Popout { get { return new XPlaneCommand("sim/GPS/g430n2_popout", "GNS NAV 2 pop out window.", "GPSg430n2popout", XPlaneCommands.GPSG430n2Popout); } }
        private XPlaneCommand GPSG430n2Popup { get { return new XPlaneCommand("sim/GPS/g430n2_popup", "GNS NAV 2 toggle popup.", "GPSg430n2popup", XPlaneCommands.GPSG430n2Popup); } }
        private XPlaneCommand GPSG430n2Cvol { get { return new XPlaneCommand("sim/GPS/g430n2_cvol", "GNS NAV 2 COM audio.", "GPSg430n2cvol", XPlaneCommands.GPSG430n2Cvol); } }
        private XPlaneCommand GPSG430n2Vvol { get { return new XPlaneCommand("sim/GPS/g430n2_vvol", "GNS NAV 2 NAV ID", "GPSg430n2vvol", XPlaneCommands.GPSG430n2Vvol); } }
        private XPlaneCommand GPSG430n2CvolUp { get { return new XPlaneCommand("sim/GPS/g430n2_cvol_up", "GNS NAV 2 COM audio volume up.", "GPSg430n2cvol Up", XPlaneCommands.GPSG430n2CvolUp); } }
        private XPlaneCommand GPSG430n2CvolDn { get { return new XPlaneCommand("sim/GPS/g430n2_cvol_dn", "GNS NAV 2 COM audio volume down.", "GPSg430n2cvol Dn", XPlaneCommands.GPSG430n2CvolDn); } }
        private XPlaneCommand GPSG430n2VvolUp { get { return new XPlaneCommand("sim/GPS/g430n2_vvol_up", "GNS NAV 2 NAV audio volume up.", "GPSg430n2vvol Up", XPlaneCommands.GPSG430n2VvolUp); } }
        private XPlaneCommand GPSG430n2VvolDn { get { return new XPlaneCommand("sim/GPS/g430n2_vvol_dn", "GNS NAV 2 NAV audio volume down.", "GPSg430n2vvol Dn", XPlaneCommands.GPSG430n2VvolDn); } }
        private XPlaneCommand GPSG1000n1Nvol { get { return new XPlaneCommand("sim/GPS/g1000n1_nvol", "G1000 pilot NAV audio.", "GPSg1000n1nvol", XPlaneCommands.GPSG1000n1Nvol); } }
        private XPlaneCommand GPSG1000n1NvolUp { get { return new XPlaneCommand("sim/GPS/g1000n1_nvol_up", "G1000 pilot NAV audio volume up.", "GPSg1000n1nvol Up", XPlaneCommands.GPSG1000n1NvolUp); } }
        private XPlaneCommand GPSG1000n1NvolDn { get { return new XPlaneCommand("sim/GPS/g1000n1_nvol_dn", "G1000 pilot NAV audio volume down.", "GPSg1000n1nvol Dn", XPlaneCommands.GPSG1000n1NvolDn); } }
        private XPlaneCommand GPSG1000n1NavFf { get { return new XPlaneCommand("sim/GPS/g1000n1_nav_ff", "G1000 pilot NAV flip flop.", "GPSg1000n1nav Ff", XPlaneCommands.GPSG1000n1NavFf); } }
        private XPlaneCommand GPSG1000n1NavOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n1_nav_outer_up", "G1000 pilot NAV outer ring tune up.", "GPSg1000n1nav Outer Up", XPlaneCommands.GPSG1000n1NavOuterUp); } }
        private XPlaneCommand GPSG1000n1NavOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n1_nav_outer_down", "G1000 pilot NAV outer ring tune down.", "GPSg1000n1nav Outer Down", XPlaneCommands.GPSG1000n1NavOuterDown); } }
        private XPlaneCommand GPSG1000n1NavInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n1_nav_inner_up", "G1000 pilot NAV inner ring tune up.", "GPSg1000n1nav Inner Up", XPlaneCommands.GPSG1000n1NavInnerUp); } }
        private XPlaneCommand GPSG1000n1NavInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n1_nav_inner_down", "G1000 pilot NAV inner ring tune down.", "GPSg1000n1nav Inner Down", XPlaneCommands.GPSG1000n1NavInnerDown); } }
        private XPlaneCommand GPSG1000n1Nav12 { get { return new XPlaneCommand("sim/GPS/g1000n1_nav12", "G1000 pilot PFD NAV 1/2.", "GPSg1000n1nav12", XPlaneCommands.GPSG1000n1Nav12); } }
        private XPlaneCommand GPSG1000n1HdgUp { get { return new XPlaneCommand("sim/GPS/g1000n1_hdg_up", "G1000 pilot HDG up.", "GPSg1000n1hdg Up", XPlaneCommands.GPSG1000n1HdgUp); } }
        private XPlaneCommand GPSG1000n1HdgDown { get { return new XPlaneCommand("sim/GPS/g1000n1_hdg_down", "G1000 pilot HDG down.", "GPSg1000n1hdg Down", XPlaneCommands.GPSG1000n1HdgDown); } }
        private XPlaneCommand GPSG1000n1HdgSync { get { return new XPlaneCommand("sim/GPS/g1000n1_hdg_sync", "G1000 pilot HDG sync.", "GPSg1000n1hdg Sync", XPlaneCommands.GPSG1000n1HdgSync); } }
        private XPlaneCommand GPSG1000n1Ap { get { return new XPlaneCommand("sim/GPS/g1000n1_ap", "G1000 pilot autopilot.", "GPSg1000n1ap", XPlaneCommands.GPSG1000n1Ap); } }
        private XPlaneCommand GPSG1000n1Fd { get { return new XPlaneCommand("sim/GPS/g1000n1_fd", "G1000 pilot flight director.", "GPSg1000n1fd", XPlaneCommands.GPSG1000n1Fd); } }
        private XPlaneCommand GPSG1000n1Yd { get { return new XPlaneCommand("sim/GPS/g1000n1_yd", "G1000 pilot yaw damper.", "GPSg1000n1yd", XPlaneCommands.GPSG1000n1Yd); } }
        private XPlaneCommand GPSG1000n1Hdg { get { return new XPlaneCommand("sim/GPS/g1000n1_hdg", "G1000 pilot AP HDG.", "GPSg1000n1hdg", XPlaneCommands.GPSG1000n1Hdg); } }
        private XPlaneCommand GPSG1000n1Alt { get { return new XPlaneCommand("sim/GPS/g1000n1_alt", "G1000 pilot AP ALT.", "GPSg1000n1alt", XPlaneCommands.GPSG1000n1Alt); } }
        private XPlaneCommand GPSG1000n1Nav { get { return new XPlaneCommand("sim/GPS/g1000n1_nav", "G1000 pilot AP NAV.", "GPSg1000n1nav", XPlaneCommands.GPSG1000n1Nav); } }
        private XPlaneCommand GPSG1000n1Vnv { get { return new XPlaneCommand("sim/GPS/g1000n1_vnv", "G1000 pilot AP VNAV.", "GPSg1000n1vnv", XPlaneCommands.GPSG1000n1Vnv); } }
        private XPlaneCommand GPSG1000n1Apr { get { return new XPlaneCommand("sim/GPS/g1000n1_apr", "G1000 pilot AP approach.", "GPSg1000n1apr", XPlaneCommands.GPSG1000n1Apr); } }
        private XPlaneCommand GPSG1000n1Bc { get { return new XPlaneCommand("sim/GPS/g1000n1_bc", "G1000 pilot AP backcourse.", "GPSg1000n1bc", XPlaneCommands.GPSG1000n1Bc); } }
        private XPlaneCommand GPSG1000n1Vs { get { return new XPlaneCommand("sim/GPS/g1000n1_vs", "G1000 pilot AP vertical speed.", "GPSg1000n1vs", XPlaneCommands.GPSG1000n1Vs); } }
        private XPlaneCommand GPSG1000n1Flc { get { return new XPlaneCommand("sim/GPS/g1000n1_flc", "G1000 pilot AP flight level change.", "GPSg1000n1flc", XPlaneCommands.GPSG1000n1Flc); } }
        private XPlaneCommand GPSG1000n1NoseUp { get { return new XPlaneCommand("sim/GPS/g1000n1_nose_up", "G1000 pilot AP nose up.", "GPSg1000n1nose Up", XPlaneCommands.GPSG1000n1NoseUp); } }
        private XPlaneCommand GPSG1000n1NoseDown { get { return new XPlaneCommand("sim/GPS/g1000n1_nose_down", "G1000 pilot AP nose down.", "GPSg1000n1nose Down", XPlaneCommands.GPSG1000n1NoseDown); } }
        private XPlaneCommand GPSG1000n1AltOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n1_alt_outer_up", "G1000 pilot altitude outer ring up.", "GPSg1000n1alt Outer Up", XPlaneCommands.GPSG1000n1AltOuterUp); } }
        private XPlaneCommand GPSG1000n1AltOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n1_alt_outer_down", "G1000 pilot altitude outer ring down.", "GPSg1000n1alt Outer Down", XPlaneCommands.GPSG1000n1AltOuterDown); } }
        private XPlaneCommand GPSG1000n1AltInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n1_alt_inner_up", "G1000 pilot altitude inner ring up.", "GPSg1000n1alt Inner Up", XPlaneCommands.GPSG1000n1AltInnerUp); } }
        private XPlaneCommand GPSG1000n1AltInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n1_alt_inner_down", "G1000 pilot altitude inner ring down.", "GPSg1000n1alt Inner Down", XPlaneCommands.GPSG1000n1AltInnerDown); } }
        private XPlaneCommand GPSG1000n1Softkey1 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey1", "G1000 pilot PFD Softkey 1.", "GPSg1000n1softkey1", XPlaneCommands.GPSG1000n1Softkey1); } }
        private XPlaneCommand GPSG1000n1Softkey2 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey2", "G1000 pilot PFD Softkey 2.", "GPSg1000n1softkey2", XPlaneCommands.GPSG1000n1Softkey2); } }
        private XPlaneCommand GPSG1000n1Softkey3 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey3", "G1000 pilot PFD Softkey 3.", "GPSg1000n1softkey3", XPlaneCommands.GPSG1000n1Softkey3); } }
        private XPlaneCommand GPSG1000n1Softkey4 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey4", "G1000 pilot PFD Softkey 4.", "GPSg1000n1softkey4", XPlaneCommands.GPSG1000n1Softkey4); } }
        private XPlaneCommand GPSG1000n1Softkey5 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey5", "G1000 pilot PFD Softkey 5.", "GPSg1000n1softkey5", XPlaneCommands.GPSG1000n1Softkey5); } }
        private XPlaneCommand GPSG1000n1Softkey6 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey6", "G1000 pilot PFD Softkey 6.", "GPSg1000n1softkey6", XPlaneCommands.GPSG1000n1Softkey6); } }
        private XPlaneCommand GPSG1000n1Softkey7 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey7", "G1000 pilot PFD Softkey 7.", "GPSg1000n1softkey7", XPlaneCommands.GPSG1000n1Softkey7); } }
        private XPlaneCommand GPSG1000n1Softkey8 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey8", "G1000 pilot PFD Softkey 8.", "GPSg1000n1softkey8", XPlaneCommands.GPSG1000n1Softkey8); } }
        private XPlaneCommand GPSG1000n1Softkey9 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey9", "G1000 pilot PFD Softkey 9.", "GPSg1000n1softkey9", XPlaneCommands.GPSG1000n1Softkey9); } }
        private XPlaneCommand GPSG1000n1Softkey10 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey10", "G1000 pilot PFD Softkey 10.", "GPSg1000n1softkey10", XPlaneCommands.GPSG1000n1Softkey10); } }
        private XPlaneCommand GPSG1000n1Softkey11 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey11", "G1000 pilot PFD Softkey 11.", "GPSg1000n1softkey11", XPlaneCommands.GPSG1000n1Softkey11); } }
        private XPlaneCommand GPSG1000n1Softkey12 { get { return new XPlaneCommand("sim/GPS/g1000n1_softkey12", "G1000 pilot PFD Softkey 12.", "GPSg1000n1softkey12", XPlaneCommands.GPSG1000n1Softkey12); } }
        private XPlaneCommand GPSG1000n1Cvol { get { return new XPlaneCommand("sim/GPS/g1000n1_cvol", "G1000 pilot COM audio.", "GPSg1000n1cvol", XPlaneCommands.GPSG1000n1Cvol); } }
        private XPlaneCommand GPSG1000n1CvolUp { get { return new XPlaneCommand("sim/GPS/g1000n1_cvol_up", "G1000 pilot COM audio volume up.", "GPSg1000n1cvol Up", XPlaneCommands.GPSG1000n1CvolUp); } }
        private XPlaneCommand GPSG1000n1CvolDn { get { return new XPlaneCommand("sim/GPS/g1000n1_cvol_dn", "G1000 pilot COM audio volume down.", "GPSg1000n1cvol Dn", XPlaneCommands.GPSG1000n1CvolDn); } }
        private XPlaneCommand GPSG1000n1ComFf { get { return new XPlaneCommand("sim/GPS/g1000n1_com_ff", "G1000 pilot COM flip flop.", "GPSg1000n1com Ff", XPlaneCommands.GPSG1000n1ComFf); } }
        private XPlaneCommand GPSG1000n1ComOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n1_com_outer_up", "G1000 pilot COM outer ring tune up.", "GPSg1000n1com Outer Up", XPlaneCommands.GPSG1000n1ComOuterUp); } }
        private XPlaneCommand GPSG1000n1ComOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n1_com_outer_down", "G1000 pilot COM outer ring tune down.", "GPSg1000n1com Outer Down", XPlaneCommands.GPSG1000n1ComOuterDown); } }
        private XPlaneCommand GPSG1000n1ComInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n1_com_inner_up", "G1000 pilot COM inner ring tune up.", "GPSg1000n1com Inner Up", XPlaneCommands.GPSG1000n1ComInnerUp); } }
        private XPlaneCommand GPSG1000n1ComInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n1_com_inner_down", "G1000 pilot COM inner ring tune down.", "GPSg1000n1com Inner Down", XPlaneCommands.GPSG1000n1ComInnerDown); } }
        private XPlaneCommand GPSG1000n1Com12 { get { return new XPlaneCommand("sim/GPS/g1000n1_com12", "G1000 pilot PFD Com 1/2.", "GPSg1000n1com12", XPlaneCommands.GPSG1000n1Com12); } }
        private XPlaneCommand GPSG1000n1CrsUp { get { return new XPlaneCommand("sim/GPS/g1000n1_crs_up", "G1000 pilot CRS up.", "GPSg1000n1crs Up", XPlaneCommands.GPSG1000n1CrsUp); } }
        private XPlaneCommand GPSG1000n1CrsDown { get { return new XPlaneCommand("sim/GPS/g1000n1_crs_down", "G1000 pilot CRS down.", "GPSg1000n1crs Down", XPlaneCommands.GPSG1000n1CrsDown); } }
        private XPlaneCommand GPSG1000n1CrsSync { get { return new XPlaneCommand("sim/GPS/g1000n1_crs_sync", "G1000 pilot CRS sync.", "GPSg1000n1crs Sync", XPlaneCommands.GPSG1000n1CrsSync); } }
        private XPlaneCommand GPSG1000n1BaroUp { get { return new XPlaneCommand("sim/GPS/g1000n1_baro_up", "G1000 pilot baro up.", "GPSg1000n1baro Up", XPlaneCommands.GPSG1000n1BaroUp); } }
        private XPlaneCommand GPSG1000n1BaroDown { get { return new XPlaneCommand("sim/GPS/g1000n1_baro_down", "G1000 pilot baro down.", "GPSg1000n1baro Down", XPlaneCommands.GPSG1000n1BaroDown); } }
        private XPlaneCommand GPSG1000n1RangeUp { get { return new XPlaneCommand("sim/GPS/g1000n1_range_up", "G1000 pilot range up.", "GPSg1000n1range Up", XPlaneCommands.GPSG1000n1RangeUp); } }
        private XPlaneCommand GPSG1000n1RangeDown { get { return new XPlaneCommand("sim/GPS/g1000n1_range_down", "G1000 pilot range down.", "GPSg1000n1range Down", XPlaneCommands.GPSG1000n1RangeDown); } }
        private XPlaneCommand GPSG1000n1PanUp { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_up", "G1000 pilot pan up.", "GPSg1000n1pan Up", XPlaneCommands.GPSG1000n1PanUp); } }
        private XPlaneCommand GPSG1000n1PanDown { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_down", "G1000 pilot pan down.", "GPSg1000n1pan Down", XPlaneCommands.GPSG1000n1PanDown); } }
        private XPlaneCommand GPSG1000n1PanLeft { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_left", "G1000 pilot pan left.", "GPSg1000n1pan Left", XPlaneCommands.GPSG1000n1PanLeft); } }
        private XPlaneCommand GPSG1000n1PanRight { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_right", "G1000 pilot pan right.", "GPSg1000n1pan Right", XPlaneCommands.GPSG1000n1PanRight); } }
        private XPlaneCommand GPSG1000n1PanUpLeft { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_up_left", "G1000 pilot pan up left.", "GPSg1000n1pan Up Left", XPlaneCommands.GPSG1000n1PanUpLeft); } }
        private XPlaneCommand GPSG1000n1PanDownLeft { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_down_left", "G1000 pilot pan down left.", "GPSg1000n1pan Down Left", XPlaneCommands.GPSG1000n1PanDownLeft); } }
        private XPlaneCommand GPSG1000n1PanUpRight { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_up_right", "G1000 pilot pan up right.", "GPSg1000n1pan Up Right", XPlaneCommands.GPSG1000n1PanUpRight); } }
        private XPlaneCommand GPSG1000n1PanDownRight { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_down_right", "G1000 pilot pan down right.", "GPSg1000n1pan Down Right", XPlaneCommands.GPSG1000n1PanDownRight); } }
        private XPlaneCommand GPSG1000n1PanPush { get { return new XPlaneCommand("sim/GPS/g1000n1_pan_push", "G1000 pilot push pan.", "GPSg1000n1pan Push", XPlaneCommands.GPSG1000n1PanPush); } }
        private XPlaneCommand GPSG1000n1Direct { get { return new XPlaneCommand("sim/GPS/g1000n1_direct", "G1000 pilot -D->.", "GPSg1000n1direct", XPlaneCommands.GPSG1000n1Direct); } }
        private XPlaneCommand GPSG1000n1Menu { get { return new XPlaneCommand("sim/GPS/g1000n1_menu", "G1000 pilot menu.", "GPSg1000n1menu", XPlaneCommands.GPSG1000n1Menu); } }
        private XPlaneCommand GPSG1000n1Fpl { get { return new XPlaneCommand("sim/GPS/g1000n1_fpl", "G1000 pilot FPL.", "GPSg1000n1fpl", XPlaneCommands.GPSG1000n1Fpl); } }
        private XPlaneCommand GPSG1000n1Proc { get { return new XPlaneCommand("sim/GPS/g1000n1_proc", "G1000 pilot proc.", "GPSg1000n1proc", XPlaneCommands.GPSG1000n1Proc); } }
        private XPlaneCommand GPSG1000n1Clr { get { return new XPlaneCommand("sim/GPS/g1000n1_clr", "G1000 pilot CLR.", "GPSg1000n1clr", XPlaneCommands.GPSG1000n1Clr); } }
        private XPlaneCommand GPSG1000n1Ent { get { return new XPlaneCommand("sim/GPS/g1000n1_ent", "G1000 pilot ENT.", "GPSg1000n1ent", XPlaneCommands.GPSG1000n1Ent); } }
        private XPlaneCommand GPSG1000n1FmsOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n1_fms_outer_up", "G1000 pilot FMS outer ring tune up.", "GPSg1000n1fms Outer Up", XPlaneCommands.GPSG1000n1FmsOuterUp); } }
        private XPlaneCommand GPSG1000n1FmsOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n1_fms_outer_down", "G1000 pilot FMS outer ring tune down.", "GPSg1000n1fms Outer Down", XPlaneCommands.GPSG1000n1FmsOuterDown); } }
        private XPlaneCommand GPSG1000n1FmsInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n1_fms_inner_up", "G1000 pilot FMS inner ring tune up.", "GPSg1000n1fms Inner Up", XPlaneCommands.GPSG1000n1FmsInnerUp); } }
        private XPlaneCommand GPSG1000n1FmsInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n1_fms_inner_down", "G1000 pilot FMS inner ring tune down.", "GPSg1000n1fms Inner Down", XPlaneCommands.GPSG1000n1FmsInnerDown); } }
        private XPlaneCommand GPSG1000n1Cursor { get { return new XPlaneCommand("sim/GPS/g1000n1_cursor", "G1000 pilot FMS cursor.", "GPSg1000n1cursor", XPlaneCommands.GPSG1000n1Cursor); } }
        private XPlaneCommand GPSG1000n1Popout { get { return new XPlaneCommand("sim/GPS/g1000n1_popout", "G1000 pilot pop out window.", "GPSg1000n1popout", XPlaneCommands.GPSG1000n1Popout); } }
        private XPlaneCommand GPSG1000n1Popup { get { return new XPlaneCommand("sim/GPS/g1000n1_popup", "G1000 pilot popup.", "GPSg1000n1popup", XPlaneCommands.GPSG1000n1Popup); } }
        private XPlaneCommand GPSG1000n2Nvol { get { return new XPlaneCommand("sim/GPS/g1000n2_nvol", "G1000 copilot NAV audio.", "GPSg1000n2nvol", XPlaneCommands.GPSG1000n2Nvol); } }
        private XPlaneCommand GPSG1000n2NvolUp { get { return new XPlaneCommand("sim/GPS/g1000n2_nvol_up", "G1000 copilot NAV audio volume up.", "GPSg1000n2nvol Up", XPlaneCommands.GPSG1000n2NvolUp); } }
        private XPlaneCommand GPSG1000n2NvolDn { get { return new XPlaneCommand("sim/GPS/g1000n2_nvol_dn", "G1000 copilot NAV audio volume down.", "GPSg1000n2nvol Dn", XPlaneCommands.GPSG1000n2NvolDn); } }
        private XPlaneCommand GPSG1000n2NavFf { get { return new XPlaneCommand("sim/GPS/g1000n2_nav_ff", "G1000 copilot NAV flip flop.", "GPSg1000n2nav Ff", XPlaneCommands.GPSG1000n2NavFf); } }
        private XPlaneCommand GPSG1000n2NavOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n2_nav_outer_up", "G1000 copilot NAV outer ring tune up.", "GPSg1000n2nav Outer Up", XPlaneCommands.GPSG1000n2NavOuterUp); } }
        private XPlaneCommand GPSG1000n2NavOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n2_nav_outer_down", "G1000 copilot NAV outer ring tune down.", "GPSg1000n2nav Outer Down", XPlaneCommands.GPSG1000n2NavOuterDown); } }
        private XPlaneCommand GPSG1000n2NavInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n2_nav_inner_up", "G1000 copilot NAV inner ring tune up.", "GPSg1000n2nav Inner Up", XPlaneCommands.GPSG1000n2NavInnerUp); } }
        private XPlaneCommand GPSG1000n2NavInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n2_nav_inner_down", "G1000 copilot NAV inner ring tune down.", "GPSg1000n2nav Inner Down", XPlaneCommands.GPSG1000n2NavInnerDown); } }
        private XPlaneCommand GPSG1000n2Nav12 { get { return new XPlaneCommand("sim/GPS/g1000n2_nav12", "G1000 copilot PFD NAV 1/2.", "GPSg1000n2nav12", XPlaneCommands.GPSG1000n2Nav12); } }
        private XPlaneCommand GPSG1000n2HdgUp { get { return new XPlaneCommand("sim/GPS/g1000n2_hdg_up", "G1000 copilot HDG up.", "GPSg1000n2hdg Up", XPlaneCommands.GPSG1000n2HdgUp); } }
        private XPlaneCommand GPSG1000n2HdgDown { get { return new XPlaneCommand("sim/GPS/g1000n2_hdg_down", "G1000 copilot HDG down.", "GPSg1000n2hdg Down", XPlaneCommands.GPSG1000n2HdgDown); } }
        private XPlaneCommand GPSG1000n2HdgSync { get { return new XPlaneCommand("sim/GPS/g1000n2_hdg_sync", "G1000 copilot HDG sync.", "GPSg1000n2hdg Sync", XPlaneCommands.GPSG1000n2HdgSync); } }
        private XPlaneCommand GPSG1000n2Ap { get { return new XPlaneCommand("sim/GPS/g1000n2_ap", "G1000 copilot autopilot.", "GPSg1000n2ap", XPlaneCommands.GPSG1000n2Ap); } }
        private XPlaneCommand GPSG1000n2Fd { get { return new XPlaneCommand("sim/GPS/g1000n2_fd", "G1000 copilot flight director.", "GPSg1000n2fd", XPlaneCommands.GPSG1000n2Fd); } }
        private XPlaneCommand GPSG1000n2Yd { get { return new XPlaneCommand("sim/GPS/g1000n2_yd", "G1000 copilot yaw damper.", "GPSg1000n2yd", XPlaneCommands.GPSG1000n2Yd); } }
        private XPlaneCommand GPSG1000n2Hdg { get { return new XPlaneCommand("sim/GPS/g1000n2_hdg", "G1000 copilot AP HDG.", "GPSg1000n2hdg", XPlaneCommands.GPSG1000n2Hdg); } }
        private XPlaneCommand GPSG1000n2Alt { get { return new XPlaneCommand("sim/GPS/g1000n2_alt", "G1000 copilot AP ALT.", "GPSg1000n2alt", XPlaneCommands.GPSG1000n2Alt); } }
        private XPlaneCommand GPSG1000n2Nav { get { return new XPlaneCommand("sim/GPS/g1000n2_nav", "G1000 copilot AP NAV.", "GPSg1000n2nav", XPlaneCommands.GPSG1000n2Nav); } }
        private XPlaneCommand GPSG1000n2Vnv { get { return new XPlaneCommand("sim/GPS/g1000n2_vnv", "G1000 copilot AP VNAV.", "GPSg1000n2vnv", XPlaneCommands.GPSG1000n2Vnv); } }
        private XPlaneCommand GPSG1000n2Apr { get { return new XPlaneCommand("sim/GPS/g1000n2_apr", "G1000 copilot AP approach.", "GPSg1000n2apr", XPlaneCommands.GPSG1000n2Apr); } }
        private XPlaneCommand GPSG1000n2Bc { get { return new XPlaneCommand("sim/GPS/g1000n2_bc", "G1000 copilot AP backcourse.", "GPSg1000n2bc", XPlaneCommands.GPSG1000n2Bc); } }
        private XPlaneCommand GPSG1000n2Vs { get { return new XPlaneCommand("sim/GPS/g1000n2_vs", "G1000 copilot AP vertical speed.", "GPSg1000n2vs", XPlaneCommands.GPSG1000n2Vs); } }
        private XPlaneCommand GPSG1000n2Flc { get { return new XPlaneCommand("sim/GPS/g1000n2_flc", "G1000 copilot AP flight level change.", "GPSg1000n2flc", XPlaneCommands.GPSG1000n2Flc); } }
        private XPlaneCommand GPSG1000n2NoseUp { get { return new XPlaneCommand("sim/GPS/g1000n2_nose_up", "G1000 copilot AP nose up.", "GPSg1000n2nose Up", XPlaneCommands.GPSG1000n2NoseUp); } }
        private XPlaneCommand GPSG1000n2NoseDown { get { return new XPlaneCommand("sim/GPS/g1000n2_nose_down", "G1000 copilot AP nose down.", "GPSg1000n2nose Down", XPlaneCommands.GPSG1000n2NoseDown); } }
        private XPlaneCommand GPSG1000n2AltOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n2_alt_outer_up", "G1000 copilot altitude outer ring up.", "GPSg1000n2alt Outer Up", XPlaneCommands.GPSG1000n2AltOuterUp); } }
        private XPlaneCommand GPSG1000n2AltOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n2_alt_outer_down", "G1000 copilot altitude outer ring down.", "GPSg1000n2alt Outer Down", XPlaneCommands.GPSG1000n2AltOuterDown); } }
        private XPlaneCommand GPSG1000n2AltInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n2_alt_inner_up", "G1000 copilot altitude inner ring up.", "GPSg1000n2alt Inner Up", XPlaneCommands.GPSG1000n2AltInnerUp); } }
        private XPlaneCommand GPSG1000n2AltInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n2_alt_inner_down", "G1000 copilot altitude inner ring down.", "GPSg1000n2alt Inner Down", XPlaneCommands.GPSG1000n2AltInnerDown); } }
        private XPlaneCommand GPSG1000n2Softkey1 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey1", "G1000 copilot PFD Softkey 1.", "GPSg1000n2softkey1", XPlaneCommands.GPSG1000n2Softkey1); } }
        private XPlaneCommand GPSG1000n2Softkey2 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey2", "G1000 copilot PFD Softkey 2.", "GPSg1000n2softkey2", XPlaneCommands.GPSG1000n2Softkey2); } }
        private XPlaneCommand GPSG1000n2Softkey3 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey3", "G1000 copilot PFD Softkey 3.", "GPSg1000n2softkey3", XPlaneCommands.GPSG1000n2Softkey3); } }
        private XPlaneCommand GPSG1000n2Softkey4 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey4", "G1000 copilot PFD Softkey 4.", "GPSg1000n2softkey4", XPlaneCommands.GPSG1000n2Softkey4); } }
        private XPlaneCommand GPSG1000n2Softkey5 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey5", "G1000 copilot PFD Softkey 5.", "GPSg1000n2softkey5", XPlaneCommands.GPSG1000n2Softkey5); } }
        private XPlaneCommand GPSG1000n2Softkey6 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey6", "G1000 copilot PFD Softkey 6.", "GPSg1000n2softkey6", XPlaneCommands.GPSG1000n2Softkey6); } }
        private XPlaneCommand GPSG1000n2Softkey7 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey7", "G1000 copilot PFD Softkey 7.", "GPSg1000n2softkey7", XPlaneCommands.GPSG1000n2Softkey7); } }
        private XPlaneCommand GPSG1000n2Softkey8 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey8", "G1000 copilot PFD Softkey 8.", "GPSg1000n2softkey8", XPlaneCommands.GPSG1000n2Softkey8); } }
        private XPlaneCommand GPSG1000n2Softkey9 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey9", "G1000 copilot PFD Softkey 9.", "GPSg1000n2softkey9", XPlaneCommands.GPSG1000n2Softkey9); } }
        private XPlaneCommand GPSG1000n2Softkey10 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey10", "G1000 copilot PFD Softkey 10.", "GPSg1000n2softkey10", XPlaneCommands.GPSG1000n2Softkey10); } }
        private XPlaneCommand GPSG1000n2Softkey11 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey11", "G1000 copilot PFD Softkey 11.", "GPSg1000n2softkey11", XPlaneCommands.GPSG1000n2Softkey11); } }
        private XPlaneCommand GPSG1000n2Softkey12 { get { return new XPlaneCommand("sim/GPS/g1000n2_softkey12", "G1000 copilot PFD Softkey 12.", "GPSg1000n2softkey12", XPlaneCommands.GPSG1000n2Softkey12); } }
        private XPlaneCommand GPSG1000n2Cvol { get { return new XPlaneCommand("sim/GPS/g1000n2_cvol", "G1000 copilot COM audio.", "GPSg1000n2cvol", XPlaneCommands.GPSG1000n2Cvol); } }
        private XPlaneCommand GPSG1000n2CvolUp { get { return new XPlaneCommand("sim/GPS/g1000n2_cvol_up", "G1000 copilot COM audio volume up.", "GPSg1000n2cvol Up", XPlaneCommands.GPSG1000n2CvolUp); } }
        private XPlaneCommand GPSG1000n2CvolDn { get { return new XPlaneCommand("sim/GPS/g1000n2_cvol_dn", "G1000 copilot COM audio volume down.", "GPSg1000n2cvol Dn", XPlaneCommands.GPSG1000n2CvolDn); } }
        private XPlaneCommand GPSG1000n2ComFf { get { return new XPlaneCommand("sim/GPS/g1000n2_com_ff", "G1000 copilot COM flip flop.", "GPSg1000n2com Ff", XPlaneCommands.GPSG1000n2ComFf); } }
        private XPlaneCommand GPSG1000n2ComOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n2_com_outer_up", "G1000 copilot COM outer ring tune up.", "GPSg1000n2com Outer Up", XPlaneCommands.GPSG1000n2ComOuterUp); } }
        private XPlaneCommand GPSG1000n2ComOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n2_com_outer_down", "G1000 copilot COM outer ring tune down.", "GPSg1000n2com Outer Down", XPlaneCommands.GPSG1000n2ComOuterDown); } }
        private XPlaneCommand GPSG1000n2ComInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n2_com_inner_up", "G1000 copilot COM inner ring tune up.", "GPSg1000n2com Inner Up", XPlaneCommands.GPSG1000n2ComInnerUp); } }
        private XPlaneCommand GPSG1000n2ComInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n2_com_inner_down", "G1000 copilot COM inner ring tune down.", "GPSg1000n2com Inner Down", XPlaneCommands.GPSG1000n2ComInnerDown); } }
        private XPlaneCommand GPSG1000n2Com12 { get { return new XPlaneCommand("sim/GPS/g1000n2_com12", "G1000 copilot PFD Com 1/2.", "GPSg1000n2com12", XPlaneCommands.GPSG1000n2Com12); } }
        private XPlaneCommand GPSG1000n2CrsUp { get { return new XPlaneCommand("sim/GPS/g1000n2_crs_up", "G1000 copilot CRS up.", "GPSg1000n2crs Up", XPlaneCommands.GPSG1000n2CrsUp); } }
        private XPlaneCommand GPSG1000n2CrsDown { get { return new XPlaneCommand("sim/GPS/g1000n2_crs_down", "G1000 copilot CRS down.", "GPSg1000n2crs Down", XPlaneCommands.GPSG1000n2CrsDown); } }
        private XPlaneCommand GPSG1000n2CrsSync { get { return new XPlaneCommand("sim/GPS/g1000n2_crs_sync", "G1000 copilot CRS sync.", "GPSg1000n2crs Sync", XPlaneCommands.GPSG1000n2CrsSync); } }
        private XPlaneCommand GPSG1000n2BaroUp { get { return new XPlaneCommand("sim/GPS/g1000n2_baro_up", "G1000 copilot baro up.", "GPSg1000n2baro Up", XPlaneCommands.GPSG1000n2BaroUp); } }
        private XPlaneCommand GPSG1000n2BaroDown { get { return new XPlaneCommand("sim/GPS/g1000n2_baro_down", "G1000 copilot baro down.", "GPSg1000n2baro Down", XPlaneCommands.GPSG1000n2BaroDown); } }
        private XPlaneCommand GPSG1000n2RangeUp { get { return new XPlaneCommand("sim/GPS/g1000n2_range_up", "G1000 copilot range up.", "GPSg1000n2range Up", XPlaneCommands.GPSG1000n2RangeUp); } }
        private XPlaneCommand GPSG1000n2RangeDown { get { return new XPlaneCommand("sim/GPS/g1000n2_range_down", "G1000 copilot range down.", "GPSg1000n2range Down", XPlaneCommands.GPSG1000n2RangeDown); } }
        private XPlaneCommand GPSG1000n2PanUp { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_up", "G1000 copilot pan up.", "GPSg1000n2pan Up", XPlaneCommands.GPSG1000n2PanUp); } }
        private XPlaneCommand GPSG1000n2PanDown { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_down", "G1000 copilot pan down.", "GPSg1000n2pan Down", XPlaneCommands.GPSG1000n2PanDown); } }
        private XPlaneCommand GPSG1000n2PanLeft { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_left", "G1000 copilot pan left.", "GPSg1000n2pan Left", XPlaneCommands.GPSG1000n2PanLeft); } }
        private XPlaneCommand GPSG1000n2PanRight { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_right", "G1000 copilot pan right.", "GPSg1000n2pan Right", XPlaneCommands.GPSG1000n2PanRight); } }
        private XPlaneCommand GPSG1000n2PanUpLeft { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_up_left", "G1000 copilot pan up left.", "GPSg1000n2pan Up Left", XPlaneCommands.GPSG1000n2PanUpLeft); } }
        private XPlaneCommand GPSG1000n2PanDownLeft { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_down_left", "G1000 copilot pan down left.", "GPSg1000n2pan Down Left", XPlaneCommands.GPSG1000n2PanDownLeft); } }
        private XPlaneCommand GPSG1000n2PanUpRight { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_up_right", "G1000 copilot pan up right.", "GPSg1000n2pan Up Right", XPlaneCommands.GPSG1000n2PanUpRight); } }
        private XPlaneCommand GPSG1000n2PanDownRight { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_down_right", "G1000 copilot pan down right.", "GPSg1000n2pan Down Right", XPlaneCommands.GPSG1000n2PanDownRight); } }
        private XPlaneCommand GPSG1000n2PanPush { get { return new XPlaneCommand("sim/GPS/g1000n2_pan_push", "G1000 copilot push pan.", "GPSg1000n2pan Push", XPlaneCommands.GPSG1000n2PanPush); } }
        private XPlaneCommand GPSG1000n2Direct { get { return new XPlaneCommand("sim/GPS/g1000n2_direct", "G1000 copilot -D->.", "GPSg1000n2direct", XPlaneCommands.GPSG1000n2Direct); } }
        private XPlaneCommand GPSG1000n2Menu { get { return new XPlaneCommand("sim/GPS/g1000n2_menu", "G1000 copilot menu.", "GPSg1000n2menu", XPlaneCommands.GPSG1000n2Menu); } }
        private XPlaneCommand GPSG1000n2Fpl { get { return new XPlaneCommand("sim/GPS/g1000n2_fpl", "G1000 copilot FPL.", "GPSg1000n2fpl", XPlaneCommands.GPSG1000n2Fpl); } }
        private XPlaneCommand GPSG1000n2Proc { get { return new XPlaneCommand("sim/GPS/g1000n2_proc", "G1000 copilot proc.", "GPSg1000n2proc", XPlaneCommands.GPSG1000n2Proc); } }
        private XPlaneCommand GPSG1000n2Clr { get { return new XPlaneCommand("sim/GPS/g1000n2_clr", "G1000 copilot CLR.", "GPSg1000n2clr", XPlaneCommands.GPSG1000n2Clr); } }
        private XPlaneCommand GPSG1000n2Ent { get { return new XPlaneCommand("sim/GPS/g1000n2_ent", "G1000 copilot ENT.", "GPSg1000n2ent", XPlaneCommands.GPSG1000n2Ent); } }
        private XPlaneCommand GPSG1000n2FmsOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n2_fms_outer_up", "G1000 copilot FMS outer ring tune up.", "GPSg1000n2fms Outer Up", XPlaneCommands.GPSG1000n2FmsOuterUp); } }
        private XPlaneCommand GPSG1000n2FmsOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n2_fms_outer_down", "G1000 copilot FMS outer ring tune down.", "GPSg1000n2fms Outer Down", XPlaneCommands.GPSG1000n2FmsOuterDown); } }
        private XPlaneCommand GPSG1000n2FmsInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n2_fms_inner_up", "G1000 copilot FMS inner ring tune up.", "GPSg1000n2fms Inner Up", XPlaneCommands.GPSG1000n2FmsInnerUp); } }
        private XPlaneCommand GPSG1000n2FmsInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n2_fms_inner_down", "G1000 copilot FMS inner ring tune down.", "GPSg1000n2fms Inner Down", XPlaneCommands.GPSG1000n2FmsInnerDown); } }
        private XPlaneCommand GPSG1000n2Cursor { get { return new XPlaneCommand("sim/GPS/g1000n2_cursor", "G1000 copilot FMS cursor.", "GPSg1000n2cursor", XPlaneCommands.GPSG1000n2Cursor); } }
        private XPlaneCommand GPSG1000n2Popout { get { return new XPlaneCommand("sim/GPS/g1000n2_popout", "G1000 copilot pop out window.", "GPSg1000n2popout", XPlaneCommands.GPSG1000n2Popout); } }
        private XPlaneCommand GPSG1000n2Popup { get { return new XPlaneCommand("sim/GPS/g1000n2_popup", "G1000 copilot popup.", "GPSg1000n2popup", XPlaneCommands.GPSG1000n2Popup); } }
        private XPlaneCommand GPSG1000n3Nvol { get { return new XPlaneCommand("sim/GPS/g1000n3_nvol", "G1000 MFD NAV audio.", "GPSg1000n3nvol", XPlaneCommands.GPSG1000n3Nvol); } }
        private XPlaneCommand GPSG1000n3NvolUp { get { return new XPlaneCommand("sim/GPS/g1000n3_nvol_up", "G1000 MFD NAV audio volume up.", "GPSg1000n3nvol Up", XPlaneCommands.GPSG1000n3NvolUp); } }
        private XPlaneCommand GPSG1000n3NvolDn { get { return new XPlaneCommand("sim/GPS/g1000n3_nvol_dn", "G1000 MFD NAV audio volume down.", "GPSg1000n3nvol Dn", XPlaneCommands.GPSG1000n3NvolDn); } }
        private XPlaneCommand GPSG1000n3NavFf { get { return new XPlaneCommand("sim/GPS/g1000n3_nav_ff", "G1000 MFD NAV flip flop.", "GPSg1000n3nav Ff", XPlaneCommands.GPSG1000n3NavFf); } }
        private XPlaneCommand GPSG1000n3NavOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n3_nav_outer_up", "G1000 MFD NAV outer ring tune up.", "GPSg1000n3nav Outer Up", XPlaneCommands.GPSG1000n3NavOuterUp); } }
        private XPlaneCommand GPSG1000n3NavOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n3_nav_outer_down", "G1000 MFD NAV outer ring tune down.", "GPSg1000n3nav Outer Down", XPlaneCommands.GPSG1000n3NavOuterDown); } }
        private XPlaneCommand GPSG1000n3NavInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n3_nav_inner_up", "G1000 MFD NAV inner ring tune up.", "GPSg1000n3nav Inner Up", XPlaneCommands.GPSG1000n3NavInnerUp); } }
        private XPlaneCommand GPSG1000n3NavInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n3_nav_inner_down", "G1000 MFD NAV inner ring tune down.", "GPSg1000n3nav Inner Down", XPlaneCommands.GPSG1000n3NavInnerDown); } }
        private XPlaneCommand GPSG1000n3Nav12 { get { return new XPlaneCommand("sim/GPS/g1000n3_nav12", "G1000 MFD NAV 1/2.", "GPSg1000n3nav12", XPlaneCommands.GPSG1000n3Nav12); } }
        private XPlaneCommand GPSG1000n3HdgUp { get { return new XPlaneCommand("sim/GPS/g1000n3_hdg_up", "G1000 MFD HDG up.", "GPSg1000n3hdg Up", XPlaneCommands.GPSG1000n3HdgUp); } }
        private XPlaneCommand GPSG1000n3HdgDown { get { return new XPlaneCommand("sim/GPS/g1000n3_hdg_down", "G1000 MFD HDG down.", "GPSg1000n3hdg Down", XPlaneCommands.GPSG1000n3HdgDown); } }
        private XPlaneCommand GPSG1000n3HdgSync { get { return new XPlaneCommand("sim/GPS/g1000n3_hdg_sync", "G1000 MFD HDG sync.", "GPSg1000n3hdg Sync", XPlaneCommands.GPSG1000n3HdgSync); } }
        private XPlaneCommand GPSG1000n3Ap { get { return new XPlaneCommand("sim/GPS/g1000n3_ap", "G1000 MFD autopilot.", "GPSg1000n3ap", XPlaneCommands.GPSG1000n3Ap); } }
        private XPlaneCommand GPSG1000n3Fd { get { return new XPlaneCommand("sim/GPS/g1000n3_fd", "G1000 MFD flight director.", "GPSg1000n3fd", XPlaneCommands.GPSG1000n3Fd); } }
        private XPlaneCommand GPSG1000n3Yd { get { return new XPlaneCommand("sim/GPS/g1000n3_yd", "G1000 MFD yaw damper.", "GPSg1000n3yd", XPlaneCommands.GPSG1000n3Yd); } }
        private XPlaneCommand GPSG1000n3Hdg { get { return new XPlaneCommand("sim/GPS/g1000n3_hdg", "G1000 MFD AP HDG.", "GPSg1000n3hdg", XPlaneCommands.GPSG1000n3Hdg); } }
        private XPlaneCommand GPSG1000n3Alt { get { return new XPlaneCommand("sim/GPS/g1000n3_alt", "G1000 MFD AP ALT.", "GPSg1000n3alt", XPlaneCommands.GPSG1000n3Alt); } }
        private XPlaneCommand GPSG1000n3Nav { get { return new XPlaneCommand("sim/GPS/g1000n3_nav", "G1000 MFD AP NAV.", "GPSg1000n3nav", XPlaneCommands.GPSG1000n3Nav); } }
        private XPlaneCommand GPSG1000n3Vnv { get { return new XPlaneCommand("sim/GPS/g1000n3_vnv", "G1000 MFD AP VNAV.", "GPSg1000n3vnv", XPlaneCommands.GPSG1000n3Vnv); } }
        private XPlaneCommand GPSG1000n3Apr { get { return new XPlaneCommand("sim/GPS/g1000n3_apr", "G1000 MFD AP approach.", "GPSg1000n3apr", XPlaneCommands.GPSG1000n3Apr); } }
        private XPlaneCommand GPSG1000n3Bc { get { return new XPlaneCommand("sim/GPS/g1000n3_bc", "G1000 MFD AP backcourse.", "GPSg1000n3bc", XPlaneCommands.GPSG1000n3Bc); } }
        private XPlaneCommand GPSG1000n3Vs { get { return new XPlaneCommand("sim/GPS/g1000n3_vs", "G1000 MFD AP vertical speed.", "GPSg1000n3vs", XPlaneCommands.GPSG1000n3Vs); } }
        private XPlaneCommand GPSG1000n3Flc { get { return new XPlaneCommand("sim/GPS/g1000n3_flc", "G1000 MFD AP flight level change.", "GPSg1000n3flc", XPlaneCommands.GPSG1000n3Flc); } }
        private XPlaneCommand GPSG1000n3NoseUp { get { return new XPlaneCommand("sim/GPS/g1000n3_nose_up", "G1000 MFD AP nose up.", "GPSg1000n3nose Up", XPlaneCommands.GPSG1000n3NoseUp); } }
        private XPlaneCommand GPSG1000n3NoseDown { get { return new XPlaneCommand("sim/GPS/g1000n3_nose_down", "G1000 MFD AP nose down.", "GPSg1000n3nose Down", XPlaneCommands.GPSG1000n3NoseDown); } }
        private XPlaneCommand GPSG1000n3AltOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n3_alt_outer_up", "G1000 MFD altitude outer ring up.", "GPSg1000n3alt Outer Up", XPlaneCommands.GPSG1000n3AltOuterUp); } }
        private XPlaneCommand GPSG1000n3AltOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n3_alt_outer_down", "G1000 MFD altitude outer ring down.", "GPSg1000n3alt Outer Down", XPlaneCommands.GPSG1000n3AltOuterDown); } }
        private XPlaneCommand GPSG1000n3AltInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n3_alt_inner_up", "G1000 MFD altitude inner ring up.", "GPSg1000n3alt Inner Up", XPlaneCommands.GPSG1000n3AltInnerUp); } }
        private XPlaneCommand GPSG1000n3AltInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n3_alt_inner_down", "G1000 MFD altitude inner ring down.", "GPSg1000n3alt Inner Down", XPlaneCommands.GPSG1000n3AltInnerDown); } }
        private XPlaneCommand GPSG1000n3Softkey1 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey1", "G1000 MFD Softkey 1.", "GPSg1000n3softkey1", XPlaneCommands.GPSG1000n3Softkey1); } }
        private XPlaneCommand GPSG1000n3Softkey2 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey2", "G1000 MFD Softkey 2.", "GPSg1000n3softkey2", XPlaneCommands.GPSG1000n3Softkey2); } }
        private XPlaneCommand GPSG1000n3Softkey3 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey3", "G1000 MFD Softkey 3.", "GPSg1000n3softkey3", XPlaneCommands.GPSG1000n3Softkey3); } }
        private XPlaneCommand GPSG1000n3Softkey4 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey4", "G1000 MFD Softkey 4.", "GPSg1000n3softkey4", XPlaneCommands.GPSG1000n3Softkey4); } }
        private XPlaneCommand GPSG1000n3Softkey5 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey5", "G1000 MFD Softkey 5.", "GPSg1000n3softkey5", XPlaneCommands.GPSG1000n3Softkey5); } }
        private XPlaneCommand GPSG1000n3Softkey6 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey6", "G1000 MFD Softkey 6.", "GPSg1000n3softkey6", XPlaneCommands.GPSG1000n3Softkey6); } }
        private XPlaneCommand GPSG1000n3Softkey7 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey7", "G1000 MFD Softkey 7.", "GPSg1000n3softkey7", XPlaneCommands.GPSG1000n3Softkey7); } }
        private XPlaneCommand GPSG1000n3Softkey8 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey8", "G1000 MFD Softkey 8.", "GPSg1000n3softkey8", XPlaneCommands.GPSG1000n3Softkey8); } }
        private XPlaneCommand GPSG1000n3Softkey9 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey9", "G1000 MFD Softkey 9.", "GPSg1000n3softkey9", XPlaneCommands.GPSG1000n3Softkey9); } }
        private XPlaneCommand GPSG1000n3Softkey10 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey10", "G1000 MFD Softkey 10.", "GPSg1000n3softkey10", XPlaneCommands.GPSG1000n3Softkey10); } }
        private XPlaneCommand GPSG1000n3Softkey11 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey11", "G1000 MFD Softkey 11.", "GPSg1000n3softkey11", XPlaneCommands.GPSG1000n3Softkey11); } }
        private XPlaneCommand GPSG1000n3Softkey12 { get { return new XPlaneCommand("sim/GPS/g1000n3_softkey12", "G1000 MFD Softkey 12.", "GPSg1000n3softkey12", XPlaneCommands.GPSG1000n3Softkey12); } }
        private XPlaneCommand GPSG1000n3Cvol { get { return new XPlaneCommand("sim/GPS/g1000n3_cvol", "G1000 MFD COM audio.", "GPSg1000n3cvol", XPlaneCommands.GPSG1000n3Cvol); } }
        private XPlaneCommand GPSG1000n3CvolUp { get { return new XPlaneCommand("sim/GPS/g1000n3_cvol_up", "G1000 MFD COM audio volume up.", "GPSg1000n3cvol Up", XPlaneCommands.GPSG1000n3CvolUp); } }
        private XPlaneCommand GPSG1000n3CvolDn { get { return new XPlaneCommand("sim/GPS/g1000n3_cvol_dn", "G1000 MFD COM audio volume down.", "GPSg1000n3cvol Dn", XPlaneCommands.GPSG1000n3CvolDn); } }
        private XPlaneCommand GPSG1000n3ComFf { get { return new XPlaneCommand("sim/GPS/g1000n3_com_ff", "G1000 MFD COM flip flop.", "GPSg1000n3com Ff", XPlaneCommands.GPSG1000n3ComFf); } }
        private XPlaneCommand GPSG1000n3ComOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n3_com_outer_up", "G1000 MFD COM outer ring tune up.", "GPSg1000n3com Outer Up", XPlaneCommands.GPSG1000n3ComOuterUp); } }
        private XPlaneCommand GPSG1000n3ComOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n3_com_outer_down", "G1000 MFD COM outer ring tune down.", "GPSg1000n3com Outer Down", XPlaneCommands.GPSG1000n3ComOuterDown); } }
        private XPlaneCommand GPSG1000n3ComInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n3_com_inner_up", "G1000 MFD COM inner ring tune up.", "GPSg1000n3com Inner Up", XPlaneCommands.GPSG1000n3ComInnerUp); } }
        private XPlaneCommand GPSG1000n3ComInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n3_com_inner_down", "G1000 MFD COM inner ring tune down.", "GPSg1000n3com Inner Down", XPlaneCommands.GPSG1000n3ComInnerDown); } }
        private XPlaneCommand GPSG1000n3Com12 { get { return new XPlaneCommand("sim/GPS/g1000n3_com12", "G1000 MFD COM 1/2.", "GPSg1000n3com12", XPlaneCommands.GPSG1000n3Com12); } }
        private XPlaneCommand GPSG1000n3CrsUp { get { return new XPlaneCommand("sim/GPS/g1000n3_crs_up", "G1000 MFD CRS up.", "GPSg1000n3crs Up", XPlaneCommands.GPSG1000n3CrsUp); } }
        private XPlaneCommand GPSG1000n3CrsDown { get { return new XPlaneCommand("sim/GPS/g1000n3_crs_down", "G1000 MFD CRS down.", "GPSg1000n3crs Down", XPlaneCommands.GPSG1000n3CrsDown); } }
        private XPlaneCommand GPSG1000n3CrsSync { get { return new XPlaneCommand("sim/GPS/g1000n3_crs_sync", "G1000 MFD CRS sync.", "GPSg1000n3crs Sync", XPlaneCommands.GPSG1000n3CrsSync); } }
        private XPlaneCommand GPSG1000n3BaroUp { get { return new XPlaneCommand("sim/GPS/g1000n3_baro_up", "G1000 MFD baro up.", "GPSg1000n3baro Up", XPlaneCommands.GPSG1000n3BaroUp); } }
        private XPlaneCommand GPSG1000n3BaroDown { get { return new XPlaneCommand("sim/GPS/g1000n3_baro_down", "G1000 MFD baro down.", "GPSg1000n3baro Down", XPlaneCommands.GPSG1000n3BaroDown); } }
        private XPlaneCommand GPSG1000n3RangeUp { get { return new XPlaneCommand("sim/GPS/g1000n3_range_up", "G1000 MFD range up.", "GPSg1000n3range Up", XPlaneCommands.GPSG1000n3RangeUp); } }
        private XPlaneCommand GPSG1000n3RangeDown { get { return new XPlaneCommand("sim/GPS/g1000n3_range_down", "G1000 MFD range down.", "GPSg1000n3range Down", XPlaneCommands.GPSG1000n3RangeDown); } }
        private XPlaneCommand GPSG1000n3PanUp { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_up", "G1000 MFD pan up.", "GPSg1000n3pan Up", XPlaneCommands.GPSG1000n3PanUp); } }
        private XPlaneCommand GPSG1000n3PanDown { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_down", "G1000 MFD pan down.", "GPSg1000n3pan Down", XPlaneCommands.GPSG1000n3PanDown); } }
        private XPlaneCommand GPSG1000n3PanLeft { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_left", "G1000 MFD pan left.", "GPSg1000n3pan Left", XPlaneCommands.GPSG1000n3PanLeft); } }
        private XPlaneCommand GPSG1000n3PanRight { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_right", "G1000 MFD pan right.", "GPSg1000n3pan Right", XPlaneCommands.GPSG1000n3PanRight); } }
        private XPlaneCommand GPSG1000n3PanUpLeft { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_up_left", "G1000 MFD pan up left.", "GPSg1000n3pan Up Left", XPlaneCommands.GPSG1000n3PanUpLeft); } }
        private XPlaneCommand GPSG1000n3PanDownLeft { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_down_left", "G1000 MFD pan down left.", "GPSg1000n3pan Down Left", XPlaneCommands.GPSG1000n3PanDownLeft); } }
        private XPlaneCommand GPSG1000n3PanUpRight { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_up_right", "G1000 MFD pan up right.", "GPSg1000n3pan Up Right", XPlaneCommands.GPSG1000n3PanUpRight); } }
        private XPlaneCommand GPSG1000n3PanDownRight { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_down_right", "G1000 MFD pan down right.", "GPSg1000n3pan Down Right", XPlaneCommands.GPSG1000n3PanDownRight); } }
        private XPlaneCommand GPSG1000n3PanPush { get { return new XPlaneCommand("sim/GPS/g1000n3_pan_push", "G1000 MFD push pan.", "GPSg1000n3pan Push", XPlaneCommands.GPSG1000n3PanPush); } }
        private XPlaneCommand GPSG1000n3Direct { get { return new XPlaneCommand("sim/GPS/g1000n3_direct", "G1000 MFD -D->.", "GPSg1000n3direct", XPlaneCommands.GPSG1000n3Direct); } }
        private XPlaneCommand GPSG1000n3Menu { get { return new XPlaneCommand("sim/GPS/g1000n3_menu", "G1000 MFD menu.", "GPSg1000n3menu", XPlaneCommands.GPSG1000n3Menu); } }
        private XPlaneCommand GPSG1000n3Fpl { get { return new XPlaneCommand("sim/GPS/g1000n3_fpl", "G1000 MFD FPL.", "GPSg1000n3fpl", XPlaneCommands.GPSG1000n3Fpl); } }
        private XPlaneCommand GPSG1000n3Proc { get { return new XPlaneCommand("sim/GPS/g1000n3_proc", "G1000 MFD proc.", "GPSg1000n3proc", XPlaneCommands.GPSG1000n3Proc); } }
        private XPlaneCommand GPSG1000n3Clr { get { return new XPlaneCommand("sim/GPS/g1000n3_clr", "G1000 MFD CLR.", "GPSg1000n3clr", XPlaneCommands.GPSG1000n3Clr); } }
        private XPlaneCommand GPSG1000n3Ent { get { return new XPlaneCommand("sim/GPS/g1000n3_ent", "G1000 MFD ENT.", "GPSg1000n3ent", XPlaneCommands.GPSG1000n3Ent); } }
        private XPlaneCommand GPSG1000n3FmsOuterUp { get { return new XPlaneCommand("sim/GPS/g1000n3_fms_outer_up", "G1000 MFD FMS outer ring tune up.", "GPSg1000n3fms Outer Up", XPlaneCommands.GPSG1000n3FmsOuterUp); } }
        private XPlaneCommand GPSG1000n3FmsOuterDown { get { return new XPlaneCommand("sim/GPS/g1000n3_fms_outer_down", "G1000 MFD FMS outer ring tune down.", "GPSg1000n3fms Outer Down", XPlaneCommands.GPSG1000n3FmsOuterDown); } }
        private XPlaneCommand GPSG1000n3FmsInnerUp { get { return new XPlaneCommand("sim/GPS/g1000n3_fms_inner_up", "G1000 MFD FMS inner ring tune up.", "GPSg1000n3fms Inner Up", XPlaneCommands.GPSG1000n3FmsInnerUp); } }
        private XPlaneCommand GPSG1000n3FmsInnerDown { get { return new XPlaneCommand("sim/GPS/g1000n3_fms_inner_down", "G1000 MFD FMS inner ring tune down.", "GPSg1000n3fms Inner Down", XPlaneCommands.GPSG1000n3FmsInnerDown); } }
        private XPlaneCommand GPSG1000n3Cursor { get { return new XPlaneCommand("sim/GPS/g1000n3_cursor", "G1000 MFD FMS cursor.", "GPSg1000n3cursor", XPlaneCommands.GPSG1000n3Cursor); } }
        private XPlaneCommand GPSG1000n3Popout { get { return new XPlaneCommand("sim/GPS/g1000n3_popout", "G1000 MFD pop out window.", "GPSg1000n3popout", XPlaneCommands.GPSG1000n3Popout); } }
        private XPlaneCommand GPSG1000n3Popup { get { return new XPlaneCommand("sim/GPS/g1000n3_popup", "G1000 MFD popup.", "GPSg1000n3popup", XPlaneCommands.GPSG1000n3Popup); } }
        private XPlaneCommand GPSGcu478A { get { return new XPlaneCommand("sim/GPS/gcu478/A", "GCU: A", "GPS Gcu478a", XPlaneCommands.GPSGcu478A); } }
        private XPlaneCommand GPSGcu478B { get { return new XPlaneCommand("sim/GPS/gcu478/B", "GCU: B", "GPS Gcu478b", XPlaneCommands.GPSGcu478B); } }
        private XPlaneCommand GPSGcu478C { get { return new XPlaneCommand("sim/GPS/gcu478/C", "GCU: C", "GPS Gcu478c", XPlaneCommands.GPSGcu478C); } }
        private XPlaneCommand GPSGcu478D { get { return new XPlaneCommand("sim/GPS/gcu478/D", "GCU: D", "GPS Gcu478d", XPlaneCommands.GPSGcu478D); } }
        private XPlaneCommand GPSGcu478E { get { return new XPlaneCommand("sim/GPS/gcu478/E", "GCU: E", "GPS Gcu478e", XPlaneCommands.GPSGcu478E); } }
        private XPlaneCommand GPSGcu478F { get { return new XPlaneCommand("sim/GPS/gcu478/F", "GCU: F", "GPS Gcu478f", XPlaneCommands.GPSGcu478F); } }
        private XPlaneCommand GPSGcu478G { get { return new XPlaneCommand("sim/GPS/gcu478/G", "GCU: G", "GPS Gcu478g", XPlaneCommands.GPSGcu478G); } }
        private XPlaneCommand GPSGcu478H { get { return new XPlaneCommand("sim/GPS/gcu478/H", "GCU: H", "GPS Gcu478h", XPlaneCommands.GPSGcu478H); } }
        private XPlaneCommand GPSGcu478I { get { return new XPlaneCommand("sim/GPS/gcu478/I", "GCU: I", "GPS Gcu478i", XPlaneCommands.GPSGcu478I); } }
        private XPlaneCommand GPSGcu478J { get { return new XPlaneCommand("sim/GPS/gcu478/J", "GCU: J", "GPS Gcu478j", XPlaneCommands.GPSGcu478J); } }
        private XPlaneCommand GPSGcu478K { get { return new XPlaneCommand("sim/GPS/gcu478/K", "GCU: K", "GPS Gcu478k", XPlaneCommands.GPSGcu478K); } }
        private XPlaneCommand GPSGcu478L { get { return new XPlaneCommand("sim/GPS/gcu478/L", "GCU: L", "GPS Gcu478l", XPlaneCommands.GPSGcu478L); } }
        private XPlaneCommand GPSGcu478M { get { return new XPlaneCommand("sim/GPS/gcu478/M", "GCU: M", "GPS Gcu478m", XPlaneCommands.GPSGcu478M); } }
        private XPlaneCommand GPSGcu478N { get { return new XPlaneCommand("sim/GPS/gcu478/N", "GCU: N", "GPS Gcu478n", XPlaneCommands.GPSGcu478N); } }
        private XPlaneCommand GPSGcu478O { get { return new XPlaneCommand("sim/GPS/gcu478/O", "GCU: O", "GPS Gcu478o", XPlaneCommands.GPSGcu478O); } }
        private XPlaneCommand GPSGcu478P { get { return new XPlaneCommand("sim/GPS/gcu478/P", "GCU: P", "GPS Gcu478p", XPlaneCommands.GPSGcu478P); } }
        private XPlaneCommand GPSGcu478Q { get { return new XPlaneCommand("sim/GPS/gcu478/Q", "GCU: Q", "GPS Gcu478q", XPlaneCommands.GPSGcu478Q); } }
        private XPlaneCommand GPSGcu478R { get { return new XPlaneCommand("sim/GPS/gcu478/R", "GCU: R", "GPS Gcu478r", XPlaneCommands.GPSGcu478R); } }
        private XPlaneCommand GPSGcu478S { get { return new XPlaneCommand("sim/GPS/gcu478/S", "GCU: S", "GPS Gcu478s", XPlaneCommands.GPSGcu478S); } }
        private XPlaneCommand GPSGcu478T { get { return new XPlaneCommand("sim/GPS/gcu478/T", "GCU: T", "GPS Gcu478t", XPlaneCommands.GPSGcu478T); } }
        private XPlaneCommand GPSGcu478U { get { return new XPlaneCommand("sim/GPS/gcu478/U", "GCU: U", "GPS Gcu478u", XPlaneCommands.GPSGcu478U); } }
        private XPlaneCommand GPSGcu478V { get { return new XPlaneCommand("sim/GPS/gcu478/V", "GCU: V", "GPS Gcu478v", XPlaneCommands.GPSGcu478V); } }
        private XPlaneCommand GPSGcu478W { get { return new XPlaneCommand("sim/GPS/gcu478/W", "GCU: W", "GPS Gcu478w", XPlaneCommands.GPSGcu478W); } }
        private XPlaneCommand GPSGcu478X { get { return new XPlaneCommand("sim/GPS/gcu478/X", "GCU: X", "GPS Gcu478x", XPlaneCommands.GPSGcu478X); } }
        private XPlaneCommand GPSGcu478Y { get { return new XPlaneCommand("sim/GPS/gcu478/Y", "GCU: Y", "GPS Gcu478y", XPlaneCommands.GPSGcu478Y); } }
        private XPlaneCommand GPSGcu478Z { get { return new XPlaneCommand("sim/GPS/gcu478/Z", "GCU: Z", "GPS Gcu478z", XPlaneCommands.GPSGcu478Z); } }
        private XPlaneCommand GPSGcu4780 { get { return new XPlaneCommand("sim/GPS/gcu478/0", "GCU: 0", "GPS Gcu4780", XPlaneCommands.GPSGcu4780); } }
        private XPlaneCommand GPSGcu4781 { get { return new XPlaneCommand("sim/GPS/gcu478/1", "GCU: 1", "GPS Gcu4781", XPlaneCommands.GPSGcu4781); } }
        private XPlaneCommand GPSGcu4782 { get { return new XPlaneCommand("sim/GPS/gcu478/2", "GCU: 2", "GPS Gcu4782", XPlaneCommands.GPSGcu4782); } }
        private XPlaneCommand GPSGcu4783 { get { return new XPlaneCommand("sim/GPS/gcu478/3", "GCU: 3", "GPS Gcu4783", XPlaneCommands.GPSGcu4783); } }
        private XPlaneCommand GPSGcu4784 { get { return new XPlaneCommand("sim/GPS/gcu478/4", "GCU: 4", "GPS Gcu4784", XPlaneCommands.GPSGcu4784); } }
        private XPlaneCommand GPSGcu4785 { get { return new XPlaneCommand("sim/GPS/gcu478/5", "GCU: 5", "GPS Gcu4785", XPlaneCommands.GPSGcu4785); } }
        private XPlaneCommand GPSGcu4786 { get { return new XPlaneCommand("sim/GPS/gcu478/6", "GCU: 6", "GPS Gcu4786", XPlaneCommands.GPSGcu4786); } }
        private XPlaneCommand GPSGcu4787 { get { return new XPlaneCommand("sim/GPS/gcu478/7", "GCU: 7", "GPS Gcu4787", XPlaneCommands.GPSGcu4787); } }
        private XPlaneCommand GPSGcu4788 { get { return new XPlaneCommand("sim/GPS/gcu478/8", "GCU: 8", "GPS Gcu4788", XPlaneCommands.GPSGcu4788); } }
        private XPlaneCommand GPSGcu4789 { get { return new XPlaneCommand("sim/GPS/gcu478/9", "GCU: 9", "GPS Gcu4789", XPlaneCommands.GPSGcu4789); } }
        private XPlaneCommand GPSGcu478Dot { get { return new XPlaneCommand("sim/GPS/gcu478/dot", "GCU: dot", "GPS Gcu478dot", XPlaneCommands.GPSGcu478Dot); } }
        private XPlaneCommand GPSGcu478Minus { get { return new XPlaneCommand("sim/GPS/gcu478/minus", "GCU: minus", "GPS Gcu478minus", XPlaneCommands.GPSGcu478Minus); } }
        private XPlaneCommand GPSGcu478Spc { get { return new XPlaneCommand("sim/GPS/gcu478/spc", "GCU: space", "GPS Gcu478spc", XPlaneCommands.GPSGcu478Spc); } }
        private XPlaneCommand GPSGcu478Bksp { get { return new XPlaneCommand("sim/GPS/gcu478/bksp", "GCU: backspace", "GPS Gcu478bksp", XPlaneCommands.GPSGcu478Bksp); } }
        private XPlaneCommand GPSGcu478HdgUp { get { return new XPlaneCommand("sim/GPS/gcu478/hdg_up", "GCU: HDG up.", "GPS Gcu478hdg Up", XPlaneCommands.GPSGcu478HdgUp); } }
        private XPlaneCommand GPSGcu478HdgDown { get { return new XPlaneCommand("sim/GPS/gcu478/hdg_down", "GCU: HDG down.", "GPS Gcu478hdg Down", XPlaneCommands.GPSGcu478HdgDown); } }
        private XPlaneCommand GPSGcu478HdgSync { get { return new XPlaneCommand("sim/GPS/gcu478/hdg_sync", "GCU: HDG sync.", "GPS Gcu478hdg Sync", XPlaneCommands.GPSGcu478HdgSync); } }
        private XPlaneCommand GPSGcu478CrsUp { get { return new XPlaneCommand("sim/GPS/gcu478/crs_up", "GCU: CRS up.", "GPS Gcu478crs Up", XPlaneCommands.GPSGcu478CrsUp); } }
        private XPlaneCommand GPSGcu478CrsDown { get { return new XPlaneCommand("sim/GPS/gcu478/crs_down", "GCU: CRS down.", "GPS Gcu478crs Down", XPlaneCommands.GPSGcu478CrsDown); } }
        private XPlaneCommand GPSGcu478CrsSync { get { return new XPlaneCommand("sim/GPS/gcu478/crs_sync", "GCU: CRS sync.", "GPS Gcu478crs Sync", XPlaneCommands.GPSGcu478CrsSync); } }
        private XPlaneCommand GPSGcu478AltUp { get { return new XPlaneCommand("sim/GPS/gcu478/alt_up", "GCU: altitude up.", "GPS Gcu478alt Up", XPlaneCommands.GPSGcu478AltUp); } }
        private XPlaneCommand GPSGcu478AltDown { get { return new XPlaneCommand("sim/GPS/gcu478/alt_down", "GCU: altitude down.", "GPS Gcu478alt Down", XPlaneCommands.GPSGcu478AltDown); } }
        private XPlaneCommand GPSGcu478AltSync { get { return new XPlaneCommand("sim/GPS/gcu478/alt_sync", "GCU: altitude sync.", "GPS Gcu478alt Sync", XPlaneCommands.GPSGcu478AltSync); } }
        private XPlaneCommand GPSGcu478RangeUp { get { return new XPlaneCommand("sim/GPS/gcu478/range_up", "GCU: range up.", "GPS Gcu478range Up", XPlaneCommands.GPSGcu478RangeUp); } }
        private XPlaneCommand GPSGcu478RangeDown { get { return new XPlaneCommand("sim/GPS/gcu478/range_down", "GCU: range down.", "GPS Gcu478range Down", XPlaneCommands.GPSGcu478RangeDown); } }
        private XPlaneCommand GPSGcu478PanUp { get { return new XPlaneCommand("sim/GPS/gcu478/pan_up", "GCU: pan up.", "GPS Gcu478pan Up", XPlaneCommands.GPSGcu478PanUp); } }
        private XPlaneCommand GPSGcu478PanDown { get { return new XPlaneCommand("sim/GPS/gcu478/pan_down", "GCU: pan down.", "GPS Gcu478pan Down", XPlaneCommands.GPSGcu478PanDown); } }
        private XPlaneCommand GPSGcu478PanLeft { get { return new XPlaneCommand("sim/GPS/gcu478/pan_left", "GCU: pan left.", "GPS Gcu478pan Left", XPlaneCommands.GPSGcu478PanLeft); } }
        private XPlaneCommand GPSGcu478PanRight { get { return new XPlaneCommand("sim/GPS/gcu478/pan_right", "GCU: pan right.", "GPS Gcu478pan Right", XPlaneCommands.GPSGcu478PanRight); } }
        private XPlaneCommand GPSGcu478PanUpLeft { get { return new XPlaneCommand("sim/GPS/gcu478/pan_up_left", "GCU: pan up left.", "GPS Gcu478pan Up Left", XPlaneCommands.GPSGcu478PanUpLeft); } }
        private XPlaneCommand GPSGcu478PanDownLeft { get { return new XPlaneCommand("sim/GPS/gcu478/pan_down_left", "GCU: pan down left.", "GPS Gcu478pan Down Left", XPlaneCommands.GPSGcu478PanDownLeft); } }
        private XPlaneCommand GPSGcu478PanUpRight { get { return new XPlaneCommand("sim/GPS/gcu478/pan_up_right", "GCU: pan up right.", "GPS Gcu478pan Up Right", XPlaneCommands.GPSGcu478PanUpRight); } }
        private XPlaneCommand GPSGcu478PanDownRight { get { return new XPlaneCommand("sim/GPS/gcu478/pan_down_right", "GCU: pan down right.", "GPS Gcu478pan Down Right", XPlaneCommands.GPSGcu478PanDownRight); } }
        private XPlaneCommand GPSGcu478PanPush { get { return new XPlaneCommand("sim/GPS/gcu478/pan_push", "GCU: push pan.", "GPS Gcu478pan Push", XPlaneCommands.GPSGcu478PanPush); } }
        private XPlaneCommand GPSGcu478Fms { get { return new XPlaneCommand("sim/GPS/gcu478/fms", "GCU: FMS", "GPS Gcu478fms", XPlaneCommands.GPSGcu478Fms); } }
        private XPlaneCommand GPSGcu478Xpdr { get { return new XPlaneCommand("sim/GPS/gcu478/xpdr", "GCU: XPDR", "GPS Gcu478xpdr", XPlaneCommands.GPSGcu478Xpdr); } }
        private XPlaneCommand GPSGcu478Com { get { return new XPlaneCommand("sim/GPS/gcu478/com", "GCU: COM", "GPS Gcu478com", XPlaneCommands.GPSGcu478Com); } }
        private XPlaneCommand GPSGcu478Nav { get { return new XPlaneCommand("sim/GPS/gcu478/nav", "GCU: NAV", "GPS Gcu478nav", XPlaneCommands.GPSGcu478Nav); } }
        private XPlaneCommand GPSGcu478Ff { get { return new XPlaneCommand("sim/GPS/gcu478/ff", "GCU: COM/NAV flip flop.", "GPS Gcu478ff", XPlaneCommands.GPSGcu478Ff); } }
        private XPlaneCommand GPSGcu478Direct { get { return new XPlaneCommand("sim/GPS/gcu478/direct", "GCU: -D->.", "GPS Gcu478direct", XPlaneCommands.GPSGcu478Direct); } }
        private XPlaneCommand GPSGcu478Menu { get { return new XPlaneCommand("sim/GPS/gcu478/menu", "GCU: menu.", "GPS Gcu478menu", XPlaneCommands.GPSGcu478Menu); } }
        private XPlaneCommand GPSGcu478Fpl { get { return new XPlaneCommand("sim/GPS/gcu478/fpl", "GCU: FPL.", "GPS Gcu478fpl", XPlaneCommands.GPSGcu478Fpl); } }
        private XPlaneCommand GPSGcu478Proc { get { return new XPlaneCommand("sim/GPS/gcu478/proc", "GCU: proc.", "GPS Gcu478proc", XPlaneCommands.GPSGcu478Proc); } }
        private XPlaneCommand GPSGcu478Clr { get { return new XPlaneCommand("sim/GPS/gcu478/clr", "GCU: CLR.", "GPS Gcu478clr", XPlaneCommands.GPSGcu478Clr); } }
        private XPlaneCommand GPSGcu478Ent { get { return new XPlaneCommand("sim/GPS/gcu478/ent", "GCU: ENT.", "GPS Gcu478ent", XPlaneCommands.GPSGcu478Ent); } }
        private XPlaneCommand GPSGcu478OuterUp { get { return new XPlaneCommand("sim/GPS/gcu478/outer_up", "GCU: outer ring tune up.", "GPS Gcu478outer Up", XPlaneCommands.GPSGcu478OuterUp); } }
        private XPlaneCommand GPSGcu478OuterDown { get { return new XPlaneCommand("sim/GPS/gcu478/outer_down", "GCU: outer ring tune down.", "GPS Gcu478outer Down", XPlaneCommands.GPSGcu478OuterDown); } }
        private XPlaneCommand GPSGcu478InnerUp { get { return new XPlaneCommand("sim/GPS/gcu478/inner_up", "GCU: inner ring tune up.", "GPS Gcu478inner Up", XPlaneCommands.GPSGcu478InnerUp); } }
        private XPlaneCommand GPSGcu478InnerDown { get { return new XPlaneCommand("sim/GPS/gcu478/inner_down", "GCU: inner ring tune down.", "GPS Gcu478inner Down", XPlaneCommands.GPSGcu478InnerDown); } }
        private XPlaneCommand GPSGcu478Cursor { get { return new XPlaneCommand("sim/GPS/gcu478/cursor", "GCU: cursor/1-2.", "GPS Gcu478cursor", XPlaneCommands.GPSGcu478Cursor); } }
        private XPlaneCommand GPSG1000DisplayReversion { get { return new XPlaneCommand("sim/GPS/G1000_display_reversion", "G1000 red button for display reversion (backup display).", "GPSg1000display Reversion", XPlaneCommands.GPSG1000DisplayReversion); } }
        private XPlaneCommand SystemsOverspeedTest { get { return new XPlaneCommand("sim/systems/overspeed_test", "Prop overspeed test.", "Systems Overspeed Test", XPlaneCommands.SystemsOverspeedTest); } }
        private XPlaneCommand FuelIndicateAux { get { return new XPlaneCommand("sim/fuel/indicate_aux", "Fuel tanks show auxiliary tanks.", "Fuel Indicate Aux", XPlaneCommands.FuelIndicateAux); } }
        private XPlaneCommand FuelIndicateAll { get { return new XPlaneCommand("sim/fuel/indicate_all", "Fuel tanks show all tanks.", "Fuel Indicate All", XPlaneCommands.FuelIndicateAll); } }
        private XPlaneCommand FuelIndicateNacelle { get { return new XPlaneCommand("sim/fuel/indicate_nacelle", "Fuel tanks show nacelle tanks.", "Fuel Indicate Nacelle", XPlaneCommands.FuelIndicateNacelle); } }
        private XPlaneCommand AutopilotTestAutoAnnunciators { get { return new XPlaneCommand("sim/autopilot/test_auto_annunciators", "Autopilot test annunciators.", "Autopilot Test Auto Annunciators", XPlaneCommands.AutopilotTestAutoAnnunciators); } }
        private XPlaneCommand FlightControlsPitchTrimaUp { get { return new XPlaneCommand("sim/flight_controls/pitch_trimA_up", "Pitch trim A up.", "Flight Controls Pitch Trima Up", XPlaneCommands.FlightControlsPitchTrimaUp); } }
        private XPlaneCommand FlightControlsPitchTrimaDown { get { return new XPlaneCommand("sim/flight_controls/pitch_trimA_down", "Pitch trim A down.", "Flight Controls Pitch Trima Down", XPlaneCommands.FlightControlsPitchTrimaDown); } }
        private XPlaneCommand FlightControlsPitchTrimbUp { get { return new XPlaneCommand("sim/flight_controls/pitch_trimB_up", "Pitch trim B up.", "Flight Controls Pitch Trimb Up", XPlaneCommands.FlightControlsPitchTrimbUp); } }
        private XPlaneCommand FlightControlsPitchTrimbDown { get { return new XPlaneCommand("sim/flight_controls/pitch_trimB_down", "Pitch trim B down.", "Flight Controls Pitch Trimb Down", XPlaneCommands.FlightControlsPitchTrimbDown); } }
        private XPlaneCommand FlightControlsPitchTrimUp { get { return new XPlaneCommand("sim/flight_controls/pitch_trim_up", "Pitch trim up.", "Flight Controls Pitch Trim Up", XPlaneCommands.FlightControlsPitchTrimUp); } }
        private XPlaneCommand FlightControlsPitchTrimDown { get { return new XPlaneCommand("sim/flight_controls/pitch_trim_down", "Pitch trim down.", "Flight Controls Pitch Trim Down", XPlaneCommands.FlightControlsPitchTrimDown); } }
        private XPlaneCommand FlightControlsPitchTrimUpMech { get { return new XPlaneCommand("sim/flight_controls/pitch_trim_up_mech", "Pitch trim up - Mechanical, not servo.", "Flight Controls Pitch Trim Up Mech", XPlaneCommands.FlightControlsPitchTrimUpMech); } }
        private XPlaneCommand FlightControlsPitchTrimDownMech { get { return new XPlaneCommand("sim/flight_controls/pitch_trim_down_mech", "Pitch trim down - Mechanical, not servo.", "Flight Controls Pitch Trim Down Mech", XPlaneCommands.FlightControlsPitchTrimDownMech); } }
        private XPlaneCommand FlightControlsAileronTrimLeft { get { return new XPlaneCommand("sim/flight_controls/aileron_trim_left", "Roll trim left.", "Flight Controls Aileron Trim Left", XPlaneCommands.FlightControlsAileronTrimLeft); } }
        private XPlaneCommand FlightControlsAileronTrimRight { get { return new XPlaneCommand("sim/flight_controls/aileron_trim_right", "Roll trim right.", "Flight Controls Aileron Trim Right", XPlaneCommands.FlightControlsAileronTrimRight); } }
        private XPlaneCommand FlightControlsRudderTrimLeft { get { return new XPlaneCommand("sim/flight_controls/rudder_trim_left", "Yaw trim left.", "Flight Controls Rudder Trim Left", XPlaneCommands.FlightControlsRudderTrimLeft); } }
        private XPlaneCommand FlightControlsRudderTrimRight { get { return new XPlaneCommand("sim/flight_controls/rudder_trim_right", "Yaw trim right.", "Flight Controls Rudder Trim Right", XPlaneCommands.FlightControlsRudderTrimRight); } }
        private XPlaneCommand FlightControlsGyroRotorTrimUp { get { return new XPlaneCommand("sim/flight_controls/gyro_rotor_trim_up", "Gyro rotor trim up.", "Flight Controls Gyro Rotor Trim Up", XPlaneCommands.FlightControlsGyroRotorTrimUp); } }
        private XPlaneCommand FlightControlsGyroRotorTrimDown { get { return new XPlaneCommand("sim/flight_controls/gyro_rotor_trim_down", "Gyro rotor trim down.", "Flight Controls Gyro Rotor Trim Down", XPlaneCommands.FlightControlsGyroRotorTrimDown); } }
        private XPlaneCommand FlightControlsRotorRpmTrimUp { get { return new XPlaneCommand("sim/flight_controls/rotor_rpm_trim_up", "Rotor RPM trim up.", "Flight Controls Rotor Rpm Trim Up", XPlaneCommands.FlightControlsRotorRpmTrimUp); } }
        private XPlaneCommand FlightControlsRotorRpmTrimDown { get { return new XPlaneCommand("sim/flight_controls/rotor_rpm_trim_down", "Rotor RPM trim down.", "Flight Controls Rotor Rpm Trim Down", XPlaneCommands.FlightControlsRotorRpmTrimDown); } }
        private XPlaneCommand FlightControlsMagneticLock { get { return new XPlaneCommand("sim/flight_controls/magnetic_lock", "Controls magnetic lock.", "Flight Controls Magnetic Lock", XPlaneCommands.FlightControlsMagneticLock); } }
        private XPlaneCommand FlightControlsPitchTrimTakeoff { get { return new XPlaneCommand("sim/flight_controls/pitch_trim_takeoff", "Pitch trim takeoff.", "Flight Controls Pitch Trim Takeoff", XPlaneCommands.FlightControlsPitchTrimTakeoff); } }
        private XPlaneCommand FlightControlsAileronTrimCenter { get { return new XPlaneCommand("sim/flight_controls/aileron_trim_center", "Aileron trim center.", "Flight Controls Aileron Trim Center", XPlaneCommands.FlightControlsAileronTrimCenter); } }
        private XPlaneCommand FlightControlsRudderTrimCenter { get { return new XPlaneCommand("sim/flight_controls/rudder_trim_center", "Rudder trim center.", "Flight Controls Rudder Trim Center", XPlaneCommands.FlightControlsRudderTrimCenter); } }
        private XPlaneCommand FlightControlsRudderLft { get { return new XPlaneCommand("sim/flight_controls/rudder_lft", "Rudder left.", "Flight Controls Rudder Lft", XPlaneCommands.FlightControlsRudderLft); } }
        private XPlaneCommand FlightControlsRudderCtr { get { return new XPlaneCommand("sim/flight_controls/rudder_ctr", "Rudder center.", "Flight Controls Rudder Ctr", XPlaneCommands.FlightControlsRudderCtr); } }
        private XPlaneCommand FlightControlsRudderRgt { get { return new XPlaneCommand("sim/flight_controls/rudder_rgt", "Rudder right.", "Flight Controls Rudder Rgt", XPlaneCommands.FlightControlsRudderRgt); } }
        private XPlaneCommand AutopilotSetOttSeldispALTVVIVvi { get { return new XPlaneCommand("sim/autopilot/set_ott_seldisp_ALT_VVI_vvi", "Let ott_seldisp_ALT_VVI show VVI.", "Autopilot Set Ott Seldisp ALTVVI VVI", XPlaneCommands.AutopilotSetOttSeldispALTVVIVvi); } }
        private XPlaneCommand AutopilotSetOttSeldispALTVVIAlt { get { return new XPlaneCommand("sim/autopilot/set_ott_seldisp_ALT_VVI_alt", "Let ott_seldisp_ALT_VVI show ALT.", "Autopilot Set Ott Seldisp ALTVVI Alt", XPlaneCommands.AutopilotSetOttSeldispALTVVIAlt); } }
        private XPlaneCommand OperationGroundSpeedChange { get { return new XPlaneCommand("sim/operation/ground_speed_change", "Sim 1x 2x 4x ground speed.", "Operation Ground Speed Change", XPlaneCommands.OperationGroundSpeedChange); } }
        private XPlaneCommand OperationFreezeToggle { get { return new XPlaneCommand("sim/operation/freeze_toggle", "Freeze the groundspeed of the simulation.", "Operation Freeze Toggle", XPlaneCommands.OperationFreezeToggle); } }
        private XPlaneCommand InstrumentsTimerStartStop { get { return new XPlaneCommand("sim/instruments/timer_start_stop", "Start or stop the timer.", "Instruments Timer Start Stop", XPlaneCommands.InstrumentsTimerStartStop); } }
        private XPlaneCommand InstrumentsTimerReset { get { return new XPlaneCommand("sim/instruments/timer_reset", "Reset the timer.", "Instruments Timer Reset", XPlaneCommands.InstrumentsTimerReset); } }
        private XPlaneCommand InstrumentsTimerShowDate { get { return new XPlaneCommand("sim/instruments/timer_show_date", "Show date on the chrono.", "Instruments Timer Show Date", XPlaneCommands.InstrumentsTimerShowDate); } }
        private XPlaneCommand InstrumentsTimerMode { get { return new XPlaneCommand("sim/instruments/timer_mode", "Timer/clock mode for chronos.", "Instruments Timer Mode", XPlaneCommands.InstrumentsTimerMode); } }
        private XPlaneCommand InstrumentsTimerCycle { get { return new XPlaneCommand("sim/instruments/timer_cycle", "Timer start/stop/reset.", "Instruments Timer Cycle", XPlaneCommands.InstrumentsTimerCycle); } }
        private XPlaneCommand OperationTimeDown { get { return new XPlaneCommand("sim/operation/time_down", "Time: a little earlier.", "Operation Time Down", XPlaneCommands.OperationTimeDown); } }
        private XPlaneCommand OperationTimeUp { get { return new XPlaneCommand("sim/operation/time_up", "Time: a little later.", "Operation Time Up", XPlaneCommands.OperationTimeUp); } }
        private XPlaneCommand OperationTimeDownLots { get { return new XPlaneCommand("sim/operation/time_down_lots", "Time: a lot earlier.", "Operation Time Down Lots", XPlaneCommands.OperationTimeDownLots); } }
        private XPlaneCommand OperationTimeUpLots { get { return new XPlaneCommand("sim/operation/time_up_lots", "Time: a lot later.", "Operation Time Up Lots", XPlaneCommands.OperationTimeUpLots); } }
        private XPlaneCommand OperationDateDown { get { return new XPlaneCommand("sim/operation/date_down", "Date: a little earlier.", "Operation Date Down", XPlaneCommands.OperationDateDown); } }
        private XPlaneCommand OperationDateUp { get { return new XPlaneCommand("sim/operation/date_up", "Date: a little later.", "Operation Date Up", XPlaneCommands.OperationDateUp); } }
        private XPlaneCommand InstrumentsTimerIsGMT { get { return new XPlaneCommand("sim/instruments/timer_is_GMT", "Timer is GMT.", "Instruments Timer Is GMT", XPlaneCommands.InstrumentsTimerIsGMT); } }
        private XPlaneCommand OperationFlightmodelSpeedChange { get { return new XPlaneCommand("sim/operation/flightmodel_speed_change", "Sim 1x 2x 4x sim speed.", "Operation Flightmodel Speed Change", XPlaneCommands.OperationFlightmodelSpeedChange); } }
        private XPlaneCommand OperationPauseToggle { get { return new XPlaneCommand("sim/operation/pause_toggle", "Pause the simulation.", "Operation Pause Toggle", XPlaneCommands.OperationPauseToggle); } }
        private XPlaneCommand OperationVideoRecordToggle { get { return new XPlaneCommand("sim/operation/video_record_toggle", "Toggle AVI movie recording.", "Operation Video Record Toggle", XPlaneCommands.OperationVideoRecordToggle); } }
        private XPlaneCommand OperationConfigureVideoRecording { get { return new XPlaneCommand("sim/operation/configure_video_recording", "Configure AVI movie recording.", "Operation Configure Video Recording", XPlaneCommands.OperationConfigureVideoRecording); } }
        private XPlaneCommand ReplayReplayToggle { get { return new XPlaneCommand("sim/replay/replay_toggle", "Toggle replay mode on/off.", "Replay Replay Toggle", XPlaneCommands.ReplayReplayToggle); } }
        private XPlaneCommand ReplayReplayOff { get { return new XPlaneCommand("sim/replay/replay_off", "Replay mode off.", "Replay Replay Off", XPlaneCommands.ReplayReplayOff); } }
        private XPlaneCommand ReplayReplayControlsToggle { get { return new XPlaneCommand("sim/replay/replay_controls_toggle", "Replay mode: Toggle controls visibility.", "Replay Replay Controls Toggle", XPlaneCommands.ReplayReplayControlsToggle); } }
        private XPlaneCommand ReplayRepBegin { get { return new XPlaneCommand("sim/replay/rep_begin", "Replay mode: go to beginning.", "Replay Rep Begin", XPlaneCommands.ReplayRepBegin); } }
        private XPlaneCommand ReplayRepPlayFr { get { return new XPlaneCommand("sim/replay/rep_play_fr", "Replay mode: play fast reverse.", "Replay Rep Play Fr", XPlaneCommands.ReplayRepPlayFr); } }
        private XPlaneCommand ReplayRepPlayRr { get { return new XPlaneCommand("sim/replay/rep_play_rr", "Replay mode: play reverse.", "Replay Rep Play Rr", XPlaneCommands.ReplayRepPlayRr); } }
        private XPlaneCommand ReplayRepPlaySr { get { return new XPlaneCommand("sim/replay/rep_play_sr", "Replay mode: play slow reverse.", "Replay Rep Play Sr", XPlaneCommands.ReplayRepPlaySr); } }
        private XPlaneCommand ReplayRepPause { get { return new XPlaneCommand("sim/replay/rep_pause", "Replay mode: pause.", "Replay Rep Pause", XPlaneCommands.ReplayRepPause); } }
        private XPlaneCommand ReplayRepPlaySf { get { return new XPlaneCommand("sim/replay/rep_play_sf", "Replay mode: play slow forward.", "Replay Rep Play Sf", XPlaneCommands.ReplayRepPlaySf); } }
        private XPlaneCommand ReplayRepPlayRf { get { return new XPlaneCommand("sim/replay/rep_play_rf", "Replay mode: play forward.", "Replay Rep Play Rf", XPlaneCommands.ReplayRepPlayRf); } }
        private XPlaneCommand ReplayRepPlayFf { get { return new XPlaneCommand("sim/replay/rep_play_ff", "Replay mode: play fast forward.", "Replay Rep Play Ff", XPlaneCommands.ReplayRepPlayFf); } }
        private XPlaneCommand ReplayRepEnd { get { return new XPlaneCommand("sim/replay/rep_end", "Replay mode: go to end.", "Replay Rep End", XPlaneCommands.ReplayRepEnd); } }
        private XPlaneCommand OperationToggleLogbook { get { return new XPlaneCommand("sim/operation/toggle_logbook", "Toggle logbook window.", "Operation Toggle Logbook", XPlaneCommands.OperationToggleLogbook); } }
        private XPlaneCommand OperationSaveFlight { get { return new XPlaneCommand("sim/operation/save_flight", "Toggle Save Flight window.", "Operation Save Flight", XPlaneCommands.OperationSaveFlight); } }
        private XPlaneCommand OperationLoadFlight { get { return new XPlaneCommand("sim/operation/load_flight", "Toggle Load Flight window.", "Operation Load Flight", XPlaneCommands.OperationLoadFlight); } }
        private XPlaneCommand OperationTextFileToggle { get { return new XPlaneCommand("sim/operation/text_file_toggle", "Toggle text file.", "Operation Text File Toggle", XPlaneCommands.OperationTextFileToggle); } }
        private XPlaneCommand OperationChecklistToggle { get { return new XPlaneCommand("sim/operation/checklist_toggle", "Toggle checklist.", "Operation Checklist Toggle", XPlaneCommands.OperationChecklistToggle); } }
        private XPlaneCommand OperationChecklistNext { get { return new XPlaneCommand("sim/operation/checklist_next", "Next item in checklist.", "Operation Checklist Next", XPlaneCommands.OperationChecklistNext); } }
        private XPlaneCommand OperationChecklistPrevious { get { return new XPlaneCommand("sim/operation/checklist_previous", "Previous item in checklist.", "Operation Checklist Previous", XPlaneCommands.OperationChecklistPrevious); } }
        private XPlaneCommand OperationContactAtc { get { return new XPlaneCommand("sim/operation/contact_atc", "Contact ATC.", "Operation Contact ATC", XPlaneCommands.OperationContactAtc); } }
        private XPlaneCommand OperationToggleAiFlies { get { return new XPlaneCommand("sim/operation/toggle_ai_flies", "Toggle AI flying your aircraft.", "Operation Toggle AI Flies", XPlaneCommands.OperationToggleAiFlies); } }
        private XPlaneCommand OperationToggleYoke { get { return new XPlaneCommand("sim/operation/toggle_yoke", "Toggle yoke visibility.", "Operation Toggle Yoke", XPlaneCommands.OperationToggleYoke); } }
        private XPlaneCommand OperationResetFlight { get { return new XPlaneCommand("sim/operation/reset_flight", "Reset flight to most recent start.", "Operation Reset Flight", XPlaneCommands.OperationResetFlight); } }
        private XPlaneCommand OperationGoToDefault { get { return new XPlaneCommand("sim/operation/go_to_default", "Reset flight to nearest airport.", "Operation Go To Default", XPlaneCommands.OperationGoToDefault); } }
        private XPlaneCommand OperationResetToRunway { get { return new XPlaneCommand("sim/operation/reset_to_runway", "Reset flight to nearest runway.", "Operation Reset To Runway", XPlaneCommands.OperationResetToRunway); } }
        private XPlaneCommand OperationGoNextRunway { get { return new XPlaneCommand("sim/operation/go_next_runway", "Reset flight to next runway on current airport", "Operation Go Next Runway", XPlaneCommands.OperationGoNextRunway); } }
        private XPlaneCommand OperationGrassFieldTakeoff { get { return new XPlaneCommand("sim/operation/Grass_Field_Takeoff", "Grass field takeoff.", "Operation Grass Field Takeoff", XPlaneCommands.OperationGrassFieldTakeoff); } }
        private XPlaneCommand OperationDirtFieldTakeoff { get { return new XPlaneCommand("sim/operation/Dirt_Field_Takeoff", "Dirt field takeoff.", "Operation Dirt Field Takeoff", XPlaneCommands.OperationDirtFieldTakeoff); } }
        private XPlaneCommand OperationGravelFieldTakeoff { get { return new XPlaneCommand("sim/operation/Gravel_Field_Takeoff", "Gravel field takeoff.", "Operation Gravel Field Takeoff", XPlaneCommands.OperationGravelFieldTakeoff); } }
        private XPlaneCommand OperationWaterWayTakeoff { get { return new XPlaneCommand("sim/operation/Water_Way_Takeoff", "Waterway takeoff.", "Operation Water Way Takeoff", XPlaneCommands.OperationWaterWayTakeoff); } }
        private XPlaneCommand OperationHelipadTakeoff { get { return new XPlaneCommand("sim/operation/Helipad_Takeoff", "Helipad takeoff.", "Operation Helipad Takeoff", XPlaneCommands.OperationHelipadTakeoff); } }
        private XPlaneCommand OperationCarrierCatshot { get { return new XPlaneCommand("sim/operation/Carrier_Catshot", "Carrier catshot.", "Operation Carrier Catshot", XPlaneCommands.OperationCarrierCatshot); } }
        private XPlaneCommand OperationGliderWinch { get { return new XPlaneCommand("sim/operation/Glider_Winch", "Glider winch start.", "Operation Glider Winch", XPlaneCommands.OperationGliderWinch); } }
        private XPlaneCommand OperationGliderTow { get { return new XPlaneCommand("sim/operation/Glider_Tow", "Glider tow start.", "Operation Glider Tow", XPlaneCommands.OperationGliderTow); } }
        private XPlaneCommand OperationAirDropFromB52 { get { return new XPlaneCommand("sim/operation/Air_Drop_from_B_52", "Airdrop from B-52.", "Operation Air Drop From B52", XPlaneCommands.OperationAirDropFromB52); } }
        private XPlaneCommand OperationStartCarried { get { return new XPlaneCommand("sim/operation/start_carried", "Other aircraft carries your own.", "Operation Start Carried", XPlaneCommands.OperationStartCarried); } }
        private XPlaneCommand OperationPiggybackShuttleOn747 { get { return new XPlaneCommand("sim/operation/Piggyback_Shuttle_on_747", "Piggyback Shuttle on 747.", "Operation Piggyback Shuttle On747", XPlaneCommands.OperationPiggybackShuttleOn747); } }
        private XPlaneCommand OperationCarryOtherAircraft { get { return new XPlaneCommand("sim/operation/carry_other_aircraft", "Carry another aircraft.", "Operation Carry Other Aircraft", XPlaneCommands.OperationCarryOtherAircraft); } }
        private XPlaneCommand OperationFormationFlying { get { return new XPlaneCommand("sim/operation/Formation_Flying", "Formation flying.", "Operation Formation Flying", XPlaneCommands.OperationFormationFlying); } }
        private XPlaneCommand OperationAirRefuelBoom { get { return new XPlaneCommand("sim/operation/Air_Refuel_Boom", "Air refueling (boom).", "Operation Air Refuel Boom", XPlaneCommands.OperationAirRefuelBoom); } }
        private XPlaneCommand OperationAirRefuelBasket { get { return new XPlaneCommand("sim/operation/Air_Refuel_Basket", "Air refueling (basket).", "Operation Air Refuel Basket", XPlaneCommands.OperationAirRefuelBasket); } }
        private XPlaneCommand OperationAircraftCarrierApproach { get { return new XPlaneCommand("sim/operation/Aircraft_Carrier_Approach", "Aircraft carrier approach.", "Operation Aircraft Carrier Approach", XPlaneCommands.OperationAircraftCarrierApproach); } }
        private XPlaneCommand OperationFrigateApproach { get { return new XPlaneCommand("sim/operation/Frigate_Approach", "Frigate approach.", "Operation Frigate Approach", XPlaneCommands.OperationFrigateApproach); } }
        private XPlaneCommand OperationMediumOilRigApproach { get { return new XPlaneCommand("sim/operation/Medium_Oil_Rig_Approach", "Medium oil rig approach.", "Operation Medium Oil Rig Approach", XPlaneCommands.OperationMediumOilRigApproach); } }
        private XPlaneCommand OperationLargeOilPlatformApproach { get { return new XPlaneCommand("sim/operation/Large_Oil_Platform_Approach", "Large oil platform approach.", "Operation Large Oil Platform Approach", XPlaneCommands.OperationLargeOilPlatformApproach); } }
        private XPlaneCommand OperationForestFireApproach { get { return new XPlaneCommand("sim/operation/Forest_Fire_Approach", "Forest fire approach.", "Operation Forest Fire Approach", XPlaneCommands.OperationForestFireApproach); } }
        private XPlaneCommand OperationSpaceShuttleFullReEntry { get { return new XPlaneCommand("sim/operation/Space_Shuttle_Full_Re_entry", "Space Shuttle: Full re-entry.", "Operation Space Shuttle Full Re Entry", XPlaneCommands.OperationSpaceShuttleFullReEntry); } }
        private XPlaneCommand OperationSpaceShuttleFinalReEntry { get { return new XPlaneCommand("sim/operation/Space_Shuttle_Final_Re_entry", "Space Shuttle: Final re-entry.", "Operation Space Shuttle Final Re Entry", XPlaneCommands.OperationSpaceShuttleFinalReEntry); } }
        private XPlaneCommand OperationSpaceShuttleFullApproach { get { return new XPlaneCommand("sim/operation/Space_Shuttle_Full_Approach", "Space Shuttle: Full approach.", "Operation Space Shuttle Full Approach", XPlaneCommands.OperationSpaceShuttleFullApproach); } }
        private XPlaneCommand OperationSpaceShuttleFinalApproach { get { return new XPlaneCommand("sim/operation/Space_Shuttle_Final_Approach", "Space Shuttle: Final approach.", "Operation Space Shuttle Final Approach", XPlaneCommands.OperationSpaceShuttleFinalApproach); } }
        private XPlaneCommand ViewAiControlsViews { get { return new XPlaneCommand("sim/view/ai_controls_views", "Toggle AI controls your views.", "View AI Controls Views", XPlaneCommands.ViewAiControlsViews); } }
        private XPlaneCommand ViewFreeCamera { get { return new XPlaneCommand("sim/view/free_camera", "Free camera.", "View Free Camera", XPlaneCommands.ViewFreeCamera); } }
        private XPlaneCommand ViewDefaultView { get { return new XPlaneCommand("sim/view/default_view", "Default view.", "View Default View", XPlaneCommands.ViewDefaultView); } }
        private XPlaneCommand ViewForwardWith2DPanel { get { return new XPlaneCommand("sim/view/forward_with_2d_panel", "Forward with 2-D panel.", "View Forward With2d Panel", XPlaneCommands.ViewForwardWith2DPanel); } }
        private XPlaneCommand ViewForwardWithHud { get { return new XPlaneCommand("sim/view/forward_with_hud", "Forward with HUD.", "View Forward With Hud", XPlaneCommands.ViewForwardWithHud); } }
        private XPlaneCommand ViewForwardWithNothing { get { return new XPlaneCommand("sim/view/forward_with_nothing", "Forward with nothing.", "View Forward With Nothing", XPlaneCommands.ViewForwardWithNothing); } }
        private XPlaneCommand ViewLinearSpot { get { return new XPlaneCommand("sim/view/linear_spot", "Linear spot.", "View Linear Spot", XPlaneCommands.ViewLinearSpot); } }
        private XPlaneCommand ViewStillSpot { get { return new XPlaneCommand("sim/view/still_spot", "Still spot.", "View Still Spot", XPlaneCommands.ViewStillSpot); } }
        private XPlaneCommand ViewRunway { get { return new XPlaneCommand("sim/view/runway", "Runway.", "View Runway", XPlaneCommands.ViewRunway); } }
        private XPlaneCommand ViewCircle { get { return new XPlaneCommand("sim/view/circle", "Circle.", "View Circle", XPlaneCommands.ViewCircle); } }
        private XPlaneCommand ViewTower { get { return new XPlaneCommand("sim/view/tower", "Tower.", "View Tower", XPlaneCommands.ViewTower); } }
        private XPlaneCommand ViewRidealong { get { return new XPlaneCommand("sim/view/ridealong", "Ride-along.", "View Ridealong", XPlaneCommands.ViewRidealong); } }
        private XPlaneCommand ViewTrackWeapon { get { return new XPlaneCommand("sim/view/track_weapon", "Track weapon.", "View Track Weapon", XPlaneCommands.ViewTrackWeapon); } }
        private XPlaneCommand ViewChase { get { return new XPlaneCommand("sim/view/chase", "Chase.", "View Chase", XPlaneCommands.ViewChase); } }
        private XPlaneCommand View3DCockpitCmndLook { get { return new XPlaneCommand("sim/view/3d_cockpit_cmnd_look", "3-D cockpit.", "View3d Cockpit Cmnd Look", XPlaneCommands.View3DCockpitCmndLook); } }
        private XPlaneCommand View3DCockpitToggle { get { return new XPlaneCommand("sim/view/3d_cockpit_toggle", "Toggle between 2-D and 3-D cockpit.", "View3d Cockpit Toggle", XPlaneCommands.View3DCockpitToggle); } }
        private XPlaneCommand ViewLockGeo { get { return new XPlaneCommand("sim/view/lock_geo", "Lock onto current location.", "View Lock Geo", XPlaneCommands.ViewLockGeo); } }
        private XPlaneCommand ViewCinemaVerite { get { return new XPlaneCommand("sim/view/cinema_verite", "Cinema verite.", "View Cinema Verite", XPlaneCommands.ViewCinemaVerite); } }
        private XPlaneCommand ViewSunglasses { get { return new XPlaneCommand("sim/view/sunglasses", "Sunglasses.", "View Sunglasses", XPlaneCommands.ViewSunglasses); } }
        private XPlaneCommand ViewNightVision { get { return new XPlaneCommand("sim/view/night_vision", "Night vision.", "View Night Vision", XPlaneCommands.ViewNightVision); } }
        private XPlaneCommand ViewFlashlightRed { get { return new XPlaneCommand("sim/view/flashlight_red", "Toggle the red flashlight.", "View Flashlight Red", XPlaneCommands.ViewFlashlightRed); } }
        private XPlaneCommand ViewFlashlightWht { get { return new XPlaneCommand("sim/view/flashlight_wht", "Toggle the white flashlight.", "View Flashlight Wht", XPlaneCommands.ViewFlashlightWht); } }
        private XPlaneCommand ViewGlanceLeft { get { return new XPlaneCommand("sim/view/glance_left", "Glance left.", "View Glance Left", XPlaneCommands.ViewGlanceLeft); } }
        private XPlaneCommand ViewGlanceRight { get { return new XPlaneCommand("sim/view/glance_right", "Glance right.", "View Glance Right", XPlaneCommands.ViewGlanceRight); } }
        private XPlaneCommand ViewUpLeft { get { return new XPlaneCommand("sim/view/up_left", "Glance up and left.", "View Up Left", XPlaneCommands.ViewUpLeft); } }
        private XPlaneCommand ViewUpRight { get { return new XPlaneCommand("sim/view/up_right", "Glance up and right.", "View Up Right", XPlaneCommands.ViewUpRight); } }
        private XPlaneCommand ViewStraightUp { get { return new XPlaneCommand("sim/view/straight_up", "Glance straight up.", "View Straight Up", XPlaneCommands.ViewStraightUp); } }
        private XPlaneCommand ViewStraightDown { get { return new XPlaneCommand("sim/view/straight_down", "Glance straight down.", "View Straight Down", XPlaneCommands.ViewStraightDown); } }
        private XPlaneCommand ViewLeft45 { get { return new XPlaneCommand("sim/view/left_45", "Glance 45 degrees left.", "View Left45", XPlaneCommands.ViewLeft45); } }
        private XPlaneCommand ViewRight45 { get { return new XPlaneCommand("sim/view/right_45", "Glance 45 degrees right.", "View Right45", XPlaneCommands.ViewRight45); } }
        private XPlaneCommand ViewLeft90 { get { return new XPlaneCommand("sim/view/left_90", "Glance 90 degrees left.", "View Left90", XPlaneCommands.ViewLeft90); } }
        private XPlaneCommand ViewRight90 { get { return new XPlaneCommand("sim/view/right_90", "Glance 90 degrees right.", "View Right90", XPlaneCommands.ViewRight90); } }
        private XPlaneCommand ViewLeft135 { get { return new XPlaneCommand("sim/view/left_135", "Glance 135 degrees left.", "View Left135", XPlaneCommands.ViewLeft135); } }
        private XPlaneCommand ViewRight135 { get { return new XPlaneCommand("sim/view/right_135", "Glance 135 degrees right.", "View Right135", XPlaneCommands.ViewRight135); } }
        private XPlaneCommand ViewBack { get { return new XPlaneCommand("sim/view/back", "Glance backward.", "View Back", XPlaneCommands.ViewBack); } }
        private XPlaneCommand View3DPathToggle { get { return new XPlaneCommand("sim/view/3d_path_toggle", "3-D path toggle.", "View3d Path Toggle", XPlaneCommands.View3DPathToggle); } }
        private XPlaneCommand View3DPathReset { get { return new XPlaneCommand("sim/view/3d_path_reset", "3-D path reset.", "View3d Path Reset", XPlaneCommands.View3DPathReset); } }
        private XPlaneCommand ViewShowPhysicsModel { get { return new XPlaneCommand("sim/view/show_physics_model", "Toggle physics model visualization.", "View Show Physics Model", XPlaneCommands.ViewShowPhysicsModel); } }
        private XPlaneCommand ViewMouseClickRegionsToggle { get { return new XPlaneCommand("sim/view/mouse_click_regions_toggle", "Toggle visualization of clickable cockpit areas.", "View Mouse Click Regions Toggle", XPlaneCommands.ViewMouseClickRegionsToggle); } }
        private XPlaneCommand ViewInstrumentDescriptionsToggle { get { return new XPlaneCommand("sim/view/instrument_descriptions_toggle", "Toggle instrument descriptions on hover.", "View Instrument Descriptions Toggle", XPlaneCommands.ViewInstrumentDescriptionsToggle); } }
        private XPlaneCommand ViewQuickLook0 { get { return new XPlaneCommand("sim/view/quick_look_0", "Go to save 3-D cockpit location #1.", "View Quick Look0", XPlaneCommands.ViewQuickLook0); } }
        private XPlaneCommand ViewQuickLook1 { get { return new XPlaneCommand("sim/view/quick_look_1", "Go to save 3-D cockpit location #2.", "View Quick Look1", XPlaneCommands.ViewQuickLook1); } }
        private XPlaneCommand ViewQuickLook2 { get { return new XPlaneCommand("sim/view/quick_look_2", "Go to save 3-D cockpit location #3.", "View Quick Look2", XPlaneCommands.ViewQuickLook2); } }
        private XPlaneCommand ViewQuickLook3 { get { return new XPlaneCommand("sim/view/quick_look_3", "Go to save 3-D cockpit location #4.", "View Quick Look3", XPlaneCommands.ViewQuickLook3); } }
        private XPlaneCommand ViewQuickLook4 { get { return new XPlaneCommand("sim/view/quick_look_4", "Go to save 3-D cockpit location #5.", "View Quick Look4", XPlaneCommands.ViewQuickLook4); } }
        private XPlaneCommand ViewQuickLook5 { get { return new XPlaneCommand("sim/view/quick_look_5", "Go to save 3-D cockpit location #6.", "View Quick Look5", XPlaneCommands.ViewQuickLook5); } }
        private XPlaneCommand ViewQuickLook6 { get { return new XPlaneCommand("sim/view/quick_look_6", "Go to save 3-D cockpit location #7.", "View Quick Look6", XPlaneCommands.ViewQuickLook6); } }
        private XPlaneCommand ViewQuickLook7 { get { return new XPlaneCommand("sim/view/quick_look_7", "Go to save 3-D cockpit location #8.", "View Quick Look7", XPlaneCommands.ViewQuickLook7); } }
        private XPlaneCommand ViewQuickLook8 { get { return new XPlaneCommand("sim/view/quick_look_8", "Go to save 3-D cockpit location #9.", "View Quick Look8", XPlaneCommands.ViewQuickLook8); } }
        private XPlaneCommand ViewQuickLook9 { get { return new XPlaneCommand("sim/view/quick_look_9", "Go to save 3-D cockpit location #10.", "View Quick Look9", XPlaneCommands.ViewQuickLook9); } }
        private XPlaneCommand ViewQuickLook10 { get { return new XPlaneCommand("sim/view/quick_look_10", "Go to save 3-D cockpit location #11.", "View Quick Look10", XPlaneCommands.ViewQuickLook10); } }
        private XPlaneCommand ViewQuickLook11 { get { return new XPlaneCommand("sim/view/quick_look_11", "Go to save 3-D cockpit location #12.", "View Quick Look11", XPlaneCommands.ViewQuickLook11); } }
        private XPlaneCommand ViewQuickLook12 { get { return new XPlaneCommand("sim/view/quick_look_12", "Go to save 3-D cockpit location #13.", "View Quick Look12", XPlaneCommands.ViewQuickLook12); } }
        private XPlaneCommand ViewQuickLook13 { get { return new XPlaneCommand("sim/view/quick_look_13", "Go to save 3-D cockpit location #14.", "View Quick Look13", XPlaneCommands.ViewQuickLook13); } }
        private XPlaneCommand ViewQuickLook14 { get { return new XPlaneCommand("sim/view/quick_look_14", "Go to save 3-D cockpit location #15.", "View Quick Look14", XPlaneCommands.ViewQuickLook14); } }
        private XPlaneCommand ViewQuickLook15 { get { return new XPlaneCommand("sim/view/quick_look_15", "Go to save 3-D cockpit location #16.", "View Quick Look15", XPlaneCommands.ViewQuickLook15); } }
        private XPlaneCommand ViewQuickLook16 { get { return new XPlaneCommand("sim/view/quick_look_16", "Go to save 3-D cockpit location #17.", "View Quick Look16", XPlaneCommands.ViewQuickLook16); } }
        private XPlaneCommand ViewQuickLook17 { get { return new XPlaneCommand("sim/view/quick_look_17", "Go to save 3-D cockpit location #18.", "View Quick Look17", XPlaneCommands.ViewQuickLook17); } }
        private XPlaneCommand ViewQuickLook18 { get { return new XPlaneCommand("sim/view/quick_look_18", "Go to save 3-D cockpit location #19.", "View Quick Look18", XPlaneCommands.ViewQuickLook18); } }
        private XPlaneCommand ViewQuickLook19 { get { return new XPlaneCommand("sim/view/quick_look_19", "Go to save 3-D cockpit location #20.", "View Quick Look19", XPlaneCommands.ViewQuickLook19); } }
        private XPlaneCommand ViewQuickLook0Mem { get { return new XPlaneCommand("sim/view/quick_look_0_mem", "Memorize 3-D cockpit location #1.", "View Quick Look0mem", XPlaneCommands.ViewQuickLook0Mem); } }
        private XPlaneCommand ViewQuickLook1Mem { get { return new XPlaneCommand("sim/view/quick_look_1_mem", "Memorize 3-D cockpit location #2.", "View Quick Look1mem", XPlaneCommands.ViewQuickLook1Mem); } }
        private XPlaneCommand ViewQuickLook2Mem { get { return new XPlaneCommand("sim/view/quick_look_2_mem", "Memorize 3-D cockpit location #3.", "View Quick Look2mem", XPlaneCommands.ViewQuickLook2Mem); } }
        private XPlaneCommand ViewQuickLook3Mem { get { return new XPlaneCommand("sim/view/quick_look_3_mem", "Memorize 3-D cockpit location #4.", "View Quick Look3mem", XPlaneCommands.ViewQuickLook3Mem); } }
        private XPlaneCommand ViewQuickLook4Mem { get { return new XPlaneCommand("sim/view/quick_look_4_mem", "Memorize 3-D cockpit location #5.", "View Quick Look4mem", XPlaneCommands.ViewQuickLook4Mem); } }
        private XPlaneCommand ViewQuickLook5Mem { get { return new XPlaneCommand("sim/view/quick_look_5_mem", "Memorize 3-D cockpit location #6.", "View Quick Look5mem", XPlaneCommands.ViewQuickLook5Mem); } }
        private XPlaneCommand ViewQuickLook6Mem { get { return new XPlaneCommand("sim/view/quick_look_6_mem", "Memorize 3-D cockpit location #7.", "View Quick Look6mem", XPlaneCommands.ViewQuickLook6Mem); } }
        private XPlaneCommand ViewQuickLook7Mem { get { return new XPlaneCommand("sim/view/quick_look_7_mem", "Memorize 3-D cockpit location #8.", "View Quick Look7mem", XPlaneCommands.ViewQuickLook7Mem); } }
        private XPlaneCommand ViewQuickLook8Mem { get { return new XPlaneCommand("sim/view/quick_look_8_mem", "Memorize 3-D cockpit location #9.", "View Quick Look8mem", XPlaneCommands.ViewQuickLook8Mem); } }
        private XPlaneCommand ViewQuickLook9Mem { get { return new XPlaneCommand("sim/view/quick_look_9_mem", "Memorize 3-D cockpit location #10.", "View Quick Look9mem", XPlaneCommands.ViewQuickLook9Mem); } }
        private XPlaneCommand ViewQuickLook10Mem { get { return new XPlaneCommand("sim/view/quick_look_10_mem", "Memorize 3-D cockpit location #11.", "View Quick Look10mem", XPlaneCommands.ViewQuickLook10Mem); } }
        private XPlaneCommand ViewQuickLook11Mem { get { return new XPlaneCommand("sim/view/quick_look_11_mem", "Memorize 3-D cockpit location #12.", "View Quick Look11mem", XPlaneCommands.ViewQuickLook11Mem); } }
        private XPlaneCommand ViewQuickLook12Mem { get { return new XPlaneCommand("sim/view/quick_look_12_mem", "Memorize 3-D cockpit location #13.", "View Quick Look12mem", XPlaneCommands.ViewQuickLook12Mem); } }
        private XPlaneCommand ViewQuickLook13Mem { get { return new XPlaneCommand("sim/view/quick_look_13_mem", "Memorize 3-D cockpit location #14.", "View Quick Look13mem", XPlaneCommands.ViewQuickLook13Mem); } }
        private XPlaneCommand ViewQuickLook14Mem { get { return new XPlaneCommand("sim/view/quick_look_14_mem", "Memorize 3-D cockpit location #15.", "View Quick Look14mem", XPlaneCommands.ViewQuickLook14Mem); } }
        private XPlaneCommand ViewQuickLook15Mem { get { return new XPlaneCommand("sim/view/quick_look_15_mem", "Memorize 3-D cockpit location #16.", "View Quick Look15mem", XPlaneCommands.ViewQuickLook15Mem); } }
        private XPlaneCommand ViewQuickLook16Mem { get { return new XPlaneCommand("sim/view/quick_look_16_mem", "Memorize 3-D cockpit location #17.", "View Quick Look16mem", XPlaneCommands.ViewQuickLook16Mem); } }
        private XPlaneCommand ViewQuickLook17Mem { get { return new XPlaneCommand("sim/view/quick_look_17_mem", "Memorize 3-D cockpit location #18.", "View Quick Look17mem", XPlaneCommands.ViewQuickLook17Mem); } }
        private XPlaneCommand ViewQuickLook18Mem { get { return new XPlaneCommand("sim/view/quick_look_18_mem", "Memorize 3-D cockpit location #19.", "View Quick Look18mem", XPlaneCommands.ViewQuickLook18Mem); } }
        private XPlaneCommand ViewQuickLook19Mem { get { return new XPlaneCommand("sim/view/quick_look_19_mem", "Memorize 3-D cockpit location #20.", "View Quick Look19mem", XPlaneCommands.ViewQuickLook19Mem); } }
        private XPlaneCommand GeneralLeft { get { return new XPlaneCommand("sim/general/left", "Move view left.", "General Left", XPlaneCommands.GeneralLeft); } }
        private XPlaneCommand GeneralRight { get { return new XPlaneCommand("sim/general/right", "Move view right.", "General Right", XPlaneCommands.GeneralRight); } }
        private XPlaneCommand GeneralUp { get { return new XPlaneCommand("sim/general/up", "Move view up.", "General Up", XPlaneCommands.GeneralUp); } }
        private XPlaneCommand GeneralDown { get { return new XPlaneCommand("sim/general/down", "Move view down.", "General Down", XPlaneCommands.GeneralDown); } }
        private XPlaneCommand GeneralForward { get { return new XPlaneCommand("sim/general/forward", "Move view forward.", "General Forward", XPlaneCommands.GeneralForward); } }
        private XPlaneCommand GeneralBackward { get { return new XPlaneCommand("sim/general/backward", "Move view backward.", "General Backward", XPlaneCommands.GeneralBackward); } }
        private XPlaneCommand GeneralZoomIn { get { return new XPlaneCommand("sim/general/zoom_in", "Zoom in.", "General Zoom In", XPlaneCommands.GeneralZoomIn); } }
        private XPlaneCommand GeneralZoomOut { get { return new XPlaneCommand("sim/general/zoom_out", "Zoom out.", "General Zoom Out", XPlaneCommands.GeneralZoomOut); } }
        private XPlaneCommand GeneralHatSwitchLeft { get { return new XPlaneCommand("sim/general/hat_switch_left", "Hat switch left.", "General Hat Switch Left", XPlaneCommands.GeneralHatSwitchLeft); } }
        private XPlaneCommand GeneralHatSwitchRight { get { return new XPlaneCommand("sim/general/hat_switch_right", "Hat switch right.", "General Hat Switch Right", XPlaneCommands.GeneralHatSwitchRight); } }
        private XPlaneCommand GeneralHatSwitchUp { get { return new XPlaneCommand("sim/general/hat_switch_up", "Hat switch up.", "General Hat Switch Up", XPlaneCommands.GeneralHatSwitchUp); } }
        private XPlaneCommand GeneralHatSwitchDown { get { return new XPlaneCommand("sim/general/hat_switch_down", "Hat switch down.", "General Hat Switch Down", XPlaneCommands.GeneralHatSwitchDown); } }
        private XPlaneCommand GeneralHatSwitchUpLeft { get { return new XPlaneCommand("sim/general/hat_switch_up_left", "Hat switch up + left.", "General Hat Switch Up Left", XPlaneCommands.GeneralHatSwitchUpLeft); } }
        private XPlaneCommand GeneralHatSwitchUpRight { get { return new XPlaneCommand("sim/general/hat_switch_up_right", "Hat switch up + right.", "General Hat Switch Up Right", XPlaneCommands.GeneralHatSwitchUpRight); } }
        private XPlaneCommand GeneralHatSwitchDownLeft { get { return new XPlaneCommand("sim/general/hat_switch_down_left", "Hat switch down + left.", "General Hat Switch Down Left", XPlaneCommands.GeneralHatSwitchDownLeft); } }
        private XPlaneCommand GeneralHatSwitchDownRight { get { return new XPlaneCommand("sim/general/hat_switch_down_right", "Hat switch down + right.", "General Hat Switch Down Right", XPlaneCommands.GeneralHatSwitchDownRight); } }
        private XPlaneCommand GeneralLeftFast { get { return new XPlaneCommand("sim/general/left_fast", "Move view left fast.", "General Left Fast", XPlaneCommands.GeneralLeftFast); } }
        private XPlaneCommand GeneralRightFast { get { return new XPlaneCommand("sim/general/right_fast", "Move view right fast.", "General Right Fast", XPlaneCommands.GeneralRightFast); } }
        private XPlaneCommand GeneralUpFast { get { return new XPlaneCommand("sim/general/up_fast", "Move view up fast.", "General Up Fast", XPlaneCommands.GeneralUpFast); } }
        private XPlaneCommand GeneralDownFast { get { return new XPlaneCommand("sim/general/down_fast", "Move view down fast.", "General Down Fast", XPlaneCommands.GeneralDownFast); } }
        private XPlaneCommand GeneralForwardFast { get { return new XPlaneCommand("sim/general/forward_fast", "Move view forward fast.", "General Forward Fast", XPlaneCommands.GeneralForwardFast); } }
        private XPlaneCommand GeneralBackwardFast { get { return new XPlaneCommand("sim/general/backward_fast", "Move view backward fast.", "General Backward Fast", XPlaneCommands.GeneralBackwardFast); } }
        private XPlaneCommand GeneralZoomInFast { get { return new XPlaneCommand("sim/general/zoom_in_fast", "Zoom in fast.", "General Zoom In Fast", XPlaneCommands.GeneralZoomInFast); } }
        private XPlaneCommand GeneralZoomOutFast { get { return new XPlaneCommand("sim/general/zoom_out_fast", "Zoom out fast.", "General Zoom Out Fast", XPlaneCommands.GeneralZoomOutFast); } }
        private XPlaneCommand GeneralLeftSlow { get { return new XPlaneCommand("sim/general/left_slow", "Move view left slow.", "General Left Slow", XPlaneCommands.GeneralLeftSlow); } }
        private XPlaneCommand GeneralRightSlow { get { return new XPlaneCommand("sim/general/right_slow", "Move view right slow.", "General Right Slow", XPlaneCommands.GeneralRightSlow); } }
        private XPlaneCommand GeneralUpSlow { get { return new XPlaneCommand("sim/general/up_slow", "Move view up slow.", "General Up Slow", XPlaneCommands.GeneralUpSlow); } }
        private XPlaneCommand GeneralDownSlow { get { return new XPlaneCommand("sim/general/down_slow", "Move view down slow.", "General Down Slow", XPlaneCommands.GeneralDownSlow); } }
        private XPlaneCommand GeneralForwardSlow { get { return new XPlaneCommand("sim/general/forward_slow", "Move view forward slow.", "General Forward Slow", XPlaneCommands.GeneralForwardSlow); } }
        private XPlaneCommand GeneralBackwardSlow { get { return new XPlaneCommand("sim/general/backward_slow", "Move view backward slow.", "General Backward Slow", XPlaneCommands.GeneralBackwardSlow); } }
        private XPlaneCommand GeneralZoomInSlow { get { return new XPlaneCommand("sim/general/zoom_in_slow", "Zoom in slow.", "General Zoom In Slow", XPlaneCommands.GeneralZoomInSlow); } }
        private XPlaneCommand GeneralZoomOutSlow { get { return new XPlaneCommand("sim/general/zoom_out_slow", "Zoom out slow.", "General Zoom Out Slow", XPlaneCommands.GeneralZoomOutSlow); } }
        private XPlaneCommand GeneralRotUp { get { return new XPlaneCommand("sim/general/rot_up", "Rotate view: tilt up.", "General Rot Up", XPlaneCommands.GeneralRotUp); } }
        private XPlaneCommand GeneralRotDown { get { return new XPlaneCommand("sim/general/rot_down", "Rotate view: tilt down.", "General Rot Down", XPlaneCommands.GeneralRotDown); } }
        private XPlaneCommand GeneralRotLeft { get { return new XPlaneCommand("sim/general/rot_left", "Rotate view: pan left.", "General Rot Left", XPlaneCommands.GeneralRotLeft); } }
        private XPlaneCommand GeneralRotRight { get { return new XPlaneCommand("sim/general/rot_right", "Rotate view: pan right.", "General Rot Right", XPlaneCommands.GeneralRotRight); } }
        private XPlaneCommand GeneralRotUpFast { get { return new XPlaneCommand("sim/general/rot_up_fast", "Rotate view: tilt up fast.", "General Rot Up Fast", XPlaneCommands.GeneralRotUpFast); } }
        private XPlaneCommand GeneralRotDownFast { get { return new XPlaneCommand("sim/general/rot_down_fast", "Rotate view: tilt down fast.", "General Rot Down Fast", XPlaneCommands.GeneralRotDownFast); } }
        private XPlaneCommand GeneralRotLeftFast { get { return new XPlaneCommand("sim/general/rot_left_fast", "Rotate view: pan left fast.", "General Rot Left Fast", XPlaneCommands.GeneralRotLeftFast); } }
        private XPlaneCommand GeneralRotRightFast { get { return new XPlaneCommand("sim/general/rot_right_fast", "Rotate view: pan right fast.", "General Rot Right Fast", XPlaneCommands.GeneralRotRightFast); } }
        private XPlaneCommand GeneralRotUpSlow { get { return new XPlaneCommand("sim/general/rot_up_slow", "Rotate view: tilt up slow.", "General Rot Up Slow", XPlaneCommands.GeneralRotUpSlow); } }
        private XPlaneCommand GeneralRotDownSlow { get { return new XPlaneCommand("sim/general/rot_down_slow", "Rotate view: tilt down slow.", "General Rot Down Slow", XPlaneCommands.GeneralRotDownSlow); } }
        private XPlaneCommand GeneralRotLeftSlow { get { return new XPlaneCommand("sim/general/rot_left_slow", "Rotate view: pan left slow.", "General Rot Left Slow", XPlaneCommands.GeneralRotLeftSlow); } }
        private XPlaneCommand GeneralRotRightSlow { get { return new XPlaneCommand("sim/general/rot_right_slow", "Rotate view: pan right slow.", "General Rot Right Slow", XPlaneCommands.GeneralRotRightSlow); } }
        private XPlaneCommand GeneralTrackP0 { get { return new XPlaneCommand("sim/general/track_p0", "Views track aircraft: Yours.", "General Track P0", XPlaneCommands.GeneralTrackP0); } }
        private XPlaneCommand GeneralTrackPNext { get { return new XPlaneCommand("sim/general/track_p_next", "Views track aircraft: Next", "General Track P Next", XPlaneCommands.GeneralTrackPNext); } }
        private XPlaneCommand GeneralTrackPPrev { get { return new XPlaneCommand("sim/general/track_p_prev", "Views track aircraft: Previous.", "General Track P Prev", XPlaneCommands.GeneralTrackPPrev); } }
        private XPlaneCommand GeneralToggleArtificialStabWin { get { return new XPlaneCommand("sim/general/toggle_artificial_stab_win", "Toggle the artificial stability constants window.", "General Toggle Artificial Stab Win", XPlaneCommands.GeneralToggleArtificialStabWin); } }
        private XPlaneCommand GeneralToggleTrafficPaths { get { return new XPlaneCommand("sim/general/toggle_traffic_paths", "Toggle display of traffic paths.", "General Toggle Traffic Paths", XPlaneCommands.GeneralToggleTrafficPaths); } }
        private XPlaneCommand GeneralToggleAutopilotConstantsWin { get { return new XPlaneCommand("sim/general/toggle_autopilot_constants_win", "Toggle the autopilot constants window.", "General Toggle Autopilot Constants Win", XPlaneCommands.GeneralToggleAutopilotConstantsWin); } }
        private XPlaneCommand GeneralToggleFadecWin { get { return new XPlaneCommand("sim/general/toggle_fadec_win", "Toggle the FADEC constants window.", "General Toggle Fadec Win", XPlaneCommands.GeneralToggleFadecWin); } }
        private XPlaneCommand GeneralToggleControlDeflectionsWin { get { return new XPlaneCommand("sim/general/toggle_control_deflections_win", "Toggle the control deflections window.", "General Toggle Control Deflections Win", XPlaneCommands.GeneralToggleControlDeflectionsWin); } }
        private XPlaneCommand GeneralToggleWeaponGuidanceWin { get { return new XPlaneCommand("sim/general/toggle_weapon_guidance_win", "Toggle the weapon guidance window.", "General Toggle Weapon Guidance Win", XPlaneCommands.GeneralToggleWeaponGuidanceWin); } }
        private XPlaneCommand DeveloperToggleTextureBrowser { get { return new XPlaneCommand("sim/developer/toggle_texture_browser", "Toggle the texture browser window.", "Developer Toggle Texture Browser", XPlaneCommands.DeveloperToggleTextureBrowser); } }
        private XPlaneCommand DeveloperToggleParticleBrowser { get { return new XPlaneCommand("sim/developer/toggle_particle_browser", "Toggle the particle system browser window.", "Developer Toggle Particle Browser", XPlaneCommands.DeveloperToggleParticleBrowser); } }
        private XPlaneCommand GeneralToggleProjectionWin { get { return new XPlaneCommand("sim/general/toggle_projection_win", "Toggle the projection configuration window.", "General Toggle Projection Win", XPlaneCommands.GeneralToggleProjectionWin); } }
        private XPlaneCommand DeveloperToggleMicroprofiler { get { return new XPlaneCommand("sim/developer/toggle_microprofiler", "Toggle the frame timing profiler window.", "Developer Toggle Microprofiler", XPlaneCommands.DeveloperToggleMicroprofiler); } }
        private XPlaneCommand DeveloperToggleVramProfiler { get { return new XPlaneCommand("sim/developer/toggle_vram_profiler", "Toggle the VRAM profiler window.", "Developer Toggle Vram Profiler", XPlaneCommands.DeveloperToggleVramProfiler); } }
        private XPlaneCommand DeveloperTogglePluginAdmin { get { return new XPlaneCommand("sim/developer/toggle_plugin_admin", "Toggle the Plugin Admin window.", "Developer Toggle Plugin Admin", XPlaneCommands.DeveloperTogglePluginAdmin); } }
        private XPlaneCommand OperationToggleSkyColorsWin { get { return new XPlaneCommand("sim/operation/toggle_sky_colors_win", "Toggle the sky colors window.", "Operation Toggle Sky Colors Win", XPlaneCommands.OperationToggleSkyColorsWin); } }
        private XPlaneCommand VRXpadHomeButton { get { return new XPlaneCommand("sim/VR/xpad/home_button", "Press the Home Button of the VR xPad.", "VR Xpad Home Button", XPlaneCommands.VRXpadHomeButton); } }
        private XPlaneCommand VRToggle3DMouseCursor { get { return new XPlaneCommand("sim/VR/toggle_3d_mouse_cursor", "Toggle the 3-d mouse cursor in VR.", "VR Toggle3d Mouse Cursor", XPlaneCommands.VRToggle3DMouseCursor); } }
        private XPlaneCommand VRToggleVr { get { return new XPlaneCommand("sim/VR/toggle_vr", "Toggle enabling of VR hardware.", "VR Toggle Vr", XPlaneCommands.VRToggleVr); } }
        private XPlaneCommand VRGeneralResetView { get { return new XPlaneCommand("sim/VR/general/reset_view", "Reset VR View.", "VR General Reset View", XPlaneCommands.VRGeneralResetView); } }
        private XPlaneCommand VRQuickZoomView { get { return new XPlaneCommand("sim/VR/quick_zoom_view", "Temporarily and Quickly Zoom in while in VR.", "VR Quick Zoom View", XPlaneCommands.VRQuickZoomView); } }
        private XPlaneCommand VRReservedSelect { get { return new XPlaneCommand("sim/VR/reserved/select", "Reserved: VR Select/Trigger Button.", "VR Reserved Select", XPlaneCommands.VRReservedSelect); } }
        private XPlaneCommand VRReservedMenu { get { return new XPlaneCommand("sim/VR/reserved/menu", "Reserved: VR Menu Button.", "VR Reserved Menu", XPlaneCommands.VRReservedMenu); } }
        private XPlaneCommand VRReservedTouchpad { get { return new XPlaneCommand("sim/VR/reserved/touchpad", "Reserved: VR Thumbstick/Touchpad Button.", "VR Reserved Touchpad", XPlaneCommands.VRReservedTouchpad); } }
    }
}
