const lenis = new Lenis()

lenis.on('scroll', (e) => {
  if (e.targetScroll > 30) {
    document.body.dataset.isScrolled = true
  }
  else {
    document.body.dataset.isScrolled = false
  }

  if (e.direction === -1) {
    document.body.dataset.scrollDirection = 'up'
  }
  else {
    document.body.dataset.scrollDirection = 'down'
  }

  lazyLoad();
})

lenis.on('scroll', ScrollTrigger.update)

gsap.ticker.add((time)=>{
  lenis.raf(time * 1000)
})

gsap.ticker.lagSmoothing(0)

gsap.registerPlugin(ScrollTrigger,MotionPathPlugin)

ScrollTrigger.defaults({
    toggleActions: 'play none none none',
    markers: false
    })


function lazyLoad() {
    var assets = document.querySelectorAll("img,video,iframe.lazy");
    Array.from(assets).
    filter(function(asset) { return asset.hasAttribute('data-src');})
    .forEach(function(asset) {
        if (asset.getBoundingClientRect().top < window.innerHeight +200) {
            asset.src = asset.getAttribute('data-src');
            asset.removeAttribute('data-src');
            asset.onload = function() {
                if (asset.tagName === 'IMG') {
                    asset.classList.add('lazy-active');
                }
            };
    
            if (asset.tagName === 'VIDEO') {
                asset.onended = function() {
                    asset.play();
                };
            }
        }
    }); 
    if (document.querySelectorAll('img[data-src]').length === 0 && 
    document.querySelectorAll('video[data-src]').length === 0 && 
    document.querySelectorAll('iframe[data-src]').length === 0) {
        window.removeEventListener('scroll', lazyLoad);
    }
    }


document.addEventListener('DOMContentLoaded', function() {
  lazyLoad();
  console.log(window.innerWidth, window.innerHeight)


  var navbar = document.querySelector('.navbar');
  var navbarBurger = navbar.querySelector('.burger');
  var navbarMobileMenu = navbar.querySelector('.mobile-menu');
  var mobileMenuClose = navbarMobileMenu.querySelector('.close');
  navbarBurger.addEventListener('click', function() {
    navbarMobileMenu.classList.add('active');
  });

  mobileMenuClose.addEventListener('click', function() {
    navbarMobileMenu.classList.remove('active');
  });



  var faqs = document.querySelectorAll('.faqs .faq');
  Array.from(faqs).forEach(function(faq) {
    faq.addEventListener('click', function() {
      faqs.forEach(function(faq) {
        if (faq.classList.contains('active')){
          faq.classList.remove('active');
          var icon = faq.querySelector('.icon img');
          icon.src = icon.src.replace('minus', 'plus');
        }
      });



      faq.classList.toggle('active');
      var icon = faq.querySelector('.icon img');
      if (faq.classList.contains('active')) {
        icon.src = icon.src.replace('plus', 'minus');
      }
      else {
        icon.src = icon.src.replace('minus', 'plus');
      }
    });
  });
  
  if (window.innerWidth < 992) {
    console.log('mobile')
    var mobileFlicktys = document.querySelectorAll('[data-mobile-flickity]');
  

    Array.from(mobileFlicktys).forEach(function(mobileFlickty) {
      var buttons=false;
      if (mobileFlickty.dataset.mobileFlickity === 'buttons') {
        buttons = true;
      }


      var flickty = new Flickity(mobileFlickty, {
        cellAlign: 'left',
        pageDots: (buttons ? false : true),
        prevNextButtons: buttons,
        draggable: true,
        freeScroll: false,
      });
    });


    var footerMenus = document.querySelectorAll('.footer .menus .menu');
    Array.from(footerMenus).forEach(function(footerMenu) {
      var menuToggle = footerMenu.querySelector('h2');
      menuToggle.addEventListener('click', function() {
        footerMenus.forEach(function(footerMenu) {
          footerMenu.classList.remove('active');
          
        });
        footerMenu.classList.toggle('active');
      });
    });
  }

});