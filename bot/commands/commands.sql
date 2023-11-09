drop procedure if exists is_league_account_registered;
create procedure is_league_account_registered(in p_puuid varchar(78))
begin
    select exists(select * from registered_users users where users.puuid = p_puuid) from dual;
end;

drop procedure if exists register_user;
create procedure register_user(
    in p_puuid varchar(78),
    in p_account_id varchar(56),
    in p_summoner_id varchar(63),
    in p_game_name varchar(255),
    in p_tag_line varchar(255),
    in p_summoner_name varchar(255)
)
begin
    insert into registered_users (
        puuid,
        account_id,
        summoner_id,
        game_name,
        tag_line,
        summoner_name,
        has_been_initialized
    )
    values (
        p_puuid,
        p_account_id,
        p_summoner_id,
        p_game_name,
        p_tag_line,
        p_summoner_name,
        false
    );
end;

drop procedure if exists fetch_champion_play_time_data;
create procedure fetch_champion_play_time_data(in p_puuid varchar(78),in p_champion_id varchar(255))
begin
    select
        coalesce(sum(matches.game_duration), 0) as total_play_time_in_seconds,
        coalesce(sum(if(matches.game_mode = 'CLASSIC', matches.game_duration, 0)), 0) as rift_playtime_in_seconds,
        coalesce(sum(if(matches.game_mode = 'ARAM', matches.game_duration, 0)), 0) as aram_playtime_in_seconds,
        count(*) as total_amount,
        coalesce(sum(if(matches.game_mode = 'CLASSIC', 1, 0)), 0) as rift_amount,
        coalesce(sum(if(matches.game_mode = 'ARAM', 1, 0)), 0) as aram_amount,
        coalesce(min(matches.game_start_timestamp), current_timestamp) as first_played,
        coalesce(max(matches.game_start_timestamp), current_timestamp) as last_played
    from
        matches
        inner join match_participants participants on participants.match_id = matches.match_id
    where
        participants.puuid = p_puuid and participants.champion_id = p_champion_id;
end;

drop procedure if exists fetch_champion_plays_per_day;
create procedure fetch_champion_plays_per_day(in p_puuid varchar(78),in p_champion_id varchar(255))
begin
    select
        count(*) as amount,
        date(matches.game_start_timestamp) date
    from
        matches
        inner join match_participants participants on participants.match_id = matches.match_id
    where
        participants.puuid = p_puuid and champion_id = p_champion_id
    group by
        date
    order by
        date;
end;