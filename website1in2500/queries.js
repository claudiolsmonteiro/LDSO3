var promise = require('bluebird');

var options = {
    // Initialization Options
    promiseLib: promise
};

var pgp = require('pg-promise')(options);
pgp.pg.defaults.ssl = true;
//postgres://tvkkymmzveumqn:FbKUj0a9eNPfYZXoqyWrZ_unkw@ec2-54-217-233-192.eu-west-1.compute.amazonaws.com:5432/d2u4fcq0idqldbHeroku CLI
var connectionString = 'postgres://tvkkymmzveumqn:FbKUj0a9eNPfYZXoqyWrZ_unkw@ec2-54-217-233-192.eu-west-1.compute.amazonaws.com:5432/d2u4fcq0idqldb' || 'postgres://localhost:5432/puppies';
var db = pgp(connectionString);

// add query functions

module.exports = {
    getAllPlayers: getAllPlayers,
    getSinglePlayer: getSinglePlayer
    //createPlayer: createPlayer,
    //updatePlayer: updatePlayer,
    //removePlayer: removePlayer
};

function getAllPlayers(req, res, next) {
    //db.any('select p.id,p.name,p.age,p.sex, s.points from players as p, scores as s where p.id = s.id')
    db.any('select id,name,age,sex, points from players NATURAL JOIN scores')
        .then(function (data) {
            res.status(200)
                .json({
                    status: 'success',
                    data: data,
                    message: 'Retrieved ALL players'
                });
        })
        .catch(function (err) {
            return next(err);
        });
}

function getSinglePlayer(req, res, next) {
    var playerID = parseInt(req.params.id);
    db.many('select id,name,age,sex, points from players NATURAL JOIN scores where id = $1', playerID)
        .then(function (data) {
            res.status(200)
                .json({
                    status: 'success',
                    data: data,
                    message: 'Retrieved ONE player'
                });
        })
        .catch(function (err) {
            return next(err);
        });
}

/*
function createPlayer(req, res, next) {
    req.body.age = parseInt(req.body.age);
    db.none('insert into players(name, age, sex)' +
        'values(${name},${age}, ${sex})',
        req.body)
        .then(function () {
            res.status(200)
                .json({
                    status: 'success',
                    message: 'Inserted one player'
                });
        })
        .catch(function (err) {
            return next(err);
        });
}


*/