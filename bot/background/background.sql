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