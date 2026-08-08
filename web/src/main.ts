import {GiphyApi, type Gif} from "./api"
import './style.css'

const form = document.querySelector<HTMLFormElement>("form")!;
const input = document.querySelector<HTMLInputElement>("input")!;
const results = document.querySelector<HTMLElement>("#results")!;
const status = document.querySelector<HTMLElement>("#status")!;

var api = new GiphyApi();

form.addEventListener("submit", async (event: SubmitEvent) => {
  event.preventDefault();

  const query = input.value.trim();

  status.innerText = "Searching...";
  results.innerHTML = "";

  try {
    const gifs = await api.search(query);
    
    renderGifs(gifs);
    
    status.textContent = `Found ${gifs.length} gifs`;
  }
  catch (error) {
    console.error(error);
    status.textContent = "Oh no, an error!";
  }
});

async function loadTrending(): Promise<void> {
  status.textContent = "Loading...";

  try {
    const gifs = await api.trending();
    
    renderGifs(gifs);
  } catch (error) {
    console.error(error);
    status.textContent = "Oh no, an error1";
  }
};

function renderGifs(gifs: Gif[]): void {
  results.innerHTML = gifs
    .map(
      gif => `
        <article>
          <img src="${gif.images.original.url}" alt="${gif.title}" />
        </article>
      `,
    )
    .join("");
}

loadTrending();
