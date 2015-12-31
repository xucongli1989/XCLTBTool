//by:xcl @2012.8  qq:80213876
;(function ($) { 
    $.extend({
        XCLTableList:function(options){
            options = $.extend({},funs.Defaults, options);
            funs.Init(options);
            $(options.tableClass).each(function(){

                /******ȫѡ��ؿ�ʼ*****/
                //����ȫѡʱ
                $(options.checkAllClass).on("ifChanged", function () {
                    var $ckAll=$(this).closest(options.tableClass).find(options.checkAllClass);
                    var $ckItem=$(this).closest(options.tableClass).find(options.checkItemClass);
                    if(this.checked){
                        $ckItem.prop({"checked":true}).parent().addClass("checked");
                    }else{
                        $ckItem.prop({"checked":false}).parent().removeClass("checked");
                    }
                    funs.GetCheckBoxIDs($ckAll,$ckItem);
                });
                //�����б��е�checkboxʱ
                $(options.checkItemClass).on("ifChanged", function () {
                    var $ckAll=$(this).closest(options.tableClass).find(options.checkAllClass);
                    var $ckItem=$(this).closest(options.tableClass).find(options.checkItemClass);
                    var flag=1;
                    $ckItem.each(function(){
                        if(!this.checked){
                            flag=0;
                            return false;
                        }
                    });
                    if(flag==1){
                        $ckAll.prop({"checked":true}).parent().addClass("checked");
                    }else{
                        $ckAll.prop({"checked":false}).parent().removeClass("checked");
                    }
                    funs.GetCheckBoxIDs($ckAll,$ckItem);
                });
                /******ȫѡ��ؽ���*****/

            });
        }
    });
    var funs={
        Defaults:{
            tableClass:".tableList",//table��class
            checkAllClass:".checkAll",//ȫѡ��ťclass
            checkItemClass:".checkItem"//ѡ����class
        },
        Init:function(options){
            //�����Ϊѡ��ʱ����ʱѡ��ȫѡ��
            $(options.tableClass).each(function(){
                    var $ckAll=$(this).closest(options.tableClass).find(options.checkAllClass);
                    var $ckItem=$(this).closest(options.tableClass).find(options.checkItemClass);
                    var isAllChecked = ($ckItem.length > 0 && $ckItem.filter(":checked").length == $ckItem.length);
                    if (isAllChecked) {
                        $ckAll.prop({"checked":true}).parent().addClass("checked");
                    }
                    funs.GetCheckBoxIDs($ckAll, $ckItem);
            });
        },
        //���б��е�checkbox��value��������ʽ�浽ȫѡ��checkbox��value��
        GetCheckBoxIDs:function(ckAll,ckItem){
            var v=[];
            ckItem.each(function(){
                if(this.checked){
                    v.push(this.value);
                }
            });
            ckAll.val(v.toString());
        }
    }
})(jQuery);