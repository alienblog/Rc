var admin = window.admin = {};

admin.category = {};
admin.article = {};
admin.article.list = {};
admin.article.edit = {};

(function (a) {
    var TableInit = function () {
        var oTableInit = new Object();

        oTableInit.Init = function () {
            $("#tb_categories").bootstrapTable({
                url: "/api/Category",
                method: 'get',
                toolbar: '#toolbar',
                striped: true,
                cache: false,
                pagination: true,
                sortable: true,
                sortOrder: 'asc',
                queryParams: oTableInit.queryParams,
                sidePagination: 'server',
                pageNumber: 1,
                pageSize: 10,
                pageList: [10, 25, 50],
                search: false,
                uniqueId: 'Id',
                showToggle: true,
                columns: [{
                    checkbox: true
                }, {
                        field: 'Name',
                        title: '分类'
                    }, {
                        field: 'Sort',
                        title: '排序'
                    }]
            });
        }

        oTableInit.queryParams = function (params) {
            var temp = {
                limit: params.limit,
                offset: params.offset
            };

            return temp;
        }

        return oTableInit;
    }

    var ButtonInit = function () {
        var oInit = new Object();
        var postdata = {};

        oInit.Init = function () {
            $("#btn_add").click(function () {
                $("#myModalLabel").text("新增");
                $("#myModal").find(".form-control").val("");
                $('#myModal').modal();

                postdata.Id = 0;
            });

            $("#btn_edit").click(function () {
                var arrselections = $("#tb_categories").bootstrapTable('getSelections');
                if (arrselections.length > 1) {
                    toastr.warning('只能选择一行进行编辑');

                    return;
                }
                if (arrselections.length <= 0) {
                    toastr.warning('请选择有效数据');

                    return;
                }
                $("#myModalLabel").text("编辑");
                console.log(arrselections[0]);
                $('input[name="name"]').val(arrselections[0].Name);
                $('input[name="sort"]').val(arrselections[0].Sort);

                postdata.Id = arrselections[0].Id;
                $('#myModal').modal();
            });

            $("#btn_delete").click(function () {
                var arrselections = $("#tb_categories").bootstrapTable('getSelections');
                if (arrselections.length <= 0) {
                    toastr.warning('请选择有效数据');
                    return;
                }

                bootbox.confirm("确认要删除选择的数据吗？", function (e) {
                    if (arrselections.length > 1) {
                        toastr.warning('只能选择一行进行编辑');

                        return;
                    }
                    $.ajax({
                        type: "delete",
                        url: "/api/Category?Id=" + arrselections[0].Id,
                        success: function (data, status) {
                            if (status == "success") {
                                if (data.success) {
                                    toastr.success('提交数据成功');
                                    $('#myModal').modal('hide');
                                    $("#tb_categories").bootstrapTable('refresh');
                                }
                                else {
                                    toastr.error(data.errorMessage);
                                }
                            }
                        },
                        error: function () {
                            toastr.error('Error');
                        },
                        complete: function () {

                        }

                    });
                });
            });

            $("#btn_submit").click(function () {
                postdata.Name = $('input[name="name"]').val();
                postdata.Sort = ($('input[name="sort"]').val() || 0) * 1;
                $.ajax({
                    type: "post",
                    url: "/api/Category",
                    data: postdata,
                    success: function (data, status) {
                        if (status == "success") {
                            if (data.success) {
                                toastr.success('提交数据成功');
                                $("#tb_categories").bootstrapTable('refresh');
                                $('#myModal').modal('hide');
                            } else {
                                toastr.error(data.errorMessage);
                            }
                        }
                    },
                    error: function () {
                        toastr.error('Error');
                    },
                    complete: function () {

                    }

                });
            });

            $("#btn_query").click(function () {
                $("#tb_categories").bootstrapTable('refresh');
            });
        };

        return oInit;
    };

    a.init = function () {
        var oTable = new TableInit();
        oTable.Init();

        var oButton = new ButtonInit();
        oButton.Init();
    }
})(admin.category);

(function (list) {
    var TableInit = function () {
        var oInit = new Object();

        oInit.init = function () {
            $("#tb_articles").bootstrapTable({
                url: "/api/Article",
                method: 'get',
                toolbar: '#toolbar',
                striped: true,
                cache: false,
                pagination: true,
                sortable: true,
                sortOrder: 'asc',
                sidePagination: 'server',
                pageNumber: 1,
                pageSize: 10,
                pageList: [10, 25, 50],
                search: false,
                uniqueId: 'Id',
                showToggle: true,
                columns: [{
                    checkbox: true
                }, {
                        field: 'Title',
                        title: '标题'
                    }, {
                        field: 'CategoryName',
                        title: '分类'
                    }, {
                        field: 'CreatedDateString',
                        title: '创建时间'
                    },{
                        field: 'IsDraft',
                        title: '草稿'
                    }]
            });
        }

        return oInit;
    }

    var ButtonInit = function () {
        var oInit = new Object();
        var postdata = {};

        oInit.init = function () {
            $('#btn_edit').click(function () {
                var arrselections = $("#tb_articles").bootstrapTable('getSelections');
                if (arrselections.length <= 0) {
                    toastr.warning('请选择有效数据');
                    return;
                }
                location.href = "/Admin/Admin/ArticleEdit?id=" + arrselections[0].Id;
            });

            $('#btn_delete').click(function () {
                var arrselections = $("#tb_articles").bootstrapTable('getSelections');
                if (arrselections.length <= 0) {
                    toastr.warning('请选择有效数据');
                    return;
                }
                bootbox.confirm("确认要删除选择的数据吗？", function (e) {
                    if (arrselections.length > 1) {
                        toastr.warning('只能选择一行进行编辑');

                        return;
                    }
                    $.ajax({
                        type: "delete",
                        url: "/api/Article?Id=" + arrselections[0].Id,
                        success: function (data, status) {
                            if (status == "success") {
                                if (data.success) {
                                    toastr.success('提交数据成功');
                                    $("#tb_articles").bootstrapTable('refresh');
                                }
                                else {
                                    toastr.error(data.errorMessage);
                                }
                            }
                        },
                        error: function () {
                            toastr.error('Error');
                        },
                        complete: function () {

                        }

                    });
                });
            });
        };

        return oInit;
    }

    list.init = function () {
        var oTable = new TableInit();
        oTable.init();

        var oButton = new ButtonInit();
        oButton.init();
    }
})(admin.article.list);

(function (edit) {
    var editor;
    var tagCtl;
    var EditInit = function () {
        var oInit = new Object();


        oInit.init = function () {

            editor = editormd("editormd", {
                width: "100%",
                height: 640,
                syncScrolling: "single",
                path: "/Scripts/editor/lib/",
                saveHTMLToTextarea: true,
                imageUpload: true,
                imageUploadURL: '/api/File'
            });


            var id = $('input[name="articleId"]').val();
            var tagUrl = '/api/Tag';
            if (id) {
                tagUrl += "?id=" + id;
            }
            $.get(tagUrl, function (result) {

                var suggestions = [];
                var tags = [];
                if (result.allTags) {
                    suggestions = result.allTags.map(function (t) {
                        return t.Name;
                    });
                }
                if (result.tags) {
                    tags = result.tags.map(function (t) {
                        return t.Name;
                    });
                }
                edit.tagCtl = tagCtl = $("#tags").tags({
                    tagSize: "lg",
                    suggestions: suggestions,
                    tagData: tags
                });
            });
        }

        return oInit;
    }

    var ButtonInit = function () {
        var oInit = new Object();
        var postdata = {};

        function applyData() {
            postdata.Id = ($('input[name="articleId"]').val() || 0) * 1;
            postdata.Title = $('input[name="title"]').val();
            postdata.Summary = $('textarea[name="summary"]').val();
            postdata.CategoryId = $('select[name="category"]').val();
            postdata.Markdown = editor.getMarkdown();
            postdata.Content = editor.getHTML();
            postdata.PicUrl = $('input[name="picUrl"]').val();
            postdata.Tags = tagCtl.getTags().map(function (t) {
                return { Name: t }
            });
            if (!postdata.PicUrl) {
                postdata.PicUrl = getPicUrl(postdata.Markdown);
            }
        };

        function getPicUrl(md) {
            var reg = /\/Uploads[\S]+.[\w]/;
            var results = reg.exec(md);
            if (results && results.length > 0) {
                return results[0];
            }
            return "";
        }

        function saveArticle() {
            applyData();
            $.ajax({
                type: "post",
                url: "/api/Article",
                data: postdata,
                success: function (data, status) {
                    if (status == "success") {
                        if (data.success) {
                            $('input[name="articleId"]').val(data.Id);
                            toastr.success('提交数据成功');
                        } else {
                            toastr.error(data.errorMessage);
                        }
                    }
                },
                error: function () {
                    toastr.error('Error');
                },
                complete: function () {

                }

            });
        }

        oInit.init = function () {
            $('#btn_save').click(function () {
                postdata.IsDraft = false;
                saveArticle();
            });
            
            $('#btn_save_draft').click(function(){
                postdata.IsDraft = true;
                saveArticle();
            });

            $('#btn_edit').click(function () {
                var arrselections = $("#tb_articles").bootstrapTable('getSelections');
                if (arrselections.length <= 0) {
                    toastr.warning('请选择有效数据');
                    return;
                }
                location.href = "/Admin/Admin/ArticleEdit?id=" + arrselections[0].Id;
            });
        };

        return oInit;
    }

    edit.init = function () {
        var oEdit = new EditInit();
        oEdit.init();

        var oButton = new ButtonInit();
        oButton.init();
    }
})(admin.article.edit);
