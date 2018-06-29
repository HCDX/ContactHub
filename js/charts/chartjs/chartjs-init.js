var gridbordercolor = "#eee";

var InitiateChartJS = function () {
    return {
        init: function (bData,lData,page) {

           
            var barChartData = {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                datasets: [
                    {
                        fillColor: "#dce1e5",
                        strokeColor: "#dce1e5",
                        highlightStroke: "#fff",
                        highlightFill: "#fff",
                        scaleFontColor: "#FFFFFF",
						scaleShowGridLines : false,
						data: bData
                    }
                ]

            };
			
			
			
			 var lineChartData = {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                datasets: [
                    {
                        fillColor: "#dce1e5",
                        strokeColor: "#dce1e5",
                        highlightStroke: "#fff",
                        highlightFill: "#fff",
                        scaleFontColor: "#FFFFFF",
						scaleShowGridLines : false,
                        data: lData 
                    }
                ]

            };
			
			var DashbarChartData = {
                labels: ["J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D"],
                datasets: [
                    {
                        fillColor: "#8cc474",
                        strokeColor: "#8cc474",
                        highlightStroke: "#38990e",
                        highlightFill: "#38990e", 
                        scaleFontColor: "#FFFFFF",
						scaleShowGridLines : true,
						data: bData
                    }
                ]

            };
			
			 var DashlineChartData = {
                labels: ["J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D"],
                datasets: [
                    {
                        fillColor: "#8cc474",
                        strokeColor: "#8cc474",
                        highlightStroke: "#38990e",
                        highlightFill: "#38990e", 
                        data: lData 
                    }
                ]

            };
			
			if(page=="def")
			{
					new Chart(document.getElementById("line").getContext("2d")).Line(lineChartData, {
					scaleFontColor: "#FFFFFF", scaleFontSize: 12,scaleShowGridLines : false,
					 scaleLineColor: "#FFFFFF"
				});
				
				
				 
				 new Chart(document.getElementById("bar").getContext("2d")).Bar(barChartData, {
					scaleFontColor: "#FFFFFF", scaleFontSize: 12,scaleShowGridLines : false,
					scaleBeginAtZero: true, scaleLineColor: "#FFFFFF"
				});
			}
			else if (page=="dash")
			{
					new Chart(document.getElementById("line").getContext("2d")).Line(DashlineChartData, {
					scaleFontColor: "#7897b4",  scaleFontSize: 12 
				});
				
				
				 
				 new Chart(document.getElementById("bar").getContext("2d")).Bar(DashbarChartData, {
					scaleFontColor: "#7897b4", scaleFontSize: 12 
				});
			
			}

            
        }
    };
}();
