export type Gif = {
  id: string,
  title: string,
  url: string,
  images: {
    original: {
      url: string,
      width: string,
      height: string
    }
  },
};

export type GifResponse = {
  data: Gif[]
};

export class GiphyApi {
  constructor(private readonly baseUrl: string = "/api/v1") {}

  async trending(): Promise<Gif[]> {
    const data = await this.get<GifResponse>("/trending");
    
    return data.data;
  }

  async search(query: string): Promise<Gif[]> {
    if (!query) {
      return this.trending();
    }

    const params = new URLSearchParams({keyword: query});

    const data = await this.get<GifResponse>(`/search/${params}`);
    
    return data.data;
  }

  private async get<T>(path: string): Promise<T> {
    const response = await fetch(`${this.baseUrl}${path}`);

    if (!response.ok) {
      throw new Error(`Api request failed: ${response.status}`);
    }

    return response.json() as Promise<T>;
  }
}
