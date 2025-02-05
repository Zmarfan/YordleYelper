﻿create table game_modes(
    mode varchar(100) not null,

    constraint game_modes_pk primary key (mode)
);

create table game_types(
    type varchar(100) not null,

    constraint game_type_pk primary key (type)
);

create table stat_perks(
    id int not null,
    name varchar(100) not null,

    constraint stat_perks_pk primary key (id)
);

create table map_ids(
    id int not null,
    name varchar(255) not null,

    constraint map_ids_pk primary key (id)
);

create table registered_users(
    puuid varchar(78) not null,
    account_id varchar(56) not null unique,
    summoner_id varchar(63) not null unique,
    game_name varchar(255) not null,
    tag_line varchar(255) not null,
    summoner_name varchar(255) not null,
    has_been_initialized bool not null,
    last_matches_check timestamp,
    
    constraint registered_users_pk primary key (puuid)
);

create table match_ids(
    id varchar(255) not null,

    constraint match_ids_pk primary key (id)
);

create table matches(
    match_id varchar(255) not null,
    game_creation_timestamp timestamp not null,
    game_start_timestamp timestamp not null,
    game_end_timestamp timestamp not null,
    game_duration int not null,
    game_mode varchar(100) not null,
    game_type varchar(100) not null,
    map_id int not null,
    
    constraint matches_pk primary key (match_id),
    constraint matches_match_id foreign key (match_id) references match_ids(id),
    constraint matches_game_mode foreign key (game_mode) references game_modes(mode),
    constraint matches_game_type foreign key (game_type) references game_types(type),
    constraint matches_map_id foreign key (map_id) references map_ids(id)
);

create table match_teams(
    match_id varchar(255) not null,
    left_team bool not null,
    
    win bool not null,
    
    champion_id_ban_1 int,
    champion_id_ban_2 int,
    champion_id_ban_3 int,
    champion_id_ban_4 int,
    champion_id_ban_5 int,
    
    first_to_take_baron bool not null,
    first_to_take_champion bool not null,
    first_to_take_dragon bool not null,
    first_to_take_inhibitor bool not null,
    first_to_take_rift_herald bool not null,
    first_to_take_tower bool not null,

    baron_amount int not null,
    champion_amount int not null,
    dragon_amount int not null,
    inhibitor_amount int not null,
    rift_herald_amount int not null,
    tower_amount int not null,
    
    constraint match_teams_pk primary key (match_id, left_team),
    constraint match_teams_match_id foreign key (match_id) references match_ids(id)
);

create table match_participant_challenges(
    puuid varchar(78) not null,
    match_id varchar(255) not null,

    12_assist_streak_count float, 
    ability_uses float, 
    aces_before_15_minutes float, 
    allied_jungle_monster_kills float, 
    baron_buff_gold_advantage_over_threshold float, 
    baron_takedowns float, 
    blast_cone_opposite_opponent_count float, 
    bounty_gold float, 
    buffs_stolen float, 
    complete_support_quest_in_time float, 
    control_wards_placed float, 
    control_ward_time_coverage_in_river_or_enemy_half float, 
    damage_per_minute float, 
    damage_taken_on_team_percentage float, 
    danced_with_rift_herald float, 
    deaths_by_enemy_champs float, 
    dodge_skill_shots_small_window float, 
    double_aces float, 
    dragon_takedowns float,
    earliest_baron float,
    earliest_dragon_takedown float,
    earliest_elder_dragon float,
    early_laning_phase_gold_exp_advantage float, 
    effective_heal_and_shielding float, 
    elder_dragon_kills_with_opposing_soul float, 
    elder_dragon_multikills float, 
    enemy_champion_immobilizations float, 
    enemy_jungle_monster_kills float, 
    epic_monster_kills_near_enemy_jungler float, 
    epic_monster_kills_within_30_seconds_of_spawn float, 
    epic_monster_steals float, 
    epic_monster_stolen_without_smite float,
    faster_support_quest_completion float,
    fastest_legendary float,
    first_turret_killed float, 
    first_turret_killed_time float, 
    flawless_aces float, 
    full_team_takedown float, 
    game_length float, 
    get_takedowns_in_all_lanes_early_jungle_as_laner float, 
    gold_per_minute float,
    had_afk_teammate float,
    had_open_nexus float,
    highest_champion_damage float,
    highest_crowd_control_score float,
    highest_ward_kills float,
    immobilize_and_kill_with_ally float, 
    initial_buff_count float, 
    initial_crab_count float, 
    jungle_cs_before_10_minutes float, 
    jungler_kills_early_jungle float, 
    jungler_takedowns_near_damaged_epic_monster float, 
    k_turrets_destroyed_before_plates_fall float, 
    kda float, 
    kill_after_hidden_with_ally float, 
    kill_participation float, 
    killed_champ_took_full_team_damage_survived float, 
    killing_sprees float, 
    kills_near_enemy_turret float, 
    kills_on_laners_early_jungle_as_jungler float,
    kills_on_other_lanes_early_jungle_as_laner float,
    kills_on_recently_healed_by_aram_pack float, 
    kills_under_own_turret float, 
    kills_with_help_from_epic_monster float, 
    knock_enemy_into_team_and_kill float, 
    land_skill_shots_early_game float, 
    lane_minions_first_10_minutes float, 
    laning_phase_gold_exp_advantage float, 
    legendary_count float, 
    lost_an_inhibitor float, 
    max_cs_advantage_on_lane_opponent float, 
    max_kill_deficit float, 
    max_level_lead_lane_opponent float, 
    mejais_full_stack_in_time float, 
    more_enemy_jungle_than_opponent float,
    most_wards_destroyed_one_sweeper float,
    multi_kill_one_spell float, 
    multikills float, 
    multikills_after_aggressive_flash float,
    multi_turret_rift_herald_count float,
    mythic_item_used float, 
    outer_turret_executes_before_10_minutes float, 
    outnumbered_kills float, 
    outnumbered_nexus_kill float, 
    perfect_dragon_souls_taken float, 
    perfect_game float, 
    pick_kill_with_ally float, 
    played_champ_select_position float, 
    poro_explosions float, 
    quick_cleanse float, 
    quick_first_turret float, 
    quick_solo_kills float, 
    rift_herald_takedowns float, 
    save_ally_from_death float, 
    scuttle_crab_kills float,
    shortest_time_to_ace_from_first_takedown float,
    skillshots_dodged float, 
    skillshots_hit float, 
    snowballs_hit float, 
    solo_baron_kills float,
    solo_turrets_lategame float, 
    solo_kills float, 
    stealth_wards_placed float, 
    survived_single_digit_hp_count float, 
    survived_three_immobilizes_in_fight float, 
    takedown_on_first_turret float, 
    takedowns float, 
    takedowns_after_gaining_level_advantage float, 
    takedowns_before_jungle_minion_spawn float, 
    takedowns_first_xminutes float,
    takedowns_first_25_minutes float,
    takedowns_in_alcove float, 
    takedowns_in_enemy_fountain float, 
    team_baron_kills float, 
    team_damage_percentage float, 
    team_elder_dragon_kills float, 
    team_rift_herald_kills float,
    teleport_takedowns float,
    third_inhibitor_destroyed_time float,
    three_wards_one_sweeper_count float,
    took_large_damage_survived float, 
    turret_plates_taken float, 
    turret_takedowns float, 
    turrets_taken_with_rift_herald float, 
    twenty_minions_in_3_seconds_count float, 
    two_wards_one_sweeper_count float, 
    unseen_recalls float, 
    vision_score_advantage_lane_opponent float, 
    vision_score_per_minute float, 
    ward_takedowns float, 
    ward_takedowns_before_20_m float, 
    wards_guarded float,

    constraint match_participant_challenges_pk primary key (puuid, match_id),
    constraint match_participant_challenges_match_id foreign key (match_id) references matches(match_id)
);

create table match_participants(
    puuid varchar(78) not null,
    match_id varchar(255) not null,
    left_team bool not null,
    participant_id int not null,
    
    summoner_name varchar(63) not null,
    summoner_level int not null,
    profile_icon_id int not null,
    
    win bool not null,
    game_ended_in_early_surrender bool not null,
    game_ended_in_surrender bool not null,
    team_early_surrendered bool not null,
    
    kills int not null,
    deaths int not null,
    assists int not null,
    team_position varchar(255) not null,
    time_played int not null,
    baron_kills int not null,
    dragon_kills int not null,
    bounty_level int not null,
    
    double_kills int not null,
    triple_kills int not null,
    quadra_kills int not null,
    penta_kills int not null,
    unreal_kills int not null,
    killing_sprees int not null,
    largest_killing_spree int not null,
    largest_multi_kill int not null,
    
    champion_id int not null,
    champion_name varchar(255) not null,
    champion_experience int not null,
    champion_level int not null,
    champion_transform int not null,
    
    primary_perk_style int not null,
    primary_perk int not null,
    primary_perk_var_1 int not null,
    primary_perk_var_2 int not null,
    primary_perk_var_3 int not null,
    primary_perk_1 int not null,
    primary_perk_1_var_1 int not null,
    primary_perk_1_var_2 int not null,
    primary_perk_1_var_3 int not null,
    primary_perk_2 int not null,
    primary_perk_2_var_1 int not null,
    primary_perk_2_var_2 int not null,
    primary_perk_2_var_3 int not null,
    primary_perk_3 int not null,
    primary_perk_3_var_1 int not null,
    primary_perk_3_var_2 int not null,
    primary_perk_3_var_3 int not null,
    secondary_perk_style int not null,
    secondary_perk_1 int not null,
    secondary_perk_1_var_1 int not null,
    secondary_perk_1_var_2 int not null,
    secondary_perk_1_var_3 int not null,
    secondary_perk_2 int not null,
    secondary_perk_2_var_1 int not null,
    secondary_perk_2_var_2 int not null,
    secondary_perk_2_var_3 int not null,
    
    stat_perk_offensive int not null,
    stat_perk_flex int not null,
    stat_perk_defensive int not null,
    
    spell_q_casts int not null,
    spell_w_casts int not null,
    spell_e_casts int not null,
    spell_r_casts int not null,
    summoner_1_id int not null,
    summoner_2_id int not null,
    summoner_1_casts int not null,
    summoner_2_casts int not null,
    
    gold_earned int not null,
    gold_spent int not null,
    consumables_purchased int not null,
    items_purchased int not null,
    sight_wards_bought_in_game int not null,
    vision_wards_bought_in_game int not null,
    item0 int not null,
    item1 int not null,
    item2 int not null,
    item3 int not null,
    item4 int not null,
    item5 int not null,
    item6 int not null,

    first_blood_assist bool not null,
    first_blood_kill bool not null,
    first_tower_assist bool not null,
    first_tower_kill bool not null,
    turret_kills int not null,
    turret_takedowns int not null,
    turrets_lost int not null,
    inhibitor_kills int not null,
    inhibitor_takedowns int not null,
    inhibitors_lost int not null,
    damage_dealt_to_buildings int not null,
    damage_dealt_to_objectives int not null,
    damage_dealt_to_turrets int not null,
    damage_self_mitigated int not null,
    nexus_kills int not null,
    nexus_lost int not null,
    nexus_takedowns int not null,
    total_minions_killed int not null,
    neutral_minions_killed int not null,
    total_ally_jungle_minions_killed int not null,
    total_enemy_jungle_minions_killed int not null,
    total_damage_dealt int not null,
    total_damage_dealt_to_champions int not null,
    total_damage_shielded_on_teammates int not null,
    total_damage_taken int not null,
    total_heal int not null,
    total_heals_on_teammates int not null,
    largest_critical_strike int not null,
    longest_time_spent_living int not null,
    magic_damage_dealt int not null,
    magic_damage_dealt_to_champions int not null,
    magic_damage_taken int not null,
    true_damage_dealt int not null,
    true_damage_dealt_to_champions int not null,
    true_damage_taken int not null,
    objectives_stolen int not null,
    objectives_stolen_assists int not null,
    physical_damage_dealt int not null,
    physical_damage_dealt_to_champions int not null,
    physical_damage_taken int not null,
    time_cc_ing_others int not null,
    total_time_cc_dealt int not null,
    total_time_spent_dead int not null,
    total_units_healed int not null,
    
    vision_score int not null,
    detector_wards_placed int not null,
    wards_killed int not null,
    wards_placed int not null,

    all_in_pings int not null,
    assist_me_pings int not null,
    bait_pings int not null,
    basic_pings int not null,
    command_pings int not null,
    danger_pings int not null,
    enemy_missing_pings int not null,
    enemy_vision_pings int not null,
    get_back_pings int not null,
    hold_pings int not null,
    need_vision_pings int not null,
    on_my_way_pings int not null,
    push_pings int not null,
    vision_cleared_pings int not null,

    constraint match_participants_pk primary key (puuid, match_id),
    constraint match_participants_match_id foreign key (match_id) references matches(match_id),
    constraint match_participants_left_team foreign key (match_id, left_team) references match_teams(match_id, left_team),
    constraint match_participants_stat_perk_offensive foreign key (stat_perk_offensive) references stat_perks(id),
    constraint match_participants_stat_perk_flex foreign key (stat_perk_flex) references stat_perks(id),
    constraint match_participants_stat_perk_defensive foreign key (stat_perk_defensive) references stat_perks(id)
);