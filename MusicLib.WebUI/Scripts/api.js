const api = {
    utils: {
        successCallback: function (apiResult, callback) {
            if (!apiResult.IsSuccess) {
                api.utils.handleError(apiResult.Error);
            } else {
                callback(apiResult.Result);
            }
        },
        handleError: function (message) {
            console.error(message);
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_DANGER,
                title: 'Some error. See console for details',
                message: message
            });
        }
    }

};

function createApiService(module) {
    module.service('apiService', function ($http) {
        this.list = {
            url: '/api/audio/list',
            send: function (callback) {
                return $http({
                    method: "get",
                    url: this.url
                }).then(function (response) {
                    api.utils.successCallback(response.data, callback);
                }, function (response) {
                    api.utils.handleError(response.statusText);
                });
            }
        };

        this.upload = {
            url: '/api/audio/upload',
            send: function (fileinput, callback) {
                var fd = new FormData();
                fd.append('file', fileinput.files[0]);

                return $http({
                    method: "post",
                    url: this.url,
                    data: fd,
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (response) {
                    api.utils.successCallback(response.data, callback);
                }, function (response) {
                    api.utils.handleError(response.statusText);
                });
            }
        };

        this.delete = {
            url: '/api/audio/delete',
            send: function (id, callback) {
                return $http({
                    method: "post",
                    url: (this.url + '?id=' + id)
                }).then(function (response) {
                    api.utils.successCallback(response.data, callback);
                }, function (response) {
                    api.utils.handleError(response.statusText);
                });
            }
        };
    });
}
