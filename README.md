# job-portal
Semestral project for course FI:PV226 Seminar of Lab of Software Architectures and Information Systems

# Assignment in Czech

*Systém bude umožňovať*:
* Firmám (po prihlásení) vytvárať pracovné ponuky. Pre každú pracovnú ponuku musia firmy vyplniť základné informácie (typ práce, lokalita, ...) a zadať otázky na uchádzačov (Stačí aby boli textové, napríklad "Prečo ste vhodný kandidát?" a pod.).
* Neregistrovaným uchádzačom prezerať pracovné ponuky a prihlásiť sa na konkrétnu ponuku. Uchádzač musí vyplniť svoje základné údaje (meno, email, vzdelanie, ...), ktoré sú pre každú pracovnú ponuku rovnaké a potom zodpovedať špeciálne otázky na uchádzačov pre konkrétnu pozíciu.
* Registrovať sa ako uchádzač. Nový uchádzač zadá svoje základné údaje (meno, email, vzdelanie, ...), ktoré sú vyžadované pre každú ponuku. Keď sa teraz uchádzač prihlási na pracovnú ponuku, nemusí ich už znova zadávať, automaticky sa mu predvyplnia. Stále však musí zodpovedať špeciálne otázky pre konkrétnu pozíciu.
* Firmám zobraziť si všetky svoje pracovné ponuky a prihlásených uchádzačov. Každá prihláška na pracovnú pozíciu má 3 možné stavy - nerozhodnuté/prijatý/neprijatý. Uchádzača vie buď prijať alebo odmietnúť.
* Registrovanému uchádzačovi pozrieť si všetky pozície, na ktoré sa prihlásil a či bol na ne prijatý
* Cez verejné API získavať všetky pracovné ponuky

*Príklad možného rozšírenia*: _Schopnosti_
* Firma pri zadávaní pracovných ponúk vyplní schopnosti ktoré sú požadované (stačí vo forme tagov napr. "C#", "ASP.NET MVC", "HTML"). Vyhľadávanie ponúk je potom možné obmedziť podľa schopností.
* Uchádzač pri registrácii do systému zadá svoje schopnosti. Systém mu vie podľa toho navrhovať najvhodnejšie pracovné pozície.
