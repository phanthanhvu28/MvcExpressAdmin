app.controller("myCntrl", function ($scope, myService) {
    $scope.divEmployee = false;
    GetAllGhiChu();
    //To Get All Records  
    function GetAllGhiChu() {
        var getData = myService.getGhiChu();
        getData.then(function (emp) {
            $scope.ghichus = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }  
    
});