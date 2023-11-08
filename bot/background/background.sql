drop procedure if exists get_not_initialized_player_puuids;
create procedure get_not_initialized_player_puuids()
begin
    select
        users.puuid
    from
        registered_users users
    where
        users.has_been_initialized = false;
end;

drop procedure if exists insert_match_ids;
create procedure insert_match_ids(in p_match_ids text)
begin
    declare v_pos int;
    declare v_nextpos int;
    declare v_item varchar(255);

    set v_pos = 1;

    while v_pos < length(p_match_ids) do
        set v_nextpos = if(locate(',', p_match_ids, v_pos) > 0, locate(',', p_match_ids, v_pos), length(p_match_ids) + 1);
        set v_item = substring(p_match_ids, v_pos, v_nextpos - v_pos);

        insert ignore into match_ids (id) values (v_item);

        set v_pos = v_nextpos + 1;
    end while;
end;

drop procedure if exists mark_player_as_initialized;
create procedure mark_player_as_initialized(in p_puuid varchar(79))
begin
    update 
        registered_users 
    set 
        has_been_initialized = true,
        last_matches_check = current_timestamp
    where 
        puuid = p_puuid;
end;

drop procedure if exists get_daily_users_data_fetch;
create procedure get_daily_users_data_fetch()
begin
    select
        users.puuid
    from
        registered_users users
    where
        users.has_been_initialized = true and (users.last_matches_check + interval 1 day) <= current_timestamp;
end;

drop procedure if exists mark_player_as_completed_daily_data_fetch;
create procedure mark_player_as_completed_daily_data_fetch(in p_puuid varchar(79))
begin
    update registered_users set last_matches_check = current_timestamp where puuid = p_puuid;
end;

drop procedure if exists fetch_match_ids_with_no_data;
create procedure fetch_match_ids_with_no_data()
begin
    select
        match_ids.id
    from
        match_ids
        left join match_participants participants on participants.match_id = match_ids.id
    where
        participants.match_id is null;
end;

drop procedure if exists insert_match_data;
create procedure insert_match_data(
    in p_match_id varchar(255),
    in p_game_creation_timestamp timestamp,
    in p_game_start_timestamp timestamp,
    in p_game_end_timestamp timestamp,
    in p_game_duration int,
    in p_game_mode varchar(100),
    in p_game_type varchar(100),
    in p_map_id int
)
begin
    insert into matches (
        match_id,
        game_creation_timestamp,
        game_start_timestamp,
        game_end_timestamp,
        game_duration,
        game_mode,
        game_type,
        map_id
    ) values (
        p_match_id,
        p_game_creation_timestamp,
        p_game_start_timestamp,
        p_game_end_timestamp,
        p_game_duration,
        p_game_mode,
        p_game_type,
        p_map_id
    );
end;

drop procedure if exists insert_match_team_data;
create procedure insert_match_team_data(
    in p_match_id varchar(255),
    in p_left_team bool,
    in p_win bool,
    in p_champion_id_ban_1 int,
    in p_champion_id_ban_2 int,
    in p_champion_id_ban_3 int,
    in p_champion_id_ban_4 int,
    in p_champion_id_ban_5 int,
    in p_first_to_take_baron bool,
    in p_first_to_take_champion bool,
    in p_first_to_take_dragon bool,
    in p_first_to_take_inhibitor bool,
    in p_first_to_take_rift_herald bool,
    in p_first_to_take_tower bool,
    in p_baron_amount int,
    in p_champion_amount int,
    in p_dragon_amount int,
    in p_inhibitor_amount int,
    in p_rift_herald_amount int,
    in p_tower_amount int
)
begin
    insert into match_teams (
        match_id,
        left_team,
        win,
        champion_id_ban_1,
        champion_id_ban_2,
        champion_id_ban_3,
        champion_id_ban_4,
        champion_id_ban_5,
        first_to_take_baron,
        first_to_take_champion,
        first_to_take_dragon,
        first_to_take_inhibitor,
        first_to_take_rift_herald,
        first_to_take_tower,
        baron_amount,
        champion_amount,
        dragon_amount,
        inhibitor_amount,
        rift_herald_amount,
        tower_amount
    ) values (
        p_match_id,
        p_left_team,
        p_win,
        p_champion_id_ban_1,
        p_champion_id_ban_2,
        p_champion_id_ban_3,
        p_champion_id_ban_4,
        p_champion_id_ban_5,
        p_first_to_take_baron,
        p_first_to_take_champion,
        p_first_to_take_dragon,
        p_first_to_take_inhibitor,
        p_first_to_take_rift_herald,
        p_first_to_take_tower,
        p_baron_amount,
        p_champion_amount,
        p_dragon_amount,
        p_inhibitor_amount,
        p_rift_herald_amount,
        p_tower_amount
    );
end;