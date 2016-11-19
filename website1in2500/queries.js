var promise = require('bluebird');

var options = {
    // Initialization Options
    promiseLib: promise
};

var pgp = require('pg-promise')(options);
pgp.pg.defaults.ssl = true;
//postgres://tvkkymmzveumqn:FbKUj0a9eNPfYZXoqyWrZ_unkw@ec2-54-217-233-192.eu-west-1.compute.amazonaws.com:5432/d2u4fcq0idqldbHeroku CLI
var connectionString = 'postgres://tvkkymmzveumqn:FbKUj0a9eNPfYZXoqyWrZ_unkw@ec2-54-217-233-192.eu-west-1.compute.amazonaws.com:5432/d2u4fcq0idqldb' || 'postgres://localhost:5432/players';
var db = pgp(connectionString);

// add query functions

module.exports = {
    getAllPlayers: getAllPlayers,
    getSinglePlayer: getSinglePlayer,
    createPlayer: createPlayer,
    createScore: createScore
    //removePlayer: removePlayer
};

function getAllPlayers(req, res, next) {
    //db.any('select p.id,p.name,p.age,p.sex, s.points from players as p, scores as s where p.id = s.id')
    db.any('select id,name,age,sex, points, timeused  from players NATURAL JOIN scores')
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
    db.many('select id,name,age,sex, points, timeused from players NATURAL JOIN scores where id = $1', playerID)
        .then(function (data) {
            res.status(200)
                .json({
                    status: 'success',
                    data: data,
                    message: 'Retrieved player score(s)'
                });
        })
        .catch(function (err) {

            return next(err);
        });
}


function createPlayer(req, res, next) {
    req.body.age = parseInt(req.body.age);
    req.body.points = parseInt(req.body.points);
    req.body.timeused = parseFloat(req.body.timeused);
    db.one('insert into players(name, age, sex)' +
        'values(${name}, ${age}, ${sex}) returning id',
        req.body)
        .then(function (data) {
            console.log(parseInt(data.id));
            db.none('insert into scores(id, points, timeused)' +
                'values('+parseInt(data.id)+', ${points}, ${timeused})',
                req.body)
                .then(function () {
                    res.status(200)
                        .json({
                            status: 'success',
                            data: data,
                            message: 'Inserted player and score'
                        });
                })
                .catch(function (err) {
                    return next(err);
                });
        });

}

function createScore(req, res, next) {
    req.body.pid = parseInt(req.body.pid);
    req.body.points = parseInt(req.body.points);
    req.body.timeused = parseFloat(req.body.timeused);
    db.none('insert into scores(id, points, timeused)' +
        'values(${pid}, ${points}, ${timeused})',
        req.body)
        .then(function () {
            res.status(200)
                .json({
                    status: 'success',
                    message: 'Updated score'
                });
        })
        .catch(function (err) {
            return next(err);
        });
}

