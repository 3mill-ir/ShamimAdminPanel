function ConfigsFormSubmit() {
    var color = document.getElementsByClassName("theme-color active")[0].getAttribute("data-color");
    document.getElementById('ColorInput').value = color;
    var BackGroundColor = document.getElementsByClassName("bg-color active")[0].getAttribute("data-color");
    document.getElementById('BackGroundInput').value = BackGroundColor;
    var Theme = document.getElementsByClassName("theme active")[0].getAttribute("data-theme");
    document.getElementById('ThemeInput').value = Theme;
    var c1 = document.getElementById('switch-sidebar'); c1.value = c1.checked;
    var c2 = document.getElementById('switch-sidebar-hover'); c2.value = c2.checked;
    var c3 = document.getElementById('switch-submenu-hover'); c3.value = c3.checked;
    var c4 = document.getElementById('switch-topbar'); c4.value = c4.checked;
    var c5 = document.getElementById('switch-boxed'); c5.value = c5.checked;
}
function destroySideScroll() {
    $('.sidebar-inner').mCustomScrollbar("destroy");
}
function createSideScroll() {
    if ($.fn.mCustomScrollbar) {
        destroySideScroll();
        if (!$('body').hasClass('sidebar-collapsed') && !$('body').hasClass('sidebar-collapsed') && !$('body').hasClass('submenu-hover') && $('body').hasClass('fixed-sidebar')) {
            $('.sidebar-inner').mCustomScrollbar({
                scrollButtons: {
                    enable: false
                },
                autoHideScrollbar: true,
                scrollInertia: 150,
                theme: "light-thin",
                advanced: {
                    updateOnContentResize: true
                }
            });
        }
        if ($('body').hasClass('sidebar-top')) {
            destroySideScroll();
        }
    }
}
function SetConfigs(color, background, Theme, boxedLayout, FixedTopbar, SubmenuonHover, SidebaronHover, FixedSidebar) {
    handleLayout();
    toggleBuilder()
    if (color != '') {
        mainColor();
        var a1 = document.querySelector('[data-color="' + color + '"]').id;
        document.getElementById(a1).click();
    }
    if (background != '') {
        backgroundColor();
        var a2 = document.querySelector('[data-color="' + background + '"]').id;
        document.getElementById(a2).click();
    }
    if (Theme != '') {
        handleTheme();
        var a3 = document.querySelector('[data-theme="' + Theme + '"]').id;
        document.getElementById(a3).click();
    }
    var c1 = document.getElementById('switch-sidebar');
    var c2 = document.getElementById('switch-sidebar-hover');
    var c3 = document.getElementById('switch-submenu-hover');
    var c4 = document.getElementById('switch-topbar');
    var c5 = document.getElementById('switch-boxed');
    if (FixedSidebar == 'True' && $.cookie('fixed-sidebar') == null)
        c1.click();
    if (FixedSidebar == 'False' && $.cookie('fixed-sidebar') == '1')
        c1.click();

    if (SidebaronHover == 'True' && $.cookie('sidebar-hover') == null)
        c2.click();
    if (SidebaronHover == 'False' && $.cookie('sidebar-hover') == '1')
        c2.click();

    if (SubmenuonHover == 'True' && $.cookie('submenu-hover') == null)
        c3.click();
    if (SubmenuonHover == 'False' && $.cookie('submenu-hover') == '1')
        c3.click();

    if (FixedTopbar == 'True' && $.cookie('fluid-topbar') == '1')
        c4.click();
    if (FixedTopbar == 'False' && $.cookie('fluid-topbar') == null)
        c4.click();

    if (boxedLayout == 'True' && $.cookie('boxed-layout') == null)
        c5.click();

    if (boxedLayout == 'False' && $.cookie('boxed-layout') == '1') {
        c5.click();
        c5.click();
    }
    //if (FixedTopbar == 'True')
    //    document.getElementById('PipoPageTittle').style = "padding: 0 25px;height: 165px;float: right;";
    //else
    //    document.getElementById('PipoPageTittle').style = "";
}