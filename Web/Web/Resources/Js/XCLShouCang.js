var XCLShouCang = {
    XCLJsonMessageName: "",
    RootURL: "",
    PageConfig: {},
    Init: function () {
        var mainThis = this;

        $("input").iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'icheckbox_square-blue',
            increaseArea: '20%'
        });

        $(".XCLToolTip").popover();
        $(".XCLToolTipHover").popover({trigger:"hover"});
        
        $(".divMenuItem a").hover(function () {
            $(".divMenuItem a").removeClass("activeItem");
            $(this).addClass("activeItem");
        }, function () {
            $(".divMenuItem a").removeClass("activeItem");
            $(".divMenuItem a.currentMenu").addClass("activeItem");
        });
    },
    SelectMenu: function ($menu) {
        $menu.addClass("activeItem").addClass("currentMenu");
    }
};
$(function () {
    XCLShouCang.Init();
});



XCLShouCang.Home = {
    ServerNoticeHub: null,
    TMSearchHasDone:false,//查询是否结束
    Init: function () {
        var that = this;

        XCLShouCang.SelectMenu($("#menuTBSearch"));

        var $txtTMSearchKey = $("#txtTMSearchKey"), $btnSearchType = $("#btnSearchType"), $btnSearchTypeMenu = $("#btnSearchTypeMenu");
        $txtTMSearchKey.on("keypress", function (event) {
            if (event.keyCode == 13) {
                that.TMSearch();
                return false;
            }
        }).on("focus", function () {
            $(this).select();
        });
        $("#btnTMSearch").on("click", function () {
            that.TMSearch();
            return false;
        });
        that.InitSetSearchTypeSelect($btnSearchTypeMenu.find("a:eq(0)"));
        $btnSearchTypeMenu.find("a").on("click", function () {
            that.InitSetSearchTypeSelect($(this));
        });

        $("body").on("click", "#btnRatePageView", function () {
            var url=$(this).attr("xcl-url"),title=$(this).attr("xcl-title");
            art.dialog.open(url, {title:title,width:1000,height:700});
            return false;
        });

        //查询服务通知连接
        that.ServerNoticeHub = $.connection.XCLHub;
        that.ServerNoticeHub.client.showProductListProcess = function (data) {
            if (that.TMSearchHasDone) {
                return false;
            }
            var rendered = template(that.TempID.divSearchingTemp, data);
            $("#alterTMSearch").html(rendered);
        };
    },
    InitSetSearchTypeSelect: function ($menuItem) {
        var $txtTMSearchKey = $("#txtTMSearchKey"), $btnSearchType = $("#btnSearchType"), $btnSearchTypeMenu = $("#btnSearchTypeMenu");
        var val = $menuItem.attr("xcl-value"), txt = $menuItem.text();
        $txtTMSearchKey.attr({ "placeholder": $menuItem.attr("xcl-tip") });
        $btnSearchType.find(".btnText").text(txt);
        $btnSearchType.attr({ "xcl-value": val });
        $("#hdSearchTypeValue").val(val);
    },
    TempID:{
        divSearchingTemp:"divSearchingTemp",//执行中模板
        divSearchSuccessTemp:"divSearchSuccessTemp",//执行成功模板
    },
    TMSearch: function () {
        var that = this;
        var $txtTMSearchKey = $("#txtTMSearchKey");
        var $btnTMSearch = $("#btnTMSearch");
        var $alterTMSearch = $("#alterTMSearch");
        var $btnSearchType = $("#btnSearchType");
        var $hdSearchTypeValue=$("#hdSearchTypeValue");
        
        var formData = $btnTMSearch.closest("form").serialize();

        var $divBuyerInfo = $("#divBuyerInfo");
        

        var typeIsUrl=$hdSearchTypeValue.val() == "1";

        $txtTMSearchKey.val($.trim($txtTMSearchKey.val()));

        //输入框验证
        if ($txtTMSearchKey.val() == "") {
            art.dialog.tips("请输入【" + $btnSearchType.text() + "】！");
            return false;
        }

        if (typeIsUrl) {
            if ($txtTMSearchKey.val().toUpperCase().indexOf("HTTP://") < 0) {
                $txtTMSearchKey.val("http://" + $txtTMSearchKey.val());
            }
        }

        if (typeIsUrl && !/^http[s]?:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/.test($txtTMSearchKey.val())) {
            art.dialog.tips("您输入的链接无效！");
            return false;
        }

        if ($txtTMSearchKey.prop("disabled")) {
            return false;
        }

        //禁止或解除禁止 输入
        var disabledForm =function(state) {
            $txtTMSearchKey.prop({ "disabled": state });
            $btnSearchType.prop({ "disabled": state });
            $btnTMSearch.prop({ "disabled": state });
            that.TMSearchHasDone = !state;
        };
        disabledForm(true);

       $divBuyerInfo.hide();
       $alterTMSearch.html(template(that.TempID.divSearchingTemp, { Message: "查询中，请稍后..." })).fadeIn();

       var gofun = function () {
           ///<summary>
           ///ajax查询数据
           ///</summary>

           if ($hdSearchTypeValue.val() == "2") {//淘宝账号查询，直接输出的是html
               $.ajax({
                   type: "POST",
                   dataType: "html",
                   url: XCLShouCang.RootURL + "TMSearch/GoSearch",
                   data: formData + "&connectionID=" + $.connection.hub.id,
                   success: function (data) {
                       if (data.match("^\{(.+:.+,*){1,}\}$")) {
                           data = $.parseJSON(data);
                           data=data[XCLShouCang.XCLJsonMessageName] || data;
                           if (!data.IsSuccess) {
                               $alterTMSearch.html(data.Message);
                           }
                       } else {
                           $alterTMSearch.hide();
                           $divBuyerInfo.html(data).fadeIn();
                       }
                   },
                   error: function () {
                       $alterTMSearch.html("出错啦，请重试！");
                   },
                   complete: function () {
                       //$.connection.hub.stop();
                       disabledForm(false);
                   }
               });
           } else {
               $.ajax({
                   type: "POST",
                   dataType: "JSON",
                   url: XCLShouCang.RootURL + "TMSearch/GoSearch",
                   data: formData + "&connectionID=" + $.connection.hub.id,
                   success: function (data) {
                       data = data[XCLShouCang.XCLJsonMessageName] || data;
                       if (data.IsSuccess) {
                           var rendered = template(that.TempID.divSearchSuccessTemp, data.CustomObject);
                           $alterTMSearch.html(rendered);
                       } else {
                           $alterTMSearch.html(data.Message);
                       }
                   },
                   error: function () {
                       $alterTMSearch.html("出错啦，请重试！");
                   },
                   complete: function () {
                       //$.connection.hub.stop();
                       disabledForm(false);
                   }
               });
           }

       };

        //如果长连接已连接，则直接查询，否则start连接，再查询
       if (that.ServerNoticeHub && that.ServerNoticeHub.connection.state == $.signalR.connectionState.connected) {
           gofun();
       } else {
           $.connection.hub.start({ transport: ['longPolling', 'webSockets'] }).done(function () {
               gofun();
           });
       }

    }
};


XCLShouCang.TMSearch = {};
XCLShouCang.TMSearch.ProductList = {
    Init: function () {
        $(".XCLProductToolTip").popover({ html: true,trigger: "hover" });
    }
};

XCLShouCang.TMSearch.ProductReport = {
    Init: function () {

        var searchKeyID= $("#SearchKeyID").val();

        $.getJSON(XCLShouCang.RootURL + "ProductReport/GetTopProductCount", { searchKeyID: searchKeyID }, function (data) {
            var chartsData = { ShopName: [], Rep_ProductCount: [] };
            if (data && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    chartsData.ShopName.push(data[i].ShopName);
                    chartsData.Rep_ProductCount.push(data[i].Rep_ProductCount);
                }
            }

            $("#divProductCountRep").highcharts({
                chart: {
                    type: 'column',
                    margin: [50, 50, 100, 80]
                },
                title: {
                    text: '宝贝数量最多的前20位商家'
                },
                xAxis: {
                    categories: chartsData.ShopName,
                    labels: {
                        rotation: -45,
                        align: 'right',
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '宝贝数'
                    }
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    pointFormat: '<b>{point.y} </b>',
                },
                series: [{
                    name: 'Population',
                    data: chartsData.Rep_ProductCount,
                    dataLabels: {
                        enabled: true
                    }
                }]
            });
        });

        $.getJSON(XCLShouCang.RootURL + "ProductReport/GetProductCountGroupProvince", { searchKeyID: searchKeyID }, function (data) {
            var chartsData = { ProductProvince: [], Rep_ProductCount: [] };
            if (data && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    chartsData.ProductProvince.push(data[i].ProductProvince);
                    chartsData.Rep_ProductCount.push(data[i].Rep_ProductCount);
                }
            }

            $("#divProductProvinceCountRep").highcharts({
                chart: {
                    type: 'column',
                    margin: [50, 50, 100, 80]
                },
                title: {
                    text: '宝贝所在地'
                },
                xAxis: {
                    categories: chartsData.ProductProvince,
                    labels: {
                        rotation: -45,
                        align: 'right',
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '宝贝数'
                    }
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    pointFormat: '<b>{point.y} </b>',
                },
                series: [{
                    name: 'Population',
                    data: chartsData.Rep_ProductCount,
                    dataLabels: {
                        enabled: true
                    }
                }]
            });
        });

    }
};




XCLShouCang.Express = {};
XCLShouCang.Express.ExpressSearch = {
    Init: function () {
        XCLShouCang.SelectMenu($("#menuExpress"));
    }
}


XCLShouCang.Tb11 = {};
XCLShouCang.Tb11.Index = {
    Init: function () {
        XCLShouCang.SelectMenu($("#menuTb11"));
        $(".XCLRemarkToolTip").popover({ placement: 'left', html: true, trigger: "hover" });
        $(".XCLProductToolTip").popover({ html: true, trigger: "hover" });
    }
}
