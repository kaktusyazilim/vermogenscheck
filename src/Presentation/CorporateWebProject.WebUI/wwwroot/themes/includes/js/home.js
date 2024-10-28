


window.onload = function() {

    var processSections = document.querySelectorAll('.process-sec');
    processSections.forEach(function(section) {
        var body = section.querySelector('.body');

        



        if (window.innerWidth < 991) {
            var TL = gsap.timeline({
                scrollTrigger: {
                    trigger: section,
                    start: '50% center',
                    end: `+=${body.offsetHeight + 2500}`,
                    toggleActions: 'play none none reverse',
                    markers: false,
                    pin: true,
                    scrub: 0.2,
                    anticipatePin: 1,
                },
                ease: 'none',
            });
    
            var cards = body.querySelectorAll('.card');
    
            Array.from(cards).reverse().forEach(function(card, index) {
                let Y = 0 - (index * 20);
                gsap.set(card, {
                    zIndex: index,
                    y: Y + 50,
                });
            });
    
            cards.forEach(function(card, index) {
                let Scale = 1 - (0.03 * index) ;
    
                gsap.set(card, {
                    scale: Scale,
                });
            });
    
            
            cards.forEach(function(card, index) {
    
                if (index === 0) {
                    TL.to(card, {
                        y: -650,
                        duration: 1,
                        delay: 0.05
                    }, 0);
                }
                else if (index === cards.length - 2) {
                    TL.to(card, {
                        y: -650,
                        duration: 1
                    },'-=0.4');
                }
    
                else if (index !== cards.length - 1) {
                    TL.to(card, {
                        y: -750,
                        duration: 1
                    },'-=0.4');
                }
                else {
    
                }
            });
        }
        else {
            var TL = gsap.timeline({
                scrollTrigger: {
                    trigger: section,
                    start: '30% center',
                    end: `+=${body.offsetHeight + 3500} 100%`,
                    toggleActions: 'play none none reverse',
                    markers: false,
                    pin: true,
                    scrub: 0,
                },
                ease: 'none',
            });
    
            var cards = body.querySelectorAll('.card');
    
            Array.from(cards).reverse().forEach(function(card, index) {
                let Y = 0 - (index * 20);
                gsap.set(card, {
                    zIndex: index,
                    y: Y + 50,
                });
            });
    
            cards.forEach(function(card, index) {
                let Scale = 1 - (0.03 * index) ;
    
                gsap.set(card, {
                    scale: Scale,
                });
            });
    
            
            cards.forEach(function(card, index) {
    
                if (index === 0) {
                    TL.to(card, {
                        y: -750,
                        duration: 1
                    }, 0);
                }
                else if (index === cards.length - 2) {
                    TL.to(card, {
                        y: -600,
                        duration: 1
                    },'-=0.4');
                }
    
                else if (index !== cards.length - 1) {
                    TL.to(card, {
                        y: -750,
                        duration: 1
                    },'-=0.4');
                }
                else {
    
                }
            });
        }
    });
}