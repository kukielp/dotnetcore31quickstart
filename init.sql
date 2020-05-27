--Create a new sample databse and grant all privilgies
DROP DATABASE IF EXISTS posts;
CREATE DATABASE posts;
grant all privileges on database posts to postgres;