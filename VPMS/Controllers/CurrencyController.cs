﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data.DBContext;
using System.Web;
using Humanizer;

namespace VPMSWeb.Controllers
{
	public class CurrencyController : Controller
	{
		private readonly CurrencyDBContext _currencyDBContext = new CurrencyDBContext();
		private readonly dynamic CountriesList = Host.CreateApplicationBuilder().Configuration.GetSection("Countries");
		private readonly dynamic TimezonesList = Host.CreateApplicationBuilder().Configuration.GetSection("Timezones");

		public void SetCurrentCurrency(string country)
		{
			var currency = _currencyDBContext.mst_currency.Where(x => x.Country == country).FirstOrDefault();

			//HttpContext.Session.SetString("Currency", currency != null ? currency.CurrencySymbol : "$");
		}

		public void SetCurrentCurrencyByCountryCode(string code)
		{
			//SetCurrentCurrency(Countries[code]);
			SetCurrentCurrency(CountriesList[code]);
		}

		public void SetCurrentCurrencyByTimezone(string timezone)
		{
			var countryCode = TimezonesList.GetSection(timezone).GetSection("c:0").Value;

			//SetCurrentCurrency(Countries[Timezones[timezone][0]]);
			SetCurrentCurrency(CountriesList[countryCode]);
		}


		//variables
		public Dictionary<string, string> Countries = new Dictionary<string, string>()
		{
				{"AD","Andorra"},
				{"AE","United Arab Emirates"},
				{"AF","Afghanistan"},
				{"AG","Antigua and Barbuda"},
				{"AI","Anguilla"},
				{"AL","Albania"},
				{"AM","Armenia"},
				{"AO","Angola"},
				{"AQ","Antarctica"},
				{"AR","Argentina"},
				{"AS","American Samoa"},
				{"AT","Austria"},
				{"AU","Australia"},
				{"AW","Aruba"},
				{"AX","Åland Islands"},
				{"AZ","Azerbaijan"},
				{"BA","Bosnia and Herzegovina"},
				{"BB","Barbados"},
				{"BD","Bangladesh"},
				{"BE","Belgium"},
				{"BF","Burkina Faso"},
				{"BG","Bulgaria"},
				{"BH","Bahrain"},
				{"BI","Burundi"},
				{"BJ","Benin"},
				{"BL","Saint Barthélemy"},
				{"BM","Bermuda"},
				{"BN","Brunei"},
				{"BO","Bolivia"},
				{"BQ","Caribbean Netherlands"},
				{"BR","Brazil"},
				{"BS","Bahamas"},
				{"BT","Bhutan"},
				{"BV","Bouvet Island"},
				{"BW","Botswana"},
				{"BY","Belarus"},
				{"BZ","Belize"},
				{"CA","Canada"},
				{"CC","Cocos Islands"},
				{"CD","Democratic Republic of the Congo"},
				{"CF","Central African Republic"},
				{"CG","Republic of the Congo"},
				{"CH","Switzerland"},
				{"CI","Ivory Coast"},
				{"CK","Cook Islands"},
				{"CL","Chile"},
				{"CM","Cameroon"},
				{"CN","China"},
				{"CO","Colombia"},
				{"CR","Costa Rica"},
				{"CU","Cuba"},
				{"CV","Cabo Verde"},
				{"CW","Curaçao"},
				{"CX","Christmas Island"},
				{"CY","Cyprus"},
				{"CZ","Czechia"},
				{"DE","Germany"},
				{"DJ","Djibouti"},
				{"DK","Denmark"},
				{"DM","Dominica"},
				{"DO","Dominican Republic"},
				{"DZ","Algeria"},
				{"EC","Ecuador"},
				{"EE","Estonia"},
				{"EG","Egypt"},
				{"EH","Western Sahara"},
				{"ER","Eritrea"},
				{"ES","Spain"},
				{"ET","Ethiopia"},
				{"FI","Finland"},
				{"FJ","Fiji"},
				{"FK","Falkland Islands"},
				{"FM","Micronesia"},
				{"FO","Faroe Islands"},
				{"FR","France"},
				{"GA","Gabon"},
				{"GB","United Kingdom"},
				{"GD","Grenada"},
				{"GE","Georgia"},
				{"GF","French Guiana"},
				{"GG","Guernsey"},
				{"GH","Ghana"},
				{"GI","Gibraltar"},
				{"GL","Greenland"},
				{"GM","Gambia"},
				{"GN","Guinea"},
				{"GP","Guadeloupe"},
				{"GQ","Equatorial Guinea"},
				{"GR","Greece"},
				{"GS","South Georgia and the South Sandwich Islands"},
				{"GT","Guatemala"},
				{"GU","Guam"},
				{"GW","Guinea-Bissau"},
				{"GY","Guyana"},
				{"HK","Hong Kong"},
				{"HM","Heard Island and McDonald Islands"},
				{"HN","Honduras"},
				{"HR","Croatia"},
				{"HT","Haiti"},
				{"HU","Hungary"},
				{"ID","Indonesia"},
				{"IE","Ireland"},
				{"IL","Israel"},
				{"IM","Isle of Man"},
				{"IN","India"},
				{"IO","British Indian Ocean Territory"},
				{"IQ","Iraq"},
				{"IR","Iran"},
				{"IS","Iceland"},
				{"IT","Italy"},
				{"JE","Jersey"},
				{"JM","Jamaica"},
				{"JO","Jordan"},
				{"JP","Japan"},
				{"KE","Kenya"},
				{"KG","Kyrgyzstan"},
				{"KH","Cambodia"},
				{"KI","Kiribati"},
				{"KM","Comoros"},
				{"KN","Saint Kitts and Nevis"},
				{"KP","North Korea"},
				{"KR","South Korea"},
				{"KW","Kuwait"},
				{"KY","Cayman Islands"},
				{"KZ","Kazakhstan"},
				{"LA","Laos"},
				{"LB","Lebanon"},
				{"LC","Saint Lucia"},
				{"LI","Liechtenstein"},
				{"LK","Sri Lanka"},
				{"LR","Liberia"},
				{"LS","Lesotho"},
				{"LT","Lithuania"},
				{"LU","Luxembourg"},
				{"LV","Latvia"},
				{"LY","Libya"},
				{"MA","Morocco"},
				{"MC","Monaco"},
				{"MD","Moldova"},
				{"ME","Montenegro"},
				{"MF","Saint Martin"},
				{"MG","Madagascar"},
				{"MH","Marshall Islands"},
				{"MK","North Macedonia"},
				{"ML","Mali"},
				{"MM","Myanmar"},
				{"MN","Mongolia"},
				{"MO","Macao"},
				{"MP","Northern Mariana Islands"},
				{"MQ","Martinique"},
				{"MR","Mauritania"},
				{"MS","Montserrat"},
				{"MT","Malta"},
				{"MU","Mauritius"},
				{"MV","Maldives"},
				{"MW","Malawi"},
				{"MX","Mexico"},
				{"MY","Malaysia"},
				{"MZ","Mozambique"},
				{"NA","Namibia"},
				{"NC","New Caledonia"},
				{"NE","Niger"},
				{"NF","Norfolk Island"},
				{"NG","Nigeria"},
				{"NI","Nicaragua"},
				{"NL","Netherlands"},
				{"NO","Norway"},
				{"NP","Nepal"},
				{"NR","Nauru"},
				{"NU","Niue"},
				{"NZ","New Zealand"},
				{"OM","Oman"},
				{"PA","Panama"},
				{"PE","Peru"},
				{"PF","French Polynesia"},
				{"PG","Papua New Guinea"},
				{"PH","Philippines"},
				{"PK","Pakistan"},
				{"PL","Poland"},
				{"PM","Saint Pierre and Miquelon"},
				{"PN","Pitcairn"},
				{"PR","Puerto Rico"},
				{"PS","Palestine"},
				{"PT","Portugal"},
				{"PW","Palau"},
				{"PY","Paraguay"},
				{"QA","Qatar"},
				{"RE","Réunion"},
				{"RO","Romania"},
				{"RS","Serbia"},
				{"RU","Russia"},
				{"RW","Rwanda"},
				{"SA","Saudi Arabia"},
				{"SB","Solomon Islands"},
				{"SC","Seychelles"},
				{"SD","Sudan"},
				{"SE","Sweden"},
				{"SG","Singapore"},
				{"SH","Saint Helena, Ascension and Tristan da Cunha"},
				{"SI","Slovenia"},
				{"SJ","Svalbard and Jan Mayen"},
				{"SK","Slovakia"},
				{"SL","Sierra Leone"},
				{"SM","San Marino"},
				{"SN","Senegal"},
				{"SO","Somalia"},
				{"SR","Suriname"},
				{"SS","South Sudan"},
				{"ST","Sao Tome and Principe"},
				{"SV","El Salvador"},
				{"SX","Sint Maarten"},
				{"SY","Syria"},
				{"SZ","Eswatini"},
				{"TC","Turks and Caicos Islands"},
				{"TD","Chad"},
				{"TF","French Southern Territories"},
				{"TG","Togo"},
				{"TH","Thailand"},
				{"TJ","Tajikistan"},
				{"TK","Tokelau"},
				{"TL","Timor-Leste"},
				{"TM","Turkmenistan"},
				{"TN","Tunisia"},
				{"TO","Tonga"},
				{"TR","Turkey"},
				{"TT","Trinidad and Tobago"},
				{"TV","Tuvalu"},
				{"TW","Taiwan"},
				{"TZ","Tanzania"},
				{"UA","Ukraine"},
				{"UG","Uganda"},
				{"UM","United States Minor Outlying Islands"},
				{"US","United States of America"},
				{"UY","Uruguay"},
				{"UZ","Uzbekistan"},
				{"VA","Holy See"},
				{"VC","Saint Vincent and the Grenadines"},
				{"VE","Venezuela"},
				{"VG","Virgin Islands (UK)"},
				{"VI","Virgin Islands (US)"},
				{"VN","Vietnam"},
				{"VU","Vanuatu"},
				{"WF","Wallis and Futuna"},
				{"WS","Samoa"},
				{"YE","Yemen"},
				{"YT","Mayotte"},
				{"ZA","South Africa"},
				{"ZM","Zambia"},
				{"ZW","Zimbabwe"}
		};

		public Dictionary<string, List<string>> Timezones = new Dictionary<string, List<string>>()
		{
			{"Africa/Abidjan",new List<string>(){"CI", "BF", "GH", "GM", "GN", "ML", "MR", "SH", "SL", "SN", "TG"} },
			{"Africa/Accra",new List<string>(){"GH"}},
			{"Africa/Addis_Ababa",new List<string>(){"ET"}},
			{"Africa/Algiers",new List<string>(){"DZ"}},
			{"Africa/Asmara",new List<string>(){"ER"}},
			{"Africa/Asmera",new List<string>(){"ER"}},
			{"Africa/Bamako",new List<string>(){"ML"}},
			{"Africa/Bangui",new List<string>(){"CF"}},
			{"Africa/Banjul",new List<string>(){"GM"}},
			{"Africa/Bissau",new List<string>(){"GW"}},
			{"Africa/Blantyre",new List<string>(){"MW"}},
			{"Africa/Brazzaville",new List<string>(){"CG"}},
			{"Africa/Bujumbura",new List<string>(){"BI"}},
			{"Africa/Cairo",new List<string>(){"EG"}},
			{"Africa/Conakry",new List<string>(){"GN"}},
			{"Africa/Dakar",new List<string>(){"SN"}},
			{"Africa/Dar_es_Salaam",new List<string>(){"TZ"}},
			{"Africa/Djibouti",new List<string>(){"DJ"}},
			{"Africa/Douala",new List<string>(){"CM"}},
			{"Africa/Freetown",new List<string>(){"SL"}},
			{"Africa/Gaborone",new List<string>(){"BW"}},
			{"Africa/Harare",new List<string>(){"ZW"}},
			{"Africa/Johannesburg",new List<string>(){"ZA", "LS", "SZ"}},
			{"Africa/Juba",new List<string>(){"SS"}},
			{"Africa/Kampala",new List<string>(){"UG"}},
			{"Africa/Khartoum",new List<string>(){"SD"}},
			{"Africa/Kigali",new List<string>(){"RW"}},
			{"Africa/Kinshasa",new List<string>(){"CD"}},
			{"Africa/Lagos",new List<string>(){"NG", "AO", "BJ", "CD", "CF", "CG", "CM", "GA", "GQ", "NE"}},
			{"Africa/Libreville",new List<string>(){"GA"}},
			{"Africa/Lome",new List<string>(){"TG"}},
			{"Africa/Luanda",new List<string>(){"AO"}},
			{"Africa/Lubumbashi",new List<string>(){"CD"}},
			{"Africa/Lusaka",new List<string>(){"ZM"}},
			{"Africa/Malabo",new List<string>(){"GQ"}},
			{"Africa/Maputo",new List<string>(){"MZ", "BI", "BW", "CD", "MW", "RW", "ZM", "ZW"}},
			{"Africa/Maseru",new List<string>(){"LS"}},
			{"Africa/Mbabane",new List<string>(){"SZ"}},
			{"Africa/Mogadishu",new List<string>(){"SO"}},
			{"Africa/Monrovia",new List<string>(){"LR"}},
			{"Africa/Nairobi",new List<string>(){"KE", "DJ", "ER", "ET", "KM", "MG", "SO", "TZ", "UG", "YT"}},
			{"Africa/Ndjamena",new List<string>(){"TD"}},
			{"Africa/Niamey",new List<string>(){"NE"}},
			{"Africa/Nouakchott",new List<string>(){"MR"}},
			{"Africa/Ouagadougou",new List<string>(){"BF"}},
			{"Africa/Porto-Novo",new List<string>(){"BJ"}},
			{"Africa/Sao_Tome",new List<string>(){"ST"}},
			{"Africa/Timbuktu",new List<string>(){"ML"}},
			{"Africa/Tripoli",new List<string>(){"LY"}},
			{"Africa/Tunis",new List<string>(){"TN"}},
			{"Africa/Windhoek",new List<string>(){"NA"}},
			{"America/Anguilla",new List<string>(){"AI"}},
			{"America/Antigua",new List<string>(){"AG"}},
			{"America/Araguaina",new List<string>(){"BR"}},
			{"America/Argentina/Buenos_Aires",new List<string>(){"AR"}},
			{"America/Argentina/Catamarca",new List<string>(){"AR"}},
			{"America/Argentina/Cordoba",new List<string>(){"AR"}},
			{"America/Argentina/Jujuy",new List<string>(){"AR"}},
			{"America/Argentina/La_Rioja",new List<string>(){"AR"}},
			{"America/Argentina/Mendoza",new List<string>(){"AR"}},
			{"America/Argentina/Rio_Gallegos",new List<string>(){"AR"}},
			{"America/Argentina/Salta",new List<string>(){"AR"}},
			{"America/Argentina/San_Juan",new List<string>(){"AR"}},
			{"America/Argentina/San_Luis",new List<string>(){"AR"}},
			{"America/Argentina/Tucuman",new List<string>(){"AR"}},
			{"America/Argentina/Ushuaia",new List<string>(){"AR"}},
			{"America/Aruba",new List<string>(){"AW"}},
			{"America/Atikokan",new List<string>(){"CA"}},
			{"America/Bahia",new List<string>(){"BR"}},
			{"America/Barbados",new List<string>(){"BB"}},
			{"America/Belem",new List<string>(){"BR"}},
			{"America/Belize",new List<string>(){"BZ"}},
			{"America/Blanc-Sablon",new List<string>(){"CA"}},
			{"America/Boa_Vista",new List<string>(){"BR"}},
			{"America/Bogota",new List<string>(){"CO"}},
			{"America/Campo_Grande",new List<string>(){"BR"}},
			{"America/Cancun",new List<string>(){"MX"}},
			{"America/Caracas",new List<string>(){"VE"}},
			{"America/Cayenne",new List<string>(){"GF"}},
			{"America/Cayman",new List<string>(){"KY"}},
			{"America/Coral_Harbour",new List<string>(){"CA"}},
			{"America/Costa_Rica",new List<string>(){"CR"}},
			{"America/Creston",new List<string>(){"CA"}},
			{"America/Cuiaba",new List<string>(){"BR"}},
			{"America/Curacao",new List<string>(){"CW"}},
			{"America/Danmarkshavn",new List<string>(){"GL"}},
			{"America/Dawson",new List<string>(){"CA"}},
			{"America/Dawson_Creek",new List<string>(){"CA"}},
			{"America/Dominica",new List<string>(){"DM"}},
			{"America/Eirunepe",new List<string>(){"BR"}},
			{"America/El_Salvador",new List<string>(){"SV"}},
			{"America/Fort_Nelson",new List<string>(){"CA"}},
			{"America/Fortaleza",new List<string>(){"BR"}},
			{"America/Grenada",new List<string>(){"GD"}},
			{"America/Guadeloupe",new List<string>(){"GP"}},
			{"America/Guatemala",new List<string>(){"GT"}},
			{"America/Guayaquil",new List<string>(){"EC"}},
			{"America/Guyana",new List<string>(){"GY"}},
			{"America/Hermosillo",new List<string>(){"MX"}},
			{"America/Jamaica",new List<string>(){"JM"}},
			{"America/Kralendijk",new List<string>(){"BQ"}},
			{"America/La_Paz",new List<string>(){"BO"}},
			{"America/Lima",new List<string>(){"PE"}},
			{"America/Lower_Princes",new List<string>(){"SX"}},
			{"America/Maceio",new List<string>(){"BR"}},
			{"America/Managua",new List<string>(){"NI"}},
			{"America/Manaus",new List<string>(){"BR"}},
			{"America/Marigot",new List<string>(){"MF"}},
			{"America/Martinique",new List<string>(){"MQ"}},
			{"America/Montevideo",new List<string>(){"UY"}},
			{"America/Montreal",new List<string>(){"CA"}},
			{"America/Montserrat",new List<string>(){"MS"}},
			{"America/Nassau",new List<string>(){"BS"}},
			{"America/Noronha",new List<string>(){"BR"}},
			{"America/Panama",new List<string>(){"PA", "CA", "KY"}},
			{"America/Paramaribo",new List<string>(){"SR"}},
			{"America/Phoenix",new List<string>(){"US", "CA"}},
			{"America/Port_of_Spain",new List<string>(){"TT"}},
			{"America/Porto_Velho",new List<string>(){"BR"}},
			{"America/Punta_Arenas",new List<string>(){"CL"}},
			{"America/Recife",new List<string>(){"BR"}},
			{"America/Regina",new List<string>(){"CA"}},
			{"America/Rio_Branco",new List<string>(){"BR"}},
			{"America/Santarem",new List<string>(){"BR"}},
			{"America/Santo_Domingo",new List<string>(){"DO"}},
			{"America/Sao_Paulo",new List<string>(){"BR"}},
			{"America/St_Barthelemy",new List<string>(){"BL"}},
			{"America/St_Kitts",new List<string>(){"KN"}},
			{"America/St_Lucia",new List<string>(){"LC"}},
			{"America/St_Thomas",new List<string>(){"VI"}},
			{"America/St_Vincent",new List<string>(){"VC"}},
			{"America/Swift_Current",new List<string>(){"CA"}},
			{"America/Tegucigalpa",new List<string>(){"HN"}},
			{"America/Tortola",new List<string>(){"VG"}},
			{"America/Virgin",new List<string>(){"VI"}},
			{"America/Whitehorse",new List<string>(){"CA"}},
			{"Antarctica/Casey",new List<string>(){"AQ"}},
			{"Antarctica/Davis",new List<string>(){"AQ"}},
			{"Antarctica/DumontDUrville",new List<string>(){"AQ"}},
			{"Antarctica/Mawson",new List<string>(){"AQ"}},
			{"Antarctica/McMurdo",new List<string>(){"AQ"}},
			{"Antarctica/Palmer",new List<string>(){"AQ"}},
			{"Antarctica/Rothera",new List<string>(){"AQ"}},
			{"Antarctica/South_Pole",new List<string>(){"AQ"}},
			{"Antarctica/Syowa",new List<string>(){"AQ"}},
			{"Antarctica/Vostok",new List<string>(){"AQ"}},
			{"Arctic/Longyearbyen",new List<string>(){"SJ"}},
			{"Asia/Aden",new List<string>(){"YE"}},
			{"Asia/Almaty",new List<string>(){"KZ"}},
			{"Asia/Anadyr",new List<string>(){"RU"}},
			{"Asia/Aqtau",new List<string>(){"KZ"}},
			{"Asia/Aqtobe",new List<string>(){"KZ"}},
			{"Asia/Ashgabat",new List<string>(){"TM"}},
			{"Asia/Atyrau",new List<string>(){"KZ"}},
			{"Asia/Baghdad",new List<string>(){"IQ"}},
			{"Asia/Bahrain",new List<string>(){"BH"}},
			{"Asia/Baku",new List<string>(){"AZ"}},
			{"Asia/Bangkok",new List<string>(){"TH", "KH", "LA", "VN"}},
			{"Asia/Barnaul",new List<string>(){"RU"}},
			{"Asia/Bishkek",new List<string>(){"KG"}},
			{"Asia/Brunei",new List<string>(){"BN"}},
			{"Asia/Chita",new List<string>(){"RU"}},
			{"Asia/Choibalsan",new List<string>(){"MN"}},
			{"Asia/Colombo",new List<string>(){"LK"}},
			{"Asia/Dhaka",new List<string>(){"BD"}},
			{"Asia/Dili",new List<string>(){"TL"}},
			{"Asia/Dubai",new List<string>(){"AE", "OM"}},
			{"Asia/Dushanbe",new List<string>(){"TJ"}},
			{"Asia/Ho_Chi_Minh",new List<string>(){"VN"}},
			{"Asia/Hong_Kong",new List<string>(){"HK"}},
			{"Asia/Hovd",new List<string>(){"MN"}},
			{"Asia/Irkutsk",new List<string>(){"RU"}},
			{"Asia/Jakarta",new List<string>(){"ID"}},
			{"Asia/Jayapura",new List<string>(){"ID"}},
			{"Asia/Kabul",new List<string>(){"AF"}},
			{"Asia/Kamchatka",new List<string>(){"RU"}},
			{"Asia/Karachi",new List<string>(){"PK"}},
			{"Asia/Kathmandu",new List<string>(){"NP"}},
			{"Asia/Khandyga",new List<string>(){"RU"}},
			{"Asia/Kolkata",new List<string>(){"IN"}},
			{"Asia/Krasnoyarsk",new List<string>(){"RU"}},
			{"Asia/Kuala_Lumpur",new List<string>(){"MY"}},
			{"Asia/Kuching",new List<string>(){"MY"}},
			{"Asia/Kuwait",new List<string>(){"KW"}},
			{"Asia/Macau",new List<string>(){"MO"}},
			{"Asia/Magadan",new List<string>(){"RU"}},
			{"Asia/Makassar",new List<string>(){"ID"}},
			{"Asia/Manila",new List<string>(){"PH"}},
			{"Asia/Muscat",new List<string>(){"OM"}},
			{"Asia/Novokuznetsk",new List<string>(){"RU"}},
			{"Asia/Novosibirsk",new List<string>(){"RU"}},
			{"Asia/Omsk",new List<string>(){"RU"}},
			{"Asia/Oral",new List<string>(){"KZ"}},
			{"Asia/Phnom_Penh",new List<string>(){"KH"}},
			{"Asia/Pontianak",new List<string>(){"ID"}},
			{"Asia/Pyongyang",new List<string>(){"KP"}},
			{"Asia/Qatar",new List<string>(){"QA", "BH"}},
			{"Asia/Qostanay",new List<string>(){"KZ"}},
			{"Asia/Qyzylorda",new List<string>(){"KZ"}},
			{"Asia/Riyadh",new List<string>(){"SA", "AQ", "KW", "YE"}},
			{"Asia/Sakhalin",new List<string>(){"RU"}},
			{"Asia/Samarkand",new List<string>(){"UZ"}},
			{"Asia/Seoul",new List<string>(){"KR"}},
			{"Asia/Shanghai",new List<string>(){"CN"}},
			{"Asia/Singapore",new List<string>(){"SG", "MY"}},
			{"Asia/Srednekolymsk",new List<string>(){"RU"}},
			{"Asia/Taipei",new List<string>(){"TW"}},
			{"Asia/Tashkent",new List<string>(){"UZ"}},
			{"Asia/Tbilisi",new List<string>(){"GE"}},
			{"Asia/Thimphu",new List<string>(){"BT"}},
			{"Asia/Tokyo",new List<string>(){"JP"}},
			{"Asia/Tomsk",new List<string>(){"RU"}},
			{"Asia/Ulaanbaatar",new List<string>(){"MN"}},
			{"Asia/Urumqi",new List<string>(){"CN"}},
			{"Asia/Ust-Nera",new List<string>(){"RU"}},
			{"Asia/Vientiane",new List<string>(){"LA"}},
			{"Asia/Vladivostok",new List<string>(){"RU"}},
			{"Asia/Yakutsk",new List<string>(){"RU"}},
			{"Asia/Yangon",new List<string>(){"MM"}},
			{"Asia/Yekaterinburg",new List<string>(){"RU"}},
			{"Asia/Yerevan",new List<string>(){"AM"}},
			{"Atlantic/Cape_Verde",new List<string>(){"CV"}},
			{"Atlantic/Jan_Mayen",new List<string>(){"SJ"}},
			{"Atlantic/Reykjavik",new List<string>(){"IS"}},
			{"Atlantic/South_Georgia",new List<string>(){"GS"}},
			{"Atlantic/St_Helena",new List<string>(){"SH"}},
			{"Atlantic/Stanley",new List<string>(){"FK"}},
			{"Australia/Brisbane",new List<string>(){"AU"}},
			{"Australia/Darwin",new List<string>(){"AU"}},
			{"Australia/Eucla",new List<string>(){"AU"}},
			{"Australia/Lindeman",new List<string>(){"AU"}},
			{"Australia/Perth",new List<string>(){"AU"}},
			{"Canada/Eastern",new List<string>(){"CA"}},
			{"Europe/Astrakhan",new List<string>(){"RU"}},
			{"Europe/Belfast",new List<string>(){"GB"}},
			{"Europe/Bratislava",new List<string>(){"SK"}},
			{"Europe/Busingen",new List<string>(){"DE"}},
			{"Europe/Guernsey",new List<string>(){"GG"}},
			{"Europe/Isle_of_Man",new List<string>(){"IM"}},
			{"Europe/Istanbul",new List<string>(){"TR"}},
			{"Europe/Jersey",new List<string>(){"JE"}},
			{"Europe/Kaliningrad",new List<string>(){"RU"}},
			{"Europe/Kirov",new List<string>(){"RU"}},
			{"Europe/Ljubljana",new List<string>(){"SI"}},
			{"Europe/Mariehamn",new List<string>(){"AX"}},
			{"Europe/Minsk",new List<string>(){"BY"}},
			{"Europe/Moscow",new List<string>(){"RU"}},
			{"Europe/Podgorica",new List<string>(){"ME"}},
			{"Europe/Samara",new List<string>(){"RU"}},
			{"Europe/San_Marino",new List<string>(){"SM"}},
			{"Europe/Sarajevo",new List<string>(){"BA"}},
			{"Europe/Saratov",new List<string>(){"RU"}},
			{"Europe/Simferopol",new List<string>(){"RU", "UA"}},
			{"Europe/Skopje",new List<string>(){"MK"}},
			{"Europe/Ulyanovsk",new List<string>(){"RU"}},
			{"Europe/Vaduz",new List<string>(){"LI"}},
			{"Europe/Vatican",new List<string>(){"VA"}},
			{"Europe/Volgograd",new List<string>(){"RU"}},
			{"Europe/Zagreb",new List<string>(){"HR"}},
			{"GB",new List<string>(){"GB"}},
			{"GB-Eire",new List<string>(){"GB"}},
			{"Indian/Antananarivo",new List<string>(){"MG"}},
			{"Indian/Chagos",new List<string>(){"IO"}},
			{"Indian/Christmas",new List<string>(){"CX"}},
			{"Indian/Cocos",new List<string>(){"CC"}},
			{"Indian/Comoro",new List<string>(){"KM"}},
			{"Indian/Kerguelen",new List<string>(){"TF", "HM"}},
			{"Indian/Mahe",new List<string>(){"SC"}},
			{"Indian/Maldives",new List<string>(){"MV"}},
			{"Indian/Mauritius",new List<string>(){"MU"}},
			{"Indian/Mayotte",new List<string>(){"YT"}},
			{"Indian/Reunion",new List<string>(){"RE", "TF"}},
			{"NZ",new List<string>(){"NZ"}},
			{"Pacific/Apia",new List<string>(){"WS"}},
			{"Pacific/Bougainville",new List<string>(){"PG"}},
			{"Pacific/Chuuk",new List<string>(){"FM"}},
			{"Pacific/Efate",new List<string>(){"VU"}},
			{"Pacific/Fakaofo",new List<string>(){"TK"}},
			{"Pacific/Funafuti",new List<string>(){"TV"}},
			{"Pacific/Galapagos",new List<string>(){"EC"}},
			{"Pacific/Gambier",new List<string>(){"PF"}},
			{"Pacific/Guadalcanal",new List<string>(){"SB"}},
			{"Pacific/Guam",new List<string>(){"GU", "MP"}},
			{"Pacific/Honolulu",new List<string>(){"US", "UM"}},
			{"Pacific/Johnston",new List<string>(){"UM"}},
			{"Pacific/Kanton",new List<string>(){"KI"}},
			{"Pacific/Kiritimati",new List<string>(){"KI"}},
			{"Pacific/Kosrae",new List<string>(){"FM"}},
			{"Pacific/Kwajalein",new List<string>(){"MH"}},
			{"Pacific/Majuro",new List<string>(){"MH"}},
			{"Pacific/Marquesas",new List<string>(){"PF"}},
			{"Pacific/Midway",new List<string>(){"UM"}},
			{"Pacific/Nauru",new List<string>(){"NR"}},
			{"Pacific/Niue",new List<string>(){"NU"}},
			{"Pacific/Noumea",new List<string>(){"NC"}},
			{"Pacific/Pago_Pago",new List<string>(){"AS", "UM"}},
			{"Pacific/Palau",new List<string>(){"PW"}},
			{"Pacific/Pitcairn",new List<string>(){"PN"}},
			{"Pacific/Pohnpei",new List<string>(){"FM"}},
			{"Pacific/Port_Moresby",new List<string>(){"PG", "AQ"}},
			{"Pacific/Rarotonga",new List<string>(){"CK"}},
			{"Pacific/Saipan",new List<string>(){"MP"}},
			{"Pacific/Samoa",new List<string>(){"WS"}},
			{"Pacific/Tahiti",new List<string>(){"PF"}},
			{"Pacific/Tarawa",new List<string>(){"KI"}},
			{"Pacific/Tongatapu",new List<string>(){"TO"}},
			{"Pacific/Wake",new List<string>(){"UM"}},
			{"Pacific/Wallis",new List<string>(){"WF"}},
			{"Singapore",new List<string>(){"SG"}},
			{"US/Arizona",new List<string>(){"US"}},
			{"US/Hawaii",new List<string>(){"US"}},
			{"US/Samoa",new List<string>(){"WS"}},


		};
	}

}
