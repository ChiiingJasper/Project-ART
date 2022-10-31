window.onload = function () {

	// ---------------
	// basic usage
	// ---------------
	var $ = new City();
	$.showProvinces("#province");
	$.showCities("#city");

	// ------------------
	// additional methods 
	// -------------------

	// will return all provinces 
	console.log($.getProvinces());

	// will return all cities 
	console.log($.getAllCities());


}

window.onload = function () {

	var $ = new City();

	$.showProvinces('#province');

	$.showCities('#city');

}