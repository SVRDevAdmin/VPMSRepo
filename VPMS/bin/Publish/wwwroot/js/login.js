SetCurrentCurrencyByTimezone();
// SetCurrentCurrencyByCountryCode("MY");

// $.get("https://ipinfo.io/json", function (response) {
// 	SetCurrentCurrencyByCountryCode(response.country);
// }, "jsonp");

function SetCurrentCurrencyByCountryCode(code) {
	fetch('/Currency/SetCurrentCurrencyByCountryCode?code=' + code)
		.then(response => {
			if (!response.ok) {
				throw new Error('Network response was not ok');
			}
		})
		.catch(error => {
			console.error('There was a problem with the fetch operation:', error);
		});
}

function SetCurrentCurrencyByTimezone() {
	var timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
	fetch('/Currency/SetCurrentCurrencyByTimezone?timezone=' + timezone)
		.then(response => {
			if (!response.ok) {
				throw new Error('Network response was not ok');
			}
		})
		.catch(error => {
			console.error('There was a problem with the fetch operation:', error);
		});
}

// fetch('https://api.ipregistry.co/?key=tryout')
// 	.then(function (response) {
// 		return response.json();
// 	})
// 	.then(function (payload) {
// 		setCurrency(payload.location.country.name);
// 	});

function setCurrency(country) {
	fetch('/Currency/SetCurrentCurrency?country=' + country)
		.then(response => {
			if (!response.ok) {
				throw new Error('Network response was not ok');
			}
		})
		.catch(error => {
			console.error('There was a problem with the fetch operation:', error);
		});
}

var country = getCountry();
var state = getState();

// alert("Country : " + country + ", State : " + state);

// setCurrency(country);