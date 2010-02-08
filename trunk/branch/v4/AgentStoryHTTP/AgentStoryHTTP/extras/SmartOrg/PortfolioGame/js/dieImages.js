function DieImages(imageFolder, extension, count, type) {
	this.imageFolder = imageFolder;
	this.extension = extension;
	this.count = count;
	this.type = type;
	this.currentImagePointer = 0;
	this.hasNextImage = hasNextImage;
	this.nextImage = nextImage;
	this.reset = reset;
	this.isOfType = isOfType;
}

function hasNextImage() {
	return (this.currentImagePointer<this.count);
}

function nextImage() {
	var image = this.imageFolder+(this.currentImagePointer+1)+this.extension;
	this.currentImagePointer++;
	return image;
}

function reset() {
	this.currentImagePointer = 0;
}

function isOfType(type) {
	return type == this.type;
}
