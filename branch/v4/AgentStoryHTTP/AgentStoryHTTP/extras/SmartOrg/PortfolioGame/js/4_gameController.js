var gameController = {
	TWENTY_SIDED_IMAGE_FOLDER: "./../extras/SmartOrg/PortfolioGame/images/20/",
	SIX_SIDED_IMAGE_FOLDER: "./../extras/SmartOrg/PortfolioGame/images/6/",
	B4_SIDED_IMAGE_FOLDER: "./../extras/SmartOrg/PortfolioGame/images/b4/",
	W4_SIDED_IMAGE_FOLDER: "./../extras/SmartOrg/PortfolioGame/images/w4/",
	PNG: ".png",
	TWENTY_SIDED_TYPE: "20-SIDED",
	SIX_SIDED_TYPE: "6-SIDED",
	B4_SIDED_TYPE: "4-SIDED-BLACK",
	W4_SIDED_TYPE: "4-SIDED-WHITE",
	
	loadImagesIntoBrowserCache: function() {
		this.die = this.createAllDie();
		if (document.images) 
		{
			for (i=0;i<this.die.length;i++) {
				while (this.die[i].hasNextImage()) {
					img = new Image();
					img.src = this.die[i].nextImage();
				}
			}
		}
	},
	createAllDie: function() {
		var die = new Array();
		die[0] = this.createTwentySidedDie();
		die[1] = this.createSixSidedDie();
		die[2] = this.createWhiteFourSidedDie();
		die[3] = this.createBlackFourSidedDie();
		return die;
	},
	createTwentySidedDie: function() {
		return new DieImages(this.TWENTY_SIDED_IMAGE_FOLDER, this.PNG, 20, this.TWENTY_SIDED_TYPE);
	}, 
	createSixSidedDie: function() {
		return new DieImages(this.SIX_SIDED_IMAGE_FOLDER, this.PNG, 6, this.SIX_SIDED_TYPE);
	},
	createWhiteFourSidedDie: function() {
		return new DieImages(this.W4_SIDED_IMAGE_FOLDER, this.PNG, 4, this.W4_SIDED_TYPE);
	},
	createBlackFourSidedDie: function() {
		return new DieImages(this.B4_SIDED_IMAGE_FOLDER, this.PNG, 4, this.B4_SIDED_TYPE);
	},
	dieForType: function(type) {
		var match = false;
		for (i=0;i<this.die.length && !match; i++) {
			if (this.die[i].isOfType(type)) {
				match = true;
			}
		}
		if (match) {
			this.die[i-1].reset();
			return this.die[i-1];
		} else {
			throw("Could not find die for type "+type);
		}
	}
}
