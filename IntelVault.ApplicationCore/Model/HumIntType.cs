namespace IntelVault.ApplicationCore.Model;

/// <summary>
/// Human intelligence (HUMINT) are gathered from a person in the location in question. Sources can include the following:
/// 
/// Advisors or foreign internal defense (FID) personnel working with host nation (HN) forces or populations
/// Diplomatic reporting by accredited diplomats (e.g. military attachés)
/// Espionage clandestine reporting, access agents, couriers, cutouts
/// Military attachés
/// Non-governmental organizations (NGOs)
/// Prisoners of war (POWs) or detainees
/// Refugees
/// Routine patrolling (military police, patrols, etc.)
/// Special reconnaissance
/// Traveler debriefing (e.g. CIA Domestic Contact Service)
/// </summary>
public enum HumIntType
{
    Advisors,
    DiplomaticReporting,
    Espionage,
    MilitaryAttachés,
    NonGovernmentalOrganizations,
    POW,
    Refugees,
    RoutinePatrolling,
    SpecialReconnaissance,
    TravelerDebriefing

}