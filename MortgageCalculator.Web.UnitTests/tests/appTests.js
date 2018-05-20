describe('Mortgage Calculator App Tests', function () {
    beforeEach(module('MortgageCalculatorApp'));

    beforeEach(function () {
        module("MortgageCalculatorApp", function ($provide) {
            $provide.constant('appConfig', {
                apiBaseUrl: 'http://localhost:49608/'
            });
        });
    });

    describe("Index Contoller Test Suite", function () {
        var $scope, httpBackend;

        beforeEach(module("MortgageCalculatorApp"));

        beforeEach(inject(function ($controller, $rootScope, $httpBackend) {
            $scope = $rootScope.$new();
            $scope.mortgageData = {};
            $scope.amount = 100000;
            $scope.duration = 5;

            $controller("indexController", { $scope: $scope });

            httpBackend = $httpBackend;
            
        }));

        var mortgages = [
            {
                "MortgageId": 4,
                "Name": "Variable Home Loan (Principal and Interest)",
                "MortgageType": 0,
                "InterestRepayment": 1,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 5.12
            },
            {
                "MortgageId": 3,
                "Name": "Variable Home Loan (Interest Only)",
                "MortgageType": 0,
                "InterestRepayment": 0,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 5.24
            },
            {
                "MortgageId": 6,
                "Name": "Variable Investment Loan (Interest Only)",
                "MortgageType": 0,
                "InterestRepayment": 0,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 5.39
            },
            {
                "MortgageId": 5,
                "Name": "Variable Investment Loan (Principal and Interest)",
                "MortgageType": 0,
                "InterestRepayment": 1,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 5.99
            },
            {
                "MortgageId": 2,
                "Name": "Fixed Home Loan (Principal and Interest)",
                "MortgageType": 1,
                "InterestRepayment": 1,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 4.81
            },
            {
                "MortgageId": 1,
                "Name": "Fixed Home Loan (Interest Only)",
                "MortgageType": 1,
                "InterestRepayment": 0,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 4.99
            },
            {
                "MortgageId": 8,
                "Name": "Fixed Investment Loan (Interest Only)",
                "MortgageType": 1,
                "InterestRepayment": 0,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 5.19
            },
            {
                "MortgageId": 7,
                "Name": "Fixed Investment Loan (Principal and Interest)",
                "MortgageType": 1,
                "InterestRepayment": 1,
                "EffectiveStartDate": "2018-05-19T11:54:54.4114543+05:30",
                "EffectiveEndDate": "2019-05-20T11:54:54.4114543+05:30",
                "TermsInMonths": 12,
                "CancellationFee": 259.99,
                "EstablishmentFee": 199.99,
                "SchemaIdentifier": "",
                "InterestRate": 5.89
            }];

        it("Get all mortgages", function () {
            $scope.mortgageType = "";

            httpBackend.expectGET("http://localhost:49608/api/mortgage").respond(mortgages);

            $scope.getMortgageDetails();

            httpBackend.flush();

            expect($scope.mortgageData.length).toBe(8);

        });

        it("Get Fixed type mortgages", function () {
            $scope.mortgageType = "0";

            httpBackend.expectGET("http://localhost:49608/api/mortgage/getbytype/0").respond(mortgages.filter(function (item) { return item.MortgageType == 0 }));

            $scope.getMortgageDetails();

            httpBackend.flush();

            expect($scope.mortgageData.length).toBe(4);

        });

        it("Get Variable type mortgages", function () {
            $scope.mortgageType = "1";

            httpBackend.expectGET("http://localhost:49608/api/mortgage/getbytype/1").respond(mortgages.filter(function (item) { return item.MortgageType == 1 }));

            $scope.getMortgageDetails();

            httpBackend.flush();

            expect($scope.mortgageData.length).toBe(4);

        });

        it("Variable Home Loan (Principal and Interest)", function () {

            var interest = parseFloat($scope.interest(mortgages[0]).toFixed(2));

            expect(interest).toBe(13557.56);

        });

        it("Variable Home Loan (Interest Only)", function () {

            var interest = parseFloat($scope.interest(mortgages[1]).toFixed(2));

            expect(interest).toBe(26200.00);

        });

        it("Variable Investment Loan (Interest Only)", function () {

            var interest = parseFloat($scope.interest(mortgages[2]).toFixed(2));

            expect(interest).toBe(26950.00);

        });

        it("Variable Investment Loan (Principal and Interest)", function () {

            var interest = parseFloat($scope.interest(mortgages[3]).toFixed(2));

            expect(interest).toBe(15968.91);

        });

        it("Fixed Home Loan (Principal and Interest)", function () {

            var interest = parseFloat($scope.interest(mortgages[4]).toFixed(2));

            expect(interest).toBe(12705.86);

        });

        it("Fixed Home Loan (Interest Only)", function () {

            var interest = parseFloat($scope.interest(mortgages[5]).toFixed(2));

            expect(interest).toBe(24950.00);

        });

        it("Fixed Investment Loan (Interest Only)", function () {

            var interest = parseFloat($scope.interest(mortgages[6]).toFixed(2));

            expect(interest).toBe(25950.00);

        });

        it("Fixed Investment Loan (Principal and Interest)", function () {

            var interest = parseFloat($scope.interest(mortgages[7]).toFixed(2));

            expect(interest).toBe(15690.17);

        });
    });
});