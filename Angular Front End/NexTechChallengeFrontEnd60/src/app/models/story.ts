export class Story {
  by: string;
  descendants: number;
  id: number;
  kids: [number];
  score: number;
  time: number;
  title: string;
  type: string;
  url: string

  static createNew(): Story {
    let newStory = new Story();
    return newStory;
  }
}