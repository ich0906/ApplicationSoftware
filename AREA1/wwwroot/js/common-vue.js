Vue.mixin({
    methods: {
        fileDownload : function (url) {
            $.fileDownload(url, {
                httpMethod: 'GET',
                prepareCallback: function (url) {
                   // loader = Vue.$loading.show();
                },
                abortCallback: function (url) {
                    hideLoader();
                },
                successCallback: function (url) {
                    hideLoader();
                },
                failCallback: function (responseHtml, url, error) {
                    hideLoader();
                }
            });
        }
    }
});

// #########################################################
// Vue JS filter
// #########################################################
Vue.filter('dateTime', function (dateStr) {
    return moment(dateStr).format('YYYY-MM-DD HH:mm');
});

Vue.filter('date', function (dateStr) {
    return moment(dateStr).format('YYYY-MM-DD');
});

Vue.filter('prettyBytes', function (num) {
    // jacked from: https://github.com/sindresorhus/pretty-bytes
    if (typeof num !== 'number' || isNaN(num)) {
      throw new TypeError('Expected a number');
    }

    var exponent;
    var unit;
    var neg = num < 0;
    var units = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

    if (neg) {
        num = -num;
    }

    if (num < 1) {
        return (neg ? '-' : '') + num + ' B';
    }

    exponent = Math.min(Math.floor(Math.log(num) / Math.log(1000)), units.length - 1);
    num = (num / Math.pow(1000, exponent)).toFixed(2) * 1;
    unit = units[exponent];

    return (neg ? '-' : '') + num + ' ' + unit;
});


//#########################################################
// Loading overlay
//#########################################################
let loader = null;
let reqCnt = 0;

function hideLoader() {
    if (reqCnt < 1) {
        loader && loader.hide();
        loader = null;
    }
}

axios.interceptors.request.use(function (config) {
    reqCnt++;

    if (!loader) {
        loader = Vue.$loading.show();
    }
    
    return config
}, function (error) {
    reqCnt = 0;
    hideLoader();

    return Promise.reject(error);
});

axios.interceptors.response.use(function (response) {
    reqCnt--;

    hideLoader();

    let data = response.data;

    if (data.hasOwnProperty('errorCount') && data.hasOwnProperty('fieldErrors')) {
        if (data.errorCount > 0) {
            let errorMessage = '';
            let inputField = '';

            $.each(data.fieldErrors, function (index, collection) {
                inputField = collection.field;

                if (inputField != '') {
                    errorMessage += inputField + ' : ' + collection.message + '\n';
                } else {
                    errorMessage += collection.message + '\n';
                }
            });

            alert(errorMessage);

            return new Promise(function () {
                
            });
            //throw new axios.Cancel('Operation canceled by the user.');
            //return Promise.reject('');
        }
    }
    
    return response;
}, function (error) {
    alert('?§Î•òÍ∞Ä Î∞úÏÉù?òÏ??µÎãà??');
    reqCnt = 0;
    hideLoader();
    
    return Promise.reject(error);
});


//#########################################################
//File upload list
//#########################################################
Vue.component('file-view-list', {
    template: '<div>' + 
              '<div v-for="item in fileList" class="board_viewfile">' +
              '<span>?åÏùº : </span><a href="#" @click="fileDownload(item.download)">{{ item.fileName }}</a>' + 
              '<span class="format">[ {{ item.fileSize | prettyBytes }} ]</span> ' + 
              '</div>' + 
              '</div>',
    props: {
        storageId: {
            type: String,
            required: true
        },
        viewMode: {
            type: String,
            default: 'true'
        }
    },
    data: function () {
        return {
            fileList: []
        }
    },
    mounted: function () {
    },
    methods: {
        getData: function (attachId) {
            if (attachId != null) {
                axios.post('/common/file/UploadFileList.do', { storageId: this.storageId, attachId: attachId })
                .then(function (response) {
                    this.fileList = response.data;
                }.bind(this));
            }
        }
    }
});

Vue.component('file-upload-list', {
	template: '<div>' + 
	'<div v-for="item in fileList" class="board_viewfile">' +
	'<span>?åÏùº : </span><a href="#" @click="fileDownload(item.download)">{{ item.fileName }}</a>' + 
	'<span class="format">[ {{ item.fileSize | prettyBytes }} ]</span> ' + 
	'<span v-show="viewMode === \'false\'"><a href="#" @click="deleteFile(item.storageId, item.attachId, item.fileSn)">[??†ú]</a></span>' +
	'</div>' + 
	'</div>',
	props: {
		storageId: {
			type: String,
			required: true
		},
		viewMode: {
			type: String,
		default: 'true'
		}
	},
	data: function () {
		return {
			fileList: []
		}
	},
	mounted: function () {
	},
	methods: {
		getData: function (attachId) {
			if (attachId != null) {
				axios.post('/common/file/UploadFileList.do', { storageId: this.storageId, attachId: attachId })
				.then(function (response) {
					this.fileList = response.data;
				}.bind(this));
			}
		},
		deleteFile: function (storageId, attachId, fileSn) {
			axios.post('/common/file/UserUploadFileDelete.do', { storageId: this.storageId, attachId: attachId, fileSn: fileSn })
			.then(function (response) {
				this.getData(attachId);
			}.bind(this));
			
		}
	}
});


//#########################################################
// File upload
//#########################################################
Vue.component('file-upload', {
    template: '<div><input type="file" ref="uploadFile" style="width: 100%;" ></div>',
    props: {
        multiple: {
            type: Boolean
        },
        storageId: {
            type: String,
            required: true
        },
        attachId: {
            type: String
        },
        accept : {
        	type: String
        },        
        params : {
            type: String
        }
    },
    data: function () {
        return {
            
        }
    },
    mounted: function () {
        if (this.multiple === 'true') {
            this.$refs.uploadFile['multiple'] = true;    
        }
        if (this.accept == undefined || this.accept == null) {
        	this.$refs.uploadFile['accept'] = '*/*';
        } else {
        	this.$refs.uploadFile['accept'] = accept + '/*';
        }
    },
    methods: {
        reset: function () {
            let $fileInput = this.$refs.uploadFile.value = '';
        },
        upload: function () {
            if (this.attachId === undefined || this.attachId === null) {
                this.attachId = '';
            }
            
            let $fileInput = this.$refs.uploadFile;
            let formData = new FormData();
            formData.append('storageId', this.storageId);
            formData.append('attachId', this.attachId);
            formData.append('params', this.params);
            
            if ($fileInput.files.length > 0) {
                for (let i = 0, len = $fileInput.files.length; i < len; i++) {
                    formData.append('files[' + i + ']', $fileInput.files[i]);
                }
                axios.post('/File/UserUploadFile', formData, {
                    headers: { 
                        'Content-Type': 'multipart/form-data;'
                    },
                    onUploadProgress: function (progressEvent) {
                        let uploadPercentage = parseInt(Math.round((progressEvent.loaded * 100) / progressEvent.total));
                    }
                }).then(function (response) {
                    let attachId = response.data;
                    
                    //console.log('##### attachId : ' + attachId);
                    this.$emit('upload-complete', attachId);
                }.bind(this))
                .catch(function (ex) {
                    this.$emit('upload-fail', attachId);
                });
            } else {
                //console.log('?ÖÎ°ú???Ä???åÏùº???ÜÏäµ?àÎã§!!');
                this.$emit('upload-complete');
            }
        }
    }
});
