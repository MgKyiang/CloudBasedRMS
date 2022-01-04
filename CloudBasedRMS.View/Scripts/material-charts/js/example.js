$(document).ready(function() {
	var exampleBarChartData = {
		"datasets": {
			"values": [5, 10, 30, 50, 20,60],
			"labels": [
				"Apples", 
				"Oranges", 
				"Berries", 
				"Peaches", 
				"Bananas",
                 "Stabeery"
			],
			"color": "blue"
		},
		"title": "Example Bar Chart",
		"noY": true,
		"height": "300px",
		"width": "500px",
		"background": "#FFFFFF",
		"shadowDepth": "1"
	};
	MaterialCharts.bar("#bar-chart-example", exampleBarChartData)

	var examplePieChartData = {
		"dataset": {
			"values": [5, 30, 5, 20, 40,60],
			"labels": [
				"Apples", 
				"Oranges", 
				"Berries", 
				"Peaches", 
				"Bananas",
                "Stabeery"
			],
		},
		"title": "Example Pie Chart",
		"height": "300px",
		"width": "500px",
		"background": "#FFFFFF",
		"shadowDepth": "1"
	};
	MaterialCharts.pie("#pie-chart-example", examplePieChartData)
});