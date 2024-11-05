const lenis = new Lenis()

lenis.on('scroll', (e) => {
  if (e.targetScroll > 150) {
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
  lenis.raf(time * 700)
})

gsap.ticker.lagSmoothing(0)

gsap.registerPlugin(ScrollTrigger,MotionPathPlugin,Flip)

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

  smoothLines = document.querySelectorAll('[data-line-auto]');
  smoothLines.forEach(function(lineDiv) {

      var delay = parseInt(lineDiv.getAttribute('data-line-delay')) || 0;
      new SmoothLine(lineDiv, delay);

      
  });

  document.querySelectorAll('[data-smooth-scroll]').forEach(function(link) {
    link.addEventListener('click', function(e) {
      e.preventDefault();
      var target = document.querySelector(link.getAttribute('href'));
      lenis.scrollTo(target, 2000);
    });
  });

});

document.addEventListener('DOMContentLoaded', function() {
  var burger = document.querySelector('.navbar .burger');
  var menu = document.querySelector('.navbar .right');

  burger.addEventListener('click', function() {
    menu.classList.toggle('active');
    burger.classList.toggle('active');
  });
})