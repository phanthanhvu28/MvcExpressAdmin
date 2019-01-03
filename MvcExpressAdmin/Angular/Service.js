app.service("myService", function ($http) {

    //get All GhiChu
    this.getGhiChu = function () {
        return $http.get("AngularMvc/getAll");
    };   
});