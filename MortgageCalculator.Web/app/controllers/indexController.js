'use strict';

app.controller("indexController", ['$http', '$scope', 'appConfig', function ($http, $scope, appConfig) {
    $scope.mortgageData = {};

    $scope.getMortgageDetails = function () {
        var url = "";

        if ($scope.mortgageType === "")
            url = appConfig.apiBaseUrl + "api/mortgage";
        else
            url = appConfig.apiBaseUrl + "api/mortgage/getbytype/" + $scope.mortgageType;

        $http.get(url).then(function (results) {
            $scope.mortgageData = results.data;
        });
    };

    $scope.interest = function (mortgage) {

        var interest = 0;

        if (mortgage.InterestRepayment === 0) //interest only loans
        {
            var interestRate = mortgage.InterestRate / 100;
            interest = $scope.amount * $scope.duration * interestRate;
        }
        else if (mortgage.InterestRepayment === 1) // principal and interest loans
        {
            var months = $scope.duration * 12;
            var roi = mortgage.InterestRate / 12 / 100;
            var emi = $scope.amount * roi * Math.pow((1 + roi), months) / (Math.pow((1 + roi), months) - 1);
            var totalPayment = emi * months;
            interest = totalPayment - $scope.amount;
        }

        return interest;
    };

}]);