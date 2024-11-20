using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class SimConnectEvents
    {
        public static readonly SimConnectEvents Instance;

        static SimConnectEvents()
        {
            Instance = new SimConnectEvents();
        }

        SimConnectEvents()
        {
            Events = new Dictionary<SimConnectEventId, SimConnectEvent>();
            Events.Add(Abort.Id, Abort);
            Events.Add(AddFuelQuantity.Id, AddFuelQuantity);
            Events.Add(Adf.Id, Adf);
            Events.Add(Adf1Dec.Id, Adf1Dec);
            Events.Add(Adf1Inc.Id, Adf1Inc);
            Events.Add(Adf10Dec.Id, Adf10Dec);
            Events.Add(Adf10Inc.Id, Adf10Inc);
            Events.Add(Adf100Dec.Id, Adf100Dec);
            Events.Add(Adf100Inc.Id, Adf100Inc);
            Events.Add(AdfActiveSet.Id, AdfActiveSet);
            Events.Add(AdfCardDec.Id, AdfCardDec);
            Events.Add(AdfCardInc.Id, AdfCardInc);
            Events.Add(AdfCardSet.Id, AdfCardSet);
            Events.Add(AdfCompleteSet.Id, AdfCompleteSet);
            Events.Add(AdfExtendedSet.Id, AdfExtendedSet);
            Events.Add(AdfFractDecCarry.Id, AdfFractDecCarry);
            Events.Add(AdfFractIncCarry.Id, AdfFractIncCarry);
            Events.Add(AdfHighrangeSet.Id, AdfHighrangeSet);
            Events.Add(AdfLowrangeSet.Id, AdfLowrangeSet);
            Events.Add(AdfNeedleSet.Id, AdfNeedleSet);
            Events.Add(AdfOutsideSource.Id, AdfOutsideSource);
            Events.Add(AdfSet.Id, AdfSet);
            Events.Add(AdfStbySet.Id, AdfStbySet);
            Events.Add(AdfVolumeDec.Id, AdfVolumeDec);
            Events.Add(AdfVolumeInc.Id, AdfVolumeInc);
            Events.Add(AdfVolumeSet.Id, AdfVolumeSet);
            Events.Add(Adf1RadioSwap.Id, Adf1RadioSwap);
            Events.Add(Adf1RadioTenthsDec.Id, Adf1RadioTenthsDec);
            Events.Add(Adf1RadioTenthsInc.Id, Adf1RadioTenthsInc);
            Events.Add(Adf1WholeDec.Id, Adf1WholeDec);
            Events.Add(Adf1WholeInc.Id, Adf1WholeInc);
            Events.Add(Adf21Dec.Id, Adf21Dec);
            Events.Add(Adf21Inc.Id, Adf21Inc);
            Events.Add(Adf210Dec.Id, Adf210Dec);
            Events.Add(Adf210Inc.Id, Adf210Inc);
            Events.Add(Adf2100Dec.Id, Adf2100Dec);
            Events.Add(Adf2100Inc.Id, Adf2100Inc);
            Events.Add(Adf2ActiveSet.Id, Adf2ActiveSet);
            Events.Add(Adf2CompleteSet.Id, Adf2CompleteSet);
            Events.Add(Adf2ExtendedSet.Id, Adf2ExtendedSet);
            Events.Add(Adf2FractDecCarry.Id, Adf2FractDecCarry);
            Events.Add(Adf2FractIncCarry.Id, Adf2FractIncCarry);
            Events.Add(Adf2HighrangeSet.Id, Adf2HighrangeSet);
            Events.Add(Adf2LowrangeSet.Id, Adf2LowrangeSet);
            Events.Add(Adf2NeedleSet.Id, Adf2NeedleSet);
            Events.Add(Adf2OutsideSource.Id, Adf2OutsideSource);
            Events.Add(Adf2RadioSwap.Id, Adf2RadioSwap);
            Events.Add(Adf2RadioTenthsDec.Id, Adf2RadioTenthsDec);
            Events.Add(Adf2RadioTenthsInc.Id, Adf2RadioTenthsInc);
            Events.Add(Adf2Set.Id, Adf2Set);
            Events.Add(Adf2StbySet.Id, Adf2StbySet);
            Events.Add(Adf2VolumeDec.Id, Adf2VolumeDec);
            Events.Add(Adf2VolumeInc.Id, Adf2VolumeInc);
            Events.Add(Adf2VolumeSet.Id, Adf2VolumeSet);
            Events.Add(Adf2WholeDec.Id, Adf2WholeDec);
            Events.Add(Adf2WholeInc.Id, Adf2WholeInc);
            Events.Add(AdventureAction.Id, AdventureAction);
            Events.Add(AileronCenter.Id, AileronCenter);
            Events.Add(AileronLeft.Id, AileronLeft);
            Events.Add(AileronRight.Id, AileronRight);
            Events.Add(AileronSet.Id, AileronSet);
            Events.Add(AileronTrimDisabledSet.Id, AileronTrimDisabledSet);
            Events.Add(AileronTrimDisabledToggle.Id, AileronTrimDisabledToggle);
            Events.Add(AileronTrimLeft.Id, AileronTrimLeft);
            Events.Add(AileronTrimRight.Id, AileronTrimRight);
            Events.Add(AileronTrimSet.Id, AileronTrimSet);
            Events.Add(AileronTrimSetEx1.Id, AileronTrimSetEx1);
            Events.Add(AileronsLeft.Id, AileronsLeft);
            Events.Add(AileronsRight.Id, AileronsRight);
            Events.Add(AirspeedBugSelect.Id, AirspeedBugSelect);
            Events.Add(AllLightsToggle.Id, AllLightsToggle);
            Events.Add(AlternatorOff.Id, AlternatorOff);
            Events.Add(AlternatorOn.Id, AlternatorOn);
            Events.Add(AlternatorSet.Id, AlternatorSet);
            Events.Add(AltitudeBugSelect.Id, AltitudeBugSelect);
            Events.Add(AltitudeSlotIndexSet.Id, AltitudeSlotIndexSet);
            Events.Add(AnalysisManeuverStop.Id, AnalysisManeuverStop);
            Events.Add(AnnunciatorSwitchOff.Id, AnnunciatorSwitchOff);
            Events.Add(AnnunciatorSwitchOn.Id, AnnunciatorSwitchOn);
            Events.Add(AnnunciatorSwitchToggle.Id, AnnunciatorSwitchToggle);
            Events.Add(AntiIceGradualSet.Id, AntiIceGradualSet);
            Events.Add(AntiIceGradualSetEng1.Id, AntiIceGradualSetEng1);
            Events.Add(AntiIceGradualSetEng2.Id, AntiIceGradualSetEng2);
            Events.Add(AntiIceGradualSetEng3.Id, AntiIceGradualSetEng3);
            Events.Add(AntiIceGradualSetEng4.Id, AntiIceGradualSetEng4);
            Events.Add(AntiIceOff.Id, AntiIceOff);
            Events.Add(AntiIceOn.Id, AntiIceOn);
            Events.Add(AntiIceSet.Id, AntiIceSet);
            Events.Add(AntiIceSetEng1.Id, AntiIceSetEng1);
            Events.Add(AntiIceSetEng2.Id, AntiIceSetEng2);
            Events.Add(AntiIceSetEng3.Id, AntiIceSetEng3);
            Events.Add(AntiIceSetEng4.Id, AntiIceSetEng4);
            Events.Add(AntiIceToggle.Id, AntiIceToggle);
            Events.Add(AntiIceToggleEng1.Id, AntiIceToggleEng1);
            Events.Add(AntiIceToggleEng2.Id, AntiIceToggleEng2);
            Events.Add(AntiIceToggleEng3.Id, AntiIceToggleEng3);
            Events.Add(AntiIceToggleEng4.Id, AntiIceToggleEng4);
            Events.Add(AntidetonationTankValveToggle.Id, AntidetonationTankValveToggle);
            Events.Add(AntiskidBrakesToggle.Id, AntiskidBrakesToggle);
            Events.Add(ApAirspeedHold.Id, ApAirspeedHold);
            Events.Add(ApAirspeedOff.Id, ApAirspeedOff);
            Events.Add(ApAirspeedOn.Id, ApAirspeedOn);
            Events.Add(ApAirspeedSet.Id, ApAirspeedSet);
            Events.Add(ApAltHold.Id, ApAltHold);
            Events.Add(ApAltHoldOff.Id, ApAltHoldOff);
            Events.Add(ApAltHoldOn.Id, ApAltHoldOn);
            Events.Add(ApAltRadioModeOff.Id, ApAltRadioModeOff);
            Events.Add(ApAltRadioModeOn.Id, ApAltRadioModeOn);
            Events.Add(ApAltRadioModeSet.Id, ApAltRadioModeSet);
            Events.Add(ApAltRadioModeToggle.Id, ApAltRadioModeToggle);
            Events.Add(ApAltVarDec.Id, ApAltVarDec);
            Events.Add(ApAltVarInc.Id, ApAltVarInc);
            Events.Add(ApAltVarSetEnglish.Id, ApAltVarSetEnglish);
            Events.Add(ApAltVarSetMetric.Id, ApAltVarSetMetric);
            Events.Add(ApAprHold.Id, ApAprHold);
            Events.Add(ApAprHoldOff.Id, ApAprHoldOff);
            Events.Add(ApAprHoldOn.Id, ApAprHoldOn);
            Events.Add(ApAttHold.Id, ApAttHold);
            Events.Add(ApAttHoldOff.Id, ApAttHoldOff);
            Events.Add(ApAttHoldOn.Id, ApAttHoldOn);
            Events.Add(ApAvionicsManagedOff.Id, ApAvionicsManagedOff);
            Events.Add(ApAvionicsManagedOn.Id, ApAvionicsManagedOn);
            Events.Add(ApAvionicsManagedSet.Id, ApAvionicsManagedSet);
            Events.Add(ApAvionicsManagedToggle.Id, ApAvionicsManagedToggle);
            Events.Add(ApBankHold.Id, ApBankHold);
            Events.Add(ApBankHoldOff.Id, ApBankHoldOff);
            Events.Add(ApBankHoldOn.Id, ApBankHoldOn);
            Events.Add(ApBcHold.Id, ApBcHold);
            Events.Add(ApBcHoldOff.Id, ApBcHoldOff);
            Events.Add(ApBcHoldOn.Id, ApBcHoldOn);
            Events.Add(ApHdgHold.Id, ApHdgHold);
            Events.Add(ApHdgHoldOff.Id, ApHdgHoldOff);
            Events.Add(ApHdgHoldOn.Id, ApHdgHoldOn);
            Events.Add(ApHeadingBugSetEx1.Id, ApHeadingBugSetEx1);
            Events.Add(ApLocHold.Id, ApLocHold);
            Events.Add(ApLocHoldOff.Id, ApLocHoldOff);
            Events.Add(ApLocHoldOn.Id, ApLocHoldOn);
            Events.Add(ApMachHold.Id, ApMachHold);
            Events.Add(ApMachOff.Id, ApMachOff);
            Events.Add(ApMachOn.Id, ApMachOn);
            Events.Add(ApMachSet.Id, ApMachSet);
            Events.Add(ApMachVarDec.Id, ApMachVarDec);
            Events.Add(ApMachVarInc.Id, ApMachVarInc);
            Events.Add(ApMachVarSet.Id, ApMachVarSet);
            Events.Add(ApMachVarSetEx1.Id, ApMachVarSetEx1);
            Events.Add(ApManagedSpeedInMachOff.Id, ApManagedSpeedInMachOff);
            Events.Add(ApManagedSpeedInMachOn.Id, ApManagedSpeedInMachOn);
            Events.Add(ApManagedSpeedInMachSet.Id, ApManagedSpeedInMachSet);
            Events.Add(ApManagedSpeedInMachToggle.Id, ApManagedSpeedInMachToggle);
            Events.Add(ApMaster.Id, ApMaster);
            Events.Add(ApMasterAlt.Id, ApMasterAlt);
            Events.Add(ApMaxBankAngleSet.Id, ApMaxBankAngleSet);
            Events.Add(ApMaxBankDec.Id, ApMaxBankDec);
            Events.Add(ApMaxBankInc.Id, ApMaxBankInc);
            Events.Add(ApMaxBankSet.Id, ApMaxBankSet);
            Events.Add(ApMaxBankVelocitySet.Id, ApMaxBankVelocitySet);
            Events.Add(ApN1Hold.Id, ApN1Hold);
            Events.Add(ApN1RefDec.Id, ApN1RefDec);
            Events.Add(ApN1RefInc.Id, ApN1RefInc);
            Events.Add(ApN1RefSet.Id, ApN1RefSet);
            Events.Add(ApNavSelectSet.Id, ApNavSelectSet);
            Events.Add(ApNav1Hold.Id, ApNav1Hold);
            Events.Add(ApNav1HoldOff.Id, ApNav1HoldOff);
            Events.Add(ApNav1HoldOn.Id, ApNav1HoldOn);
            Events.Add(ApPanelAltitudeHold.Id, ApPanelAltitudeHold);
            Events.Add(ApPanelAltitudeOff.Id, ApPanelAltitudeOff);
            Events.Add(ApPanelAltitudeOn.Id, ApPanelAltitudeOn);
            Events.Add(ApPanelAltitudeSet.Id, ApPanelAltitudeSet);
            Events.Add(ApPanelHeadingHold.Id, ApPanelHeadingHold);
            Events.Add(ApPanelHeadingOff.Id, ApPanelHeadingOff);
            Events.Add(ApPanelHeadingOn.Id, ApPanelHeadingOn);
            Events.Add(ApPanelHeadingSet.Id, ApPanelHeadingSet);
            Events.Add(ApPanelMachHold.Id, ApPanelMachHold);
            Events.Add(ApPanelMachHoldToggle.Id, ApPanelMachHoldToggle);
            Events.Add(ApPanelMachOff.Id, ApPanelMachOff);
            Events.Add(ApPanelMachOn.Id, ApPanelMachOn);
            Events.Add(ApPanelMachSet.Id, ApPanelMachSet);
            Events.Add(ApPanelSpeedHold.Id, ApPanelSpeedHold);
            Events.Add(ApPanelSpeedHoldToggle.Id, ApPanelSpeedHoldToggle);
            Events.Add(ApPanelSpeedOff.Id, ApPanelSpeedOff);
            Events.Add(ApPanelSpeedOn.Id, ApPanelSpeedOn);
            Events.Add(ApPanelSpeedSet.Id, ApPanelSpeedSet);
            Events.Add(ApPanelVsHold.Id, ApPanelVsHold);
            Events.Add(ApPanelVsOff.Id, ApPanelVsOff);
            Events.Add(ApPanelVsOn.Id, ApPanelVsOn);
            Events.Add(ApPanelVsSet.Id, ApPanelVsSet);
            Events.Add(ApPitchLeveler.Id, ApPitchLeveler);
            Events.Add(ApPitchLevelerOff.Id, ApPitchLevelerOff);
            Events.Add(ApPitchLevelerOn.Id, ApPitchLevelerOn);
            Events.Add(ApPitchRefIncDn.Id, ApPitchRefIncDn);
            Events.Add(ApPitchRefIncUp.Id, ApPitchRefIncUp);
            Events.Add(ApPitchRefSelect.Id, ApPitchRefSelect);
            Events.Add(ApPitchRefSet.Id, ApPitchRefSet);
            Events.Add(ApRpmSlotIndexSet.Id, ApRpmSlotIndexSet);
            Events.Add(ApSpdVarDec.Id, ApSpdVarDec);
            Events.Add(ApSpdVarInc.Id, ApSpdVarInc);
            Events.Add(ApSpdVarSet.Id, ApSpdVarSet);
            Events.Add(ApSpdVarSetEx1.Id, ApSpdVarSetEx1);
            Events.Add(ApSpeedSlotIndexSet.Id, ApSpeedSlotIndexSet);
            Events.Add(ApVsHold.Id, ApVsHold);
            Events.Add(ApVsOff.Id, ApVsOff);
            Events.Add(ApVsOn.Id, ApVsOn);
            Events.Add(ApVsSet.Id, ApVsSet);
            Events.Add(ApVsSlotIndexSet.Id, ApVsSlotIndexSet);
            Events.Add(ApVsVarDec.Id, ApVsVarDec);
            Events.Add(ApVsVarInc.Id, ApVsVarInc);
            Events.Add(ApVsVarSetCurrent.Id, ApVsVarSetCurrent);
            Events.Add(ApVsVarSetEnglish.Id, ApVsVarSetEnglish);
            Events.Add(ApVsVarSetMetric.Id, ApVsVarSetMetric);
            Events.Add(ApWingLeveler.Id, ApWingLeveler);
            Events.Add(ApWingLevelerOff.Id, ApWingLevelerOff);
            Events.Add(ApWingLevelerOn.Id, ApWingLevelerOn);
            Events.Add(ApuBleedAirSourceSet.Id, ApuBleedAirSourceSet);
            Events.Add(ApuBleedAirSourceToggle.Id, ApuBleedAirSourceToggle);
            Events.Add(ApuExtinguishFire.Id, ApuExtinguishFire);
            Events.Add(ApuGeneratorSwitchSet.Id, ApuGeneratorSwitchSet);
            Events.Add(ApuGeneratorSwitchToggle.Id, ApuGeneratorSwitchToggle);
            Events.Add(ApuOffSwitch.Id, ApuOffSwitch);
            Events.Add(ApuStarter.Id, ApuStarter);
            Events.Add(Atc.Id, Atc);
            Events.Add(AtcMenu0.Id, AtcMenu0);
            Events.Add(AtcMenu1.Id, AtcMenu1);
            Events.Add(AtcMenu2.Id, AtcMenu2);
            Events.Add(AtcMenu3.Id, AtcMenu3);
            Events.Add(AtcMenu4.Id, AtcMenu4);
            Events.Add(AtcMenu5.Id, AtcMenu5);
            Events.Add(AtcMenu6.Id, AtcMenu6);
            Events.Add(AtcMenu7.Id, AtcMenu7);
            Events.Add(AtcMenu8.Id, AtcMenu8);
            Events.Add(AtcMenu9.Id, AtcMenu9);
            Events.Add(AtcMenuClose.Id, AtcMenuClose);
            Events.Add(AtcMenuOpen.Id, AtcMenuOpen);
            Events.Add(AttitudeBarsPositionDown.Id, AttitudeBarsPositionDown);
            Events.Add(AttitudeBarsPositionSet.Id, AttitudeBarsPositionSet);
            Events.Add(AttitudeBarsPositionUp.Id, AttitudeBarsPositionUp);
            Events.Add(AttitudeCageButton.Id, AttitudeCageButton);
            Events.Add(AudioPanelVolumeDec.Id, AudioPanelVolumeDec);
            Events.Add(AudioPanelVolumeInc.Id, AudioPanelVolumeInc);
            Events.Add(AudioPanelVolumeSet.Id, AudioPanelVolumeSet);
            Events.Add(AutoHoverOff.Id, AutoHoverOff);
            Events.Add(AutoHoverOn.Id, AutoHoverOn);
            Events.Add(AutoHoverSet.Id, AutoHoverSet);
            Events.Add(AutoHoverToggle.Id, AutoHoverToggle);
            Events.Add(AutoThrottleArm.Id, AutoThrottleArm);
            Events.Add(AutoThrottleDisconnect.Id, AutoThrottleDisconnect);
            Events.Add(AutoThrottleToGa.Id, AutoThrottleToGa);
            Events.Add(AutobrakeDisarm.Id, AutobrakeDisarm);
            Events.Add(AutobrakeHiSet.Id, AutobrakeHiSet);
            Events.Add(AutobrakeLoSet.Id, AutobrakeLoSet);
            Events.Add(AutobrakeMedSet.Id, AutobrakeMedSet);
            Events.Add(AutocoordOff.Id, AutocoordOff);
            Events.Add(AutocoordOn.Id, AutocoordOn);
            Events.Add(AutocoordSet.Id, AutocoordSet);
            Events.Add(AutocoordToggle.Id, AutocoordToggle);
            Events.Add(AutopilotAirspeedAcquire.Id, AutopilotAirspeedAcquire);
            Events.Add(AutopilotDisengageSet.Id, AutopilotDisengageSet);
            Events.Add(AutopilotDisengageToggle.Id, AutopilotDisengageToggle);
            Events.Add(AutopilotOff.Id, AutopilotOff);
            Events.Add(AutopilotOn.Id, AutopilotOn);
            Events.Add(AutopilotPanelAirspeedSet.Id, AutopilotPanelAirspeedSet);
            Events.Add(AutopilotPanelCruiseSpeed.Id, AutopilotPanelCruiseSpeed);
            Events.Add(AutopilotPanelMaxSpeed.Id, AutopilotPanelMaxSpeed);
            Events.Add(AutorudderToggle.Id, AutorudderToggle);
            Events.Add(AvionicsMaster1Off.Id, AvionicsMaster1Off);
            Events.Add(AvionicsMaster1On.Id, AvionicsMaster1On);
            Events.Add(AvionicsMaster1Set.Id, AvionicsMaster1Set);
            Events.Add(AvionicsMaster2Off.Id, AvionicsMaster2Off);
            Events.Add(AvionicsMaster2On.Id, AvionicsMaster2On);
            Events.Add(AvionicsMaster2Set.Id, AvionicsMaster2Set);
            Events.Add(AvionicsMasterSet.Id, AvionicsMasterSet);
            Events.Add(AxisAileronsSet.Id, AxisAileronsSet);
            Events.Add(AxisCollectiveSet.Id, AxisCollectiveSet);
            Events.Add(AxisConditionLever1Set.Id, AxisConditionLever1Set);
            Events.Add(AxisConditionLever2Set.Id, AxisConditionLever2Set);
            Events.Add(AxisConditionLever3Set.Id, AxisConditionLever3Set);
            Events.Add(AxisConditionLever4Set.Id, AxisConditionLever4Set);
            Events.Add(AxisCyclicLateralSet.Id, AxisCyclicLateralSet);
            Events.Add(AxisCyclicLongitudinalSet.Id, AxisCyclicLongitudinalSet);
            Events.Add(AxisElevTrimSet.Id, AxisElevTrimSet);
            Events.Add(AxisElevatorSet.Id, AxisElevatorSet);
            Events.Add(AxisFlapsSet.Id, AxisFlapsSet);
            Events.Add(AxisHelicopterThrottleSet.Id, AxisHelicopterThrottleSet);
            Events.Add(AxisHelicopterThrottle1Set.Id, AxisHelicopterThrottle1Set);
            Events.Add(AxisHelicopterThrottle2Set.Id, AxisHelicopterThrottle2Set);
            Events.Add(AxisIndSet.Id, AxisIndSet);
            Events.Add(AxisLeftBrakeLinearSet.Id, AxisLeftBrakeLinearSet);
            Events.Add(AxisLeftBrakeSet.Id, AxisLeftBrakeSet);
            Events.Add(AxisMixtureSet.Id, AxisMixtureSet);
            Events.Add(AxisMixture1Set.Id, AxisMixture1Set);
            Events.Add(AxisMixture2Set.Id, AxisMixture2Set);
            Events.Add(AxisMixture3Set.Id, AxisMixture3Set);
            Events.Add(AxisMixture4Set.Id, AxisMixture4Set);
            Events.Add(AxisPanHeading.Id, AxisPanHeading);
            Events.Add(AxisPanPitch.Id, AxisPanPitch);
            Events.Add(AxisPanTilt.Id, AxisPanTilt);
            Events.Add(AxisPropellerSet.Id, AxisPropellerSet);
            Events.Add(AxisPropeller1Set.Id, AxisPropeller1Set);
            Events.Add(AxisPropeller2Set.Id, AxisPropeller2Set);
            Events.Add(AxisPropeller3Set.Id, AxisPropeller3Set);
            Events.Add(AxisPropeller4Set.Id, AxisPropeller4Set);
            Events.Add(AxisRightBrakeLinearSet.Id, AxisRightBrakeLinearSet);
            Events.Add(AxisRightBrakeSet.Id, AxisRightBrakeSet);
            Events.Add(AxisRotorBrakeSet.Id, AxisRotorBrakeSet);
            Events.Add(AxisRudderSet.Id, AxisRudderSet);
            Events.Add(AxisSlewAheadSet.Id, AxisSlewAheadSet);
            Events.Add(AxisSlewAltSet.Id, AxisSlewAltSet);
            Events.Add(AxisSlewBankSet.Id, AxisSlewBankSet);
            Events.Add(AxisSlewHeadingSet.Id, AxisSlewHeadingSet);
            Events.Add(AxisSlewPitchSet.Id, AxisSlewPitchSet);
            Events.Add(AxisSlewSidewaysSet.Id, AxisSlewSidewaysSet);
            Events.Add(AxisSpoilerSet.Id, AxisSpoilerSet);
            Events.Add(AxisSteeringSet.Id, AxisSteeringSet);
            Events.Add(AxisSteeringSetHelicopters.Id, AxisSteeringSetHelicopters);
            Events.Add(AxisTailRotorSet.Id, AxisTailRotorSet);
            Events.Add(AxisThrottleMinus.Id, AxisThrottleMinus);
            Events.Add(AxisThrottlePlus.Id, AxisThrottlePlus);
            Events.Add(AxisThrottleSet.Id, AxisThrottleSet);
            Events.Add(AxisThrottle1Set.Id, AxisThrottle1Set);
            Events.Add(AxisThrottle2Set.Id, AxisThrottle2Set);
            Events.Add(AxisThrottle3Set.Id, AxisThrottle3Set);
            Events.Add(AxisThrottle4Set.Id, AxisThrottle4Set);
            Events.Add(AxisVerticalSpeedSet.Id, AxisVerticalSpeedSet);
            Events.Add(BackToFly.Id, BackToFly);
            Events.Add(BailOut.Id, BailOut);
            Events.Add(Barometric.Id, Barometric);
            Events.Add(BarometricStdPressure.Id, BarometricStdPressure);
            Events.Add(Battery1Set.Id, Battery1Set);
            Events.Add(Battery2Set.Id, Battery2Set);
            Events.Add(Battery3Set.Id, Battery3Set);
            Events.Add(Battery4Set.Id, Battery4Set);
            Events.Add(BeaconLightsOff.Id, BeaconLightsOff);
            Events.Add(BeaconLightsOn.Id, BeaconLightsOn);
            Events.Add(BeaconLightsSet.Id, BeaconLightsSet);
            Events.Add(BleedAirSourceControlDec.Id, BleedAirSourceControlDec);
            Events.Add(BleedAirSourceControlInc.Id, BleedAirSourceControlInc);
            Events.Add(BleedAirSourceControlSet.Id, BleedAirSourceControlSet);
            Events.Add(BombView.Id, BombView);
            Events.Add(Brakes.Id, Brakes);
            Events.Add(BrakesLeft.Id, BrakesLeft);
            Events.Add(BrakesRight.Id, BrakesRight);
            Events.Add(BreakerAdfSet.Id, BreakerAdfSet);
            Events.Add(BreakerAdfToggle.Id, BreakerAdfToggle);
            Events.Add(BreakerAltfldSet.Id, BreakerAltfldSet);
            Events.Add(BreakerAltfldToggle.Id, BreakerAltfldToggle);
            Events.Add(BreakerAutopilotSet.Id, BreakerAutopilotSet);
            Events.Add(BreakerAutopilotToggle.Id, BreakerAutopilotToggle);
            Events.Add(BreakerAvnbus1Set.Id, BreakerAvnbus1Set);
            Events.Add(BreakerAvnbus1Toggle.Id, BreakerAvnbus1Toggle);
            Events.Add(BreakerAvnbus2Set.Id, BreakerAvnbus2Set);
            Events.Add(BreakerAvnbus2Toggle.Id, BreakerAvnbus2Toggle);
            Events.Add(BreakerAvnfanSet.Id, BreakerAvnfanSet);
            Events.Add(BreakerAvnfanToggle.Id, BreakerAvnfanToggle);
            Events.Add(BreakerFlapSet.Id, BreakerFlapSet);
            Events.Add(BreakerFlapToggle.Id, BreakerFlapToggle);
            Events.Add(BreakerGpsSet.Id, BreakerGpsSet);
            Events.Add(BreakerGpsToggle.Id, BreakerGpsToggle);
            Events.Add(BreakerInstSet.Id, BreakerInstSet);
            Events.Add(BreakerInstToggle.Id, BreakerInstToggle);
            Events.Add(BreakerInstltsSet.Id, BreakerInstltsSet);
            Events.Add(BreakerInstltsToggle.Id, BreakerInstltsToggle);
            Events.Add(BreakerNavcom1Set.Id, BreakerNavcom1Set);
            Events.Add(BreakerNavcom1Toggle.Id, BreakerNavcom1Toggle);
            Events.Add(BreakerNavcom2Set.Id, BreakerNavcom2Set);
            Events.Add(BreakerNavcom2Toggle.Id, BreakerNavcom2Toggle);
            Events.Add(BreakerNavcom3Set.Id, BreakerNavcom3Set);
            Events.Add(BreakerNavcom3Toggle.Id, BreakerNavcom3Toggle);
            Events.Add(BreakerTurncoordSet.Id, BreakerTurncoordSet);
            Events.Add(BreakerTurncoordToggle.Id, BreakerTurncoordToggle);
            Events.Add(BreakerWarnSet.Id, BreakerWarnSet);
            Events.Add(BreakerWarnToggle.Id, BreakerWarnToggle);
            Events.Add(BreakerXpndrSet.Id, BreakerXpndrSet);
            Events.Add(BreakerXpndrToggle.Id, BreakerXpndrToggle);
            Events.Add(CabinLightsOff.Id, CabinLightsOff);
            Events.Add(CabinLightsOn.Id, CabinLightsOn);
            Events.Add(CabinLightsPowerSettingSet.Id, CabinLightsPowerSettingSet);
            Events.Add(CabinLightsSet.Id, CabinLightsSet);
            Events.Add(CabinNoSmokingAlertSwitchToggle.Id, CabinNoSmokingAlertSwitchToggle);
            Events.Add(CabinSeatbeltsAlertSwitchToggle.Id, CabinSeatbeltsAlertSwitchToggle);
            Events.Add(CaptureScreenshot.Id, CaptureScreenshot);
            Events.Add(CenterAilerRudder.Id, CenterAilerRudder);
            Events.Add(CenterNt361Check.Id, CenterNt361Check);
            Events.Add(ChaseView.Id, ChaseView);
            Events.Add(ChaseViewNext.Id, ChaseViewNext);
            Events.Add(ChaseViewPrev.Id, ChaseViewPrev);
            Events.Add(ChaseViewToggle.Id, ChaseViewToggle);
            Events.Add(ChvppApAltWing.Id, ChvppApAltWing);
            Events.Add(ChvppLeftHatDown.Id, ChvppLeftHatDown);
            Events.Add(ChvppLeftHatUp.Id, ChvppLeftHatUp);
            Events.Add(ClockHoursDec.Id, ClockHoursDec);
            Events.Add(ClockHoursInc.Id, ClockHoursInc);
            Events.Add(ClockHoursSet.Id, ClockHoursSet);
            Events.Add(ClockMinutesDec.Id, ClockMinutesDec);
            Events.Add(ClockMinutesInc.Id, ClockMinutesInc);
            Events.Add(ClockMinutesSet.Id, ClockMinutesSet);
            Events.Add(ClockSecondsZero.Id, ClockSecondsZero);
            Events.Add(CloseView.Id, CloseView);
            Events.Add(CollectiveDecr.Id, CollectiveDecr);
            Events.Add(CollectiveIncr.Id, CollectiveIncr);
            Events.Add(Com1SpacingModeSwitch.Id, Com1SpacingModeSwitch);
            Events.Add(Com2SpacingModeSwitch.Id, Com2SpacingModeSwitch);
            Events.Add(Com3SpacingModeSwitch.Id, Com3SpacingModeSwitch);
            Events.Add(ComRadio.Id, ComRadio);
            Events.Add(ComRadioFractDec.Id, ComRadioFractDec);
            Events.Add(ComRadioFractDecCarry.Id, ComRadioFractDecCarry);
            Events.Add(ComRadioFractInc.Id, ComRadioFractInc);
            Events.Add(ComRadioFractIncCarry.Id, ComRadioFractIncCarry);
            Events.Add(ComRadioSet.Id, ComRadioSet);
            Events.Add(ComRadioSetHz.Id, ComRadioSetHz);
            Events.Add(ComRadioSwap.Id, ComRadioSwap);
            Events.Add(ComRadioWholeDec.Id, ComRadioWholeDec);
            Events.Add(ComRadioWholeInc.Id, ComRadioWholeInc);
            Events.Add(ComReceiveAllSet.Id, ComReceiveAllSet);
            Events.Add(ComReceiveAllToggle.Id, ComReceiveAllToggle);
            Events.Add(ComStbyRadioSet.Id, ComStbyRadioSet);
            Events.Add(ComStbyRadioSetHz.Id, ComStbyRadioSetHz);
            Events.Add(ComStbyRadioSwap.Id, ComStbyRadioSwap);
            Events.Add(Com1RadioSwap.Id, Com1RadioSwap);
            Events.Add(Com1ReceiveSelect.Id, Com1ReceiveSelect);
            Events.Add(Com1StoredFrequencyIndexSet.Id, Com1StoredFrequencyIndexSet);
            Events.Add(Com1StoredFrequencySet.Id, Com1StoredFrequencySet);
            Events.Add(Com1StoredFrequencySetHz.Id, Com1StoredFrequencySetHz);
            Events.Add(Com1TransmitSelect.Id, Com1TransmitSelect);
            Events.Add(Com1VolumeDec.Id, Com1VolumeDec);
            Events.Add(Com1VolumeInc.Id, Com1VolumeInc);
            Events.Add(Com1VolumeSet.Id, Com1VolumeSet);
            Events.Add(Com2RadioFractDec.Id, Com2RadioFractDec);
            Events.Add(Com2RadioFractDecCarry.Id, Com2RadioFractDecCarry);
            Events.Add(Com2RadioFractInc.Id, Com2RadioFractInc);
            Events.Add(Com2RadioFractIncCarry.Id, Com2RadioFractIncCarry);
            Events.Add(Com2RadioSet.Id, Com2RadioSet);
            Events.Add(Com2RadioSetHz.Id, Com2RadioSetHz);
            Events.Add(Com2RadioSwap.Id, Com2RadioSwap);
            Events.Add(Com2RadioWholeDec.Id, Com2RadioWholeDec);
            Events.Add(Com2RadioWholeInc.Id, Com2RadioWholeInc);
            Events.Add(Com2ReceiveSelect.Id, Com2ReceiveSelect);
            Events.Add(Com2StbyRadioSet.Id, Com2StbyRadioSet);
            Events.Add(Com2StbyRadioSetHz.Id, Com2StbyRadioSetHz);
            Events.Add(Com2StoredFrequencyIndexSet.Id, Com2StoredFrequencyIndexSet);
            Events.Add(Com2StoredFrequencySet.Id, Com2StoredFrequencySet);
            Events.Add(Com2StoredFrequencySetHz.Id, Com2StoredFrequencySetHz);
            Events.Add(Com2TransmitSelect.Id, Com2TransmitSelect);
            Events.Add(Com2VolumeDec.Id, Com2VolumeDec);
            Events.Add(Com2VolumeInc.Id, Com2VolumeInc);
            Events.Add(Com2VolumeSet.Id, Com2VolumeSet);
            Events.Add(Com3RadioFractDec.Id, Com3RadioFractDec);
            Events.Add(Com3RadioFractDecCarry.Id, Com3RadioFractDecCarry);
            Events.Add(Com3RadioFractInc.Id, Com3RadioFractInc);
            Events.Add(Com3RadioFractIncCarry.Id, Com3RadioFractIncCarry);
            Events.Add(Com3RadioSet.Id, Com3RadioSet);
            Events.Add(Com3RadioSetHz.Id, Com3RadioSetHz);
            Events.Add(Com3RadioSwap.Id, Com3RadioSwap);
            Events.Add(Com3RadioWholeDec.Id, Com3RadioWholeDec);
            Events.Add(Com3RadioWholeInc.Id, Com3RadioWholeInc);
            Events.Add(Com3ReceiveSelect.Id, Com3ReceiveSelect);
            Events.Add(Com3StbyRadioSet.Id, Com3StbyRadioSet);
            Events.Add(Com3StbyRadioSetHz.Id, Com3StbyRadioSetHz);
            Events.Add(Com3StoredFrequencyIndexSet.Id, Com3StoredFrequencyIndexSet);
            Events.Add(Com3StoredFrequencySet.Id, Com3StoredFrequencySet);
            Events.Add(Com3StoredFrequencySetHz.Id, Com3StoredFrequencySetHz);
            Events.Add(Com3VolumeDec.Id, Com3VolumeDec);
            Events.Add(Com3VolumeInc.Id, Com3VolumeInc);
            Events.Add(Com3VolumeSet.Id, Com3VolumeSet);
            Events.Add(ConcordeNoseVisorFullExt.Id, ConcordeNoseVisorFullExt);
            Events.Add(ConcordeNoseVisorFullRet.Id, ConcordeNoseVisorFullRet);
            Events.Add(ConditionLever1CutOff.Id, ConditionLever1CutOff);
            Events.Add(ConditionLever1Dec.Id, ConditionLever1Dec);
            Events.Add(ConditionLever1HighIdle.Id, ConditionLever1HighIdle);
            Events.Add(ConditionLever1Inc.Id, ConditionLever1Inc);
            Events.Add(ConditionLever1LowIdle.Id, ConditionLever1LowIdle);
            Events.Add(ConditionLever1Set.Id, ConditionLever1Set);
            Events.Add(ConditionLever2CutOff.Id, ConditionLever2CutOff);
            Events.Add(ConditionLever2Dec.Id, ConditionLever2Dec);
            Events.Add(ConditionLever2HighIdle.Id, ConditionLever2HighIdle);
            Events.Add(ConditionLever2Inc.Id, ConditionLever2Inc);
            Events.Add(ConditionLever2LowIdle.Id, ConditionLever2LowIdle);
            Events.Add(ConditionLever2Set.Id, ConditionLever2Set);
            Events.Add(ConditionLever3CutOff.Id, ConditionLever3CutOff);
            Events.Add(ConditionLever3Dec.Id, ConditionLever3Dec);
            Events.Add(ConditionLever3HighIdle.Id, ConditionLever3HighIdle);
            Events.Add(ConditionLever3Inc.Id, ConditionLever3Inc);
            Events.Add(ConditionLever3LowIdle.Id, ConditionLever3LowIdle);
            Events.Add(ConditionLever3Set.Id, ConditionLever3Set);
            Events.Add(ConditionLever4CutOff.Id, ConditionLever4CutOff);
            Events.Add(ConditionLever4Dec.Id, ConditionLever4Dec);
            Events.Add(ConditionLever4HighIdle.Id, ConditionLever4HighIdle);
            Events.Add(ConditionLever4Inc.Id, ConditionLever4Inc);
            Events.Add(ConditionLever4LowIdle.Id, ConditionLever4LowIdle);
            Events.Add(ConditionLever4Set.Id, ConditionLever4Set);
            Events.Add(ConditionLeverCutOff.Id, ConditionLeverCutOff);
            Events.Add(ConditionLeverDec.Id, ConditionLeverDec);
            Events.Add(ConditionLeverHighIdle.Id, ConditionLeverHighIdle);
            Events.Add(ConditionLeverInc.Id, ConditionLeverInc);
            Events.Add(ConditionLeverLowIdle.Id, ConditionLeverLowIdle);
            Events.Add(ConditionLeverSet.Id, ConditionLeverSet);
            Events.Add(CopilotTransmitterSet.Id, CopilotTransmitterSet);
            Events.Add(Cowlflap1Set.Id, Cowlflap1Set);
            Events.Add(Cowlflap2Set.Id, Cowlflap2Set);
            Events.Add(Cowlflap3Set.Id, Cowlflap3Set);
            Events.Add(Cowlflap4Set.Id, Cowlflap4Set);
            Events.Add(CrossFeedLeftToRight.Id, CrossFeedLeftToRight);
            Events.Add(CrossFeedOff.Id, CrossFeedOff);
            Events.Add(CrossFeedOpen.Id, CrossFeedOpen);
            Events.Add(CrossFeedRightToLeft.Id, CrossFeedRightToLeft);
            Events.Add(CrossFeedToggle.Id, CrossFeedToggle);
            Events.Add(CyclicLateralLeft.Id, CyclicLateralLeft);
            Events.Add(CyclicLateralRight.Id, CyclicLateralRight);
            Events.Add(CyclicLongitudinalDown.Id, CyclicLongitudinalDown);
            Events.Add(CyclicLongitudinalUp.Id, CyclicLongitudinalUp);
            Events.Add(Debug0.Id, Debug0);
            Events.Add(Debug1.Id, Debug1);
            Events.Add(Debug2.Id, Debug2);
            Events.Add(Debug3.Id, Debug3);
            Events.Add(Debug4.Id, Debug4);
            Events.Add(Debug5.Id, Debug5);
            Events.Add(Debug6.Id, Debug6);
            Events.Add(Debug7.Id, Debug7);
            Events.Add(Debug8.Id, Debug8);
            Events.Add(Debug9.Id, Debug9);
            Events.Add(DebugA.Id, DebugA);
            Events.Add(DebugB.Id, DebugB);
            Events.Add(DebugC.Id, DebugC);
            Events.Add(DebugD.Id, DebugD);
            Events.Add(DebugE.Id, DebugE);
            Events.Add(DebugF.Id, DebugF);
            Events.Add(DebugG.Id, DebugG);
            Events.Add(DebugH.Id, DebugH);
            Events.Add(DebugI.Id, DebugI);
            Events.Add(DebugJ.Id, DebugJ);
            Events.Add(DebugK.Id, DebugK);
            Events.Add(DebugL.Id, DebugL);
            Events.Add(DebugM.Id, DebugM);
            Events.Add(DebugN.Id, DebugN);
            Events.Add(DebugO.Id, DebugO);
            Events.Add(DebugP.Id, DebugP);
            Events.Add(DebugQ.Id, DebugQ);
            Events.Add(DebugR.Id, DebugR);
            Events.Add(DebugS.Id, DebugS);
            Events.Add(DebugT.Id, DebugT);
            Events.Add(DebugU.Id, DebugU);
            Events.Add(DebugV.Id, DebugV);
            Events.Add(DebugW.Id, DebugW);
            Events.Add(DebugX.Id, DebugX);
            Events.Add(DebugY.Id, DebugY);
            Events.Add(DebugZ.Id, DebugZ);
            Events.Add(DecConcordeNoseVisor.Id, DecConcordeNoseVisor);
            Events.Add(DecCowlFlaps.Id, DecCowlFlaps);
            Events.Add(DecCowlFlaps1.Id, DecCowlFlaps1);
            Events.Add(DecCowlFlaps2.Id, DecCowlFlaps2);
            Events.Add(DecCowlFlaps3.Id, DecCowlFlaps3);
            Events.Add(DecCowlFlaps4.Id, DecCowlFlaps4);
            Events.Add(DecisionHeightSet.Id, DecisionHeightSet);
            Events.Add(DecreaseAutobrakeControl.Id, DecreaseAutobrakeControl);
            Events.Add(DecreaseDecisionAltitudeMsl.Id, DecreaseDecisionAltitudeMsl);
            Events.Add(DecreaseDecisionHeight.Id, DecreaseDecisionHeight);
            Events.Add(DecreaseHeloGovBeep.Id, DecreaseHeloGovBeep);
            Events.Add(DecreaseThrottle.Id, DecreaseThrottle);
            Events.Add(DemoRecord1Sec.Id, DemoRecord1Sec);
            Events.Add(DemoRecord5Sec.Id, DemoRecord5Sec);
            Events.Add(DemoRecordStop.Id, DemoRecordStop);
            Events.Add(DemoStop.Id, DemoStop);
            Events.Add(Dme.Id, Dme);
            Events.Add(DmeSelect.Id, DmeSelect);
            Events.Add(Dme1Toggle.Id, Dme1Toggle);
            Events.Add(Dme2Toggle.Id, Dme2Toggle);
            Events.Add(Egt.Id, Egt);
            Events.Add(EgtDec.Id, EgtDec);
            Events.Add(EgtInc.Id, EgtInc);
            Events.Add(EgtSet.Id, EgtSet);
            Events.Add(Egt1Dec.Id, Egt1Dec);
            Events.Add(Egt1Inc.Id, Egt1Inc);
            Events.Add(Egt1Set.Id, Egt1Set);
            Events.Add(Egt2Dec.Id, Egt2Dec);
            Events.Add(Egt2Inc.Id, Egt2Inc);
            Events.Add(Egt2Set.Id, Egt2Set);
            Events.Add(Egt3Dec.Id, Egt3Dec);
            Events.Add(Egt3Inc.Id, Egt3Inc);
            Events.Add(Egt3Set.Id, Egt3Set);
            Events.Add(Egt4Dec.Id, Egt4Dec);
            Events.Add(Egt4Inc.Id, Egt4Inc);
            Events.Add(Egt4Set.Id, Egt4Set);
            Events.Add(ElectFuelPump1Set.Id, ElectFuelPump1Set);
            Events.Add(ElectFuelPump2Set.Id, ElectFuelPump2Set);
            Events.Add(ElectFuelPump3Set.Id, ElectFuelPump3Set);
            Events.Add(ElectFuelPump4Set.Id, ElectFuelPump4Set);
            Events.Add(ElectricalAlternatorBreakerToggle.Id, ElectricalAlternatorBreakerToggle);
            Events.Add(ElectricalBatteryBreakerToggle.Id, ElectricalBatteryBreakerToggle);
            Events.Add(ElectricalBusBreakerToggle.Id, ElectricalBusBreakerToggle);
            Events.Add(ElectricalBusToAlternatorConnectionToggle.Id, ElectricalBusToAlternatorConnectionToggle);
            Events.Add(ElectricalBusToBatteryConnectionToggle.Id, ElectricalBusToBatteryConnectionToggle);
            Events.Add(ElectricalBusToBusConnectionToggle.Id, ElectricalBusToBusConnectionToggle);
            Events.Add(ElectricalBusToCircuitConnectionToggle.Id, ElectricalBusToCircuitConnectionToggle);
            Events.Add(ElectricalBusToExternalPowerConnectionToggle.Id, ElectricalBusToExternalPowerConnectionToggle);
            Events.Add(ElectricalCircuitBreakerToggle.Id, ElectricalCircuitBreakerToggle);
            Events.Add(ElectricalCircuitPowerSettingSet.Id, ElectricalCircuitPowerSettingSet);
            Events.Add(ElectricalCircuitToggle.Id, ElectricalCircuitToggle);
            Events.Add(ElectricalExecuteProcedure.Id, ElectricalExecuteProcedure);
            Events.Add(ElectricalExternalPowerBreakerToggle.Id, ElectricalExternalPowerBreakerToggle);
            Events.Add(ElevDown.Id, ElevDown);
            Events.Add(ElevTrimDn.Id, ElevTrimDn);
            Events.Add(ElevTrimUp.Id, ElevTrimUp);
            Events.Add(ElevUp.Id, ElevUp);
            Events.Add(ElevatorDown.Id, ElevatorDown);
            Events.Add(ElevatorSet.Id, ElevatorSet);
            Events.Add(ElevatorTrimDisabledSet.Id, ElevatorTrimDisabledSet);
            Events.Add(ElevatorTrimDisabledToggle.Id, ElevatorTrimDisabledToggle);
            Events.Add(ElevatorTrimSet.Id, ElevatorTrimSet);
            Events.Add(ElevatorUp.Id, ElevatorUp);
            Events.Add(EltOff.Id, EltOff);
            Events.Add(EltOn.Id, EltOn);
            Events.Add(EltSet.Id, EltSet);
            Events.Add(EltToggle.Id, EltToggle);
            Events.Add(Engine.Id, Engine);
            Events.Add(EngineAutoShutdown.Id, EngineAutoShutdown);
            Events.Add(EngineAutoStart.Id, EngineAutoStart);
            Events.Add(EngineBleedAirSourceSet.Id, EngineBleedAirSourceSet);
            Events.Add(EngineBleedAirSourceToggle.Id, EngineBleedAirSourceToggle);
            Events.Add(EngineFuelflowBugPosition1.Id, EngineFuelflowBugPosition1);
            Events.Add(EngineFuelflowBugPosition2.Id, EngineFuelflowBugPosition2);
            Events.Add(EngineFuelflowBugPosition3.Id, EngineFuelflowBugPosition3);
            Events.Add(EngineFuelflowBugPosition4.Id, EngineFuelflowBugPosition4);
            Events.Add(EngineMaster1Set.Id, EngineMaster1Set);
            Events.Add(EngineMaster1Toggle.Id, EngineMaster1Toggle);
            Events.Add(EngineMaster2Set.Id, EngineMaster2Set);
            Events.Add(EngineMaster2Toggle.Id, EngineMaster2Toggle);
            Events.Add(EngineMaster3Set.Id, EngineMaster3Set);
            Events.Add(EngineMaster3Toggle.Id, EngineMaster3Toggle);
            Events.Add(EngineMaster4Set.Id, EngineMaster4Set);
            Events.Add(EngineMaster4Toggle.Id, EngineMaster4Toggle);
            Events.Add(EngineMasterSet.Id, EngineMasterSet);
            Events.Add(EngineMasterToggle.Id, EngineMasterToggle);
            Events.Add(EngineModeCrankSet.Id, EngineModeCrankSet);
            Events.Add(EngineModeIgnStart.Id, EngineModeIgnStart);
            Events.Add(EngineModeNormSet.Id, EngineModeNormSet);
            Events.Add(EnginePrimer.Id, EnginePrimer);
            Events.Add(Exit.Id, Exit);
            Events.Add(ExternalSystemSet.Id, ExternalSystemSet);
            Events.Add(ExternalSystemToggle.Id, ExternalSystemToggle);
            Events.Add(ExtinguishEngineFire.Id, ExtinguishEngineFire);
            Events.Add(EyepointBack.Id, EyepointBack);
            Events.Add(EyepointDown.Id, EyepointDown);
            Events.Add(EyepointForward.Id, EyepointForward);
            Events.Add(EyepointLeft.Id, EyepointLeft);
            Events.Add(EyepointReset.Id, EyepointReset);
            Events.Add(EyepointRight.Id, EyepointRight);
            Events.Add(EyepointUp.Id, EyepointUp);
            Events.Add(FireAllGuns.Id, FireAllGuns);
            Events.Add(FirePrimaryGuns.Id, FirePrimaryGuns);
            Events.Add(FireSecondaryGuns.Id, FireSecondaryGuns);
            Events.Add(Flaps1.Id, Flaps1);
            Events.Add(Flaps2.Id, Flaps2);
            Events.Add(Flaps3.Id, Flaps3);
            Events.Add(Flaps4.Id, Flaps4);
            Events.Add(FlapsContinuousDecr.Id, FlapsContinuousDecr);
            Events.Add(FlapsContinuousIncr.Id, FlapsContinuousIncr);
            Events.Add(FlapsContinuousSet.Id, FlapsContinuousSet);
            Events.Add(FlapsDecr.Id, FlapsDecr);
            Events.Add(FlapsDown.Id, FlapsDown);
            Events.Add(FlapsIncr.Id, FlapsIncr);
            Events.Add(FlapsSet.Id, FlapsSet);
            Events.Add(FlapsUp.Id, FlapsUp);
            Events.Add(FlightLevelChange.Id, FlightLevelChange);
            Events.Add(FlightLevelChangeOff.Id, FlightLevelChangeOff);
            Events.Add(FlightLevelChangeOn.Id, FlightLevelChangeOn);
            Events.Add(FlightMap.Id, FlightMap);
            Events.Add(FlyByWireElacToggle.Id, FlyByWireElacToggle);
            Events.Add(FlyByWireFacToggle.Id, FlyByWireFacToggle);
            Events.Add(FlyByWireSecToggle.Id, FlyByWireSecToggle);
            Events.Add(ForceEnd.Id, ForceEnd);
            Events.Add(FreezeAltitudeSet.Id, FreezeAltitudeSet);
            Events.Add(FreezeAltitudeToggle.Id, FreezeAltitudeToggle);
            Events.Add(FreezeAttitudeSet.Id, FreezeAttitudeSet);
            Events.Add(FreezeAttitudeToggle.Id, FreezeAttitudeToggle);
            Events.Add(FreezeLatitudeLongitudeSet.Id, FreezeLatitudeLongitudeSet);
            Events.Add(FreezeLatitudeLongitudeToggle.Id, FreezeLatitudeLongitudeToggle);
            Events.Add(FrequencySwap.Id, FrequencySwap);
            Events.Add(FuelDumpSwitchSet.Id, FuelDumpSwitchSet);
            Events.Add(FuelDumpToggle.Id, FuelDumpToggle);
            Events.Add(FuelPump.Id, FuelPump);
            Events.Add(FuelSelector1Crossfeed.Id, FuelSelector1Crossfeed);
            Events.Add(FuelSelector1Isolate.Id, FuelSelector1Isolate);
            Events.Add(FuelSelector2All.Id, FuelSelector2All);
            Events.Add(FuelSelector2Center.Id, FuelSelector2Center);
            Events.Add(FuelSelector2Crossfeed.Id, FuelSelector2Crossfeed);
            Events.Add(FuelSelector2Isolate.Id, FuelSelector2Isolate);
            Events.Add(FuelSelector2Left.Id, FuelSelector2Left);
            Events.Add(FuelSelector2LeftAux.Id, FuelSelector2LeftAux);
            Events.Add(FuelSelector2LeftMain.Id, FuelSelector2LeftMain);
            Events.Add(FuelSelector2Off.Id, FuelSelector2Off);
            Events.Add(FuelSelector2Right.Id, FuelSelector2Right);
            Events.Add(FuelSelector2RightAux.Id, FuelSelector2RightAux);
            Events.Add(FuelSelector2RightMain.Id, FuelSelector2RightMain);
            Events.Add(FuelSelector2Set.Id, FuelSelector2Set);
            Events.Add(FuelSelector3All.Id, FuelSelector3All);
            Events.Add(FuelSelector3Center.Id, FuelSelector3Center);
            Events.Add(FuelSelector3Crossfeed.Id, FuelSelector3Crossfeed);
            Events.Add(FuelSelector3Isolate.Id, FuelSelector3Isolate);
            Events.Add(FuelSelector3Left.Id, FuelSelector3Left);
            Events.Add(FuelSelector3LeftAux.Id, FuelSelector3LeftAux);
            Events.Add(FuelSelector3LeftMain.Id, FuelSelector3LeftMain);
            Events.Add(FuelSelector3Off.Id, FuelSelector3Off);
            Events.Add(FuelSelector3Right.Id, FuelSelector3Right);
            Events.Add(FuelSelector3RightAux.Id, FuelSelector3RightAux);
            Events.Add(FuelSelector3RightMain.Id, FuelSelector3RightMain);
            Events.Add(FuelSelector3Set.Id, FuelSelector3Set);
            Events.Add(FuelSelector4All.Id, FuelSelector4All);
            Events.Add(FuelSelector4Center.Id, FuelSelector4Center);
            Events.Add(FuelSelector4Crossfeed.Id, FuelSelector4Crossfeed);
            Events.Add(FuelSelector4Isolate.Id, FuelSelector4Isolate);
            Events.Add(FuelSelector4Left.Id, FuelSelector4Left);
            Events.Add(FuelSelector4LeftAux.Id, FuelSelector4LeftAux);
            Events.Add(FuelSelector4LeftMain.Id, FuelSelector4LeftMain);
            Events.Add(FuelSelector4Off.Id, FuelSelector4Off);
            Events.Add(FuelSelector4Right.Id, FuelSelector4Right);
            Events.Add(FuelSelector4RightAux.Id, FuelSelector4RightAux);
            Events.Add(FuelSelector4RightMain.Id, FuelSelector4RightMain);
            Events.Add(FuelSelector4Set.Id, FuelSelector4Set);
            Events.Add(FuelSelectorAll.Id, FuelSelectorAll);
            Events.Add(FuelSelectorCenter.Id, FuelSelectorCenter);
            Events.Add(FuelSelectorLeft.Id, FuelSelectorLeft);
            Events.Add(FuelSelectorLeftAux.Id, FuelSelectorLeftAux);
            Events.Add(FuelSelectorLeftMain.Id, FuelSelectorLeftMain);
            Events.Add(FuelSelectorOff.Id, FuelSelectorOff);
            Events.Add(FuelSelectorRight.Id, FuelSelectorRight);
            Events.Add(FuelSelectorRightAux.Id, FuelSelectorRightAux);
            Events.Add(FuelSelectorRightMain.Id, FuelSelectorRightMain);
            Events.Add(FuelSelectorSet.Id, FuelSelectorSet);
            Events.Add(FuelTransferCustomIndexToggle.Id, FuelTransferCustomIndexToggle);
            Events.Add(FuelsystemJunctionSet.Id, FuelsystemJunctionSet);
            Events.Add(FuelsystemPumpOff.Id, FuelsystemPumpOff);
            Events.Add(FuelsystemPumpOn.Id, FuelsystemPumpOn);
            Events.Add(FuelsystemPumpSet.Id, FuelsystemPumpSet);
            Events.Add(FuelsystemPumpToggle.Id, FuelsystemPumpToggle);
            Events.Add(FuelsystemTriggerOff.Id, FuelsystemTriggerOff);
            Events.Add(FuelsystemTriggerOn.Id, FuelsystemTriggerOn);
            Events.Add(FuelsystemTriggerSet.Id, FuelsystemTriggerSet);
            Events.Add(FuelsystemTriggerToggle.Id, FuelsystemTriggerToggle);
            Events.Add(FuelsystemValveClose.Id, FuelsystemValveClose);
            Events.Add(FuelsystemValveOpen.Id, FuelsystemValveOpen);
            Events.Add(FuelsystemValveSet.Id, FuelsystemValveSet);
            Events.Add(FuelsystemValveToggle.Id, FuelsystemValveToggle);
            Events.Add(FullWindowToggle.Id, FullWindowToggle);
            Events.Add(GLimiterOff.Id, GLimiterOff);
            Events.Add(GLimiterOn.Id, GLimiterOn);
            Events.Add(GLimiterSet.Id, GLimiterSet);
            Events.Add(GLimiterToggle.Id, GLimiterToggle);
            Events.Add(G1000MfdClearButton.Id, G1000MfdClearButton);
            Events.Add(G1000MfdCursorButton.Id, G1000MfdCursorButton);
            Events.Add(G1000MfdDirecttoButton.Id, G1000MfdDirecttoButton);
            Events.Add(G1000MfdEnterButton.Id, G1000MfdEnterButton);
            Events.Add(G1000MfdFlightplanButton.Id, G1000MfdFlightplanButton);
            Events.Add(G1000MfdGroupKnobDec.Id, G1000MfdGroupKnobDec);
            Events.Add(G1000MfdGroupKnobInc.Id, G1000MfdGroupKnobInc);
            Events.Add(G1000MfdMenuButton.Id, G1000MfdMenuButton);
            Events.Add(G1000MfdPageKnobDec.Id, G1000MfdPageKnobDec);
            Events.Add(G1000MfdPageKnobInc.Id, G1000MfdPageKnobInc);
            Events.Add(G1000MfdProcedureButton.Id, G1000MfdProcedureButton);
            Events.Add(G1000MfdSoftkey1.Id, G1000MfdSoftkey1);
            Events.Add(G1000MfdSoftkey10.Id, G1000MfdSoftkey10);
            Events.Add(G1000MfdSoftkey11.Id, G1000MfdSoftkey11);
            Events.Add(G1000MfdSoftkey12.Id, G1000MfdSoftkey12);
            Events.Add(G1000MfdSoftkey2.Id, G1000MfdSoftkey2);
            Events.Add(G1000MfdSoftkey3.Id, G1000MfdSoftkey3);
            Events.Add(G1000MfdSoftkey4.Id, G1000MfdSoftkey4);
            Events.Add(G1000MfdSoftkey5.Id, G1000MfdSoftkey5);
            Events.Add(G1000MfdSoftkey6.Id, G1000MfdSoftkey6);
            Events.Add(G1000MfdSoftkey7.Id, G1000MfdSoftkey7);
            Events.Add(G1000MfdSoftkey8.Id, G1000MfdSoftkey8);
            Events.Add(G1000MfdSoftkey9.Id, G1000MfdSoftkey9);
            Events.Add(G1000MfdZoominButton.Id, G1000MfdZoominButton);
            Events.Add(G1000MfdZoomoutButton.Id, G1000MfdZoomoutButton);
            Events.Add(G1000PfdClearButton.Id, G1000PfdClearButton);
            Events.Add(G1000PfdCursorButton.Id, G1000PfdCursorButton);
            Events.Add(G1000PfdDirecttoButton.Id, G1000PfdDirecttoButton);
            Events.Add(G1000PfdEnterButton.Id, G1000PfdEnterButton);
            Events.Add(G1000PfdFlightplanButton.Id, G1000PfdFlightplanButton);
            Events.Add(G1000PfdGroupKnobDec.Id, G1000PfdGroupKnobDec);
            Events.Add(G1000PfdGroupKnobInc.Id, G1000PfdGroupKnobInc);
            Events.Add(G1000PfdMenuButton.Id, G1000PfdMenuButton);
            Events.Add(G1000PfdPageKnobDec.Id, G1000PfdPageKnobDec);
            Events.Add(G1000PfdPageKnobInc.Id, G1000PfdPageKnobInc);
            Events.Add(G1000PfdProcedureButton.Id, G1000PfdProcedureButton);
            Events.Add(G1000PfdSoftkey1.Id, G1000PfdSoftkey1);
            Events.Add(G1000PfdSoftkey10.Id, G1000PfdSoftkey10);
            Events.Add(G1000PfdSoftkey11.Id, G1000PfdSoftkey11);
            Events.Add(G1000PfdSoftkey12.Id, G1000PfdSoftkey12);
            Events.Add(G1000PfdSoftkey2.Id, G1000PfdSoftkey2);
            Events.Add(G1000PfdSoftkey3.Id, G1000PfdSoftkey3);
            Events.Add(G1000PfdSoftkey4.Id, G1000PfdSoftkey4);
            Events.Add(G1000PfdSoftkey5.Id, G1000PfdSoftkey5);
            Events.Add(G1000PfdSoftkey6.Id, G1000PfdSoftkey6);
            Events.Add(G1000PfdSoftkey7.Id, G1000PfdSoftkey7);
            Events.Add(G1000PfdSoftkey8.Id, G1000PfdSoftkey8);
            Events.Add(G1000PfdSoftkey9.Id, G1000PfdSoftkey9);
            Events.Add(G1000PfdZoominButton.Id, G1000PfdZoominButton);
            Events.Add(G1000PfdZoomoutButton.Id, G1000PfdZoomoutButton);
            Events.Add(GaugeKeystroke.Id, GaugeKeystroke);
            Events.Add(GearDown.Id, GearDown);
            Events.Add(GearEmergencyHandleToggle.Id, GearEmergencyHandleToggle);
            Events.Add(GearPump.Id, GearPump);
            Events.Add(GearSet.Id, GearSet);
            Events.Add(GearToggle.Id, GearToggle);
            Events.Add(GearUp.Id, GearUp);
            Events.Add(GlareshieldLightsOff.Id, GlareshieldLightsOff);
            Events.Add(GlareshieldLightsOn.Id, GlareshieldLightsOn);
            Events.Add(GlareshieldLightsPowerSettingSet.Id, GlareshieldLightsPowerSettingSet);
            Events.Add(GlareshieldLightsSet.Id, GlareshieldLightsSet);
            Events.Add(GlareshieldLightsToggle.Id, GlareshieldLightsToggle);
            Events.Add(GpsActivateButton.Id, GpsActivateButton);
            Events.Add(GpsButton1.Id, GpsButton1);
            Events.Add(GpsButton2.Id, GpsButton2);
            Events.Add(GpsButton3.Id, GpsButton3);
            Events.Add(GpsButton4.Id, GpsButton4);
            Events.Add(GpsButton5.Id, GpsButton5);
            Events.Add(GpsClearAllButton.Id, GpsClearAllButton);
            Events.Add(GpsClearButton.Id, GpsClearButton);
            Events.Add(GpsClearButtonDown.Id, GpsClearButtonDown);
            Events.Add(GpsClearButtonUp.Id, GpsClearButtonUp);
            Events.Add(GpsCursorButton.Id, GpsCursorButton);
            Events.Add(GpsDirecttoButton.Id, GpsDirecttoButton);
            Events.Add(GpsEnterButton.Id, GpsEnterButton);
            Events.Add(GpsFlightplanButton.Id, GpsFlightplanButton);
            Events.Add(GpsGroupKnobDec.Id, GpsGroupKnobDec);
            Events.Add(GpsGroupKnobInc.Id, GpsGroupKnobInc);
            Events.Add(GpsMenuButton.Id, GpsMenuButton);
            Events.Add(GpsMsgButton.Id, GpsMsgButton);
            Events.Add(GpsMsgButtonDown.Id, GpsMsgButtonDown);
            Events.Add(GpsMsgButtonUp.Id, GpsMsgButtonUp);
            Events.Add(GpsNearestButton.Id, GpsNearestButton);
            Events.Add(GpsObs.Id, GpsObs);
            Events.Add(GpsObsButton.Id, GpsObsButton);
            Events.Add(GpsObsDec.Id, GpsObsDec);
            Events.Add(GpsObsInc.Id, GpsObsInc);
            Events.Add(GpsObsOff.Id, GpsObsOff);
            Events.Add(GpsObsOn.Id, GpsObsOn);
            Events.Add(GpsObsSet.Id, GpsObsSet);
            Events.Add(GpsPageKnobDec.Id, GpsPageKnobDec);
            Events.Add(GpsPageKnobInc.Id, GpsPageKnobInc);
            Events.Add(GpsPowerButton.Id, GpsPowerButton);
            Events.Add(GpsProcedureButton.Id, GpsProcedureButton);
            Events.Add(GpsSetupButton.Id, GpsSetupButton);
            Events.Add(GpsTerrainButton.Id, GpsTerrainButton);
            Events.Add(GpsVnavButton.Id, GpsVnavButton);
            Events.Add(GpsZoominButton.Id, GpsZoominButton);
            Events.Add(GpsZoomoutButton.Id, GpsZoomoutButton);
            Events.Add(GpwsSwitchToggle.Id, GpwsSwitchToggle);
            Events.Add(GunsightSel.Id, GunsightSel);
            Events.Add(GunsightToggle.Id, GunsightToggle);
            Events.Add(GyroDriftDec.Id, GyroDriftDec);
            Events.Add(GyroDriftInc.Id, GyroDriftInc);
            Events.Add(GyroDriftSet.Id, GyroDriftSet);
            Events.Add(GyroDriftSetEx1.Id, GyroDriftSetEx1);
            Events.Add(HeadingBugDec.Id, HeadingBugDec);
            Events.Add(HeadingBugInc.Id, HeadingBugInc);
            Events.Add(HeadingBugSelect.Id, HeadingBugSelect);
            Events.Add(HeadingBugSet.Id, HeadingBugSet);
            Events.Add(HeadingGyroSet.Id, HeadingGyroSet);
            Events.Add(HeadingSlotIndexSet.Id, HeadingSlotIndexSet);
            Events.Add(HeliBeepDecrease.Id, HeliBeepDecrease);
            Events.Add(HeliBeepIncrease.Id, HeliBeepIncrease);
            Events.Add(HelicopterEngine1BeepTrimDecrease.Id, HelicopterEngine1BeepTrimDecrease);
            Events.Add(HelicopterEngine1BeepTrimIncrease.Id, HelicopterEngine1BeepTrimIncrease);
            Events.Add(HelicopterEngine1BeepTrimSet.Id, HelicopterEngine1BeepTrimSet);
            Events.Add(HelicopterEngine1GovernorSwitchOff.Id, HelicopterEngine1GovernorSwitchOff);
            Events.Add(HelicopterEngine1GovernorSwitchOn.Id, HelicopterEngine1GovernorSwitchOn);
            Events.Add(HelicopterEngine1GovernorSwitchSet.Id, HelicopterEngine1GovernorSwitchSet);
            Events.Add(HelicopterEngine1GovernorSwitchToggle.Id, HelicopterEngine1GovernorSwitchToggle);
            Events.Add(HelicopterEngine2BeepTrimDecrease.Id, HelicopterEngine2BeepTrimDecrease);
            Events.Add(HelicopterEngine2BeepTrimIncrease.Id, HelicopterEngine2BeepTrimIncrease);
            Events.Add(HelicopterEngine2BeepTrimSet.Id, HelicopterEngine2BeepTrimSet);
            Events.Add(HelicopterEngine2GovernorSwitchOff.Id, HelicopterEngine2GovernorSwitchOff);
            Events.Add(HelicopterEngine2GovernorSwitchOn.Id, HelicopterEngine2GovernorSwitchOn);
            Events.Add(HelicopterEngine2GovernorSwitchSet.Id, HelicopterEngine2GovernorSwitchSet);
            Events.Add(HelicopterEngine2GovernorSwitchToggle.Id, HelicopterEngine2GovernorSwitchToggle);
            Events.Add(HelicopterThrottleCut.Id, HelicopterThrottleCut);
            Events.Add(HelicopterThrottleDec.Id, HelicopterThrottleDec);
            Events.Add(HelicopterThrottleFull.Id, HelicopterThrottleFull);
            Events.Add(HelicopterThrottleInc.Id, HelicopterThrottleInc);
            Events.Add(HelicopterThrottleSet.Id, HelicopterThrottleSet);
            Events.Add(HelicopterThrottle1Cut.Id, HelicopterThrottle1Cut);
            Events.Add(HelicopterThrottle1Dec.Id, HelicopterThrottle1Dec);
            Events.Add(HelicopterThrottle1Full.Id, HelicopterThrottle1Full);
            Events.Add(HelicopterThrottle1Inc.Id, HelicopterThrottle1Inc);
            Events.Add(HelicopterThrottle1Set.Id, HelicopterThrottle1Set);
            Events.Add(HelicopterThrottle2Cut.Id, HelicopterThrottle2Cut);
            Events.Add(HelicopterThrottle2Dec.Id, HelicopterThrottle2Dec);
            Events.Add(HelicopterThrottle2Full.Id, HelicopterThrottle2Full);
            Events.Add(HelicopterThrottle2Inc.Id, HelicopterThrottle2Inc);
            Events.Add(HelicopterThrottle2Set.Id, HelicopterThrottle2Set);
            Events.Add(HoistDeploySet.Id, HoistDeploySet);
            Events.Add(HoistDeployToggle.Id, HoistDeployToggle);
            Events.Add(HoistSwitchExtend.Id, HoistSwitchExtend);
            Events.Add(HoistSwitchRetract.Id, HoistSwitchRetract);
            Events.Add(HoistSwitchSelect.Id, HoistSwitchSelect);
            Events.Add(HoistSwitchSet.Id, HoistSwitchSet);
            Events.Add(HornTrigger.Id, HornTrigger);
            Events.Add(HudColor.Id, HudColor);
            Events.Add(HudUnits.Id, HudUnits);
            Events.Add(HydraulicSwitchToggle.Id, HydraulicSwitchToggle);
            Events.Add(IncConcordeNoseVisor.Id, IncConcordeNoseVisor);
            Events.Add(IncCowlFlaps.Id, IncCowlFlaps);
            Events.Add(IncCowlFlaps1.Id, IncCowlFlaps1);
            Events.Add(IncCowlFlaps2.Id, IncCowlFlaps2);
            Events.Add(IncCowlFlaps3.Id, IncCowlFlaps3);
            Events.Add(IncCowlFlaps4.Id, IncCowlFlaps4);
            Events.Add(IncreaseAutobrakeControl.Id, IncreaseAutobrakeControl);
            Events.Add(IncreaseDecisionAltitudeMsl.Id, IncreaseDecisionAltitudeMsl);
            Events.Add(IncreaseDecisionHeight.Id, IncreaseDecisionHeight);
            Events.Add(IncreaseHeloGovBeep.Id, IncreaseHeloGovBeep);
            Events.Add(IncreaseThrottle.Id, IncreaseThrottle);
            Events.Add(InductorCompassRefDec.Id, InductorCompassRefDec);
            Events.Add(InductorCompassRefInc.Id, InductorCompassRefInc);
            Events.Add(IntercomModeSet.Id, IntercomModeSet);
            Events.Add(InvokeHelp.Id, InvokeHelp);
            Events.Add(IsolateTurbineOff.Id, IsolateTurbineOff);
            Events.Add(IsolateTurbineOn.Id, IsolateTurbineOn);
            Events.Add(IsolateTurbineSet.Id, IsolateTurbineSet);
            Events.Add(IsolateTurbineToggle.Id, IsolateTurbineToggle);
            Events.Add(JetStarter.Id, JetStarter);
            Events.Add(JoystickCalibrate.Id, JoystickCalibrate);
            Events.Add(KeyboardOverlay.Id, KeyboardOverlay);
            Events.Add(KneeboardView.Id, KneeboardView);
            Events.Add(KohlsmanDec.Id, KohlsmanDec);
            Events.Add(KohlsmanInc.Id, KohlsmanInc);
            Events.Add(KohlsmanSet.Id, KohlsmanSet);
            Events.Add(LabelColorCycle.Id, LabelColorCycle);
            Events.Add(LandingLightDown.Id, LandingLightDown);
            Events.Add(LandingLightHome.Id, LandingLightHome);
            Events.Add(LandingLightLeft.Id, LandingLightLeft);
            Events.Add(LandingLightRight.Id, LandingLightRight);
            Events.Add(LandingLightUp.Id, LandingLightUp);
            Events.Add(LandingLightsOff.Id, LandingLightsOff);
            Events.Add(LandingLightsOn.Id, LandingLightsOn);
            Events.Add(LandingLightsSet.Id, LandingLightsSet);
            Events.Add(LandingLightsToggle.Id, LandingLightsToggle);
            Events.Add(Letterbox.Id, Letterbox);
            Events.Add(LightPotentiometer1Set.Id, LightPotentiometer1Set);
            Events.Add(LightPotentiometer10Set.Id, LightPotentiometer10Set);
            Events.Add(LightPotentiometer11Set.Id, LightPotentiometer11Set);
            Events.Add(LightPotentiometer12Set.Id, LightPotentiometer12Set);
            Events.Add(LightPotentiometer13Set.Id, LightPotentiometer13Set);
            Events.Add(LightPotentiometer14Set.Id, LightPotentiometer14Set);
            Events.Add(LightPotentiometer15Set.Id, LightPotentiometer15Set);
            Events.Add(LightPotentiometer16Set.Id, LightPotentiometer16Set);
            Events.Add(LightPotentiometer17Set.Id, LightPotentiometer17Set);
            Events.Add(LightPotentiometer18Set.Id, LightPotentiometer18Set);
            Events.Add(LightPotentiometer19Set.Id, LightPotentiometer19Set);
            Events.Add(LightPotentiometer2Set.Id, LightPotentiometer2Set);
            Events.Add(LightPotentiometer20Set.Id, LightPotentiometer20Set);
            Events.Add(LightPotentiometer21Set.Id, LightPotentiometer21Set);
            Events.Add(LightPotentiometer22Set.Id, LightPotentiometer22Set);
            Events.Add(LightPotentiometer23Set.Id, LightPotentiometer23Set);
            Events.Add(LightPotentiometer24Set.Id, LightPotentiometer24Set);
            Events.Add(LightPotentiometer25Set.Id, LightPotentiometer25Set);
            Events.Add(LightPotentiometer26Set.Id, LightPotentiometer26Set);
            Events.Add(LightPotentiometer27Set.Id, LightPotentiometer27Set);
            Events.Add(LightPotentiometer28Set.Id, LightPotentiometer28Set);
            Events.Add(LightPotentiometer29Set.Id, LightPotentiometer29Set);
            Events.Add(LightPotentiometer3Set.Id, LightPotentiometer3Set);
            Events.Add(LightPotentiometer30Set.Id, LightPotentiometer30Set);
            Events.Add(LightPotentiometer4Set.Id, LightPotentiometer4Set);
            Events.Add(LightPotentiometer5Set.Id, LightPotentiometer5Set);
            Events.Add(LightPotentiometer6Set.Id, LightPotentiometer6Set);
            Events.Add(LightPotentiometer7Set.Id, LightPotentiometer7Set);
            Events.Add(LightPotentiometer8Set.Id, LightPotentiometer8Set);
            Events.Add(LightPotentiometer9Set.Id, LightPotentiometer9Set);
            Events.Add(LightPotentiometerDec.Id, LightPotentiometerDec);
            Events.Add(LightPotentiometerInc.Id, LightPotentiometerInc);
            Events.Add(LightPotentiometerSet.Id, LightPotentiometerSet);
            Events.Add(LodZoomIn.Id, LodZoomIn);
            Events.Add(LodZoomOut.Id, LodZoomOut);
            Events.Add(LogoLightsSet.Id, LogoLightsSet);
            Events.Add(LowHeightWarningGaugeWillSet.Id, LowHeightWarningGaugeWillSet);
            Events.Add(LowHeightWarningSet.Id, LowHeightWarningSet);
            Events.Add(LowHightWarningGaugeWillSet.Id, LowHightWarningGaugeWillSet);
            Events.Add(LowHightWarningSet.Id, LowHightWarningSet);
            Events.Add(MacCreadySettingDec.Id, MacCreadySettingDec);
            Events.Add(MacCreadySettingInc.Id, MacCreadySettingInc);
            Events.Add(MacCreadySettingSet.Id, MacCreadySettingSet);
            Events.Add(MacroBegin.Id, MacroBegin);
            Events.Add(MacroEnd.Id, MacroEnd);
            Events.Add(Magneto.Id, Magneto);
            Events.Add(MagnetoBoth.Id, MagnetoBoth);
            Events.Add(MagnetoDecr.Id, MagnetoDecr);
            Events.Add(MagnetoIncr.Id, MagnetoIncr);
            Events.Add(MagnetoLeft.Id, MagnetoLeft);
            Events.Add(MagnetoOff.Id, MagnetoOff);
            Events.Add(MagnetoRight.Id, MagnetoRight);
            Events.Add(MagnetoSet.Id, MagnetoSet);
            Events.Add(MagnetoStart.Id, MagnetoStart);
            Events.Add(Magneto1Both.Id, Magneto1Both);
            Events.Add(Magneto1Decr.Id, Magneto1Decr);
            Events.Add(Magneto1Incr.Id, Magneto1Incr);
            Events.Add(Magneto1Left.Id, Magneto1Left);
            Events.Add(Magneto1Off.Id, Magneto1Off);
            Events.Add(Magneto1Right.Id, Magneto1Right);
            Events.Add(Magneto1Set.Id, Magneto1Set);
            Events.Add(Magneto1Start.Id, Magneto1Start);
            Events.Add(Magneto2Both.Id, Magneto2Both);
            Events.Add(Magneto2Decr.Id, Magneto2Decr);
            Events.Add(Magneto2Incr.Id, Magneto2Incr);
            Events.Add(Magneto2Left.Id, Magneto2Left);
            Events.Add(Magneto2Off.Id, Magneto2Off);
            Events.Add(Magneto2Right.Id, Magneto2Right);
            Events.Add(Magneto2Set.Id, Magneto2Set);
            Events.Add(Magneto2Start.Id, Magneto2Start);
            Events.Add(Magneto3Both.Id, Magneto3Both);
            Events.Add(Magneto3Decr.Id, Magneto3Decr);
            Events.Add(Magneto3Incr.Id, Magneto3Incr);
            Events.Add(Magneto3Left.Id, Magneto3Left);
            Events.Add(Magneto3Off.Id, Magneto3Off);
            Events.Add(Magneto3Right.Id, Magneto3Right);
            Events.Add(Magneto3Set.Id, Magneto3Set);
            Events.Add(Magneto3Start.Id, Magneto3Start);
            Events.Add(Magneto4Both.Id, Magneto4Both);
            Events.Add(Magneto4Decr.Id, Magneto4Decr);
            Events.Add(Magneto4Incr.Id, Magneto4Incr);
            Events.Add(Magneto4Left.Id, Magneto4Left);
            Events.Add(Magneto4Off.Id, Magneto4Off);
            Events.Add(Magneto4Right.Id, Magneto4Right);
            Events.Add(Magneto4Set.Id, Magneto4Set);
            Events.Add(Magneto4Start.Id, Magneto4Start);
            Events.Add(ManualFuelPressurePump.Id, ManualFuelPressurePump);
            Events.Add(ManualFuelPressurePumpSet.Id, ManualFuelPressurePumpSet);
            Events.Add(ManualFuelTransfer.Id, ManualFuelTransfer);
            Events.Add(MapOrientationCycle.Id, MapOrientationCycle);
            Events.Add(MapOrientationSet.Id, MapOrientationSet);
            Events.Add(MapZoomFineIn.Id, MapZoomFineIn);
            Events.Add(MapZoomFineOut.Id, MapZoomFineOut);
            Events.Add(MapZoomSet.Id, MapZoomSet);
            Events.Add(MarkerBeaconSensitivityHigh.Id, MarkerBeaconSensitivityHigh);
            Events.Add(MarkerBeaconTestMute.Id, MarkerBeaconTestMute);
            Events.Add(MarkerSoundSet.Id, MarkerSoundSet);
            Events.Add(MarkerSoundToggle.Id, MarkerSoundToggle);
            Events.Add(MasterBatteryOff.Id, MasterBatteryOff);
            Events.Add(MasterBatteryOn.Id, MasterBatteryOn);
            Events.Add(MasterBatterySet.Id, MasterBatterySet);
            Events.Add(MasterCautionAcknowledge.Id, MasterCautionAcknowledge);
            Events.Add(MasterCautionOff.Id, MasterCautionOff);
            Events.Add(MasterCautionOn.Id, MasterCautionOn);
            Events.Add(MasterCautionSet.Id, MasterCautionSet);
            Events.Add(MasterCautionToggle.Id, MasterCautionToggle);
            Events.Add(MasterWarningAcknowledge.Id, MasterWarningAcknowledge);
            Events.Add(MasterWarningOff.Id, MasterWarningOff);
            Events.Add(MasterWarningOn.Id, MasterWarningOn);
            Events.Add(MasterWarningSet.Id, MasterWarningSet);
            Events.Add(MasterWarningToggle.Id, MasterWarningToggle);
            Events.Add(Minus.Id, Minus);
            Events.Add(MinusShift.Id, MinusShift);
            Events.Add(MixtureDecr.Id, MixtureDecr);
            Events.Add(MixtureDecrSmall.Id, MixtureDecrSmall);
            Events.Add(MixtureIncr.Id, MixtureIncr);
            Events.Add(MixtureIncrSmall.Id, MixtureIncrSmall);
            Events.Add(MixtureLean.Id, MixtureLean);
            Events.Add(MixtureRich.Id, MixtureRich);
            Events.Add(MixtureSet.Id, MixtureSet);
            Events.Add(MixtureSetBest.Id, MixtureSetBest);
            Events.Add(Mixture1Decr.Id, Mixture1Decr);
            Events.Add(Mixture1DecrSmall.Id, Mixture1DecrSmall);
            Events.Add(Mixture1Incr.Id, Mixture1Incr);
            Events.Add(Mixture1IncrSmall.Id, Mixture1IncrSmall);
            Events.Add(Mixture1Lean.Id, Mixture1Lean);
            Events.Add(Mixture1Rich.Id, Mixture1Rich);
            Events.Add(Mixture1Set.Id, Mixture1Set);
            Events.Add(Mixture2Decr.Id, Mixture2Decr);
            Events.Add(Mixture2DecrSmall.Id, Mixture2DecrSmall);
            Events.Add(Mixture2Incr.Id, Mixture2Incr);
            Events.Add(Mixture2IncrSmall.Id, Mixture2IncrSmall);
            Events.Add(Mixture2Lean.Id, Mixture2Lean);
            Events.Add(Mixture2Rich.Id, Mixture2Rich);
            Events.Add(Mixture2Set.Id, Mixture2Set);
            Events.Add(Mixture3Decr.Id, Mixture3Decr);
            Events.Add(Mixture3DecrSmall.Id, Mixture3DecrSmall);
            Events.Add(Mixture3Incr.Id, Mixture3Incr);
            Events.Add(Mixture3IncrSmall.Id, Mixture3IncrSmall);
            Events.Add(Mixture3Lean.Id, Mixture3Lean);
            Events.Add(Mixture3Rich.Id, Mixture3Rich);
            Events.Add(Mixture3Set.Id, Mixture3Set);
            Events.Add(Mixture4Decr.Id, Mixture4Decr);
            Events.Add(Mixture4DecrSmall.Id, Mixture4DecrSmall);
            Events.Add(Mixture4Incr.Id, Mixture4Incr);
            Events.Add(Mixture4IncrSmall.Id, Mixture4IncrSmall);
            Events.Add(Mixture4Lean.Id, Mixture4Lean);
            Events.Add(Mixture4Rich.Id, Mixture4Rich);
            Events.Add(Mixture4Set.Id, Mixture4Set);
            Events.Add(MouseAsYokeResume.Id, MouseAsYokeResume);
            Events.Add(MouseAsYokeSuspend.Id, MouseAsYokeSuspend);
            Events.Add(MouseAsYokeToggle.Id, MouseAsYokeToggle);
            Events.Add(MouseLookToggle.Id, MouseLookToggle);
            Events.Add(MpActivateChat.Id, MpActivateChat);
            Events.Add(MpBroadcastVoiceCaptureStart.Id, MpBroadcastVoiceCaptureStart);
            Events.Add(MpBroadcastVoiceCaptureStop.Id, MpBroadcastVoiceCaptureStop);
            Events.Add(MpChat.Id, MpChat);
            Events.Add(MpPauseSession.Id, MpPauseSession);
            Events.Add(MpPlayerCycle.Id, MpPlayerCycle);
            Events.Add(MpPlayerFollow.Id, MpPlayerFollow);
            Events.Add(MpTransferControl.Id, MpTransferControl);
            Events.Add(MpVoiceCaptureStart.Id, MpVoiceCaptureStart);
            Events.Add(MpVoiceCaptureStop.Id, MpVoiceCaptureStop);
            Events.Add(NavLightsOff.Id, NavLightsOff);
            Events.Add(NavLightsOn.Id, NavLightsOn);
            Events.Add(NavLightsSet.Id, NavLightsSet);
            Events.Add(NavRadio.Id, NavRadio);
            Events.Add(Nav1CloseFreqSet.Id, Nav1CloseFreqSet);
            Events.Add(Nav1RadioFractDec.Id, Nav1RadioFractDec);
            Events.Add(Nav1RadioFractDecCarry.Id, Nav1RadioFractDecCarry);
            Events.Add(Nav1RadioFractInc.Id, Nav1RadioFractInc);
            Events.Add(Nav1RadioFractIncCarry.Id, Nav1RadioFractIncCarry);
            Events.Add(Nav1RadioSet.Id, Nav1RadioSet);
            Events.Add(Nav1RadioSetHz.Id, Nav1RadioSetHz);
            Events.Add(Nav1RadioSwap.Id, Nav1RadioSwap);
            Events.Add(Nav1RadioWholeDec.Id, Nav1RadioWholeDec);
            Events.Add(Nav1RadioWholeInc.Id, Nav1RadioWholeInc);
            Events.Add(Nav1StbySet.Id, Nav1StbySet);
            Events.Add(Nav1StbySetHz.Id, Nav1StbySetHz);
            Events.Add(Nav1VolumeDec.Id, Nav1VolumeDec);
            Events.Add(Nav1VolumeInc.Id, Nav1VolumeInc);
            Events.Add(Nav1VolumeSet.Id, Nav1VolumeSet);
            Events.Add(Nav1VolumeSetEx1.Id, Nav1VolumeSetEx1);
            Events.Add(Nav2CloseFreqSet.Id, Nav2CloseFreqSet);
            Events.Add(Nav2RadioFractDec.Id, Nav2RadioFractDec);
            Events.Add(Nav2RadioFractDecCarry.Id, Nav2RadioFractDecCarry);
            Events.Add(Nav2RadioFractInc.Id, Nav2RadioFractInc);
            Events.Add(Nav2RadioFractIncCarry.Id, Nav2RadioFractIncCarry);
            Events.Add(Nav2RadioSet.Id, Nav2RadioSet);
            Events.Add(Nav2RadioSetHz.Id, Nav2RadioSetHz);
            Events.Add(Nav2RadioSwap.Id, Nav2RadioSwap);
            Events.Add(Nav2RadioWholeDec.Id, Nav2RadioWholeDec);
            Events.Add(Nav2RadioWholeInc.Id, Nav2RadioWholeInc);
            Events.Add(Nav2StbySet.Id, Nav2StbySet);
            Events.Add(Nav2StbySetHz.Id, Nav2StbySetHz);
            Events.Add(Nav2VolumeDec.Id, Nav2VolumeDec);
            Events.Add(Nav2VolumeInc.Id, Nav2VolumeInc);
            Events.Add(Nav2VolumeSet.Id, Nav2VolumeSet);
            Events.Add(Nav2VolumeSetEx1.Id, Nav2VolumeSetEx1);
            Events.Add(Nav3CloseFreqSet.Id, Nav3CloseFreqSet);
            Events.Add(Nav3RadioFractDec.Id, Nav3RadioFractDec);
            Events.Add(Nav3RadioFractDecCarry.Id, Nav3RadioFractDecCarry);
            Events.Add(Nav3RadioFractInc.Id, Nav3RadioFractInc);
            Events.Add(Nav3RadioFractIncCarry.Id, Nav3RadioFractIncCarry);
            Events.Add(Nav3RadioSet.Id, Nav3RadioSet);
            Events.Add(Nav3RadioSetHz.Id, Nav3RadioSetHz);
            Events.Add(Nav3RadioSwap.Id, Nav3RadioSwap);
            Events.Add(Nav3RadioWholeDec.Id, Nav3RadioWholeDec);
            Events.Add(Nav3RadioWholeInc.Id, Nav3RadioWholeInc);
            Events.Add(Nav3StbySet.Id, Nav3StbySet);
            Events.Add(Nav3StbySetHz.Id, Nav3StbySetHz);
            Events.Add(Nav3VolumeDec.Id, Nav3VolumeDec);
            Events.Add(Nav3VolumeInc.Id, Nav3VolumeInc);
            Events.Add(Nav3VolumeSet.Id, Nav3VolumeSet);
            Events.Add(Nav3VolumeSetEx1.Id, Nav3VolumeSetEx1);
            Events.Add(Nav4CloseFreqSet.Id, Nav4CloseFreqSet);
            Events.Add(Nav4RadioFractDec.Id, Nav4RadioFractDec);
            Events.Add(Nav4RadioFractDecCarry.Id, Nav4RadioFractDecCarry);
            Events.Add(Nav4RadioFractInc.Id, Nav4RadioFractInc);
            Events.Add(Nav4RadioFractIncCarry.Id, Nav4RadioFractIncCarry);
            Events.Add(Nav4RadioSet.Id, Nav4RadioSet);
            Events.Add(Nav4RadioSetHz.Id, Nav4RadioSetHz);
            Events.Add(Nav4RadioSwap.Id, Nav4RadioSwap);
            Events.Add(Nav4RadioWholeDec.Id, Nav4RadioWholeDec);
            Events.Add(Nav4RadioWholeInc.Id, Nav4RadioWholeInc);
            Events.Add(Nav4StbySet.Id, Nav4StbySet);
            Events.Add(Nav4StbySetHz.Id, Nav4StbySetHz);
            Events.Add(Nav4VolumeDec.Id, Nav4VolumeDec);
            Events.Add(Nav4VolumeInc.Id, Nav4VolumeInc);
            Events.Add(Nav4VolumeSet.Id, Nav4VolumeSet);
            Events.Add(Nav4VolumeSetEx1.Id, Nav4VolumeSetEx1);
            Events.Add(NewMap.Id, NewMap);
            Events.Add(NewView.Id, NewView);
            Events.Add(NextSubView.Id, NextSubView);
            Events.Add(NextView.Id, NextView);
            Events.Add(NitrousTankValveToggle.Id, NitrousTankValveToggle);
            Events.Add(NoseWheelSteeringLimitSet.Id, NoseWheelSteeringLimitSet);
            Events.Add(OilCoolingFlapsDown.Id, OilCoolingFlapsDown);
            Events.Add(OilCoolingFlapsSet.Id, OilCoolingFlapsSet);
            Events.Add(OilCoolingFlapsToggle.Id, OilCoolingFlapsToggle);
            Events.Add(OilCoolingFlapsUp.Id, OilCoolingFlapsUp);
            Events.Add(OtherAircraftView.Id, OtherAircraftView);
            Events.Add(Overlaymenu.Id, Overlaymenu);
            Events.Add(PanDown.Id, PanDown);
            Events.Add(PanLeft.Id, PanLeft);
            Events.Add(PanLeftDown.Id, PanLeftDown);
            Events.Add(PanLeftUp.Id, PanLeftUp);
            Events.Add(PanReset.Id, PanReset);
            Events.Add(PanResetCockpit.Id, PanResetCockpit);
            Events.Add(PanRight.Id, PanRight);
            Events.Add(PanRightDown.Id, PanRightDown);
            Events.Add(PanRightUp.Id, PanRightUp);
            Events.Add(PanTiltLeft.Id, PanTiltLeft);
            Events.Add(PanTiltRight.Id, PanTiltRight);
            Events.Add(PanUp.Id, PanUp);
            Events.Add(PanView.Id, PanView);
            Events.Add(Panel1.Id, Panel1);
            Events.Add(Panel2.Id, Panel2);
            Events.Add(Panel3.Id, Panel3);
            Events.Add(Panel4.Id, Panel4);
            Events.Add(Panel5.Id, Panel5);
            Events.Add(Panel6.Id, Panel6);
            Events.Add(Panel7.Id, Panel7);
            Events.Add(Panel8.Id, Panel8);
            Events.Add(Panel9.Id, Panel9);
            Events.Add(PanelHudNext.Id, PanelHudNext);
            Events.Add(PanelHudPrevious.Id, PanelHudPrevious);
            Events.Add(PanelIdClose.Id, PanelIdClose);
            Events.Add(PanelIdOpen.Id, PanelIdOpen);
            Events.Add(PanelIdToggle.Id, PanelIdToggle);
            Events.Add(PanelLightsOff.Id, PanelLightsOff);
            Events.Add(PanelLightsOn.Id, PanelLightsOn);
            Events.Add(PanelLightsPowerSettingSet.Id, PanelLightsPowerSettingSet);
            Events.Add(PanelLightsSet.Id, PanelLightsSet);
            Events.Add(PanelLightsToggle.Id, PanelLightsToggle);
            Events.Add(PanelSelect1.Id, PanelSelect1);
            Events.Add(PanelSelect2.Id, PanelSelect2);
            Events.Add(PanelToggle.Id, PanelToggle);
            Events.Add(ParkingBrakeSet.Id, ParkingBrakeSet);
            Events.Add(ParkingBrakes.Id, ParkingBrakes);
            Events.Add(PauseOff.Id, PauseOff);
            Events.Add(PauseOn.Id, PauseOn);
            Events.Add(PauseSet.Id, PauseSet);
            Events.Add(PauseToggle.Id, PauseToggle);
            Events.Add(PedestralLightsOff.Id, PedestralLightsOff);
            Events.Add(PedestralLightsOn.Id, PedestralLightsOn);
            Events.Add(PedestralLightsPowerSettingSet.Id, PedestralLightsPowerSettingSet);
            Events.Add(PedestralLightsSet.Id, PedestralLightsSet);
            Events.Add(PedestralLightsToggle.Id, PedestralLightsToggle);
            Events.Add(PilotTransmitterSet.Id, PilotTransmitterSet);
            Events.Add(PitotHeatOff.Id, PitotHeatOff);
            Events.Add(PitotHeatOn.Id, PitotHeatOn);
            Events.Add(PitotHeatSet.Id, PitotHeatSet);
            Events.Add(PitotHeatToggle.Id, PitotHeatToggle);
            Events.Add(Plus.Id, Plus);
            Events.Add(PlusShift.Id, PlusShift);
            Events.Add(PointOfInterestCycleNext.Id, PointOfInterestCycleNext);
            Events.Add(PointOfInterestCyclePrevious.Id, PointOfInterestCyclePrevious);
            Events.Add(PointOfInterestTogglePointer.Id, PointOfInterestTogglePointer);
            Events.Add(PressurizationClimbRateDec.Id, PressurizationClimbRateDec);
            Events.Add(PressurizationClimbRateInc.Id, PressurizationClimbRateInc);
            Events.Add(PressurizationClimbRateSet.Id, PressurizationClimbRateSet);
            Events.Add(PressurizationPressureAltDec.Id, PressurizationPressureAltDec);
            Events.Add(PressurizationPressureAltInc.Id, PressurizationPressureAltInc);
            Events.Add(PressurizationPressureDumpSwitch.Id, PressurizationPressureDumpSwitch);
            Events.Add(PrevSubView.Id, PrevSubView);
            Events.Add(PrevView.Id, PrevView);
            Events.Add(PropForceBetaOff.Id, PropForceBetaOff);
            Events.Add(PropForceBetaOn.Id, PropForceBetaOn);
            Events.Add(PropForceBetaSet.Id, PropForceBetaSet);
            Events.Add(PropForceBetaToggle.Id, PropForceBetaToggle);
            Events.Add(PropForceBetaValueSet.Id, PropForceBetaValueSet);
            Events.Add(PropLockOff.Id, PropLockOff);
            Events.Add(PropLockOn.Id, PropLockOn);
            Events.Add(PropLockSet.Id, PropLockSet);
            Events.Add(PropLockToggle.Id, PropLockToggle);
            Events.Add(PropPitchAxisSetEx1.Id, PropPitchAxisSetEx1);
            Events.Add(PropPitchDecr.Id, PropPitchDecr);
            Events.Add(PropPitchDecrSmall.Id, PropPitchDecrSmall);
            Events.Add(PropPitchDecreaseEx1.Id, PropPitchDecreaseEx1);
            Events.Add(PropPitchDecreaseSmallEx1.Id, PropPitchDecreaseSmallEx1);
            Events.Add(PropPitchHi.Id, PropPitchHi);
            Events.Add(PropPitchHiEx1.Id, PropPitchHiEx1);
            Events.Add(PropPitchIncr.Id, PropPitchIncr);
            Events.Add(PropPitchIncrSmall.Id, PropPitchIncrSmall);
            Events.Add(PropPitchIncreaseEx1.Id, PropPitchIncreaseEx1);
            Events.Add(PropPitchIncreaseSmallEx1.Id, PropPitchIncreaseSmallEx1);
            Events.Add(PropPitchLo.Id, PropPitchLo);
            Events.Add(PropPitchLoEx1.Id, PropPitchLoEx1);
            Events.Add(PropPitchSet.Id, PropPitchSet);
            Events.Add(PropPitch1AxisSetEx1.Id, PropPitch1AxisSetEx1);
            Events.Add(PropPitch1Decr.Id, PropPitch1Decr);
            Events.Add(PropPitch1DecrSmall.Id, PropPitch1DecrSmall);
            Events.Add(PropPitch1DecreaseEx1.Id, PropPitch1DecreaseEx1);
            Events.Add(PropPitch1DecreaseSmallEx1.Id, PropPitch1DecreaseSmallEx1);
            Events.Add(PropPitch1Hi.Id, PropPitch1Hi);
            Events.Add(PropPitch1HiEx1.Id, PropPitch1HiEx1);
            Events.Add(PropPitch1Incr.Id, PropPitch1Incr);
            Events.Add(PropPitch1IncrSmall.Id, PropPitch1IncrSmall);
            Events.Add(PropPitch1IncreaseEx1.Id, PropPitch1IncreaseEx1);
            Events.Add(PropPitch1IncreaseSmallEx1.Id, PropPitch1IncreaseSmallEx1);
            Events.Add(PropPitch1Lo.Id, PropPitch1Lo);
            Events.Add(PropPitch1LoEx1.Id, PropPitch1LoEx1);
            Events.Add(PropPitch1Set.Id, PropPitch1Set);
            Events.Add(PropPitch2AxisSetEx1.Id, PropPitch2AxisSetEx1);
            Events.Add(PropPitch2Decr.Id, PropPitch2Decr);
            Events.Add(PropPitch2DecrSmall.Id, PropPitch2DecrSmall);
            Events.Add(PropPitch2DecreaseEx1.Id, PropPitch2DecreaseEx1);
            Events.Add(PropPitch2DecreaseSmallEx1.Id, PropPitch2DecreaseSmallEx1);
            Events.Add(PropPitch2Hi.Id, PropPitch2Hi);
            Events.Add(PropPitch2HiEx1.Id, PropPitch2HiEx1);
            Events.Add(PropPitch2Incr.Id, PropPitch2Incr);
            Events.Add(PropPitch2IncrSmall.Id, PropPitch2IncrSmall);
            Events.Add(PropPitch2IncreaseEx1.Id, PropPitch2IncreaseEx1);
            Events.Add(PropPitch2IncreaseSmallEx1.Id, PropPitch2IncreaseSmallEx1);
            Events.Add(PropPitch2Lo.Id, PropPitch2Lo);
            Events.Add(PropPitch2LoEx1.Id, PropPitch2LoEx1);
            Events.Add(PropPitch2Set.Id, PropPitch2Set);
            Events.Add(PropPitch3AxisSetEx1.Id, PropPitch3AxisSetEx1);
            Events.Add(PropPitch3Decr.Id, PropPitch3Decr);
            Events.Add(PropPitch3DecrSmall.Id, PropPitch3DecrSmall);
            Events.Add(PropPitch3DecreaseEx1.Id, PropPitch3DecreaseEx1);
            Events.Add(PropPitch3DecreaseSmallEx1.Id, PropPitch3DecreaseSmallEx1);
            Events.Add(PropPitch3Hi.Id, PropPitch3Hi);
            Events.Add(PropPitch3HiEx1.Id, PropPitch3HiEx1);
            Events.Add(PropPitch3Incr.Id, PropPitch3Incr);
            Events.Add(PropPitch3IncrSmall.Id, PropPitch3IncrSmall);
            Events.Add(PropPitch3IncreaseEx1.Id, PropPitch3IncreaseEx1);
            Events.Add(PropPitch3IncreaseSmallEx1.Id, PropPitch3IncreaseSmallEx1);
            Events.Add(PropPitch3Lo.Id, PropPitch3Lo);
            Events.Add(PropPitch3LoEx1.Id, PropPitch3LoEx1);
            Events.Add(PropPitch3Set.Id, PropPitch3Set);
            Events.Add(PropPitch4AxisSetEx1.Id, PropPitch4AxisSetEx1);
            Events.Add(PropPitch4Decr.Id, PropPitch4Decr);
            Events.Add(PropPitch4DecrSmall.Id, PropPitch4DecrSmall);
            Events.Add(PropPitch4DecreaseEx1.Id, PropPitch4DecreaseEx1);
            Events.Add(PropPitch4DecreaseSmallEx1.Id, PropPitch4DecreaseSmallEx1);
            Events.Add(PropPitch4Hi.Id, PropPitch4Hi);
            Events.Add(PropPitch4HiEx1.Id, PropPitch4HiEx1);
            Events.Add(PropPitch4Incr.Id, PropPitch4Incr);
            Events.Add(PropPitch4IncrSmall.Id, PropPitch4IncrSmall);
            Events.Add(PropPitch4IncreaseEx1.Id, PropPitch4IncreaseEx1);
            Events.Add(PropPitch4IncreaseSmallEx1.Id, PropPitch4IncreaseSmallEx1);
            Events.Add(PropPitch4Lo.Id, PropPitch4Lo);
            Events.Add(PropPitch4LoEx1.Id, PropPitch4LoEx1);
            Events.Add(PropPitch4Set.Id, PropPitch4Set);
            Events.Add(PropellerReverseThrustHold.Id, PropellerReverseThrustHold);
            Events.Add(PropellerReverseThrustToggle.Id, PropellerReverseThrustToggle);
            Events.Add(RadiatorCoolingFlapsDown.Id, RadiatorCoolingFlapsDown);
            Events.Add(RadiatorCoolingFlapsSet.Id, RadiatorCoolingFlapsSet);
            Events.Add(RadiatorCoolingFlapsToggle.Id, RadiatorCoolingFlapsToggle);
            Events.Add(RadiatorCoolingFlapsUp.Id, RadiatorCoolingFlapsUp);
            Events.Add(RadioAdfIdentDisable.Id, RadioAdfIdentDisable);
            Events.Add(RadioAdfIdentEnable.Id, RadioAdfIdentEnable);
            Events.Add(RadioAdfIdentSet.Id, RadioAdfIdentSet);
            Events.Add(RadioAdfIdentToggle.Id, RadioAdfIdentToggle);
            Events.Add(RadioAdf2IdentDisable.Id, RadioAdf2IdentDisable);
            Events.Add(RadioAdf2IdentEnable.Id, RadioAdf2IdentEnable);
            Events.Add(RadioAdf2IdentSet.Id, RadioAdf2IdentSet);
            Events.Add(RadioAdf2IdentToggle.Id, RadioAdf2IdentToggle);
            Events.Add(RadioComm1AutoswitchToggle.Id, RadioComm1AutoswitchToggle);
            Events.Add(RadioComm2AutoswitchToggle.Id, RadioComm2AutoswitchToggle);
            Events.Add(RadioCommnav1TestToggle.Id, RadioCommnav1TestToggle);
            Events.Add(RadioCommnav2TestToggle.Id, RadioCommnav2TestToggle);
            Events.Add(RadioCommnav3TestToggle.Id, RadioCommnav3TestToggle);
            Events.Add(RadioDme1IdentDisable.Id, RadioDme1IdentDisable);
            Events.Add(RadioDme1IdentEnable.Id, RadioDme1IdentEnable);
            Events.Add(RadioDme1IdentSet.Id, RadioDme1IdentSet);
            Events.Add(RadioDme1IdentToggle.Id, RadioDme1IdentToggle);
            Events.Add(RadioDme2IdentDisable.Id, RadioDme2IdentDisable);
            Events.Add(RadioDme2IdentEnable.Id, RadioDme2IdentEnable);
            Events.Add(RadioDme2IdentSet.Id, RadioDme2IdentSet);
            Events.Add(RadioDme2IdentToggle.Id, RadioDme2IdentToggle);
            Events.Add(RadioNav1AutoswitchToggle.Id, RadioNav1AutoswitchToggle);
            Events.Add(RadioNav2AutoswitchToggle.Id, RadioNav2AutoswitchToggle);
            Events.Add(RadioSelectedDmeIdentDisable.Id, RadioSelectedDmeIdentDisable);
            Events.Add(RadioSelectedDmeIdentEnable.Id, RadioSelectedDmeIdentEnable);
            Events.Add(RadioSelectedDmeIdentSet.Id, RadioSelectedDmeIdentSet);
            Events.Add(RadioSelectedDmeIdentToggle.Id, RadioSelectedDmeIdentToggle);
            Events.Add(RadioVor1IdentDisable.Id, RadioVor1IdentDisable);
            Events.Add(RadioVor1IdentEnable.Id, RadioVor1IdentEnable);
            Events.Add(RadioVor1IdentSet.Id, RadioVor1IdentSet);
            Events.Add(RadioVor1IdentToggle.Id, RadioVor1IdentToggle);
            Events.Add(RadioVor2IdentDisable.Id, RadioVor2IdentDisable);
            Events.Add(RadioVor2IdentEnable.Id, RadioVor2IdentEnable);
            Events.Add(RadioVor2IdentSet.Id, RadioVor2IdentSet);
            Events.Add(RadioVor2IdentToggle.Id, RadioVor2IdentToggle);
            Events.Add(RadioVor3IdentDisable.Id, RadioVor3IdentDisable);
            Events.Add(RadioVor3IdentEnable.Id, RadioVor3IdentEnable);
            Events.Add(RadioVor3IdentSet.Id, RadioVor3IdentSet);
            Events.Add(RadioVor3IdentToggle.Id, RadioVor3IdentToggle);
            Events.Add(RadioVor4IdentDisable.Id, RadioVor4IdentDisable);
            Events.Add(RadioVor4IdentEnable.Id, RadioVor4IdentEnable);
            Events.Add(RadioVor4IdentSet.Id, RadioVor4IdentSet);
            Events.Add(RadioVor4IdentToggle.Id, RadioVor4IdentToggle);
            Events.Add(ReadoutsFlight.Id, ReadoutsFlight);
            Events.Add(ReadoutsSlew.Id, ReadoutsSlew);
            Events.Add(RecognitionLightsSet.Id, RecognitionLightsSet);
            Events.Add(RefreshScenery.Id, RefreshScenery);
            Events.Add(ReleaseDropTank1.Id, ReleaseDropTank1);
            Events.Add(ReleaseDropTank2.Id, ReleaseDropTank2);
            Events.Add(ReleaseDropTankAll.Id, ReleaseDropTankAll);
            Events.Add(ReleaseDroppableObjects.Id, ReleaseDroppableObjects);
            Events.Add(ReloadPanels.Id, ReloadPanels);
            Events.Add(ReloadUserAircraft.Id, ReloadUserAircraft);
            Events.Add(RepairAndRefuel.Id, RepairAndRefuel);
            Events.Add(ReplayStop.Id, ReplayStop);
            Events.Add(RequestCatering.Id, RequestCatering);
            Events.Add(RequestFuelKey.Id, RequestFuelKey);
            Events.Add(RequestLuggage.Id, RequestLuggage);
            Events.Add(RequestPowerSupply.Id, RequestPowerSupply);
            Events.Add(ResetGForceIndicator.Id, ResetGForceIndicator);
            Events.Add(ResetMaxRpmIndicator.Id, ResetMaxRpmIndicator);
            Events.Add(RetractFloatSwitchDec.Id, RetractFloatSwitchDec);
            Events.Add(RetractFloatSwitchInc.Id, RetractFloatSwitchInc);
            Events.Add(RotorAxisTailRotorSet.Id, RotorAxisTailRotorSet);
            Events.Add(RotorBrake.Id, RotorBrake);
            Events.Add(RotorBrakeOff.Id, RotorBrakeOff);
            Events.Add(RotorBrakeOn.Id, RotorBrakeOn);
            Events.Add(RotorBrakeSet.Id, RotorBrakeSet);
            Events.Add(RotorBrakeToggle.Id, RotorBrakeToggle);
            Events.Add(RotorClutchSwitchSet.Id, RotorClutchSwitchSet);
            Events.Add(RotorClutchSwitchToggle.Id, RotorClutchSwitchToggle);
            Events.Add(RotorGovSwitchOff.Id, RotorGovSwitchOff);
            Events.Add(RotorGovSwitchOn.Id, RotorGovSwitchOn);
            Events.Add(RotorGovSwitchSet.Id, RotorGovSwitchSet);
            Events.Add(RotorGovSwitchToggle.Id, RotorGovSwitchToggle);
            Events.Add(RotorLateralTrimDec.Id, RotorLateralTrimDec);
            Events.Add(RotorLateralTrimInc.Id, RotorLateralTrimInc);
            Events.Add(RotorLateralTrimSet.Id, RotorLateralTrimSet);
            Events.Add(RotorLongitudinalTrimDec.Id, RotorLongitudinalTrimDec);
            Events.Add(RotorLongitudinalTrimInc.Id, RotorLongitudinalTrimInc);
            Events.Add(RotorLongitudinalTrimSet.Id, RotorLongitudinalTrimSet);
            Events.Add(RotorTrimReset.Id, RotorTrimReset);
            Events.Add(RpmSlotIndexSet.Id, RpmSlotIndexSet);
            Events.Add(RudderAxisMinus.Id, RudderAxisMinus);
            Events.Add(RudderAxisPlus.Id, RudderAxisPlus);
            Events.Add(RudderCenter.Id, RudderCenter);
            Events.Add(RudderLeft.Id, RudderLeft);
            Events.Add(RudderRight.Id, RudderRight);
            Events.Add(RudderSet.Id, RudderSet);
            Events.Add(RudderTrimDisabledSet.Id, RudderTrimDisabledSet);
            Events.Add(RudderTrimDisabledToggle.Id, RudderTrimDisabledToggle);
            Events.Add(RudderTrimLeft.Id, RudderTrimLeft);
            Events.Add(RudderTrimReset.Id, RudderTrimReset);
            Events.Add(RudderTrimRight.Id, RudderTrimRight);
            Events.Add(RudderTrimSet.Id, RudderTrimSet);
            Events.Add(RudderTrimSetEx1.Id, RudderTrimSetEx1);
            Events.Add(ScriptEvent1.Id, ScriptEvent1);
            Events.Add(ScriptEvent2.Id, ScriptEvent2);
            Events.Add(SeeOwnAcOff.Id, SeeOwnAcOff);
            Events.Add(SeeOwnAcOn.Id, SeeOwnAcOn);
            Events.Add(SeeOwnAcSet.Id, SeeOwnAcSet);
            Events.Add(SeeOwnAcToggle.Id, SeeOwnAcToggle);
            Events.Add(Select1.Id, Select1);
            Events.Add(Select2.Id, Select2);
            Events.Add(Select3.Id, Select3);
            Events.Add(Select4.Id, Select4);
            Events.Add(SelectNextTarget.Id, SelectNextTarget);
            Events.Add(SelectPrevTarget.Id, SelectPrevTarget);
            Events.Add(SetAutobrakeControl.Id, SetAutobrakeControl);
            Events.Add(SetDecisionAltitudeMsl.Id, SetDecisionAltitudeMsl);
            Events.Add(SetExternalPower.Id, SetExternalPower);
            Events.Add(SetFuelTransferAft.Id, SetFuelTransferAft);
            Events.Add(SetFuelTransferAuto.Id, SetFuelTransferAuto);
            Events.Add(SetFuelTransferCustom.Id, SetFuelTransferCustom);
            Events.Add(SetFuelTransferForward.Id, SetFuelTransferForward);
            Events.Add(SetFuelTransferOff.Id, SetFuelTransferOff);
            Events.Add(SetFuelValveEng1.Id, SetFuelValveEng1);
            Events.Add(SetFuelValveEng2.Id, SetFuelValveEng2);
            Events.Add(SetFuelValveEng3.Id, SetFuelValveEng3);
            Events.Add(SetFuelValveEng4.Id, SetFuelValveEng4);
            Events.Add(SetHeloGovBeep.Id, SetHeloGovBeep);
            Events.Add(SetLaunchBarSwitch.Id, SetLaunchBarSwitch);
            Events.Add(SetReverseThrustOff.Id, SetReverseThrustOff);
            Events.Add(SetReverseThrustOn.Id, SetReverseThrustOn);
            Events.Add(SetStarterAllHeld.Id, SetStarterAllHeld);
            Events.Add(SetStarter1Held.Id, SetStarter1Held);
            Events.Add(SetStarter2Held.Id, SetStarter2Held);
            Events.Add(SetStarter3Held.Id, SetStarter3Held);
            Events.Add(SetStarter4Held.Id, SetStarter4Held);
            Events.Add(SetTailHookHandle.Id, SetTailHookHandle);
            Events.Add(SetThrottle1ReverseThrustOff.Id, SetThrottle1ReverseThrustOff);
            Events.Add(SetThrottle1ReverseThrustOn.Id, SetThrottle1ReverseThrustOn);
            Events.Add(SetThrottle2ReverseThrustOff.Id, SetThrottle2ReverseThrustOff);
            Events.Add(SetThrottle2ReverseThrustOn.Id, SetThrottle2ReverseThrustOn);
            Events.Add(SetThrottle3ReverseThrustOff.Id, SetThrottle3ReverseThrustOff);
            Events.Add(SetThrottle3ReverseThrustOn.Id, SetThrottle3ReverseThrustOn);
            Events.Add(SetThrottle4ReverseThrustOff.Id, SetThrottle4ReverseThrustOff);
            Events.Add(SetThrottle4ReverseThrustOn.Id, SetThrottle4ReverseThrustOn);
            Events.Add(SetWingFold.Id, SetWingFold);
            Events.Add(ShutoffValveOff.Id, ShutoffValveOff);
            Events.Add(ShutoffValveOn.Id, ShutoffValveOn);
            Events.Add(ShutoffValveToggle.Id, ShutoffValveToggle);
            Events.Add(SimRate.Id, SimRate);
            Events.Add(SimRateDecr.Id, SimRateDecr);
            Events.Add(SimRateIncr.Id, SimRateIncr);
            Events.Add(SimRateSet.Id, SimRateSet);
            Events.Add(SimReset.Id, SimReset);
            Events.Add(SimuiWindowHideshow.Id, SimuiWindowHideshow);
            Events.Add(SituationReset.Id, SituationReset);
            Events.Add(SituationSave.Id, SituationSave);
            Events.Add(SkipAction.Id, SkipAction);
            Events.Add(SlewAheadMinus.Id, SlewAheadMinus);
            Events.Add(SlewAheadPlus.Id, SlewAheadPlus);
            Events.Add(SlewAltitDnFast.Id, SlewAltitDnFast);
            Events.Add(SlewAltitDnSlow.Id, SlewAltitDnSlow);
            Events.Add(SlewAltitFreeze.Id, SlewAltitFreeze);
            Events.Add(SlewAltitMinus.Id, SlewAltitMinus);
            Events.Add(SlewAltitPlus.Id, SlewAltitPlus);
            Events.Add(SlewAltitUpFast.Id, SlewAltitUpFast);
            Events.Add(SlewAltitUpSlow.Id, SlewAltitUpSlow);
            Events.Add(SlewBankMinus.Id, SlewBankMinus);
            Events.Add(SlewBankPlus.Id, SlewBankPlus);
            Events.Add(SlewFreeze.Id, SlewFreeze);
            Events.Add(SlewHeadingMinus.Id, SlewHeadingMinus);
            Events.Add(SlewHeadingPlus.Id, SlewHeadingPlus);
            Events.Add(SlewLeft.Id, SlewLeft);
            Events.Add(SlewOff.Id, SlewOff);
            Events.Add(SlewOn.Id, SlewOn);
            Events.Add(SlewPitchDnFast.Id, SlewPitchDnFast);
            Events.Add(SlewPitchDnSlow.Id, SlewPitchDnSlow);
            Events.Add(SlewPitchFreeze.Id, SlewPitchFreeze);
            Events.Add(SlewPitchMinus.Id, SlewPitchMinus);
            Events.Add(SlewPitchPlus.Id, SlewPitchPlus);
            Events.Add(SlewPitchUpFast.Id, SlewPitchUpFast);
            Events.Add(SlewPitchUpSlow.Id, SlewPitchUpSlow);
            Events.Add(SlewReset.Id, SlewReset);
            Events.Add(SlewRight.Id, SlewRight);
            Events.Add(SlewSet.Id, SlewSet);
            Events.Add(SlewToggle.Id, SlewToggle);
            Events.Add(SlingPickupRelease.Id, SlingPickupRelease);
            Events.Add(SmokeOff.Id, SmokeOff);
            Events.Add(SmokeOn.Id, SmokeOn);
            Events.Add(SmokeSet.Id, SmokeSet);
            Events.Add(SmokeToggle.Id, SmokeToggle);
            Events.Add(SnapView.Id, SnapView);
            Events.Add(SoundOff.Id, SoundOff);
            Events.Add(SoundOn.Id, SoundOn);
            Events.Add(SoundSet.Id, SoundSet);
            Events.Add(SoundToggle.Id, SoundToggle);
            Events.Add(SpMultiplayerScoreDisplay.Id, SpMultiplayerScoreDisplay);
            Events.Add(SpeedSlotIndexSet.Id, SpeedSlotIndexSet);
            Events.Add(SpoilersArmOff.Id, SpoilersArmOff);
            Events.Add(SpoilersArmOn.Id, SpoilersArmOn);
            Events.Add(SpoilersArmSet.Id, SpoilersArmSet);
            Events.Add(SpoilersArmToggle.Id, SpoilersArmToggle);
            Events.Add(SpoilersDec.Id, SpoilersDec);
            Events.Add(SpoilersInc.Id, SpoilersInc);
            Events.Add(SpoilersOff.Id, SpoilersOff);
            Events.Add(SpoilersOn.Id, SpoilersOn);
            Events.Add(SpoilersSet.Id, SpoilersSet);
            Events.Add(SpoilersToggle.Id, SpoilersToggle);
            Events.Add(StarterGen.Id, StarterGen);
            Events.Add(StarterOff.Id, StarterOff);
            Events.Add(StarterSet.Id, StarterSet);
            Events.Add(StarterStart.Id, StarterStart);
            Events.Add(Starter1Set.Id, Starter1Set);
            Events.Add(Starter2Set.Id, Starter2Set);
            Events.Add(Starter3Set.Id, Starter3Set);
            Events.Add(Starter4Set.Id, Starter4Set);
            Events.Add(SteeringDec.Id, SteeringDec);
            Events.Add(SteeringInc.Id, SteeringInc);
            Events.Add(SteeringSet.Id, SteeringSet);
            Events.Add(StopAllGuns.Id, StopAllGuns);
            Events.Add(StopPrimaryGuns.Id, StopPrimaryGuns);
            Events.Add(StopSecondaryGuns.Id, StopSecondaryGuns);
            Events.Add(StrobesOff.Id, StrobesOff);
            Events.Add(StrobesOn.Id, StrobesOn);
            Events.Add(StrobesSet.Id, StrobesSet);
            Events.Add(StrobesToggle.Id, StrobesToggle);
            Events.Add(SyncFlightDirectorPitch.Id, SyncFlightDirectorPitch);
            Events.Add(Tacan1ActiveChannelSet.Id, Tacan1ActiveChannelSet);
            Events.Add(Tacan1ActiveModeSet.Id, Tacan1ActiveModeSet);
            Events.Add(Tacan1ObiDec.Id, Tacan1ObiDec);
            Events.Add(Tacan1ObiFastDec.Id, Tacan1ObiFastDec);
            Events.Add(Tacan1ObiFastInc.Id, Tacan1ObiFastInc);
            Events.Add(Tacan1ObiInc.Id, Tacan1ObiInc);
            Events.Add(Tacan1Set.Id, Tacan1Set);
            Events.Add(Tacan1StandbyChannelSet.Id, Tacan1StandbyChannelSet);
            Events.Add(Tacan1StandbyModeSet.Id, Tacan1StandbyModeSet);
            Events.Add(Tacan1Swap.Id, Tacan1Swap);
            Events.Add(Tacan1VolumeDec.Id, Tacan1VolumeDec);
            Events.Add(Tacan1VolumeInc.Id, Tacan1VolumeInc);
            Events.Add(Tacan1VolumeSet.Id, Tacan1VolumeSet);
            Events.Add(Tacan2ActiveChannelSet.Id, Tacan2ActiveChannelSet);
            Events.Add(Tacan2ActiveModeSet.Id, Tacan2ActiveModeSet);
            Events.Add(Tacan2ObiDec.Id, Tacan2ObiDec);
            Events.Add(Tacan2ObiFastDec.Id, Tacan2ObiFastDec);
            Events.Add(Tacan2ObiFastInc.Id, Tacan2ObiFastInc);
            Events.Add(Tacan2ObiInc.Id, Tacan2ObiInc);
            Events.Add(Tacan2Set.Id, Tacan2Set);
            Events.Add(Tacan2StandbyChannelSet.Id, Tacan2StandbyChannelSet);
            Events.Add(Tacan2StandbyModeSet.Id, Tacan2StandbyModeSet);
            Events.Add(Tacan2Swap.Id, Tacan2Swap);
            Events.Add(Tacan2VolumeDec.Id, Tacan2VolumeDec);
            Events.Add(Tacan2VolumeInc.Id, Tacan2VolumeInc);
            Events.Add(Tacan2VolumeSet.Id, Tacan2VolumeSet);
            Events.Add(TailRotorDecr.Id, TailRotorDecr);
            Events.Add(TailRotorIncr.Id, TailRotorIncr);
            Events.Add(TakeoffAssistArmSet.Id, TakeoffAssistArmSet);
            Events.Add(TakeoffAssistArmToggle.Id, TakeoffAssistArmToggle);
            Events.Add(TakeoffAssistFire.Id, TakeoffAssistFire);
            Events.Add(TaxiLightsOff.Id, TaxiLightsOff);
            Events.Add(TaxiLightsOn.Id, TaxiLightsOn);
            Events.Add(TaxiLightsSet.Id, TaxiLightsSet);
            Events.Add(TextScrollSet.Id, TextScrollSet);
            Events.Add(Throttle10.Id, Throttle10);
            Events.Add(Throttle20.Id, Throttle20);
            Events.Add(Throttle30.Id, Throttle30);
            Events.Add(Throttle40.Id, Throttle40);
            Events.Add(Throttle50.Id, Throttle50);
            Events.Add(Throttle60.Id, Throttle60);
            Events.Add(Throttle70.Id, Throttle70);
            Events.Add(Throttle80.Id, Throttle80);
            Events.Add(Throttle90.Id, Throttle90);
            Events.Add(ThrottleAxisSetEx1.Id, ThrottleAxisSetEx1);
            Events.Add(ThrottleCut.Id, ThrottleCut);
            Events.Add(ThrottleCutEx1.Id, ThrottleCutEx1);
            Events.Add(ThrottleDecr.Id, ThrottleDecr);
            Events.Add(ThrottleDecrSmall.Id, ThrottleDecrSmall);
            Events.Add(ThrottleDecreaseEx1.Id, ThrottleDecreaseEx1);
            Events.Add(ThrottleDecreaseSmallEx1.Id, ThrottleDecreaseSmallEx1);
            Events.Add(ThrottleFull.Id, ThrottleFull);
            Events.Add(ThrottleFullEx1.Id, ThrottleFullEx1);
            Events.Add(ThrottleIncr.Id, ThrottleIncr);
            Events.Add(ThrottleIncreaseEx1.Id, ThrottleIncreaseEx1);
            Events.Add(ThrottleIncreaseSmallEx1.Id, ThrottleIncreaseSmallEx1);
            Events.Add(ThrottleReverseThrustHold.Id, ThrottleReverseThrustHold);
            Events.Add(ThrottleReverseThrustToggle.Id, ThrottleReverseThrustToggle);
            Events.Add(ThrottleSet.Id, ThrottleSet);
            Events.Add(Throttle1AxisSetEx1.Id, Throttle1AxisSetEx1);
            Events.Add(Throttle1Cut.Id, Throttle1Cut);
            Events.Add(Throttle1CutEx1.Id, Throttle1CutEx1);
            Events.Add(Throttle1Decr.Id, Throttle1Decr);
            Events.Add(Throttle1DecrSmall.Id, Throttle1DecrSmall);
            Events.Add(Throttle1DecreaseEx1.Id, Throttle1DecreaseEx1);
            Events.Add(Throttle1DecreaseSmallEx1.Id, Throttle1DecreaseSmallEx1);
            Events.Add(Throttle1Full.Id, Throttle1Full);
            Events.Add(Throttle1FullEx1.Id, Throttle1FullEx1);
            Events.Add(Throttle1Incr.Id, Throttle1Incr);
            Events.Add(Throttle1IncrSmall.Id, Throttle1IncrSmall);
            Events.Add(Throttle1IncreaseEx1.Id, Throttle1IncreaseEx1);
            Events.Add(Throttle1IncreaseSmallEx1.Id, Throttle1IncreaseSmallEx1);
            Events.Add(Throttle1ReverseThrustHold.Id, Throttle1ReverseThrustHold);
            Events.Add(Throttle1Set.Id, Throttle1Set);
            Events.Add(Throttle2AxisSetEx1.Id, Throttle2AxisSetEx1);
            Events.Add(Throttle2Cut.Id, Throttle2Cut);
            Events.Add(Throttle2CutEx1.Id, Throttle2CutEx1);
            Events.Add(Throttle2Decr.Id, Throttle2Decr);
            Events.Add(Throttle2DecrSmall.Id, Throttle2DecrSmall);
            Events.Add(Throttle2DecreaseEx1.Id, Throttle2DecreaseEx1);
            Events.Add(Throttle2DecreaseSmallEx1.Id, Throttle2DecreaseSmallEx1);
            Events.Add(Throttle2Full.Id, Throttle2Full);
            Events.Add(Throttle2FullEx1.Id, Throttle2FullEx1);
            Events.Add(Throttle2Incr.Id, Throttle2Incr);
            Events.Add(Throttle2IncrSmall.Id, Throttle2IncrSmall);
            Events.Add(Throttle2IncreaseEx1.Id, Throttle2IncreaseEx1);
            Events.Add(Throttle2IncreaseSmallEx1.Id, Throttle2IncreaseSmallEx1);
            Events.Add(Throttle2ReverseThrustHold.Id, Throttle2ReverseThrustHold);
            Events.Add(Throttle2Set.Id, Throttle2Set);
            Events.Add(Throttle3AxisSetEx1.Id, Throttle3AxisSetEx1);
            Events.Add(Throttle3Cut.Id, Throttle3Cut);
            Events.Add(Throttle3CutEx1.Id, Throttle3CutEx1);
            Events.Add(Throttle3Decr.Id, Throttle3Decr);
            Events.Add(Throttle3DecrSmall.Id, Throttle3DecrSmall);
            Events.Add(Throttle3DecreaseEx1.Id, Throttle3DecreaseEx1);
            Events.Add(Throttle3DecreaseSmallEx1.Id, Throttle3DecreaseSmallEx1);
            Events.Add(Throttle3Full.Id, Throttle3Full);
            Events.Add(Throttle3FullEx1.Id, Throttle3FullEx1);
            Events.Add(Throttle3Incr.Id, Throttle3Incr);
            Events.Add(Throttle3IncrSmall.Id, Throttle3IncrSmall);
            Events.Add(Throttle3IncreaseEx1.Id, Throttle3IncreaseEx1);
            Events.Add(Throttle3IncreaseSmallEx1.Id, Throttle3IncreaseSmallEx1);
            Events.Add(Throttle3ReverseThrustHold.Id, Throttle3ReverseThrustHold);
            Events.Add(Throttle3Set.Id, Throttle3Set);
            Events.Add(Throttle4AxisSetEx1.Id, Throttle4AxisSetEx1);
            Events.Add(Throttle4Cut.Id, Throttle4Cut);
            Events.Add(Throttle4CutEx1.Id, Throttle4CutEx1);
            Events.Add(Throttle4Decr.Id, Throttle4Decr);
            Events.Add(Throttle4DecrSmall.Id, Throttle4DecrSmall);
            Events.Add(Throttle4DecreaseEx1.Id, Throttle4DecreaseEx1);
            Events.Add(Throttle4DecreaseSmallEx1.Id, Throttle4DecreaseSmallEx1);
            Events.Add(Throttle4Full.Id, Throttle4Full);
            Events.Add(Throttle4FullEx1.Id, Throttle4FullEx1);
            Events.Add(Throttle4Incr.Id, Throttle4Incr);
            Events.Add(Throttle4IncrSmall.Id, Throttle4IncrSmall);
            Events.Add(Throttle4IncreaseEx1.Id, Throttle4IncreaseEx1);
            Events.Add(Throttle4IncreaseSmallEx1.Id, Throttle4IncreaseSmallEx1);
            Events.Add(Throttle4ReverseThrustHold.Id, Throttle4ReverseThrustHold);
            Events.Add(Throttle4Set.Id, Throttle4Set);
            Events.Add(ToggleAfterburner.Id, ToggleAfterburner);
            Events.Add(ToggleAfterburner1.Id, ToggleAfterburner1);
            Events.Add(ToggleAfterburner2.Id, ToggleAfterburner2);
            Events.Add(ToggleAfterburner3.Id, ToggleAfterburner3);
            Events.Add(ToggleAfterburner4.Id, ToggleAfterburner4);
            Events.Add(ToggleAircraftExit.Id, ToggleAircraftExit);
            Events.Add(ToggleAircraftExitFast.Id, ToggleAircraftExitFast);
            Events.Add(ToggleAircraftLabels.Id, ToggleAircraftLabels);
            Events.Add(ToggleAirportNameDisplay.Id, ToggleAirportNameDisplay);
            Events.Add(ToggleAllStarters.Id, ToggleAllStarters);
            Events.Add(ToggleAlternateStatic.Id, ToggleAlternateStatic);
            Events.Add(ToggleAlternator1.Id, ToggleAlternator1);
            Events.Add(ToggleAlternator2.Id, ToggleAlternator2);
            Events.Add(ToggleAlternator3.Id, ToggleAlternator3);
            Events.Add(ToggleAlternator4.Id, ToggleAlternator4);
            Events.Add(ToggleAutofeatherArm.Id, ToggleAutofeatherArm);
            Events.Add(ToggleAvionicsMaster.Id, ToggleAvionicsMaster);
            Events.Add(ToggleBeaconLights.Id, ToggleBeaconLights);
            Events.Add(ToggleCabinLights.Id, ToggleCabinLights);
            Events.Add(ToggleDamageText.Id, ToggleDamageText);
            Events.Add(ToggleDme.Id, ToggleDme);
            Events.Add(ToggleElectFuelPump.Id, ToggleElectFuelPump);
            Events.Add(ToggleElectFuelPump1.Id, ToggleElectFuelPump1);
            Events.Add(ToggleElectFuelPump2.Id, ToggleElectFuelPump2);
            Events.Add(ToggleElectFuelPump3.Id, ToggleElectFuelPump3);
            Events.Add(ToggleElectFuelPump4.Id, ToggleElectFuelPump4);
            Events.Add(ToggleElectricVacuumPump.Id, ToggleElectricVacuumPump);
            Events.Add(ToggleElectricalFailure.Id, ToggleElectricalFailure);
            Events.Add(ToggleEnemyIndicator.Id, ToggleEnemyIndicator);
            Events.Add(ToggleEngine1Failure.Id, ToggleEngine1Failure);
            Events.Add(ToggleEngine2Failure.Id, ToggleEngine2Failure);
            Events.Add(ToggleEngine3Failure.Id, ToggleEngine3Failure);
            Events.Add(ToggleEngine4Failure.Id, ToggleEngine4Failure);
            Events.Add(ToggleExternalPower.Id, ToggleExternalPower);
            Events.Add(ToggleFeatherSwitch1.Id, ToggleFeatherSwitch1);
            Events.Add(ToggleFeatherSwitch2.Id, ToggleFeatherSwitch2);
            Events.Add(ToggleFeatherSwitch3.Id, ToggleFeatherSwitch3);
            Events.Add(ToggleFeatherSwitch4.Id, ToggleFeatherSwitch4);
            Events.Add(ToggleFeatherSwitches.Id, ToggleFeatherSwitches);
            Events.Add(ToggleFlightDirector.Id, ToggleFlightDirector);
            Events.Add(ToggleFuelValveAll.Id, ToggleFuelValveAll);
            Events.Add(ToggleFuelValveEng1.Id, ToggleFuelValveEng1);
            Events.Add(ToggleFuelValveEng2.Id, ToggleFuelValveEng2);
            Events.Add(ToggleFuelValveEng3.Id, ToggleFuelValveEng3);
            Events.Add(ToggleFuelValveEng4.Id, ToggleFuelValveEng4);
            Events.Add(ToggleGpsDrivesNav1.Id, ToggleGpsDrivesNav1);
            Events.Add(ToggleHydraulicFailure.Id, ToggleHydraulicFailure);
            Events.Add(ToggleIcs.Id, ToggleIcs);
            Events.Add(ToggleJetway.Id, ToggleJetway);
            Events.Add(ToggleLaunchBarSwitch.Id, ToggleLaunchBarSwitch);
            Events.Add(ToggleLeftBrakeFailure.Id, ToggleLeftBrakeFailure);
            Events.Add(ToggleLogoLights.Id, ToggleLogoLights);
            Events.Add(ToggleMasterAlternator.Id, ToggleMasterAlternator);
            Events.Add(ToggleMasterBattery.Id, ToggleMasterBattery);
            Events.Add(ToggleMasterBatteryAlternator.Id, ToggleMasterBatteryAlternator);
            Events.Add(ToggleMasterIgnitionSwitch.Id, ToggleMasterIgnitionSwitch);
            Events.Add(ToggleMasterStarterSwitch.Id, ToggleMasterStarterSwitch);
            Events.Add(ToggleNavLights.Id, ToggleNavLights);
            Events.Add(TogglePadlock.Id, TogglePadlock);
            Events.Add(TogglePitotBlockage.Id, TogglePitotBlockage);
            Events.Add(TogglePrimer.Id, TogglePrimer);
            Events.Add(TogglePrimer1.Id, TogglePrimer1);
            Events.Add(TogglePrimer2.Id, TogglePrimer2);
            Events.Add(TogglePrimer3.Id, TogglePrimer3);
            Events.Add(TogglePrimer4.Id, TogglePrimer4);
            Events.Add(TogglePropellerDeice.Id, TogglePropellerDeice);
            Events.Add(TogglePropellerSync.Id, TogglePropellerSync);
            Events.Add(TogglePushback.Id, TogglePushback);
            Events.Add(ToggleRaceresultsWindow.Id, ToggleRaceresultsWindow);
            Events.Add(ToggleRadInsSwitch.Id, ToggleRadInsSwitch);
            Events.Add(ToggleRadar.Id, ToggleRadar);
            Events.Add(ToggleRadio.Id, ToggleRadio);
            Events.Add(ToggleRamptruck.Id, ToggleRamptruck);
            Events.Add(ToggleRecognitionLights.Id, ToggleRecognitionLights);
            Events.Add(ToggleRightBrakeFailure.Id, ToggleRightBrakeFailure);
            Events.Add(ToggleSpeaker.Id, ToggleSpeaker);
            Events.Add(ToggleStarter1.Id, ToggleStarter1);
            Events.Add(ToggleStarter2.Id, ToggleStarter2);
            Events.Add(ToggleStarter3.Id, ToggleStarter3);
            Events.Add(ToggleStarter4.Id, ToggleStarter4);
            Events.Add(ToggleStaticPortBlockage.Id, ToggleStaticPortBlockage);
            Events.Add(ToggleStructuralDeice.Id, ToggleStructuralDeice);
            Events.Add(ToggleTacanDrivesNav1.Id, ToggleTacanDrivesNav1);
            Events.Add(ToggleTailHookHandle.Id, ToggleTailHookHandle);
            Events.Add(ToggleTailwheelLock.Id, ToggleTailwheelLock);
            Events.Add(ToggleTaxiLights.Id, ToggleTaxiLights);
            Events.Add(ToggleThrottle1ReverseThrust.Id, ToggleThrottle1ReverseThrust);
            Events.Add(ToggleThrottle2ReverseThrust.Id, ToggleThrottle2ReverseThrust);
            Events.Add(ToggleThrottle3ReverseThrust.Id, ToggleThrottle3ReverseThrust);
            Events.Add(ToggleThrottle4ReverseThrust.Id, ToggleThrottle4ReverseThrust);
            Events.Add(ToggleTotalBrakeFailure.Id, ToggleTotalBrakeFailure);
            Events.Add(ToggleTurnIndicatorSwitch.Id, ToggleTurnIndicatorSwitch);
            Events.Add(ToggleVacuumFailure.Id, ToggleVacuumFailure);
            Events.Add(ToggleVariometerSwitch.Id, ToggleVariometerSwitch);
            Events.Add(ToggleWaterBallastValve.Id, ToggleWaterBallastValve);
            Events.Add(ToggleWaterRudder.Id, ToggleWaterRudder);
            Events.Add(ToggleWingFold.Id, ToggleWingFold);
            Events.Add(ToggleWingLights.Id, ToggleWingLights);
            Events.Add(TooltipUnitsSet.Id, TooltipUnitsSet);
            Events.Add(TooltipUnitsToggle.Id, TooltipUnitsToggle);
            Events.Add(TowPlaneRelease.Id, TowPlaneRelease);
            Events.Add(TowPlaneRequest.Id, TowPlaneRequest);
            Events.Add(TrueAirspeedCalDec.Id, TrueAirspeedCalDec);
            Events.Add(TrueAirspeedCalInc.Id, TrueAirspeedCalInc);
            Events.Add(TrueAirspeedCalSet.Id, TrueAirspeedCalSet);
            Events.Add(TugDisable.Id, TugDisable);
            Events.Add(TugHeading.Id, TugHeading);
            Events.Add(TugSpeed.Id, TugSpeed);
            Events.Add(TurbineIgnitionSwitchSet.Id, TurbineIgnitionSwitchSet);
            Events.Add(TurbineIgnitionSwitchSet1.Id, TurbineIgnitionSwitchSet1);
            Events.Add(TurbineIgnitionSwitchSet2.Id, TurbineIgnitionSwitchSet2);
            Events.Add(TurbineIgnitionSwitchSet3.Id, TurbineIgnitionSwitchSet3);
            Events.Add(TurbineIgnitionSwitchSet4.Id, TurbineIgnitionSwitchSet4);
            Events.Add(TurbineIgnitionSwitchToggle.Id, TurbineIgnitionSwitchToggle);
            Events.Add(UnlockTarget.Id, UnlockTarget);
            Events.Add(Userinterrupt.Id, Userinterrupt);
            Events.Add(VariometerSoundToggle.Id, VariometerSoundToggle);
            Events.Add(VerticalSpeedDec.Id, VerticalSpeedDec);
            Events.Add(VerticalSpeedInc.Id, VerticalSpeedInc);
            Events.Add(VerticalSpeedSet.Id, VerticalSpeedSet);
            Events.Add(VerticalSpeedZero.Id, VerticalSpeedZero);
            Events.Add(VideoRecordToggle.Id, VideoRecordToggle);
            Events.Add(View.Id, View);
            Events.Add(ViewAlwaysPanDown.Id, ViewAlwaysPanDown);
            Events.Add(ViewAlwaysPanUp.Id, ViewAlwaysPanUp);
            Events.Add(ViewAux00.Id, ViewAux00);
            Events.Add(ViewAux01.Id, ViewAux01);
            Events.Add(ViewAux02.Id, ViewAux02);
            Events.Add(ViewAux03.Id, ViewAux03);
            Events.Add(ViewAux04.Id, ViewAux04);
            Events.Add(ViewAux05.Id, ViewAux05);
            Events.Add(ViewAxisIndicatorCycle.Id, ViewAxisIndicatorCycle);
            Events.Add(ViewCameraSelect0.Id, ViewCameraSelect0);
            Events.Add(ViewCameraSelect1.Id, ViewCameraSelect1);
            Events.Add(ViewCameraSelect2.Id, ViewCameraSelect2);
            Events.Add(ViewCameraSelect3.Id, ViewCameraSelect3);
            Events.Add(ViewCameraSelect4.Id, ViewCameraSelect4);
            Events.Add(ViewCameraSelect5.Id, ViewCameraSelect5);
            Events.Add(ViewCameraSelect6.Id, ViewCameraSelect6);
            Events.Add(ViewCameraSelect7.Id, ViewCameraSelect7);
            Events.Add(ViewCameraSelect8.Id, ViewCameraSelect8);
            Events.Add(ViewCameraSelect9.Id, ViewCameraSelect9);
            Events.Add(ViewCameraSelectStart.Id, ViewCameraSelectStart);
            Events.Add(ViewChaseDistanceAdd.Id, ViewChaseDistanceAdd);
            Events.Add(ViewChaseDistanceSub.Id, ViewChaseDistanceSub);
            Events.Add(ViewCockpitForward.Id, ViewCockpitForward);
            Events.Add(ViewDirectionSet.Id, ViewDirectionSet);
            Events.Add(ViewDown.Id, ViewDown);
            Events.Add(ViewForward.Id, ViewForward);
            Events.Add(ViewForwardLeft.Id, ViewForwardLeft);
            Events.Add(ViewForwardLeftUp.Id, ViewForwardLeftUp);
            Events.Add(ViewForwardRight.Id, ViewForwardRight);
            Events.Add(ViewForwardRightUp.Id, ViewForwardRightUp);
            Events.Add(ViewForwardUp.Id, ViewForwardUp);
            Events.Add(ViewLeft.Id, ViewLeft);
            Events.Add(ViewLeftUp.Id, ViewLeftUp);
            Events.Add(ViewLinkingSet.Id, ViewLinkingSet);
            Events.Add(ViewLinkingToggle.Id, ViewLinkingToggle);
            Events.Add(ViewMode.Id, ViewMode);
            Events.Add(ViewModeRev.Id, ViewModeRev);
            Events.Add(ViewPanelAlphaDec.Id, ViewPanelAlphaDec);
            Events.Add(ViewPanelAlphaInc.Id, ViewPanelAlphaInc);
            Events.Add(ViewPanelAlphaSelect.Id, ViewPanelAlphaSelect);
            Events.Add(ViewPanelAlphaSet.Id, ViewPanelAlphaSet);
            Events.Add(ViewPreviousToggle.Id, ViewPreviousToggle);
            Events.Add(ViewRear.Id, ViewRear);
            Events.Add(ViewRearLeft.Id, ViewRearLeft);
            Events.Add(ViewRearLeftUp.Id, ViewRearLeftUp);
            Events.Add(ViewRearRight.Id, ViewRearRight);
            Events.Add(ViewRearRightUp.Id, ViewRearRightUp);
            Events.Add(ViewRearUp.Id, ViewRearUp);
            Events.Add(ViewReset.Id, ViewReset);
            Events.Add(ViewRight.Id, ViewRight);
            Events.Add(ViewRightUp.Id, ViewRightUp);
            Events.Add(ViewSnapPanel.Id, ViewSnapPanel);
            Events.Add(ViewSnapPanelReset.Id, ViewSnapPanelReset);
            Events.Add(ViewTrackPanToggle.Id, ViewTrackPanToggle);
            Events.Add(ViewType.Id, ViewType);
            Events.Add(ViewTypeRev.Id, ViewTypeRev);
            Events.Add(ViewUp.Id, ViewUp);
            Events.Add(ViewVirtualCockpitForward.Id, ViewVirtualCockpitForward);
            Events.Add(ViewWindowTitlesToggle.Id, ViewWindowTitlesToggle);
            Events.Add(ViewWindowToFront.Id, ViewWindowToFront);
            Events.Add(View1DirectionSet.Id, View1DirectionSet);
            Events.Add(View1ModeSet.Id, View1ModeSet);
            Events.Add(View1ZoomSet.Id, View1ZoomSet);
            Events.Add(View2DirectionSet.Id, View2DirectionSet);
            Events.Add(View2ModeSet.Id, View2ModeSet);
            Events.Add(View2ZoomSet.Id, View2ZoomSet);
            Events.Add(VirtualCopilotAction.Id, VirtualCopilotAction);
            Events.Add(VirtualCopilotSet.Id, VirtualCopilotSet);
            Events.Add(VirtualCopilotToggle.Id, VirtualCopilotToggle);
            Events.Add(VorObs.Id, VorObs);
            Events.Add(Vor1ObiDec.Id, Vor1ObiDec);
            Events.Add(Vor1ObiFastDec.Id, Vor1ObiFastDec);
            Events.Add(Vor1ObiFastInc.Id, Vor1ObiFastInc);
            Events.Add(Vor1ObiInc.Id, Vor1ObiInc);
            Events.Add(Vor1Set.Id, Vor1Set);
            Events.Add(Vor2ObiDec.Id, Vor2ObiDec);
            Events.Add(Vor2ObiFastDec.Id, Vor2ObiFastDec);
            Events.Add(Vor2ObiFastInc.Id, Vor2ObiFastInc);
            Events.Add(Vor2ObiInc.Id, Vor2ObiInc);
            Events.Add(Vor2Set.Id, Vor2Set);
            Events.Add(Vor3ObiDec.Id, Vor3ObiDec);
            Events.Add(Vor3ObiFastDec.Id, Vor3ObiFastDec);
            Events.Add(Vor3ObiFastInc.Id, Vor3ObiFastInc);
            Events.Add(Vor3ObiInc.Id, Vor3ObiInc);
            Events.Add(Vor3Set.Id, Vor3Set);
            Events.Add(Vor4ObiDec.Id, Vor4ObiDec);
            Events.Add(Vor4ObiFastDec.Id, Vor4ObiFastDec);
            Events.Add(Vor4ObiFastInc.Id, Vor4ObiFastInc);
            Events.Add(Vor4ObiInc.Id, Vor4ObiInc);
            Events.Add(Vor4Set.Id, Vor4Set);
            Events.Add(VsSlotIndexSet.Id, VsSlotIndexSet);
            Events.Add(VsiBugSelect.Id, VsiBugSelect);
            Events.Add(WarEmergencyPower.Id, WarEmergencyPower);
            Events.Add(WindowTitlesSet.Id, WindowTitlesSet);
            Events.Add(WindshieldDeiceOff.Id, WindshieldDeiceOff);
            Events.Add(WindshieldDeiceOn.Id, WindshieldDeiceOn);
            Events.Add(WindshieldDeiceSet.Id, WindshieldDeiceSet);
            Events.Add(WindshieldDeiceToggle.Id, WindshieldDeiceToggle);
            Events.Add(WingLightsOff.Id, WingLightsOff);
            Events.Add(WingLightsOn.Id, WingLightsOn);
            Events.Add(WingLightsSet.Id, WingLightsSet);
            Events.Add(Xpndr.Id, Xpndr);
            Events.Add(Xpndr1Dec.Id, Xpndr1Dec);
            Events.Add(Xpndr1Inc.Id, Xpndr1Inc);
            Events.Add(Xpndr10Dec.Id, Xpndr10Dec);
            Events.Add(Xpndr10Inc.Id, Xpndr10Inc);
            Events.Add(Xpndr100Dec.Id, Xpndr100Dec);
            Events.Add(Xpndr100Inc.Id, Xpndr100Inc);
            Events.Add(Xpndr1000Dec.Id, Xpndr1000Dec);
            Events.Add(Xpndr1000Inc.Id, Xpndr1000Inc);
            Events.Add(XpndrDecCarry.Id, XpndrDecCarry);
            Events.Add(XpndrIdentOff.Id, XpndrIdentOff);
            Events.Add(XpndrIdentOn.Id, XpndrIdentOn);
            Events.Add(XpndrIdentSet.Id, XpndrIdentSet);
            Events.Add(XpndrIdentToggle.Id, XpndrIdentToggle);
            Events.Add(XpndrIncCarry.Id, XpndrIncCarry);
            Events.Add(XpndrSet.Id, XpndrSet);
            Events.Add(YawDamperOff.Id, YawDamperOff);
            Events.Add(YawDamperOn.Id, YawDamperOn);
            Events.Add(YawDamperSet.Id, YawDamperSet);
            Events.Add(YawDamperToggle.Id, YawDamperToggle);
            Events.Add(YaxisInvertToggle.Id, YaxisInvertToggle);
            Events.Add(Zoom1X.Id, Zoom1X);
            Events.Add(ZoomIn.Id, ZoomIn);
            Events.Add(ZoomInFine.Id, ZoomInFine);
            Events.Add(ZoomMinus.Id, ZoomMinus);
            Events.Add(ZoomOut.Id, ZoomOut);
            Events.Add(ZoomOutFine.Id, ZoomOutFine);
            Events.Add(ZoomPlus.Id, ZoomPlus);
            Events.Add(ZuluDaySet.Id, ZuluDaySet);
            Events.Add(ZuluHoursSet.Id, ZuluHoursSet);
            Events.Add(ZuluMinutesSet.Id, ZuluMinutesSet);
            Events.Add(ZuluYearSet.Id, ZuluYearSet);
        }

        public Dictionary<SimConnectEventId, SimConnectEvent> Events { get; set; }

        private SimConnectEvent Abort { get { return new SimConnectEvent() { Id = SimConnectEventId.Abort, Name = "ABORT", Units = "N/A", Description = "Quit Microsoft Flight Simulator without a message." }; } }
        private SimConnectEvent AddFuelQuantity { get { return new SimConnectEvent() { Id = SimConnectEventId.AddFuelQuantity, Name = "ADD_FUEL_QUANTITY", Units = "[0]: The fuel quantity", Description = "Adds fuel to the aircraft, 25% of capacity by default. 0 to 65535 (max fuel) can be passed." }; } }
        private SimConnectEvent Adf { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf, Name = "ADF", Units = "N/A", Description = "Sequentially selects the ADF tuner digits for use with +/-. Follow by SELECT_1 for ADF 1, or SELECT_2 for ADF 2." }; } }
        private SimConnectEvent Adf1Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1Dec, Name = "ADF_1_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 1 KHz, with wrapping." }; } }
        private SimConnectEvent Adf1Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1Inc, Name = "ADF_1_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 1 KHz, with wrapping." }; } }
        private SimConnectEvent Adf10Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf10Dec, Name = "ADF_10_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 10 KHz, with wrapping." }; } }
        private SimConnectEvent Adf10Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf10Inc, Name = "ADF_10_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 10 KHz, with wrapping." }; } }
        private SimConnectEvent Adf100Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf100Dec, Name = "ADF_100_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 100 KHz, with wrapping." }; } }
        private SimConnectEvent Adf100Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf100Inc, Name = "ADF_100_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 100 KHz, with wrapping." }; } }
        private SimConnectEvent AdfActiveSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfActiveSet, Name = "ADF_ACTIVE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 active frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent AdfCardDec { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfCardDec, Name = "ADF_CARD_DEC", Units = "N/A", Description = "Decrements the ADF card by 10° if the key is pressed more than 2 seconds, 4° if the key is pressed more than 1 second, or by 1° otherwise. The resulting value is clamped between 0° and 360°." }; } }
        private SimConnectEvent AdfCardInc { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfCardInc, Name = "ADF_CARD_INC", Units = "N/A", Description = "Increments the ADF card by 10° if the key is pressed more than 2 seconds, 4° if the key is pressed more than 1 second, or by 1° otherwise. The resulting value is clamped between 0° and 360°." }; } }
        private SimConnectEvent AdfCardSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfCardSet, Name = "ADF_CARD_SET", Units = "[0]: Card value", Description = "Sets the ADF card. The resulting value is clamped between 0° and 360°." }; } }
        private SimConnectEvent AdfCompleteSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfCompleteSet, Name = "ADF_COMPLETE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent AdfExtendedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfExtendedSet, Name = "ADF_EXTENDED_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 frequency (thousands and tenths, BCD32 encoded HZ)." }; } }
        private SimConnectEvent AdfFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfFractDecCarry, Name = "ADF_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 0.1 KHz, with carry." }; } }
        private SimConnectEvent AdfFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfFractIncCarry, Name = "ADF_FRACT_INC_CARRY", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 0.1 KHz, with carry." }; } }
        private SimConnectEvent AdfHighrangeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfHighrangeSet, Name = "ADF_HIGHRANGE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 highrange frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent AdfLowrangeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfLowrangeSet, Name = "ADF_LOWRANGE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 lowrange frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent AdfNeedleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfNeedleSet, Name = "ADF_NEEDLE_SET", Units = "[0]: Needle value", Description = "Sets the ADF 1 / 2 needle value, in radians." }; } }
        private SimConnectEvent AdfOutsideSource { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfOutsideSource, Name = "ADF_OUTSIDE_SOURCE", Units = "[0]: Bool", Description = "When TRUE sets ADF 1 / 2 source to be outside, when FALSE it's not. This enables you to use the ADF_NEEDLE_SET / ADF2_NEEDLE_SET events to set the ADF needle instead of relying on the simulation source." }; } }
        private SimConnectEvent AdfSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfSet, Name = "ADF_SET", Units = "[0]: Frequency value", Description = "Sets ADF 1 / 2 frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent AdfStbySet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfStbySet, Name = "ADF_STBY_SET", Units = "[0]: Frequency value", Description = "Sets ADF 1 / 2 standby frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent AdfVolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfVolumeDec, Name = "ADF_VOLUME_DEC", Units = "N/A", Description = "Decrease ADF 1 / 2 volume by 0.02. The resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent AdfVolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfVolumeInc, Name = "ADF_VOLUME_INC", Units = "N/A", Description = "Increase ADF 1 / 2 volume by 0.02. The resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent AdfVolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AdfVolumeSet, Name = "ADF_VOLUME_SET", Units = "[0]: Volume value", Description = "Sets ADF 1 / 2 volume (from 0 to 100)." }; } }
        private SimConnectEvent Adf1RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1RadioSwap, Name = "ADF1_RADIO_SWAP", Units = "N/A", Description = "Swaps between the ADF 1 / 2 frequency and the standby frequency." }; } }
        private SimConnectEvent Adf1RadioTenthsDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1RadioTenthsDec, Name = "ADF1_RADIO_TENTHS_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 0.1 KHz." }; } }
        private SimConnectEvent Adf1RadioTenthsInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1RadioTenthsInc, Name = "ADF1_RADIO_TENTHS_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 0.1 KHz." }; } }
        private SimConnectEvent Adf1WholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1WholeDec, Name = "ADF1_WHOLE_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 1 KHz, with carry as digits wrap." }; } }
        private SimConnectEvent Adf1WholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf1WholeInc, Name = "ADF1_WHOLE_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 1 KHz, with carry as digits wrap." }; } }
        private SimConnectEvent Adf21Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf21Dec, Name = "ADF2_1_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 1 KHz, with wrapping." }; } }
        private SimConnectEvent Adf21Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf21Inc, Name = "ADF2_1_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 1 KHz, with wrapping." }; } }
        private SimConnectEvent Adf210Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf210Dec, Name = "ADF2_10_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 10 KHz, with wrapping." }; } }
        private SimConnectEvent Adf210Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf210Inc, Name = "ADF2_10_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 10 KHz, with wrapping." }; } }
        private SimConnectEvent Adf2100Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2100Dec, Name = "ADF2_100_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 100 KHz, with wrapping." }; } }
        private SimConnectEvent Adf2100Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2100Inc, Name = "ADF2_100_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 100 KHz, with wrapping." }; } }
        private SimConnectEvent Adf2ActiveSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2ActiveSet, Name = "ADF2_ACTIVE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 active frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent Adf2CompleteSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2CompleteSet, Name = "ADF2_COMPLETE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent Adf2ExtendedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2ExtendedSet, Name = "ADF2_EXTENDED_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 frequency (thousands and tenths, BCD32 encoded HZ)." }; } }
        private SimConnectEvent Adf2FractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2FractDecCarry, Name = "ADF2_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 0.1 KHz, with carry." }; } }
        private SimConnectEvent Adf2FractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2FractIncCarry, Name = "ADF2_FRACT_INC_CARRY", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 0.1 KHz, with carry." }; } }
        private SimConnectEvent Adf2HighrangeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2HighrangeSet, Name = "ADF2_HIGHRANGE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 highrange frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent Adf2LowrangeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2LowrangeSet, Name = "ADF2_LOWRANGE_SET", Units = "[0]: Frequency value (BCD32 encoded Hz)", Description = "Sets the ADF 1 / 2 lowrange frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent Adf2NeedleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2NeedleSet, Name = "ADF2_NEEDLE_SET", Units = "[0]: Needle value", Description = "Note that ADF_OUTSIDE_SOURCE / ADF2_OUTSIDE_SOURCE must be enabled." }; } }
        private SimConnectEvent Adf2OutsideSource { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2OutsideSource, Name = "ADF2_OUTSIDE_SOURCE", Units = "[0]: Bool", Description = "When TRUE sets ADF 1 / 2 source to be outside, when FALSE it's not. This enables you to use the ADF_NEEDLE_SET / ADF2_NEEDLE_SET events to set the ADF needle instead of relying on the simulation source." }; } }
        private SimConnectEvent Adf2RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2RadioSwap, Name = "ADF2_RADIO_SWAP", Units = "N/A", Description = "Swaps between the ADF 1 / 2 frequency and the standby frequency." }; } }
        private SimConnectEvent Adf2RadioTenthsDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2RadioTenthsDec, Name = "ADF2_RADIO_TENTHS_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 0.1 KHz." }; } }
        private SimConnectEvent Adf2RadioTenthsInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2RadioTenthsInc, Name = "ADF2_RADIO_TENTHS_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 0.1 KHz." }; } }
        private SimConnectEvent Adf2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2Set, Name = "ADF2_SET", Units = "[0]: Frequency value", Description = "Sets ADF 1 / 2 frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent Adf2StbySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2StbySet, Name = "ADF2_STBY_SET", Units = "[0]: Frequency value", Description = "Sets ADF 1 / 2 standby frequency (BCD32 encoded Hz)." }; } }
        private SimConnectEvent Adf2VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2VolumeDec, Name = "ADF2_VOLUME_DEC", Units = "N/A", Description = "Decrease ADF 1 / 2 volume by 0.02. The resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Adf2VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2VolumeInc, Name = "ADF2_VOLUME_INC", Units = "N/A", Description = "Increase ADF 1 / 2 volume by 0.02. The resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Adf2VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2VolumeSet, Name = "ADF2_VOLUME_SET", Units = "[0]: Volume value", Description = "Sets ADF 1 / 2 volume (from 0 to 100)." }; } }
        private SimConnectEvent Adf2WholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2WholeDec, Name = "ADF2_WHOLE_DEC", Units = "N/A", Description = "Decrements the ADF 1 / 2 frequency by 1 KHz, with carry as digits wrap." }; } }
        private SimConnectEvent Adf2WholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Adf2WholeInc, Name = "ADF2_WHOLE_INC", Units = "N/A", Description = "Increments the ADF 1 / 2 frequency by 1 KHz, with carry as digits wrap." }; } }
        private SimConnectEvent AdventureAction { get { return new SimConnectEvent() { Id = SimConnectEventId.AdventureAction, Name = "ADVENTURE_ACTION", Units = "-", Description = "Not used by the simulation." }; } }
        private SimConnectEvent AileronCenter { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronCenter, Name = "AILERON_CENTER", Units = "N/A", Description = "Centers aileron position. Note that this is simply an alias for the CENTER_AILER_RUDDER event.\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent AileronLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronLeft, Name = "AILERON_LEFT", Units = "N/A", Description = "Increments the left aileron by 1°. Note that this is simply an alias for the AILERONS_LEFT event." }; } }
        private SimConnectEvent AileronRight { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronRight, Name = "AILERON_RIGHT", Units = "N/A", Description = "Increments the right aileron by 1°. Note that this is simply an alias for the AILERONS_RIGHT event." }; } }
        private SimConnectEvent AileronSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronSet, Name = "AILERON_SET", Units = "[0] Position (-16383 to 16384)", Description = "Sets the aileron position." }; } }
        private SimConnectEvent AileronTrimDisabledSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronTrimDisabledSet, Name = "AILERON_TRIM_DISABLED_SET", Units = "[0]: Bool", Description = "Enable (1, TRUE) or disable (0, FALSE) the aileron trim." }; } }
        private SimConnectEvent AileronTrimDisabledToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronTrimDisabledToggle, Name = "AILERON_TRIM_DISABLED_TOGGLE", Units = "N/A", Description = "Toggle the aileron trim disabled option between on (1) and off (0)." }; } }
        private SimConnectEvent AileronTrimLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronTrimLeft, Name = "AILERON_TRIM_LEFT", Units = "N/A", Description = "Increments the left aileron trim by 0.001." }; } }
        private SimConnectEvent AileronTrimRight { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronTrimRight, Name = "AILERON_TRIM_RIGHT", Units = "N/A", Description = "Increments the right aileron trim by 0.001." }; } }
        private SimConnectEvent AileronTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronTrimSet, Name = "AILERON_TRIM_SET", Units = "[0] Position (-100 to 100)", Description = "Sets the aileron trim." }; } }
        private SimConnectEvent AileronTrimSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronTrimSetEx1, Name = "AILERON_TRIM_SET_EX1", Units = "[0] Position (-16383 to 16384)", Description = "Sets the aileron trim with extra precision." }; } }
        private SimConnectEvent AileronsLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronsLeft, Name = "AILERONS_LEFT", Units = "N/A", Description = "Increments the left ailerons by 1°." }; } }
        private SimConnectEvent AileronsRight { get { return new SimConnectEvent() { Id = SimConnectEventId.AileronsRight, Name = "AILERONS_RIGHT", Units = "N/A", Description = "Increments the right ailerons by 1°." }; } }
        private SimConnectEvent AirspeedBugSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.AirspeedBugSelect, Name = "AIRSPEED_BUG_SELECT", Units = "[0]: the reference value", Description = "Selects the airspeed reference for use with +/-" }; } }
        private SimConnectEvent AllLightsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AllLightsToggle, Name = "ALL_LIGHTS_TOGGLE", Units = "[0]: light circuit index", Description = "Toggle all lights" }; } }
        private SimConnectEvent AlternatorOff { get { return new SimConnectEvent() { Id = SimConnectEventId.AlternatorOff, Name = "ALTERNATOR_OFF", Units = "[0]: alternator index", Description = "Turns the indexed alternator off. The alternator index is the N index of the alternator.N definition." }; } }
        private SimConnectEvent AlternatorOn { get { return new SimConnectEvent() { Id = SimConnectEventId.AlternatorOn, Name = "ALTERNATOR_ON", Units = "[0]: alternator index", Description = "Turns the indexed alternator on. The alternator index is the N index of the alternator.N definition." }; } }
        private SimConnectEvent AlternatorSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AlternatorSet, Name = "ALTERNATOR_SET", Units = "[0]: The state on/off (bool)\r\n[1]: alternator index", Description = "Sets the indexed alternator. The alternator index is the N index of the alternator.N definition." }; } }
        private SimConnectEvent AltitudeBugSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.AltitudeBugSelect, Name = "ALTITUDE_BUG_SELECT", Units = "[0]: the reference value", Description = "Selects the altitude reference for use with +/-" }; } }
        private SimConnectEvent AltitudeSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AltitudeSlotIndexSet, Name = "ALTITUDE_SLOT_INDEX_SET", Units = "", Description = "Sets the index for the SimVar AUTOPILOT ALTITUDE LOCK VAR which the altitude hold mode will track when captured. See alt_mode_slot_index for more information." }; } }
        private SimConnectEvent AnalysisManeuverStop { get { return new SimConnectEvent() { Id = SimConnectEventId.AnalysisManeuverStop, Name = "ANALYSIS_MANEUVER_STOP", Units = "-", Description = "Not used by the simulation." }; } }
        private SimConnectEvent AnnunciatorSwitchOff { get { return new SimConnectEvent() { Id = SimConnectEventId.AnnunciatorSwitchOff, Name = "ANNUNCIATOR_SWITCH_OFF", Units = "N/A", Description = "Turns off (0) the annunciator switch." }; } }
        private SimConnectEvent AnnunciatorSwitchOn { get { return new SimConnectEvent() { Id = SimConnectEventId.AnnunciatorSwitchOn, Name = "ANNUNCIATOR_SWITCH_ON", Units = "N/A", Description = "Turns on (1) the annunciator switch." }; } }
        private SimConnectEvent AnnunciatorSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AnnunciatorSwitchToggle, Name = "ANNUNCIATOR_SWITCH_TOGGLE", Units = "N/A", Description = "Toggles the annunciator switch off (0) and on (1)." }; } }
        private SimConnectEvent AntiIceGradualSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceGradualSet, Name = "ANTI_ICE_GRADUAL_SET", Units = "[0]: Position (0 - 16384)", Description = "Sets engine anti-ice switch to a value between 0 and 16384.\r\nControlled engines are set through the SimVar ENGINE CONTROL SELECT." }; } }
        private SimConnectEvent AntiIceGradualSetEng1 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceGradualSetEng1, Name = "ANTI_ICE_GRADUAL_SET_ENG1", Units = "[0]: Position (0 - 16384)", Description = "Sets the engine 1/2/3/4 anti-ice switch to a value between 0 and 16384." }; } }
        private SimConnectEvent AntiIceGradualSetEng2 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceGradualSetEng2, Name = "ANTI_ICE_GRADUAL_SET_ENG2", Units = "[0]: Position (0 - 16384)", Description = "Sets the engine 1/2/3/4 anti-ice switch to a value between 0 and 16384." }; } }
        private SimConnectEvent AntiIceGradualSetEng3 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceGradualSetEng3, Name = "ANTI_ICE_GRADUAL_SET_ENG3", Units = "[0]: Position (0 - 16384)", Description = "Sets the engine 1/2/3/4 anti-ice switch to a value between 0 and 16384." }; } }
        private SimConnectEvent AntiIceGradualSetEng4 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceGradualSetEng4, Name = "ANTI_ICE_GRADUAL_SET_ENG4", Units = "[0]: Position (0 - 16384)", Description = "Sets the engine 1/2/3/4 anti-ice switch to a value between 0 and 16384." }; } }
        private SimConnectEvent AntiIceOff { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceOff, Name = "ANTI_ICE_OFF", Units = "N/A", Description = "Sets anti-ice switches off.\r\nControlled engines are set through the SimVar ENGINE CONTROL SELECT." }; } }
        private SimConnectEvent AntiIceOn { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceOn, Name = "ANTI_ICE_ON", Units = "N/A", Description = "Sets anti-ice switches on\r\nControlled engines are set through the SimVar ENGINE CONTROL SELECT." }; } }
        private SimConnectEvent AntiIceSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceSet, Name = "ANTI_ICE_SET", Units = "[0]: Bool", Description = "Sets anti-ice switches from argument (0,1). Controlled engines are set through the SimVar ENGINE CONTROL SELECT." }; } }
        private SimConnectEvent AntiIceSetEng1 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceSetEng1, Name = "ANTI_ICE_SET_ENG1", Units = "[0]: Bool", Description = "Sets engine 1/2/3/4 anti-ice switch (0,1)" }; } }
        private SimConnectEvent AntiIceSetEng2 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceSetEng2, Name = "ANTI_ICE_SET_ENG2", Units = "[0]: Bool", Description = "Sets engine 1/2/3/4 anti-ice switch (0,1)" }; } }
        private SimConnectEvent AntiIceSetEng3 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceSetEng3, Name = "ANTI_ICE_SET_ENG3", Units = "[0]: Bool", Description = "Sets engine 1/2/3/4 anti-ice switch (0,1)" }; } }
        private SimConnectEvent AntiIceSetEng4 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceSetEng4, Name = "ANTI_ICE_SET_ENG4", Units = "[0]: Bool", Description = "Sets engine 1/2/3/4 anti-ice switch (0,1)" }; } }
        private SimConnectEvent AntiIceToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceToggle, Name = "ANTI_ICE_TOGGLE", Units = "N/A", Description = "Toggle anti-ice switches.\r\nControlled engines are set through the SimVar ENGINE CONTROL SELECT." }; } }
        private SimConnectEvent AntiIceToggleEng1 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceToggleEng1, Name = "ANTI_ICE_TOGGLE_ENG1", Units = "N/A", Description = "Toggle engine 1/2/3/4 anti-ice switch on (1) or off (0)." }; } }
        private SimConnectEvent AntiIceToggleEng2 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceToggleEng2, Name = "ANTI_ICE_TOGGLE_ENG2", Units = "N/A", Description = "Toggle engine 1/2/3/4 anti-ice switch on (1) or off (0)." }; } }
        private SimConnectEvent AntiIceToggleEng3 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceToggleEng3, Name = "ANTI_ICE_TOGGLE_ENG3", Units = "N/A", Description = "Toggle engine 1/2/3/4 anti-ice switch on (1) or off (0)." }; } }
        private SimConnectEvent AntiIceToggleEng4 { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiIceToggleEng4, Name = "ANTI_ICE_TOGGLE_ENG4", Units = "N/A", Description = "Toggle engine 1/2/3/4 anti-ice switch on (1) or off (0)." }; } }
        private SimConnectEvent AntidetonationTankValveToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AntidetonationTankValveToggle, Name = "ANTIDETONATION_TANK_VALVE_TOGGLE", Units = "[0]: Tank index (optional)", Description = "Toggle the anti-detonation valve. Pass a value to determine which tank to use if there are multiple tanks. See the Fuel Selector Codes list for the correct tank code to use. Note that this key requires the [ANTIDETONATION_SYSTEM.N] system to have been set" }; } }
        private SimConnectEvent AntiskidBrakesToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AntiskidBrakesToggle, Name = "ANTISKID_BRAKES_TOGGLE", Units = "N/A", Description = "Turn the anti-skid braking system on or off." }; } }
        private SimConnectEvent ApAirspeedHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAirspeedHold, Name = "AP_AIRSPEED_HOLD", Units = "N/A", Description = "Toggles airspeed hold mode" }; } }
        private SimConnectEvent ApAirspeedOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAirspeedOff, Name = "AP_AIRSPEED_OFF", Units = "N/A", Description = "Turns airspeed hold off" }; } }
        private SimConnectEvent ApAirspeedOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAirspeedOn, Name = "AP_AIRSPEED_ON", Units = "N/A", Description = "Turns airspeed hold on" }; } }
        private SimConnectEvent ApAirspeedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAirspeedSet, Name = "AP_AIRSPEED_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Sets airspeed hold on/off (1,0)" }; } }
        private SimConnectEvent ApAltHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltHold, Name = "AP_ALT_HOLD", Units = "N/A", Description = "Toggles altitude hold mode" }; } }
        private SimConnectEvent ApAltHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltHoldOff, Name = "AP_ALT_HOLD_OFF", Units = "N/A", Description = "Turns off altitude hold mode" }; } }
        private SimConnectEvent ApAltHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltHoldOn, Name = "AP_ALT_HOLD_ON", Units = "N/A", Description = "Turns altitude hold mode on" }; } }
        private SimConnectEvent ApAltRadioModeOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltRadioModeOff, Name = "AP_ALT_RADIO_MODE_OFF", Units = "N/A", Description = "Deactivate autopilot radio altitude mode." }; } }
        private SimConnectEvent ApAltRadioModeOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltRadioModeOn, Name = "AP_ALT_RADIO_MODE_ON", Units = "N/A", Description = "Activate autopilot radio altitude mode." }; } }
        private SimConnectEvent ApAltRadioModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltRadioModeSet, Name = "AP_ALT_RADIO_MODE_SET", Units = "[0]: bool", Description = "Set autopilot radio altitude mode." }; } }
        private SimConnectEvent ApAltRadioModeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltRadioModeToggle, Name = "AP_ALT_RADIO_MODE_TOGGLE", Units = "N/A", Description = "Toggle autopilot radio altitude mode." }; } }
        private SimConnectEvent ApAltVarDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltVarDec, Name = "AP_ALT_VAR_DEC", Units = "[0]: New reference altitude\r\n[1]: Index", Description = "Decrements the reference altitude." }; } }
        private SimConnectEvent ApAltVarInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltVarInc, Name = "AP_ALT_VAR_INC", Units = "[0]: New reference altitude\r\n[1]: Index", Description = "Increments the reference altitude." }; } }
        private SimConnectEvent ApAltVarSetEnglish { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltVarSetEnglish, Name = "AP_ALT_VAR_SET_ENGLISH", Units = "[0]: New reference altitude\r\n[1]: Index", Description = "Sets altitude reference in feet" }; } }
        private SimConnectEvent ApAltVarSetMetric { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAltVarSetMetric, Name = "AP_ALT_VAR_SET_METRIC", Units = "[0]: New reference altitude\r\n[1]: Index", Description = "Sets reference altitude in meters" }; } }
        private SimConnectEvent ApAprHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAprHold, Name = "AP_APR_HOLD", Units = "N/A", Description = "Toggles approach hold (localizer and glide-slope)" }; } }
        private SimConnectEvent ApAprHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAprHoldOff, Name = "AP_APR_HOLD_OFF", Units = "N/A", Description = "Turns off approach hold mode" }; } }
        private SimConnectEvent ApAprHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAprHoldOn, Name = "AP_APR_HOLD_ON", Units = "N/A", Description = "Turns both AP localizer and glide-slope modes on/armed" }; } }
        private SimConnectEvent ApAttHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAttHold, Name = "AP_ATT_HOLD", Units = "N/A", Description = "Toggle attitude hold mode" }; } }
        private SimConnectEvent ApAttHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAttHoldOff, Name = "AP_ATT_HOLD_OFF", Units = "N/A", Description = "Turns off attitude hold mode" }; } }
        private SimConnectEvent ApAttHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAttHoldOn, Name = "AP_ATT_HOLD_ON", Units = "N/A", Description = "Turns on AP wing leveler and pitch hold mode" }; } }
        private SimConnectEvent ApAvionicsManagedOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAvionicsManagedOff, Name = "AP_AVIONICS_MANAGED_OFF", Units = "N/A", Description = "Turn off the Managed Avionics mode. This is linked to the SimVar AUTOPILOT AVIONICS MANAGED." }; } }
        private SimConnectEvent ApAvionicsManagedOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAvionicsManagedOn, Name = "AP_AVIONICS_MANAGED_ON", Units = "N/A", Description = "Turn on the Managed Avionics mode. This is linked to the SimVar AUTOPILOT AVIONICS MANAGED." }; } }
        private SimConnectEvent ApAvionicsManagedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAvionicsManagedSet, Name = "AP_AVIONICS_MANAGED_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Set the autopilot managed avionics mode (TRUE for on, FALSE for off)." }; } }
        private SimConnectEvent ApAvionicsManagedToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApAvionicsManagedToggle, Name = "AP_AVIONICS_MANAGED_TOGGLE", Units = "N/A", Description = "Toggle on/off the avionics managed mode on the autopilot." }; } }
        private SimConnectEvent ApBankHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApBankHold, Name = "AP_BANK_HOLD", Units = "N/A", Description = "Toggles the autopilot bank hold mode on / off." }; } }
        private SimConnectEvent ApBankHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApBankHoldOff, Name = "AP_BANK_HOLD_OFF", Units = "N/A", Description = "Turns off the autopilot bank hold mode." }; } }
        private SimConnectEvent ApBankHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApBankHoldOn, Name = "AP_BANK_HOLD_ON", Units = "N/A", Description = "Turns on the autopilot bank hold mode." }; } }
        private SimConnectEvent ApBcHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApBcHold, Name = "AP_BC_HOLD", Units = "N/A", Description = "Toggles the backcourse mode for the localizer hold" }; } }
        private SimConnectEvent ApBcHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApBcHoldOff, Name = "AP_BC_HOLD_OFF", Units = "N/A", Description = "Turns off backcourse mode for localizer hold" }; } }
        private SimConnectEvent ApBcHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApBcHoldOn, Name = "AP_BC_HOLD_ON", Units = "N/A", Description = "Turns localizer back course hold mode on/armed" }; } }
        private SimConnectEvent ApHdgHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApHdgHold, Name = "AP_HDG_HOLD", Units = "N/A", Description = "Toggles heading hold mode" }; } }
        private SimConnectEvent ApHdgHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApHdgHoldOff, Name = "AP_HDG_HOLD_OFF", Units = "N/A", Description = "Turns off heading hold mode" }; } }
        private SimConnectEvent ApHdgHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApHdgHoldOn, Name = "AP_HDG_HOLD_ON", Units = "N/A", Description = "Turns heading hold mode on" }; } }
        private SimConnectEvent ApHeadingBugSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ApHeadingBugSetEx1, Name = "AP_HEADING_BUG_SET_EX1", Units = "[0]: Value between 0 and 16384\r\n[1]: Index", Description = "Set the heading hold reference bug. This is the same as the HEADING_BUG_SET event only it permits a much greater degree of precision by permitting the input of a larger integer value that is then transformed by the simulation into a floating point heading\r\ninput value: 477\r\nprocess: 477 * (360/16384)\r\nactual value: 10.504º" }; } }
        private SimConnectEvent ApLocHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApLocHold, Name = "AP_LOC_HOLD", Units = "N/A", Description = "Toggles localizer (only) hold mode" }; } }
        private SimConnectEvent ApLocHoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApLocHoldOff, Name = "AP_LOC_HOLD_OFF", Units = "N/A", Description = "Turns off localizer hold mode" }; } }
        private SimConnectEvent ApLocHoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApLocHoldOn, Name = "AP_LOC_HOLD_ON", Units = "N/A", Description = "Turns AP localizer hold on/armed and glide-slope hold mode off" }; } }
        private SimConnectEvent ApMachHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachHold, Name = "AP_MACH_HOLD", Units = "N/A", Description = "Toggles mach hold" }; } }
        private SimConnectEvent ApMachOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachOff, Name = "AP_MACH_OFF", Units = "N/A", Description = "Turns mach hold off" }; } }
        private SimConnectEvent ApMachOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachOn, Name = "AP_MACH_ON", Units = "N/A", Description = "Turns mach hold on" }; } }
        private SimConnectEvent ApMachSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachSet, Name = "AP_MACH_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Sets mach hold on/off (1,0)" }; } }
        private SimConnectEvent ApMachVarDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachVarDec, Name = "AP_MACH_VAR_DEC", Units = "[0]: the Index of the engine to target (1 - 4)", Description = "Decrements the reference mach by the amount set in the systems.cfg using the mach_increment parameter.\r\nNOTE: along with the AP_MACH_VAR_DEC, AP_MACH_VAR_SET, and AP_MACH_VAR_SET_EX1 keys, this value is clamped to to the values given in the systems.cfg by the parameters min_Mach_ref and max_Mach_ref (or their default values if not set)." }; } }
        private SimConnectEvent ApMachVarInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachVarInc, Name = "AP_MACH_VAR_INC", Units = "[0]: Index of the engine to target (1 - 4)", Description = "Increments the reference mach by the amount set in the systems.cfg using the mach_increment parameter.\r\nNOTE: along with the AP_MACH_VAR_INC, AP_MACH_VAR_SET, and AP_MACH_VAR_SET_EX1 keys, this value is clamped to to the values given in the systems.cfg by the parameters min_Mach_ref and max_Mach_ref (or their default values if not set)." }; } }
        private SimConnectEvent ApMachVarSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachVarSet, Name = "AP_MACH_VAR_SET", Units = "[0]: Integer mach value / 100 (eg: 100 as value results as mach 1)\r\n[1]: Index of the engine to target (1 - 4)", Description = "Sets the mach reference.\r\nNOTE: along with the AP_MACH_VAR_DEC, AP_MACH_VAR_INC, and AP_MACH_VAR_SET_EX1 keys, this value is clamped to to the values given in the systems.cfg by the parameters min_Mach_ref and max_Mach_ref (or their default values if not set)." }; } }
        private SimConnectEvent ApMachVarSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMachVarSetEx1, Name = "AP_MACH_VAR_SET_EX1", Units = "[0]: Integer mach value \\ 1000000 (eg: 1000 as value results as mach 0.001)\r\n[1]: Index of the engine to target (1 - 4)", Description = "Sets the mach reference using a precise value.\r\nNOTE: along with the AP_MACH_VAR_DEC, AP_MACH_VAR_INC, and AP_MACH_VAR_SET keys, this value is clamped to to the values given in the systems.cfg by the parameters min_Mach_ref and max_Mach_ref (or their default values if not set)." }; } }
        private SimConnectEvent ApManagedSpeedInMachOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApManagedSpeedInMachOff, Name = "AP_MANAGED_SPEED_IN_MACH_OFF", Units = "N/A", Description = "Turns off the use of the mach value to compute airspeed used by AP." }; } }
        private SimConnectEvent ApManagedSpeedInMachOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApManagedSpeedInMachOn, Name = "AP_MANAGED_SPEED_IN_MACH_ON", Units = "N/A", Description = "Turns on the use of the mach value to compute airspeed used by AP." }; } }
        private SimConnectEvent ApManagedSpeedInMachSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApManagedSpeedInMachSet, Name = "AP_MANAGED_SPEED_IN_MACH_SET", Units = "[0]: use TRUE/FALSE to enabled/disable", Description = "Sets the use of the mach value to compute airspeed used by AP." }; } }
        private SimConnectEvent ApManagedSpeedInMachToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApManagedSpeedInMachToggle, Name = "AP_MANAGED_SPEED_IN_MACH_TOGGLE", Units = "N/A", Description = "Toggle the use of the mach value to compute airspeed used by AP." }; } }
        private SimConnectEvent ApMaster { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMaster, Name = "AP_MASTER", Units = "N/A", Description = "Toggles AP on/off" }; } }
        private SimConnectEvent ApMasterAlt { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMasterAlt, Name = "AP_MASTER_ALT", Units = "N/A", Description = "No longer used in the simulation.\r\nUse AP_MASTER instead." }; } }
        private SimConnectEvent ApMaxBankAngleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMaxBankAngleSet, Name = "AP_MAX_BANK_ANGLE_SET", Units = "[0]: angle", Description = "Set the bank angle for the aircraft and override the predefined max bank angle of the aircraft." }; } }
        private SimConnectEvent ApMaxBankDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMaxBankDec, Name = "AP_MAX_BANK_DEC", Units = "N/A", Description = "Decrement the AP max bank angle index.\r\nNote that if there is only one index possible then using this event will do nothing. However if there are 2 or more available indices, this event will decrease the index by 1, and when the number passes 0, it will loop back around again to the max index -\r\nNOTE: for further information on indices, please see the AP_MAX_BANK_SET event." }; } }
        private SimConnectEvent ApMaxBankInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMaxBankInc, Name = "AP_MAX_BANK_INC", Units = "N/A", Description = "Increment the AP max bank angle index.\r\nNote that if there is only one index possible then using this event will do nothing. However if there are 2 or more available indices, this event will increase the index by 1, and when the number passes the maximum available indices - 1, it will loop back\r\nNOTE: for further information on indices, please see the AP_MAX_BANK_SET event." }; } }
        private SimConnectEvent ApMaxBankSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMaxBankSet, Name = "AP_MAX_BANK_SET", Units = "[0]: the index to use for max bank angle.", Description = "Sets the autopilot max bank angle index to the parameter [0] value, where the value is clamped between 0 and the number of available indices. The indices correspond to the number of values set for the max_bank table, plus index 0 which corresponds to the \r\nTo give an example, if the max_bank table has 2 values and auto_max_bank is enabled, then the indices for this event would be:\r\n0: use auto banking\r\n1: use the first value in the max_bank table\r\n2: use the second value in the max_bank table.\r\nIf auto banking is not enabled, then setting this to 0 will have no effect." }; } }
        private SimConnectEvent ApMaxBankVelocitySet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApMaxBankVelocitySet, Name = "AP_MAX_BANK_VELOCITY_SET", Units = "[0]: Velocity", Description = "Set the bank velocity of the aircraft and overrides the predifined maximum bank velocity of the aircraft." }; } }
        private SimConnectEvent ApN1Hold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApN1Hold, Name = "AP_N1_HOLD", Units = "N/A", Description = "Autopilot, hold the N1 percentage at its current level." }; } }
        private SimConnectEvent ApN1RefDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ApN1RefDec, Name = "AP_N1_REF_DEC", Units = "N/A", Description = "Decrement the autopilot N1 reference." }; } }
        private SimConnectEvent ApN1RefInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ApN1RefInc, Name = "AP_N1_REF_INC", Units = "N/A", Description = "Increment the autopilot N1 reference." }; } }
        private SimConnectEvent ApN1RefSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApN1RefSet, Name = "AP_N1_REF_SET", Units = "[0]: Integer N1 reference value\r\n[1]: Index of the engine to target (1 - 4)", Description = "Sets the autopilot N1 reference." }; } }
        private SimConnectEvent ApNavSelectSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApNavSelectSet, Name = "AP_NAV_SELECT_SET", Units = "[0]: the NAVindex to use", Description = "Sets the NAV (1 or 2) which is used by the Nav hold modes" }; } }
        private SimConnectEvent ApNav1Hold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApNav1Hold, Name = "AP_NAV1_HOLD", Units = "N/A", Description = "Toggles the nav hold mode" }; } }
        private SimConnectEvent ApNav1HoldOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApNav1HoldOff, Name = "AP_NAV1_HOLD_OFF", Units = "N/A", Description = "Turns off nav hold mode" }; } }
        private SimConnectEvent ApNav1HoldOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApNav1HoldOn, Name = "AP_NAV1_HOLD_ON", Units = "N/A", Description = "Turns lateral hold mode on" }; } }
        private SimConnectEvent ApPanelAltitudeHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelAltitudeHold, Name = "AP_PANEL_ALTITUDE_HOLD", Units = "N/A", Description = "Toggles altitude hold mode on/off" }; } }
        private SimConnectEvent ApPanelAltitudeOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelAltitudeOff, Name = "AP_PANEL_ALTITUDE_OFF", Units = "N/A", Description = "Turns altitude hold mode off" }; } }
        private SimConnectEvent ApPanelAltitudeOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelAltitudeOn, Name = "AP_PANEL_ALTITUDE_ON", Units = "N/A", Description = "Turns altitude hold mode on (without capturing current altitude)" }; } }
        private SimConnectEvent ApPanelAltitudeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelAltitudeSet, Name = "AP_PANEL_ALTITUDE_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Sets altitude hold mode on/off (1,0)" }; } }
        private SimConnectEvent ApPanelHeadingHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelHeadingHold, Name = "AP_PANEL_HEADING_HOLD", Units = "N/A", Description = "Toggles heading hold mode on/off" }; } }
        private SimConnectEvent ApPanelHeadingOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelHeadingOff, Name = "AP_PANEL_HEADING_OFF", Units = "N/A", Description = "Turns heading mode off" }; } }
        private SimConnectEvent ApPanelHeadingOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelHeadingOn, Name = "AP_PANEL_HEADING_ON", Units = "N/A", Description = "Turns heading mode on (without capturing current heading)" }; } }
        private SimConnectEvent ApPanelHeadingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelHeadingSet, Name = "AP_PANEL_HEADING_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Set heading mode on/off (1,0)" }; } }
        private SimConnectEvent ApPanelMachHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelMachHold, Name = "AP_PANEL_MACH_HOLD", Units = "N/A", Description = "Toggles mach hold" }; } }
        private SimConnectEvent ApPanelMachHoldToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelMachHoldToggle, Name = "AP_PANEL_MACH_HOLD_TOGGLE", Units = "N/A", Description = "Sets mach hold reference to current mach" }; } }
        private SimConnectEvent ApPanelMachOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelMachOff, Name = "AP_PANEL_MACH_OFF", Units = "N/A", Description = "Turns off mach hold" }; } }
        private SimConnectEvent ApPanelMachOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelMachOn, Name = "AP_PANEL_MACH_ON", Units = "N/A", Description = "Turns on mach hold" }; } }
        private SimConnectEvent ApPanelMachSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelMachSet, Name = "AP_PANEL_MACH_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Sets mach hold on/off (1,0)" }; } }
        private SimConnectEvent ApPanelSpeedHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelSpeedHold, Name = "AP_PANEL_SPEED_HOLD", Units = "N/A", Description = "Toggles airspeed hold mode" }; } }
        private SimConnectEvent ApPanelSpeedHoldToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelSpeedHoldToggle, Name = "AP_PANEL_SPEED_HOLD_TOGGLE", Units = "N/A", Description = "Turns airspeed hold mode on with current airspeed" }; } }
        private SimConnectEvent ApPanelSpeedOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelSpeedOff, Name = "AP_PANEL_SPEED_OFF", Units = "N/A", Description = "Turns off speed hold mode" }; } }
        private SimConnectEvent ApPanelSpeedOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelSpeedOn, Name = "AP_PANEL_SPEED_ON", Units = "N/A", Description = "Turns on speed hold mode" }; } }
        private SimConnectEvent ApPanelSpeedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelSpeedSet, Name = "AP_PANEL_SPEED_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Set speed hold mode on/off (1,0)" }; } }
        private SimConnectEvent ApPanelVsHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelVsHold, Name = "AP_PANEL_VS_HOLD", Units = "N/A", Description = "Toggles the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApPanelVsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelVsOff, Name = "AP_PANEL_VS_OFF", Units = "N/A", Description = "Turn off the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApPanelVsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelVsOn, Name = "AP_PANEL_VS_ON", Units = "N/A", Description = "Turn on the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApPanelVsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPanelVsSet, Name = "AP_PANEL_VS_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Enables or diables the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApPitchLeveler { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchLeveler, Name = "AP_PITCH_LEVELER", Units = "N/A", Description = "Toggles the AP mode that maintains the pitch and sets the pitch reference to 0°." }; } }
        private SimConnectEvent ApPitchLevelerOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchLevelerOff, Name = "AP_PITCH_LEVELER_OFF", Units = "N/A", Description = "Turns off the AP mode that maintains the pitch and sets the pitch reference to 0°." }; } }
        private SimConnectEvent ApPitchLevelerOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchLevelerOn, Name = "AP_PITCH_LEVELER_ON", Units = "N/A", Description = "Turns on the AP mode that maintains the pitch and sets the pitch reference to 0°." }; } }
        private SimConnectEvent ApPitchRefIncDn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchRefIncDn, Name = "AP_PITCH_REF_INC_DN", Units = "N/A", Description = "Decrements the pitch reference for pitch hold mode" }; } }
        private SimConnectEvent ApPitchRefIncUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchRefIncUp, Name = "AP_PITCH_REF_INC_UP", Units = "N/A", Description = "Increments the pitch reference for pitch hold mode" }; } }
        private SimConnectEvent ApPitchRefSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchRefSelect, Name = "AP_PITCH_REF_SELECT", Units = "N/A", Description = "Selects pitch reference for use with +/-" }; } }
        private SimConnectEvent ApPitchRefSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApPitchRefSet, Name = "AP_PITCH_REF_SET", Units = "[0]: pitch value between -16384 and 16384", Description = "Sets the pitch reference value that will be maintained by the AP pitch hold mode. The pitch value supplied as parameter [0] will be divided by 16384 before being multiplied by the max_pitch value." }; } }
        private SimConnectEvent ApRpmSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApRpmSlotIndexSet, Name = "AP_RPM_SLOT_INDEX_SET", Units = "[0]: slot index from 1 to 4", Description = "Sets the managed index for the RPM hold mode." }; } }
        private SimConnectEvent ApSpdVarDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ApSpdVarDec, Name = "AP_SPD_VAR_DEC", Units = "N/A", Description = "Decrements airspeed hold reference" }; } }
        private SimConnectEvent ApSpdVarInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ApSpdVarInc, Name = "AP_SPD_VAR_INC", Units = "N/A", Description = "Increments airspeed hold reference" }; } }
        private SimConnectEvent ApSpdVarSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApSpdVarSet, Name = "AP_SPD_VAR_SET", Units = "[0]: value in Knots.\r\n[1]: the managed index, from 1 to 4, or 0.", Description = "Sets airspeed reference in knots" }; } }
        private SimConnectEvent ApSpdVarSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ApSpdVarSetEx1, Name = "AP_SPD_VAR_SET_EX1", Units = "[0]: value in Knots.\r\n[1]: the managed index, from 1 to 4, or 0.", Description = "Set the airspeed reference, in knots, for the maintain speed AP mode. The speed supplied as parameter [0] will be divided by 100 to give you more precision with the value, for example: giving 55050 will result in an airspeed hold of 550.50 knots. For para" }; } }
        private SimConnectEvent ApSpeedSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApSpeedSlotIndexSet, Name = "AP_SPEED_SLOT_INDEX_SET", Units = "[0]: slot index from 1 to 4", Description = "Sets the managed index for the speed hold mode." }; } }
        private SimConnectEvent ApVsHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsHold, Name = "AP_VS_HOLD", Units = "N/A", Description = "Toggles the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApVsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsOff, Name = "AP_VS_OFF", Units = "N/A", Description = "Turn off the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApVsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsOn, Name = "AP_VS_ON", Units = "N/A", Description = "Turn on the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApVsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsSet, Name = "AP_VS_SET", Units = "[0]: TRUE/FALSE to enable/disable", Description = "Sets the AP mode that maintains a vertical speed." }; } }
        private SimConnectEvent ApVsSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsSlotIndexSet, Name = "AP_VS_SLOT_INDEX_SET", Units = "[0]: slot index from 1 to 4", Description = "Sets the managed index for the vertical speed hold mode." }; } }
        private SimConnectEvent ApVsVarDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsVarDec, Name = "AP_VS_VAR_DEC", Units = "N/A", Description = "Decrements vertical speed reference" }; } }
        private SimConnectEvent ApVsVarInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsVarInc, Name = "AP_VS_VAR_INC", Units = "N/A", Description = "Increments vertical speed reference" }; } }
        private SimConnectEvent ApVsVarSetCurrent { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsVarSetCurrent, Name = "AP_VS_VAR_SET_CURRENT", Units = "N/A", Description = "Sets the current managed index vertical speed reference to be the current vertical speed (the current index can be set using AP_VS_SLOT_INDEX_SET). If no vertical speed indicator is on the aircraft, the vertical speed reference will calculated based on th" }; } }
        private SimConnectEvent ApVsVarSetEnglish { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsVarSetEnglish, Name = "AP_VS_VAR_SET_ENGLISH", Units = "[0]: New VS reference\r\n[1]: Index", Description = "Sets reference vertical speed in feet per minute" }; } }
        private SimConnectEvent ApVsVarSetMetric { get { return new SimConnectEvent() { Id = SimConnectEventId.ApVsVarSetMetric, Name = "AP_VS_VAR_SET_METRIC", Units = "[0]: New VS reference\r\n[1]: Index", Description = "Sets vertical speed reference in meters per minute" }; } }
        private SimConnectEvent ApWingLeveler { get { return new SimConnectEvent() { Id = SimConnectEventId.ApWingLeveler, Name = "AP_WING_LEVELER", Units = "N/A", Description = "Toggles wing leveler mode" }; } }
        private SimConnectEvent ApWingLevelerOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ApWingLevelerOff, Name = "AP_WING_LEVELER_OFF", Units = "N/A", Description = "Turns off wing leveler mode" }; } }
        private SimConnectEvent ApWingLevelerOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ApWingLevelerOn, Name = "AP_WING_LEVELER_ON", Units = "N/A", Description = "Turns wing leveler mode on" }; } }
        private SimConnectEvent ApuBleedAirSourceSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuBleedAirSourceSet, Name = "APU_BLEED_AIR_SOURCE_SET", Units = "[0]: bool", Description = "Sets if the APU is the source of the bleed air system (true) or not (false)." }; } }
        private SimConnectEvent ApuBleedAirSourceToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuBleedAirSourceToggle, Name = "APU_BLEED_AIR_SOURCE_TOGGLE", Units = "N/A", Description = "Toggle if the APU is the source of the bleed air system or not." }; } }
        private SimConnectEvent ApuExtinguishFire { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuExtinguishFire, Name = "APU_EXTINGUISH_FIRE", Units = "[0]: fire extinguisher index", Description = "Extinguish fire on APU using the indexed fire extinguisher." }; } }
        private SimConnectEvent ApuGeneratorSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuGeneratorSwitchSet, Name = "APU_GENERATOR_SWITCH_SET", Units = "[0]: New switch value (0, or 1)\r\n[1]: index of the APU switch", Description = "Set the auxiliary generator switch (0,1)." }; } }
        private SimConnectEvent ApuGeneratorSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuGeneratorSwitchToggle, Name = "APU_GENERATOR_SWITCH_TOGGLE", Units = "N/A", Description = "Turn the auxiliary generator on or off." }; } }
        private SimConnectEvent ApuOffSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuOffSwitch, Name = "APU_OFF_SWITCH", Units = "N/A", Description = "Turn the APU off." }; } }
        private SimConnectEvent ApuStarter { get { return new SimConnectEvent() { Id = SimConnectEventId.ApuStarter, Name = "APU_STARTER", Units = "N/A", Description = "Start up the auxiliary power unit (APU)." }; } }
        private SimConnectEvent Atc { get { return new SimConnectEvent() { Id = SimConnectEventId.Atc, Name = "ATC", Units = "N/A", Description = "Activates the ATC window." }; } }
        private SimConnectEvent AtcMenu0 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu0, Name = "ATC_MENU_0", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu1 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu1, Name = "ATC_MENU_1", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu2 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu2, Name = "ATC_MENU_2", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu3 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu3, Name = "ATC_MENU_3", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu4 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu4, Name = "ATC_MENU_4", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu5 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu5, Name = "ATC_MENU_5", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu6 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu6, Name = "ATC_MENU_6", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu7 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu7, Name = "ATC_MENU_7", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu8 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu8, Name = "ATC_MENU_8", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenu9 { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenu9, Name = "ATC_MENU_9", Units = "N/A", Description = "Selects ATC option 0 - 9." }; } }
        private SimConnectEvent AtcMenuClose { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenuClose, Name = "ATC_MENU_CLOSE", Units = "N/A", Description = "Closes the ATC menu screen." }; } }
        private SimConnectEvent AtcMenuOpen { get { return new SimConnectEvent() { Id = SimConnectEventId.AtcMenuOpen, Name = "ATC_MENU_OPEN", Units = "N/A", Description = "Opens the ATC menu screen." }; } }
        private SimConnectEvent AttitudeBarsPositionDown { get { return new SimConnectEvent() { Id = SimConnectEventId.AttitudeBarsPositionDown, Name = "ATTITUDE_BARS_POSITION_DOWN", Units = "N/A", Description = "Decrements the attitude indicator pitch reference bars. If you hold down the key for more than 2 seconds it will decrement by 30, if you hold down the key for more than 1 second it will decrement by 10, otherwise it will decrement by 5." }; } }
        private SimConnectEvent AttitudeBarsPositionSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AttitudeBarsPositionSet, Name = "ATTITUDE_BARS_POSITION_SET", Units = "[0]: Index\r\n[1]: Value", Description = "Sets attitude indicator pitch reference bars. Input value can be between -100 and 100." }; } }
        private SimConnectEvent AttitudeBarsPositionUp { get { return new SimConnectEvent() { Id = SimConnectEventId.AttitudeBarsPositionUp, Name = "ATTITUDE_BARS_POSITION_UP", Units = "N/A", Description = "Increments the attitude indicator pitch reference bars. If you hold down the key for more than 2 seconds it will increment by 30, if you hold down the key for more than 1 second it will increment by 10, otherwise it will increment by 5." }; } }
        private SimConnectEvent AttitudeCageButton { get { return new SimConnectEvent() { Id = SimConnectEventId.AttitudeCageButton, Name = "ATTITUDE_CAGE_BUTTON", Units = "N/A", Description = "Cages attitude indicator at 0 pitch and bank." }; } }
        private SimConnectEvent AudioPanelVolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.AudioPanelVolumeDec, Name = "AUDIO_PANEL_VOLUME_DEC", Units = "N/A", Description = "Decreases the audio panel sound volume by 0.01. The resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent AudioPanelVolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.AudioPanelVolumeInc, Name = "AUDIO_PANEL_VOLUME_INC", Units = "N/A", Description = "Increases the audio panel sound volume by 0.01. The resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent AudioPanelVolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AudioPanelVolumeSet, Name = "AUDIO_PANEL_VOLUME_SET", Units = "[0]: Volume", Description = "Set the audio panel volume to the given value, clamped between 0 and 1." }; } }
        private SimConnectEvent AutoHoverOff { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoHoverOff, Name = "AUTO_HOVER_OFF", Units = "N/A", Description = "Disable the auto-hover function of the helicopter, if one is available.\r\nNOTE: This is currently not implemented in the Simulation" }; } }
        private SimConnectEvent AutoHoverOn { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoHoverOn, Name = "AUTO_HOVER_ON", Units = "N/A", Description = "Enable the auto-hover function of the helicopter - if one is available.\r\nNOTE: This is currently not implemented in the Simulation" }; } }
        private SimConnectEvent AutoHoverSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoHoverSet, Name = "AUTO_HOVER_SET", Units = "[0]: True/False (1, 0)", Description = "Set the auto-hover - if available - to either on (True, 1) or off (False, 0).\r\nNOTE: This is currently not implemented in the Simulation" }; } }
        private SimConnectEvent AutoHoverToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoHoverToggle, Name = "AUTO_HOVER_TOGGLE", Units = "N/A", Description = "Toggle the auto-hover - if available - between on (True, 1) and off (False, 0).\r\nNOTE: This is currently not implemented in the Simulation" }; } }
        private SimConnectEvent AutoThrottleArm { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoThrottleArm, Name = "AUTO_THROTTLE_ARM", Units = "N/A", Description = "Toggles autothrottle arming mode" }; } }
        private SimConnectEvent AutoThrottleDisconnect { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoThrottleDisconnect, Name = "AUTO_THROTTLE_DISCONNECT", Units = "[0]: The engine to target. Use 0 to target all engines, or 1 to 4 to target a specific engine.", Description = "Disconnect the AutoThrottle of the AP for one engine or all engines." }; } }
        private SimConnectEvent AutoThrottleToGa { get { return new SimConnectEvent() { Id = SimConnectEventId.AutoThrottleToGa, Name = "AUTO_THROTTLE_TO_GA", Units = "N/A", Description = "Toggles Takeoff/Go Around mode" }; } }
        private SimConnectEvent AutobrakeDisarm { get { return new SimConnectEvent() { Id = SimConnectEventId.AutobrakeDisarm, Name = "AUTOBRAKE_DISARM", Units = "N/A", Description = "Sets the autobrake switch to either :\r\nthe off position (position 1) when the auto_brakes parameter is greater than 0.\r\nthe RTO position (position 0) when the auto_brakes parameter is 0." }; } }
        private SimConnectEvent AutobrakeHiSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutobrakeHiSet, Name = "AUTOBRAKE_HI_SET", Units = "N/A", Description = "Sets the autobrake switch to the maximum position (ie: the auto_brakes number, so that the maximum braking force will be applied)." }; } }
        private SimConnectEvent AutobrakeLoSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutobrakeLoSet, Name = "AUTOBRAKE_LO_SET", Units = "N/A", Description = "Sets the autobrake switch to the minimum position (position 2, the first position after the off position)." }; } }
        private SimConnectEvent AutobrakeMedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutobrakeMedSet, Name = "AUTOBRAKE_MED_SET", Units = "N/A", Description = "Sets the autobrake switch to a medium position (the exact position will depend on the number of autobreaks defined by the auto_brakes parameter)." }; } }
        private SimConnectEvent AutocoordOff { get { return new SimConnectEvent() { Id = SimConnectEventId.AutocoordOff, Name = "AUTOCOORD_OFF", Units = "-", Description = "Not used in the simulation." }; } }
        private SimConnectEvent AutocoordOn { get { return new SimConnectEvent() { Id = SimConnectEventId.AutocoordOn, Name = "AUTOCOORD_ON", Units = "-", Description = "Not used in the simulation." }; } }
        private SimConnectEvent AutocoordSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutocoordSet, Name = "AUTOCOORD_SET", Units = "-", Description = "Not used in the simulation." }; } }
        private SimConnectEvent AutocoordToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AutocoordToggle, Name = "AUTOCOORD_TOGGLE", Units = "N/A", Description = "Switch inversion of Y axis controls on or off. " }; } }
        private SimConnectEvent AutopilotAirspeedAcquire { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotAirspeedAcquire, Name = "AUTOPILOT_AIRSPEED_ACQUIRE", Units = "N/A", Description = "Triggers both the AP_SPD_VAR_SET (with value 0) event and the AP_AIRSPEED_ON event.\r\nNOTE: This is a legacy event and you should be calling the above mentioned events directly." }; } }
        private SimConnectEvent AutopilotDisengageSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotDisengageSet, Name = "AUTOPILOT_DISENGAGE_SET", Units = "[0]: boolean value to enable/disable the disengage value", Description = "Set if the AP should be disengaged or not." }; } }
        private SimConnectEvent AutopilotDisengageToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotDisengageToggle, Name = "AUTOPILOT_DISENGAGE_TOGGLE", Units = "N/A", Description = "Toggle the status of the AP disengage value." }; } }
        private SimConnectEvent AutopilotOff { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotOff, Name = "AUTOPILOT_OFF", Units = "N/A", Description = "Turns AP off" }; } }
        private SimConnectEvent AutopilotOn { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotOn, Name = "AUTOPILOT_ON", Units = "N/A", Description = "Turns AP on" }; } }
        private SimConnectEvent AutopilotPanelAirspeedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotPanelAirspeedSet, Name = "AUTOPILOT_PANEL_AIRSPEED_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent AutopilotPanelCruiseSpeed { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotPanelCruiseSpeed, Name = "AUTOPILOT_PANEL_CRUISE_SPEED", Units = "N/A", Description = "No longer used in the simulation.\r\nPreviously used by the Concorde flight model." }; } }
        private SimConnectEvent AutopilotPanelMaxSpeed { get { return new SimConnectEvent() { Id = SimConnectEventId.AutopilotPanelMaxSpeed, Name = "AUTOPILOT_PANEL_MAX_SPEED", Units = "N/A", Description = "No longer used in the simulation.\r\nPreviously used by the Concorde flight model." }; } }
        private SimConnectEvent AutorudderToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.AutorudderToggle, Name = "AUTORUDDER_TOGGLE", Units = "N/A", Description = "Turn the automatic rudder control feature on or off." }; } }
        private SimConnectEvent AvionicsMaster1Off { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMaster1Off, Name = "AVIONICS_MASTER_1_OFF", Units = "N/A", Description = "Sets avionics master 1 / 2 switch to off (0)." }; } }
        private SimConnectEvent AvionicsMaster1On { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMaster1On, Name = "AVIONICS_MASTER_1_ON", Units = "N/A", Description = "Sets avionics master 1 / 2 switch to on (1)." }; } }
        private SimConnectEvent AvionicsMaster1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMaster1Set, Name = "AVIONICS_MASTER_1_SET", Units = "[0]: Bool", Description = "Sets avionics master 1 / 2 switch to on (1) or off (0)." }; } }
        private SimConnectEvent AvionicsMaster2Off { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMaster2Off, Name = "AVIONICS_MASTER_2_OFF", Units = "N/A", Description = "Sets avionics master 1 / 2 switch to off (0)." }; } }
        private SimConnectEvent AvionicsMaster2On { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMaster2On, Name = "AVIONICS_MASTER_2_ON", Units = "N/A", Description = "Sets avionics master 1 / 2 switch to on (1)." }; } }
        private SimConnectEvent AvionicsMaster2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMaster2Set, Name = "AVIONICS_MASTER_2_SET", Units = "[0]: Bool", Description = "Sets avionics master 1 / 2 switch to on (1) or off (0)." }; } }
        private SimConnectEvent AvionicsMasterSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AvionicsMasterSet, Name = "AVIONICS_MASTER_SET", Units = "[0]: Bool", Description = "Sets the avionics master switch to on or off." }; } }
        private SimConnectEvent AxisAileronsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisAileronsSet, Name = "AXIS_AILERONS_SET", Units = "[0] Position (-16383 to 16384)", Description = "Sets the aileron position." }; } }
        private SimConnectEvent AxisCollectiveSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisCollectiveSet, Name = "AXIS_COLLECTIVE_SET", Units = "[0]: Set the collective (0 to 16384)", Description = "Set the collective pitch angle (a value from 0 to 1 interpolated from the 0 to 16384 input)." }; } }
        private SimConnectEvent AxisConditionLever1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisConditionLever1Set, Name = "AXIS_CONDITION_LEVER_1_SET", Units = "[0]: Axis value", Description = "Sets the condition lever position based on the percentage final value of axis the input where:" }; } }
        private SimConnectEvent AxisConditionLever2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisConditionLever2Set, Name = "AXIS_CONDITION_LEVER_2_SET", Units = "[0]: Axis value", Description = "0% - 33.3% = cutoff" }; } }
        private SimConnectEvent AxisConditionLever3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisConditionLever3Set, Name = "AXIS_CONDITION_LEVER_3_SET", Units = "[0]: Axis value", Description = "33.3% - 66.6% = low idle" }; } }
        private SimConnectEvent AxisConditionLever4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisConditionLever4Set, Name = "AXIS_CONDITION_LEVER_4_SET", Units = "[0]: Axis value", Description = "66.6% - 100% = high" }; } }
        private SimConnectEvent AxisCyclicLateralSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisCyclicLateralSet, Name = "AXIS_CYCLIC_LATERAL_SET", Units = "[0]: Set the lateral cyclic (-16384 to 16384)", Description = "Set the lateral cyclic axis as a value between -16384 and 16384." }; } }
        private SimConnectEvent AxisCyclicLongitudinalSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisCyclicLongitudinalSet, Name = "AXIS_CYCLIC_LONGITUDINAL_SET", Units = "[0]: Set the longitudinal cyclic (-16384 to 16384)", Description = "Set the longitudinal cyclic axis." }; } }
        private SimConnectEvent AxisElevTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisElevTrimSet, Name = "AXIS_ELEV_TRIM_SET", Units = "[0]: Trim position (-16383 to 16384)", Description = "Sets the elevator trim position (input will be normalised to a value between -1 and 1)." }; } }
        private SimConnectEvent AxisElevatorSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisElevatorSet, Name = "AXIS_ELEVATOR_SET", Units = "[0]: Position (-16383 to 16384)", Description = "Sets the elevator position (input will be normalised to a value between -1 and 1). Note that this is simply an alias for the ELEVATOR_SET event." }; } }
        private SimConnectEvent AxisFlapsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisFlapsSet, Name = "AXIS_FLAPS_SET", Units = "N/A", Description = "Sets flap handle to closest increment (-16383 - +16383)" }; } }
        private SimConnectEvent AxisHelicopterThrottleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisHelicopterThrottleSet, Name = "AXIS_HELICOPTER_THROTTLE_SET", Units = "[0]: Set all throttles (0 to 16384)", Description = "Set all throttles to a value from 0 to 1 (interpolated from the 0 to 16384 input)." }; } }
        private SimConnectEvent AxisHelicopterThrottle1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisHelicopterThrottle1Set, Name = "AXIS_HELICOPTER_THROTTLE1_SET", Units = "[0]: Set throttle 1 or 2 (0 to 16384)", Description = "Set the throttle 1 or 2 value from 0 to 1 (interpolated from the 0 to 16384 input)." }; } }
        private SimConnectEvent AxisHelicopterThrottle2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisHelicopterThrottle2Set, Name = "AXIS_HELICOPTER_THROTTLE2_SET", Units = "[0]: Set throttle 1 or 2 (0 to 16384)", Description = "Set the throttle 1 or 2 value from 0 to 1 (interpolated from the 0 to 16384 input)." }; } }
        private SimConnectEvent AxisIndSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisIndSet, Name = "AXIS_IND_SET", Units = "-", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent AxisLeftBrakeLinearSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisLeftBrakeLinearSet, Name = "AXIS_LEFT_BRAKE_LINEAR_SET", Units = "[0]: the brake position from -16383 to 16383", Description = "Sets the left brake position from an axis controller (e.g. joystick) to the value given as the parameter [0], from -16383 (0 braking) to +16383 (maximum braking). Note that this is on a linear scale:\r\nFALSE\r\n0 = 50%\r\nFALSE\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent AxisLeftBrakeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisLeftBrakeSet, Name = "AXIS_LEFT_BRAKE_SET", Units = "[0]: the brake position from -16383 to 16383", Description = "Sets the left brake position from an axis controller (e.g. joystick) to the value given as the parameter [0], from -16383 (0 braking) to +16383 (maximum braking). Note that this is on a non-linear scale:\r\nFALSE\r\nFALSE\r\n0 = 27%\r\nFALSE\r\nFALSE" }; } }
        private SimConnectEvent AxisMixtureSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisMixtureSet, Name = "AXIS_MIXTURE_SET", Units = "[0]: Position", Description = "2 for high" }; } }
        private SimConnectEvent AxisMixture1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisMixture1Set, Name = "AXIS_MIXTURE1_SET", Units = "[0]: Position", Description = "2 for high" }; } }
        private SimConnectEvent AxisMixture2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisMixture2Set, Name = "AXIS_MIXTURE2_SET", Units = "[0]: Position", Description = "2 for high" }; } }
        private SimConnectEvent AxisMixture3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisMixture3Set, Name = "AXIS_MIXTURE3_SET", Units = "[0]: Position", Description = "2 for high" }; } }
        private SimConnectEvent AxisMixture4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisMixture4Set, Name = "AXIS_MIXTURE4_SET", Units = "[0]: Position", Description = "2 for high" }; } }
        private SimConnectEvent AxisPanHeading { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPanHeading, Name = "AXIS_PAN_HEADING", Units = "N/A", Description = "Sets the heading of the axis. Requires an angle." }; } }
        private SimConnectEvent AxisPanPitch { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPanPitch, Name = "AXIS_PAN_PITCH", Units = "N/A", Description = "Sets the pitch of the camera axis. Requires an angle." }; } }
        private SimConnectEvent AxisPanTilt { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPanTilt, Name = "AXIS_PAN_TILT", Units = "N/A", Description = "Sets the tilt of the axis. Requires an angle." }; } }
        private SimConnectEvent AxisPropellerSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPropellerSet, Name = "AXIS_PROPELLER_SET", Units = "N/A", Description = "Set propeller levers exact value (-16383 to +16383)" }; } }
        private SimConnectEvent AxisPropeller1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPropeller1Set, Name = "AXIS_PROPELLER1_SET", Units = "N/A", Description = "Set propeller lever 1/2/3/4 exact value (-16383 to +16383)" }; } }
        private SimConnectEvent AxisPropeller2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPropeller2Set, Name = "AXIS_PROPELLER2_SET", Units = "N/A", Description = "Set propeller lever 1/2/3/4 exact value (-16383 to +16383)" }; } }
        private SimConnectEvent AxisPropeller3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPropeller3Set, Name = "AXIS_PROPELLER3_SET", Units = "N/A", Description = "Set propeller lever 1/2/3/4 exact value (-16383 to +16383)" }; } }
        private SimConnectEvent AxisPropeller4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisPropeller4Set, Name = "AXIS_PROPELLER4_SET", Units = "N/A", Description = "Set propeller lever 1/2/3/4 exact value (-16383 to +16383)" }; } }
        private SimConnectEvent AxisRightBrakeLinearSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisRightBrakeLinearSet, Name = "AXIS_RIGHT_BRAKE_LINEAR_SET", Units = "[0]: the brake position from -16383 to 16383", Description = "Sets the right brake position from an axis controller (e.g. joystick) to the value given as the parameter [0], from -16383 (0 braking) to +16383 (maximum braking). Note that this is on a linear scale:\r\nFALSE\r\n0 = 50%\r\nFALSE\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent AxisRightBrakeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisRightBrakeSet, Name = "AXIS_RIGHT_BRAKE_SET", Units = "[0]: the brake position from -16383 to 16383", Description = "Sets the right brake position from an axis controller (e.g. joystick) to the value given as the parameter [0], from -16383 (0 braking) to +16383 (maximum braking). Note that this is on a non-linear scale:\r\nFALSE\r\nFALSE\r\n0 = 27%\r\nFALSE\r\nFALSE" }; } }
        private SimConnectEvent AxisRotorBrakeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisRotorBrakeSet, Name = "AXIS_ROTOR_BRAKE_SET", Units = "[0]: Throttle value (0 to 16384)", Description = "Set all throttles based on the input value. The input is between 0 and 16384, which will be normalised to a value between 0 and 1." }; } }
        private SimConnectEvent AxisRudderSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisRudderSet, Name = "AXIS_RUDDER_SET", Units = "[0]: Value to set", Description = "Sets rudder position (-16383 - +16383)" }; } }
        private SimConnectEvent AxisSlewAheadSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSlewAheadSet, Name = "AXIS_SLEW_AHEAD_SET", Units = "[0]: Value to set", Description = "Sets forward slew (+/- 16383)" }; } }
        private SimConnectEvent AxisSlewAltSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSlewAltSet, Name = "AXIS_SLEW_ALT_SET", Units = "[0]: Value to set", Description = "Sets vertical slew (+/- 16383)" }; } }
        private SimConnectEvent AxisSlewBankSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSlewBankSet, Name = "AXIS_SLEW_BANK_SET", Units = "[0]: Value to set", Description = "Sets roll slew (+/- 16383)" }; } }
        private SimConnectEvent AxisSlewHeadingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSlewHeadingSet, Name = "AXIS_SLEW_HEADING_SET", Units = "[0]: Value to set", Description = "Sets heading slew (+/- 16383)" }; } }
        private SimConnectEvent AxisSlewPitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSlewPitchSet, Name = "AXIS_SLEW_PITCH_SET", Units = "[0]: Value to set", Description = "Sets pitch slew (+/- 16383)" }; } }
        private SimConnectEvent AxisSlewSidewaysSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSlewSidewaysSet, Name = "AXIS_SLEW_SIDEWAYS_SET", Units = "[0]: Value to set", Description = "Sets sideways slew (+/- 16383)" }; } }
        private SimConnectEvent AxisSpoilerSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSpoilerSet, Name = "AXIS_SPOILER_SET", Units = "[0]: Positon (0 - 1)", Description = "Sets spoiler handle position." }; } }
        private SimConnectEvent AxisSteeringSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSteeringSet, Name = "AXIS_STEERING_SET", Units = "[0]: Steering position (+/-16384)", Description = "Sets the value of the nose wheel steering position. Zero is straight ahead (-16384, far left +16384, far right)." }; } }
        private SimConnectEvent AxisSteeringSetHelicopters { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisSteeringSetHelicopters, Name = "AXIS_STEERING_SET", Units = "[0]: Set the steering amount (-16384 to 16384)", Description = "Set the steering axis value from -1 to 1 (interpolated from the -16384 to 16384 input)." }; } }
        private SimConnectEvent AxisTailRotorSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisTailRotorSet, Name = "AXIS_TAIL_ROTOR_SET", Units = "[0]: Set tail rotor speed (0 to 16384)", Description = "Sets the tail rotor speed as a value from 0 to 1 (interpolated from the 0 to 16384 input)." }; } }
        private SimConnectEvent AxisThrottleMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottleMinus, Name = "AXIS_THROTTLE_MINUS", Units = "[0]: the value between 0 - 16384", Description = "Subtracts the given value from the throttle of all engines (the final position will depend on the min_throttle_limit value)." }; } }
        private SimConnectEvent AxisThrottlePlus { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottlePlus, Name = "AXIS_THROTTLE_PLUS", Units = "[0]: the value between 0 - 16384", Description = "Adds to the throttle of all engines a value comprised between 0 and 16384." }; } }
        private SimConnectEvent AxisThrottleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottleSet, Name = "AXIS_THROTTLE_SET", Units = "[0]: the value between 0 - 16384", Description = "Set throttles (0- 16383)" }; } }
        private SimConnectEvent AxisThrottle1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottle1Set, Name = "AXIS_THROTTLE1_SET", Units = "[0]: the value between 0 - 16384", Description = "Set throttle 1/2/3/4 exactly (0 - 16383)" }; } }
        private SimConnectEvent AxisThrottle2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottle2Set, Name = "AXIS_THROTTLE2_SET", Units = "[0]: the value between 0 - 16384", Description = "Set throttle 1/2/3/4 exactly (0 - 16383)" }; } }
        private SimConnectEvent AxisThrottle3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottle3Set, Name = "AXIS_THROTTLE3_SET", Units = "[0]: the value between 0 - 16384", Description = "Set throttle 1/2/3/4 exactly (0 - 16383)" }; } }
        private SimConnectEvent AxisThrottle4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisThrottle4Set, Name = "AXIS_THROTTLE4_SET", Units = "[0]: the value between 0 - 16384", Description = "Set throttle 1/2/3/4 exactly (0 - 16383)" }; } }
        private SimConnectEvent AxisVerticalSpeedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.AxisVerticalSpeedSet, Name = "AXIS_VERTICAL_SPEED_SET", Units = "[0] Value between 0 and 16000", Description = "Sets zoom level to 1" }; } }
        private SimConnectEvent BackToFly { get { return new SimConnectEvent() { Id = SimConnectEventId.BackToFly, Name = "BACK_TO_FLY", Units = "N/A", Description = "This will raise the aircraft off the ground and into flight, or - if already in flight - it will force the aircraft to gain height." }; } }
        private SimConnectEvent BailOut { get { return new SimConnectEvent() { Id = SimConnectEventId.BailOut, Name = "BAIL_OUT", Units = "-", Description = "Not used by the simulation" }; } }
        private SimConnectEvent Barometric { get { return new SimConnectEvent() { Id = SimConnectEventId.Barometric, Name = "BAROMETRIC", Units = "N/A", Description = "Syncs altimeter setting to sea level pressure, or 29.92 if above 18000 feet." }; } }
        private SimConnectEvent BarometricStdPressure { get { return new SimConnectEvent() { Id = SimConnectEventId.BarometricStdPressure, Name = "BAROMETRIC_STD_PRESSURE", Units = "[0]: the index of the altimeter", Description = "Set the altimeter setting to the pressure at the standard atmospheric level (1013.25 hPa)." }; } }
        private SimConnectEvent Battery1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Battery1Set, Name = "BATTERY1_SET", Units = "[0]: bool", Description = "Set the battery 1/2/3/4 switch to on or off." }; } }
        private SimConnectEvent Battery2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Battery2Set, Name = "BATTERY2_SET", Units = "[0]: bool", Description = "Set the battery 1/2/3/4 switch to on or off." }; } }
        private SimConnectEvent Battery3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Battery3Set, Name = "BATTERY3_SET", Units = "[0]: bool", Description = "Set the battery 1/2/3/4 switch to on or off." }; } }
        private SimConnectEvent Battery4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Battery4Set, Name = "BATTERY4_SET", Units = "[0]: bool", Description = "Set the battery 1/2/3/4 switch to on or off." }; } }
        private SimConnectEvent BeaconLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.BeaconLightsOff, Name = "BEACON_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn beacon lights off" }; } }
        private SimConnectEvent BeaconLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.BeaconLightsOn, Name = "BEACON_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn beacon lights on" }; } }
        private SimConnectEvent BeaconLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BeaconLightsSet, Name = "BEACON_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set beacon lights on/off (1,0)" }; } }
        private SimConnectEvent BleedAirSourceControlDec { get { return new SimConnectEvent() { Id = SimConnectEventId.BleedAirSourceControlDec, Name = "BLEED_AIR_SOURCE_CONTROL_DEC", Units = "N/A", Description = "Decreases the bleed air source control. Order of operation is Engines -> APU -> Off -> Auto." }; } }
        private SimConnectEvent BleedAirSourceControlInc { get { return new SimConnectEvent() { Id = SimConnectEventId.BleedAirSourceControlInc, Name = "BLEED_AIR_SOURCE_CONTROL_INC", Units = "N/A", Description = "Increases the bleed air source control. Order of operation is Auto -> Off -> APU -> Engines." }; } }
        private SimConnectEvent BleedAirSourceControlSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BleedAirSourceControlSet, Name = "BLEED_AIR_SOURCE_CONTROL_SET", Units = "[0]: source value", Description = "Sets the bleed air system source. The input parameter [0] can be one of the following:\r\n0 - auto\r\n1 - off\r\n2 - apu\r\n3 - engines" }; } }
        private SimConnectEvent BombView { get { return new SimConnectEvent() { Id = SimConnectEventId.BombView, Name = "BOMB_VIEW", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent Brakes { get { return new SimConnectEvent() { Id = SimConnectEventId.Brakes, Name = "BRAKES", Units = "N/A", Description = "Increment brake pressure" }; } }
        private SimConnectEvent BrakesLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.BrakesLeft, Name = "BRAKES_LEFT", Units = "N/A", Description = "Increments left brake pressure" }; } }
        private SimConnectEvent BrakesRight { get { return new SimConnectEvent() { Id = SimConnectEventId.BrakesRight, Name = "BRAKES_RIGHT", Units = "N/A", Description = "Increments right brake pressure" }; } }
        private SimConnectEvent BreakerAdfSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAdfSet, Name = "BREAKER_ADF_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the ADF breaker" }; } }
        private SimConnectEvent BreakerAdfToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAdfToggle, Name = "BREAKER_ADF_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the ADF breaker" }; } }
        private SimConnectEvent BreakerAltfldSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAltfldSet, Name = "BREAKER_ALTFLD_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the  " }; } }
        private SimConnectEvent BreakerAltfldToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAltfldToggle, Name = "BREAKER_ALTFLD_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the" }; } }
        private SimConnectEvent BreakerAutopilotSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAutopilotSet, Name = "BREAKER_AUTOPILOT_SET", Units = "[1]: State, either connected (1) or not (0).", Description = "Set the autopilot breaker" }; } }
        private SimConnectEvent BreakerAutopilotToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAutopilotToggle, Name = "BREAKER_AUTOPILOT_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the autopilot breaker" }; } }
        private SimConnectEvent BreakerAvnbus1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAvnbus1Set, Name = "BREAKER_AVNBUS1_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the  " }; } }
        private SimConnectEvent BreakerAvnbus1Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAvnbus1Toggle, Name = "BREAKER_AVNBUS1_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the " }; } }
        private SimConnectEvent BreakerAvnbus2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAvnbus2Set, Name = "BREAKER_AVNBUS2_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the  " }; } }
        private SimConnectEvent BreakerAvnbus2Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAvnbus2Toggle, Name = "BREAKER_AVNBUS2_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the " }; } }
        private SimConnectEvent BreakerAvnfanSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAvnfanSet, Name = "BREAKER_AVNFAN_SET", Units = "[0]: circuit.N index", Description = "Set the fan breaker" }; } }
        private SimConnectEvent BreakerAvnfanToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerAvnfanToggle, Name = "BREAKER_AVNFAN_TOGGLE", Units = "[0]: circuit.N index", Description = "Toggle the Fan breaker" }; } }
        private SimConnectEvent BreakerFlapSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerFlapSet, Name = "BREAKER_FLAP_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the flaps breaker" }; } }
        private SimConnectEvent BreakerFlapToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerFlapToggle, Name = "BREAKER_FLAP_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the flaps breaker" }; } }
        private SimConnectEvent BreakerGpsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerGpsSet, Name = "BREAKER_GPS_SET", Units = "[2]: Source bus.N Index", Description = "Set the GPS breaker" }; } }
        private SimConnectEvent BreakerGpsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerGpsToggle, Name = "BREAKER_GPS_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the GPS breaker" }; } }
        private SimConnectEvent BreakerInstSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerInstSet, Name = "BREAKER_INST_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the instrument breaker" }; } }
        private SimConnectEvent BreakerInstToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerInstToggle, Name = "BREAKER_INST_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the instruments breaker" }; } }
        private SimConnectEvent BreakerInstltsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerInstltsSet, Name = "BREAKER_INSTLTS_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the  " }; } }
        private SimConnectEvent BreakerInstltsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerInstltsToggle, Name = "BREAKER_INSTLTS_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the" }; } }
        private SimConnectEvent BreakerNavcom1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerNavcom1Set, Name = "BREAKER_NAVCOM1_SET", Units = "[2]: Source bus.N Index", Description = "Set the NAV/COM 1 breaker" }; } }
        private SimConnectEvent BreakerNavcom1Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerNavcom1Toggle, Name = "BREAKER_NAVCOM1_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the NAV / COM 1 breaker" }; } }
        private SimConnectEvent BreakerNavcom2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerNavcom2Set, Name = "BREAKER_NAVCOM2_SET", Units = "For legacy aircraft:", Description = "Set the NAV/COM 2 breaker" }; } }
        private SimConnectEvent BreakerNavcom2Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerNavcom2Toggle, Name = "BREAKER_NAVCOM2_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the NAV / COM 2 breaker" }; } }
        private SimConnectEvent BreakerNavcom3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerNavcom3Set, Name = "BREAKER_NAVCOM3_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the NAV/COM 3 breaker" }; } }
        private SimConnectEvent BreakerNavcom3Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerNavcom3Toggle, Name = "BREAKER_NAVCOM3_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the NAV / COM 3 breaker" }; } }
        private SimConnectEvent BreakerTurncoordSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerTurncoordSet, Name = "BREAKER_TURNCOORD_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the  " }; } }
        private SimConnectEvent BreakerTurncoordToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerTurncoordToggle, Name = "BREAKER_TURNCOORD_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the" }; } }
        private SimConnectEvent BreakerWarnSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerWarnSet, Name = "BREAKER_WARN_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the warning breaker" }; } }
        private SimConnectEvent BreakerWarnToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerWarnToggle, Name = "BREAKER_WARN_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the warning breaker" }; } }
        private SimConnectEvent BreakerXpndrSet { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerXpndrSet, Name = "BREAKER_XPNDR_SET", Units = "[0]: State, either connected (1) or not (0).", Description = "Set the transponder breaker" }; } }
        private SimConnectEvent BreakerXpndrToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.BreakerXpndrToggle, Name = "BREAKER_XPNDR_TOGGLE", Units = "[1]: Source bus.N Index", Description = "Toggle the transponder breaker" }; } }
        private SimConnectEvent CabinLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.CabinLightsOff, Name = "CABIN_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn cabin lights off" }; } }
        private SimConnectEvent CabinLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.CabinLightsOn, Name = "CABIN_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn cabin lights on" }; } }
        private SimConnectEvent CabinLightsPowerSettingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.CabinLightsPowerSettingSet, Name = "CABIN_LIGHTS_POWER_SETTING_SET", Units = "[0]: cabin light circuit index\r\n[1]: power setting (%)", Description = "Set cabin light circuit power setting. Takes two indices, the circuit and the power setting (see SimVars And Keys for more information). The index is the value assigned to the circuit Type when the circuit.N was defined." }; } }
        private SimConnectEvent CabinLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.CabinLightsSet, Name = "CABIN_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set cabin lights on/off (1,0)" }; } }
        private SimConnectEvent CabinNoSmokingAlertSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.CabinNoSmokingAlertSwitchToggle, Name = "CABIN_NO_SMOKING_ALERT_SWITCH_TOGGLE", Units = "N/A", Description = "Turn the \"No smoking\" alert on or off." }; } }
        private SimConnectEvent CabinSeatbeltsAlertSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.CabinSeatbeltsAlertSwitchToggle, Name = "CABIN_SEATBELTS_ALERT_SWITCH_TOGGLE", Units = "N/A", Description = "Turn the \"Fasten seatbelts\" alert on or off." }; } }
        private SimConnectEvent CaptureScreenshot { get { return new SimConnectEvent() { Id = SimConnectEventId.CaptureScreenshot, Name = "CAPTURE_SCREENSHOT", Units = "N/A", Description = "Capture the current view as a screenshot. Which will be saved to a bmp file in: My Documents\\Pictures\\" }; } }
        private SimConnectEvent CenterAilerRudder { get { return new SimConnectEvent() { Id = SimConnectEventId.CenterAilerRudder, Name = "CENTER_AILER_RUDDER", Units = "N/A", Description = "Centers the aileron and rudder positions." }; } }
        private SimConnectEvent CenterNt361Check { get { return new SimConnectEvent() { Id = SimConnectEventId.CenterNt361Check, Name = "CENTER_NT361_CHECK", Units = "N/A", Description = "Check to see if NT 361 Flight Trainer should be centered" }; } }
        private SimConnectEvent ChaseView { get { return new SimConnectEvent() { Id = SimConnectEventId.ChaseView, Name = "CHASE_VIEW", Units = "-", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent ChaseViewNext { get { return new SimConnectEvent() { Id = SimConnectEventId.ChaseViewNext, Name = "CHASE_VIEW_NEXT", Units = "N/A", Description = "Cycle view to next target" }; } }
        private SimConnectEvent ChaseViewPrev { get { return new SimConnectEvent() { Id = SimConnectEventId.ChaseViewPrev, Name = "CHASE_VIEW_PREV", Units = "N/A", Description = "Cycle view to previous target" }; } }
        private SimConnectEvent ChaseViewToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ChaseViewToggle, Name = "CHASE_VIEW_TOGGLE", Units = "N/A", Description = "Toggles chase view on/off" }; } }
        private SimConnectEvent ChvppApAltWing { get { return new SimConnectEvent() { Id = SimConnectEventId.ChvppApAltWing, Name = "CHVPP_AP_ALT_WING", Units = "N/A", Description = "CH Virtual Pilot Pro altitude hold and wing level." }; } }
        private SimConnectEvent ChvppLeftHatDown { get { return new SimConnectEvent() { Id = SimConnectEventId.ChvppLeftHatDown, Name = "CHVPP_LEFT_HAT_DOWN", Units = "N/A", Description = "CH Virtual Pilot Pro down - left hat keypress." }; } }
        private SimConnectEvent ChvppLeftHatUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ChvppLeftHatUp, Name = "CHVPP_LEFT_HAT_UP", Units = "N/A", Description = "CH Virtual Pilot Pro up - left hat keypress." }; } }
        private SimConnectEvent ClockHoursDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockHoursDec, Name = "CLOCK_HOURS_DEC", Units = "N/A", Description = "Decrements time by hours" }; } }
        private SimConnectEvent ClockHoursInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockHoursInc, Name = "CLOCK_HOURS_INC", Units = "N/A", Description = "Increments time by hours" }; } }
        private SimConnectEvent ClockHoursSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockHoursSet, Name = "CLOCK_HOURS_SET", Units = "N/A", Description = "Sets hour of day" }; } }
        private SimConnectEvent ClockMinutesDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockMinutesDec, Name = "CLOCK_MINUTES_DEC", Units = "N/A", Description = "Decrements time by minutes" }; } }
        private SimConnectEvent ClockMinutesInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockMinutesInc, Name = "CLOCK_MINUTES_INC", Units = "N/A", Description = "Increments time by minutes" }; } }
        private SimConnectEvent ClockMinutesSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockMinutesSet, Name = "CLOCK_MINUTES_SET", Units = "N/A", Description = "Sets minutes of the hour" }; } }
        private SimConnectEvent ClockSecondsZero { get { return new SimConnectEvent() { Id = SimConnectEventId.ClockSecondsZero, Name = "CLOCK_SECONDS_ZERO", Units = "N/A", Description = "Zeros seconds" }; } }
        private SimConnectEvent CloseView { get { return new SimConnectEvent() { Id = SimConnectEventId.CloseView, Name = "CLOSE_VIEW", Units = "N/A", Description = "Close current view" }; } }
        private SimConnectEvent CollectiveDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.CollectiveDecr, Name = "COLLECTIVE_DECR", Units = "N/A", Description = "Decrease the engine collective by 0.05. Minimum value is clamped to 0." }; } }
        private SimConnectEvent CollectiveIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.CollectiveIncr, Name = "COLLECTIVE_INCR", Units = "N/A", Description = "Increase the engine collective by 0.05. Minimum value is clamped to 0." }; } }
        private SimConnectEvent Com1SpacingModeSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1SpacingModeSwitch, Name = "COM_1_SPACING_MODE_SWITCH", Units = "N/A", Description = "Toggle between the different modes for COM 1/2/3." }; } }
        private SimConnectEvent Com2SpacingModeSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2SpacingModeSwitch, Name = "COM_2_SPACING_MODE_SWITCH", Units = "N/A", Description = "Toggle between the different modes for COM 1/2/3." }; } }
        private SimConnectEvent Com3SpacingModeSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3SpacingModeSwitch, Name = "COM_3_SPACING_MODE_SWITCH", Units = "N/A", Description = "Toggle between the different modes for COM 1/2/3." }; } }
        private SimConnectEvent ComRadio { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadio, Name = "COM_RADIO", Units = "[0]: Bool", Description = "Sequentially selects the COM tuner digits for use with +/-. Follow by SELECT_2 for COM 2 or SELECT_3 for COM 3." }; } }
        private SimConnectEvent ComRadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioFractDec, Name = "COM_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements COM 1/2/3 frequency by 25 KHz, with no carry when digit wraps" }; } }
        private SimConnectEvent ComRadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioFractDecCarry, Name = "COM_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement COM 1/2/3 frequency by 25 KHz, and carry when digit wraps" }; } }
        private SimConnectEvent ComRadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioFractInc, Name = "COM_RADIO_FRACT_INC", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 25 KHz, with no carry when digit wraps" }; } }
        private SimConnectEvent ComRadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioFractIncCarry, Name = "COM_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 25 KHz, and carry when digit wraps" }; } }
        private SimConnectEvent ComRadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioSet, Name = "COM_RADIO_SET", Units = "[0]: Frequency value (BCD16 encoded Hz)", Description = "Sets COM 1/2/3 frequency as a BCD16 encoded value." }; } }
        private SimConnectEvent ComRadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioSetHz, Name = "COM_RADIO_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 frequency as Hz." }; } }
        private SimConnectEvent ComRadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioSwap, Name = "COM_RADIO_SWAP", Units = "N/A", Description = "Swaps COM 1/2/3 frequency with the standby frequency." }; } }
        private SimConnectEvent ComRadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioWholeDec, Name = "COM_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrement COM 1/2/3 frequency by 1 MHz. Values from 118 to 137, and this will wrap if the values go over 137. " }; } }
        private SimConnectEvent ComRadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ComRadioWholeInc, Name = "COM_RADIO_WHOLE_INC", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 1 MHz. Values are from 118 to 137, and this will wrap if the values go under 118. " }; } }
        private SimConnectEvent ComReceiveAllSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ComReceiveAllSet, Name = "COM_RECEIVE_ALL_SET", Units = "[0] Bool", Description = "Sets whether to receive on all COM radios (1, 0)" }; } }
        private SimConnectEvent ComReceiveAllToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ComReceiveAllToggle, Name = "COM_RECEIVE_ALL_TOGGLE", Units = "N/A", Description = "Toggles receive on (1) or off (0) for all COM radios." }; } }
        private SimConnectEvent ComStbyRadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ComStbyRadioSet, Name = "COM_STBY_RADIO_SET", Units = "[0]: Frequency value (BCD16 encoded Hz)", Description = "Sets COM 1/2/3 standby frequency as a BCD16 encoded value." }; } }
        private SimConnectEvent ComStbyRadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.ComStbyRadioSetHz, Name = "COM_STBY_RADIO_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 standby frequency in Hz." }; } }
        private SimConnectEvent ComStbyRadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.ComStbyRadioSwap, Name = "COM_STBY_RADIO_SWAP", Units = "N/A", Description = "Swaps COM 1 frequency with standby." }; } }
        private SimConnectEvent Com1RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1RadioSwap, Name = "COM1_RADIO_SWAP", Units = "N/A", Description = "NOTE: COM_RADIO_SWAP is simply an alias for COM1_RADIO_SWAP" }; } }
        private SimConnectEvent Com1ReceiveSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1ReceiveSelect, Name = "COM1_RECEIVE_SELECT", Units = "[0] Bool", Description = "Sets receive on (1) or off (0) for COM 1/2/3. " }; } }
        private SimConnectEvent Com1StoredFrequencyIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1StoredFrequencyIndexSet, Name = "COM1_STORED_FREQUENCY_INDEX_SET", Units = "N/A", Description = "This is used to select the index for when you want to store frequencies. This can be done by including this event when you store a frequency using one of the 2 available types: COM1_STORED_FREQUENCY_SET, COM1_STORED_FREQUENCY_SET_HZ.\nFor example if you wa" }; } }
        private SimConnectEvent Com1StoredFrequencySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1StoredFrequencySet, Name = "COM1_STORED_FREQUENCY_SET", Units = "[0]: Frequency value (BCD16 or BCD32 encoded Hz)", Description = "Sets the COM 1/2/3 stored frequency as a BCD encoded value." }; } }
        private SimConnectEvent Com1StoredFrequencySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1StoredFrequencySetHz, Name = "COM1_STORED_FREQUENCY_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 stored frequency as Hz." }; } }
        private SimConnectEvent Com1TransmitSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1TransmitSelect, Name = "COM1_TRANSMIT_SELECT", Units = "", Description = "Selects COM 1/2 to transmit" }; } }
        private SimConnectEvent Com1VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1VolumeDec, Name = "COM1_VOLUME_DEC", Units = "N/A", Description = "Decreases the COM 1/2/3 volume by 0.02, and the resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Com1VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1VolumeInc, Name = "COM1_VOLUME_INC", Units = "N/A", Description = "Increases the COM 1/2/3 volume by 0.02, and the resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Com1VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com1VolumeSet, Name = "COM1_VOLUME_SET", Units = "[0]: Volume (0 - 1)", Description = "Sets the COM 1/2/3 volume (from 0 to 1)." }; } }
        private SimConnectEvent Com2RadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioFractDec, Name = "COM2_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements COM 1/2/3 frequency by 25 KHz, with no carry when digit wraps" }; } }
        private SimConnectEvent Com2RadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioFractDecCarry, Name = "COM2_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement COM 1/2/3 frequency by 25 KHz, and carry when digit wraps" }; } }
        private SimConnectEvent Com2RadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioFractInc, Name = "COM2_RADIO_FRACT_INC", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 25 KHz, with no carry when digit wraps" }; } }
        private SimConnectEvent Com2RadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioFractIncCarry, Name = "COM2_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 25 KHz, and carry when digit wraps" }; } }
        private SimConnectEvent Com2RadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioSet, Name = "COM2_RADIO_SET", Units = "[0]: Frequency value (BCD16 encoded Hz)", Description = "Sets COM 1/2/3 frequency as a BCD16 encoded value." }; } }
        private SimConnectEvent Com2RadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioSetHz, Name = "COM2_RADIO_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 frequency as Hz." }; } }
        private SimConnectEvent Com2RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioSwap, Name = "COM2_RADIO_SWAP", Units = "N/A", Description = "NOTE: COM_RADIO_SWAP is simply an alias for COM1_RADIO_SWAP" }; } }
        private SimConnectEvent Com2RadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioWholeDec, Name = "COM2_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrement COM 1/2/3 frequency by 1 MHz. Values from 118 to 137, and this will wrap if the values go over 137. " }; } }
        private SimConnectEvent Com2RadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2RadioWholeInc, Name = "COM2_RADIO_WHOLE_INC", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 1 MHz. Values are from 118 to 137, and this will wrap if the values go under 118. " }; } }
        private SimConnectEvent Com2ReceiveSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2ReceiveSelect, Name = "COM2_RECEIVE_SELECT", Units = "[0] Bool", Description = "Sets receive on (1) or off (0) for COM 1/2/3. " }; } }
        private SimConnectEvent Com2StbyRadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2StbyRadioSet, Name = "COM2_STBY_RADIO_SET", Units = "[0]: Frequency value (BCD16 encoded Hz)", Description = "Sets COM 1/2/3 standby frequency as a BCD16 encoded value." }; } }
        private SimConnectEvent Com2StbyRadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2StbyRadioSetHz, Name = "COM2_STBY_RADIO_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 standby frequency in Hz." }; } }
        private SimConnectEvent Com2StoredFrequencyIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2StoredFrequencyIndexSet, Name = "COM2_STORED_FREQUENCY_INDEX_SET", Units = "N/A", Description = "This is used to select the index for when you want to store frequencies. This can be done by including this event when you store a frequency using one of the 2 available types: COM1_STORED_FREQUENCY_SET, COM1_STORED_FREQUENCY_SET_HZ.\nFor example if you wa" }; } }
        private SimConnectEvent Com2StoredFrequencySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2StoredFrequencySet, Name = "COM2_STORED_FREQUENCY_SET", Units = "[0]: Frequency value (BCD16 or BCD32 encoded Hz)", Description = "Sets the COM 1/2/3 stored frequency as a BCD encoded value." }; } }
        private SimConnectEvent Com2StoredFrequencySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2StoredFrequencySetHz, Name = "COM2_STORED_FREQUENCY_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 stored frequency as Hz." }; } }
        private SimConnectEvent Com2TransmitSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2TransmitSelect, Name = "COM2_TRANSMIT_SELECT", Units = "", Description = " See PILOT_TRANSMITTER_SET instead. " }; } }
        private SimConnectEvent Com2VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2VolumeDec, Name = "COM2_VOLUME_DEC", Units = "N/A", Description = "Decreases the COM 1/2/3 volume by 0.02, and the resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Com2VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2VolumeInc, Name = "COM2_VOLUME_INC", Units = "N/A", Description = "Increases the COM 1/2/3 volume by 0.02, and the resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Com2VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com2VolumeSet, Name = "COM2_VOLUME_SET", Units = "[0]: Volume (0 - 1)", Description = "Sets the COM 1/2/3 volume (from 0 to 1)." }; } }
        private SimConnectEvent Com3RadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioFractDec, Name = "COM3_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements COM 1/2/3 frequency by 25 KHz, with no carry when digit wraps" }; } }
        private SimConnectEvent Com3RadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioFractDecCarry, Name = "COM3_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement COM 1/2/3 frequency by 25 KHz, and carry when digit wraps" }; } }
        private SimConnectEvent Com3RadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioFractInc, Name = "COM3_RADIO_FRACT_INC", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 25 KHz, with no carry when digit wraps" }; } }
        private SimConnectEvent Com3RadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioFractIncCarry, Name = "COM3_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 25 KHz, and carry when digit wraps" }; } }
        private SimConnectEvent Com3RadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioSet, Name = "COM3_RADIO_SET", Units = "[0]: Frequency value (BCD16 encoded Hz)", Description = "Sets COM 1/2/3 frequency as a BCD16 encoded value." }; } }
        private SimConnectEvent Com3RadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioSetHz, Name = "COM3_RADIO_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 frequency as Hz." }; } }
        private SimConnectEvent Com3RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioSwap, Name = "COM3_RADIO_SWAP", Units = "N/A", Description = "NOTE: COM_RADIO_SWAP is simply an alias for COM1_RADIO_SWAP" }; } }
        private SimConnectEvent Com3RadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioWholeDec, Name = "COM3_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrement COM 1/2/3 frequency by 1 MHz. Values from 118 to 137, and this will wrap if the values go over 137. " }; } }
        private SimConnectEvent Com3RadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3RadioWholeInc, Name = "COM3_RADIO_WHOLE_INC", Units = "N/A", Description = "Increment COM 1/2/3 frequency by 1 MHz. Values are from 118 to 137, and this will wrap if the values go under 118. " }; } }
        private SimConnectEvent Com3ReceiveSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3ReceiveSelect, Name = "COM3_RECEIVE_SELECT", Units = "[0] Bool", Description = "Sets receive on (1) or off (0) for COM 1/2/3. " }; } }
        private SimConnectEvent Com3StbyRadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3StbyRadioSet, Name = "COM3_STBY_RADIO_SET", Units = "[0]: Frequency value (BCD16 encoded Hz)", Description = "Sets COM 1/2/3 standby frequency as a BCD16 encoded value." }; } }
        private SimConnectEvent Com3StbyRadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3StbyRadioSetHz, Name = "COM3_STBY_RADIO_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 standby frequency in Hz." }; } }
        private SimConnectEvent Com3StoredFrequencyIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3StoredFrequencyIndexSet, Name = "COM3_STORED_FREQUENCY_INDEX_SET", Units = "N/A", Description = "This is used to select the index for when you want to store frequencies. This can be done by including this event when you store a frequency using one of the 2 available types: COM1_STORED_FREQUENCY_SET, COM1_STORED_FREQUENCY_SET_HZ.\nFor example if you wa" }; } }
        private SimConnectEvent Com3StoredFrequencySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3StoredFrequencySet, Name = "COM3_STORED_FREQUENCY_SET", Units = "[0]: Frequency value (BCD16 or BCD32 encoded Hz)", Description = "Sets the COM 1/2/3 stored frequency as a BCD encoded value." }; } }
        private SimConnectEvent Com3StoredFrequencySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3StoredFrequencySetHz, Name = "COM3_STORED_FREQUENCY_SET_HZ", Units = "[0]: Frequency value (Hz)", Description = "Sets COM 1/2/3 stored frequency as Hz." }; } }
        private SimConnectEvent Com3VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3VolumeDec, Name = "COM3_VOLUME_DEC", Units = "N/A", Description = "Decreases the COM 1/2/3 volume by 0.02, and the resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Com3VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3VolumeInc, Name = "COM3_VOLUME_INC", Units = "N/A", Description = "Increases the COM 1/2/3 volume by 0.02, and the resulting value will be clamped between 0 and 1." }; } }
        private SimConnectEvent Com3VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Com3VolumeSet, Name = "COM3_VOLUME_SET", Units = "[0]: Volume (0 - 1)", Description = "Sets the COM 1/2/3 volume (from 0 to 1)." }; } }
        private SimConnectEvent ConcordeNoseVisorFullExt { get { return new SimConnectEvent() { Id = SimConnectEventId.ConcordeNoseVisorFullExt, Name = "CONCORDE_NOSE_VISOR_FULL_EXT", Units = "N/A", Description = "Depricated" }; } }
        private SimConnectEvent ConcordeNoseVisorFullRet { get { return new SimConnectEvent() { Id = SimConnectEventId.ConcordeNoseVisorFullRet, Name = "CONCORDE_NOSE_VISOR_FULL_RET", Units = "N/A", Description = "Depricated" }; } }
        private SimConnectEvent ConditionLever1CutOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever1CutOff, Name = "CONDITION_LEVER_1_CUT_OFF", Units = "N/A", Description = "Sets the condition lever for engine 1/2/3/4 to the cutoff position, which cuts the fuel flow." }; } }
        private SimConnectEvent ConditionLever1Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever1Dec, Name = "CONDITION_LEVER_1_DEC", Units = "N/A", Description = "Decreases the condition lever position by one for engine 1/2/3/4. The possible lever positions are as follows:" }; } }
        private SimConnectEvent ConditionLever1HighIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever1HighIdle, Name = "CONDITION_LEVER_1_HIGH_IDLE", Units = "N/A", Description = "Sets the condition lever for engine 1/2/3/4 to the high position (2)." }; } }
        private SimConnectEvent ConditionLever1Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever1Inc, Name = "CONDITION_LEVER_1_INC", Units = "N/A", Description = "Increments the condition lever position by one for engine 1/2/3/4. The possible lever positions are as follows:" }; } }
        private SimConnectEvent ConditionLever1LowIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever1LowIdle, Name = "CONDITION_LEVER_1_LOW_IDLE", Units = "N/A", Description = "Sets the condition lever for engine 1/2/3/4 to the low position (1)." }; } }
        private SimConnectEvent ConditionLever1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever1Set, Name = "CONDITION_LEVER_1_SET", Units = "[0]: Position", Description = "Sets the condition lever for engine 1/2/3/4 to the given position, which is one of the following:" }; } }
        private SimConnectEvent ConditionLever2CutOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever2CutOff, Name = "CONDITION_LEVER_2_CUT_OFF", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever2Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever2Dec, Name = "CONDITION_LEVER_2_DEC", Units = "N/A", Description = "0 for cutoff" }; } }
        private SimConnectEvent ConditionLever2HighIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever2HighIdle, Name = "CONDITION_LEVER_2_HIGH_IDLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever2Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever2Inc, Name = "CONDITION_LEVER_2_INC", Units = "N/A", Description = "0 for cutoff" }; } }
        private SimConnectEvent ConditionLever2LowIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever2LowIdle, Name = "CONDITION_LEVER_2_LOW_IDLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever2Set, Name = "CONDITION_LEVER_2_SET", Units = "[0]: Position", Description = "0 for cutoff" }; } }
        private SimConnectEvent ConditionLever3CutOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever3CutOff, Name = "CONDITION_LEVER_3_CUT_OFF", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever3Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever3Dec, Name = "CONDITION_LEVER_3_DEC", Units = "N/A", Description = "1 for low idle" }; } }
        private SimConnectEvent ConditionLever3HighIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever3HighIdle, Name = "CONDITION_LEVER_3_HIGH_IDLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever3Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever3Inc, Name = "CONDITION_LEVER_3_INC", Units = "N/A", Description = "1 for low idle" }; } }
        private SimConnectEvent ConditionLever3LowIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever3LowIdle, Name = "CONDITION_LEVER_3_LOW_IDLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever3Set, Name = "CONDITION_LEVER_3_SET", Units = "[0]: Position", Description = "1 for low idle" }; } }
        private SimConnectEvent ConditionLever4CutOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever4CutOff, Name = "CONDITION_LEVER_4_CUT_OFF", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever4Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever4Dec, Name = "CONDITION_LEVER_4_DEC", Units = "N/A", Description = "2 for high" }; } }
        private SimConnectEvent ConditionLever4HighIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever4HighIdle, Name = "CONDITION_LEVER_4_HIGH_IDLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever4Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever4Inc, Name = "CONDITION_LEVER_4_INC", Units = "N/A", Description = "2 for high" }; } }
        private SimConnectEvent ConditionLever4LowIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever4LowIdle, Name = "CONDITION_LEVER_4_LOW_IDLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ConditionLever4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLever4Set, Name = "CONDITION_LEVER_4_SET", Units = "[0]: Position", Description = "2 for high" }; } }
        private SimConnectEvent ConditionLeverCutOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLeverCutOff, Name = "CONDITION_LEVER_CUT_OFF", Units = "[0]: Engine index", Description = "Sets the condition lever for the indexed engine to the cutoff position, which cuts the fuel flow.\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent ConditionLeverDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLeverDec, Name = "CONDITION_LEVER_DEC", Units = "[0]: Engine index", Description = "Decrements the condition lever position by one for the indexed engine. The possible lever positions are as follows:\r\n0 for cutoff\r\n1 for low idle\r\n2 for high" }; } }
        private SimConnectEvent ConditionLeverHighIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLeverHighIdle, Name = "CONDITION_LEVER_HIGH_IDLE", Units = "[0]: Engine index", Description = "Sets the condition lever for the indexed engine to the high position (2).\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent ConditionLeverInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLeverInc, Name = "CONDITION_LEVER_INC", Units = "[0]: Engine index", Description = "Increments the condition lever position by one for the indexed engine. The possible lever positions are as follows:\r\n0 for cutoff\r\n1 for low idle\r\n2 for high" }; } }
        private SimConnectEvent ConditionLeverLowIdle { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLeverLowIdle, Name = "CONDITION_LEVER_LOW_IDLE", Units = "[0]: Engine index", Description = "Sets the condition lever for the indexed engine to the low position (1).\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent ConditionLeverSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ConditionLeverSet, Name = "CONDITION_LEVER_SET", Units = "[0]: Position", Description = "Sets the condition lever for the all engines to the given position, which is one of the following:\r\n0 for cutoff\r\n1 for low idle\r\n2 for high" }; } }
        private SimConnectEvent CopilotTransmitterSet { get { return new SimConnectEvent() { Id = SimConnectEventId.CopilotTransmitterSet, Name = "COPILOT_TRANSMITTER_SET", Units = "[0]: Value in degrees", Description = "Toggles between GPS and NAV 1 driving NAV 1 OBS display (and AP)" }; } }
        private SimConnectEvent Cowlflap1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Cowlflap1Set, Name = "COWLFLAP1_SET", Units = "[0]: position from 0 to 16983", Description = "Sets engine 1/2/3/4 cowl flap lever position (0 to 16383)" }; } }
        private SimConnectEvent Cowlflap2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Cowlflap2Set, Name = "COWLFLAP2_SET", Units = "[0]: position from 0 to 16983", Description = "Sets engine 1/2/3/4 cowl flap lever position (0 to 16383)" }; } }
        private SimConnectEvent Cowlflap3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Cowlflap3Set, Name = "COWLFLAP3_SET", Units = "[0]: position from 0 to 16983", Description = "Sets engine 1/2/3/4 cowl flap lever position (0 to 16383)" }; } }
        private SimConnectEvent Cowlflap4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Cowlflap4Set, Name = "COWLFLAP4_SET", Units = "[0]: position from 0 to 16983", Description = "Sets engine 1/2/3/4 cowl flap lever position (0 to 16383)" }; } }
        private SimConnectEvent CrossFeedLeftToRight { get { return new SimConnectEvent() { Id = SimConnectEventId.CrossFeedLeftToRight, Name = "CROSS_FEED_LEFT_TO_RIGHT", Units = "N/A", Description = "Sets the fuel crossfeed to be from left to right." }; } }
        private SimConnectEvent CrossFeedOff { get { return new SimConnectEvent() { Id = SimConnectEventId.CrossFeedOff, Name = "CROSS_FEED_OFF", Units = "N/A", Description = "Closes crossfeed valve (when used in conjunction with \"isolate\" tank)" }; } }
        private SimConnectEvent CrossFeedOpen { get { return new SimConnectEvent() { Id = SimConnectEventId.CrossFeedOpen, Name = "CROSS_FEED_OPEN", Units = "N/A", Description = "Opens cross feed valve (when used in conjunction with \"isolate\" tank)" }; } }
        private SimConnectEvent CrossFeedRightToLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.CrossFeedRightToLeft, Name = "CROSS_FEED_RIGHT_TO_LEFT", Units = "N/A", Description = "Sets the fuel crossfeed to be from right to left." }; } }
        private SimConnectEvent CrossFeedToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.CrossFeedToggle, Name = "CROSS_FEED_TOGGLE", Units = "N/A", Description = "Toggles crossfeed valve (when used in conjunction with \"isolate\" tank)" }; } }
        private SimConnectEvent CyclicLateralLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.CyclicLateralLeft, Name = "CYCLIC_LATERAL_LEFT", Units = "N/A", Description = "Change the lateral cyclic (left) by -0.098 when pressed. If held down, the change will happen more rapidly." }; } }
        private SimConnectEvent CyclicLateralRight { get { return new SimConnectEvent() { Id = SimConnectEventId.CyclicLateralRight, Name = "CYCLIC_LATERAL_RIGHT", Units = "N/A", Description = "Change the lateral cyclic (right) by 0.098 when pressed. If held down, the change will happen more rapidly." }; } }
        private SimConnectEvent CyclicLongitudinalDown { get { return new SimConnectEvent() { Id = SimConnectEventId.CyclicLongitudinalDown, Name = "CYCLIC_LONGITUDINAL_DOWN", Units = "N/A", Description = "Change the longitudinal cyclic (down) by -0.049 when pressed. If held down, the change will happen more rapidly." }; } }
        private SimConnectEvent CyclicLongitudinalUp { get { return new SimConnectEvent() { Id = SimConnectEventId.CyclicLongitudinalUp, Name = "CYCLIC_LONGITUDINAL_UP", Units = "N/A", Description = "Change the longitudinal cyclic (up) by 0.049 when pressed. If held down, the change will happen more rapidly." }; } }
        private SimConnectEvent Debug0 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug0, Name = "DEBUG_0", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug1, Name = "DEBUG_1", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug2 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug2, Name = "DEBUG_2", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug3 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug3, Name = "DEBUG_3", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug4 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug4, Name = "DEBUG_4", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug5 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug5, Name = "DEBUG_5", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug6 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug6, Name = "DEBUG_6", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug7 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug7, Name = "DEBUG_7", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug8 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug8, Name = "DEBUG_8", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent Debug9 { get { return new SimConnectEvent() { Id = SimConnectEventId.Debug9, Name = "DEBUG_9", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugA { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugA, Name = "DEBUG_A", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugB { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugB, Name = "DEBUG_B", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugC { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugC, Name = "DEBUG_C", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugD { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugD, Name = "DEBUG_D", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugE { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugE, Name = "DEBUG_E", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugF { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugF, Name = "DEBUG_F", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugG { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugG, Name = "DEBUG_G", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugH { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugH, Name = "DEBUG_H", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugI { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugI, Name = "DEBUG_I", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugJ { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugJ, Name = "DEBUG_J", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugK { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugK, Name = "DEBUG_K", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugL { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugL, Name = "DEBUG_L", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugM { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugM, Name = "DEBUG_M", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugN { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugN, Name = "DEBUG_N", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugO { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugO, Name = "DEBUG_O", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugP { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugP, Name = "DEBUG_P", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugQ { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugQ, Name = "DEBUG_Q", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugR { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugR, Name = "DEBUG_R", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugS { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugS, Name = "DEBUG_S", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugT { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugT, Name = "DEBUG_T", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugU { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugU, Name = "DEBUG_U", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugV { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugV, Name = "DEBUG_V", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugW { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugW, Name = "DEBUG_W", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugX { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugX, Name = "DEBUG_X", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugY { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugY, Name = "DEBUG_Y", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DebugZ { get { return new SimConnectEvent() { Id = SimConnectEventId.DebugZ, Name = "DEBUG_Z", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent DecConcordeNoseVisor { get { return new SimConnectEvent() { Id = SimConnectEventId.DecConcordeNoseVisor, Name = "DEC_CONCORDE_NOSE_VISOR", Units = "N/A", Description = "Depricated" }; } }
        private SimConnectEvent DecCowlFlaps { get { return new SimConnectEvent() { Id = SimConnectEventId.DecCowlFlaps, Name = "DEC_COWL_FLAPS", Units = "N/A", Description = "Decrement cowl flap levers" }; } }
        private SimConnectEvent DecCowlFlaps1 { get { return new SimConnectEvent() { Id = SimConnectEventId.DecCowlFlaps1, Name = "DEC_COWL_FLAPS1", Units = "N/A", Description = "Decrement engine 1 cowl flap lever" }; } }
        private SimConnectEvent DecCowlFlaps2 { get { return new SimConnectEvent() { Id = SimConnectEventId.DecCowlFlaps2, Name = "DEC_COWL_FLAPS2", Units = "N/A", Description = "Decrement engine 1 cowl flap lever" }; } }
        private SimConnectEvent DecCowlFlaps3 { get { return new SimConnectEvent() { Id = SimConnectEventId.DecCowlFlaps3, Name = "DEC_COWL_FLAPS3", Units = "N/A", Description = "Decrement engine 1 cowl flap lever" }; } }
        private SimConnectEvent DecCowlFlaps4 { get { return new SimConnectEvent() { Id = SimConnectEventId.DecCowlFlaps4, Name = "DEC_COWL_FLAPS4", Units = "N/A", Description = "Decrement engine 1 cowl flap lever" }; } }
        private SimConnectEvent DecisionHeightSet { get { return new SimConnectEvent() { Id = SimConnectEventId.DecisionHeightSet, Name = "DECISION_HEIGHT_SET", Units = "[0]: height (m)", Description = "Set the AGL decision height reference, in meters." }; } }
        private SimConnectEvent DecreaseAutobrakeControl { get { return new SimConnectEvent() { Id = SimConnectEventId.DecreaseAutobrakeControl, Name = "DECREASE_AUTOBRAKE_CONTROL", Units = "N/A", Description = "Decrements the autobrake level by 1. When the level reaches 0, autobreaks will be off, and the event will no longer decrement further." }; } }
        private SimConnectEvent DecreaseDecisionAltitudeMsl { get { return new SimConnectEvent() { Id = SimConnectEventId.DecreaseDecisionAltitudeMsl, Name = "DECREASE_DECISION_ALTITUDE_MSL", Units = "[0]: amount", Description = "Decrements the MSL decision height reference by the amount given, or by 10m if no amount is given." }; } }
        private SimConnectEvent DecreaseDecisionHeight { get { return new SimConnectEvent() { Id = SimConnectEventId.DecreaseDecisionHeight, Name = "DECREASE_DECISION_HEIGHT", Units = "N/A", Description = "Decrements the AGL decision height reference by 1m." }; } }
        private SimConnectEvent DecreaseHeloGovBeep { get { return new SimConnectEvent() { Id = SimConnectEventId.DecreaseHeloGovBeep, Name = "DECREASE_HELO_GOV_BEEP", Units = "[0]: value\r\n[1]: engine", Description = "If the helicopter has an engine trimmer, this event can be used to decrease the nominal engine/rotor RPM that the governor is trying to maintain for the indexed engine.\r\nThe amount that the trim will be adjusted by is set using the engine_trim_rate CFG parameter, and the min and max achievable values are set using engine_trim_min and engine_trim_max. Alternatively, you may supply a value that will override that set in the" }; } }
        private SimConnectEvent DecreaseThrottle { get { return new SimConnectEvent() { Id = SimConnectEventId.DecreaseThrottle, Name = "DECREASE_THROTTLE", Units = "[0]: the value between 0 - 16384", Description = "Decrement throttles" }; } }
        private SimConnectEvent DemoRecord1Sec { get { return new SimConnectEvent() { Id = SimConnectEventId.DemoRecord1Sec, Name = "DEMO_RECORD_1_SEC", Units = "N/A", Description = "Record 1 second demo." }; } }
        private SimConnectEvent DemoRecord5Sec { get { return new SimConnectEvent() { Id = SimConnectEventId.DemoRecord5Sec, Name = "DEMO_RECORD_5_SEC", Units = "N/A", Description = "Record 5 second demo." }; } }
        private SimConnectEvent DemoRecordStop { get { return new SimConnectEvent() { Id = SimConnectEventId.DemoRecordStop, Name = "DEMO_RECORD_STOP", Units = "N/A", Description = "Stops demo system recording." }; } }
        private SimConnectEvent DemoStop { get { return new SimConnectEvent() { Id = SimConnectEventId.DemoStop, Name = "DEMO_STOP", Units = "N/A", Description = "Stops demo system playback." }; } }
        private SimConnectEvent Dme { get { return new SimConnectEvent() { Id = SimConnectEventId.Dme, Name = "DME", Units = "N/A", Description = "Selects the DME for use with +/-" }; } }
        private SimConnectEvent DmeSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.DmeSelect, Name = "DME_SELECT", Units = "[0]: DME ID", Description = "Selects one of the two DME systems (1, 2)." }; } }
        private SimConnectEvent Dme1Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.Dme1Toggle, Name = "DME1_TOGGLE", Units = "N/A", Description = "Sets the DME 1 / 2 display to NAV 1 / 2." }; } }
        private SimConnectEvent Dme2Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.Dme2Toggle, Name = "DME2_TOGGLE", Units = "N/A", Description = "Sets the DME 1 / 2 display to NAV 1 / 2." }; } }
        private SimConnectEvent Egt { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt, Name = "EGT", Units = "N/A", Description = "Selects EGT bug for +/-. Follow by SELECT_1, SELECT_2, SELECT_3, or SELECT_4 to target the appropriate bug." }; } }
        private SimConnectEvent EgtDec { get { return new SimConnectEvent() { Id = SimConnectEventId.EgtDec, Name = "EGT_DEC", Units = "N/A", Description = "Decrements all EGT bugs." }; } }
        private SimConnectEvent EgtInc { get { return new SimConnectEvent() { Id = SimConnectEventId.EgtInc, Name = "EGT_INC", Units = "N/A", Description = "Increments all EGT bugs." }; } }
        private SimConnectEvent EgtSet { get { return new SimConnectEvent() { Id = SimConnectEventId.EgtSet, Name = "EGT_SET", Units = "[0]: Bug value (0 to 32768)", Description = "Sets all EGT bugs to the given value. Input will be normalised between 0 and 1." }; } }
        private SimConnectEvent Egt1Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt1Dec, Name = "EGT1_DEC", Units = "N/A", Description = "Decrements the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt1Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt1Inc, Name = "EGT1_INC", Units = "N/A", Description = "Increments the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt1Set, Name = "EGT1_SET", Units = "[0]: Bug value (0 to 32768)", Description = "Sets the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt2Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt2Dec, Name = "EGT2_DEC", Units = "N/A", Description = "Decrements the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt2Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt2Inc, Name = "EGT2_INC", Units = "N/A", Description = "Increments the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt2Set, Name = "EGT2_SET", Units = "[0]: Bug value (0 to 32768)", Description = "Sets the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt3Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt3Dec, Name = "EGT3_DEC", Units = "N/A", Description = "Decrements the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt3Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt3Inc, Name = "EGT3_INC", Units = "N/A", Description = "Increments the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt3Set, Name = "EGT3_SET", Units = "[0]: Bug value (0 to 32768)", Description = "Sets the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt4Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt4Dec, Name = "EGT4_DEC", Units = "N/A", Description = "Decrements the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt4Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt4Inc, Name = "EGT4_INC", Units = "N/A", Description = "Increments the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent Egt4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Egt4Set, Name = "EGT4_SET", Units = "[0]: Bug value (0 to 32768)", Description = "Sets the specific EGT 1/2/3/4 bug value." }; } }
        private SimConnectEvent ElectFuelPump1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectFuelPump1Set, Name = "ELECT_FUEL_PUMP1_SET", Units = "[0]: The fuel quantity", Description = "Set the electrical pump status for engines 1-4. Values are as follows:" }; } }
        private SimConnectEvent ElectFuelPump2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectFuelPump2Set, Name = "ELECT_FUEL_PUMP2_SET", Units = "[0]: The fuel quantity", Description = "0 = Off" }; } }
        private SimConnectEvent ElectFuelPump3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectFuelPump3Set, Name = "ELECT_FUEL_PUMP3_SET", Units = "[0]: The fuel quantity", Description = "1 = On" }; } }
        private SimConnectEvent ElectFuelPump4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectFuelPump4Set, Name = "ELECT_FUEL_PUMP4_SET", Units = "[0]: The fuel quantity", Description = "2 = Auto\r\nThese keys are only useful when using the legacy [FUEL] system." }; } }
        private SimConnectEvent ElectricalAlternatorBreakerToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalAlternatorBreakerToggle, Name = "ELECTRICAL_ALTERNATOR_BREAKER_TOGGLE", Units = "[0] Source bus index\r\n[1] Target alternator index", Description = "Toggle the alternator breaker state with a bus. Takes two indices, the bus and the alternator index (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the alternator index is the N index of the alternato" }; } }
        private SimConnectEvent ElectricalBatteryBreakerToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBatteryBreakerToggle, Name = "ELECTRICAL_BATTERY_BREAKER_TOGGLE", Units = "[0] Source bus index\r\n[1] Target battery index", Description = "Toggle the battery breaker state with a bus. Takes two indices, the bus and the battery index (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the circuit index is the N index of thebattery.N definitio" }; } }
        private SimConnectEvent ElectricalBusBreakerToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBusBreakerToggle, Name = "ELECTRICAL_BUS_BREAKER_TOGGLE", Units = "[0]: Source bus index\r\n[1]: Target Bus Index", Description = "Toggle the breaker state between busses. Takes two indices, the source bus and the target bus (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition." }; } }
        private SimConnectEvent ElectricalBusToAlternatorConnectionToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBusToAlternatorConnectionToggle, Name = "ELECTRICAL_BUS_TO_ALTERNATOR_CONNECTION_TOGGLE", Units = "[0]: Source bus index\r\n[1]: Alternator index", Description = "Toggle alternator connection state with a bus. Takes two indices, the bus and the alternator (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the alternator index is the N index of the alternator.N def" }; } }
        private SimConnectEvent ElectricalBusToBatteryConnectionToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBusToBatteryConnectionToggle, Name = "ELECTRICAL_BUS_TO_BATTERY_CONNECTION_TOGGLE", Units = "[0]: Source bus index\r\n[1]: battery index", Description = "Toggle battery connection state with a bus. Takes two indices, the bus and the battery (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the battery index is the N index of the battery.N definition." }; } }
        private SimConnectEvent ElectricalBusToBusConnectionToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBusToBusConnectionToggle, Name = "ELECTRICAL_BUS_TO_BUS_CONNECTION_TOGGLE", Units = "[0]: Source bus index\r\n[1]: bus index", Description = "Toggle bus connection state with a bus. Takes two indices, the initial bus and the second bus (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition in both cases." }; } }
        private SimConnectEvent ElectricalBusToCircuitConnectionToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBusToCircuitConnectionToggle, Name = "ELECTRICAL_BUS_TO_CIRCUIT_CONNECTION_TOGGLE", Units = "[0]: Source bus index\r\n[1]: circuit index", Description = "Toggle circuit connection state with a bus. Takes two indices, the bus and the circuit (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the circuit index is the value assigned to the circuit Type when " }; } }
        private SimConnectEvent ElectricalBusToExternalPowerConnectionToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalBusToExternalPowerConnectionToggle, Name = "ELECTRICAL_BUS_TO_EXTERNAL_POWER_CONNECTION_TOGGLE", Units = "[0]: Source bus index\r\n[1]: external power index", Description = "Toggle external power connection state with a bus. Takes two indices, the bus and the external power source (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the external power index is the N index of t" }; } }
        private SimConnectEvent ElectricalCircuitBreakerToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalCircuitBreakerToggle, Name = "ELECTRICAL_CIRCUIT_BREAKER_TOGGLE", Units = "[0] Source bus index\r\n[1] Target circuit index", Description = "Toggle the circuit breaker state with a bus. Takes two indices, the bus and the circuit index (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the circuit index is the N index of the circuit.N definiti" }; } }
        private SimConnectEvent ElectricalCircuitPowerSettingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalCircuitPowerSettingSet, Name = "ELECTRICAL_CIRCUIT_POWER_SETTING_SET", Units = "[0]: circuit index\r\n[1]: circuit power setting (%)", Description = "Set circuit power setting. Takes two indices, the circuit and the power setting (see SimVars And Keys for more information). The circuit index is the value assigned to the circuit Type when the circuit.N was defined." }; } }
        private SimConnectEvent ElectricalCircuitToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalCircuitToggle, Name = "ELECTRICAL_CIRCUIT_TOGGLE", Units = "circuit index", Description = "Toggle the indexed circuit switch state. The index is the value assigned to the circuit N when the circuit.N was defined." }; } }
        private SimConnectEvent ElectricalExecuteProcedure { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalExecuteProcedure, Name = "ELECTRICAL_EXECUTE_PROCEDURE", Units = "[0]: procedure index\r\n[1]: bInverse (optional)", Description = "Execute procedure.N with bInverse as an optional argument" }; } }
        private SimConnectEvent ElectricalExternalPowerBreakerToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElectricalExternalPowerBreakerToggle, Name = "ELECTRICAL_EXTERNAL_POWER_BREAKER_TOGGLE", Units = "[0] Source bus index\r\n[1] Target external power index", Description = "Toggle the external power breaker state with a bus. Takes two indices, the bus and the external power index (see SimVars And Keys for more information). The bus index is the N index of the bus.N definition, and the external power index is the N index of t" }; } }
        private SimConnectEvent ElevDown { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevDown, Name = "ELEV_DOWN", Units = "N/A", Description = "Decrements the elevator by -0.05 (to a minimum of -1). When the key is released the elevator will return to it's original position.\r\nNote that this is simply an alias for the ELEVATOR_DOWN event." }; } }
        private SimConnectEvent ElevTrimDn { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevTrimDn, Name = "ELEV_TRIM_DN", Units = "N/A", Description = "Decrements the elevator trim by -0.0005. Holding down the key will cause the trim to decrement faster over time." }; } }
        private SimConnectEvent ElevTrimUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevTrimUp, Name = "ELEV_TRIM_UP", Units = "N/A", Description = "Increments the elevator trim by 0.0005. Holding down the key will cause the trim to increment faster over time." }; } }
        private SimConnectEvent ElevUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevUp, Name = "ELEV_UP", Units = "N/A", Description = "Increments elevator by 0.05 (to a maximum of 1). When the key is released the elevator will return to it's original position.\r\nNote that this is simply an alias for the ELEVATOR_UP event." }; } }
        private SimConnectEvent ElevatorDown { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevatorDown, Name = "ELEVATOR_DOWN", Units = "N/A", Description = "Decrements the elevator by -0.05 (to a minimum of -1). When the key is released the elevator will return to it's original position." }; } }
        private SimConnectEvent ElevatorSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevatorSet, Name = "ELEVATOR_SET", Units = "[0]: Position (-16383 to 16384)", Description = "Sets elevator position (input will be normalised to a value between -1 and 1)." }; } }
        private SimConnectEvent ElevatorTrimDisabledSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevatorTrimDisabledSet, Name = "ELEVATOR_TRIM_DISABLED_SET", Units = "[0]: Bool", Description = "Sets the Elevator Trim Disabled to be on (TRUE) or off (FALSE)." }; } }
        private SimConnectEvent ElevatorTrimDisabledToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevatorTrimDisabledToggle, Name = "ELEVATOR_TRIM_DISABLED_TOGGLE", Units = "N/A", Description = "Toggles the Elevator Trim Disabled between on (1, TRUE) and off (0, FALSE)." }; } }
        private SimConnectEvent ElevatorTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevatorTrimSet, Name = "ELEVATOR_TRIM_SET", Units = "[0]: Trim position (-16383 to 16384)", Description = "Sets the elevator trim position." }; } }
        private SimConnectEvent ElevatorUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ElevatorUp, Name = "ELEVATOR_UP", Units = "N/A", Description = "Increments the elevator by 0.05 (to a maximum of 1). When the key is released the elevator will return to it's original position." }; } }
        private SimConnectEvent EltOff { get { return new SimConnectEvent() { Id = SimConnectEventId.EltOff, Name = "ELT_OFF", Units = "N/A", Description = "Switches the ELT off (0)." }; } }
        private SimConnectEvent EltOn { get { return new SimConnectEvent() { Id = SimConnectEventId.EltOn, Name = "ELT_ON", Units = "N/A", Description = "Switches the ELT on (1)." }; } }
        private SimConnectEvent EltSet { get { return new SimConnectEvent() { Id = SimConnectEventId.EltSet, Name = "ELT_SET", Units = "[0]: Bool", Description = "Sets the ELT on (1) or off (0)." }; } }
        private SimConnectEvent EltToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EltToggle, Name = "ELT_TOGGLE", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent Engine { get { return new SimConnectEvent() { Id = SimConnectEventId.Engine, Name = "ENGINE", Units = "N/A", Description = "Sets engines for 1,2,3,4 selection (to be followed by SELECT_n)" }; } }
        private SimConnectEvent EngineAutoShutdown { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineAutoShutdown, Name = "ENGINE_AUTO_SHUTDOWN", Units = "N/A", Description = "Triggers auto-shutdown" }; } }
        private SimConnectEvent EngineAutoStart { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineAutoStart, Name = "ENGINE_AUTO_START", Units = "N/A", Description = "Triggers auto-start" }; } }
        private SimConnectEvent EngineBleedAirSourceSet { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineBleedAirSourceSet, Name = "ENGINE_BLEED_AIR_SOURCE_SET", Units = "[0]: The engine index to target (from 1 to 4)\r\n[1]: Set to TRUE/FALSE to set the engine as source (TRUE) or not (FALSE)", Description = "Sets if the indexed engine is a source to the bleed air system or not." }; } }
        private SimConnectEvent EngineBleedAirSourceToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineBleedAirSourceToggle, Name = "ENGINE_BLEED_AIR_SOURCE_TOGGLE", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineFuelflowBugPosition1 { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineFuelflowBugPosition1, Name = "ENGINE_FUELFLOW_BUG_POSITION1", Units = "", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent EngineFuelflowBugPosition2 { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineFuelflowBugPosition2, Name = "ENGINE_FUELFLOW_BUG_POSITION2", Units = "", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent EngineFuelflowBugPosition3 { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineFuelflowBugPosition3, Name = "ENGINE_FUELFLOW_BUG_POSITION3", Units = "", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent EngineFuelflowBugPosition4 { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineFuelflowBugPosition4, Name = "ENGINE_FUELFLOW_BUG_POSITION4", Units = "", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent EngineMaster1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster1Set, Name = "ENGINE_MASTER_1_SET", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster1Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster1Toggle, Name = "ENGINE_MASTER_1_TOGGLE", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster2Set, Name = "ENGINE_MASTER_2_SET", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster2Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster2Toggle, Name = "ENGINE_MASTER_2_TOGGLE", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster3Set, Name = "ENGINE_MASTER_3_SET", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster3Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster3Toggle, Name = "ENGINE_MASTER_3_TOGGLE", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster4Set, Name = "ENGINE_MASTER_4_SET", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMaster4Toggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMaster4Toggle, Name = "ENGINE_MASTER_4_TOGGLE", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMasterSet { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMasterSet, Name = "ENGINE_MASTER_SET", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineMasterToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineMasterToggle, Name = "ENGINE_MASTER_TOGGLE", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineModeCrankSet { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineModeCrankSet, Name = "ENGINE_MODE_CRANK_SET", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineModeIgnStart { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineModeIgnStart, Name = "ENGINE_MODE_IGN_START", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EngineModeNormSet { get { return new SimConnectEvent() { Id = SimConnectEventId.EngineModeNormSet, Name = "ENGINE_MODE_NORM_SET", Units = "N/A", Description = "Toggles the indexed engine as a source to the bleed air system. Note that if you supply 0 instead of a single engine index, then the event will target all engines." }; } }
        private SimConnectEvent EnginePrimer { get { return new SimConnectEvent() { Id = SimConnectEventId.EnginePrimer, Name = "ENGINE_PRIMER", Units = "N/A", Description = "Trigger engine primers" }; } }
        private SimConnectEvent Exit { get { return new SimConnectEvent() { Id = SimConnectEventId.Exit, Name = "EXIT", Units = "N/A", Description = "Quit Microsoft Flight Simulator with a message" }; } }
        private SimConnectEvent ExternalSystemSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ExternalSystemSet, Name = "EXTERNAL_SYSTEM_SET", Units = "[0]: Value", Description = "Generic key event to set a number value." }; } }
        private SimConnectEvent ExternalSystemToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ExternalSystemToggle, Name = "EXTERNAL_SYSTEM_TOGGLE", Units = "N/A", Description = "Generic key event to toggle a value on/off (true/false)." }; } }
        private SimConnectEvent ExtinguishEngineFire { get { return new SimConnectEvent() { Id = SimConnectEventId.ExtinguishEngineFire, Name = "EXTINGUISH_ENGINE_FIRE", Units = "[0]: combined index (see description)", Description = "This key event requires a two digit number for parameter [0]. The first digit represents the fire extinguisher index to use, and the second represents the engine index. For example, a value of 11 would represent using bottle 1 on engine 1. 21 would repres" }; } }
        private SimConnectEvent EyepointBack { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointBack, Name = "EYEPOINT_BACK", Units = "N/A", Description = "Move eyepoint backward" }; } }
        private SimConnectEvent EyepointDown { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointDown, Name = "EYEPOINT_DOWN", Units = "N/A", Description = "Move eyepoint down" }; } }
        private SimConnectEvent EyepointForward { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointForward, Name = "EYEPOINT_FORWARD", Units = "N/A", Description = "Move eyepoint forward" }; } }
        private SimConnectEvent EyepointLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointLeft, Name = "EYEPOINT_LEFT", Units = "N/A", Description = "Move eyepoint left" }; } }
        private SimConnectEvent EyepointReset { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointReset, Name = "EYEPOINT_RESET", Units = "N/A", Description = "Move eyepoint to default position" }; } }
        private SimConnectEvent EyepointRight { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointRight, Name = "EYEPOINT_RIGHT", Units = "N/A", Description = "Move eyepoint right" }; } }
        private SimConnectEvent EyepointUp { get { return new SimConnectEvent() { Id = SimConnectEventId.EyepointUp, Name = "EYEPOINT_UP", Units = "N/A", Description = "Move eyepoint up" }; } }
        private SimConnectEvent FireAllGuns { get { return new SimConnectEvent() { Id = SimConnectEventId.FireAllGuns, Name = "FIRE_ALL_GUNS", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent FirePrimaryGuns { get { return new SimConnectEvent() { Id = SimConnectEventId.FirePrimaryGuns, Name = "FIRE_PRIMARY_GUNS", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent FireSecondaryGuns { get { return new SimConnectEvent() { Id = SimConnectEventId.FireSecondaryGuns, Name = "FIRE_SECONDARY_GUNS", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent Flaps1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Flaps1, Name = "FLAPS_1", Units = "N/A", Description = "Sets flap handle to first extension position" }; } }
        private SimConnectEvent Flaps2 { get { return new SimConnectEvent() { Id = SimConnectEventId.Flaps2, Name = "FLAPS_2", Units = "N/A", Description = "Sets flap handle to second extension position" }; } }
        private SimConnectEvent Flaps3 { get { return new SimConnectEvent() { Id = SimConnectEventId.Flaps3, Name = "FLAPS_3", Units = "N/A", Description = "Sets flap handle to third extension position" }; } }
        private SimConnectEvent Flaps4 { get { return new SimConnectEvent() { Id = SimConnectEventId.Flaps4, Name = "FLAPS_4", Units = "N/A", Description = "Sets flap handle to fourth extension position" }; } }
        private SimConnectEvent FlapsContinuousDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsContinuousDecr, Name = "FLAPS_CONTINUOUS_DECR", Units = "N/A", Description = "Sets flap handle to fourth extension position" }; } }
        private SimConnectEvent FlapsContinuousIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsContinuousIncr, Name = "FLAPS_CONTINUOUS_INCR", Units = "N/A", Description = "Sets flap handle to fourth extension position" }; } }
        private SimConnectEvent FlapsContinuousSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsContinuousSet, Name = "FLAPS_CONTINUOUS_SET", Units = "N/A", Description = "Sets flap handle to fourth extension position" }; } }
        private SimConnectEvent FlapsDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsDecr, Name = "FLAPS_DECR", Units = "N/A", Description = "Decrements flap handle position" }; } }
        private SimConnectEvent FlapsDown { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsDown, Name = "FLAPS_DOWN", Units = "N/A", Description = "Sets flap handle to full extension position" }; } }
        private SimConnectEvent FlapsIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsIncr, Name = "FLAPS_INCR", Units = "N/A", Description = "Increments flap handle position" }; } }
        private SimConnectEvent FlapsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsSet, Name = "FLAPS_SET", Units = "N/A", Description = "Sets flap handle to closest increment (0 to 16383)" }; } }
        private SimConnectEvent FlapsUp { get { return new SimConnectEvent() { Id = SimConnectEventId.FlapsUp, Name = "FLAPS_UP", Units = "N/A", Description = "Sets flap handle to full retract position" }; } }
        private SimConnectEvent FlightLevelChange { get { return new SimConnectEvent() { Id = SimConnectEventId.FlightLevelChange, Name = "FLIGHT_LEVEL_CHANGE", Units = "N/A", Description = "Toggles the autopilot FLC mode on or off. When on, the AP will adjust the engine power to try and fly the aircraft at a pitch attitude corresponding to the desired flight profile (climb or descent), while maintaining the airspeed reference." }; } }
        private SimConnectEvent FlightLevelChangeOff { get { return new SimConnectEvent() { Id = SimConnectEventId.FlightLevelChangeOff, Name = "FLIGHT_LEVEL_CHANGE_OFF", Units = "N/A", Description = "Turns off the autopilot FLC mode." }; } }
        private SimConnectEvent FlightLevelChangeOn { get { return new SimConnectEvent() { Id = SimConnectEventId.FlightLevelChangeOn, Name = "FLIGHT_LEVEL_CHANGE_ON", Units = "N/A", Description = "Turn on the autopilot FLC mode. This mode adjusts engine power to fly the aircraft at a pitch attitude corresponding to the desired flight profile (climb or descent), while maintaining the airspeed reference." }; } }
        private SimConnectEvent FlightMap { get { return new SimConnectEvent() { Id = SimConnectEventId.FlightMap, Name = "FLIGHT_MAP", Units = "N/A", Description = "Brings up flight map" }; } }
        private SimConnectEvent FlyByWireElacToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FlyByWireElacToggle, Name = "FLY_BY_WIRE_ELAC_TOGGLE", Units = "N/A", Description = "Turn on or off the fly by wire Elevators and Ailerons computer." }; } }
        private SimConnectEvent FlyByWireFacToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FlyByWireFacToggle, Name = "FLY_BY_WIRE_FAC_TOGGLE", Units = "N/A", Description = "Turn on or off the fly by wire Flight Augmentation computer." }; } }
        private SimConnectEvent FlyByWireSecToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FlyByWireSecToggle, Name = "FLY_BY_WIRE_SEC_TOGGLE", Units = "N/A", Description = "Turn on or off the fly by wire Spoilers and Elevators computer." }; } }
        private SimConnectEvent ForceEnd { get { return new SimConnectEvent() { Id = SimConnectEventId.ForceEnd, Name = "FORCE_END", Units = "N/A", Description = "Brings up flight map" }; } }
        private SimConnectEvent FreezeAltitudeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FreezeAltitudeSet, Name = "FREEZE_ALTITUDE_SET", Units = "N/A", Description = "Freezes the altitude of the aircraft." }; } }
        private SimConnectEvent FreezeAltitudeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FreezeAltitudeToggle, Name = "FREEZE_ALTITUDE_TOGGLE", Units = "N/A", Description = "Turns the freezing of the altitude of the aircraft on or off." }; } }
        private SimConnectEvent FreezeAttitudeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FreezeAttitudeSet, Name = "FREEZE_ATTITUDE_SET", Units = "N/A", Description = "Freezes the attitude (pitch, bank and heading) of the aircraft." }; } }
        private SimConnectEvent FreezeAttitudeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FreezeAttitudeToggle, Name = "FREEZE_ATTITUDE_TOGGLE", Units = "N/A", Description = "Turns the freezing of the attitude (pitch, bank and heading) of the aircraft on or off." }; } }
        private SimConnectEvent FreezeLatitudeLongitudeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FreezeLatitudeLongitudeSet, Name = "FREEZE_LATITUDE_LONGITUDE_SET", Units = "N/A", Description = "Freezes the lat/lon position of the aircraft." }; } }
        private SimConnectEvent FreezeLatitudeLongitudeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FreezeLatitudeLongitudeToggle, Name = "FREEZE_LATITUDE_LONGITUDE_TOGGLE", Units = "N/A", Description = "Turns the freezing of the lat/lon position of the aircraft (either user or AI controlled) on or off. If this key event is set, it means that the latitude and longitude of the aircraft are not being controlled by ESP, so enabling, for example, a SimConnect" }; } }
        private SimConnectEvent FrequencySwap { get { return new SimConnectEvent() { Id = SimConnectEventId.FrequencySwap, Name = "FREQUENCY_SWAP", Units = "N/A", Description = "Swaps frequency with standby on whichever NAV or COM radio is selected." }; } }
        private SimConnectEvent FuelDumpSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelDumpSwitchSet, Name = "FUEL_DUMP_SWITCH_SET", Units = "", Description = "Set to 1 (TRUE) or 0 (FALSE). The switch can only be set to TRUE if fuel_dump_rate is specified in the aircraft configuration file, indicating that a fuel dump system exists.\r\nThis key is only useful when using the legacy [FUEL] system." }; } }
        private SimConnectEvent FuelDumpToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelDumpToggle, Name = "FUEL_DUMP_TOGGLE", Units = "", Description = "Used to turn on (1, TRUE) or off (0, FALSE) the fuel dump switch.\r\nThis key is only useful when using the legacy [FUEL] system." }; } }
        private SimConnectEvent FuelPump { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelPump, Name = "FUEL_PUMP", Units = "N/A", Description = "Toggle electric fuel pumps" }; } }
        private SimConnectEvent FuelSelector1Crossfeed { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector1Crossfeed, Name = "FUEL_SELECTOR_1_CROSSFEED", Units = "N/A", Description = "Sets fuel selector 1 to \"Crossfeed\"." }; } }
        private SimConnectEvent FuelSelector1Isolate { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector1Isolate, Name = "FUEL_SELECTOR_1_ISOLATE", Units = "N/A", Description = "Sets fuel selector 1 to \"Isolate\"." }; } }
        private SimConnectEvent FuelSelector2All { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2All, Name = "FUEL_SELECTOR_2_ALL", Units = "N/A", Description = "Turns selector 2 to ALL position." }; } }
        private SimConnectEvent FuelSelector2Center { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Center, Name = "FUEL_SELECTOR_2_CENTER", Units = "N/A", Description = "Turns selector 2 to CENTER position." }; } }
        private SimConnectEvent FuelSelector2Crossfeed { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Crossfeed, Name = "FUEL_SELECTOR_2_CROSSFEED", Units = "N/A", Description = "Sets fuel selector 2 to \"Crossfeed\"." }; } }
        private SimConnectEvent FuelSelector2Isolate { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Isolate, Name = "FUEL_SELECTOR_2_ISOLATE", Units = "N/A", Description = "Sets fuel selector 2 to \"Isolate\"." }; } }
        private SimConnectEvent FuelSelector2Left { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Left, Name = "FUEL_SELECTOR_2_LEFT", Units = "N/A", Description = "Turns selector 2 to LEFT position (fuel will be retrieved from Left Tip then Left Aux then Left Main)." }; } }
        private SimConnectEvent FuelSelector2LeftAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2LeftAux, Name = "FUEL_SELECTOR_2_LEFT_AUX", Units = "N/A", Description = "Turns selector 2 to LEFT AUX position." }; } }
        private SimConnectEvent FuelSelector2LeftMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2LeftMain, Name = "FUEL_SELECTOR_2_LEFT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 2 to the left Main tank." }; } }
        private SimConnectEvent FuelSelector2Off { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Off, Name = "FUEL_SELECTOR_2_OFF", Units = "N/A", Description = "Turns selector 2 to OFF position." }; } }
        private SimConnectEvent FuelSelector2Right { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Right, Name = "FUEL_SELECTOR_2_RIGHT", Units = "N/A", Description = "Turns selector 2 to RIGHT position (fuel will be retrieved from Right Tip then Right Aux then Right Main)" }; } }
        private SimConnectEvent FuelSelector2RightAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2RightAux, Name = "FUEL_SELECTOR_2_RIGHT_AUX", Units = "N/A", Description = "Turns selector 2 to RIGHT AUX position." }; } }
        private SimConnectEvent FuelSelector2RightMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2RightMain, Name = "FUEL_SELECTOR_2_RIGHT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 2 to the right Main tank." }; } }
        private SimConnectEvent FuelSelector2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector2Set, Name = "FUEL_SELECTOR_2_SET", Units = "N/A", Description = "Sets selector 2 position (see the Fuel Selector Codes list for the correct code to use)." }; } }
        private SimConnectEvent FuelSelector3All { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3All, Name = "FUEL_SELECTOR_3_ALL", Units = "N/A", Description = "Turns selector 3 to ALL position." }; } }
        private SimConnectEvent FuelSelector3Center { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Center, Name = "FUEL_SELECTOR_3_CENTER", Units = "N/A", Description = "Turns selector 3 to CENTER position." }; } }
        private SimConnectEvent FuelSelector3Crossfeed { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Crossfeed, Name = "FUEL_SELECTOR_3_CROSSFEED", Units = "N/A", Description = "Sets fuel selector 3 to \"Crossfeed\"." }; } }
        private SimConnectEvent FuelSelector3Isolate { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Isolate, Name = "FUEL_SELECTOR_3_ISOLATE", Units = "N/A", Description = "Sets fuel selector 3 to \"Isolate\"." }; } }
        private SimConnectEvent FuelSelector3Left { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Left, Name = "FUEL_SELECTOR_3_LEFT", Units = "N/A", Description = "Turns selector 3 to LEFT position (fuel will be retrieved from Left Tip then Left Aux then Left Main)." }; } }
        private SimConnectEvent FuelSelector3LeftAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3LeftAux, Name = "FUEL_SELECTOR_3_LEFT_AUX", Units = "N/A", Description = "Turns selector 3 to LEFT AUX position." }; } }
        private SimConnectEvent FuelSelector3LeftMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3LeftMain, Name = "FUEL_SELECTOR_3_LEFT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 3 to the left Main tank." }; } }
        private SimConnectEvent FuelSelector3Off { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Off, Name = "FUEL_SELECTOR_3_OFF", Units = "N/A", Description = "Turns selector 3 to OFF position." }; } }
        private SimConnectEvent FuelSelector3Right { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Right, Name = "FUEL_SELECTOR_3_RIGHT", Units = "N/A", Description = "Turns selector 3 to RIGHT position (fuel will be retrieved from Right Tip then Right Aux then Right Main)." }; } }
        private SimConnectEvent FuelSelector3RightAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3RightAux, Name = "FUEL_SELECTOR_3_RIGHT_AUX", Units = "N/A", Description = "Turns selector 3 to RIGHT AUX position." }; } }
        private SimConnectEvent FuelSelector3RightMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3RightMain, Name = "FUEL_SELECTOR_3_RIGHT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 3 to the right Main tank." }; } }
        private SimConnectEvent FuelSelector3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector3Set, Name = "FUEL_SELECTOR_3_SET", Units = "N/A", Description = "Sets selector 3 position (see the Fuel Selector Codes list for the correct code to use)." }; } }
        private SimConnectEvent FuelSelector4All { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4All, Name = "FUEL_SELECTOR_4_ALL", Units = "N/A", Description = "Turns selector 4 to ALL position." }; } }
        private SimConnectEvent FuelSelector4Center { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Center, Name = "FUEL_SELECTOR_4_CENTER", Units = "N/A", Description = "Turns selector 4 to CENTER position." }; } }
        private SimConnectEvent FuelSelector4Crossfeed { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Crossfeed, Name = "FUEL_SELECTOR_4_CROSSFEED", Units = "N/A", Description = "Sets fuel selector 4 to \"Crossfeed\"." }; } }
        private SimConnectEvent FuelSelector4Isolate { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Isolate, Name = "FUEL_SELECTOR_4_ISOLATE", Units = "N/A", Description = "Sets fuel selector 4 to \"Isolate\"." }; } }
        private SimConnectEvent FuelSelector4Left { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Left, Name = "FUEL_SELECTOR_4_LEFT", Units = "N/A", Description = "Turns selector 4 to LEFT position (fuel will be retrieved from Left Tip then Left Aux then Left Main)." }; } }
        private SimConnectEvent FuelSelector4LeftAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4LeftAux, Name = "FUEL_SELECTOR_4_LEFT_AUX", Units = "N/A", Description = "Turns selector 4 to LEFT AUX position." }; } }
        private SimConnectEvent FuelSelector4LeftMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4LeftMain, Name = "FUEL_SELECTOR_4_LEFT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 4 to the left Main tank." }; } }
        private SimConnectEvent FuelSelector4Off { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Off, Name = "FUEL_SELECTOR_4_OFF", Units = "N/A", Description = "Turns selector 4 to OFF position." }; } }
        private SimConnectEvent FuelSelector4Right { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Right, Name = "FUEL_SELECTOR_4_RIGHT", Units = "N/A", Description = "Turns selector 4 to RIGHT position (fuel will be retrieved from Right Tip then Right Aux then Right Main)." }; } }
        private SimConnectEvent FuelSelector4RightAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4RightAux, Name = "FUEL_SELECTOR_4_RIGHT_AUX", Units = "N/A", Description = "Turns selector 4 to RIGHT AUX position." }; } }
        private SimConnectEvent FuelSelector4RightMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4RightMain, Name = "FUEL_SELECTOR_4_RIGHT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 4 to the right Main tank." }; } }
        private SimConnectEvent FuelSelector4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelector4Set, Name = "FUEL_SELECTOR_4_SET", Units = "N/A", Description = "Sets selector 4 position (see the Fuel Selector Codes list for the correct code to use)." }; } }
        private SimConnectEvent FuelSelectorAll { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorAll, Name = "FUEL_SELECTOR_ALL", Units = "N/A", Description = "Turn fuel selector 1 to the ALL position." }; } }
        private SimConnectEvent FuelSelectorCenter { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorCenter, Name = "FUEL_SELECTOR_CENTER", Units = "N/A", Description = "Turns selector 1 to CENTER position." }; } }
        private SimConnectEvent FuelSelectorLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorLeft, Name = "FUEL_SELECTOR_LEFT", Units = "N/A", Description = "Turns selector 1 to LEFT position (fuel will be retrieved from Left Tip then Left Aux then Left Main)." }; } }
        private SimConnectEvent FuelSelectorLeftAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorLeftAux, Name = "FUEL_SELECTOR_LEFT_AUX", Units = "N/A", Description = "Turns selector 1 to LEFT AUX position." }; } }
        private SimConnectEvent FuelSelectorLeftMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorLeftMain, Name = "FUEL_SELECTOR_LEFT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 1 to the left Main tank." }; } }
        private SimConnectEvent FuelSelectorOff { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorOff, Name = "FUEL_SELECTOR_OFF", Units = "N/A", Description = "Turn fuel selector 1 to the OFF position." }; } }
        private SimConnectEvent FuelSelectorRight { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorRight, Name = "FUEL_SELECTOR_RIGHT", Units = "N/A", Description = "Turns selector 1 to RIGHT position (fuel will be retrieved from Right Tip then Right Aux then Right Main)." }; } }
        private SimConnectEvent FuelSelectorRightAux { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorRightAux, Name = "FUEL_SELECTOR_RIGHT_AUX", Units = "N/A", Description = "Turns selector 1 to RIGHT AUX position." }; } }
        private SimConnectEvent FuelSelectorRightMain { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorRightMain, Name = "FUEL_SELECTOR_RIGHT_MAIN", Units = "N/A", Description = "Sets the fuel selector for engine 1 to the right Main tank." }; } }
        private SimConnectEvent FuelSelectorSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelSelectorSet, Name = "FUEL_SELECTOR_SET", Units = "N/A", Description = "Sets selector 1 position (see the Fuel Selector Codes list for the correct code to use)." }; } }
        private SimConnectEvent FuelTransferCustomIndexToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelTransferCustomIndexToggle, Name = "FUEL_TRANSFER_CUSTOM_INDEX_TOGGLE", Units = "[0]: Valve Index", Description = "Toggle a custom fuel transfer pump on/off. The index is the Pump ID value supplied as part of the fuel pump definition for the fuel_transfer_pump.N parameter in the flight_model.cfg file." }; } }
        private SimConnectEvent FuelsystemJunctionSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemJunctionSet, Name = "FUELSYSTEM_JUNCTION_SET", Units = "[0]: Junction Index\r\n[1]: Option index", Description = "Set the current junction options for which lines are open or closed at any given time. This event requires two parameters: the first is the index of the junction (as defined by the N index of the Junction.N parameter), and the second is the Option index, " }; } }
        private SimConnectEvent FuelsystemPumpOff { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemPumpOff, Name = "FUELSYSTEM_PUMP_OFF", Units = "[0]: Pump Index", Description = "Turn a fuel pump off. The event requires the N index of the Pump.N parameter to define the pump to use." }; } }
        private SimConnectEvent FuelsystemPumpOn { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemPumpOn, Name = "FUELSYSTEM_PUMP_ON", Units = "[0]: Pump Index", Description = "Turn a fuel pump on. The event requires the N index of the Pump.N parameter to define the pump to use." }; } }
        private SimConnectEvent FuelsystemPumpSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemPumpSet, Name = "FUELSYSTEM_PUMP_SET", Units = "[0]: Pump Index\r\n[1]: Status\r\n    0 = Off\r\n    1 = On\r\n    2 = Auto", Description = "Set a fuel pump to be either on or off or auto. The event requires the N index of the Pump.N parameter to define the pump to use." }; } }
        private SimConnectEvent FuelsystemPumpToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemPumpToggle, Name = "FUELSYSTEM_PUMP_TOGGLE", Units = "[0]: Pump Index", Description = "Toggle a fuel pump on/off. The event requires the N index of the Pump.N parameter to define the pump to use." }; } }
        private SimConnectEvent FuelsystemTriggerOff { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemTriggerOff, Name = "FUELSYSTEM_TRIGGER_OFF", Units = "[0]: Trigger Index", Description = "Turn a trigger event off. The event requires the N index of the Trigger.N parameter to define the trigger to switch off." }; } }
        private SimConnectEvent FuelsystemTriggerOn { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemTriggerOn, Name = "FUELSYSTEM_TRIGGER_ON", Units = "[0]: Trigger Index", Description = "Turn a trigger event on. The event requires the N index of the Trigger.N parameter to define the trigger to switch off." }; } }
        private SimConnectEvent FuelsystemTriggerSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemTriggerSet, Name = "FUELSYSTEM_TRIGGER_SET", Units = "[0]: Trigger Index\r\n[1]: Status, either on (1) or off (0)", Description = "Set a trigger event to be either on or off. The event requires the N index of the Trigger.N parameter to define the trigger to switch off." }; } }
        private SimConnectEvent FuelsystemTriggerToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemTriggerToggle, Name = "FUELSYSTEM_TRIGGER_TOGGLE", Units = "[0]: Trigger Index", Description = "Toggle a trigger event on/off. The event requires the N index of the Trigger.N parameter to define the trigger to switch off." }; } }
        private SimConnectEvent FuelsystemValveClose { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemValveClose, Name = "FUELSYSTEM_VALVE_CLOSE", Units = "[0]: Valve Index", Description = "Close a specific valve in the fuel system. The event requires the N index of the Valve.N parameter to define the valve to target." }; } }
        private SimConnectEvent FuelsystemValveOpen { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemValveOpen, Name = "FUELSYSTEM_VALVE_OPEN", Units = "[0]: Valve Index", Description = "Open a specific valve in the fuel system. The event requires the N index of the Valve.N parameter to define the valve to target." }; } }
        private SimConnectEvent FuelsystemValveSet { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemValveSet, Name = "FUELSYSTEM_VALVE_SET", Units = "[0]: Valve Index\r\n[1]: Status, either open (1) or closed (0)", Description = "Set a valve to be either open or closed. The event requires the N index of the Valve.N parameter to define the valve to target." }; } }
        private SimConnectEvent FuelsystemValveToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FuelsystemValveToggle, Name = "FUELSYSTEM_VALVE_TOGGLE", Units = "[0]: Valve Index", Description = "Toggle a valve open/closed. The event requires the N index of the Valve.N parameter to define the valve to target." }; } }
        private SimConnectEvent FullWindowToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.FullWindowToggle, Name = "FULL_WINDOW_TOGGLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent GLimiterOff { get { return new SimConnectEvent() { Id = SimConnectEventId.GLimiterOff, Name = "G_LIMITER_OFF", Units = "N/A", Description = "Turns Flying Tips on/off" }; } }
        private SimConnectEvent GLimiterOn { get { return new SimConnectEvent() { Id = SimConnectEventId.GLimiterOn, Name = "G_LIMITER_ON", Units = "N/A", Description = "Turns Flying Tips on/off" }; } }
        private SimConnectEvent GLimiterSet { get { return new SimConnectEvent() { Id = SimConnectEventId.GLimiterSet, Name = "G_LIMITER_SET", Units = "N/A", Description = "Turns Flying Tips on/off" }; } }
        private SimConnectEvent GLimiterToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.GLimiterToggle, Name = "G_LIMITER_TOGGLE", Units = "N/A", Description = "Turns Flying Tips on/off" }; } }
        private SimConnectEvent G1000MfdClearButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdClearButton, Name = "G1000_MFD_CLEAR_BUTTON", Units = "N/A", Description = "Clears the current input." }; } }
        private SimConnectEvent G1000MfdCursorButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdCursorButton, Name = "G1000_MFD_CURSOR_BUTTON", Units = "N/A", Description = "Toggles on or off a screen cursor." }; } }
        private SimConnectEvent G1000MfdDirecttoButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdDirecttoButton, Name = "G1000_MFD_DIRECTTO_BUTTON", Units = "N/A", Description = "Turn to the Direct To page." }; } }
        private SimConnectEvent G1000MfdEnterButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdEnterButton, Name = "G1000_MFD_ENTER_BUTTON", Units = "N/A", Description = "Enters the current input." }; } }
        private SimConnectEvent G1000MfdFlightplanButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdFlightplanButton, Name = "G1000_MFD_FLIGHTPLAN_BUTTON", Units = "N/A", Description = "The multi-function display (MFD) should display its current flight plan." }; } }
        private SimConnectEvent G1000MfdGroupKnobDec { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdGroupKnobDec, Name = "G1000_MFD_GROUP_KNOB_DEC", Units = "N/A", Description = "Step down through the page groups." }; } }
        private SimConnectEvent G1000MfdGroupKnobInc { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdGroupKnobInc, Name = "G1000_MFD_GROUP_KNOB_INC", Units = "N/A", Description = "Step up through the page groups." }; } }
        private SimConnectEvent G1000MfdMenuButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdMenuButton, Name = "G1000_MFD_MENU_BUTTON", Units = "N/A", Description = "If a segmented flight plan is highlighted, activates the associated menu." }; } }
        private SimConnectEvent G1000MfdPageKnobDec { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdPageKnobDec, Name = "G1000_MFD_PAGE_KNOB_DEC", Units = "N/A", Description = "Step down through the individual pages." }; } }
        private SimConnectEvent G1000MfdPageKnobInc { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdPageKnobInc, Name = "G1000_MFD_PAGE_KNOB_INC", Units = "N/A", Description = "Step up through the individual pages." }; } }
        private SimConnectEvent G1000MfdProcedureButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdProcedureButton, Name = "G1000_MFD_PROCEDURE_BUTTON", Units = "N/A", Description = "Turn to the Procedure page." }; } }
        private SimConnectEvent G1000MfdSoftkey1 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey1, Name = "G1000_MFD_SOFTKEY1", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey10 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey10, Name = "G1000_MFD_SOFTKEY10", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey11 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey11, Name = "G1000_MFD_SOFTKEY11", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey12 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey12, Name = "G1000_MFD_SOFTKEY12", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey2 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey2, Name = "G1000_MFD_SOFTKEY2", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey3 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey3, Name = "G1000_MFD_SOFTKEY3", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey4 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey4, Name = "G1000_MFD_SOFTKEY4", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey5 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey5, Name = "G1000_MFD_SOFTKEY5", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey6 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey6, Name = "G1000_MFD_SOFTKEY6", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey7 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey7, Name = "G1000_MFD_SOFTKEY7", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey8 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey8, Name = "G1000_MFD_SOFTKEY8", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdSoftkey9 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdSoftkey9, Name = "G1000_MFD_SOFTKEY9", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000MfdZoominButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdZoominButton, Name = "G1000_MFD_ZOOMIN_BUTTON", Units = "N/A", Description = "Zoom in on the current map." }; } }
        private SimConnectEvent G1000MfdZoomoutButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000MfdZoomoutButton, Name = "G1000_MFD_ZOOMOUT_BUTTON", Units = "N/A", Description = "Zoom out on the current map." }; } }
        private SimConnectEvent G1000PfdClearButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdClearButton, Name = "G1000_PFD_CLEAR_BUTTON", Units = "N/A", Description = "Clears the current input." }; } }
        private SimConnectEvent G1000PfdCursorButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdCursorButton, Name = "G1000_PFD_CURSOR_BUTTON", Units = "N/A", Description = "Turns on or off a screen cursor." }; } }
        private SimConnectEvent G1000PfdDirecttoButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdDirecttoButton, Name = "G1000_PFD_DIRECTTO_BUTTON", Units = "N/A", Description = "Turn to the Direct To page." }; } }
        private SimConnectEvent G1000PfdEnterButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdEnterButton, Name = "G1000_PFD_ENTER_BUTTON", Units = "N/A", Description = "Enters the current input." }; } }
        private SimConnectEvent G1000PfdFlightplanButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdFlightplanButton, Name = "G1000_PFD_FLIGHTPLAN_BUTTON", Units = "N/A", Description = "The primary flight display (PFD) should display its current flight plan." }; } }
        private SimConnectEvent G1000PfdGroupKnobDec { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdGroupKnobDec, Name = "G1000_PFD_GROUP_KNOB_DEC", Units = "N/A", Description = "Step down through the page groups." }; } }
        private SimConnectEvent G1000PfdGroupKnobInc { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdGroupKnobInc, Name = "G1000_PFD_GROUP_KNOB_INC", Units = "N/A", Description = "Step up through the page groups." }; } }
        private SimConnectEvent G1000PfdMenuButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdMenuButton, Name = "G1000_PFD_MENU_BUTTON", Units = "N/A", Description = "If a segmented flight plan is highlighted, activates the associated menu." }; } }
        private SimConnectEvent G1000PfdPageKnobDec { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdPageKnobDec, Name = "G1000_PFD_PAGE_KNOB_DEC", Units = "N/A", Description = "Step down through the individual pages." }; } }
        private SimConnectEvent G1000PfdPageKnobInc { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdPageKnobInc, Name = "G1000_PFD_PAGE_KNOB_INC", Units = "N/A", Description = "Step up through the individual pages." }; } }
        private SimConnectEvent G1000PfdProcedureButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdProcedureButton, Name = "G1000_PFD_PROCEDURE_BUTTON", Units = "N/A", Description = "Turn to the Procedure page." }; } }
        private SimConnectEvent G1000PfdSoftkey1 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey1, Name = "G1000_PFD_SOFTKEY1", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey10 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey10, Name = "G1000_PFD_SOFTKEY10", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey11 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey11, Name = "G1000_PFD_SOFTKEY11", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey12 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey12, Name = "G1000_PFD_SOFTKEY12", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey2 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey2, Name = "G1000_PFD_SOFTKEY2", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey3 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey3, Name = "G1000_PFD_SOFTKEY3", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey4 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey4, Name = "G1000_PFD_SOFTKEY4", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey5 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey5, Name = "G1000_PFD_SOFTKEY5", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey6 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey6, Name = "G1000_PFD_SOFTKEY6", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey7 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey7, Name = "G1000_PFD_SOFTKEY7", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey8 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey8, Name = "G1000_PFD_SOFTKEY8", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdSoftkey9 { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdSoftkey9, Name = "G1000_PFD_SOFTKEY9", Units = "N/A", Description = "Initiate the action for the icon displayed in the softkey position." }; } }
        private SimConnectEvent G1000PfdZoominButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdZoominButton, Name = "G1000_PFD_ZOOMIN_BUTTON", Units = "N/A", Description = "Zoom in on the current map." }; } }
        private SimConnectEvent G1000PfdZoomoutButton { get { return new SimConnectEvent() { Id = SimConnectEventId.G1000PfdZoomoutButton, Name = "G1000_PFD_ZOOMOUT_BUTTON", Units = "N/A", Description = "Zoom out on the current map." }; } }
        private SimConnectEvent GaugeKeystroke { get { return new SimConnectEvent() { Id = SimConnectEventId.GaugeKeystroke, Name = "GAUGE_KEYSTROKE", Units = "N/A", Description = "Enables a keystroke to be sent to a gauge that is in focus. The keystrokes can only be in the range 0 to 9, A to Z, and the four keys: plus, minus, comma and period. This is typically used to allow some keyboard entry to a complex device such as a GPS to " }; } }
        private SimConnectEvent GearDown { get { return new SimConnectEvent() { Id = SimConnectEventId.GearDown, Name = "GEAR_DOWN", Units = "N/A", Description = "Sets gear handle in DOWN position" }; } }
        private SimConnectEvent GearEmergencyHandleToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.GearEmergencyHandleToggle, Name = "GEAR_EMERGENCY_HANDLE_TOGGLE", Units = "N/A", Description = "Sets gear handle in DOWN position" }; } }
        private SimConnectEvent GearPump { get { return new SimConnectEvent() { Id = SimConnectEventId.GearPump, Name = "GEAR_PUMP", Units = "N/A", Description = "Increments emergency gear extension" }; } }
        private SimConnectEvent GearSet { get { return new SimConnectEvent() { Id = SimConnectEventId.GearSet, Name = "GEAR_SET", Units = "[0]: Position", Description = "Sets gear handle position up/down (0,1)" }; } }
        private SimConnectEvent GearToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.GearToggle, Name = "GEAR_TOGGLE", Units = "N/A", Description = "Toggle gear handle" }; } }
        private SimConnectEvent GearUp { get { return new SimConnectEvent() { Id = SimConnectEventId.GearUp, Name = "GEAR_UP", Units = "N/A", Description = "Sets gear handle in UP position" }; } }
        private SimConnectEvent GlareshieldLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.GlareshieldLightsOff, Name = "GLARESHIELD_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn glareshield lights off" }; } }
        private SimConnectEvent GlareshieldLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.GlareshieldLightsOn, Name = "GLARESHIELD_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn glareshield lights on" }; } }
        private SimConnectEvent GlareshieldLightsPowerSettingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.GlareshieldLightsPowerSettingSet, Name = "GLARESHIELD_LIGHTS_POWER_SETTING_SET", Units = "[0]: glareshield light circuit index\r\n[1]: power setting (%)", Description = "Set glareshield light circuit power setting. Takes two indices, the circuit and the power setting (see SimVars And Keys for more information). The index is the value assigned to the circuit Type when the circuit.N was defined." }; } }
        private SimConnectEvent GlareshieldLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.GlareshieldLightsSet, Name = "GLARESHIELD_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set glareshield lights on/off (1,0)" }; } }
        private SimConnectEvent GlareshieldLightsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.GlareshieldLightsToggle, Name = "GLARESHIELD_LIGHTS_TOGGLE", Units = "[0]: light circuit index", Description = "Toggle glareshield lights" }; } }
        private SimConnectEvent GpsActivateButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsActivateButton, Name = "GPS_ACTIVATE_BUTTON", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent GpsButton1 { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsButton1, Name = "GPS_BUTTON1", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent GpsButton2 { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsButton2, Name = "GPS_BUTTON2", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent GpsButton3 { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsButton3, Name = "GPS_BUTTON3", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent GpsButton4 { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsButton4, Name = "GPS_BUTTON4", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent GpsButton5 { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsButton5, Name = "GPS_BUTTON5", Units = "N/A", Description = "Toggles the ELT between on (1) and off (0)." }; } }
        private SimConnectEvent GpsClearAllButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsClearAllButton, Name = "GPS_CLEAR_ALL_BUTTON", Units = "N/A", Description = "Clears all data immediately" }; } }
        private SimConnectEvent GpsClearButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsClearButton, Name = "GPS_CLEAR_BUTTON", Units = "N/A", Description = "Clears entered data on a page" }; } }
        private SimConnectEvent GpsClearButtonDown { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsClearButtonDown, Name = "GPS_CLEAR_BUTTON_DOWN", Units = "N/A", Description = "Triggers the pressing of the Clear button" }; } }
        private SimConnectEvent GpsClearButtonUp { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsClearButtonUp, Name = "GPS_CLEAR_BUTTON_UP", Units = "N/A", Description = "Triggers the release of the Clear button." }; } }
        private SimConnectEvent GpsCursorButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsCursorButton, Name = "GPS_CURSOR_BUTTON", Units = "N/A", Description = "Selects GPS cursor" }; } }
        private SimConnectEvent GpsDirecttoButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsDirecttoButton, Name = "GPS_DIRECTTO_BUTTON", Units = "N/A", Description = "Brings up the \"Direct To\" page" }; } }
        private SimConnectEvent GpsEnterButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsEnterButton, Name = "GPS_ENTER_BUTTON", Units = "N/A", Description = "Approves entered data." }; } }
        private SimConnectEvent GpsFlightplanButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsFlightplanButton, Name = "GPS_FLIGHTPLAN_BUTTON", Units = "N/A", Description = "Displays the programmed flightplan." }; } }
        private SimConnectEvent GpsGroupKnobDec { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsGroupKnobDec, Name = "GPS_GROUP_KNOB_DEC", Units = "N/A", Description = "Decrements cursor." }; } }
        private SimConnectEvent GpsGroupKnobInc { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsGroupKnobInc, Name = "GPS_GROUP_KNOB_INC", Units = "N/A", Description = "Increments cursor." }; } }
        private SimConnectEvent GpsMenuButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsMenuButton, Name = "GPS_MENU_BUTTON", Units = "N/A", Description = "Brings up page to select active legs in a flightplan." }; } }
        private SimConnectEvent GpsMsgButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsMsgButton, Name = "GPS_MSG_BUTTON", Units = "N/A", Description = "Toggles the Message Page." }; } }
        private SimConnectEvent GpsMsgButtonDown { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsMsgButtonDown, Name = "GPS_MSG_BUTTON_DOWN", Units = "N/A", Description = "Triggers the pressing of the message button." }; } }
        private SimConnectEvent GpsMsgButtonUp { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsMsgButtonUp, Name = "GPS_MSG_BUTTON_UP", Units = "N/A", Description = "Triggers the release of the message button." }; } }
        private SimConnectEvent GpsNearestButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsNearestButton, Name = "GPS_NEAREST_BUTTON", Units = "N/A", Description = "Selects Nearest Airport Page." }; } }
        private SimConnectEvent GpsObs { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObs, Name = "GPS_OBS", Units = "N/A", Description = "Toggle GPS OBS mode active status on/off." }; } }
        private SimConnectEvent GpsObsButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObsButton, Name = "GPS_OBS_BUTTON", Units = "N/A", Description = "Toggles automatic sequencing of waypoints." }; } }
        private SimConnectEvent GpsObsDec { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObsDec, Name = "GPS_OBS_DEC", Units = "N/A", Description = "Decreases GPS OBS value by 1 degree (if the value goes below 1 it will wrap to 360)." }; } }
        private SimConnectEvent GpsObsInc { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObsInc, Name = "GPS_OBS_INC", Units = "N/A", Description = "Increases GPS OBS value by 1 degree (if the value goes above 360 it will wrap to 1)." }; } }
        private SimConnectEvent GpsObsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObsOff, Name = "GPS_OBS_OFF", Units = "N/A", Description = "Turn the GPS OBS mode to be inactive." }; } }
        private SimConnectEvent GpsObsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObsOn, Name = "GPS_OBS_ON", Units = "N/A", Description = "Turn on the GPS OBS mode to be active" }; } }
        private SimConnectEvent GpsObsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsObsSet, Name = "GPS_OBS_SET", Units = "[0]: Value in degrees", Description = "Set the GPS OBS value to a new value, in degrees." }; } }
        private SimConnectEvent GpsPageKnobDec { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsPageKnobDec, Name = "GPS_PAGE_KNOB_DEC", Units = "[0]: Value in degrees", Description = "Decrements through pages" }; } }
        private SimConnectEvent GpsPageKnobInc { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsPageKnobInc, Name = "GPS_PAGE_KNOB_INC", Units = "[0]: Value in degrees", Description = "Increments through pages" }; } }
        private SimConnectEvent GpsPowerButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsPowerButton, Name = "GPS_POWER_BUTTON", Units = "[0]: Value in degrees", Description = "Toggles power button" }; } }
        private SimConnectEvent GpsProcedureButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsProcedureButton, Name = "GPS_PROCEDURE_BUTTON", Units = "[0]: Value in degrees", Description = "Displays the approach procedure page." }; } }
        private SimConnectEvent GpsSetupButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsSetupButton, Name = "GPS_SETUP_BUTTON", Units = "[0]: Value in degrees", Description = "Toggles power button" }; } }
        private SimConnectEvent GpsTerrainButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsTerrainButton, Name = "GPS_TERRAIN_BUTTON", Units = "[0]: Value in degrees", Description = "Displays terrain information on default display" }; } }
        private SimConnectEvent GpsVnavButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsVnavButton, Name = "GPS_VNAV_BUTTON", Units = "[0]: Value in degrees", Description = "Displays terrain information on default display" }; } }
        private SimConnectEvent GpsZoominButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsZoominButton, Name = "GPS_ZOOMIN_BUTTON", Units = "[0]: Value in degrees", Description = "Zooms in default display" }; } }
        private SimConnectEvent GpsZoomoutButton { get { return new SimConnectEvent() { Id = SimConnectEventId.GpsZoomoutButton, Name = "GPS_ZOOMOUT_BUTTON", Units = "[0]: Value in degrees", Description = "Zooms out default display" }; } }
        private SimConnectEvent GpwsSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.GpwsSwitchToggle, Name = "GPWS_SWITCH_TOGGLE", Units = "N/A", Description = "Turn the ground proximity warning system (GPWS) on or off." }; } }
        private SimConnectEvent GunsightSel { get { return new SimConnectEvent() { Id = SimConnectEventId.GunsightSel, Name = "GUNSIGHT_SEL", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent GunsightToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.GunsightToggle, Name = "GUNSIGHT_TOGGLE", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent GyroDriftDec { get { return new SimConnectEvent() { Id = SimConnectEventId.GyroDriftDec, Name = "GYRO_DRIFT_DEC", Units = "N/A", Description = "Decrements heading indicator." }; } }
        private SimConnectEvent GyroDriftInc { get { return new SimConnectEvent() { Id = SimConnectEventId.GyroDriftInc, Name = "GYRO_DRIFT_INC", Units = "N/A", Description = "Increments heading indicator." }; } }
        private SimConnectEvent GyroDriftSet { get { return new SimConnectEvent() { Id = SimConnectEventId.GyroDriftSet, Name = "GYRO_DRIFT_SET", Units = "[0]: Drift angle (degrees)", Description = "Sets heading indicator drift angle (degrees)." }; } }
        private SimConnectEvent GyroDriftSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.GyroDriftSetEx1, Name = "GYRO_DRIFT_SET_EX1", Units = "[0]: Drift angle (degrees)", Description = "Sets heading indicator drift angle (degrees)." }; } }
        private SimConnectEvent HeadingBugDec { get { return new SimConnectEvent() { Id = SimConnectEventId.HeadingBugDec, Name = "HEADING_BUG_DEC", Units = "N/A", Description = "Decrements heading hold reference bug" }; } }
        private SimConnectEvent HeadingBugInc { get { return new SimConnectEvent() { Id = SimConnectEventId.HeadingBugInc, Name = "HEADING_BUG_INC", Units = "N/A", Description = "Increments heading hold reference bug" }; } }
        private SimConnectEvent HeadingBugSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.HeadingBugSelect, Name = "HEADING_BUG_SELECT", Units = "[0]: heading bug index", Description = "Selects the heading bug for use with +/-" }; } }
        private SimConnectEvent HeadingBugSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HeadingBugSet, Name = "HEADING_BUG_SET", Units = "[0]: Value in degrees\r\n[1]: Index", Description = "Set the heading hold reference bug in degrees. The event takes integer values only, from 0º to 360º." }; } }
        private SimConnectEvent HeadingGyroSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HeadingGyroSet, Name = "HEADING_GYRO_SET", Units = "[0]: Drift angle (degrees)", Description = "Sets heading indicator to 0 drift error." }; } }
        private SimConnectEvent HeadingSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HeadingSlotIndexSet, Name = "HEADING_SLOT_INDEX_SET", Units = "N/A", Description = "Set autopilot heading slot index." }; } }
        private SimConnectEvent HeliBeepDecrease { get { return new SimConnectEvent() { Id = SimConnectEventId.HeliBeepDecrease, Name = "HELI_BEEP_DECREASE", Units = "[0]: value", Description = "Sets rotor brake switch on. Deprecated in favour of ROTOR_BRAKE_ON." }; } }
        private SimConnectEvent HeliBeepIncrease { get { return new SimConnectEvent() { Id = SimConnectEventId.HeliBeepIncrease, Name = "HELI_BEEP_INCREASE", Units = "[0]: value", Description = "Sets rotor brake switch on. Deprecated in favour of ROTOR_BRAKE_ON." }; } }
        private SimConnectEvent HelicopterEngine1BeepTrimDecrease { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1BeepTrimDecrease, Name = "HELICOPTER_ENGINE_1_BEEP_TRIM_DECREASE", Units = "[0]: value", Description = "For a helicopter, decrease the engine 1 trim RPM by the given value amount." }; } }
        private SimConnectEvent HelicopterEngine1BeepTrimIncrease { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1BeepTrimIncrease, Name = "HELICOPTER_ENGINE_1_BEEP_TRIM_INCREASE", Units = "[0]: value", Description = "For a helicopter, increase the engine 1 trim RPM by the given value amount." }; } }
        private SimConnectEvent HelicopterEngine1BeepTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1BeepTrimSet, Name = "HELICOPTER_ENGINE_1_BEEP_TRIM_SET", Units = "[0]: value", Description = "For a helicopter, set the engine 1 trim RPM to the given value." }; } }
        private SimConnectEvent HelicopterEngine1GovernorSwitchOff { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1GovernorSwitchOff, Name = "HELICOPTER_ENGINE_1_GOVERNOR_SWITCH_OFF", Units = "N/A", Description = "For a helicopter, toggle the engine 1 governor switch between ON (1) and OFF (0)." }; } }
        private SimConnectEvent HelicopterEngine1GovernorSwitchOn { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1GovernorSwitchOn, Name = "HELICOPTER_ENGINE_1_GOVERNOR_SWITCH_ON", Units = "N/A", Description = "For a helicopter, set the engine 1 governor switch ON." }; } }
        private SimConnectEvent HelicopterEngine1GovernorSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1GovernorSwitchSet, Name = "HELICOPTER_ENGINE_1_GOVERNOR_SWITCH_SET", Units = "[0]: 0 or 1", Description = "For a helicopter, set the engine 1 governor switch to either ON (1) or OFF (0)." }; } }
        private SimConnectEvent HelicopterEngine1GovernorSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine1GovernorSwitchToggle, Name = "HELICOPTER_ENGINE_1_GOVERNOR_SWITCH_TOGGLE", Units = "N/A", Description = "For a helicopter, set the engine 1 governor switch OFF." }; } }
        private SimConnectEvent HelicopterEngine2BeepTrimDecrease { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2BeepTrimDecrease, Name = "HELICOPTER_ENGINE_2_BEEP_TRIM_DECREASE", Units = "[0]: value", Description = "For a helicopter, decrease the engine 2 trim RPM by the given value amount." }; } }
        private SimConnectEvent HelicopterEngine2BeepTrimIncrease { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2BeepTrimIncrease, Name = "HELICOPTER_ENGINE_2_BEEP_TRIM_INCREASE", Units = "[0]: value", Description = "For a helicopter, increase the engine 2 trim RPM by the given value amount." }; } }
        private SimConnectEvent HelicopterEngine2BeepTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2BeepTrimSet, Name = "HELICOPTER_ENGINE_2_BEEP_TRIM_SET", Units = "[0]: value", Description = "For a helicopter, set the engine 2 trim RPM to the given value." }; } }
        private SimConnectEvent HelicopterEngine2GovernorSwitchOff { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2GovernorSwitchOff, Name = "HELICOPTER_ENGINE_2_GOVERNOR_SWITCH_OFF", Units = "N/A", Description = "For a helicopter, toggle the engine 2 governor switch between ON (1) and OFF (0)." }; } }
        private SimConnectEvent HelicopterEngine2GovernorSwitchOn { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2GovernorSwitchOn, Name = "HELICOPTER_ENGINE_2_GOVERNOR_SWITCH_ON", Units = "N/A", Description = "For a helicopter, set the engine 2 governor switch ON." }; } }
        private SimConnectEvent HelicopterEngine2GovernorSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2GovernorSwitchSet, Name = "HELICOPTER_ENGINE_2_GOVERNOR_SWITCH_SET", Units = "[0]: 0 or 1", Description = "For a helicopter, set the engine 2 governor switch to either ON (1) or OFF (0)." }; } }
        private SimConnectEvent HelicopterEngine2GovernorSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterEngine2GovernorSwitchToggle, Name = "HELICOPTER_ENGINE_2_GOVERNOR_SWITCH_TOGGLE", Units = "N/A", Description = "For a helicopter, set the engine 2 governor switch OFF." }; } }
        private SimConnectEvent HelicopterThrottleCut { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottleCut, Name = "HELICOPTER_THROTTLE_CUT", Units = "N/A", Description = "Cut all throttles." }; } }
        private SimConnectEvent HelicopterThrottleDec { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottleDec, Name = "HELICOPTER_THROTTLE_DEC", Units = "[0]: Decrement value (0 to 16384)", Description = "By default this will decrement all throttles by 1/128, to a minimum of 0. If you provide an input parameter then this will be internally normalised to a value between 0 and 1 and used to decrement instead." }; } }
        private SimConnectEvent HelicopterThrottleFull { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottleFull, Name = "HELICOPTER_THROTTLE_FULL", Units = "N/A", Description = "Set all throttles to full." }; } }
        private SimConnectEvent HelicopterThrottleInc { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottleInc, Name = "HELICOPTER_THROTTLE_INC", Units = "[0]: Increment value (0 to 16384)", Description = "By default this will increment all throttles by 1/128, to a maximum of 1. If you provide an input parameter then this will be internally normalised to a value between 0 and 1 and used to increment instead." }; } }
        private SimConnectEvent HelicopterThrottleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottleSet, Name = "HELICOPTER_THROTTLE_SET", Units = "[0]: Throttle value (0 to 16384)", Description = "Set all throttles based on the input value. The input is between 0 and 16384, which will be normalised to a value between 0 and 1." }; } }
        private SimConnectEvent HelicopterThrottle1Cut { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle1Cut, Name = "HELICOPTER_THROTTLE1_CUT", Units = "N/A", Description = "Cut throttle 1/2." }; } }
        private SimConnectEvent HelicopterThrottle1Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle1Dec, Name = "HELICOPTER_THROTTLE1_DEC", Units = "[0]: Decrement value (0 to 16384) ", Description = "By default this will decrement throttle 1 or 2 by 1/128, to a minimum of 0. If you provide an input parameter then this will be internally normalised to a value between 0 and 1 and used to decrement instead." }; } }
        private SimConnectEvent HelicopterThrottle1Full { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle1Full, Name = "HELICOPTER_THROTTLE1_FULL", Units = "N/A", Description = "Set throttle 1 or 2 to full." }; } }
        private SimConnectEvent HelicopterThrottle1Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle1Inc, Name = "HELICOPTER_THROTTLE1_INC", Units = "[0]: Increment value (0 to 16384)", Description = "By default this will increment throttle 1 or 2 by 1/128, to a maximum of 1. If you provide an input parameter then this will be internally normalised to a value between 0 and 1 and used to increment instead." }; } }
        private SimConnectEvent HelicopterThrottle1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle1Set, Name = "HELICOPTER_THROTTLE1_SET", Units = "[0]: Throttle value (0 to 16384)", Description = "Set throttle 1 or 2 based on the input value. The input is between 0 and 16384, which will be normalised to a value between 0 and 1." }; } }
        private SimConnectEvent HelicopterThrottle2Cut { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle2Cut, Name = "HELICOPTER_THROTTLE2_CUT", Units = "N/A", Description = "Cut throttle 1/2." }; } }
        private SimConnectEvent HelicopterThrottle2Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle2Dec, Name = "HELICOPTER_THROTTLE2_DEC", Units = "[0]: Decrement value (0 to 16384) ", Description = "By default this will decrement throttle 1 or 2 by 1/128, to a minimum of 0. If you provide an input parameter then this will be internally normalised to a value between 0 and 1 and used to decrement instead." }; } }
        private SimConnectEvent HelicopterThrottle2Full { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle2Full, Name = "HELICOPTER_THROTTLE2_FULL", Units = "N/A", Description = "Set throttle 1 or 2 to full." }; } }
        private SimConnectEvent HelicopterThrottle2Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle2Inc, Name = "HELICOPTER_THROTTLE2_INC", Units = "[0]: Increment value (0 to 16384)", Description = "By default this will increment throttle 1 or 2 by 1/128, to a maximum of 1. If you provide an input parameter then this will be internally normalised to a value between 0 and 1 and used to increment instead." }; } }
        private SimConnectEvent HelicopterThrottle2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.HelicopterThrottle2Set, Name = "HELICOPTER_THROTTLE2_SET", Units = "[0]: Throttle value (0 to 16384)", Description = "Set throttle 1 or 2 based on the input value. The input is between 0 and 16384, which will be normalised to a value between 0 and 1." }; } }
        private SimConnectEvent HoistDeploySet { get { return new SimConnectEvent() { Id = SimConnectEventId.HoistDeploySet, Name = "HOIST_DEPLOY_SET", Units = "", Description = "The hoist deployment setting. The value should be set to one of the following:\r\n0 - set hoist switch to retract the arm\r\n1 - set hoist switch to extend the arm" }; } }
        private SimConnectEvent HoistDeployToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.HoistDeployToggle, Name = "HOIST_DEPLOY_TOGGLE", Units = "", Description = "Toggles the hoist arm switch, extend or retract." }; } }
        private SimConnectEvent HoistSwitchExtend { get { return new SimConnectEvent() { Id = SimConnectEventId.HoistSwitchExtend, Name = "HOIST_SWITCH_EXTEND", Units = "[0]: Bool", Description = "The rate at which a hoist cable extends (set in the Aircraft Configuration File)" }; } }
        private SimConnectEvent HoistSwitchRetract { get { return new SimConnectEvent() { Id = SimConnectEventId.HoistSwitchRetract, Name = "HOIST_SWITCH_RETRACT", Units = "[0]: Bool", Description = "The rate at which a hoist cable retracts (set in the Aircraft Configuration File)." }; } }
        private SimConnectEvent HoistSwitchSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.HoistSwitchSelect, Name = "HOIST_SWITCH_SELECT", Units = "[0]: Bool", Description = "The rate at which a hoist cable retracts (set in the Aircraft Configuration File)." }; } }
        private SimConnectEvent HoistSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.HoistSwitchSet, Name = "HOIST_SWITCH_SET", Units = "[0]: Bool", Description = "The hoist control switch setting. Should be set to one of the following values:\r\n<0 up\r\n0 off\r\n>0 down" }; } }
        private SimConnectEvent HornTrigger { get { return new SimConnectEvent() { Id = SimConnectEventId.HornTrigger, Name = "HORN_TRIGGER", Units = "N/A", Description = "Trigger the aircraft horn." }; } }
        private SimConnectEvent HudColor { get { return new SimConnectEvent() { Id = SimConnectEventId.HudColor, Name = "HUD_COLOR", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent HudUnits { get { return new SimConnectEvent() { Id = SimConnectEventId.HudUnits, Name = "HUD_UNITS", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent HydraulicSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.HydraulicSwitchToggle, Name = "HYDRAULIC_SWITCH_TOGGLE", Units = "[0]: TRUE/FALSE to set or the hydraulic switch on/off", Description = "Turn the hydraulic switch on or off." }; } }
        private SimConnectEvent IncConcordeNoseVisor { get { return new SimConnectEvent() { Id = SimConnectEventId.IncConcordeNoseVisor, Name = "INC_CONCORDE_NOSE_VISOR", Units = "N/A", Description = "Depricated" }; } }
        private SimConnectEvent IncCowlFlaps { get { return new SimConnectEvent() { Id = SimConnectEventId.IncCowlFlaps, Name = "INC_COWL_FLAPS", Units = "N/A", Description = "Increment cowl flap levers" }; } }
        private SimConnectEvent IncCowlFlaps1 { get { return new SimConnectEvent() { Id = SimConnectEventId.IncCowlFlaps1, Name = "INC_COWL_FLAPS1", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent IncCowlFlaps2 { get { return new SimConnectEvent() { Id = SimConnectEventId.IncCowlFlaps2, Name = "INC_COWL_FLAPS2", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent IncCowlFlaps3 { get { return new SimConnectEvent() { Id = SimConnectEventId.IncCowlFlaps3, Name = "INC_COWL_FLAPS3", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent IncCowlFlaps4 { get { return new SimConnectEvent() { Id = SimConnectEventId.IncCowlFlaps4, Name = "INC_COWL_FLAPS4", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent IncreaseAutobrakeControl { get { return new SimConnectEvent() { Id = SimConnectEventId.IncreaseAutobrakeControl, Name = "INCREASE_AUTOBRAKE_CONTROL", Units = "N/A", Description = "Increases the autobrake level by 1. When the level reaches the auto_brakes value, the event will no longer increment further." }; } }
        private SimConnectEvent IncreaseDecisionAltitudeMsl { get { return new SimConnectEvent() { Id = SimConnectEventId.IncreaseDecisionAltitudeMsl, Name = "INCREASE_DECISION_ALTITUDE_MSL", Units = "[0]: amount", Description = "Increments the MSL decision height reference by the amount given, or by 10m if no amount is given." }; } }
        private SimConnectEvent IncreaseDecisionHeight { get { return new SimConnectEvent() { Id = SimConnectEventId.IncreaseDecisionHeight, Name = "INCREASE_DECISION_HEIGHT", Units = "N/A", Description = "Increments the AGL decision height reference by 1m." }; } }
        private SimConnectEvent IncreaseHeloGovBeep { get { return new SimConnectEvent() { Id = SimConnectEventId.IncreaseHeloGovBeep, Name = "INCREASE_HELO_GOV_BEEP ", Units = "[0]: value\r\n[1]: engine", Description = "If the helicopter has an engine trimmer, this event can be used to increase the nominal engine/rotor RPM that the governor is trying to maintain for the indexed engine.\r\nThe amount that the trim will be adjusted by is set using the engine_trim_rate CFG parameter, and the min and max achievable values are set using engine_trim_min and engine_trim_max. Alternatively, you may supply a value that will override that set in the" }; } }
        private SimConnectEvent IncreaseThrottle { get { return new SimConnectEvent() { Id = SimConnectEventId.IncreaseThrottle, Name = "INCREASE_THROTTLE", Units = "[0]: the value between 0 - 16384", Description = "Increment throttles" }; } }
        private SimConnectEvent InductorCompassRefDec { get { return new SimConnectEvent() { Id = SimConnectEventId.InductorCompassRefDec, Name = "INDUCTOR_COMPASS_REF_DEC", Units = "N/A", Description = "Sets heading indicator to 0 drift error." }; } }
        private SimConnectEvent InductorCompassRefInc { get { return new SimConnectEvent() { Id = SimConnectEventId.InductorCompassRefInc, Name = "INDUCTOR_COMPASS_REF_INC", Units = "N/A", Description = "Sets heading indicator to 0 drift error." }; } }
        private SimConnectEvent IntercomModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.IntercomModeSet, Name = "INTERCOM_MODE_SET", Units = "N/A", Description = "Swaps frequency with standby on whichever NAV or COM radio is selected." }; } }
        private SimConnectEvent InvokeHelp { get { return new SimConnectEvent() { Id = SimConnectEventId.InvokeHelp, Name = "INVOKE_HELP", Units = "N/A", Description = "Brings up Help system" }; } }
        private SimConnectEvent IsolateTurbineOff { get { return new SimConnectEvent() { Id = SimConnectEventId.IsolateTurbineOff, Name = "ISOLATE_TURBINE_OFF", Units = "[0]: Engine index (1 to 4)", Description = "Using this key will end the \"isolation\" for the engine, effectively enabling the engine drag and thrust again.\r\nThis key takes an engine number as a parameter (from 1 to 4 to flag a specific engine, or 0 to affect all engines).\r\nIMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent IsolateTurbineOn { get { return new SimConnectEvent() { Id = SimConnectEventId.IsolateTurbineOn, Name = "ISOLATE_TURBINE_ON", Units = "[0]: Engine index (1 to 4)", Description = "Using this key will \"isolate\" the given engine, effectively nullyfing the engine drag and thrust.\r\nThis key takes an engine number as a parameter (from 1 to 4 to flag a specific engine, or 0 to affect all engines).\r\nIMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent IsolateTurbineSet { get { return new SimConnectEvent() { Id = SimConnectEventId.IsolateTurbineSet, Name = "ISOLATE_TURBINE_SET", Units = "[0]: Engine index (1 to 4)\r\n[1]: State (TRUE / FALSE)", Description = "Setting this to TRUE will \"isolate\" the engine, effectively nullyfing the engine drag and thrust.\r\nThis key takes two parameters: an engine number (from 1 to 4 to flag a specific engine, or 0 to affect all engines), and a TRUE / FALSE second parameter to set the engine isolation.\r\nIMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent IsolateTurbineToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.IsolateTurbineToggle, Name = "ISOLATE_TURBINE_TOGGLE", Units = "[0]: Engine index (1 to 4)", Description = "This key can be used to toggle an engines \"isolated\" state, where an isolated engine has its drag and thrust effectively nullified.\r\nThis key takes an engine number as a parameter (from 1 to 4 to flag a specific engine, or 0 to affect all engines).\r\nIMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent JetStarter { get { return new SimConnectEvent() { Id = SimConnectEventId.JetStarter, Name = "JET_STARTER", Units = "[0]: Index", Description = "Selects jet engine starter (for +/- sequence)" }; } }
        private SimConnectEvent JoystickCalibrate { get { return new SimConnectEvent() { Id = SimConnectEventId.JoystickCalibrate, Name = "JOYSTICK_CALIBRATE", Units = "N/A", Description = "Toggles joystick on/off" }; } }
        private SimConnectEvent KeyboardOverlay { get { return new SimConnectEvent() { Id = SimConnectEventId.KeyboardOverlay, Name = "KEYBOARD_OVERLAY", Units = "N/A", Description = "Toggles joystick on/off" }; } }
        private SimConnectEvent KneeboardView { get { return new SimConnectEvent() { Id = SimConnectEventId.KneeboardView, Name = "KNEEBOARD_VIEW", Units = "N/A", Description = "Toggles kneeboard" }; } }
        private SimConnectEvent KohlsmanDec { get { return new SimConnectEvent() { Id = SimConnectEventId.KohlsmanDec, Name = "KOHLSMAN_DEC", Units = "N/A", Description = "Decrements altimeter setting." }; } }
        private SimConnectEvent KohlsmanInc { get { return new SimConnectEvent() { Id = SimConnectEventId.KohlsmanInc, Name = "KOHLSMAN_INC", Units = "N/A", Description = "Increments altimeter setting." }; } }
        private SimConnectEvent KohlsmanSet { get { return new SimConnectEvent() { Id = SimConnectEventId.KohlsmanSet, Name = "KOHLSMAN_SET", Units = "[0]: Value to set\r\n[1]: Altimeter index", Description = "Sets altimeter setting (Millibars * 16)." }; } }
        private SimConnectEvent LabelColorCycle { get { return new SimConnectEvent() { Id = SimConnectEventId.LabelColorCycle, Name = "LABEL_COLOR_CYCLE", Units = "N/A", Description = "Toggles kneeboard" }; } }
        private SimConnectEvent LandingLightDown { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightDown, Name = "LANDING_LIGHT_DOWN", Units = "[0]: light circuit index", Description = "Rotate landing light down" }; } }
        private SimConnectEvent LandingLightHome { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightHome, Name = "LANDING_LIGHT_HOME", Units = "[0]: light circuit index", Description = "Return landing light to default position" }; } }
        private SimConnectEvent LandingLightLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightLeft, Name = "LANDING_LIGHT_LEFT", Units = "[0]: light circuit index", Description = "Rotate landing light left" }; } }
        private SimConnectEvent LandingLightRight { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightRight, Name = "LANDING_LIGHT_RIGHT", Units = "[0]: light circuit index", Description = "Rotate landing light right" }; } }
        private SimConnectEvent LandingLightUp { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightUp, Name = "LANDING_LIGHT_UP", Units = "[0]: light circuit index", Description = "Rotate landing light up" }; } }
        private SimConnectEvent LandingLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightsOff, Name = "LANDING_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn landing lights off" }; } }
        private SimConnectEvent LandingLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightsOn, Name = "LANDING_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn landing lights on" }; } }
        private SimConnectEvent LandingLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightsSet, Name = "LANDING_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set landing lights on/off (1,0)" }; } }
        private SimConnectEvent LandingLightsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.LandingLightsToggle, Name = "LANDING_LIGHTS_TOGGLE", Units = "[0]: light circuit index", Description = "Toggle landing lights" }; } }
        private SimConnectEvent Letterbox { get { return new SimConnectEvent() { Id = SimConnectEventId.Letterbox, Name = "LETTERBOX", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent LightPotentiometer1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer1Set, Name = "LIGHT_POTENTIOMETER_1_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer10Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer10Set, Name = "LIGHT_POTENTIOMETER_10_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer11Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer11Set, Name = "LIGHT_POTENTIOMETER_11_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer12Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer12Set, Name = "LIGHT_POTENTIOMETER_12_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer13Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer13Set, Name = "LIGHT_POTENTIOMETER_13_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer14Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer14Set, Name = "LIGHT_POTENTIOMETER_14_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer15Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer15Set, Name = "LIGHT_POTENTIOMETER_15_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer16Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer16Set, Name = "LIGHT_POTENTIOMETER_16_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer17Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer17Set, Name = "LIGHT_POTENTIOMETER_17_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer18Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer18Set, Name = "LIGHT_POTENTIOMETER_18_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer19Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer19Set, Name = "LIGHT_POTENTIOMETER_19_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer2Set, Name = "LIGHT_POTENTIOMETER_2_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer20Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer20Set, Name = "LIGHT_POTENTIOMETER_20_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer21Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer21Set, Name = "LIGHT_POTENTIOMETER_21_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer22Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer22Set, Name = "LIGHT_POTENTIOMETER_22_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer23Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer23Set, Name = "LIGHT_POTENTIOMETER_23_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer24Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer24Set, Name = "LIGHT_POTENTIOMETER_24_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer25Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer25Set, Name = "LIGHT_POTENTIOMETER_25_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer26Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer26Set, Name = "LIGHT_POTENTIOMETER_26_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer27Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer27Set, Name = "LIGHT_POTENTIOMETER_27_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer28Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer28Set, Name = "LIGHT_POTENTIOMETER_28_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer29Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer29Set, Name = "LIGHT_POTENTIOMETER_29_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer3Set, Name = "LIGHT_POTENTIOMETER_3_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer30Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer30Set, Name = "LIGHT_POTENTIOMETER_30_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer4Set, Name = "LIGHT_POTENTIOMETER_4_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer5Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer5Set, Name = "LIGHT_POTENTIOMETER_5_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer6Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer6Set, Name = "LIGHT_POTENTIOMETER_6_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer7Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer7Set, Name = "LIGHT_POTENTIOMETER_7_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer8Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer8Set, Name = "LIGHT_POTENTIOMETER_8_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometer9Set { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometer9Set, Name = "LIGHT_POTENTIOMETER_9_SET", Units = "[0]: Potentiometer value", Description = "" }; } }
        private SimConnectEvent LightPotentiometerDec { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometerDec, Name = "LIGHT_POTENTIOMETER_DEC", Units = "N/A", Description = "Toggle landing lights" }; } }
        private SimConnectEvent LightPotentiometerInc { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometerInc, Name = "LIGHT_POTENTIOMETER_INC", Units = "N/A", Description = "Toggle landing lights" }; } }
        private SimConnectEvent LightPotentiometerSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LightPotentiometerSet, Name = "LIGHT_POTENTIOMETER_SET", Units = "[0]: Index\r\n[1]: Potentiometer value", Description = "Toggle landing lights" }; } }
        private SimConnectEvent LodZoomIn { get { return new SimConnectEvent() { Id = SimConnectEventId.LodZoomIn, Name = "LOD_ZOOM_IN", Units = "N/A", Description = "Toggles kneeboard" }; } }
        private SimConnectEvent LodZoomOut { get { return new SimConnectEvent() { Id = SimConnectEventId.LodZoomOut, Name = "LOD_ZOOM_OUT", Units = "N/A", Description = "Toggles kneeboard" }; } }
        private SimConnectEvent LogoLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LogoLightsSet, Name = "LOGO_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set logo lights on/off (1,0)" }; } }
        private SimConnectEvent LowHeightWarningGaugeWillSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LowHeightWarningGaugeWillSet, Name = "LOW_HEIGHT_WARNING_GAUGE_WILL_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent LowHeightWarningSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LowHeightWarningSet, Name = "LOW_HEIGHT_WARNING_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent LowHightWarningGaugeWillSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LowHightWarningGaugeWillSet, Name = "LOW_HIGHT_WARNING_GAUGE_WILL_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent LowHightWarningSet { get { return new SimConnectEvent() { Id = SimConnectEventId.LowHightWarningSet, Name = "LOW_HIGHT_WARNING_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MacCreadySettingDec { get { return new SimConnectEvent() { Id = SimConnectEventId.MacCreadySettingDec, Name = "MAC_CREADY_SETTING_DEC", Units = "N/A", Description = "Decrements the MacCready setting. Default decrement value is 0.1m/s, however holding down the key for more than 1 second will increase the amount to 0.5m/s, and holding it down for more than 2 seconds will further increase this to 1m/s. Note that the resu" }; } }
        private SimConnectEvent MacCreadySettingInc { get { return new SimConnectEvent() { Id = SimConnectEventId.MacCreadySettingInc, Name = "MAC_CREADY_SETTING_INC", Units = "N/A", Description = "Increments the MacCready setting. Default increment value is 0.1m/s, however holding down the key for more than 1 second will increase the amount to 0.5m/s, and holding it down for more than 2 seconds will further increase this to 1m/s. Note that the resu" }; } }
        private SimConnectEvent MacCreadySettingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MacCreadySettingSet, Name = "MAC_CREADY_SETTING_SET", Units = "[0]: MacCready value in m/s", Description = "Set the MacCready setting to a value between 0 and 5 m/s." }; } }
        private SimConnectEvent MacroBegin { get { return new SimConnectEvent() { Id = SimConnectEventId.MacroBegin, Name = "MACRO_BEGIN", Units = "N/A", Description = "Toggles kneeboard" }; } }
        private SimConnectEvent MacroEnd { get { return new SimConnectEvent() { Id = SimConnectEventId.MacroEnd, Name = "MACRO_END", Units = "N/A", Description = "Toggles kneeboard" }; } }
        private SimConnectEvent Magneto { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto, Name = "MAGNETO", Units = "[0]: Magneto index", Description = "Selects magnetos (for +/- sequence)" }; } }
        private SimConnectEvent MagnetoBoth { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoBoth, Name = "MAGNETO_BOTH", Units = "[0]: Magneto index", Description = "Set indexed engine magnetos on" }; } }
        private SimConnectEvent MagnetoDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoDecr, Name = "MAGNETO_DECR", Units = "N/A", Description = "Decrease all magneto switches positions" }; } }
        private SimConnectEvent MagnetoIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoIncr, Name = "MAGNETO_INCR", Units = "N/A", Description = "Increase all magneto switches positions" }; } }
        private SimConnectEvent MagnetoLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoLeft, Name = "MAGNETO_LEFT", Units = "N/A", Description = "Toggle all engine left magnetos" }; } }
        private SimConnectEvent MagnetoOff { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoOff, Name = "MAGNETO_OFF", Units = "N/A", Description = "Set all engine magnetos off" }; } }
        private SimConnectEvent MagnetoRight { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoRight, Name = "MAGNETO_RIGHT", Units = "N/A", Description = "Toggle all engine right magnetos" }; } }
        private SimConnectEvent MagnetoSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoSet, Name = "MAGNETO_SET", Units = "[0]: True/False (1, 0)", Description = "Sets all engine magnetos (0,1)\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent MagnetoStart { get { return new SimConnectEvent() { Id = SimConnectEventId.MagnetoStart, Name = "MAGNETO_START", Units = "N/A", Description = "Set all engine magnetos on and toggle starters" }; } }
        private SimConnectEvent Magneto1Both { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Both, Name = "MAGNETO1_BOTH", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on" }; } }
        private SimConnectEvent Magneto1Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Decr, Name = "MAGNETO1_DECR", Units = "N/A", Description = "Decrease engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto1Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Incr, Name = "MAGNETO1_INCR", Units = "N/A", Description = "Increase engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto1Left { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Left, Name = "MAGNETO1_LEFT", Units = "N/A", Description = "Toggle engine 1/2/3/4 left magneto" }; } }
        private SimConnectEvent Magneto1Off { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Off, Name = "MAGNETO1_OFF", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos off" }; } }
        private SimConnectEvent Magneto1Right { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Right, Name = "MAGNETO1_RIGHT", Units = "N/A", Description = "Toggle engine 1/2/3/4 right magneto" }; } }
        private SimConnectEvent Magneto1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Set, Name = "MAGNETO1_SET", Units = "N/A", Description = "Set engine 1/2/3/4 magneto switch" }; } }
        private SimConnectEvent Magneto1Start { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto1Start, Name = "MAGNETO1_START", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on and toggle starter" }; } }
        private SimConnectEvent Magneto2Both { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Both, Name = "MAGNETO2_BOTH", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on" }; } }
        private SimConnectEvent Magneto2Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Decr, Name = "MAGNETO2_DECR", Units = "N/A", Description = "Decrease engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto2Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Incr, Name = "MAGNETO2_INCR", Units = "N/A", Description = "Increase engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto2Left { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Left, Name = "MAGNETO2_LEFT", Units = "N/A", Description = "Toggle engine 1/2/3/4 left magneto" }; } }
        private SimConnectEvent Magneto2Off { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Off, Name = "MAGNETO2_OFF", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos off" }; } }
        private SimConnectEvent Magneto2Right { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Right, Name = "MAGNETO2_RIGHT", Units = "N/A", Description = "Toggle engine 1/2/3/4 right magneto" }; } }
        private SimConnectEvent Magneto2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Set, Name = "MAGNETO2_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent Magneto2Start { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto2Start, Name = "MAGNETO2_START", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on and toggle starter" }; } }
        private SimConnectEvent Magneto3Both { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Both, Name = "MAGNETO3_BOTH", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on" }; } }
        private SimConnectEvent Magneto3Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Decr, Name = "MAGNETO3_DECR", Units = "N/A", Description = "Decrease engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto3Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Incr, Name = "MAGNETO3_INCR", Units = "N/A", Description = "Increase engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto3Left { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Left, Name = "MAGNETO3_LEFT", Units = "N/A", Description = "Toggle engine 1/2/3/4 left magneto" }; } }
        private SimConnectEvent Magneto3Off { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Off, Name = "MAGNETO3_OFF", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos off" }; } }
        private SimConnectEvent Magneto3Right { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Right, Name = "MAGNETO3_RIGHT", Units = "N/A", Description = "Toggle engine 1/2/3/4 right magneto" }; } }
        private SimConnectEvent Magneto3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Set, Name = "MAGNETO3_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent Magneto3Start { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto3Start, Name = "MAGNETO3_START", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on and toggle starter" }; } }
        private SimConnectEvent Magneto4Both { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Both, Name = "MAGNETO4_BOTH", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on" }; } }
        private SimConnectEvent Magneto4Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Decr, Name = "MAGNETO4_DECR", Units = "N/A", Description = "Decrease engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto4Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Incr, Name = "MAGNETO4_INCR", Units = "N/A", Description = "Increase engine 1/2/3/4 magneto switch position" }; } }
        private SimConnectEvent Magneto4Left { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Left, Name = "MAGNETO4_LEFT", Units = "N/A", Description = "Toggle engine 1/2/3/4 left magneto" }; } }
        private SimConnectEvent Magneto4Off { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Off, Name = "MAGNETO4_OFF", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos off" }; } }
        private SimConnectEvent Magneto4Right { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Right, Name = "MAGNETO4_RIGHT", Units = "N/A", Description = "Toggle engine 1/2/3/4 right magneto" }; } }
        private SimConnectEvent Magneto4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Set, Name = "MAGNETO4_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent Magneto4Start { get { return new SimConnectEvent() { Id = SimConnectEventId.Magneto4Start, Name = "MAGNETO4_START", Units = "N/A", Description = "Set engine 1/2/3/4 magnetos on and toggle starter" }; } }
        private SimConnectEvent ManualFuelPressurePump { get { return new SimConnectEvent() { Id = SimConnectEventId.ManualFuelPressurePump, Name = "MANUAL_FUEL_PRESSURE_PUMP", Units = "", Description = "Activate the manual fuel pressure pump. Used for both modern [FUEL_SYSTEM] and legacy [FUEL] systems." }; } }
        private SimConnectEvent ManualFuelPressurePumpSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ManualFuelPressurePumpSet, Name = "MANUAL_FUEL_PRESSURE_PUMP_SET", Units = "[0]: The pump index\r\n[1]: A value between 0 and 16384", Description = "Set the position of the fuel manual pump handle, as a percentage. This key is only useful when using the modern [FUEL_SYSTEM]." }; } }
        private SimConnectEvent ManualFuelTransfer { get { return new SimConnectEvent() { Id = SimConnectEventId.ManualFuelTransfer, Name = "MANUAL_FUEL_TRANSFER", Units = "[1]: A value between 0 and 16384", Description = "When set to 1 (TRUE) it sets the fuel transfer mode to manual." }; } }
        private SimConnectEvent MapOrientationCycle { get { return new SimConnectEvent() { Id = SimConnectEventId.MapOrientationCycle, Name = "MAP_ORIENTATION_CYCLE", Units = "N/A", Description = "Step through the map orientations." }; } }
        private SimConnectEvent MapOrientationSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MapOrientationSet, Name = "MAP_ORIENTATION_SET", Units = "N/A", Description = "Step through the map orientations." }; } }
        private SimConnectEvent MapZoomFineIn { get { return new SimConnectEvent() { Id = SimConnectEventId.MapZoomFineIn, Name = "MAP_ZOOM_FINE_IN", Units = "N/A", Description = "Fine zoom in map view" }; } }
        private SimConnectEvent MapZoomFineOut { get { return new SimConnectEvent() { Id = SimConnectEventId.MapZoomFineOut, Name = "MAP_ZOOM_FINE_OUT", Units = "N/A", Description = "Fine zoom out in map view" }; } }
        private SimConnectEvent MapZoomSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MapZoomSet, Name = "MAP_ZOOM_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MarkerBeaconSensitivityHigh { get { return new SimConnectEvent() { Id = SimConnectEventId.MarkerBeaconSensitivityHigh, Name = "MARKER_BEACON_SENSITIVITY_HIGH", Units = "N/A", Description = "Swaps frequency with standby on whichever NAV or COM radio is selected." }; } }
        private SimConnectEvent MarkerBeaconTestMute { get { return new SimConnectEvent() { Id = SimConnectEventId.MarkerBeaconTestMute, Name = "MARKER_BEACON_TEST_MUTE", Units = "N/A", Description = "Swaps frequency with standby on whichever NAV or COM radio is selected." }; } }
        private SimConnectEvent MarkerSoundSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MarkerSoundSet, Name = "MARKER_SOUND_SET", Units = "[0]: Bool", Description = "Sets marker beacon sound (1, 0).\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent MarkerSoundToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.MarkerSoundToggle, Name = "MARKER_SOUND_TOGGLE", Units = "N/A", Description = "Toggles marker beacon sound on/off" }; } }
        private SimConnectEvent MasterBatteryOff { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterBatteryOff, Name = "MASTER_BATTERY_OFF", Units = "[0]: battery index", Description = "Turns the indexed battery switch off. The battery index is the N index of the battery.N definition." }; } }
        private SimConnectEvent MasterBatteryOn { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterBatteryOn, Name = "MASTER_BATTERY_ON", Units = "[0]: battery index", Description = "Turns the indexed battery switch on. The battery index is the N index of the battery.N definition." }; } }
        private SimConnectEvent MasterBatterySet { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterBatterySet, Name = "MASTER_BATTERY_SET", Units = "[0]: battery index\r\n[1]: bool", Description = "Set the battery switch state. The battery index is the N index of the battery.N definition." }; } }
        private SimConnectEvent MasterCautionAcknowledge { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterCautionAcknowledge, Name = "MASTER_CAUTION_ACKNOWLEDGE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterCautionOff { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterCautionOff, Name = "MASTER_CAUTION_OFF", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterCautionOn { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterCautionOn, Name = "MASTER_CAUTION_ON", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterCautionSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterCautionSet, Name = "MASTER_CAUTION_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterCautionToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterCautionToggle, Name = "MASTER_CAUTION_TOGGLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterWarningAcknowledge { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterWarningAcknowledge, Name = "MASTER_WARNING_ACKNOWLEDGE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterWarningOff { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterWarningOff, Name = "MASTER_WARNING_OFF", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterWarningOn { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterWarningOn, Name = "MASTER_WARNING_ON", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterWarningSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterWarningSet, Name = "MASTER_WARNING_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MasterWarningToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.MasterWarningToggle, Name = "MASTER_WARNING_TOGGLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent Minus { get { return new SimConnectEvent() { Id = SimConnectEventId.Minus, Name = "MINUS", Units = "N/A", Description = "Used in conjunction with \"selected\" parameters to decrease their value (e.g.,radio frequency)" }; } }
        private SimConnectEvent MinusShift { get { return new SimConnectEvent() { Id = SimConnectEventId.MinusShift, Name = "MINUS_SHIFT", Units = "N/A", Description = "Used with other events" }; } }
        private SimConnectEvent MixtureDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureDecr, Name = "MIXTURE_DECR", Units = "N/A", Description = "Decrement mixture levers" }; } }
        private SimConnectEvent MixtureDecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureDecrSmall, Name = "MIXTURE_DECR_SMALL", Units = "N/A", Description = "Decrement mixture levers small" }; } }
        private SimConnectEvent MixtureIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureIncr, Name = "MIXTURE_INCR", Units = "N/A", Description = "Increment mixture levers" }; } }
        private SimConnectEvent MixtureIncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureIncrSmall, Name = "MIXTURE_INCR_SMALL", Units = "N/A", Description = "Increment mixture levers small" }; } }
        private SimConnectEvent MixtureLean { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureLean, Name = "MIXTURE_LEAN", Units = "N/A", Description = "Set mixture levers to max lean" }; } }
        private SimConnectEvent MixtureRich { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureRich, Name = "MIXTURE_RICH", Units = "N/A", Description = "Set mixture levers to max rich" }; } }
        private SimConnectEvent MixtureSet { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureSet, Name = "MIXTURE_SET", Units = "N/A", Description = "Engine mixture set." }; } }
        private SimConnectEvent MixtureSetBest { get { return new SimConnectEvent() { Id = SimConnectEventId.MixtureSetBest, Name = "MIXTURE_SET_BEST", Units = "N/A", Description = "Engine mixture set." }; } }
        private SimConnectEvent Mixture1Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1Decr, Name = "MIXTURE1_DECR", Units = "N/A", Description = "Decrement mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture1DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1DecrSmall, Name = "MIXTURE1_DECR_SMALL", Units = "N/A", Description = "Decrement mixture 1/2/3/4 lever small" }; } }
        private SimConnectEvent Mixture1Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1Incr, Name = "MIXTURE1_INCR", Units = "N/A", Description = "Increment mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture1IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1IncrSmall, Name = "MIXTURE1_INCR_SMALL", Units = "N/A", Description = "Increment mixture lever 1/2/3/4 small" }; } }
        private SimConnectEvent Mixture1Lean { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1Lean, Name = "MIXTURE1_LEAN", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max lean" }; } }
        private SimConnectEvent Mixture1Rich { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1Rich, Name = "MIXTURE1_RICH", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max rich" }; } }
        private SimConnectEvent Mixture1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture1Set, Name = "MIXTURE1_SET", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent Mixture2Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2Decr, Name = "MIXTURE2_DECR", Units = "N/A", Description = "Decrement mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture2DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2DecrSmall, Name = "MIXTURE2_DECR_SMALL", Units = "N/A", Description = "Decrement mixture 1/2/3/4 lever small" }; } }
        private SimConnectEvent Mixture2Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2Incr, Name = "MIXTURE2_INCR", Units = "N/A", Description = "Increment mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture2IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2IncrSmall, Name = "MIXTURE2_INCR_SMALL", Units = "N/A", Description = "Increment mixture lever 1/2/3/4 small" }; } }
        private SimConnectEvent Mixture2Lean { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2Lean, Name = "MIXTURE2_LEAN", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max lean" }; } }
        private SimConnectEvent Mixture2Rich { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2Rich, Name = "MIXTURE2_RICH", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max rich" }; } }
        private SimConnectEvent Mixture2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture2Set, Name = "MIXTURE2_SET", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent Mixture3Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3Decr, Name = "MIXTURE3_DECR", Units = "N/A", Description = "Decrement mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture3DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3DecrSmall, Name = "MIXTURE3_DECR_SMALL", Units = "N/A", Description = "Decrement mixture 1/2/3/4 lever small" }; } }
        private SimConnectEvent Mixture3Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3Incr, Name = "MIXTURE3_INCR", Units = "N/A", Description = "Increment mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture3IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3IncrSmall, Name = "MIXTURE3_INCR_SMALL", Units = "N/A", Description = "Increment mixture lever 1/2/3/4 small" }; } }
        private SimConnectEvent Mixture3Lean { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3Lean, Name = "MIXTURE3_LEAN", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max lean" }; } }
        private SimConnectEvent Mixture3Rich { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3Rich, Name = "MIXTURE3_RICH", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max rich" }; } }
        private SimConnectEvent Mixture3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture3Set, Name = "MIXTURE3_SET", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent Mixture4Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4Decr, Name = "MIXTURE4_DECR", Units = "N/A", Description = "Decrement mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture4DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4DecrSmall, Name = "MIXTURE4_DECR_SMALL", Units = "N/A", Description = "Decrement mixture 1/2/3/4 lever small" }; } }
        private SimConnectEvent Mixture4Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4Incr, Name = "MIXTURE4_INCR", Units = "N/A", Description = "Increment mixture lever 1/2/3/4" }; } }
        private SimConnectEvent Mixture4IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4IncrSmall, Name = "MIXTURE4_INCR_SMALL", Units = "N/A", Description = "Increment mixture lever 1/2/3/4 small" }; } }
        private SimConnectEvent Mixture4Lean { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4Lean, Name = "MIXTURE4_LEAN", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max lean" }; } }
        private SimConnectEvent Mixture4Rich { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4Rich, Name = "MIXTURE4_RICH", Units = "N/A", Description = "Set mixture lever 1/2/3/4 to max rich" }; } }
        private SimConnectEvent Mixture4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Mixture4Set, Name = "MIXTURE4_SET", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent MouseAsYokeResume { get { return new SimConnectEvent() { Id = SimConnectEventId.MouseAsYokeResume, Name = "MOUSE_AS_YOKE_RESUME", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MouseAsYokeSuspend { get { return new SimConnectEvent() { Id = SimConnectEventId.MouseAsYokeSuspend, Name = "MOUSE_AS_YOKE_SUSPEND", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MouseAsYokeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.MouseAsYokeToggle, Name = "MOUSE_AS_YOKE_TOGGLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent MouseLookToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.MouseLookToggle, Name = "MOUSE_LOOK_TOGGLE", Units = "N/A", Description = "Switch Mouse Look mode on or off. Mouse Look mode enables a user to control their view using the mouse, and holding down the space bar." }; } }
        private SimConnectEvent MpActivateChat { get { return new SimConnectEvent() { Id = SimConnectEventId.MpActivateChat, Name = "MP_ACTIVATE_CHAT", Units = "N/A", Description = "Activates chat window" }; } }
        private SimConnectEvent MpBroadcastVoiceCaptureStart { get { return new SimConnectEvent() { Id = SimConnectEventId.MpBroadcastVoiceCaptureStart, Name = "MP_BROADCAST_VOICE_CAPTURE_START", Units = "N/A", Description = "Start capturing audio from the users computer and transmitting it to all other players in the multiplayer session." }; } }
        private SimConnectEvent MpBroadcastVoiceCaptureStop { get { return new SimConnectEvent() { Id = SimConnectEventId.MpBroadcastVoiceCaptureStop, Name = "MP_BROADCAST_VOICE_CAPTURE_STOP", Units = "N/A", Description = "Stop capturing broadcast audio." }; } }
        private SimConnectEvent MpChat { get { return new SimConnectEvent() { Id = SimConnectEventId.MpChat, Name = "MP_CHAT", Units = "N/A", Description = "Toggles chat window visible/invisible" }; } }
        private SimConnectEvent MpPauseSession { get { return new SimConnectEvent() { Id = SimConnectEventId.MpPauseSession, Name = "MP_PAUSE_SESSION", Units = "N/A", Description = "Pause the multiplayer session." }; } }
        private SimConnectEvent MpPlayerCycle { get { return new SimConnectEvent() { Id = SimConnectEventId.MpPlayerCycle, Name = "MP_PLAYER_CYCLE", Units = "N/A", Description = "Cycle through the current user aircraft." }; } }
        private SimConnectEvent MpPlayerFollow { get { return new SimConnectEvent() { Id = SimConnectEventId.MpPlayerFollow, Name = "MP_PLAYER_FOLLOW", Units = "N/A", Description = "Set the view to follow the selected user aircraft." }; } }
        private SimConnectEvent MpTransferControl { get { return new SimConnectEvent() { Id = SimConnectEventId.MpTransferControl, Name = "MP_TRANSFER_CONTROL", Units = "N/A", Description = "Toggle to the next player to track" }; } }
        private SimConnectEvent MpVoiceCaptureStart { get { return new SimConnectEvent() { Id = SimConnectEventId.MpVoiceCaptureStart, Name = "MP_VOICE_CAPTURE_START", Units = "N/A", Description = "Start capturing audio from the users computer and transmitting it to all other players in the multiplayer session who are turned to the same radio frequency." }; } }
        private SimConnectEvent MpVoiceCaptureStop { get { return new SimConnectEvent() { Id = SimConnectEventId.MpVoiceCaptureStop, Name = "MP_VOICE_CAPTURE_STOP", Units = "N/A", Description = "Stop capturing radio audio." }; } }
        private SimConnectEvent NavLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.NavLightsOff, Name = "NAV_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn navigation lights off" }; } }
        private SimConnectEvent NavLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.NavLightsOn, Name = "NAV_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn navigation lights on" }; } }
        private SimConnectEvent NavLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.NavLightsSet, Name = "NAV_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set navigation lights on/off (1,0)" }; } }
        private SimConnectEvent NavRadio { get { return new SimConnectEvent() { Id = SimConnectEventId.NavRadio, Name = "NAV_RADIO", Units = "N/A", Description = "Sequentially selects the NAV tuner digits for use with +/-. Follow by SELECT_1, SELECT_2, SELECT_3, or SELECT_4 for NAV 1, 2, 3 or 4." }; } }
        private SimConnectEvent Nav1CloseFreqSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1CloseFreqSet, Name = "NAV1_CLOSE_FREQ_SET", Units = "[0]: Bool", Description = "This event is used to enable (set to 1, TRUE) or disable (set to 0, FALSE) the following SimVars:\nNAV_CLOSE_DME\nNAV_CLOSE_FREQUENCY\nNAV_CLOSE_IDENT\nNAV_CLOSE_LOCALIZER\nNAV_CLOSE_NAME\nAlso note that all the NAV key events are simply aliases for each other," }; } }
        private SimConnectEvent Nav1RadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioFractDec, Name = "NAV1_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav1RadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioFractDecCarry, Name = "NAV1_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav1RadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioFractInc, Name = "NAV1_RADIO_FRACT_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav1RadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioFractIncCarry, Name = "NAV1_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav1RadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioSet, Name = "NAV1_RADIO_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav1RadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioSetHz, Name = "NAV1_RADIO_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (Hz)." }; } }
        private SimConnectEvent Nav1RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioSwap, Name = "NAV1_RADIO_SWAP", Units = "N/A", Description = "Swap between the chosen NAV frequency and the corresponding standby frequency." }; } }
        private SimConnectEvent Nav1RadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioWholeDec, Name = "NAV1_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav1RadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1RadioWholeInc, Name = "NAV1_RADIO_WHOLE_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav1StbySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1StbySet, Name = "NAV1_STBY_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav1StbySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1StbySetHz, Name = "NAV1_STBY_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (Hz)." }; } }
        private SimConnectEvent Nav1VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1VolumeDec, Name = "NAV1_VOLUME_DEC", Units = "N/A", Description = "Decrement the volume by 0.02, down to a minimum of 0." }; } }
        private SimConnectEvent Nav1VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1VolumeInc, Name = "NAV1_VOLUME_INC", Units = "N/A", Description = "Increment the volume by 0.02, up to a maximum of 1." }; } }
        private SimConnectEvent Nav1VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1VolumeSet, Name = "NAV1_VOLUME_SET", Units = "[0] Volume value (0 -1)", Description = "Sets the volume for the chosen NAV." }; } }
        private SimConnectEvent Nav1VolumeSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav1VolumeSetEx1, Name = "NAV1_VOLUME_SET_EX1", Units = "[0] Volume value (0 - 100)", Description = "Sets the volume for the chosen NAV, from 0 to 100 (interpolated in the simulation to a value from 0 to 1)." }; } }
        private SimConnectEvent Nav2CloseFreqSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2CloseFreqSet, Name = "NAV2_CLOSE_FREQ_SET", Units = "[0]: Bool", Description = "This event is used to enable (set to 1, TRUE) or disable (set to 0, FALSE) the following SimVars:\nNAV_CLOSE_DME\nNAV_CLOSE_FREQUENCY\nNAV_CLOSE_IDENT\nNAV_CLOSE_LOCALIZER\nNAV_CLOSE_NAME\nAlso note that all the NAV key events are simply aliases for each other," }; } }
        private SimConnectEvent Nav2RadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioFractDec, Name = "NAV2_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav2RadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioFractDecCarry, Name = "NAV2_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav2RadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioFractInc, Name = "NAV2_RADIO_FRACT_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav2RadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioFractIncCarry, Name = "NAV2_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav2RadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioSet, Name = "NAV2_RADIO_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav2RadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioSetHz, Name = "NAV2_RADIO_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (Hz)." }; } }
        private SimConnectEvent Nav2RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioSwap, Name = "NAV2_RADIO_SWAP", Units = "N/A", Description = "Swap between the chosen NAV frequency and the corresponding standby frequency." }; } }
        private SimConnectEvent Nav2RadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioWholeDec, Name = "NAV2_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav2RadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2RadioWholeInc, Name = "NAV2_RADIO_WHOLE_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav2StbySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2StbySet, Name = "NAV2_STBY_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav2StbySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2StbySetHz, Name = "NAV2_STBY_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (Hz)." }; } }
        private SimConnectEvent Nav2VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2VolumeDec, Name = "NAV2_VOLUME_DEC", Units = "N/A", Description = "Decrement the volume by 0.02, down to a minimum of 0." }; } }
        private SimConnectEvent Nav2VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2VolumeInc, Name = "NAV2_VOLUME_INC", Units = "N/A", Description = "Increment the volume by 0.02, up to a maximum of 1." }; } }
        private SimConnectEvent Nav2VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2VolumeSet, Name = "NAV2_VOLUME_SET", Units = "[0] Volume value (0 -1)", Description = "NOTE: These events are deprecated as they no longer work correctly. Instead use the _EX1 versions, listed below." }; } }
        private SimConnectEvent Nav2VolumeSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav2VolumeSetEx1, Name = "NAV2_VOLUME_SET_EX1", Units = "[0] Volume value (0 - 100)", Description = "Sets the volume for the chosen NAV, from 0 to 100 (interpolated in the simulation to a value from 0 to 1)." }; } }
        private SimConnectEvent Nav3CloseFreqSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3CloseFreqSet, Name = "NAV3_CLOSE_FREQ_SET", Units = "[0]: Bool", Description = "This event is used to enable (set to 1, TRUE) or disable (set to 0, FALSE) the following SimVars:\nNAV_CLOSE_DME\nNAV_CLOSE_FREQUENCY\nNAV_CLOSE_IDENT\nNAV_CLOSE_LOCALIZER\nNAV_CLOSE_NAME\nAlso note that all the NAV key events are simply aliases for each other," }; } }
        private SimConnectEvent Nav3RadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioFractDec, Name = "NAV3_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav3RadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioFractDecCarry, Name = "NAV3_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav3RadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioFractInc, Name = "NAV3_RADIO_FRACT_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav3RadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioFractIncCarry, Name = "NAV3_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav3RadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioSet, Name = "NAV3_RADIO_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav3RadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioSetHz, Name = "NAV3_RADIO_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (Hz)." }; } }
        private SimConnectEvent Nav3RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioSwap, Name = "NAV3_RADIO_SWAP", Units = "N/A", Description = "Swap between the chosen NAV frequency and the corresponding standby frequency." }; } }
        private SimConnectEvent Nav3RadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioWholeDec, Name = "NAV3_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav3RadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3RadioWholeInc, Name = "NAV3_RADIO_WHOLE_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav3StbySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3StbySet, Name = "NAV3_STBY_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav3StbySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3StbySetHz, Name = "NAV3_STBY_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (Hz)." }; } }
        private SimConnectEvent Nav3VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3VolumeDec, Name = "NAV3_VOLUME_DEC", Units = "N/A", Description = "Decrement the volume by 0.02, down to a minimum of 0." }; } }
        private SimConnectEvent Nav3VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3VolumeInc, Name = "NAV3_VOLUME_INC", Units = "N/A", Description = "Increment the volume by 0.02, up to a maximum of 1." }; } }
        private SimConnectEvent Nav3VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3VolumeSet, Name = "NAV3_VOLUME_SET", Units = "[0] Volume value (0 -1)", Description = "NOTE: These events are deprecated as they no longer work correctly. Instead use the _EX1 versions, listed below." }; } }
        private SimConnectEvent Nav3VolumeSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav3VolumeSetEx1, Name = "NAV3_VOLUME_SET_EX1", Units = "[0] Volume value (0 - 100)", Description = "Sets the volume for the chosen NAV, from 0 to 100 (interpolated in the simulation to a value from 0 to 1)." }; } }
        private SimConnectEvent Nav4CloseFreqSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4CloseFreqSet, Name = "NAV4_CLOSE_FREQ_SET", Units = "[0]: Bool", Description = "This event is used to enable (set to 1, TRUE) or disable (set to 0, FALSE) the following SimVars:\nNAV_CLOSE_DME\nNAV_CLOSE_FREQUENCY\nNAV_CLOSE_IDENT\nNAV_CLOSE_LOCALIZER\nNAV_CLOSE_NAME\nAlso note that all the NAV key events are simply aliases for each other," }; } }
        private SimConnectEvent Nav4RadioFractDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioFractDec, Name = "NAV4_RADIO_FRACT_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav4RadioFractDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioFractDecCarry, Name = "NAV4_RADIO_FRACT_DEC_CARRY", Units = "N/A", Description = "Decrement the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav4RadioFractInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioFractInc, Name = "NAV4_RADIO_FRACT_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by 25 KHz." }; } }
        private SimConnectEvent Nav4RadioFractIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioFractIncCarry, Name = "NAV4_RADIO_FRACT_INC_CARRY", Units = "N/A", Description = "Increment the chosen NAV frequency by 50 KHz, and will carry when the value wraps." }; } }
        private SimConnectEvent Nav4RadioSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioSet, Name = "NAV4_RADIO_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav4RadioSetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioSetHz, Name = "NAV4_RADIO_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV frequency (Hz)." }; } }
        private SimConnectEvent Nav4RadioSwap { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioSwap, Name = "NAV4_RADIO_SWAP", Units = "N/A", Description = "Swap between the chosen NAV frequency and the corresponding standby frequency." }; } }
        private SimConnectEvent Nav4RadioWholeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioWholeDec, Name = "NAV4_RADIO_WHOLE_DEC", Units = "N/A", Description = "Decrements the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav4RadioWholeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4RadioWholeInc, Name = "NAV4_RADIO_WHOLE_INC", Units = "N/A", Description = "Increments the chosen NAV frequency by one MHz." }; } }
        private SimConnectEvent Nav4StbySet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4StbySet, Name = "NAV4_STBY_SET", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (BCD16 encoded Hz)." }; } }
        private SimConnectEvent Nav4StbySetHz { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4StbySetHz, Name = "NAV4_STBY_SET_HZ", Units = "[0] Frequency value", Description = "Sets the chosen NAV standby frequency (Hz)." }; } }
        private SimConnectEvent Nav4VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4VolumeDec, Name = "NAV4_VOLUME_DEC", Units = "N/A", Description = "Decrement the volume by 0.02, down to a minimum of 0." }; } }
        private SimConnectEvent Nav4VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4VolumeInc, Name = "NAV4_VOLUME_INC", Units = "N/A", Description = "Increment the volume by 0.02, up to a maximum of 1." }; } }
        private SimConnectEvent Nav4VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4VolumeSet, Name = "NAV4_VOLUME_SET", Units = "[0] Volume value (0 -1)", Description = "NOTE: These events are deprecated as they no longer work correctly. Instead use the _EX1 versions, listed below." }; } }
        private SimConnectEvent Nav4VolumeSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Nav4VolumeSetEx1, Name = "NAV4_VOLUME_SET_EX1", Units = "[0] Volume value (0 - 100)", Description = "Sets the volume for the chosen NAV, from 0 to 100 (interpolated in the simulation to a value from 0 to 1)." }; } }
        private SimConnectEvent NewMap { get { return new SimConnectEvent() { Id = SimConnectEventId.NewMap, Name = "NEW_MAP", Units = "N/A", Description = "Opens new map view" }; } }
        private SimConnectEvent NewView { get { return new SimConnectEvent() { Id = SimConnectEventId.NewView, Name = "NEW_VIEW", Units = "N/A", Description = "Open new view" }; } }
        private SimConnectEvent NextSubView { get { return new SimConnectEvent() { Id = SimConnectEventId.NextSubView, Name = "NEXT_SUB_VIEW", Units = "N/A", Description = "Close current view" }; } }
        private SimConnectEvent NextView { get { return new SimConnectEvent() { Id = SimConnectEventId.NextView, Name = "NEXT_VIEW", Units = "N/A", Description = "Select next view" }; } }
        private SimConnectEvent NitrousTankValveToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.NitrousTankValveToggle, Name = "NITROUS_TANK_VALVE_TOGGLE", Units = "[0]: Tank index (optional)", Description = "Toggle the nitrous valve. Pass a value to determine which tank to use if there are multiple tanks. See the Fuel Selector Codes list for the correct tank code to use. Note that this key requires the [NITROUS_SYSTEM.N] system to have been set up in the engi" }; } }
        private SimConnectEvent NoseWheelSteeringLimitSet { get { return new SimConnectEvent() { Id = SimConnectEventId.NoseWheelSteeringLimitSet, Name = "NOSE_WHEEL_STEERING_LIMIT_SET", Units = "[0]: Steering position (+/-16383)", Description = "Set the steering angle limit for the nose wheel. -180° maps to -16383 and 180° maps to 16383." }; } }
        private SimConnectEvent OilCoolingFlapsDown { get { return new SimConnectEvent() { Id = SimConnectEventId.OilCoolingFlapsDown, Name = "OIL_COOLING_FLAPS_DOWN", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent OilCoolingFlapsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.OilCoolingFlapsSet, Name = "OIL_COOLING_FLAPS_SET", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent OilCoolingFlapsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.OilCoolingFlapsToggle, Name = "OIL_COOLING_FLAPS_TOGGLE", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent OilCoolingFlapsUp { get { return new SimConnectEvent() { Id = SimConnectEventId.OilCoolingFlapsUp, Name = "OIL_COOLING_FLAPS_UP", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent OtherAircraftView { get { return new SimConnectEvent() { Id = SimConnectEventId.OtherAircraftView, Name = "OTHER_AIRCRAFT_VIEW", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent Overlaymenu { get { return new SimConnectEvent() { Id = SimConnectEventId.Overlaymenu, Name = "OVERLAYMENU", Units = "N/A", Description = "Switch Mouse Look mode on or off. Mouse Look mode enables a user to control their view using the mouse, and holding down the space bar." }; } }
        private SimConnectEvent PanDown { get { return new SimConnectEvent() { Id = SimConnectEventId.PanDown, Name = "PAN_DOWN", Units = "N/A", Description = "Pan view down" }; } }
        private SimConnectEvent PanLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.PanLeft, Name = "PAN_LEFT", Units = "N/A", Description = "Pans view left" }; } }
        private SimConnectEvent PanLeftDown { get { return new SimConnectEvent() { Id = SimConnectEventId.PanLeftDown, Name = "PAN_LEFT_DOWN", Units = "N/A", Description = "Pan view left and down" }; } }
        private SimConnectEvent PanLeftUp { get { return new SimConnectEvent() { Id = SimConnectEventId.PanLeftUp, Name = "PAN_LEFT_UP", Units = "N/A", Description = "Pan view left" }; } }
        private SimConnectEvent PanReset { get { return new SimConnectEvent() { Id = SimConnectEventId.PanReset, Name = "PAN_RESET", Units = "N/A", Description = "Reset view to forward" }; } }
        private SimConnectEvent PanResetCockpit { get { return new SimConnectEvent() { Id = SimConnectEventId.PanResetCockpit, Name = "PAN_RESET_COCKPIT", Units = "N/A", Description = "Reset panning to forward, if in cockpit view" }; } }
        private SimConnectEvent PanRight { get { return new SimConnectEvent() { Id = SimConnectEventId.PanRight, Name = "PAN_RIGHT", Units = "N/A", Description = "Pans view right" }; } }
        private SimConnectEvent PanRightDown { get { return new SimConnectEvent() { Id = SimConnectEventId.PanRightDown, Name = "PAN_RIGHT_DOWN", Units = "N/A", Description = "Pan view right and down" }; } }
        private SimConnectEvent PanRightUp { get { return new SimConnectEvent() { Id = SimConnectEventId.PanRightUp, Name = "PAN_RIGHT_UP", Units = "N/A", Description = "Pan view right and up" }; } }
        private SimConnectEvent PanTiltLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.PanTiltLeft, Name = "PAN_TILT_LEFT", Units = "N/A", Description = "Tilt view left" }; } }
        private SimConnectEvent PanTiltRight { get { return new SimConnectEvent() { Id = SimConnectEventId.PanTiltRight, Name = "PAN_TILT_RIGHT", Units = "N/A", Description = "Tilt view right" }; } }
        private SimConnectEvent PanUp { get { return new SimConnectEvent() { Id = SimConnectEventId.PanUp, Name = "PAN_UP", Units = "N/A", Description = "Pan view up" }; } }
        private SimConnectEvent PanView { get { return new SimConnectEvent() { Id = SimConnectEventId.PanView, Name = "PAN_VIEW", Units = "N/A", Description = "Pan view up" }; } }
        private SimConnectEvent Panel1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel1, Name = "PANEL_1", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel2 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel2, Name = "PANEL_2", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel3 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel3, Name = "PANEL_3", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel4 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel4, Name = "PANEL_4", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel5 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel5, Name = "PANEL_5", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel6 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel6, Name = "PANEL_6", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel7 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel7, Name = "PANEL_7", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel8 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel8, Name = "PANEL_8", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent Panel9 { get { return new SimConnectEvent() { Id = SimConnectEventId.Panel9, Name = "PANEL_9", Units = "N/A", Description = "Toggles panels 1 - 9." }; } }
        private SimConnectEvent PanelHudNext { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelHudNext, Name = "PANEL_HUD_NEXT", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent PanelHudPrevious { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelHudPrevious, Name = "PANEL_HUD_PREVIOUS", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent PanelIdClose { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelIdClose, Name = "PANEL_ID_CLOSE", Units = "[0]: panel index", Description = "Closes indexed panel (1 to 9)" }; } }
        private SimConnectEvent PanelIdOpen { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelIdOpen, Name = "PANEL_ID_OPEN", Units = "[0]: panel index", Description = "Opens indexed panel (1 to 9)" }; } }
        private SimConnectEvent PanelIdToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelIdToggle, Name = "PANEL_ID_TOGGLE", Units = "[0]: panel index", Description = "Toggles indexed panel (1 to 9)" }; } }
        private SimConnectEvent PanelLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelLightsOff, Name = "PANEL_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn panel lights off" }; } }
        private SimConnectEvent PanelLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelLightsOn, Name = "PANEL_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn panel lights on" }; } }
        private SimConnectEvent PanelLightsPowerSettingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelLightsPowerSettingSet, Name = "PANEL_LIGHTS_POWER_SETTING_SET", Units = "[0]: panel light circuit index\r\n[1]: power setting (%)", Description = "Set panel light circuit power setting. Takes two indices, the circuit and the power setting (see SimVars And Keys for more information). The index is the value assigned to the circuit Type when the circuit.N was defined." }; } }
        private SimConnectEvent PanelLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelLightsSet, Name = "PANEL_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set panel lights on/off (1,0)" }; } }
        private SimConnectEvent PanelLightsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelLightsToggle, Name = "PANEL_LIGHTS_TOGGLE", Units = "[0]: light circuit index", Description = "Toggle panel lights" }; } }
        private SimConnectEvent PanelSelect1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelSelect1, Name = "PANEL_SELECT_1", Units = "[0]: panel index", Description = "Closes indexed panel (1 to 9)" }; } }
        private SimConnectEvent PanelSelect2 { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelSelect2, Name = "PANEL_SELECT_2", Units = "[0]: panel index", Description = "Closes indexed panel (1 to 9)" }; } }
        private SimConnectEvent PanelToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PanelToggle, Name = "PANEL_TOGGLE", Units = "[0]: panel index", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ParkingBrakeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ParkingBrakeSet, Name = "PARKING_BRAKE_SET", Units = "[0]: Bool", Description = "Set the parking brake on/off" }; } }
        private SimConnectEvent ParkingBrakes { get { return new SimConnectEvent() { Id = SimConnectEventId.ParkingBrakes, Name = "PARKING_BRAKES", Units = "N/A", Description = "Toggles the parking brake on/off" }; } }
        private SimConnectEvent PauseOff { get { return new SimConnectEvent() { Id = SimConnectEventId.PauseOff, Name = "PAUSE_OFF", Units = "[0]: panel index", Description = "Turns pause off" }; } }
        private SimConnectEvent PauseOn { get { return new SimConnectEvent() { Id = SimConnectEventId.PauseOn, Name = "PAUSE_ON", Units = "[0]: panel index", Description = "Turns pause on" }; } }
        private SimConnectEvent PauseSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PauseSet, Name = "PAUSE_SET", Units = "[0]: Bool", Description = "Sets pause on/off (1,0)" }; } }
        private SimConnectEvent PauseToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PauseToggle, Name = "PAUSE_TOGGLE", Units = "[0]: panel index", Description = "Toggles pause on/off" }; } }
        private SimConnectEvent PedestralLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.PedestralLightsOff, Name = "PEDESTRAL_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn pedestral lights off" }; } }
        private SimConnectEvent PedestralLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.PedestralLightsOn, Name = "PEDESTRAL_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn pedestral lights on" }; } }
        private SimConnectEvent PedestralLightsPowerSettingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PedestralLightsPowerSettingSet, Name = "PEDESTRAL_LIGHTS_POWER_SETTING_SET", Units = "[0]: pedestal light circuit index\r\n[1]: power setting (%)", Description = "Set pedestal light circuit power setting. Takes two indices, the circuit and the power setting (see SimVars And Keys for more information). The index is the value assigned to the circuit Type when the circuit.N was defined." }; } }
        private SimConnectEvent PedestralLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PedestralLightsSet, Name = "PEDESTRAL_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set pedestral lights on/off (1,0)" }; } }
        private SimConnectEvent PedestralLightsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PedestralLightsToggle, Name = "PEDESTRAL_LIGHTS_TOGGLE", Units = "[0]: light circuit index", Description = "Toggle pedestral lights" }; } }
        private SimConnectEvent PilotTransmitterSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PilotTransmitterSet, Name = "PILOT_TRANSMITTER_SET", Units = "[0]: The Com channel to select.", Description = "This event can be used to select the COM channel to use. The input is one of the following values:\r\n0: Com1\r\n1: Com2\r\n2: Com3\r\n4: None" }; } }
        private SimConnectEvent PitotHeatOff { get { return new SimConnectEvent() { Id = SimConnectEventId.PitotHeatOff, Name = "PITOT_HEAT_OFF", Units = "N/A", Description = "Turns the pitot heat switch off." }; } }
        private SimConnectEvent PitotHeatOn { get { return new SimConnectEvent() { Id = SimConnectEventId.PitotHeatOn, Name = "PITOT_HEAT_ON", Units = "N/A", Description = "Turns the pitot heat switch on." }; } }
        private SimConnectEvent PitotHeatSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PitotHeatSet, Name = "PITOT_HEAT_SET", Units = "[0]: TRUE/FALSE to set or the pitot heat switch on/off\r\n[1]: Pitot index", Description = "Sets the pitot heat switch on/off." }; } }
        private SimConnectEvent PitotHeatToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PitotHeatToggle, Name = "PITOT_HEAT_TOGGLE", Units = "N/A", Description = "Toggles the pitot heat switch." }; } }
        private SimConnectEvent Plus { get { return new SimConnectEvent() { Id = SimConnectEventId.Plus, Name = "PLUS", Units = "[0]: Bool", Description = "Used in conjunction with \"selected\" parameters to increase their value (e.g.,radio frequency)" }; } }
        private SimConnectEvent PlusShift { get { return new SimConnectEvent() { Id = SimConnectEventId.PlusShift, Name = "PLUS_SHIFT", Units = "[0]: Bool", Description = "Used with other events" }; } }
        private SimConnectEvent PointOfInterestCycleNext { get { return new SimConnectEvent() { Id = SimConnectEventId.PointOfInterestCycleNext, Name = "POINT_OF_INTEREST_CYCLE_NEXT", Units = "N/A", Description = "Change the current point-of-interest to the next point-of-interest." }; } }
        private SimConnectEvent PointOfInterestCyclePrevious { get { return new SimConnectEvent() { Id = SimConnectEventId.PointOfInterestCyclePrevious, Name = "POINT_OF_INTEREST_CYCLE_PREVIOUS", Units = "N/A", Description = "Change the current point-of-interest to the previous point-of-interest." }; } }
        private SimConnectEvent PointOfInterestTogglePointer { get { return new SimConnectEvent() { Id = SimConnectEventId.PointOfInterestTogglePointer, Name = "POINT_OF_INTEREST_TOGGLE_POINTER", Units = "N/A", Description = "Turn the point-of-interest indicator (often a light beam) on or off. Refer to the Missions system documentation." }; } }
        private SimConnectEvent PressurizationClimbRateDec { get { return new SimConnectEvent() { Id = SimConnectEventId.PressurizationClimbRateDec, Name = "PRESSURIZATION_CLIMB_RATE_DEC", Units = "N/A", Description = "Decrement the cabin pressurization by approximately 50ft/min steps based on the initialisation value of 500ft/min." }; } }
        private SimConnectEvent PressurizationClimbRateInc { get { return new SimConnectEvent() { Id = SimConnectEventId.PressurizationClimbRateInc, Name = "PRESSURIZATION_CLIMB_RATE_INC", Units = "N/A", Description = "Increment the cabin pressurization by approximately 50ft/min step, based on the initialisation value of 500ft/min." }; } }
        private SimConnectEvent PressurizationClimbRateSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PressurizationClimbRateSet, Name = "PRESSURIZATION_CLIMB_RATE_SET", Units = "[0]: Value", Description = "Sets the cabin pressurization." }; } }
        private SimConnectEvent PressurizationPressureAltDec { get { return new SimConnectEvent() { Id = SimConnectEventId.PressurizationPressureAltDec, Name = "PRESSURIZATION_PRESSURE_ALT_DEC", Units = "N/A", Description = "Decreases the altitude that the cabin is pressurized to by approximately 50ft." }; } }
        private SimConnectEvent PressurizationPressureAltInc { get { return new SimConnectEvent() { Id = SimConnectEventId.PressurizationPressureAltInc, Name = "PRESSURIZATION_PRESSURE_ALT_INC", Units = "N/A", Description = "Increases the altitude that the cabin is pressurized to by approximately 50ft." }; } }
        private SimConnectEvent PressurizationPressureDumpSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.PressurizationPressureDumpSwitch, Name = "PRESSURIZATION_PRESSURE_DUMP_SWITCH", Units = "N/A", Description = "Toggles the pressure dump switch between on (sets the cabin pressure to the outside air pressure) and off." }; } }
        private SimConnectEvent PrevSubView { get { return new SimConnectEvent() { Id = SimConnectEventId.PrevSubView, Name = "PREV_SUB_VIEW", Units = "N/A", Description = "Select previous view" }; } }
        private SimConnectEvent PrevView { get { return new SimConnectEvent() { Id = SimConnectEventId.PrevView, Name = "PREV_VIEW", Units = "N/A", Description = "Select previous view" }; } }
        private SimConnectEvent PropForceBetaOff { get { return new SimConnectEvent() { Id = SimConnectEventId.PropForceBetaOff, Name = "PROP_FORCE_BETA_OFF", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)", Description = "This key allows you to disable the propeller Force Beta mode, in which case the internal coded simulation logic to drive the beta is used instead of the value from PROP BETA FORCED POSITION." }; } }
        private SimConnectEvent PropForceBetaOn { get { return new SimConnectEvent() { Id = SimConnectEventId.PropForceBetaOn, Name = "PROP_FORCE_BETA_ON", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)", Description = "This keys allows you to enable the propeller Force Beta mode, in which case the sim logic to drive the beta is ignored and instead the value from PROP BETA FORCED POSITION is used." }; } }
        private SimConnectEvent PropForceBetaSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PropForceBetaSet, Name = "PROP_FORCE_BETA_SET", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)\r\n[1]: Whether or not to force the prop beta (Boolean).", Description = "This key allows you to set the propeller to be in Force Beta mode, in which case the internal coded simulation logic that normally drives the beta is ignored and instead the value from PROP BETA FORCED POSITION is used." }; } }
        private SimConnectEvent PropForceBetaToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PropForceBetaToggle, Name = "PROP_FORCE_BETA_TOGGLE", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)", Description = "This key allows you to toggle between the normal and Force Beta mode. If enabled, the Force Beta mode will prevent the internal coded simulation logic from driving the beta and instead allow you to control it with the value from PROP BETA FORCED POSITION." }; } }
        private SimConnectEvent PropForceBetaValueSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PropForceBetaValueSet, Name = "PROP_FORCE_BETA_VALUE_SET", Units = "[0]: The engine index to target (from 1 to 4, or 0 for all engines)\r\n[1]: The angle that the prop should be forced to. This is stored as the 16k representation of an angle between -180 degrees and + 180 degrees", Description = "This key allows you to set the value that the prop will attempt to reach when in Forced Beta mode (this will have the same effect as setting the PROP BETA FORCED POSITION SimVar)." }; } }
        private SimConnectEvent PropLockOff { get { return new SimConnectEvent() { Id = SimConnectEventId.PropLockOff, Name = "PROP_LOCK_OFF", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropLockOn { get { return new SimConnectEvent() { Id = SimConnectEventId.PropLockOn, Name = "PROP_LOCK_ON", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropLockSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PropLockSet, Name = "PROP_LOCK_SET", Units = "[0]: True/False (1, 0)", Description = "" }; } }
        private SimConnectEvent PropLockToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PropLockToggle, Name = "PROP_LOCK_TOGGLE", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropPitchAxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchAxisSetEx1, Name = "PROP_PITCH_AXIS_SET_EX1", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropPitchDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchDecr, Name = "PROP_PITCH_DECR", Units = "N/A", Description = "Decrement prop pitch levers" }; } }
        private SimConnectEvent PropPitchDecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchDecrSmall, Name = "PROP_PITCH_DECR_SMALL", Units = "N/A", Description = "Decrease prop levers small" }; } }
        private SimConnectEvent PropPitchDecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchDecreaseEx1, Name = "PROP_PITCH_DECREASE_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitchDecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchDecreaseSmallEx1, Name = "PROP_PITCH_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitchHi { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchHi, Name = "PROP_PITCH_HI", Units = "N/A", Description = "Set prop pitch levers min (hi pitch)" }; } }
        private SimConnectEvent PropPitchHiEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchHiEx1, Name = "PROP_PITCH_HI_EX1", Units = "N/A", Description = "Set prop pitch levers min (hi pitch)" }; } }
        private SimConnectEvent PropPitchIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchIncr, Name = "PROP_PITCH_INCR", Units = "N/A", Description = "Increment prop pitch levers" }; } }
        private SimConnectEvent PropPitchIncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchIncrSmall, Name = "PROP_PITCH_INCR_SMALL", Units = "N/A", Description = "Increment prop pitch levers small" }; } }
        private SimConnectEvent PropPitchIncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchIncreaseEx1, Name = "PROP_PITCH_INCREASE_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitchIncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchIncreaseSmallEx1, Name = "PROP_PITCH_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitchLo { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchLo, Name = "PROP_PITCH_LO", Units = "N/A", Description = "Set prop pitch levers max (lo pitch)" }; } }
        private SimConnectEvent PropPitchLoEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchLoEx1, Name = "PROP_PITCH_LO_EX1", Units = "N/A", Description = "Set prop pitch levers max (lo pitch)" }; } }
        private SimConnectEvent PropPitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitchSet, Name = "PROP_PITCH_SET", Units = "N/A", Description = "Set prop pitch levers (0 to 16383)" }; } }
        private SimConnectEvent PropPitch1AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1AxisSetEx1, Name = "PROP_PITCH1_AXIS_SET_EX1", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropPitch1Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1Decr, Name = "PROP_PITCH1_DECR", Units = "N/A", Description = "Decrement prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch1DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1DecrSmall, Name = "PROP_PITCH1_DECR_SMALL", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch1DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1DecreaseEx1, Name = "PROP_PITCH1_DECREASE_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch1DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1DecreaseSmallEx1, Name = "PROP_PITCH1_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch1Hi { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1Hi, Name = "PROP_PITCH1_HI", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch1HiEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1HiEx1, Name = "PROP_PITCH1_HI_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch1Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1Incr, Name = "PROP_PITCH1_INCR", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch1IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1IncrSmall, Name = "PROP_PITCH1_INCR_SMALL", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch1IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1IncreaseEx1, Name = "PROP_PITCH1_INCREASE_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch1IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1IncreaseSmallEx1, Name = "PROP_PITCH1_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch1Lo { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1Lo, Name = "PROP_PITCH1_LO", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch1LoEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1LoEx1, Name = "PROP_PITCH1_LO_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch1Set, Name = "PROP_PITCH1_SET", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 exact value (0 to 16383)" }; } }
        private SimConnectEvent PropPitch2AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2AxisSetEx1, Name = "PROP_PITCH2_AXIS_SET_EX1", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropPitch2Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2Decr, Name = "PROP_PITCH2_DECR", Units = "N/A", Description = "Decrement prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch2DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2DecrSmall, Name = "PROP_PITCH2_DECR_SMALL", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch2DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2DecreaseEx1, Name = "PROP_PITCH2_DECREASE_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch2DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2DecreaseSmallEx1, Name = "PROP_PITCH2_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch2Hi { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2Hi, Name = "PROP_PITCH2_HI", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch2HiEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2HiEx1, Name = "PROP_PITCH2_HI_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch2Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2Incr, Name = "PROP_PITCH2_INCR", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch2IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2IncrSmall, Name = "PROP_PITCH2_INCR_SMALL", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch2IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2IncreaseEx1, Name = "PROP_PITCH2_INCREASE_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch2IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2IncreaseSmallEx1, Name = "PROP_PITCH2_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch2Lo { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2Lo, Name = "PROP_PITCH2_LO", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch2LoEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2LoEx1, Name = "PROP_PITCH2_LO_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch2Set, Name = "PROP_PITCH2_SET", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 exact value (0 to 16383)" }; } }
        private SimConnectEvent PropPitch3AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3AxisSetEx1, Name = "PROP_PITCH3_AXIS_SET_EX1", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropPitch3Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3Decr, Name = "PROP_PITCH3_DECR", Units = "N/A", Description = "Decrement prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch3DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3DecrSmall, Name = "PROP_PITCH3_DECR_SMALL", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch3DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3DecreaseEx1, Name = "PROP_PITCH3_DECREASE_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch3DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3DecreaseSmallEx1, Name = "PROP_PITCH3_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch3Hi { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3Hi, Name = "PROP_PITCH3_HI", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch3HiEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3HiEx1, Name = "PROP_PITCH3_HI_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch3Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3Incr, Name = "PROP_PITCH3_INCR", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch3IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3IncrSmall, Name = "PROP_PITCH3_INCR_SMALL", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch3IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3IncreaseEx1, Name = "PROP_PITCH3_INCREASE_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch3IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3IncreaseSmallEx1, Name = "PROP_PITCH3_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch3Lo { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3Lo, Name = "PROP_PITCH3_LO", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch3LoEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3LoEx1, Name = "PROP_PITCH3_LO_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch3Set, Name = "PROP_PITCH3_SET", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 exact value (0 to 16383)" }; } }
        private SimConnectEvent PropPitch4AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4AxisSetEx1, Name = "PROP_PITCH4_AXIS_SET_EX1", Units = "N/A", Description = "" }; } }
        private SimConnectEvent PropPitch4Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4Decr, Name = "PROP_PITCH4_DECR", Units = "N/A", Description = "Decrement prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch4DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4DecrSmall, Name = "PROP_PITCH4_DECR_SMALL", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch4DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4DecreaseEx1, Name = "PROP_PITCH4_DECREASE_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch4DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4DecreaseSmallEx1, Name = "PROP_PITCH4_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease prop lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch4Hi { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4Hi, Name = "PROP_PITCH4_HI", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch4HiEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4HiEx1, Name = "PROP_PITCH4_HI_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 min (hi pitch)" }; } }
        private SimConnectEvent PropPitch4Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4Incr, Name = "PROP_PITCH4_INCR", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4" }; } }
        private SimConnectEvent PropPitch4IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4IncrSmall, Name = "PROP_PITCH4_INCR_SMALL", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch4IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4IncreaseEx1, Name = "PROP_PITCH4_INCREASE_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch4IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4IncreaseSmallEx1, Name = "PROP_PITCH4_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment prop pitch lever 1/2/3/4 small" }; } }
        private SimConnectEvent PropPitch4Lo { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4Lo, Name = "PROP_PITCH4_LO", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch4LoEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4LoEx1, Name = "PROP_PITCH4_LO_EX1", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 max (lo pitch)" }; } }
        private SimConnectEvent PropPitch4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.PropPitch4Set, Name = "PROP_PITCH4_SET", Units = "N/A", Description = "Set prop pitch lever 1/2/3/4 exact value (0 to 16383)" }; } }
        private SimConnectEvent PropellerReverseThrustHold { get { return new SimConnectEvent() { Id = SimConnectEventId.PropellerReverseThrustHold, Name = "PROPELLER_REVERSE_THRUST_HOLD", Units = "N/A", Description = "Turns propeller synchronization switch on" }; } }
        private SimConnectEvent PropellerReverseThrustToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.PropellerReverseThrustToggle, Name = "PROPELLER_REVERSE_THRUST_TOGGLE", Units = "N/A", Description = "Turns propeller synchronization switch on" }; } }
        private SimConnectEvent RadiatorCoolingFlapsDown { get { return new SimConnectEvent() { Id = SimConnectEventId.RadiatorCoolingFlapsDown, Name = "RADIATOR_COOLING_FLAPS_DOWN", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent RadiatorCoolingFlapsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadiatorCoolingFlapsSet, Name = "RADIATOR_COOLING_FLAPS_SET", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent RadiatorCoolingFlapsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadiatorCoolingFlapsToggle, Name = "RADIATOR_COOLING_FLAPS_TOGGLE", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent RadiatorCoolingFlapsUp { get { return new SimConnectEvent() { Id = SimConnectEventId.RadiatorCoolingFlapsUp, Name = "RADIATOR_COOLING_FLAPS_UP", Units = "N/A", Description = "Increment engine 1/2/3/4 cowl flap lever" }; } }
        private SimConnectEvent RadioAdfIdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdfIdentDisable, Name = "RADIO_ADF_IDENT_DISABLE", Units = "N/A", Description = "Turns the ADF 1 / 2 ID off." }; } }
        private SimConnectEvent RadioAdfIdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdfIdentEnable, Name = "RADIO_ADF_IDENT_ENABLE", Units = "N/A", Description = "Turns the ADF 1 / 2 ID on." }; } }
        private SimConnectEvent RadioAdfIdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdfIdentSet, Name = "RADIO_ADF_IDENT_SET", Units = "[0]: True/False (1, 0)", Description = "Sets the ADF 1 / 2 ID on (1) or off (2)." }; } }
        private SimConnectEvent RadioAdfIdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdfIdentToggle, Name = "RADIO_ADF_IDENT_TOGGLE", Units = "N/A", Description = "Toggles the ADF 1 / 2 ID between on (1) and off (0)." }; } }
        private SimConnectEvent RadioAdf2IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdf2IdentDisable, Name = "RADIO_ADF2_IDENT_DISABLE", Units = "N/A", Description = "Turns the ADF 1 / 2 ID off." }; } }
        private SimConnectEvent RadioAdf2IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdf2IdentEnable, Name = "RADIO_ADF2_IDENT_ENABLE", Units = "N/A", Description = "Turns the ADF 1 / 2 ID on." }; } }
        private SimConnectEvent RadioAdf2IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdf2IdentSet, Name = "RADIO_ADF2_IDENT_SET", Units = "[0]: True/False (1, 0)", Description = "Sets the ADF 1 / 2 ID on (1) or off (2)." }; } }
        private SimConnectEvent RadioAdf2IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioAdf2IdentToggle, Name = "RADIO_ADF2_IDENT_TOGGLE", Units = "N/A", Description = "Toggles the ADF 1 / 2 ID between on (1) and off (0)." }; } }
        private SimConnectEvent RadioComm1AutoswitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioComm1AutoswitchToggle, Name = "RADIO_COMM1_AUTOSWITCH_TOGGLE", Units = "N/A", Description = "Toggles the COM 1/2 autoswitch on (1) or off (0)." }; } }
        private SimConnectEvent RadioComm2AutoswitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioComm2AutoswitchToggle, Name = "RADIO_COMM2_AUTOSWITCH_TOGGLE", Units = "N/A", Description = "Toggles the COM 1/2 autoswitch on (1) or off (0)." }; } }
        private SimConnectEvent RadioCommnav1TestToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioCommnav1TestToggle, Name = "RADIO_COMMNAV1_TEST_TOGGLE", Units = "N/A", Description = "Places COM 1/2/3 in \"test mode\"." }; } }
        private SimConnectEvent RadioCommnav2TestToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioCommnav2TestToggle, Name = "RADIO_COMMNAV2_TEST_TOGGLE", Units = "N/A", Description = "NOTE: Currently, placing COMs in test mode will have no effect other than to set the SimVar COM TEST." }; } }
        private SimConnectEvent RadioCommnav3TestToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioCommnav3TestToggle, Name = "RADIO_COMMNAV3_TEST_TOGGLE", Units = "N/A", Description = "NOTE: Currently, placing COMs in test mode will have no effect other than to set the SimVar COM TEST." }; } }
        private SimConnectEvent RadioDme1IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme1IdentDisable, Name = "RADIO_DME1_IDENT_DISABLE", Units = "N/A", Description = "Turns the DME 1 / 2 ID off (0)." }; } }
        private SimConnectEvent RadioDme1IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme1IdentEnable, Name = "RADIO_DME1_IDENT_ENABLE", Units = "N/A", Description = "Turns the DME 1 ID on (1)." }; } }
        private SimConnectEvent RadioDme1IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme1IdentSet, Name = "RADIO_DME1_IDENT_SET", Units = "[0]: Bool", Description = "Sets the DME 1 /2 ID to on (1) or off (0)." }; } }
        private SimConnectEvent RadioDme1IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme1IdentToggle, Name = "RADIO_DME1_IDENT_TOGGLE", Units = "N/A", Description = "Toggles the DME 1 / 2 ID between on (1) and off (0)." }; } }
        private SimConnectEvent RadioDme2IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme2IdentDisable, Name = "RADIO_DME2_IDENT_DISABLE", Units = "N/A", Description = "Turns the DME 1 / 2 ID off (0)." }; } }
        private SimConnectEvent RadioDme2IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme2IdentEnable, Name = "RADIO_DME2_IDENT_ENABLE", Units = "N/A", Description = "Turns the DME 1 ID on (1)." }; } }
        private SimConnectEvent RadioDme2IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme2IdentSet, Name = "RADIO_DME2_IDENT_SET", Units = "[0]: Bool", Description = "Sets the DME 1 /2 ID to on (1) or off (0)." }; } }
        private SimConnectEvent RadioDme2IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioDme2IdentToggle, Name = "RADIO_DME2_IDENT_TOGGLE", Units = "N/A", Description = "Toggles the DME 1 / 2 ID between on (1) and off (0)." }; } }
        private SimConnectEvent RadioNav1AutoswitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioNav1AutoswitchToggle, Name = "RADIO_NAV1_AUTOSWITCH_TOGGLE", Units = "[0] Volume value (0 - 100)", Description = "Sets the volume for the chosen NAV, from 0 to 100 (interpolated in the simulation to a value from 0 to 1)." }; } }
        private SimConnectEvent RadioNav2AutoswitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioNav2AutoswitchToggle, Name = "RADIO_NAV2_AUTOSWITCH_TOGGLE", Units = "[0] Volume value (0 - 100)", Description = "Sets the volume for the chosen NAV, from 0 to 100 (interpolated in the simulation to a value from 0 to 1)." }; } }
        private SimConnectEvent RadioSelectedDmeIdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioSelectedDmeIdentDisable, Name = "RADIO_SELECTED_DME_IDENT_DISABLE", Units = "N/A", Description = "Turns off the identification sound for the selected DME." }; } }
        private SimConnectEvent RadioSelectedDmeIdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioSelectedDmeIdentEnable, Name = "RADIO_SELECTED_DME_IDENT_ENABLE", Units = "N/A", Description = "Turns on the identification sound for the selected DME." }; } }
        private SimConnectEvent RadioSelectedDmeIdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioSelectedDmeIdentSet, Name = "RADIO_SELECTED_DME_IDENT_SET", Units = "[0]: Bool", Description = "Sets the DME identification sound to the given filename." }; } }
        private SimConnectEvent RadioSelectedDmeIdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioSelectedDmeIdentToggle, Name = "RADIO_SELECTED_DME_IDENT_TOGGLE", Units = "N/A", Description = "Turns on or off the identification sound for the selected DME." }; } }
        private SimConnectEvent RadioVor1IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor1IdentDisable, Name = "RADIO_VOR1_IDENT_DISABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID off." }; } }
        private SimConnectEvent RadioVor1IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor1IdentEnable, Name = "RADIO_VOR1_IDENT_ENABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID on." }; } }
        private SimConnectEvent RadioVor1IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor1IdentSet, Name = "RADIO_VOR1_IDENT_SET", Units = "[0]: Bool", Description = "Sets VOR 1/2/3/4 ID (on/off)." }; } }
        private SimConnectEvent RadioVor1IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor1IdentToggle, Name = "RADIO_VOR1_IDENT_TOGGLE", Units = "N/A", Description = "Toggles VOR 1/2/3/4 ID between on and off." }; } }
        private SimConnectEvent RadioVor2IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor2IdentDisable, Name = "RADIO_VOR2_IDENT_DISABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID off." }; } }
        private SimConnectEvent RadioVor2IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor2IdentEnable, Name = "RADIO_VOR2_IDENT_ENABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID on." }; } }
        private SimConnectEvent RadioVor2IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor2IdentSet, Name = "RADIO_VOR2_IDENT_SET", Units = "[0]: Bool", Description = "Sets VOR 1/2/3/4 ID (on/off)." }; } }
        private SimConnectEvent RadioVor2IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor2IdentToggle, Name = "RADIO_VOR2_IDENT_TOGGLE", Units = "N/A", Description = "Toggles VOR 1/2/3/4 ID between on and off." }; } }
        private SimConnectEvent RadioVor3IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor3IdentDisable, Name = "RADIO_VOR3_IDENT_DISABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID off." }; } }
        private SimConnectEvent RadioVor3IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor3IdentEnable, Name = "RADIO_VOR3_IDENT_ENABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID on." }; } }
        private SimConnectEvent RadioVor3IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor3IdentSet, Name = "RADIO_VOR3_IDENT_SET", Units = "[0]: Bool", Description = "Sets VOR 1/2/3/4 ID (on/off)." }; } }
        private SimConnectEvent RadioVor3IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor3IdentToggle, Name = "RADIO_VOR3_IDENT_TOGGLE", Units = "N/A", Description = "Toggles VOR 1/2/3/4 ID between on and off." }; } }
        private SimConnectEvent RadioVor4IdentDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor4IdentDisable, Name = "RADIO_VOR4_IDENT_DISABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID off." }; } }
        private SimConnectEvent RadioVor4IdentEnable { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor4IdentEnable, Name = "RADIO_VOR4_IDENT_ENABLE", Units = "N/A", Description = "Turns VOR 1/2/3/4 ID on." }; } }
        private SimConnectEvent RadioVor4IdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor4IdentSet, Name = "RADIO_VOR4_IDENT_SET", Units = "[0]: Bool", Description = "Sets VOR 1/2/3/4 ID (on/off)." }; } }
        private SimConnectEvent RadioVor4IdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RadioVor4IdentToggle, Name = "RADIO_VOR4_IDENT_TOGGLE", Units = "N/A", Description = "Toggles VOR 1/2/3/4 ID between on and off." }; } }
        private SimConnectEvent ReadoutsFlight { get { return new SimConnectEvent() { Id = SimConnectEventId.ReadoutsFlight, Name = "READOUTS_FLIGHT", Units = "[0]: Bool", Description = "Cycle through information readouts" }; } }
        private SimConnectEvent ReadoutsSlew { get { return new SimConnectEvent() { Id = SimConnectEventId.ReadoutsSlew, Name = "READOUTS_SLEW", Units = "[0]: Bool", Description = "Cycle through information readouts while in slew" }; } }
        private SimConnectEvent RecognitionLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RecognitionLightsSet, Name = "RECOGNITION_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light circuit index", Description = "Set recognition lights on/off (1,0)" }; } }
        private SimConnectEvent RefreshScenery { get { return new SimConnectEvent() { Id = SimConnectEventId.RefreshScenery, Name = "REFRESH_SCENERY", Units = "[0]: Bool", Description = "Reloads scenery.\r\nDeprecated, do not use." }; } }
        private SimConnectEvent ReleaseDropTank1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ReleaseDropTank1, Name = "RELEASE_DROP_TANK_1", Units = "[1]: A value between 0 and 16384", Description = "Release the first external tank that can be jettisoned." }; } }
        private SimConnectEvent ReleaseDropTank2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ReleaseDropTank2, Name = "RELEASE_DROP_TANK_2", Units = "[1]: A value between 0 and 16384", Description = "Release the second external tank that can be jettisoned." }; } }
        private SimConnectEvent ReleaseDropTankAll { get { return new SimConnectEvent() { Id = SimConnectEventId.ReleaseDropTankAll, Name = "RELEASE_DROP_TANK_ALL", Units = "[1]: A value between 0 and 16384", Description = "Release all external tanks that can be jettisoned." }; } }
        private SimConnectEvent ReleaseDroppableObjects { get { return new SimConnectEvent() { Id = SimConnectEventId.ReleaseDroppableObjects, Name = "RELEASE_DROPPABLE_OBJECTS", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent ReloadPanels { get { return new SimConnectEvent() { Id = SimConnectEventId.ReloadPanels, Name = "RELOAD_PANELS", Units = "N/A", Description = "Reload panel data" }; } }
        private SimConnectEvent ReloadUserAircraft { get { return new SimConnectEvent() { Id = SimConnectEventId.ReloadUserAircraft, Name = "RELOAD_USER_AIRCRAFT", Units = "N/A", Description = "Reloads the user aircraft data (from cache if same type loaded as an AI,otherwise from disk)" }; } }
        private SimConnectEvent RepairAndRefuel { get { return new SimConnectEvent() { Id = SimConnectEventId.RepairAndRefuel, Name = "REPAIR_AND_REFUEL", Units = "[1]: A value between 0 and 16384", Description = "Fully repair and refuel the user aircraft. Ignored if flight realism is enforced." }; } }
        private SimConnectEvent ReplayStop { get { return new SimConnectEvent() { Id = SimConnectEventId.ReplayStop, Name = "REPLAY_STOP", Units = "N/A", Description = "Stops replay system playback." }; } }
        private SimConnectEvent RequestCatering { get { return new SimConnectEvent() { Id = SimConnectEventId.RequestCatering, Name = "REQUEST_CATERING", Units = "N/A", Description = "Requests catering truck on the nearest airport." }; } }
        private SimConnectEvent RequestFuelKey { get { return new SimConnectEvent() { Id = SimConnectEventId.RequestFuelKey, Name = "REQUEST_FUEL_KEY", Units = "[1]: A value between 0 and 16384", Description = "Request a fuel truck. The aircraft must be in a parking spot for this to be successful." }; } }
        private SimConnectEvent RequestLuggage { get { return new SimConnectEvent() { Id = SimConnectEventId.RequestLuggage, Name = "REQUEST_LUGGAGE", Units = "N/A", Description = "Requests a baggage loader from the nearest airport." }; } }
        private SimConnectEvent RequestPowerSupply { get { return new SimConnectEvent() { Id = SimConnectEventId.RequestPowerSupply, Name = "REQUEST_POWER_SUPPLY", Units = "N/A", Description = "Requests ground power unit from the nearest airport." }; } }
        private SimConnectEvent ResetGForceIndicator { get { return new SimConnectEvent() { Id = SimConnectEventId.ResetGForceIndicator, Name = "RESET_G_FORCE_INDICATOR", Units = "N/A", Description = "Resets max/min indicated G force to 1.0." }; } }
        private SimConnectEvent ResetMaxRpmIndicator { get { return new SimConnectEvent() { Id = SimConnectEventId.ResetMaxRpmIndicator, Name = "RESET_MAX_RPM_INDICATOR", Units = "N/A", Description = "Reset max indicated engine rpm to 0." }; } }
        private SimConnectEvent RetractFloatSwitchDec { get { return new SimConnectEvent() { Id = SimConnectEventId.RetractFloatSwitchDec, Name = "RETRACT_FLOAT_SWITCH_DEC", Units = "N/A", Description = "If the plane has retractable floats, moves the retract position from Extend to Neutral, or Neutral to Retract." }; } }
        private SimConnectEvent RetractFloatSwitchInc { get { return new SimConnectEvent() { Id = SimConnectEventId.RetractFloatSwitchInc, Name = "RETRACT_FLOAT_SWITCH_INC", Units = "N/A", Description = "If the plane has retractable floats, moves the retract position from Retract to Neutral, or Neutral to Extend." }; } }
        private SimConnectEvent RotorAxisTailRotorSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorAxisTailRotorSet, Name = "ROTOR_AXIS_TAIL_ROTOR_SET", Units = "[0]: Throttle value (0 to 16384)", Description = "Set all throttles based on the input value. The input is between 0 and 16384, which will be normalised to a value between 0 and 1." }; } }
        private SimConnectEvent RotorBrake { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorBrake, Name = "ROTOR_BRAKE", Units = "[0]: value", Description = "Sets rotor brake switch on. Deprecated in favour of ROTOR_BRAKE_ON." }; } }
        private SimConnectEvent RotorBrakeOff { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorBrakeOff, Name = "ROTOR_BRAKE_OFF", Units = "N/A", Description = "Switches off the rotor brake switch ." }; } }
        private SimConnectEvent RotorBrakeOn { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorBrakeOn, Name = "ROTOR_BRAKE_ON", Units = "N/A", Description = "Switches on the rotor brake switch ." }; } }
        private SimConnectEvent RotorBrakeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorBrakeSet, Name = "ROTOR_BRAKE_SET", Units = "[0]: True/False (1, 0)", Description = "Set the rotor brake switch to be on (1) or off (0)." }; } }
        private SimConnectEvent RotorBrakeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorBrakeToggle, Name = "ROTOR_BRAKE_TOGGLE", Units = "N/A", Description = "Toggle the rotor brake switch between on (1) and off (0)." }; } }
        private SimConnectEvent RotorClutchSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorClutchSwitchSet, Name = "ROTOR_CLUTCH_SWITCH_SET", Units = "[0]: True/False (1, 0)", Description = "Sets the rotor clutch switch to on (1) or off (0)." }; } }
        private SimConnectEvent RotorClutchSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorClutchSwitchToggle, Name = "ROTOR_CLUTCH_SWITCH_TOGGLE", Units = "N/A", Description = "Toggles the rotor clutch switch between on (1) and off (0)." }; } }
        private SimConnectEvent RotorGovSwitchOff { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorGovSwitchOff, Name = "ROTOR_GOV_SWITCH_OFF", Units = "[0]: engine", Description = "Sets the rotor governor switch to off (0).\r\nAn engine index of 0 targets all engines, and any other value targets that specific engine." }; } }
        private SimConnectEvent RotorGovSwitchOn { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorGovSwitchOn, Name = "ROTOR_GOV_SWITCH_ON", Units = "[0]: engine", Description = "Sets the rotor governor switch to on (1).\r\nAn engine index of 0 targets all engines, and any other value targets that specific engine." }; } }
        private SimConnectEvent RotorGovSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorGovSwitchSet, Name = "ROTOR_GOV_SWITCH_SET", Units = "[0]: True/False (1, 0)\r\n[0]: engine", Description = "Sets the rotor governor switch to on/off (1,0).\r\nAn engine index of 0 targets all engines, and any other value targets that specific engine." }; } }
        private SimConnectEvent RotorGovSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorGovSwitchToggle, Name = "ROTOR_GOV_SWITCH_TOGGLE", Units = "[0]: engine", Description = "Toggles the rotor governor switch between on (1) and off (0).\r\nAn engine index of 0 targets all engines, and any other value targets that specific engine." }; } }
        private SimConnectEvent RotorLateralTrimDec { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorLateralTrimDec, Name = "ROTOR_LATERAL_TRIM_DEC", Units = "N/A", Description = "Decrements the roll (lateral) rotor trim by the amount specified by the parameter right_trim_step." }; } }
        private SimConnectEvent RotorLateralTrimInc { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorLateralTrimInc, Name = "ROTOR_LATERAL_TRIM_INC", Units = "N/A", Description = "Increments the roll (lateral) rotor trim by the amount specified by the parameter right_trim_step." }; } }
        private SimConnectEvent RotorLateralTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorLateralTrimSet, Name = "ROTOR_LATERAL_TRIM_SET", Units = "[0]: Pitch angle (+/- 16384)", Description = "Sets the roll (lateral) rotor trim to a value between -1 and 1 (interpolated from the +/-16384 input value)." }; } }
        private SimConnectEvent RotorLongitudinalTrimDec { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorLongitudinalTrimDec, Name = "ROTOR_LONGITUDINAL_TRIM_DEC", Units = "N/A", Description = "Decrements the pitch (longitudinal) rotor trim by the amount specified by the parameter front_trim_step." }; } }
        private SimConnectEvent RotorLongitudinalTrimInc { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorLongitudinalTrimInc, Name = "ROTOR_LONGITUDINAL_TRIM_INC", Units = "N/A", Description = "Increments the pitch (longitudinal) rotor trim by the amount specified by the parameter front_trim_step." }; } }
        private SimConnectEvent RotorLongitudinalTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorLongitudinalTrimSet, Name = "ROTOR_LONGITUDINAL_TRIM_SET", Units = "[0]: Pitch angle (+/- 16384)", Description = "Sets the pitch (longitudinal) rotor trim to a value between -1 and 1 (interpolated from the +/-16384 input value)." }; } }
        private SimConnectEvent RotorTrimReset { get { return new SimConnectEvent() { Id = SimConnectEventId.RotorTrimReset, Name = "ROTOR_TRIM_RESET", Units = "N/A", Description = "Resets the rotor time values to their default." }; } }
        private SimConnectEvent RpmSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RpmSlotIndexSet, Name = "RPM_SLOT_INDEX_SET", Units = "N/A", Description = "Set autopilot RPM slot index." }; } }
        private SimConnectEvent RudderAxisMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderAxisMinus, Name = "RUDDER_AXIS_MINUS", Units = "N/A", Description = "Sets rudder position (-16383 - +16383)" }; } }
        private SimConnectEvent RudderAxisPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderAxisPlus, Name = "RUDDER_AXIS_PLUS", Units = "N/A", Description = "Sets rudder position (-16383 - +16383)" }; } }
        private SimConnectEvent RudderCenter { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderCenter, Name = "RUDDER_CENTER", Units = "N/A", Description = "Centers rudder position" }; } }
        private SimConnectEvent RudderLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderLeft, Name = "RUDDER_LEFT", Units = "N/A", Description = "Increments rudder left" }; } }
        private SimConnectEvent RudderRight { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderRight, Name = "RUDDER_RIGHT", Units = "N/A", Description = "Increments rudder right" }; } }
        private SimConnectEvent RudderSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderSet, Name = "RUDDER_SET", Units = "[0]: Value to set", Description = "Sets rudder position (-16383 - +16383)" }; } }
        private SimConnectEvent RudderTrimDisabledSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimDisabledSet, Name = "RUDDER_TRIM_DISABLED_SET", Units = "[0]: Bool", Description = "Enables (TRUE) or disables (FALSE) the rudder trim." }; } }
        private SimConnectEvent RudderTrimDisabledToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimDisabledToggle, Name = "RUDDER_TRIM_DISABLED_TOGGLE", Units = "N/A", Description = "Toggles the rudder trim on (TRUE) or off (FALSE)." }; } }
        private SimConnectEvent RudderTrimLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimLeft, Name = "RUDDER_TRIM_LEFT", Units = "N/A", Description = "Increments rudder trim left" }; } }
        private SimConnectEvent RudderTrimReset { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimReset, Name = "RUDDER_TRIM_RESET", Units = "N/A", Description = "Increments rudder trim left" }; } }
        private SimConnectEvent RudderTrimRight { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimRight, Name = "RUDDER_TRIM_RIGHT", Units = "N/A", Description = "Increments aileron trim right" }; } }
        private SimConnectEvent RudderTrimSet { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimSet, Name = "RUDDER_TRIM_SET", Units = "[0]: Value to set", Description = "Sets the rudder trim value, between -100 and 100." }; } }
        private SimConnectEvent RudderTrimSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.RudderTrimSetEx1, Name = "RUDDER_TRIM_SET_EX1", Units = "[0]: Value to set", Description = "Sets the rudder trim value, between -16383 and 16383." }; } }
        private SimConnectEvent ScriptEvent1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ScriptEvent1, Name = "SCRIPT_EVENT_1", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent ScriptEvent2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ScriptEvent2, Name = "SCRIPT_EVENT_2", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent SeeOwnAcOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SeeOwnAcOff, Name = "SEE_OWN_AC_OFF", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent SeeOwnAcOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SeeOwnAcOn, Name = "SEE_OWN_AC_ON", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent SeeOwnAcSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SeeOwnAcSet, Name = "SEE_OWN_AC_SET", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent SeeOwnAcToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.SeeOwnAcToggle, Name = "SEE_OWN_AC_TOGGLE", Units = "N/A", Description = "Release one dropable object. Multiple key events will release multiple objects." }; } }
        private SimConnectEvent Select1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Select1, Name = "SELECT_1", Units = "N/A", Description = "Sets \"selected\" index (for other events) to 1/2/3/4." }; } }
        private SimConnectEvent Select2 { get { return new SimConnectEvent() { Id = SimConnectEventId.Select2, Name = "SELECT_2", Units = "N/A", Description = "Sets \"selected\" index (for other events) to 1/2/3/4." }; } }
        private SimConnectEvent Select3 { get { return new SimConnectEvent() { Id = SimConnectEventId.Select3, Name = "SELECT_3", Units = "N/A", Description = "Sets \"selected\" index (for other events) to 1/2/3/4." }; } }
        private SimConnectEvent Select4 { get { return new SimConnectEvent() { Id = SimConnectEventId.Select4, Name = "SELECT_4", Units = "N/A", Description = "Sets \"selected\" index (for other events) to 1/2/3/4." }; } }
        private SimConnectEvent SelectNextTarget { get { return new SimConnectEvent() { Id = SimConnectEventId.SelectNextTarget, Name = "SELECT_NEXT_TARGET", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent SelectPrevTarget { get { return new SimConnectEvent() { Id = SimConnectEventId.SelectPrevTarget, Name = "SELECT_PREV_TARGET", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent SetAutobrakeControl { get { return new SimConnectEvent() { Id = SimConnectEventId.SetAutobrakeControl, Name = "SET_AUTOBRAKE_CONTROL", Units = "[0]: autobreak level", Description = "Uses the input parameter [0] to set the autobreak level from 0 (off) to the value set for the auto_brakes parameter (maximum breaking). If a value greater than that specified for auto_brakes is given as input, it will be clamped to the auto_brakes value." }; } }
        private SimConnectEvent SetDecisionAltitudeMsl { get { return new SimConnectEvent() { Id = SimConnectEventId.SetDecisionAltitudeMsl, Name = "SET_DECISION_ALTITUDE_MSL", Units = "[0]: height (m)", Description = "Set the MSL decision height reference, in meters." }; } }
        private SimConnectEvent SetExternalPower { get { return new SimConnectEvent() { Id = SimConnectEventId.SetExternalPower, Name = "SET_EXTERNAL_POWER", Units = "[0]: external power index\r\n[1]: The state on/off (1, 0)", Description = "Set the external power switch state. The index is the value assigned to the circuit N when the externalpower.N was defined." }; } }
        private SimConnectEvent SetFuelTransferAft { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelTransferAft, Name = "SET_FUEL_TRANSFER_AFT", Units = "[0]: Valve Index", Description = "Set the fuel transfer system to use the \"aft\" setting, which pumps from tank 2 to tank 1." }; } }
        private SimConnectEvent SetFuelTransferAuto { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelTransferAuto, Name = "SET_FUEL_TRANSFER_AUTO", Units = "[0]: Valve Index", Description = "Set the fuel transfer pump to automatically balance the fuel in tanks 1 and 2 to maintain the CG." }; } }
        private SimConnectEvent SetFuelTransferCustom { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelTransferCustom, Name = "SET_FUEL_TRANSFER_CUSTOM", Units = "[0]: Valve Index", Description = "Set the fuel transfer mode to the \"custom\" setting. Requires that at least 1 transfer pump has been defined in the flight_model.cfg file using the fuel_transfer_pump.N parameter." }; } }
        private SimConnectEvent SetFuelTransferForward { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelTransferForward, Name = "SET_FUEL_TRANSFER_FORWARD", Units = "[0]: Valve Index", Description = "Set the fuel transfer system to use the \"forward\" setting, which pumps from tank 1 to tank 2." }; } }
        private SimConnectEvent SetFuelTransferOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelTransferOff, Name = "SET_FUEL_TRANSFER_OFF", Units = "[0]: Valve Index", Description = "Set the fuel transfer pump to off." }; } }
        private SimConnectEvent SetFuelValveEng1 { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelValveEng1, Name = "SET_FUEL_VALVE_ENG1", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent SetFuelValveEng2 { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelValveEng2, Name = "SET_FUEL_VALVE_ENG2", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent SetFuelValveEng3 { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelValveEng3, Name = "SET_FUEL_VALVE_ENG3", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent SetFuelValveEng4 { get { return new SimConnectEvent() { Id = SimConnectEventId.SetFuelValveEng4, Name = "SET_FUEL_VALVE_ENG4", Units = "N/A", Description = "Set engine 1/2/3/4 mixture." }; } }
        private SimConnectEvent SetHeloGovBeep { get { return new SimConnectEvent() { Id = SimConnectEventId.SetHeloGovBeep, Name = "SET_HELO_GOV_BEEP", Units = "[0]: value\r\n[1]: engine", Description = "This is used to set the indexed helicopter engine trimmer tot he given value directly as a negative or positive deviation from 1, where 1 is the rated nominal engine RPM.\r\nThe final engine trimmer value will be limited according to the engine_trim_min and engine_trim_max settings. " }; } }
        private SimConnectEvent SetLaunchBarSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.SetLaunchBarSwitch, Name = "SET_LAUNCH_BAR_SWITCH", Units = "[0]: Bool", Description = "Set the switch of the launch bar extension system to be on or off." }; } }
        private SimConnectEvent SetReverseThrustOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SetReverseThrustOff, Name = "SET_REVERSE_THRUST_OFF", Units = "N/A", Description = "Turn off throttle reverse thrust for all engines." }; } }
        private SimConnectEvent SetReverseThrustOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SetReverseThrustOn, Name = "SET_REVERSE_THRUST_ON", Units = "N/A", Description = "Turn on throttle reverse thrust for all engines." }; } }
        private SimConnectEvent SetStarterAllHeld { get { return new SimConnectEvent() { Id = SimConnectEventId.SetStarterAllHeld, Name = "SET_STARTER_ALL_HELD", Units = "[0]: Bool", Description = "Set the Starter for all engines to on or off. If set to on (TRUE) the starter will stay on until set to off (FALSE) with another call to the event." }; } }
        private SimConnectEvent SetStarter1Held { get { return new SimConnectEvent() { Id = SimConnectEventId.SetStarter1Held, Name = "SET_STARTER1_HELD", Units = "[0]: Bool", Description = "Set the Starter for engine 1 to on or off. If set to on (TRUE) the starter will stay on, and setting the event to off (FALSE) will disable the starter, but only after the engine RPM is above the 50% threshold. To disable the starter immediately you should" }; } }
        private SimConnectEvent SetStarter2Held { get { return new SimConnectEvent() { Id = SimConnectEventId.SetStarter2Held, Name = "SET_STARTER2_HELD", Units = "[0]: Bool", Description = "Set the Starter for engine 2 to on or off. If set to on (TRUE) the starter will stay on, and setting the event to off (FALSE) will disable the starter, but only after the engine RPM is above the 50% threshold. To disable the starter immediately you should" }; } }
        private SimConnectEvent SetStarter3Held { get { return new SimConnectEvent() { Id = SimConnectEventId.SetStarter3Held, Name = "SET_STARTER3_HELD", Units = "[0]: Bool", Description = "Set the Starter for engine 3 to on or off. If set to on (TRUE) the starter will stay on, and setting the event to off (FALSE) will disable the starter, but only after the engine RPM is above the 50% threshold. To disable the starter immediately you should" }; } }
        private SimConnectEvent SetStarter4Held { get { return new SimConnectEvent() { Id = SimConnectEventId.SetStarter4Held, Name = "SET_STARTER4_HELD", Units = "[0]: Bool", Description = "Set the Starter for engine 4 to on or off. If set to on (TRUE) the starter will stay on, and setting the event to off (FALSE) will disable the starter, but only after the engine RPM is above the 50% threshold. To disable the starter immediately you should" }; } }
        private SimConnectEvent SetTailHookHandle { get { return new SimConnectEvent() { Id = SimConnectEventId.SetTailHookHandle, Name = "SET_TAIL_HOOK_HANDLE", Units = "[0]: TRUE/FALSE to set or retract the tailhook.", Description = "Sets the tail hook handle. Takes one of the following values:\r\n1 - set tail hook\r\n0 - retract tail hook" }; } }
        private SimConnectEvent SetThrottle1ReverseThrustOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle1ReverseThrustOff, Name = "SET_THROTTLE1_REVERSE_THRUST_OFF", Units = "N/A", Description = "Turn off the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle1ReverseThrustOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle1ReverseThrustOn, Name = "SET_THROTTLE1_REVERSE_THRUST_ON", Units = "N/A", Description = "Turn on the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle2ReverseThrustOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle2ReverseThrustOff, Name = "SET_THROTTLE2_REVERSE_THRUST_OFF", Units = "N/A", Description = "Turn off the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle2ReverseThrustOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle2ReverseThrustOn, Name = "SET_THROTTLE2_REVERSE_THRUST_ON", Units = "N/A", Description = "Turn on the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle3ReverseThrustOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle3ReverseThrustOff, Name = "SET_THROTTLE3_REVERSE_THRUST_OFF", Units = "N/A", Description = "Turn off the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle3ReverseThrustOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle3ReverseThrustOn, Name = "SET_THROTTLE3_REVERSE_THRUST_ON", Units = "N/A", Description = "Turn on the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle4ReverseThrustOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle4ReverseThrustOff, Name = "SET_THROTTLE4_REVERSE_THRUST_OFF", Units = "N/A", Description = "Turn off the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetThrottle4ReverseThrustOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SetThrottle4ReverseThrustOn, Name = "SET_THROTTLE4_REVERSE_THRUST_ON", Units = "N/A", Description = "Turn on the throttle reverse thrust for engine 1/2/3/4." }; } }
        private SimConnectEvent SetWingFold { get { return new SimConnectEvent() { Id = SimConnectEventId.SetWingFold, Name = "SET_WING_FOLD", Units = "[0]: TRUE/FALSE to fold or unfold wings.", Description = "Sets the wings into the folded position suitable for storage, typically on a carrier. Takes one of the following values:\r\n1 -fold wings\r\n0 - unfold wings" }; } }
        private SimConnectEvent ShutoffValveOff { get { return new SimConnectEvent() { Id = SimConnectEventId.ShutoffValveOff, Name = "SHUTOFF_VALVE_OFF", Units = "N/A", Description = "Turns off the fuel shutoff valve (used on piston engines to disable fuel arrival)." }; } }
        private SimConnectEvent ShutoffValveOn { get { return new SimConnectEvent() { Id = SimConnectEventId.ShutoffValveOn, Name = "SHUTOFF_VALVE_ON", Units = "N/A", Description = "Turns on the fuel shutoff valve (used on piston engines to enable fuel arrival)." }; } }
        private SimConnectEvent ShutoffValveToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ShutoffValveToggle, Name = "SHUTOFF_VALVE_TOGGLE", Units = "N/A", Description = "Toggle the status of the fuel shutoff valve (used on piston engine to enable/disable fuel arrival)." }; } }
        private SimConnectEvent SimRate { get { return new SimConnectEvent() { Id = SimConnectEventId.SimRate, Name = "SIM_RATE", Units = "N/A", Description = "Selects the simulation rate. Use the PLUS and MINUS events to increment/decrement the value." }; } }
        private SimConnectEvent SimRateDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.SimRateDecr, Name = "SIM_RATE_DECR", Units = "N/A", Description = "Decreases the simulation rate, which will slow down the in-simulation time." }; } }
        private SimConnectEvent SimRateIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.SimRateIncr, Name = "SIM_RATE_INCR", Units = "N/A", Description = "Increase the simulation rate, which will speed up the in-simulation time." }; } }
        private SimConnectEvent SimRateSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SimRateSet, Name = "SIM_RATE_SET", Units = "N/A", Description = "Set the simulation rate." }; } }
        private SimConnectEvent SimReset { get { return new SimConnectEvent() { Id = SimConnectEventId.SimReset, Name = "SIM_RESET", Units = "N/A", Description = "Resets aircraft state" }; } }
        private SimConnectEvent SimuiWindowHideshow { get { return new SimConnectEvent() { Id = SimConnectEventId.SimuiWindowHideshow, Name = "SIMUI_WINDOW_HIDESHOW", Units = "N/A", Description = "Display the ATC window." }; } }
        private SimConnectEvent SituationReset { get { return new SimConnectEvent() { Id = SimConnectEventId.SituationReset, Name = "SITUATION_RESET", Units = "N/A", Description = "Resets flight situation" }; } }
        private SimConnectEvent SituationSave { get { return new SimConnectEvent() { Id = SimConnectEventId.SituationSave, Name = "SITUATION_SAVE", Units = "N/A", Description = "Saves flight situation" }; } }
        private SimConnectEvent SkipAction { get { return new SimConnectEvent() { Id = SimConnectEventId.SkipAction, Name = "SKIP_ACTION", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent SlewAheadMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAheadMinus, Name = "SLEW_AHEAD_MINUS", Units = "N/A", Description = "Decrease forward slew" }; } }
        private SimConnectEvent SlewAheadPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAheadPlus, Name = "SLEW_AHEAD_PLUS", Units = "N/A", Description = "Increase forward slew" }; } }
        private SimConnectEvent SlewAltitDnFast { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitDnFast, Name = "SLEW_ALTIT_DN_FAST", Units = "N/A", Description = "Slew downward fast" }; } }
        private SimConnectEvent SlewAltitDnSlow { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitDnSlow, Name = "SLEW_ALTIT_DN_SLOW", Units = "N/A", Description = "Slew downward slow" }; } }
        private SimConnectEvent SlewAltitFreeze { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitFreeze, Name = "SLEW_ALTIT_FREEZE", Units = "N/A", Description = "Stop vertical slew" }; } }
        private SimConnectEvent SlewAltitMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitMinus, Name = "SLEW_ALTIT_MINUS", Units = "N/A", Description = "Decrease upward slew" }; } }
        private SimConnectEvent SlewAltitPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitPlus, Name = "SLEW_ALTIT_PLUS", Units = "N/A", Description = "Increase upward slew" }; } }
        private SimConnectEvent SlewAltitUpFast { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitUpFast, Name = "SLEW_ALTIT_UP_FAST", Units = "N/A", Description = "Slew upward fast" }; } }
        private SimConnectEvent SlewAltitUpSlow { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewAltitUpSlow, Name = "SLEW_ALTIT_UP_SLOW", Units = "N/A", Description = "Slew upward slow" }; } }
        private SimConnectEvent SlewBankMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewBankMinus, Name = "SLEW_BANK_MINUS", Units = "N/A", Description = "Increase left bank slew" }; } }
        private SimConnectEvent SlewBankPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewBankPlus, Name = "SLEW_BANK_PLUS", Units = "N/A", Description = "Increase right bank slew" }; } }
        private SimConnectEvent SlewFreeze { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewFreeze, Name = "SLEW_FREEZE", Units = "N/A", Description = "Stop all slew" }; } }
        private SimConnectEvent SlewHeadingMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewHeadingMinus, Name = "SLEW_HEADING_MINUS", Units = "N/A", Description = "Increase slew heading to the left" }; } }
        private SimConnectEvent SlewHeadingPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewHeadingPlus, Name = "SLEW_HEADING_PLUS", Units = "N/A", Description = "Increase slew heading to the right" }; } }
        private SimConnectEvent SlewLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewLeft, Name = "SLEW_LEFT", Units = "N/A", Description = "Slew to the left" }; } }
        private SimConnectEvent SlewOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewOff, Name = "SLEW_OFF", Units = "N/A", Description = "Turns slew off" }; } }
        private SimConnectEvent SlewOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewOn, Name = "SLEW_ON", Units = "N/A", Description = "Turns slew on" }; } }
        private SimConnectEvent SlewPitchDnFast { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchDnFast, Name = "SLEW_PITCH_DN_FAST", Units = "N/A", Description = "Slew pitch downward fast" }; } }
        private SimConnectEvent SlewPitchDnSlow { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchDnSlow, Name = "SLEW_PITCH_DN_SLOW", Units = "N/A", Description = "Slew pitch downward slow" }; } }
        private SimConnectEvent SlewPitchFreeze { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchFreeze, Name = "SLEW_PITCH_FREEZE", Units = "N/A", Description = "Stop pitch slew" }; } }
        private SimConnectEvent SlewPitchMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchMinus, Name = "SLEW_PITCH_MINUS", Units = "N/A", Description = "Decrease pitch up slew" }; } }
        private SimConnectEvent SlewPitchPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchPlus, Name = "SLEW_PITCH_PLUS", Units = "N/A", Description = "Increase pitch up slew" }; } }
        private SimConnectEvent SlewPitchUpFast { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchUpFast, Name = "SLEW_PITCH_UP_FAST", Units = "N/A", Description = "Slew pitch upward fast" }; } }
        private SimConnectEvent SlewPitchUpSlow { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewPitchUpSlow, Name = "SLEW_PITCH_UP_SLOW", Units = "N/A", Description = "Slew pitch up slow" }; } }
        private SimConnectEvent SlewReset { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewReset, Name = "SLEW_RESET", Units = "N/A", Description = "Stop slew and reset pitch, bank, and heading all to zero." }; } }
        private SimConnectEvent SlewRight { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewRight, Name = "SLEW_RIGHT", Units = "N/A", Description = "Slew to the right" }; } }
        private SimConnectEvent SlewSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewSet, Name = "SLEW_SET", Units = "[0]: Bool", Description = "Sets slew on/off (1,0)" }; } }
        private SimConnectEvent SlewToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.SlewToggle, Name = "SLEW_TOGGLE", Units = "N/A", Description = "Toggles slew on/off" }; } }
        private SimConnectEvent SlingPickupRelease { get { return new SimConnectEvent() { Id = SimConnectEventId.SlingPickupRelease, Name = "SLING_PICKUP_RELEASE", Units = "[0]: Bool", Description = "Toggle between pickup and release mode. Hold mode is automatic and cannot be selected." }; } }
        private SimConnectEvent SmokeOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SmokeOff, Name = "SMOKE_OFF", Units = "N/A", Description = "Turns the smoke system off." }; } }
        private SimConnectEvent SmokeOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SmokeOn, Name = "SMOKE_ON", Units = "N/A", Description = "Turns the smoke system on." }; } }
        private SimConnectEvent SmokeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SmokeSet, Name = "SMOKE_SET", Units = "[0]: TRUE/FALSE to enable/disable the smoke system", Description = "Sets smoke system on/off." }; } }
        private SimConnectEvent SmokeToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.SmokeToggle, Name = "SMOKE_TOGGLE", Units = "N/A", Description = "Toggle smoke system switch." }; } }
        private SimConnectEvent SnapView { get { return new SimConnectEvent() { Id = SimConnectEventId.SnapView, Name = "SNAP_VIEW", Units = "N/A", Description = "Select previous view" }; } }
        private SimConnectEvent SoundOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SoundOff, Name = "SOUND_OFF", Units = "N/A", Description = "Turns sound off" }; } }
        private SimConnectEvent SoundOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SoundOn, Name = "SOUND_ON", Units = "N/A", Description = "Turns sound on" }; } }
        private SimConnectEvent SoundSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SoundSet, Name = "SOUND_SET", Units = "[0]: Bool", Description = "Sets sound on/off (1,0)" }; } }
        private SimConnectEvent SoundToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.SoundToggle, Name = "SOUND_TOGGLE", Units = "[0]: Bool", Description = "Toggles sound on/off" }; } }
        private SimConnectEvent SpMultiplayerScoreDisplay { get { return new SimConnectEvent() { Id = SimConnectEventId.SpMultiplayerScoreDisplay, Name = "SP_MULTIPLAYER_SCORE_DISPLAY", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent SpeedSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SpeedSlotIndexSet, Name = "SPEED_SLOT_INDEX_SET", Units = "N/A", Description = "Set autopilot speed slot index." }; } }
        private SimConnectEvent SpoilersArmOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersArmOff, Name = "SPOILERS_ARM_OFF", Units = "N/A", Description = "Sets auto-spoiler arming off (0)." }; } }
        private SimConnectEvent SpoilersArmOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersArmOn, Name = "SPOILERS_ARM_ON", Units = "N/A", Description = "Sets auto-spoiler arming on (1)." }; } }
        private SimConnectEvent SpoilersArmSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersArmSet, Name = "SPOILERS_ARM_SET", Units = "[0]: Bool", Description = "Sets auto-spoiler arming (0,1)." }; } }
        private SimConnectEvent SpoilersArmToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersArmToggle, Name = "SPOILERS_ARM_TOGGLE", Units = "N/A", Description = "Toggles arming of auto-spoilers between armed (1) and unarmed (0)." }; } }
        private SimConnectEvent SpoilersDec { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersDec, Name = "SPOILERS_DEC", Units = "N/A", Description = "Decremement the spoilers by  (down to a minimum of 0)." }; } }
        private SimConnectEvent SpoilersInc { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersInc, Name = "SPOILERS_INC", Units = "N/A", Description = "Increment the spoilers by  (down to a minimum of 0)." }; } }
        private SimConnectEvent SpoilersOff { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersOff, Name = "SPOILERS_OFF", Units = "N/A", Description = "Sets spoiler handle to full retract position." }; } }
        private SimConnectEvent SpoilersOn { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersOn, Name = "SPOILERS_ON", Units = "N/A", Description = "Sets spoiler handle to full extend position." }; } }
        private SimConnectEvent SpoilersSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersSet, Name = "SPOILERS_SET", Units = "[0]: Position (0 to 16383)", Description = "Sets spoiler handle position." }; } }
        private SimConnectEvent SpoilersToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.SpoilersToggle, Name = "SPOILERS_TOGGLE", Units = "N/A", Description = "Toggles spoiler handle." }; } }
        private SimConnectEvent StarterGen { get { return new SimConnectEvent() { Id = SimConnectEventId.StarterGen, Name = "STARTER_GEN", Units = "[0]: Bool", Description = "Not used in the simulation." }; } }
        private SimConnectEvent StarterOff { get { return new SimConnectEvent() { Id = SimConnectEventId.StarterOff, Name = "STARTER_OFF", Units = "[0]: Bool", Description = "Not used in the simulation." }; } }
        private SimConnectEvent StarterSet { get { return new SimConnectEvent() { Id = SimConnectEventId.StarterSet, Name = "STARTER_SET", Units = "[0]: Bool", Description = "Set the status of the current controlled engine starters to On/Off. Controlled engines are set through the SimVar ENGINE CONTROL SELECT." }; } }
        private SimConnectEvent StarterStart { get { return new SimConnectEvent() { Id = SimConnectEventId.StarterStart, Name = "STARTER_START", Units = "[0]: Bool", Description = "Not used in the simulation." }; } }
        private SimConnectEvent Starter1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Starter1Set, Name = "STARTER1_SET", Units = "[0]: Bool", Description = "Set the Starter for engine 1 to on or off. Note that the starter will only stay on for a short time before switching itself off again on piston engines. If you wish the starter to stay on, use SET_STARTER1_HELD." }; } }
        private SimConnectEvent Starter2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Starter2Set, Name = "STARTER2_SET", Units = "[0]: Bool", Description = "Set the Starter for engine 1 to on or off. Note that the starter will only stay on for a short time before switching itself off again on piston engines. If you wish the starter to stay on, use SET_STARTER2_HELD." }; } }
        private SimConnectEvent Starter3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Starter3Set, Name = "STARTER3_SET", Units = "[0]: Bool", Description = "Set the Starter for engine 1 to on or off. Note that the starter will only stay on for a short time before switching itself off again on piston engines. If you wish the starter to stay on, use SET_STARTER3_HELD." }; } }
        private SimConnectEvent Starter4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Starter4Set, Name = "STARTER4_SET", Units = "[0]: Bool", Description = "Set the Starter for engine 1 to on or off. Note that the starter will only stay on for a short time before switching itself off again on piston engines. If you wish the starter to stay on, use SET_STARTER4_HELD." }; } }
        private SimConnectEvent SteeringDec { get { return new SimConnectEvent() { Id = SimConnectEventId.SteeringDec, Name = "STEERING_DEC", Units = "N/A", Description = "Decrements the nose wheel steering position by 5 percent." }; } }
        private SimConnectEvent SteeringInc { get { return new SimConnectEvent() { Id = SimConnectEventId.SteeringInc, Name = "STEERING_INC", Units = "N/A", Description = "Increments the nose wheel steering position by 5 percent." }; } }
        private SimConnectEvent SteeringSet { get { return new SimConnectEvent() { Id = SimConnectEventId.SteeringSet, Name = "STEERING_SET", Units = "[0]: Steering position (+/-16383)", Description = "Sets the value of the nose wheel steering position. Zero is straight ahead (-16383, far left +16383, far right)." }; } }
        private SimConnectEvent StopAllGuns { get { return new SimConnectEvent() { Id = SimConnectEventId.StopAllGuns, Name = "STOP_ALL_GUNS", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent StopPrimaryGuns { get { return new SimConnectEvent() { Id = SimConnectEventId.StopPrimaryGuns, Name = "STOP_PRIMARY_GUNS", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent StopSecondaryGuns { get { return new SimConnectEvent() { Id = SimConnectEventId.StopSecondaryGuns, Name = "STOP_SECONDARY_GUNS", Units = "N/A", Description = "Not used in the simulation." }; } }
        private SimConnectEvent StrobesOff { get { return new SimConnectEvent() { Id = SimConnectEventId.StrobesOff, Name = "STROBES_OFF", Units = "[0]: light circuit index", Description = "Turn strobe light off" }; } }
        private SimConnectEvent StrobesOn { get { return new SimConnectEvent() { Id = SimConnectEventId.StrobesOn, Name = "STROBES_ON", Units = "[0]: light circuit index", Description = "Turn strobe lights on" }; } }
        private SimConnectEvent StrobesSet { get { return new SimConnectEvent() { Id = SimConnectEventId.StrobesSet, Name = "STROBES_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set strobe lights on/off (1,0)" }; } }
        private SimConnectEvent StrobesToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.StrobesToggle, Name = "STROBES_TOGGLE", Units = "[0]: light circuit index", Description = "Toggle strobe lights" }; } }
        private SimConnectEvent SyncFlightDirectorPitch { get { return new SimConnectEvent() { Id = SimConnectEventId.SyncFlightDirectorPitch, Name = "SYNC_FLIGHT_DIRECTOR_PITCH", Units = "N/A", Description = "Synchronizes flight director pitch with current aircraft pitch" }; } }
        private SimConnectEvent Tacan1ActiveChannelSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1ActiveChannelSet, Name = "TACAN1_ACTIVE_CHANNEL_SET", Units = "[0]: Channel value (1 - 127)", Description = "Set TACAN 1/2 active channel, from 1 to 127." }; } }
        private SimConnectEvent Tacan1ActiveModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1ActiveModeSet, Name = "TACAN1_ACTIVE_MODE_SET", Units = "[0]: Active mode value (0, 1)", Description = "Set the TACAN 1/2 active mode, either 0 (X) or 1 (Y)." }; } }
        private SimConnectEvent Tacan1ObiDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1ObiDec, Name = "TACAN1_OBI_DEC", Units = "N/A", Description = "Decrease TACAN 1/2 OBI by 1 degree. OBI bearing is between 0° and 359°, and and will loop back to 359º if you go below 0º." }; } }
        private SimConnectEvent Tacan1ObiFastDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1ObiFastDec, Name = "TACAN1_OBI_FAST_DEC", Units = "N/A", Description = "Decrease TACAN 1/2 OBI by 10 degrees. OBI bearing is between 0° and 359°, and and will loop back to 359º if you go below 0º." }; } }
        private SimConnectEvent Tacan1ObiFastInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1ObiFastInc, Name = "TACAN1_OBI_FAST_INC", Units = "N/A", Description = "Increase TACAN 1/2 OBI by 10 degrees. OBI bearing is between 0° and 359°, and and will loop back to 0º if you go above 359º." }; } }
        private SimConnectEvent Tacan1ObiInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1ObiInc, Name = "TACAN1_OBI_INC", Units = "N/A", Description = "Increase TACAN 1/2 OBI by 1 degree. OBI bearing is between 0° and 359°, and and will loop back to 0º if you go above 359º." }; } }
        private SimConnectEvent Tacan1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1Set, Name = "TACAN1_SET", Units = "[0]: Bearing indicator value", Description = "Set TACAN 1/2 Omni bearing indicator. The behavior is similar to the OBS knob on a traditional VOR." }; } }
        private SimConnectEvent Tacan1StandbyChannelSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1StandbyChannelSet, Name = "TACAN1_STANDBY_CHANNEL_SET", Units = "[0]: Channel value (1 - 127)", Description = "Set TACAN 1/2 standby channel, from 1 to 127" }; } }
        private SimConnectEvent Tacan1StandbyModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1StandbyModeSet, Name = "TACAN1_STANDBY_MODE_SET", Units = "[0]: Standby mode value (0, 1)", Description = "Set the TACAN 1/2 standby mode, either 0 (X) or 1 (Y)." }; } }
        private SimConnectEvent Tacan1Swap { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1Swap, Name = "TACAN1_SWAP", Units = "N/A", Description = "Swap between active and standby TACAN 1/2 frequencies." }; } }
        private SimConnectEvent Tacan1VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1VolumeDec, Name = "TACAN1_VOLUME_DEC", Units = "N/A", Description = "Decrease TACAN 1/2 volume by 1, down to a minimum volume of 0." }; } }
        private SimConnectEvent Tacan1VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1VolumeInc, Name = "TACAN1_VOLUME_INC", Units = "N/A", Description = "Increase TACAN 1/2 volume by 1, up to a maximum volume of 100." }; } }
        private SimConnectEvent Tacan1VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan1VolumeSet, Name = "TACAN1_VOLUME_SET", Units = "[0]: Volume value (0, 100)", Description = "Set TACAN 1/2 volume to a value from 0 (no volume) to 100 (full volume)." }; } }
        private SimConnectEvent Tacan2ActiveChannelSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2ActiveChannelSet, Name = "TACAN2_ACTIVE_CHANNEL_SET", Units = "[0]: Channel value (1 - 127)", Description = "Set TACAN 1/2 active channel, from 1 to 127." }; } }
        private SimConnectEvent Tacan2ActiveModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2ActiveModeSet, Name = "TACAN2_ACTIVE_MODE_SET", Units = "[0]: Active mode value (0, 1)", Description = "Set the TACAN 1/2 active mode, either 0 (X) or 1 (Y)." }; } }
        private SimConnectEvent Tacan2ObiDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2ObiDec, Name = "TACAN2_OBI_DEC", Units = "N/A", Description = "Decrease TACAN 1/2 OBI by 1 degree. OBI bearing is between 0° and 359°, and and will loop back to 359º if you go below 0º." }; } }
        private SimConnectEvent Tacan2ObiFastDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2ObiFastDec, Name = "TACAN2_OBI_FAST_DEC", Units = "N/A", Description = "Decrease TACAN 1/2 OBI by 10 degrees. OBI bearing is between 0° and 359°, and and will loop back to 359º if you go below 0º." }; } }
        private SimConnectEvent Tacan2ObiFastInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2ObiFastInc, Name = "TACAN2_OBI_FAST_INC", Units = "N/A", Description = "Increase TACAN 1/2 OBI by 10 degrees. OBI bearing is between 0° and 359°, and and will loop back to 0º if you go above 359º." }; } }
        private SimConnectEvent Tacan2ObiInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2ObiInc, Name = "TACAN2_OBI_INC", Units = "N/A", Description = "Increase TACAN 1/2 OBI by 1 degree. OBI bearing is between 0° and 359°, and and will loop back to 0º if you go above 359º." }; } }
        private SimConnectEvent Tacan2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2Set, Name = "TACAN2_SET", Units = "[0]: Bearing indicator value", Description = "Set TACAN 1/2 Omni bearing indicator. The behavior is similar to the OBS knob on a traditional VOR." }; } }
        private SimConnectEvent Tacan2StandbyChannelSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2StandbyChannelSet, Name = "TACAN2_STANDBY_CHANNEL_SET", Units = "[0]: Channel value (1 - 127)", Description = "Set TACAN 1/2 standby channel, from 1 to 127" }; } }
        private SimConnectEvent Tacan2StandbyModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2StandbyModeSet, Name = "TACAN2_STANDBY_MODE_SET", Units = "[0]: Standby mode value (0, 1)", Description = "Set the TACAN 1/2 standby mode, either 0 (X) or 1 (Y)." }; } }
        private SimConnectEvent Tacan2Swap { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2Swap, Name = "TACAN2_SWAP", Units = "N/A", Description = "Swap between active and standby TACAN 1/2 frequencies." }; } }
        private SimConnectEvent Tacan2VolumeDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2VolumeDec, Name = "TACAN2_VOLUME_DEC", Units = "N/A", Description = "Decrease TACAN 1/2 volume by 1, down to a minimum volume of 0." }; } }
        private SimConnectEvent Tacan2VolumeInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2VolumeInc, Name = "TACAN2_VOLUME_INC", Units = "N/A", Description = "Increase TACAN 1/2 volume by 1, up to a maximum volume of 100." }; } }
        private SimConnectEvent Tacan2VolumeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.Tacan2VolumeSet, Name = "TACAN2_VOLUME_SET", Units = "[0]: Volume value (0, 100)", Description = "Set TACAN 1/2 volume to a value from 0 (no volume) to 100 (full volume)." }; } }
        private SimConnectEvent TailRotorDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.TailRotorDecr, Name = "TAIL_ROTOR_DECR", Units = "N/A", Description = "Decrements the tail rotor by 0.1." }; } }
        private SimConnectEvent TailRotorIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.TailRotorIncr, Name = "TAIL_ROTOR_INCR", Units = "N/A", Description = "Increments the tail rotor by 0.1." }; } }
        private SimConnectEvent TakeoffAssistArmSet { get { return new SimConnectEvent() { Id = SimConnectEventId.TakeoffAssistArmSet, Name = "TAKEOFF_ASSIST_ARM_SET", Units = "[0]: Bool", Description = "Used to set or unset the launch assist arm." }; } }
        private SimConnectEvent TakeoffAssistArmToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.TakeoffAssistArmToggle, Name = "TAKEOFF_ASSIST_ARM_TOGGLE", Units = "N/A", Description = "Deploy or remove the launch assist arm." }; } }
        private SimConnectEvent TakeoffAssistFire { get { return new SimConnectEvent() { Id = SimConnectEventId.TakeoffAssistFire, Name = "TAKEOFF_ASSIST_FIRE", Units = "N/A", Description = "If everything is set up correctly. Launch from the catapult." }; } }
        private SimConnectEvent TaxiLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.TaxiLightsOff, Name = "TAXI_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn taxi light off" }; } }
        private SimConnectEvent TaxiLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.TaxiLightsOn, Name = "TAXI_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn taxi lights on" }; } }
        private SimConnectEvent TaxiLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.TaxiLightsSet, Name = "TAXI_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set taxi lights on/off (1,0)" }; } }
        private SimConnectEvent TextScrollSet { get { return new SimConnectEvent() { Id = SimConnectEventId.TextScrollSet, Name = "TEXT_SCROLL_SET", Units = "[0]: Bool", Description = "Toggles sound on/off" }; } }
        private SimConnectEvent Throttle10 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle10, Name = "THROTTLE_10", Units = "N/A", Description = "Set throttles to 10%" }; } }
        private SimConnectEvent Throttle20 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle20, Name = "THROTTLE_20", Units = "N/A", Description = "Set throttles to 20%" }; } }
        private SimConnectEvent Throttle30 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle30, Name = "THROTTLE_30", Units = "N/A", Description = "Set throttles to 30%" }; } }
        private SimConnectEvent Throttle40 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle40, Name = "THROTTLE_40", Units = "N/A", Description = "Set throttles to 40%" }; } }
        private SimConnectEvent Throttle50 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle50, Name = "THROTTLE_50", Units = "N/A", Description = "Set throttles to 50%" }; } }
        private SimConnectEvent Throttle60 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle60, Name = "THROTTLE_60", Units = "N/A", Description = "Set throttles to 60%" }; } }
        private SimConnectEvent Throttle70 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle70, Name = "THROTTLE_70", Units = "N/A", Description = "Set throttles to 70%" }; } }
        private SimConnectEvent Throttle80 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle80, Name = "THROTTLE_80", Units = "N/A", Description = "Set throttles to 80%" }; } }
        private SimConnectEvent Throttle90 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle90, Name = "THROTTLE_90", Units = "N/A", Description = "Set throttles to 90%" }; } }
        private SimConnectEvent ThrottleAxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleAxisSetEx1, Name = "THROTTLE_AXIS_SET_EX1", Units = "N/A", Description = "Set throttles to 90%" }; } }
        private SimConnectEvent ThrottleCut { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleCut, Name = "THROTTLE_CUT", Units = "N/A", Description = "Set throttles to idle" }; } }
        private SimConnectEvent ThrottleCutEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleCutEx1, Name = "THROTTLE_CUT_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent ThrottleDecr { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleDecr, Name = "THROTTLE_DECR", Units = "N/A", Description = "Decrease all throttles by 10%." }; } }
        private SimConnectEvent ThrottleDecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleDecrSmall, Name = "THROTTLE_DECR_SMALL", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent ThrottleDecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleDecreaseEx1, Name = "THROTTLE_DECREASE_EX1", Units = "N/A", Description = "Decrement throttles" }; } }
        private SimConnectEvent ThrottleDecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleDecreaseSmallEx1, Name = "THROTTLE_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease throttles small" }; } }
        private SimConnectEvent ThrottleFull { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleFull, Name = "THROTTLE_FULL", Units = "N/A", Description = "Set throttles max" }; } }
        private SimConnectEvent ThrottleFullEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleFullEx1, Name = "THROTTLE_FULL_EX1", Units = "N/A", Description = "Set throttles max" }; } }
        private SimConnectEvent ThrottleIncr { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleIncr, Name = "THROTTLE_INCR", Units = "N/A", Description = "Increase all throttles by 10%." }; } }
        private SimConnectEvent ThrottleIncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleIncreaseEx1, Name = "THROTTLE_INCREASE_EX1", Units = "N/A", Description = "Increment throttles" }; } }
        private SimConnectEvent ThrottleIncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleIncreaseSmallEx1, Name = "THROTTLE_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment throttles small" }; } }
        private SimConnectEvent ThrottleReverseThrustHold { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleReverseThrustHold, Name = "THROTTLE_REVERSE_THRUST_HOLD", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent ThrottleReverseThrustToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleReverseThrustToggle, Name = "THROTTLE_REVERSE_THRUST_TOGGLE", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent ThrottleSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ThrottleSet, Name = "THROTTLE_SET", Units = "N/A", Description = "Set throttles exactly (0- 16383)" }; } }
        private SimConnectEvent Throttle1AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1AxisSetEx1, Name = "THROTTLE1_AXIS_SET_EX1", Units = "N/A", Description = "Set throttles to 90%" }; } }
        private SimConnectEvent Throttle1Cut { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1Cut, Name = "THROTTLE1_CUT", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle1CutEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1CutEx1, Name = "THROTTLE1_CUT_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle1Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1Decr, Name = "THROTTLE1_DECR", Units = "N/A", Description = "Decrement throttle 1/2/3/4 by 10%." }; } }
        private SimConnectEvent Throttle1DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1DecrSmall, Name = "THROTTLE1_DECR_SMALL", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle1DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1DecreaseEx1, Name = "THROTTLE1_DECREASE_EX1", Units = "N/A", Description = "Decrement throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle1DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1DecreaseSmallEx1, Name = "THROTTLE1_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle1Full { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1Full, Name = "THROTTLE1_FULL", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle1FullEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1FullEx1, Name = "THROTTLE1_FULL_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle1Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1Incr, Name = "THROTTLE1_INCR", Units = "N/A", Description = "Increment throttles" }; } }
        private SimConnectEvent Throttle1IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1IncrSmall, Name = "THROTTLE1_INCR_SMALL", Units = "N/A", Description = "Increment throttles small" }; } }
        private SimConnectEvent Throttle1IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1IncreaseEx1, Name = "THROTTLE1_INCREASE_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle1IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1IncreaseSmallEx1, Name = "THROTTLE1_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle1ReverseThrustHold { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1ReverseThrustHold, Name = "THROTTLE1_REVERSE_THRUST_HOLD", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle1Set, Name = "THROTTLE1_SET", Units = "N/A", Description = "Set throttle 1/2/3/4 exactly (0 to 16383)" }; } }
        private SimConnectEvent Throttle2AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2AxisSetEx1, Name = "THROTTLE2_AXIS_SET_EX1", Units = "N/A", Description = "Set throttles to 90%" }; } }
        private SimConnectEvent Throttle2Cut { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2Cut, Name = "THROTTLE2_CUT", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle2CutEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2CutEx1, Name = "THROTTLE2_CUT_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle2Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2Decr, Name = "THROTTLE2_DECR", Units = "N/A", Description = "Decrement throttle 1/2/3/4 by 10%." }; } }
        private SimConnectEvent Throttle2DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2DecrSmall, Name = "THROTTLE2_DECR_SMALL", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle2DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2DecreaseEx1, Name = "THROTTLE2_DECREASE_EX1", Units = "N/A", Description = "Decrement throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle2DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2DecreaseSmallEx1, Name = "THROTTLE2_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle2Full { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2Full, Name = "THROTTLE2_FULL", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle2FullEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2FullEx1, Name = "THROTTLE2_FULL_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle2Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2Incr, Name = "THROTTLE2_INCR", Units = "N/A", Description = "Increment throttles" }; } }
        private SimConnectEvent Throttle2IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2IncrSmall, Name = "THROTTLE2_INCR_SMALL", Units = "N/A", Description = "Increment throttles small" }; } }
        private SimConnectEvent Throttle2IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2IncreaseEx1, Name = "THROTTLE2_INCREASE_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle2IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2IncreaseSmallEx1, Name = "THROTTLE2_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle2ReverseThrustHold { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2ReverseThrustHold, Name = "THROTTLE2_REVERSE_THRUST_HOLD", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle2Set, Name = "THROTTLE2_SET", Units = "N/A", Description = "Set throttle 1/2/3/4 exactly (0 to 16383)" }; } }
        private SimConnectEvent Throttle3AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3AxisSetEx1, Name = "THROTTLE3_AXIS_SET_EX1", Units = "N/A", Description = "Set throttles to 90%" }; } }
        private SimConnectEvent Throttle3Cut { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3Cut, Name = "THROTTLE3_CUT", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle3CutEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3CutEx1, Name = "THROTTLE3_CUT_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle3Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3Decr, Name = "THROTTLE3_DECR", Units = "N/A", Description = "Decrement throttle 1/2/3/4 by 10%." }; } }
        private SimConnectEvent Throttle3DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3DecrSmall, Name = "THROTTLE3_DECR_SMALL", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle3DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3DecreaseEx1, Name = "THROTTLE3_DECREASE_EX1", Units = "N/A", Description = "Decrement throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle3DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3DecreaseSmallEx1, Name = "THROTTLE3_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle3Full { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3Full, Name = "THROTTLE3_FULL", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle3FullEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3FullEx1, Name = "THROTTLE3_FULL_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle3Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3Incr, Name = "THROTTLE3_INCR", Units = "N/A", Description = "Increment throttles" }; } }
        private SimConnectEvent Throttle3IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3IncrSmall, Name = "THROTTLE3_INCR_SMALL", Units = "N/A", Description = "Increment throttles small" }; } }
        private SimConnectEvent Throttle3IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3IncreaseEx1, Name = "THROTTLE3_INCREASE_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle3IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3IncreaseSmallEx1, Name = "THROTTLE3_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle3ReverseThrustHold { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3ReverseThrustHold, Name = "THROTTLE3_REVERSE_THRUST_HOLD", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle3Set, Name = "THROTTLE3_SET", Units = "N/A", Description = "Set throttle 1/2/3/4 exactly (0 to 16383)" }; } }
        private SimConnectEvent Throttle4AxisSetEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4AxisSetEx1, Name = "THROTTLE4_AXIS_SET_EX1", Units = "N/A", Description = "Set throttles to 90%" }; } }
        private SimConnectEvent Throttle4Cut { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4Cut, Name = "THROTTLE4_CUT", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle4CutEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4CutEx1, Name = "THROTTLE4_CUT_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle4Decr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4Decr, Name = "THROTTLE4_DECR", Units = "N/A", Description = "Decrement throttle 1/2/3/4 by 10%." }; } }
        private SimConnectEvent Throttle4DecrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4DecrSmall, Name = "THROTTLE4_DECR_SMALL", Units = "N/A", Description = "Set throttle 1/2/3/4 to idle" }; } }
        private SimConnectEvent Throttle4DecreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4DecreaseEx1, Name = "THROTTLE4_DECREASE_EX1", Units = "N/A", Description = "Decrement throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle4DecreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4DecreaseSmallEx1, Name = "THROTTLE4_DECREASE_SMALL_EX1", Units = "N/A", Description = "Decrease throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle4Full { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4Full, Name = "THROTTLE4_FULL", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle4FullEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4FullEx1, Name = "THROTTLE4_FULL_EX1", Units = "N/A", Description = "Set throttle 1/2/3/4 max" }; } }
        private SimConnectEvent Throttle4Incr { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4Incr, Name = "THROTTLE4_INCR", Units = "N/A", Description = "Increment throttles" }; } }
        private SimConnectEvent Throttle4IncrSmall { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4IncrSmall, Name = "THROTTLE4_INCR_SMALL", Units = "N/A", Description = "Increment throttles small" }; } }
        private SimConnectEvent Throttle4IncreaseEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4IncreaseEx1, Name = "THROTTLE4_INCREASE_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4" }; } }
        private SimConnectEvent Throttle4IncreaseSmallEx1 { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4IncreaseSmallEx1, Name = "THROTTLE4_INCREASE_SMALL_EX1", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle4ReverseThrustHold { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4ReverseThrustHold, Name = "THROTTLE4_REVERSE_THRUST_HOLD", Units = "N/A", Description = "Increment throttle 1/2/3/4 small" }; } }
        private SimConnectEvent Throttle4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Throttle4Set, Name = "THROTTLE4_SET", Units = "N/A", Description = "Set throttle 1/2/3/4 exactly (0 to 16383)" }; } }
        private SimConnectEvent ToggleAfterburner { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAfterburner, Name = "TOGGLE_AFTERBURNER", Units = "N/A", Description = "Toggles afterburners" }; } }
        private SimConnectEvent ToggleAfterburner1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAfterburner1, Name = "TOGGLE_AFTERBURNER1", Units = "N/A", Description = "Toggles engine 1/2/3/4 afterburner" }; } }
        private SimConnectEvent ToggleAfterburner2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAfterburner2, Name = "TOGGLE_AFTERBURNER2", Units = "N/A", Description = "Toggles engine 1/2/3/4 afterburner" }; } }
        private SimConnectEvent ToggleAfterburner3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAfterburner3, Name = "TOGGLE_AFTERBURNER3", Units = "N/A", Description = "Toggles engine 1/2/3/4 afterburner" }; } }
        private SimConnectEvent ToggleAfterburner4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAfterburner4, Name = "TOGGLE_AFTERBURNER4", Units = "N/A", Description = "Toggles engine 1/2/3/4 afterburner" }; } }
        private SimConnectEvent ToggleAircraftExit { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAircraftExit, Name = "TOGGLE_AIRCRAFT_EXIT", Units = "N/A", Description = "Toggles primary door open/close. Usually followed by (for example) KEY_SELECT_2, etc... for subsequent doors." }; } }
        private SimConnectEvent ToggleAircraftExitFast { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAircraftExitFast, Name = "TOGGLE_AIRCRAFT_EXIT_FAST", Units = "N/A", Description = "Toggles primary door open/close. Usually followed by (for example) KEY_SELECT_2, etc... for subsequent doors." }; } }
        private SimConnectEvent ToggleAircraftLabels { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAircraftLabels, Name = "TOGGLE_AIRCRAFT_LABELS", Units = "[0]: Bool", Description = "Toggles aircraft labels" }; } }
        private SimConnectEvent ToggleAirportNameDisplay { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAirportNameDisplay, Name = "TOGGLE_AIRPORT_NAME_DISPLAY", Units = "[0]: Bool", Description = "Turn on or off the airport name." }; } }
        private SimConnectEvent ToggleAllStarters { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAllStarters, Name = "TOGGLE_ALL_STARTERS", Units = "N/A", Description = "Toggle starters" }; } }
        private SimConnectEvent ToggleAlternateStatic { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAlternateStatic, Name = "TOGGLE_ALTERNATE_STATIC", Units = "N/A", Description = "Toggles alternate static pressure port." }; } }
        private SimConnectEvent ToggleAlternator1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAlternator1, Name = "TOGGLE_ALTERNATOR1", Units = "N/A", Description = "Toggles alternator/generator 1/2/3/4 switch." }; } }
        private SimConnectEvent ToggleAlternator2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAlternator2, Name = "TOGGLE_ALTERNATOR2", Units = "N/A", Description = "Toggles alternator/generator 1/2/3/4 switch." }; } }
        private SimConnectEvent ToggleAlternator3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAlternator3, Name = "TOGGLE_ALTERNATOR3", Units = "N/A", Description = "Toggles alternator/generator 1/2/3/4 switch." }; } }
        private SimConnectEvent ToggleAlternator4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAlternator4, Name = "TOGGLE_ALTERNATOR4", Units = "N/A", Description = "Toggles alternator/generator 1/2/3/4 switch." }; } }
        private SimConnectEvent ToggleAutofeatherArm { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAutofeatherArm, Name = "TOGGLE_AUTOFEATHER_ARM", Units = "N/A", Description = "Turns auto-feather arming switch on." }; } }
        private SimConnectEvent ToggleAvionicsMaster { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleAvionicsMaster, Name = "TOGGLE_AVIONICS_MASTER", Units = "N/A", Description = "Toggles the avionics master switch" }; } }
        private SimConnectEvent ToggleBeaconLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleBeaconLights, Name = "TOGGLE_BEACON_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle beacon lights" }; } }
        private SimConnectEvent ToggleCabinLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleCabinLights, Name = "TOGGLE_CABIN_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle cockpit/cabin lights" }; } }
        private SimConnectEvent ToggleDamageText { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleDamageText, Name = "TOGGLE_DAMAGE_TEXT", Units = "[0]: Bool", Description = "Turn on or off the airport name." }; } }
        private SimConnectEvent ToggleDme { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleDme, Name = "TOGGLE_DME", Units = "N/A", Description = "Toggles DME between NAV 1 and NAV 2." }; } }
        private SimConnectEvent ToggleElectFuelPump { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectFuelPump, Name = "TOGGLE_ELECT_FUEL_PUMP", Units = "N/A", Description = "Toggle electric fuel pumps" }; } }
        private SimConnectEvent ToggleElectFuelPump1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectFuelPump1, Name = "TOGGLE_ELECT_FUEL_PUMP1", Units = "N/A", Description = "Toggle engine 1/2/3/4 electric fuel pump" }; } }
        private SimConnectEvent ToggleElectFuelPump2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectFuelPump2, Name = "TOGGLE_ELECT_FUEL_PUMP2", Units = "N/A", Description = "Toggle engine 1/2/3/4 electric fuel pump" }; } }
        private SimConnectEvent ToggleElectFuelPump3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectFuelPump3, Name = "TOGGLE_ELECT_FUEL_PUMP3", Units = "N/A", Description = "Toggle engine 1/2/3/4 electric fuel pump" }; } }
        private SimConnectEvent ToggleElectFuelPump4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectFuelPump4, Name = "TOGGLE_ELECT_FUEL_PUMP4", Units = "N/A", Description = "Toggle engine 1/2/3/4 electric fuel pump" }; } }
        private SimConnectEvent ToggleElectricVacuumPump { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectricVacuumPump, Name = "TOGGLE_ELECTRIC_VACUUM_PUMP", Units = "N/A", Description = "Toggles backup electric vacuum pump" }; } }
        private SimConnectEvent ToggleElectricalFailure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleElectricalFailure, Name = "TOGGLE_ELECTRICAL_FAILURE", Units = "N/A", Description = "Toggle electrical system failure" }; } }
        private SimConnectEvent ToggleEnemyIndicator { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleEnemyIndicator, Name = "TOGGLE_ENEMY_INDICATOR", Units = "[0]: Bool", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ToggleEngine1Failure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleEngine1Failure, Name = "TOGGLE_ENGINE1_FAILURE", Units = "N/A", Description = "Toggle engine 1/2/3/4 failure" }; } }
        private SimConnectEvent ToggleEngine2Failure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleEngine2Failure, Name = "TOGGLE_ENGINE2_FAILURE", Units = "N/A", Description = "Toggle engine 1/2/3/4 failure" }; } }
        private SimConnectEvent ToggleEngine3Failure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleEngine3Failure, Name = "TOGGLE_ENGINE3_FAILURE", Units = "N/A", Description = "Toggle engine 1/2/3/4 failure" }; } }
        private SimConnectEvent ToggleEngine4Failure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleEngine4Failure, Name = "TOGGLE_ENGINE4_FAILURE", Units = "N/A", Description = "Toggle engine 1/2/3/4 failure" }; } }
        private SimConnectEvent ToggleExternalPower { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleExternalPower, Name = "TOGGLE_EXTERNAL_POWER", Units = "[0]: external power index", Description = "Toggle external power switch state. The index is the value assigned to the circuit N when the externalpower.N was defined." }; } }
        private SimConnectEvent ToggleFeatherSwitch1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFeatherSwitch1, Name = "TOGGLE_FEATHER_SWITCH_1", Units = "N/A", Description = "Trigger propeller 1/2/3/4 switch" }; } }
        private SimConnectEvent ToggleFeatherSwitch2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFeatherSwitch2, Name = "TOGGLE_FEATHER_SWITCH_2", Units = "N/A", Description = "Trigger propeller 1/2/3/4 switch" }; } }
        private SimConnectEvent ToggleFeatherSwitch3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFeatherSwitch3, Name = "TOGGLE_FEATHER_SWITCH_3", Units = "N/A", Description = "Trigger propeller 1/2/3/4 switch" }; } }
        private SimConnectEvent ToggleFeatherSwitch4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFeatherSwitch4, Name = "TOGGLE_FEATHER_SWITCH_4", Units = "N/A", Description = "Trigger propeller 1/2/3/4 switch" }; } }
        private SimConnectEvent ToggleFeatherSwitches { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFeatherSwitches, Name = "TOGGLE_FEATHER_SWITCHES", Units = "N/A", Description = "Trigger propeller switches" }; } }
        private SimConnectEvent ToggleFlightDirector { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFlightDirector, Name = "TOGGLE_FLIGHT_DIRECTOR", Units = "N/A", Description = "Toggles flight director on/off" }; } }
        private SimConnectEvent ToggleFuelValveAll { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFuelValveAll, Name = "TOGGLE_FUEL_VALVE_ALL", Units = "N/A", Description = "Toggle engine fuel valves" }; } }
        private SimConnectEvent ToggleFuelValveEng1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFuelValveEng1, Name = "TOGGLE_FUEL_VALVE_ENG1", Units = "N/A", Description = "Toggle engine 1/2/3/4 fuel valve" }; } }
        private SimConnectEvent ToggleFuelValveEng2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFuelValveEng2, Name = "TOGGLE_FUEL_VALVE_ENG2", Units = "N/A", Description = "Toggle engine 1/2/3/4 fuel valve" }; } }
        private SimConnectEvent ToggleFuelValveEng3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFuelValveEng3, Name = "TOGGLE_FUEL_VALVE_ENG3", Units = "N/A", Description = "Toggle engine 1/2/3/4 fuel valve" }; } }
        private SimConnectEvent ToggleFuelValveEng4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleFuelValveEng4, Name = "TOGGLE_FUEL_VALVE_ENG4", Units = "N/A", Description = "Toggle engine 1/2/3/4 fuel valve" }; } }
        private SimConnectEvent ToggleGpsDrivesNav1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleGpsDrivesNav1, Name = "TOGGLE_GPS_DRIVES_NAV1", Units = "[0]: Value in degrees", Description = "Toggles between GPS and NAV 1 driving NAV 1 OBS display (and AP)" }; } }
        private SimConnectEvent ToggleHydraulicFailure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleHydraulicFailure, Name = "TOGGLE_HYDRAULIC_FAILURE", Units = "N/A", Description = "Toggles hydraulic system failure" }; } }
        private SimConnectEvent ToggleIcs { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleIcs, Name = "TOGGLE_ICS", Units = "N/A", Description = "Toggle the InterCom system on the audio panel." }; } }
        private SimConnectEvent ToggleJetway { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleJetway, Name = "TOGGLE_JETWAY", Units = "N/A", Description = "Requests a jetway, which will only be answered if the aircraft is at a parking spot, or sends already requested jetway away." }; } }
        private SimConnectEvent ToggleLaunchBarSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleLaunchBarSwitch, Name = "TOGGLE_LAUNCH_BAR_SWITCH", Units = "N/A", Description = "Toggle the request for the launch bar to be installed or removed." }; } }
        private SimConnectEvent ToggleLeftBrakeFailure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleLeftBrakeFailure, Name = "TOGGLE_LEFT_BRAKE_FAILURE", Units = "N/A", Description = "Toggles left brake failure" }; } }
        private SimConnectEvent ToggleLogoLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleLogoLights, Name = "TOGGLE_LOGO_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle logo lights" }; } }
        private SimConnectEvent ToggleMasterAlternator { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleMasterAlternator, Name = "TOGGLE_MASTER_ALTERNATOR", Units = "[0]: alternator index", Description = "Toggles main alternator/generator switch. The alternator index is the N index of the alternator.N definition." }; } }
        private SimConnectEvent ToggleMasterBattery { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleMasterBattery, Name = "TOGGLE_MASTER_BATTERY", Units = "[0]: battery index", Description = "Toggle battery switch state. The battery index is the N index of the battery.N definition." }; } }
        private SimConnectEvent ToggleMasterBatteryAlternator { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleMasterBatteryAlternator, Name = "TOGGLE_MASTER_BATTERY_ALTERNATOR", Units = "[0]: battery index", Description = "Toggles master battery and alternator switch" }; } }
        private SimConnectEvent ToggleMasterIgnitionSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleMasterIgnitionSwitch, Name = "TOGGLE_MASTER_IGNITION_SWITCH", Units = "N/A", Description = "Toggles master ignition switch" }; } }
        private SimConnectEvent ToggleMasterStarterSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleMasterStarterSwitch, Name = "TOGGLE_MASTER_STARTER_SWITCH", Units = "N/A", Description = "Toggle starters" }; } }
        private SimConnectEvent ToggleNavLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleNavLights, Name = "TOGGLE_NAV_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle navigation lights" }; } }
        private SimConnectEvent TogglePadlock { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePadlock, Name = "TOGGLE_PADLOCK", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent TogglePitotBlockage { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePitotBlockage, Name = "TOGGLE_PITOT_BLOCKAGE", Units = "N/A", Description = "Toggles blocked pitot tube" }; } }
        private SimConnectEvent TogglePrimer { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePrimer, Name = "TOGGLE_PRIMER", Units = "N/A", Description = "Trigger engine primers" }; } }
        private SimConnectEvent TogglePrimer1 { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePrimer1, Name = "TOGGLE_PRIMER1", Units = "N/A", Description = "Trigger engine 1/2/3/4 primer" }; } }
        private SimConnectEvent TogglePrimer2 { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePrimer2, Name = "TOGGLE_PRIMER2", Units = "N/A", Description = "Trigger engine 1/2/3/4 primer" }; } }
        private SimConnectEvent TogglePrimer3 { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePrimer3, Name = "TOGGLE_PRIMER3", Units = "N/A", Description = "Trigger engine 1/2/3/4 primer" }; } }
        private SimConnectEvent TogglePrimer4 { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePrimer4, Name = "TOGGLE_PRIMER4", Units = "N/A", Description = "Trigger engine 1/2/3/4 primer" }; } }
        private SimConnectEvent TogglePropellerDeice { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePropellerDeice, Name = "TOGGLE_PROPELLER_DEICE", Units = "N/A", Description = "Toggles propeller deice switch" }; } }
        private SimConnectEvent TogglePropellerSync { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePropellerSync, Name = "TOGGLE_PROPELLER_SYNC", Units = "N/A", Description = "Turns propeller synchronization switch on" }; } }
        private SimConnectEvent TogglePushback { get { return new SimConnectEvent() { Id = SimConnectEventId.TogglePushback, Name = "TOGGLE_PUSHBACK", Units = "N/A", Description = "Toggles pushback." }; } }
        private SimConnectEvent ToggleRaceresultsWindow { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRaceresultsWindow, Name = "TOGGLE_RACERESULTS_WINDOW", Units = "N/A", Description = "Show or hide multi-player race results." }; } }
        private SimConnectEvent ToggleRadInsSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRadInsSwitch, Name = "TOGGLE_RAD_INS_SWITCH", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ToggleRadar { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRadar, Name = "TOGGLE_RADAR", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ToggleRadio { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRadio, Name = "TOGGLE_RADIO", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ToggleRamptruck { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRamptruck, Name = "TOGGLE_RAMPTRUCK", Units = "N/A", Description = "Requests a boarding ramp from the nearest airport, or sends an already requested boarding ramp away." }; } }
        private SimConnectEvent ToggleRecognitionLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRecognitionLights, Name = "TOGGLE_RECOGNITION_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle recognition lights" }; } }
        private SimConnectEvent ToggleRightBrakeFailure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleRightBrakeFailure, Name = "TOGGLE_RIGHT_BRAKE_FAILURE", Units = "N/A", Description = "Toggles right brake failure" }; } }
        private SimConnectEvent ToggleSpeaker { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleSpeaker, Name = "TOGGLE_SPEAKER", Units = "N/A", Description = "Toggle the InterCom system on the audio panel." }; } }
        private SimConnectEvent ToggleStarter1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleStarter1, Name = "TOGGLE_STARTER1", Units = "N/A", Description = "Toggle starter 1" }; } }
        private SimConnectEvent ToggleStarter2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleStarter2, Name = "TOGGLE_STARTER2", Units = "N/A", Description = "Toggle starter 2" }; } }
        private SimConnectEvent ToggleStarter3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleStarter3, Name = "TOGGLE_STARTER3", Units = "N/A", Description = "Toggle starter 3" }; } }
        private SimConnectEvent ToggleStarter4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleStarter4, Name = "TOGGLE_STARTER4", Units = "N/A", Description = "Toggle starter 4" }; } }
        private SimConnectEvent ToggleStaticPortBlockage { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleStaticPortBlockage, Name = "TOGGLE_STATIC_PORT_BLOCKAGE", Units = "N/A", Description = "Toggles blocked static port" }; } }
        private SimConnectEvent ToggleStructuralDeice { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleStructuralDeice, Name = "TOGGLE_STRUCTURAL_DEICE", Units = "N/A", Description = "Toggles structural deice switch." }; } }
        private SimConnectEvent ToggleTacanDrivesNav1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleTacanDrivesNav1, Name = "TOGGLE_TACAN_DRIVES_NAV1", Units = "N/A", Description = "Toggles the TACAN DRIVES NAV SimVar to indicate that the NAV1 autopilot feature is driven by Tacan instead of classic Nav systems (VOR/ILS)." }; } }
        private SimConnectEvent ToggleTailHookHandle { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleTailHookHandle, Name = "TOGGLE_TAIL_HOOK_HANDLE", Units = "N/A", Description = "Toggles tail hook." }; } }
        private SimConnectEvent ToggleTailwheelLock { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleTailwheelLock, Name = "TOGGLE_TAILWHEEL_LOCK", Units = "N/A", Description = "Toggles tail wheel lock." }; } }
        private SimConnectEvent ToggleTaxiLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleTaxiLights, Name = "TOGGLE_TAXI_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle taxi lights" }; } }
        private SimConnectEvent ToggleThrottle1ReverseThrust { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleThrottle1ReverseThrust, Name = "TOGGLE_THROTTLE1_REVERSE_THRUST", Units = "N/A", Description = "Toggle on or off the reverse thruster for engine 1/2/3/4" }; } }
        private SimConnectEvent ToggleThrottle2ReverseThrust { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleThrottle2ReverseThrust, Name = "TOGGLE_THROTTLE2_REVERSE_THRUST", Units = "N/A", Description = "Toggle on or off the reverse thruster for engine 1/2/3/4" }; } }
        private SimConnectEvent ToggleThrottle3ReverseThrust { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleThrottle3ReverseThrust, Name = "TOGGLE_THROTTLE3_REVERSE_THRUST", Units = "N/A", Description = "Toggle on or off the reverse thruster for engine 1/2/3/4" }; } }
        private SimConnectEvent ToggleThrottle4ReverseThrust { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleThrottle4ReverseThrust, Name = "TOGGLE_THROTTLE4_REVERSE_THRUST", Units = "N/A", Description = "Toggle on or off the reverse thruster for engine 1/2/3/4" }; } }
        private SimConnectEvent ToggleTotalBrakeFailure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleTotalBrakeFailure, Name = "TOGGLE_TOTAL_BRAKE_FAILURE", Units = "N/A", Description = "Toggles brake failure (both)" }; } }
        private SimConnectEvent ToggleTurnIndicatorSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleTurnIndicatorSwitch, Name = "TOGGLE_TURN_INDICATOR_SWITCH", Units = "N/A", Description = "Turn the turn indicator on or off." }; } }
        private SimConnectEvent ToggleVacuumFailure { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleVacuumFailure, Name = "TOGGLE_VACUUM_FAILURE", Units = "N/A", Description = "Toggle vacuum system failure" }; } }
        private SimConnectEvent ToggleVariometerSwitch { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleVariometerSwitch, Name = "TOGGLE_VARIOMETER_SWITCH", Units = "N/A", Description = "Turn the variometer on or off." }; } }
        private SimConnectEvent ToggleWaterBallastValve { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleWaterBallastValve, Name = "TOGGLE_WATER_BALLAST_VALVE", Units = "[0]: valve index from 1 to n where n is the NumberOfReleaseValves defined in the systems.cfg file.", Description = "Turn the indexed water ballast valve on or off." }; } }
        private SimConnectEvent ToggleWaterRudder { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleWaterRudder, Name = "TOGGLE_WATER_RUDDER", Units = "N/A", Description = "Toggles water rudders." }; } }
        private SimConnectEvent ToggleWingFold { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleWingFold, Name = "TOGGLE_WING_FOLD", Units = "N/A", Description = "Toggles wing folding." }; } }
        private SimConnectEvent ToggleWingLights { get { return new SimConnectEvent() { Id = SimConnectEventId.ToggleWingLights, Name = "TOGGLE_WING_LIGHTS", Units = "[0]: light circuit index", Description = "Toggle wing lights" }; } }
        private SimConnectEvent TooltipUnitsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.TooltipUnitsSet, Name = "TOOLTIP_UNITS_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent TooltipUnitsToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.TooltipUnitsToggle, Name = "TOOLTIP_UNITS_TOGGLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent TowPlaneRelease { get { return new SimConnectEvent() { Id = SimConnectEventId.TowPlaneRelease, Name = "TOW_PLANE_RELEASE", Units = "N/A", Description = "Release a towed aircraft, usually a glider." }; } }
        private SimConnectEvent TowPlaneRequest { get { return new SimConnectEvent() { Id = SimConnectEventId.TowPlaneRequest, Name = "TOW_PLANE_REQUEST", Units = "N/A", Description = "Request a tow plane. The user aircraft must be tow-able, stationary, on the ground and not already attached for this to succeed." }; } }
        private SimConnectEvent TrueAirspeedCalDec { get { return new SimConnectEvent() { Id = SimConnectEventId.TrueAirspeedCalDec, Name = "TRUE_AIRSPEED_CAL_DEC", Units = "N/A", Description = "Decrements airspeed indicators true airspeed reference card." }; } }
        private SimConnectEvent TrueAirspeedCalInc { get { return new SimConnectEvent() { Id = SimConnectEventId.TrueAirspeedCalInc, Name = "TRUE_AIRSPEED_CAL_INC", Units = "N/A", Description = "Increments airspeed indicators true airspeed reference card." }; } }
        private SimConnectEvent TrueAirspeedCalSet { get { return new SimConnectEvent() { Id = SimConnectEventId.TrueAirspeedCalSet, Name = "TRUE_AIRSPEED_CAL_SET", Units = "[0]: Degrees", Description = "Sets airspeed indicators true airspeed reference card (degrees, where 0 is standard sea level conditions)." }; } }
        private SimConnectEvent TugDisable { get { return new SimConnectEvent() { Id = SimConnectEventId.TugDisable, Name = "TUG_DISABLE", Units = "N/A", Description = "Disables tug." }; } }
        private SimConnectEvent TugHeading { get { return new SimConnectEvent() { Id = SimConnectEventId.TugHeading, Name = "TUG_HEADING", Units = "[0]: Heading (0 - 4294967295", Description = "Triggers the tug and sets the desired heading. The units are a 32 bit integer (0 to 4294967295) which represent 0 to 360 degrees. To set a 45 degree angle, for example, set the value to 4294967295 / 8." }; } }
        private SimConnectEvent TugSpeed { get { return new SimConnectEvent() { Id = SimConnectEventId.TugSpeed, Name = "TUG_SPEED", Units = "[0]: Speed (ft / s)", Description = "Triggers tug, and sets desired speed, in feet per second. The speed can be either positive (forward movement) or negative (backward movement)." }; } }
        private SimConnectEvent TurbineIgnitionSwitchSet { get { return new SimConnectEvent() { Id = SimConnectEventId.TurbineIgnitionSwitchSet, Name = "TURBINE_IGNITION_SWITCH_SET", Units = "", Description = "IMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent TurbineIgnitionSwitchSet1 { get { return new SimConnectEvent() { Id = SimConnectEventId.TurbineIgnitionSwitchSet1, Name = "TURBINE_IGNITION_SWITCH_SET1", Units = "", Description = "IMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent TurbineIgnitionSwitchSet2 { get { return new SimConnectEvent() { Id = SimConnectEventId.TurbineIgnitionSwitchSet2, Name = "TURBINE_IGNITION_SWITCH_SET2", Units = "", Description = "IMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent TurbineIgnitionSwitchSet3 { get { return new SimConnectEvent() { Id = SimConnectEventId.TurbineIgnitionSwitchSet3, Name = "TURBINE_IGNITION_SWITCH_SET3", Units = "", Description = "IMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent TurbineIgnitionSwitchSet4 { get { return new SimConnectEvent() { Id = SimConnectEventId.TurbineIgnitionSwitchSet4, Name = "TURBINE_IGNITION_SWITCH_SET4", Units = "", Description = "IMPORTANT: This event is only applicable to the DarkStar aircraft and should not be used for your own aircraft." }; } }
        private SimConnectEvent TurbineIgnitionSwitchToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.TurbineIgnitionSwitchToggle, Name = "TURBINE_IGNITION_SWITCH_TOGGLE", Units = "", Description = "Turn the turbine ignition switch on or off." }; } }
        private SimConnectEvent UnlockTarget { get { return new SimConnectEvent() { Id = SimConnectEventId.UnlockTarget, Name = "UNLOCK_TARGET", Units = "N/A", Description = "Deprecated, do not use." }; } }
        private SimConnectEvent Userinterrupt { get { return new SimConnectEvent() { Id = SimConnectEventId.Userinterrupt, Name = "USERINTERRUPT", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent VariometerSoundToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.VariometerSoundToggle, Name = "VARIOMETER_SOUND_TOGGLE", Units = "N/A", Description = "Toggle the variometer sounds between on (1) and off (0)." }; } }
        private SimConnectEvent VerticalSpeedDec { get { return new SimConnectEvent() { Id = SimConnectEventId.VerticalSpeedDec, Name = "VERTICAL_SPEED_DEC", Units = "[0] Value between -16000 and 16000 to decrement by\r\n(maps to a value between -1 and 1)", Description = "By default this will decrement the vertical speed by 1/128, but you may supply a decrement amount as a parameter." }; } }
        private SimConnectEvent VerticalSpeedInc { get { return new SimConnectEvent() { Id = SimConnectEventId.VerticalSpeedInc, Name = "VERTICAL_SPEED_INC", Units = "[0] Value between -16000 and 16000 to increment by\r\n(maps to a value between -1 and 1)", Description = "By default this will increment the vertical speed by 1/128, but you may supply an increment amount as a parameter." }; } }
        private SimConnectEvent VerticalSpeedSet { get { return new SimConnectEvent() { Id = SimConnectEventId.VerticalSpeedSet, Name = "VERTICAL_SPEED_SET", Units = "[0] The axis value between -1 and 1 (maps to -16000 and 16000)", Description = "Set the vertical speed to a value between -1 and 1." }; } }
        private SimConnectEvent VerticalSpeedZero { get { return new SimConnectEvent() { Id = SimConnectEventId.VerticalSpeedZero, Name = "VERTICAL_SPEED_ZERO", Units = "N/A", Description = "Set the veretical speed to 0." }; } }
        private SimConnectEvent VideoRecordToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.VideoRecordToggle, Name = "VIDEO_RECORD_TOGGLE", Units = "N/A", Description = "Turn on or off the video recording feature. This records uncompressed AVI format files to: My Documents\\Videos\\" }; } }
        private SimConnectEvent View { get { return new SimConnectEvent() { Id = SimConnectEventId.View, Name = "VIEW", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAlwaysPanDown { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAlwaysPanDown, Name = "VIEW_ALWAYS_PAN_DOWN", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAlwaysPanUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAlwaysPanUp, Name = "VIEW_ALWAYS_PAN_UP", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAux00 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAux00, Name = "VIEW_AUX_00", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAux01 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAux01, Name = "VIEW_AUX_01", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAux02 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAux02, Name = "VIEW_AUX_02", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAux03 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAux03, Name = "VIEW_AUX_03", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAux04 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAux04, Name = "VIEW_AUX_04", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAux05 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAux05, Name = "VIEW_AUX_05", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewAxisIndicatorCycle { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewAxisIndicatorCycle, Name = "VIEW_AXIS_INDICATOR_CYCLE", Units = "N/A", Description = "Step through the view axis." }; } }
        private SimConnectEvent ViewCameraSelect0 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect0, Name = "VIEW_CAMERA_SELECT_0", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect1 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect1, Name = "VIEW_CAMERA_SELECT_1", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect2 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect2, Name = "VIEW_CAMERA_SELECT_2", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect3 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect3, Name = "VIEW_CAMERA_SELECT_3", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect4 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect4, Name = "VIEW_CAMERA_SELECT_4", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect5 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect5, Name = "VIEW_CAMERA_SELECT_5", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect6 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect6, Name = "VIEW_CAMERA_SELECT_6", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect7 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect7, Name = "VIEW_CAMERA_SELECT_7", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect8 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect8, Name = "VIEW_CAMERA_SELECT_8", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelect9 { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelect9, Name = "VIEW_CAMERA_SELECT_9", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewCameraSelectStart { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCameraSelectStart, Name = "VIEW_CAMERA_SELECT_START", Units = "N/A", Description = "Select View Direction" }; } }
        private SimConnectEvent ViewChaseDistanceAdd { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewChaseDistanceAdd, Name = "VIEW_CHASE_DISTANCE_ADD", Units = "N/A", Description = "Increments the distance of the view camera from the chase object (such as in Spot Plane view, or viewing an AI controlled aircraft)." }; } }
        private SimConnectEvent ViewChaseDistanceSub { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewChaseDistanceSub, Name = "VIEW_CHASE_DISTANCE_SUB", Units = "N/A", Description = "Decrements the distance of the view camera from the chase object." }; } }
        private SimConnectEvent ViewCockpitForward { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewCockpitForward, Name = "VIEW_COCKPIT_FORWARD", Units = "N/A", Description = "Switch immediately to the forward view, in 2D mode." }; } }
        private SimConnectEvent ViewDirectionSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewDirectionSet, Name = "VIEW_DIRECTION_SET", Units = "N/A", Description = "Switch immediately to the forward view, in 2D mode." }; } }
        private SimConnectEvent ViewDown { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewDown, Name = "VIEW_DOWN", Units = "N/A", Description = "Sets view direction down" }; } }
        private SimConnectEvent ViewForward { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewForward, Name = "VIEW_FORWARD", Units = "N/A", Description = "Sets view direction forward" }; } }
        private SimConnectEvent ViewForwardLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewForwardLeft, Name = "VIEW_FORWARD_LEFT", Units = "N/A", Description = "Sets view direction forward and left" }; } }
        private SimConnectEvent ViewForwardLeftUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewForwardLeftUp, Name = "VIEW_FORWARD_LEFT_UP", Units = "N/A", Description = "Sets view forward left and up" }; } }
        private SimConnectEvent ViewForwardRight { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewForwardRight, Name = "VIEW_FORWARD_RIGHT", Units = "N/A", Description = "Sets view direction forward and right" }; } }
        private SimConnectEvent ViewForwardRightUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewForwardRightUp, Name = "VIEW_FORWARD_RIGHT_UP", Units = "N/A", Description = "Sets view forward, right, and up" }; } }
        private SimConnectEvent ViewForwardUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewForwardUp, Name = "VIEW_FORWARD_UP", Units = "N/A", Description = "Sets view forward and up" }; } }
        private SimConnectEvent ViewLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewLeft, Name = "VIEW_LEFT", Units = "N/A", Description = "Sets view direction to the left" }; } }
        private SimConnectEvent ViewLeftUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewLeftUp, Name = "VIEW_LEFT_UP", Units = "N/A", Description = "Sets view left and up" }; } }
        private SimConnectEvent ViewLinkingSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewLinkingSet, Name = "VIEW_LINKING_SET", Units = "N/A", Description = "Links all the views from one camera together, so that panning the view will change the view of all the linked cameras." }; } }
        private SimConnectEvent ViewLinkingToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewLinkingToggle, Name = "VIEW_LINKING_TOGGLE", Units = "N/A", Description = "Turns view linking on or off." }; } }
        private SimConnectEvent ViewMode { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewMode, Name = "VIEW_MODE", Units = "N/A", Description = "Selects next view" }; } }
        private SimConnectEvent ViewModeRev { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewModeRev, Name = "VIEW_MODE_REV", Units = "N/A", Description = "Reverse view cycle" }; } }
        private SimConnectEvent ViewPanelAlphaDec { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewPanelAlphaDec, Name = "VIEW_PANEL_ALPHA_DEC", Units = "N/A", Description = "Decrement alpha-blending for the panel." }; } }
        private SimConnectEvent ViewPanelAlphaInc { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewPanelAlphaInc, Name = "VIEW_PANEL_ALPHA_INC", Units = "N/A", Description = "Increment alpha-blending for the panel." }; } }
        private SimConnectEvent ViewPanelAlphaSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewPanelAlphaSelect, Name = "VIEW_PANEL_ALPHA_SELECT", Units = "N/A", Description = "Sets the mode to change the alpha-blending, so the keys KEY_PLUS and KEY_MINUS increment and decrement the value." }; } }
        private SimConnectEvent ViewPanelAlphaSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewPanelAlphaSet, Name = "VIEW_PANEL_ALPHA_SET", Units = "N/A", Description = "Sets the alpha-blending value for the panel. Takes a parameter in the range 0to 255. The alpha-blending can be changed from the keyboard using Ctrl-Shift-T,and the plus and minus keys." }; } }
        private SimConnectEvent ViewPreviousToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewPreviousToggle, Name = "VIEW_PREVIOUS_TOGGLE", Units = "N/A", Description = "Sets the alpha-blending value for the panel. Takes a parameter in the range 0to 255. The alpha-blending can be changed from the keyboard using Ctrl-Shift-T,and the plus and minus keys." }; } }
        private SimConnectEvent ViewRear { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRear, Name = "VIEW_REAR", Units = "N/A", Description = "Sets view direction to the rear" }; } }
        private SimConnectEvent ViewRearLeft { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRearLeft, Name = "VIEW_REAR_LEFT", Units = "N/A", Description = "Sets view direction to the rear and left" }; } }
        private SimConnectEvent ViewRearLeftUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRearLeftUp, Name = "VIEW_REAR_LEFT_UP", Units = "N/A", Description = "Sets view rear left and up" }; } }
        private SimConnectEvent ViewRearRight { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRearRight, Name = "VIEW_REAR_RIGHT", Units = "N/A", Description = "Sets view direction to the rear and right" }; } }
        private SimConnectEvent ViewRearRightUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRearRightUp, Name = "VIEW_REAR_RIGHT_UP", Units = "N/A", Description = "Sets view rear, right, and up" }; } }
        private SimConnectEvent ViewRearUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRearUp, Name = "VIEW_REAR_UP", Units = "N/A", Description = "Sets view rear and up" }; } }
        private SimConnectEvent ViewReset { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewReset, Name = "VIEW_RESET", Units = "N/A", Description = "Resets the view to the default" }; } }
        private SimConnectEvent ViewRight { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRight, Name = "VIEW_RIGHT", Units = "N/A", Description = "Sets view direction to the right" }; } }
        private SimConnectEvent ViewRightUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewRightUp, Name = "VIEW_RIGHT_UP", Units = "N/A", Description = "Sets view right and up" }; } }
        private SimConnectEvent ViewSnapPanel { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewSnapPanel, Name = "VIEW_SNAP_PANEL", Units = "N/A", Description = "Sets view right and up" }; } }
        private SimConnectEvent ViewSnapPanelReset { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewSnapPanelReset, Name = "VIEW_SNAP_PANEL_RESET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ViewTrackPanToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewTrackPanToggle, Name = "VIEW_TRACK_PAN_TOGGLE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ViewType { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewType, Name = "VIEW_TYPE", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ViewTypeRev { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewTypeRev, Name = "VIEW_TYPE_REV", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent ViewUp { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewUp, Name = "VIEW_UP", Units = "N/A", Description = "Sets view up" }; } }
        private SimConnectEvent ViewVirtualCockpitForward { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewVirtualCockpitForward, Name = "VIEW_VIRTUAL_COCKPIT_FORWARD", Units = "N/A", Description = "Switch immediately to the forward view, in virtual cockpit mode." }; } }
        private SimConnectEvent ViewWindowTitlesToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewWindowTitlesToggle, Name = "VIEW_WINDOW_TITLES_TOGGLE", Units = "N/A", Description = "Turn window titles on or off." }; } }
        private SimConnectEvent ViewWindowToFront { get { return new SimConnectEvent() { Id = SimConnectEventId.ViewWindowToFront, Name = "VIEW_WINDOW_TO_FRONT", Units = "N/A", Description = "Sets active window to front" }; } }
        private SimConnectEvent View1DirectionSet { get { return new SimConnectEvent() { Id = SimConnectEventId.View1DirectionSet, Name = "VIEW1_DIRECTION_SET", Units = "N/A", Description = "Switch immediately to the forward view, in 2D mode." }; } }
        private SimConnectEvent View1ModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.View1ModeSet, Name = "VIEW1_MODE_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent View1ZoomSet { get { return new SimConnectEvent() { Id = SimConnectEventId.View1ZoomSet, Name = "VIEW1_ZOOM_SET", Units = "N/A", Description = "Sets active window to front" }; } }
        private SimConnectEvent View2DirectionSet { get { return new SimConnectEvent() { Id = SimConnectEventId.View2DirectionSet, Name = "VIEW2_DIRECTION_SET", Units = "N/A", Description = "Switch immediately to the forward view, in 2D mode." }; } }
        private SimConnectEvent View2ModeSet { get { return new SimConnectEvent() { Id = SimConnectEventId.View2ModeSet, Name = "VIEW2_MODE_SET", Units = "N/A", Description = "Not currently used in the simulation." }; } }
        private SimConnectEvent View2ZoomSet { get { return new SimConnectEvent() { Id = SimConnectEventId.View2ZoomSet, Name = "VIEW2_ZOOM_SET", Units = "N/A", Description = "Sets active window to front" }; } }
        private SimConnectEvent VirtualCopilotAction { get { return new SimConnectEvent() { Id = SimConnectEventId.VirtualCopilotAction, Name = "VIRTUAL_COPILOT_ACTION", Units = "N/A", Description = "Triggers action noted in Flying Tips" }; } }
        private SimConnectEvent VirtualCopilotSet { get { return new SimConnectEvent() { Id = SimConnectEventId.VirtualCopilotSet, Name = "VIRTUAL_COPILOT_SET", Units = "[0]: Enable or disable (TRUE/FALSE)", Description = "Sets Flying Tips on/off (1,0)" }; } }
        private SimConnectEvent VirtualCopilotToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.VirtualCopilotToggle, Name = "VIRTUAL_COPILOT_TOGGLE", Units = "N/A", Description = "Turns Flying Tips on/off" }; } }
        private SimConnectEvent VorObs { get { return new SimConnectEvent() { Id = SimConnectEventId.VorObs, Name = "VOR_OBS", Units = "N/A", Description = "Sequentially selects the VOR OBS for use with +/-. Follow by SELECT_1 for VOR 1 and SELECT_2 for VOR 2." }; } }
        private SimConnectEvent Vor1ObiDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor1ObiDec, Name = "VOR1_OBI_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor1ObiFastDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor1ObiFastDec, Name = "VOR1_OBI_FAST_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 0 and not arap." }; } }
        private SimConnectEvent Vor1ObiFastInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor1ObiFastInc, Name = "VOR1_OBI_FAST_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 360 and not arap." }; } }
        private SimConnectEvent Vor1ObiInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor1ObiInc, Name = "VOR1_OBI_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor1Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor1Set, Name = "VOR1_SET", Units = "[0]: Value (0 - 360)", Description = "Sets OBS 1/2/3/4 (0 to 360)" }; } }
        private SimConnectEvent Vor2ObiDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor2ObiDec, Name = "VOR2_OBI_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor2ObiFastDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor2ObiFastDec, Name = "VOR2_OBI_FAST_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 0 and not arap." }; } }
        private SimConnectEvent Vor2ObiFastInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor2ObiFastInc, Name = "VOR2_OBI_FAST_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 360 and not arap." }; } }
        private SimConnectEvent Vor2ObiInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor2ObiInc, Name = "VOR2_OBI_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor2Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor2Set, Name = "VOR2_SET", Units = "[0]: Value (0 - 360)", Description = "Sets OBS 1/2/3/4 (0 to 360)" }; } }
        private SimConnectEvent Vor3ObiDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor3ObiDec, Name = "VOR3_OBI_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor3ObiFastDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor3ObiFastDec, Name = "VOR3_OBI_FAST_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 0 and not arap." }; } }
        private SimConnectEvent Vor3ObiFastInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor3ObiFastInc, Name = "VOR3_OBI_FAST_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 360 and not arap." }; } }
        private SimConnectEvent Vor3ObiInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor3ObiInc, Name = "VOR3_OBI_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor3Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor3Set, Name = "VOR3_SET", Units = "[0]: Value (0 - 360)", Description = "Sets OBS 1/2/3/4 (0 to 360)" }; } }
        private SimConnectEvent Vor4ObiDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor4ObiDec, Name = "VOR4_OBI_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor4ObiFastDec { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor4ObiFastDec, Name = "VOR4_OBI_FAST_DEC", Units = "N/A", Description = "Decrements the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 0 and not arap." }; } }
        private SimConnectEvent Vor4ObiFastInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor4ObiFastInc, Name = "VOR4_OBI_FAST_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting by 10 degrees. The value will stop on 360 and not arap." }; } }
        private SimConnectEvent Vor4ObiInc { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor4ObiInc, Name = "VOR4_OBI_INC", Units = "N/A", Description = "Increments the VOR 1/2/3/4 OBS setting" }; } }
        private SimConnectEvent Vor4Set { get { return new SimConnectEvent() { Id = SimConnectEventId.Vor4Set, Name = "VOR4_SET", Units = "[0]: Value (0 - 360)", Description = "Sets OBS 1/2/3/4 (0 to 360)" }; } }
        private SimConnectEvent VsSlotIndexSet { get { return new SimConnectEvent() { Id = SimConnectEventId.VsSlotIndexSet, Name = "VS_SLOT_INDEX_SET", Units = "N/A", Description = "Set autopilot vertical speed slot index." }; } }
        private SimConnectEvent VsiBugSelect { get { return new SimConnectEvent() { Id = SimConnectEventId.VsiBugSelect, Name = "VSI_BUG_SELECT", Units = "[0]: reference value", Description = "Selects the vertical speed reference for use with +/-" }; } }
        private SimConnectEvent WarEmergencyPower { get { return new SimConnectEvent() { Id = SimConnectEventId.WarEmergencyPower, Name = "WAR_EMERGENCY_POWER", Units = "[0]: Speed (ft / s)", Description = "Triggers tug, and sets desired speed, in feet per second. The speed can be either positive (forward movement) or negative (backward movement)." }; } }
        private SimConnectEvent WindowTitlesSet { get { return new SimConnectEvent() { Id = SimConnectEventId.WindowTitlesSet, Name = "WINDOW_TITLES_SET", Units = "N/A", Description = "Turn on or off the video recording feature. This records uncompressed AVI format files to: My Documents\\Videos\\" }; } }
        private SimConnectEvent WindshieldDeiceOff { get { return new SimConnectEvent() { Id = SimConnectEventId.WindshieldDeiceOff, Name = "WINDSHIELD_DEICE_OFF", Units = "N/A", Description = "Switches on the windshield deicing system." }; } }
        private SimConnectEvent WindshieldDeiceOn { get { return new SimConnectEvent() { Id = SimConnectEventId.WindshieldDeiceOn, Name = "WINDSHIELD_DEICE_ON", Units = "N/A", Description = "Switches off the windshield deicing system." }; } }
        private SimConnectEvent WindshieldDeiceSet { get { return new SimConnectEvent() { Id = SimConnectEventId.WindshieldDeiceSet, Name = "WINDSHIELD_DEICE_SET", Units = "[0]: Bool", Description = "Sets the windshield deicing system on or off based on the input parameter [0]." }; } }
        private SimConnectEvent WindshieldDeiceToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.WindshieldDeiceToggle, Name = "WINDSHIELD_DEICE_TOGGLE", Units = "N/A", Description = "Toggles the windshield deicing system on and off." }; } }
        private SimConnectEvent WingLightsOff { get { return new SimConnectEvent() { Id = SimConnectEventId.WingLightsOff, Name = "WING_LIGHTS_OFF", Units = "[0]: light circuit index", Description = "Turn wing lights off" }; } }
        private SimConnectEvent WingLightsOn { get { return new SimConnectEvent() { Id = SimConnectEventId.WingLightsOn, Name = "WING_LIGHTS_ON", Units = "[0]: light circuit index", Description = "Turn wing light on" }; } }
        private SimConnectEvent WingLightsSet { get { return new SimConnectEvent() { Id = SimConnectEventId.WingLightsSet, Name = "WING_LIGHTS_SET", Units = "[0]: state, either on (1) or off (0)\r\n[1]: light index", Description = "Set wing lights on/off (1,0)" }; } }
        private SimConnectEvent Xpndr { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr, Name = "XPNDR", Units = "[0]: Value (0 - 360)", Description = "Sequentially selects the transponder digits for use with +/-." }; } }
        private SimConnectEvent Xpndr1Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr1Dec, Name = "XPNDR_1_DEC", Units = "N/A", Description = "Decrements the fourth digit of the transponder." }; } }
        private SimConnectEvent Xpndr1Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr1Inc, Name = "XPNDR_1_INC", Units = "N/A", Description = "Increments the fourth digit of the transponder." }; } }
        private SimConnectEvent Xpndr10Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr10Dec, Name = "XPNDR_10_DEC", Units = "N/A", Description = "Decrements the third digit of the transponder." }; } }
        private SimConnectEvent Xpndr10Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr10Inc, Name = "XPNDR_10_INC", Units = "N/A", Description = "Increments the third digit of the transponder." }; } }
        private SimConnectEvent Xpndr100Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr100Dec, Name = "XPNDR_100_DEC", Units = "N/A", Description = "Decrements the second digit of the transponder." }; } }
        private SimConnectEvent Xpndr100Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr100Inc, Name = "XPNDR_100_INC", Units = "N/A", Description = "Increments the second digit of the transponder." }; } }
        private SimConnectEvent Xpndr1000Dec { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr1000Dec, Name = "XPNDR_1000_DEC", Units = "N/A", Description = "Decrements the first digit of the transponder." }; } }
        private SimConnectEvent Xpndr1000Inc { get { return new SimConnectEvent() { Id = SimConnectEventId.Xpndr1000Inc, Name = "XPNDR_1000_INC", Units = "N/A", Description = "Increments the first digit of the transponder." }; } }
        private SimConnectEvent XpndrDecCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrDecCarry, Name = "XPNDR_DEC_CARRY", Units = "N/A", Description = "Decrements the fourth digit of the transponder, with carry." }; } }
        private SimConnectEvent XpndrIdentOff { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrIdentOff, Name = "XPNDR_IDENT_OFF", Units = "N/A", Description = "Disable the transponder Ident (can be used along with the simvar TRANSPONDER_IDENT)." }; } }
        private SimConnectEvent XpndrIdentOn { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrIdentOn, Name = "XPNDR_IDENT_ON", Units = "N/A", Description = "Enable the transponder Ident (can be used along with the simvar TRANSPONDER_IDENT). After 18 seconds it will disable automatically." }; } }
        private SimConnectEvent XpndrIdentSet { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrIdentSet, Name = "XPNDR_IDENT_SET", Units = "[0]: Bool", Description = "Set the transponder Ident on or off (can be used along with the simvar TRANSPONDER_IDENT). If set to on, it will switch off automatically after 18 seconds." }; } }
        private SimConnectEvent XpndrIdentToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrIdentToggle, Name = "XPNDR_IDENT_TOGGLE", Units = "N/A", Description = "Toggle the transponder Ident from on to off or off to on (can be used along with the simvar TRANSPONDER_IDENT)." }; } }
        private SimConnectEvent XpndrIncCarry { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrIncCarry, Name = "XPNDR_INC_CARRY", Units = "N/A", Description = "Increments the fourth digit of the transponder, with carry." }; } }
        private SimConnectEvent XpndrSet { get { return new SimConnectEvent() { Id = SimConnectEventId.XpndrSet, Name = "XPNDR_SET", Units = "[0]: Frequency value (BCD16 encoded)", Description = "Sets the transponder frequency code." }; } }
        private SimConnectEvent YawDamperOff { get { return new SimConnectEvent() { Id = SimConnectEventId.YawDamperOff, Name = "YAW_DAMPER_OFF", Units = "N/A", Description = "Turns yaw damper off" }; } }
        private SimConnectEvent YawDamperOn { get { return new SimConnectEvent() { Id = SimConnectEventId.YawDamperOn, Name = "YAW_DAMPER_ON", Units = "N/A", Description = "Turns yaw damper on" }; } }
        private SimConnectEvent YawDamperSet { get { return new SimConnectEvent() { Id = SimConnectEventId.YawDamperSet, Name = "YAW_DAMPER_SET", Units = "[0]: enable/disable yaw damper (TRUE, FALSE)", Description = "Sets yaw damper on/off (1,0)" }; } }
        private SimConnectEvent YawDamperToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.YawDamperToggle, Name = "YAW_DAMPER_TOGGLE", Units = "N/A", Description = "Toggles yaw damper on/off" }; } }
        private SimConnectEvent YaxisInvertToggle { get { return new SimConnectEvent() { Id = SimConnectEventId.YaxisInvertToggle, Name = "YAXIS_INVERT_TOGGLE", Units = "N/A", Description = "Switch inversion of Y axis controls on or off." }; } }
        private SimConnectEvent Zoom1X { get { return new SimConnectEvent() { Id = SimConnectEventId.Zoom1X, Name = "ZOOM_1X", Units = "N/A", Description = "Sets zoom level to 1" }; } }
        private SimConnectEvent ZoomIn { get { return new SimConnectEvent() { Id = SimConnectEventId.ZoomIn, Name = "ZOOM_IN", Units = "N/A", Description = "Zooms view in" }; } }
        private SimConnectEvent ZoomInFine { get { return new SimConnectEvent() { Id = SimConnectEventId.ZoomInFine, Name = "ZOOM_IN_FINE", Units = "N/A", Description = "Zoom in fine\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent ZoomMinus { get { return new SimConnectEvent() { Id = SimConnectEventId.ZoomMinus, Name = "ZOOM_MINUS", Units = "", Description = "Decreases zoom" }; } }
        private SimConnectEvent ZoomOut { get { return new SimConnectEvent() { Id = SimConnectEventId.ZoomOut, Name = "ZOOM_OUT", Units = "", Description = "Zooms view out" }; } }
        private SimConnectEvent ZoomOutFine { get { return new SimConnectEvent() { Id = SimConnectEventId.ZoomOutFine, Name = "ZOOM_OUT_FINE", Units = "", Description = "Zoom out fine\r\nNot currently used in the simulation." }; } }
        private SimConnectEvent ZoomPlus { get { return new SimConnectEvent() { Id = SimConnectEventId.ZoomPlus, Name = "ZOOM_PLUS", Units = "", Description = "Increase zoom" }; } }
        private SimConnectEvent ZuluDaySet { get { return new SimConnectEvent() { Id = SimConnectEventId.ZuluDaySet, Name = "ZULU_DAY_SET", Units = "N/A", Description = "Sets day, in zulu time" }; } }
        private SimConnectEvent ZuluHoursSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ZuluHoursSet, Name = "ZULU_HOURS_SET", Units = "N/A", Description = "Sets hours, zulu time" }; } }
        private SimConnectEvent ZuluMinutesSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ZuluMinutesSet, Name = "ZULU_MINUTES_SET", Units = "N/A", Description = "Sets minutes, in zulu time" }; } }
        private SimConnectEvent ZuluYearSet { get { return new SimConnectEvent() { Id = SimConnectEventId.ZuluYearSet, Name = "ZULU_YEAR_SET", Units = "N/A", Description = "Sets year, in zulu time" }; } }
    }
}
