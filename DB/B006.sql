
ALTER TABLE 
Libraries ADD
Domain NVARCHAR(200);

update Libraries set Domain = 'bibliotecaFloresti.ddns.net'
where id = 1

update Libraries set Domain = 'bibliotecaTasnad.ddns.net'
where id = 3