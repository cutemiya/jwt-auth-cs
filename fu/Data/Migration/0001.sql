create table Accounts (
    id serial primary key,
    email text not null,
    password text not null,
    username text not null,
    role text not null,
    refresh_token text not null,
    refresh_token_expired_time text not null
)