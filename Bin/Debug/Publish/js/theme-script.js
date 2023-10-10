jQuery(document).ready(function ($) {
	"use strict";
/*------------------------------- Slider ---------------------------------------*/
    
   $(function(){
       $('#home-slider').slick({
           dots: false,
           infinite: true,
           speed: 500,
           fade: true,
           autoplay: true,
           arrows: false,
           cssEase: 'linear'
       });
       $('#blog-slider').slick({
           dots: true,
           infinite: true,
           speed: 500,
           fade: true,
           autoplay: true,
           arrows: false,
           cssEase: 'linear'
       });
       
       $('#testimonial-images').slick({
           slidesToShow: 1,
           slidesToScroll: 1,
           arrows: false,
           fade: true,
           autoplay: true,
           asNavFor: '#testimonial-desc'
       });
       $('#testimonial-desc').slick({
           slidesToShow: 1,
           slidesToScroll: 1,
           asNavFor: '#testimonial-images',
           dots: true,
           centerMode: false,
           focusOnSelect: true,
           arrows: false
       });
       
       $('#professional-image').slick({
           slidesToShow: 1,
           slidesToScroll: 1,
           arrows: false,
           fade: true,
           autoplay: true,
           asNavFor: '#professional-desc'
       });
       $('#professional-desc').slick({
           slidesToShow: 1,
           slidesToScroll: 1,
           asNavFor: '#professional-image',
           dots: true,
           centerMode: false,
           focusOnSelect: true,
           arrows: false
       });
       
       $('#sprdct-image').slick({
           slidesToShow: 1,
           slidesToScroll: 1,
           arrows: false,
           fade: true,
           autoplay: false,
           asNavFor: '#sprdct-thumb'
       });
       $('#sprdct-thumb').slick({
           slidesToShow: 4,
           slidesToScroll: 1,
           asNavFor: '#sprdct-image',
           dots: false,
           centerMode: false,
           focusOnSelect: true,
           arrows: true,
           responsive: [
               {
                   breakpoint: 680,
                   settings: {
                       slidesToShow: 3
                   }
               },
               {
                   breakpoint: 540,
                   settings: {
                       slidesToShow: 2
                   }
               },
               {
                   breakpoint: 420,
                   settings: {
                       slidesToShow: 1
                   }
               }
           ]
       });


    });
    
/*------------------------------- input upload ---------------------------------------*/
    $(document).on('change', '.btn-file :file', function() {
      var input = $(this),
          numFiles = input.get(0).files ? input.get(0).files.length : 1,
          label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
      input.trigger('fileselect', [numFiles, label]);
    });

   $( function() {
        $('.btn-file :file').on('fileselect', function(event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

            if( input.length ) {
                input.val(log);
            } else {
                if( log ) alert(log);
            }

        });
    });
/*------------------------------- wow paralax ---------------------------------------*/
    $(function() {
        new WOW().init();
    });
    
/*------------------------------- Pretty photo ---------------------------------------*/
    $(function(){
		$("a[data-rel^='prettyPhoto']").prettyPhoto({
			autoplay: false,
			social_tools: false
		});
	});
/*------------------------------- Counter up ---------------------------------------*/
    $(function(){
        $('.counter').counterUp({
            delay: 10,
            time: 1000
        });
    });
    
/*------------------------------- menuhover ---------------------------------------*/
        
    $(function(){
        $('ul.nav li.dropdown').hover(function() {
            $(this).find('.dropdown-menu').stop(true, true).delay(0).fadeIn(0);}, function() {
            $(this).find('.dropdown-menu').stop(true, true).delay(0).fadeOut(0);
        });
    });
    

/*------------------------------- gallery ---------------------------------------*/
    $(function() {
        function lightboxPhoto() {
            
            jQuery("a[data-rel^='prettyPhoto']").prettyPhoto({
                animationSpeed:'fast',
                slideshow:5000,
                show_title:true,
                overlay_gallery: false,
                social_tools:false
            });
	
        }
	
        if(jQuery().prettyPhoto) {
            lightboxPhoto(); 
        }
	
	
        if (jQuery().quicksand) {
            // Clone applications to get a second collection
            var $data = $("#stage").clone();

            //NOTE: Only filter on the main portfolio page, not on the subcategory pages
            $('#filter-gallery li').click(function(e) {
                $("#filter-gallery li").removeClass("active");
                // Use the last category class as the category to filter by. This means that multiple categories are not supported (yet)
                var filterClass=$(this).attr('class').split(' ').slice(-1)[0];

                if (filterClass == 'all') {
                    var $filteredData = $data.find('.gallery-item');
                } else {
                    var $filteredData = $data.find('.gallery-item[data-type=' + filterClass + ']');
                }
                $("#stage").quicksand($filteredData, {
                    duration: 600,
                    adjustHeight: 'auto'
                }, 
                                      
                function(){
                    lightboxPhoto();
                });
		
                $(this).addClass("active");
                return false;
            });
        }//if quicksand
    });
    /*------------------------------- Pannel ---------------------------------------*/
    $(function(){
        $(".panel").on("show.bs.collapse hide.bs.collapse", function(e) {
        if (e.type=='show'){
          $(this).addClass('active');
        }else{
          $(this).removeClass('active');
        }
        });
    });
    
});
    
