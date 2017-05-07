	use bibliotecaTasnad

alter table Libraries
Add 
	[Description] nvarchar(MAX) null 

alter table Libraries
Add Contact nvarchar(MAX)  COLLATE SQL_Romanian_CP1250_CI_AS null



	Update Libraries 
	set [Description] = '<p>Despre activitatea bibliotecii publice, până în anul 1952, ştirile sunt relativ reduse, deoarece arhivele au fost ditruse.</p>
<p>Din ”Historia Dosmus” aflată la parohia romano-catolică din localitate, reiese că, în anul 1908 exista o bibliotecă al cărui fond era înregistrat întrun registru inventar iar cărţile erau aranjate după un catalog alfabetic existent.</p>
<p>
    Biblioteca „Clubului Tineretului” a funcţionat în clădirea Casei de Cultură iar fondul de carte se îmbogăţea nu numai prin repartiţie ci şi prin sumele încasate în urma unor spectacole date, cât şi din „taxa de carte” (1 leu pentru o carte împrumutată), ceea ce a determinat să existe un fond de cărţi în jurul a 3000-4000 de volume în preajma celui de-al doilea război mondial când se încheie prima perioadă de activitate din istoria bibliotecii.
 În 1952 biblioteca îşi reia activitatea ca Bibliotecă Raională cu 3 bibliotecari salariaţi.
</p>
<p>Din 15.02.1961 până în 1.06.1968, biblioteca funcţionează ca bibliotecă comunală, iar din anul 1968, când Tăşnadul a devenit oraş, biblioteca funcţionează cu denumirea de Bibliotecă Orăşenească, având 2 salariaţi.</p>
<p>Clădire reabilitată în anul 2012 prin Programul naţional pentru reabilitarea, modernizarea şi dotarea aşezămintelor culturale din mediul mic urban.</p>
<p>Biblioteca are o sală de lectură cu 4 locuri, oferă împrumut la domiciliu şi interbibliotecar, are catalog în format tradiţional, 4 calculatoare cu acces la internet, oferă referinţe bibliografice şi bibliografii, referinţe pe email, sesiuni de instruire a utilizatorilor, rezervări de publicaţii, programe şi evenimente culturale, expoziţii.</p>
',
Contact = '
<address>
    <strong>Facebook:</strong> <a href="https://www.facebook.com/www.reed.ro/" target="_blank"> Dând click aici </a><br /><br />
    <strong>Inţiator proiect: Roxana Liliana Chioran</strong><br />
    <strong>Inţiator proiect: Adrian Nagy:</strong>   <a href="#"> adrian1nagy@yahoo.com </a><br />
    <p>Adresă:
        STR. ÎNFRĂŢIRII, Nr. 42, TĂŞNAD, Jud. SATU-MARE
    </p>
    <p>
        Telefon: 
        0261-825 488
    </p>
</address>'
where id =1