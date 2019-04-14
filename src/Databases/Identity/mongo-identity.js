db.createCollection('Users')
db.Users.insertMany([{'Username':'Admin','Salt':'*','Hash':'*'}])
db.Users.createIndex( { 'Username': 1 }, { unique: true } )
