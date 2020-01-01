/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    var roxyFileman = '../../../../fileman/index.html?integration=ckeditor';
    config.filebrowserBrowseUrl = roxyFileman;
    config.removeDialogTabs = 'link:upload;image:upload';
    config.extraAllowedContent = true;
    config.allowedContent = true;
    config.extraPlugins = 'slideshow,contextmenu,panel,fakeobjects,floatpanel,menu';
    config.filebrowserImageBrowseUrl = roxyFileman + '&type=image';
};
