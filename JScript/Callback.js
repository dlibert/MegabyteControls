function Callback(beforecallback, endcallback,cbref) {
    this.CallbackReference = cbref;
    this.BeforeCallback = beforecallback;
    this.EndCallback = endcallback;
    this.PerformCallback = function () {this.BeforeCallback(); DoCallback(this); };
}

function DoCallback(context) {
    __theFormPostData = '';
    WebForm_InitCallback();
    context.CallbackReference();
}