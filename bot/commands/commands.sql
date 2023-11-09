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
