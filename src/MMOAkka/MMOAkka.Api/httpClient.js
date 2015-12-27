(function (module) {

    var injectParams = ['$http', '$q'];

    var httpRepository = function ($http, $q) {

        var getData = function (url) {
            var deferred = $q.defer();
            $http({ method: 'GET', url: url, contentType: 'application/json; charset=utf-8' }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });

            return deferred.promise;
        };

        var deleteData = function (url) {
            var deferred = $q.defer();
            $http({ method: 'DELETE', url: url, contentType: 'application/json; charset=utf-8' }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });

            return deferred.promise;
        };

        var patchData = function (url, jsonData) {
            var deferred = $q.defer();
            $http(
                {
                    method: 'PATCH', url: url,
                    data: JSON.stringify(jsonData), headers: { 'Content-Type': 'application/json' }
                })
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                    }).
                    error(function (data, status, headers, config) {
                        deferred.reject(status);
                    });
            return deferred.promise;
        };

        var putData = function (url, jsonData) {
            var deferred = $q.defer();
            $http(
                {
                    method: 'PUT', url: url,
                    data: JSON.stringify(jsonData), headers: { 'Content-Type': 'application/json' }
                })
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                    }).
                    error(function (data, status, headers, config) {
                        deferred.reject(status);
                    });
            return deferred.promise;
        };

        var postData = function (url, jsonData) {
            var deferred = $q.defer();
            $http(
                {
                    method: 'POST', url: url,
                    data: JSON.stringify(jsonData), headers: { 'Content-Type': 'application/json' }
                })
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject(status);
                    });
            return deferred.promise;
        };

        return {
            getData: getData,
            postData: postData,
            deleteData: deleteData,
            patchData: patchData,
            putData: putData
        };
    };

    httpRepository.$inject = injectParams;

    module.factory('httpRepository', httpRepository);

}(angular.module('mmoApp')));